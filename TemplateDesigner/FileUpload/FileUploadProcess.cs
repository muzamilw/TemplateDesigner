using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using WebSupergoo.ABCpdf6;
using TemplateDesigner.Services;
//using TemplateDesigner.Models;
using System.Linq;
using TemplateDesignerModelTypes;



namespace TemplateDesigner
{
    public delegate void FileUploadCompletedEvent(object sender, FileUploadCompletedEventArgs args);
    public class FileUploadCompletedEventArgs
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int ProductID { get; set; }
        public int FileType { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public bool IsOverwriting { get; set; }

        public FileUploadCompletedEventArgs() { }

        public FileUploadCompletedEventArgs(string fileName, string filePath, int productid, int filetype, int imagewidth, int imageheight, bool isOverwriting)
        {
            FileName = fileName;
            FilePath = filePath;
            ProductID = productid;
            FileType = filetype;
            ImageWidth = imagewidth;
            ImageHeight = imageheight;
            IsOverwriting = isOverwriting;
        }
    }

    public class FileUploadProcess
    {
        public event FileUploadCompletedEvent FileUploadCompleted;
        /// <summary>
        /// Determines if uploaded files should be renamed according to the user uploading them, otherwise if
        /// multiple users upload a file of the same name, it would try to save the file to the same name, throwing an error.
        /// Another way to prevent this is to create a seperate folder for each file.
        /// </summary>
        public bool UniqueUserUpload { get; set; }

        public FileUploadProcess()
        {
        }

        public void ProcessRequest(HttpContext context)
        {
            
            string filename = context.Request.QueryString["filename"];
            bool complete = string.IsNullOrEmpty(context.Request.QueryString["Complete"]) ? true : bool.Parse(context.Request.QueryString["Complete"]);
            bool getBytes = string.IsNullOrEmpty(context.Request.QueryString["GetBytes"]) ? false : bool.Parse(context.Request.QueryString["GetBytes"]);
            long startByte = string.IsNullOrEmpty(context.Request.QueryString["StartByte"]) ? 0 : long.Parse(context.Request.QueryString["StartByte"]);


            int ProductID = string.IsNullOrEmpty(context.Request.QueryString["ProductID"]) ? 0 : int.Parse(context.Request.QueryString["ProductID"]);
            int FileType = string.IsNullOrEmpty(context.Request.QueryString["FileType"]) ? 0 : int.Parse(context.Request.QueryString["FileType"]);
            bool isOverwriting = string.IsNullOrEmpty(context.Request.QueryString["IsOverWrite"]) ? false : bool.Parse(context.Request.QueryString["IsOverWrite"]);

            string uploadPath = context.Server.MapPath("~/Designer/Products/" + ProductID.ToString());

            string filePath;
            if (UniqueUserUpload)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    filePath = Path.Combine(uploadPath, string.Format("{0}_{1}", context.User.Identity.Name.Replace("\\", ""), filename));
                }
                else
                {
                    if (context.Session["fileUploadUser"] == null)
                        context.Session["fileUploadUser"] = Guid.NewGuid();
                    filePath = Path.Combine(uploadPath, string.Format("{0}_{1}", context.Session["fileUploadUser"], filename));
                }
            }
            else
            {
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
                filePath = Path.Combine(uploadPath, filename);
            }

            if (getBytes)
            {
                FileInfo fi = new FileInfo(filePath);
                if (!fi.Exists)
                    context.Response.Write("0");
                else
                    context.Response.Write(fi.Length.ToString());

                context.Response.Flush();
                return;
            }
            else
            {

                if (startByte > 0 && File.Exists(filePath))
                {

                    using (FileStream fs = File.Open(filePath, FileMode.Append))
                    {
                        SaveFile(context.Request.InputStream, fs);
                        fs.Close();
                    }
                }
                else
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        SaveFile(context.Request.InputStream, fs);
                        fs.Close();
                    }
                }

                if (complete)
                {
                    if (FileUploadCompleted != null)
                    {
                        int ImageWidth = 0;
                        int ImageHeight = 0;
                        if (FileType == 1 || FileType == 2)
                        {
                            System.Drawing.Image objImage = System.Drawing.Image.FromStream(context.Request.InputStream);
                            
                            ImageWidth = objImage.Width;
                            ImageHeight = objImage.Height;
                            objImage.Dispose();

                        }
                        else if (FileType == 3 || FileType == 4)
                        {
                            using (TemplateDesignerEntities db = new TemplateDesignerEntities())
                            {

                                double PDFRestrictedHeight = 0;
                                double PDFRestrictedWidth = 0;

                                //save the PDF file on the side
                                Templates oTemplate = db.Templates.Where(g => g.ProductID == ProductID).Single();
                                var SelectedProductCategory = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();
                                

                                if ( SelectedProductCategory.ApplySizeRestrictions.Value)
                                {
                                    PDFRestrictedHeight = AppCommon.MMToPoint( SelectedProductCategory.HeightRestriction.Value);
                                    PDFRestrictedWidth = AppCommon.MMToPoint(SelectedProductCategory.WidthRestriction.Value);
                                }

                                

                                XSettings.License = "393-927-439-276-6036-693";
                                Doc pdfDoc = new Doc();

                                pdfDoc.Read(filePath);
                                double side1PDFHeight = pdfDoc.Rect.Height;
                                double side1PDFWidth = pdfDoc.Rect.Width;
                                pdfDoc.Dispose();



                                if (SelectedProductCategory.ApplySizeRestrictions.Value)
                                {

                                    if (ComparePDFDIMwithCategoryDIM(side1PDFHeight, side1PDFWidth, PDFRestrictedHeight, PDFRestrictedWidth))
                                    {
                                        if (FileType == 3) //side 1
                                            oTemplate.LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + filename;
                                        else if (FileType == 4)
                                            oTemplate.Side2LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + filename;

                                        oTemplate.TempString = "success";

                                    }
                                    else
                                    {

                                        oTemplate.TempString = "Uploaded file dimensions does not match with product dimensions ( Width : " + PDFRestrictedWidth.ToString() + "mm  X Height : " + PDFRestrictedHeight.ToString() + "mm ). Please upload a file with correct dimensions.";


                                        //commit the file save.
                                    }
                                }
                                else
                                {
                                    if (FileType == 3) //side 1
                                        oTemplate.LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + filename;
                                    else if (FileType == 4)
                                        oTemplate.Side2LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + filename;

                                    oTemplate.TempString = "success";
                                }
                                

                                db.SaveChanges();
                            }
                        }
                        FileUploadCompletedEventArgs args = new FileUploadCompletedEventArgs(filename, filePath, ProductID, FileType, ImageWidth, ImageHeight, isOverwriting);
                        FileUploadCompleted(this, args);
                    }
                }
            }
        }

        private void SaveFile(Stream stream, FileStream fs)
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                fs.Write(buffer, 0, bytesRead);
            }
        }

        /// <summary>
        /// compares the PDF height and width with that of provided category dimensions
        /// </summary>
        /// <param name="PDFHeight"></param>
        /// <param name="PDFWidth"></param>
        /// <param name="PDFRestrictedHeight"></param>
        /// <param name="PDFRestrictedWidth"></param>
        /// <returns></returns>
        private bool ComparePDFDIMwithCategoryDIM(double PDFHeight, double PDFWidth, double PDFRestrictedHeight, double PDFRestrictedWidth)
        {

            bool result = false;
            if (Math.Ceiling(PDFRestrictedHeight) >= PDFHeight && Math.Floor(PDFRestrictedHeight) <= PDFHeight)
            {
                result = true;
            }
            else
                result = false;



            if (Math.Ceiling(PDFRestrictedWidth) >= PDFWidth && Math.Floor(PDFRestrictedWidth) <= PDFWidth)
            {
                result = true;
            }
            else
                result = false;

            return result;
        }
    }
}

