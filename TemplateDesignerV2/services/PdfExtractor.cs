using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ServiceModel.Web;
using Newtonsoft.Json;
//using BitMiracle.Docotic.Pdf;
using System.ServiceModel;
using System.ServiceModel.Activation;
using TemplateDesignerModelTypesV2;
using System.Drawing;
using WebSupergoo.ABCpdf7;
using System.Drawing.Drawing2D;
using TemplateDesignerV2.Services.Utilities;

namespace TemplateDesignerV2.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PdfExtractor
    {
        [WebGet(UriTemplate = "{name}")]
        // NOTE: 
        // When used in trial mode, the library imposes some restrictions.
        // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
        // for more information.

        //public bool ConvertPdfToObj(string physicalPath, int templateId, int CustomerID)
        //{
        //    List<TemplateFonts> fontsList = new List<TemplateFonts>();
        //    using (PdfDocument pdf = new PdfDocument(physicalPath))
        //    {
        //        int TemplateId = 0;

        //        if (templateId == 0)
        //        {
        //            TemplateId = SaveTemplate(pdf.Pages[0], "user generated", 0);
        //        }
        //        else
        //        {
        //            TemplateId = UpdateTemplateDocotic(pdf.Pages[0], templateId);
        //        }
        //        fontsList = GetUserFonts(TemplateId, CustomerID);
        //        //TemplateId = SaveTemplate(pdf.Pages[0], "user generated", 350);
        //        int pageNo = 0;
        //        foreach (PdfPage page in pdf.Pages)
        //        {
        //            int PageID = 0;
        //            pageNo += 1;
        //            PageID = SaveTemplatePage(pageNo, page, TemplateId, "Front", templateId + "/Side" + (pageNo).ToString() + ".pdf");
        //            int DisplayOrderPDf = 1;
        //            foreach (PdfPageObject pageObject in page.GetObjects())
        //            {
        //                if (pageObject.Type == PdfPageObjectType.Text)
        //                { 
        //                    PdfTextData text = (PdfTextData)pageObject;
        //                    SaveTextObject(text, TemplateId, DisplayOrderPDf, PageID,fontsList);
        //                }
        //                else if (pageObject.Type == PdfPageObjectType.Image)
        //                {
        //                    PdfPaintedImage image = (PdfPaintedImage)pageObject;
        //                    SaveImageObject(image, TemplateId, DisplayOrderPDf, PageID);
        //                }

        //                DisplayOrderPDf += 1;
        //            }
        //        }
        //        if (pdf.PageCount > 0)
        //        {
        //            /// generate background files 
        //            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //            {
        //                var listPages = db.TemplatePages.Where(g => g.ProductID == templateId).ToList();
        //                TemplateSvcSP oSvc = new TemplateSvcSP();
        //                string targetFolder = "";
        //                targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
        //                foreach (var objPage in listPages)
        //                {
        //                    if (File.Exists(targetFolder + templateId + "/Side" + objPage.PageNo.ToString() + ".pdf"))
        //                    {
        //                        File.Delete(targetFolder + templateId + "/Side" + objPage.PageNo.ToString() + ".pdf");
        //                    }
        //                    if (File.Exists(targetFolder + templateId + "/templatImgBk" + objPage.PageNo.ToString() + ".jpg"))
        //                    {
        //                        File.Delete(targetFolder + templateId + "/templatImgBk" + objPage.PageNo.ToString() + ".jpg");
        //                    }
        //                }
        //                oSvc.CreateBlankBackgroundPDFsByPages(templateId, pdf.Pages[0].Height, pdf.Pages[0].Width,1, listPages);

        //            }
        //        }
        //    }
        //    return true;

        //}


        // called from mis
        public bool CovertPdfToBackground(string physicalPath, int templateID)
        {

            //PdfPage page = null;
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                List<TemplateObjects> objs = db.TemplateObjects.Where(g => g.ProductID == templateID).ToList();
                List<TemplatePages> objPages = db.TemplatePages.Where(g => g.ProductID == templateID).ToList();
                foreach (var obj in objs)
                {
                    db.TemplateObjects.DeleteObject(obj);
                }
                foreach (var obj in objPages)
                {
                    db.TemplatePages.DeleteObject(obj);
                }
                db.SaveChanges();
            }
            generatePdfAsBackgroundUpdate(physicalPath, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), templateID.ToString() + "/templatImgBk", 0, templateID);
            //setGeneratedImgAsBackground();

            return true;
        }

        public bool CovertPdfToBackgroundWithObjects(string physicalPath, int templateID)
        {


            generatePdfAsBackgroundUpdateWithObjects(physicalPath, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), templateID.ToString() + "/templatImgBk", 0, templateID);

            return true;
        }
        private bool generatePdfAsBackgroundUpdateWithObjects(string PDFDoc, string savePath, string ThumbnailFileName, double CuttingMargin, int TemplateID)
        {
            XSettings.License = "810-031-225-276-0715-601";
            using (Doc theDoc = new Doc())
            {
                try
                {
                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        List<TemplateObjects> objs = db.TemplateObjects.Where(g => g.ProductID == TemplateID).ToList();
                        List<TemplatePages> objPages = db.TemplatePages.Where(g => g.ProductID == TemplateID).ToList();
                        List<TemplateBackgroundImages> objImages = db.TemplateBackgroundImages.Where(g => g.ProductID == TemplateID).ToList();
                        List<TemplateObjects> newTemplateObjects = new List<TemplateObjects>();
                        theDoc.Read(PDFDoc);
                        // theDoc.MediaBox
                        int tID = 0;
                        tID = UpdateTemplateWithoutOBjs(theDoc, TemplateID);

                        int srcPagesID = theDoc.GetInfoInt(theDoc.Root, "Pages");
                        int srcDocRot = theDoc.GetInfoInt(srcPagesID, "/Rotate");
                        for (int i = 1; i <= theDoc.PageCount; i++)
                        {
                            theDoc.PageNumber = i;
                            theDoc.Rect.String = theDoc.CropBox.String;
                            theDoc.Rect.Inset(CuttingMargin, CuttingMargin);
                            //check if folder exist
                            if (System.IO.Directory.Exists(savePath + "/" + TemplateID.ToString()) == false)
                            {
                                System.IO.Directory.CreateDirectory(savePath + "/" + TemplateID.ToString());
                            }
                            // generate image 
                            theDoc.Rendering.DotsPerInch = 150;
                            theDoc.Rendering.Save(System.IO.Path.Combine(savePath, ThumbnailFileName) + i.ToString() + ".jpg");
                            // save template page 
                            int templatePage = SaveTemplatePage(i, TemplateID, "Front", TemplateID + "/Side" + (i).ToString() + ".pdf");
                            // update template objects
                            var oldTemplatePage = objPages.Where(g => g.PageNo == i).SingleOrDefault();
                            if (oldTemplatePage != null)
                            {
                                int oldPageID = oldTemplatePage.ProductPageID;
                                List<TemplateObjects> oldObjs = objs.Where(g => g.ProductPageId == oldPageID).ToList();
                                foreach (var obj in oldObjs)
                                {
                                    obj.ProductPageId = templatePage;
                                    newTemplateObjects.Add(obj);
                                }
                            }
                            // save pdf 
                            Doc singlePagePdf = new Doc();
                            try
                            {
                                singlePagePdf.Rect.String = singlePagePdf.MediaBox.String = theDoc.MediaBox.String;
                                singlePagePdf.AddPage();
                                singlePagePdf.AddImageDoc(theDoc, i, null);
                                singlePagePdf.FrameRect();

                                int srcPageRot = theDoc.GetInfoInt(theDoc.Page, "/Rotate");
                                if (srcDocRot != 0)
                                {
                                    singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcDocRot);
                                }
                                if (srcPageRot != 0)
                                {
                                    singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcPageRot);
                                }
                                string targetFolder = "";
                                targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                                if (File.Exists(targetFolder + TemplateID + "/Side" + i.ToString() + ".pdf"))
                                {
                                    File.Delete(targetFolder + TemplateID + "/Side" + i.ToString() + ".pdf");
                                }
                                singlePagePdf.Save(targetFolder + TemplateID + "/Side" + i.ToString() + ".pdf");
                                singlePagePdf.Clear();
                            }
                            catch (Exception e)
                            {
                                throw new Exception("GenerateTemplateBackground", e);
                            }
                            finally
                            {
                                if (singlePagePdf != null)
                                    singlePagePdf.Dispose();
                            }
                        }

                        // delete old objects and pages
                        foreach (var page in objPages)
                        {
                            List<TemplateObjects> listObjs = objs.Where(g => g.ProductPageId == page.ProductPageID).ToList();
                            foreach (var obj in listObjs)
                            {
                                db.TemplateObjects.DeleteObject(obj);
                            }
                            db.TemplatePages.DeleteObject(page);

                        }
                        db.SaveChanges();
                    }
                    theDoc.Dispose();
                    return true;

                }
                catch (Exception ex)
                {
                    throw new Exception("GeneratePDfPreservingObjects", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                }
            }

        }


        private bool generatePdfAsBackgroundUpdate(string PDFDoc, string savePath, string ThumbnailFileName, double CuttingMargin, int TemplateID)
        {
            XSettings.License = "810-031-225-276-0715-601";
            using (Doc theDoc = new Doc())
            {

                try
                {
                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        List<TemplateObjects> objs = db.TemplateObjects.Where(g => g.ProductID == TemplateID).ToList();
                        List<TemplatePages> objPages = db.TemplatePages.Where(g => g.ProductID == TemplateID).ToList();
                        List<TemplateBackgroundImages> objImages = db.TemplateBackgroundImages.Where(g => g.ProductID == TemplateID).ToList();
                        foreach (var obj in objs)
                        {
                            db.TemplateObjects.DeleteObject(obj);
                        }
                        foreach (var obj in objPages)
                        {
                            db.TemplatePages.DeleteObject(obj);
                        }
                        foreach (var obj in objImages)
                        {
                            string sfilePath = System.Web.Hosting.HostingEnvironment.MapPath("/templatedesignerv2/designer/products/" + obj.ImageName);
                            string imURL = "Designer/Products/" + obj.ImageName;

                            //delete the actual image as well
                            if (System.IO.File.Exists(sfilePath))
                                System.IO.File.Delete(sfilePath);

                            db.TemplateBackgroundImages.DeleteObject(obj);
                        }
                        db.SaveChanges();
                    }

                    theDoc.Read(PDFDoc);
                    int tID = 0;
                    tID = UpdateTemplate(TemplateID, theDoc.MediaBox.Width, theDoc.MediaBox.Height);
                    int srcPagesID = theDoc.GetInfoInt(theDoc.Root, "Pages");
                    int srcDocRot = theDoc.GetInfoInt(srcPagesID, "/Rotate");
                    for (int i = 1; i <= theDoc.PageCount; i++)
                    {
                        theDoc.PageNumber = i;
                        theDoc.Rect.String = theDoc.CropBox.String;
                        theDoc.Rect.Inset(CuttingMargin, CuttingMargin);
                        //check if folder exist
                        if (System.IO.Directory.Exists(savePath + "/" + TemplateID.ToString()) == false)
                        {
                            System.IO.Directory.CreateDirectory(savePath + "/" + TemplateID.ToString());
                        }
                        // generate image 
                        theDoc.Rendering.DotsPerInch = 150;
                        theDoc.Rendering.Save(System.IO.Path.Combine(savePath, ThumbnailFileName) + i.ToString() + ".jpg");
                        // save template page 
                        int templatePage = SaveTemplatePage(i, TemplateID, "Front", TemplateID + "/Side" + (i).ToString() + ".pdf");
                        // save pdf 
                        using (Doc singlePagePdf = new Doc())
                        {
                            try
                            {
                                singlePagePdf.Rect.String = singlePagePdf.MediaBox.String = theDoc.MediaBox.String;
                                singlePagePdf.AddPage();
                                singlePagePdf.AddImageDoc(theDoc, i, null);
                                singlePagePdf.FrameRect();

                                int srcPageRot = theDoc.GetInfoInt(theDoc.Page, "/Rotate");
                                if (srcDocRot != 0)
                                {
                                    singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcDocRot);
                                }
                                if (srcPageRot != 0)
                                {
                                    singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcPageRot);
                                }
                                string targetFolder = "";
                                targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                                if (File.Exists(targetFolder + TemplateID + "/Side" + i.ToString() + ".pdf"))
                                {
                                    File.Delete(targetFolder + TemplateID + "/Side" + i.ToString() + ".pdf");
                                }
                                singlePagePdf.Save(targetFolder + TemplateID + "/Side" + i.ToString() + ".pdf");
                                singlePagePdf.Clear();
                            }
                            catch (Exception e)
                            {
                                throw new Exception("GenerateTemplateBackground", e);
                            }
                            finally
                            {
                                if (singlePagePdf != null)
                                    singlePagePdf.Dispose();
                            }
                        }
                    }
                    //   theDoc.Dispose();
                    return true;

                }
                catch (Exception ex)
                {
                    throw new Exception("GenerateTemplateThumbnail", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                }
            }

        }


        //private bool generatePdfAsBackground(string PDFDoc, string savePath, string ThumbnailFileName, double CuttingMargin,int TemplateID,int pageHeight ,int pageWidth)
        //{
        //    XSettings.License = "810-031-225-276-0715-601";
        //    Doc theDoc = new Doc();
        //    try
        //    {
        //        using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //        {
        //            List<TemplateObjects> objs = db.TemplateObjects.Where(g => g.ProductID == TemplateID).ToList();
        //            List<TemplatePages> objPages = db.TemplatePages.Where(g => g.ProductID == TemplateID).ToList();
        //            List<TemplateBackgroundImages> objImages = db.TemplateBackgroundImages.Where(g => g.ProductID == TemplateID).ToList();
        //            foreach (var obj in objs)
        //            {
        //                db.TemplateObjects.DeleteObject(obj);
        //            }
        //            foreach (var obj in objPages)
        //            {
        //                db.TemplatePages.DeleteObject(obj);
        //            }
        //            foreach (var obj in objImages)
        //            {
        //                string sfilePath = System.Web.Hosting.HostingEnvironment.MapPath("/templatedesignerv2/designer/products/" + obj.ImageName);
        //                string imURL = "Designer/Products/" + obj.ImageName;

        //                //delete the actual image as well
        //                if (System.IO.File.Exists(sfilePath))
        //                    System.IO.File.Delete(sfilePath);

        //                db.TemplateBackgroundImages.DeleteObject(obj);
        //            }
        //            db.SaveChanges();
        //        }
        //        theDoc.Read(PDFDoc);
        //        for (int i = 1; i <= theDoc.PageCount; i++)
        //        {
        //            theDoc.PageNumber = i;
        //            theDoc.Rect.String = theDoc.CropBox.String;
        //            theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

        //            if (System.IO.Directory.Exists(savePath+ "/"+ TemplateID.ToString()) == false)
        //            {
        //                System.IO.Directory.CreateDirectory(savePath + "/" + TemplateID.ToString());
        //            }

        //            theDoc.Rendering.DotsPerInch = 300;
        //            theDoc.Rendering.Save(System.IO.Path.Combine(savePath, ThumbnailFileName) +i.ToString() +  ".jpg");


        //            string imgpath = "~/Designer/Products/" + ThumbnailFileName + i.ToString() + ".jpg";
        //            string uploadPath = HttpContext.Current.Server.MapPath(imgpath);
        //            System.Drawing.Image objImage = System.Drawing.Image.FromFile(uploadPath);
        //            int ImageWidth = objImage.Width;
        //            int ImageHeight = objImage.Height;
        //            objImage.Dispose();

        //            int templatePage = SaveTemplatePage(i, TemplateID, "Front", TemplateID + "/Side" + (i).ToString() + ".pdf");   //Naveed made concatenation of TemplateID with background file name i.e 123/side1.pdf
        //            setGeneratedImgAsBackground(TemplateID,ThumbnailFileName+i.ToString() +  ".jpg",ImageWidth,ImageHeight,templatePage);

        //        }
        //        theDoc.Dispose();
        //        TemplateSvcSP oSvc = new TemplateSvcSP();
        //        using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //        {
        //            var listPages = db.TemplatePages.Where(g => g.ProductID == TemplateID).ToList();
        //            string targetFolder = "";
        //            targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
        //            foreach (var objPage in listPages)
        //            {
        //                if (File.Exists(targetFolder + TemplateID + "/Side" + objPage.PageNo.ToString() + ".pdf"))
        //                {
        //                    File.Delete(targetFolder + TemplateID + "/Side" + objPage.PageNo.ToString() + ".pdf");
        //                }
        //            }
        //            oSvc.CreateBlankBackgroundPDFsByPages(TemplateID,Convert.ToDouble(pageHeight),Convert.ToDouble(pageWidth) , 1, listPages);
        //        }
        //        generatePdfSides(TemplateID);

        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GenerateTemplateThumbnail", ex);
        //    }
        //    finally
        //    {
        //        if (theDoc != null)
        //            theDoc.Dispose();
        //    }


        //}

        private void setGeneratedImgAsBackground(int productid, string fileName, int ImageWidth, int ImageHeight, int templatePage)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                var template = db.Templates.Where(g => g.ProductID == productid).SingleOrDefault();

                TemplateObjects objImg = new TemplateObjects();
                string imagePath = "Designer/Products/" + fileName;
                objImg.ObjectType = 3;
                objImg.Name = "Image";
                objImg.IsEditable = true;
                objImg.IsHidden = false;
                objImg.IsMandatory = false;
                objImg.PositionX = 0;
                objImg.PositionY = 0;
                objImg.MaxHeight = template.PDFTemplateHeight;
                objImg.MaxWidth = template.PDFTemplateWidth;
                objImg.FontName = "";
                objImg.FontSize = 0;
                objImg.IsBold = null;
                objImg.IsItalic = null;
                objImg.Allignment = null;
                objImg.VAllignment = 0;
                objImg.ContentString = imagePath;
                objImg.ProductID = productid;
                objImg.DisplayOrderPdf = 1;
                objImg.IsPositionLocked = false;
                objImg.IsQuickText = false;
                objImg.IsTextEditable = null;
                objImg.Opacity = 1;
                objImg.LineSpacing = null;
                objImg.ColorC = 0;///  Color C M Y K 
                objImg.ColorM = 0;
                objImg.ColorK = 0;
                objImg.ColorY = 0;
                objImg.ColorHex = "#FFF"; ///  colorhex 
                objImg.RotationAngle = 0;
                objImg.IsFontCustom = true;
                objImg.ColorType = 0;
                objImg.Tint = 0;
                objImg.IsSpotColor = false;
                objImg.ContentCaseType = 0;
                objImg.IsRequireNumericValue = false;
                objImg.RColor = 0;
                objImg.GColor = 0;
                objImg.BColor = 0;
                objImg.IsSpotColor = false;
                objImg.ProductPageId = templatePage;
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Designer/Products/" + productid.ToString())))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Designer/Products/" + productid.ToString()));
                }
                db.TemplateObjects.AddObject(objImg);

                //var bgImg = new TemplateBackgroundImages();
                //bgImg.Name = fileName;
                //bgImg.ImageName = fileName;
                //bgImg.ProductID = productid;

                //bgImg.ImageWidth = ImageWidth;
                //bgImg.ImageHeight = ImageHeight;

                //bgImg.ImageType = 2;

                //db.TemplateBackgroundImages.AddObject(bgImg);
                db.SaveChanges();
            }
        }
        private int UpdateTemplate(int templateID, double mediaBoxWidth, double mediaBoxHeight)
        {
            Templates objTemplate = new Templates();

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                objTemplate = db.Templates.Where(g => g.ProductID == templateID).SingleOrDefault();
                if (objTemplate != null)
                {
                    objTemplate.PDFTemplateWidth = mediaBoxWidth;
                    objTemplate.PDFTemplateHeight = mediaBoxHeight;
                    objTemplate.CuttingMargin = 14.173228345;
                    //  db.SaveChanges();
                }
                List<TemplateObjects> objs = db.TemplateObjects.Where(g => g.ProductID == templateID).ToList();
                List<TemplatePages> objPages = db.TemplatePages.Where(g => g.ProductID == templateID).ToList();

                foreach (var obj in objs)
                {
                    db.TemplateObjects.DeleteObject(obj);
                }
                foreach (var obj in objPages)
                {
                    db.TemplatePages.DeleteObject(obj);
                }
                db.SaveChanges();
            }
            return templateID;
        }
        //private int UpdateTemplateDocotic(PdfPage doc, int templateID)
        //{
        //    Templates objTemplate = new Templates();

        //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //    {
        //        objTemplate = db.Templates.Where(g => g.ProductID == templateID).SingleOrDefault();
        //        if (objTemplate != null)
        //        {
        //            objTemplate.PDFTemplateWidth = doc.Width;
        //            objTemplate.PDFTemplateHeight = doc.Height;
        //            objTemplate.CuttingMargin = 14.173228345;
        //            //  db.SaveChanges();
        //        }
        //        List<TemplateObjects> objs = db.TemplateObjects.Where(g => g.ProductID == templateID).ToList();
        //        List<TemplatePages> objPages = db.TemplatePages.Where(g => g.ProductID == templateID).ToList();

        //        foreach (var obj in objs)
        //        {
        //            db.TemplateObjects.DeleteObject(obj);
        //        }
        //        foreach (var obj in objPages)
        //        {
        //            db.TemplatePages.DeleteObject(obj);
        //        }
        //        db.SaveChanges();
        //    }
        //    return templateID;
        //}
        private int UpdateTemplateWithoutOBjs(Doc doc, int templateID)
        {
            Templates objTemplate = new Templates();

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                objTemplate = db.Templates.Where(g => g.ProductID == templateID).SingleOrDefault();
                if (objTemplate != null)
                {
                    objTemplate.PDFTemplateWidth = doc.MediaBox.Width;
                    objTemplate.PDFTemplateHeight = doc.MediaBox.Height;
                    objTemplate.CuttingMargin = 14.173228345;
                    //  db.SaveChanges();
                }


                db.SaveChanges();
            }
            return templateID;
        }
        //private int SaveTemplate(PdfPage page,string name,int productCategoryID)
        //{
        //    Templates objTemplate = new Templates();
        //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //    {
        //        objTemplate.ProductName = name;
        //        objTemplate.ProductCategoryID = productCategoryID;
        //        objTemplate.PDFTemplateWidth = page.Width;
        //        objTemplate.PDFTemplateHeight = page.Height;
        //        objTemplate.CuttingMargin = 14.173228345;
        //        objTemplate.Orientation = 1;
        //        objTemplate.Status = 1;
        //        objTemplate.UsedCount = 1;
        //        objTemplate.UserRating = 1;
        //        objTemplate.MPCRating = 1;
        //        db.Templates.AddObject(objTemplate);
        //        db.SaveChanges();
        //    }
        //    return objTemplate.ProductID;
        //}
        private int SaveTemplatePage(int pageNo, int ProductID, string PageName, string backgroundFileName)
        {
            TemplatePages objPage = new TemplatePages();
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                objPage.ProductID = ProductID;
                objPage.PageNo = pageNo;
                objPage.PageName = PageName;
                objPage.IsPrintable = true;
                objPage.Orientation = 1;
                objPage.BackGroundType = 1;
                objPage.BackgroundFileName = backgroundFileName;
                objPage.PageType = 1;  // pageType(1 = without color 2 = with color )  Color C  Color M  Color Y Color K   
                db.TemplatePages.AddObject(objPage);
                db.SaveChanges();
            }

            return objPage.ProductPageID;
        }
        //private int SaveTextObject(PdfTextData text, int productID, int displayOrderPDF, int productPageID, List<TemplateFonts> listFonts)
        //{
        //    TemplateObjects obj = new TemplateObjects();
        //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //    {
        //        TemplateFonts objFont = listFonts.Where(g => g.FontFile == text.Font.Name).SingleOrDefault();
        //        obj.ObjectType = 2;
        //        obj.Name = "Name";
        //        obj.IsEditable = true;
        //        obj.IsHidden = false;
        //        obj.IsMandatory = false;
        //        obj.PositionX = text.Bounds.X;
        //        obj.PositionY = text.Bounds.Y;
        //        obj.MaxHeight = text.Bounds.Height;
        //        obj.MaxWidth = text.Bounds.Width;
        //        if (objFont != null)
        //        {
        //            obj.FontName = text.Font.Name;
        //            if (text.FontSize > 6)
        //                obj.FontSize = text.FontSize;
        //            else
        //                obj.FontSize = 6;

        //        }
        //        else
        //        {
        //            obj.FontName = "Arial";
        //            obj.FontSize = 6;
        //        }
        //        obj.IsBold = text.Font.Bold;
        //        obj.IsItalic = text.Font.Italic;
        //        obj.Allignment = 1;// chk it for support
        //        obj.VAllignment = 1;
        //        obj.ContentString = text.Text;
        //        obj.ProductID = productID;
        //        obj.DisplayOrderPdf = displayOrderPDF;
        //        obj.IsPositionLocked = false;
        //        obj.IsQuickText = false;
        //        obj.IsTextEditable = false;
        //        obj.Opacity = 1; 
        //        obj.LineSpacing = 1; // chk it for support

        //        obj.ColorC = 0;///  Color C M Y K 
        //        obj.ColorM = 0;
        //        obj.ColorK = 100;
        //        obj.ColorY = 0;
        //        obj.ColorHex = "#000"; ///  colorhex 
        //        if (text.RenderingMode == PdfTextRenderingMode.Fill)
        //        {
        //            PdfBrushInfo brush = text.Brush;
        //            PdfColor clr = brush.Color;
        //            PdfCmykColor cmyk = clr as PdfCmykColor;
        //            PdfRgbColor rgb = clr as PdfRgbColor;
        //            if (cmyk != null)
        //            {
        //                obj.ColorC = cmyk.C;
        //                obj.ColorY = cmyk.Y;
        //                obj.ColorK = cmyk.K;
        //                obj.ColorM = cmyk.M;
        //                ColorDataList oData = new ColorDataList();
        //                obj.ColorHex = oData.getColorHex(cmyk.C, cmyk.M, cmyk.Y, cmyk.K);

        //            }
        //            else
        //            {
        //                if (rgb != null)
        //                {
        //                    double c = (double)(255 - rgb.R) / 255;
        //                    double m = (double)(255 - rgb.G) / 255;
        //                    double y = (double)(255 - rgb.B) / 255;
        //                    double k = (double)Math.Min(c, Math.Min(m, y));

        //                    if (k == 1.0)
        //                    {
        //                        obj.ColorC = 0;
        //                        obj.ColorM = 0;
        //                        obj.ColorY = 0;
        //                        obj.ColorK = 1;
        //                    }
        //                    else
        //                    {
        //                        obj.ColorC = Convert.ToInt32((c - k) / (1 - k));
        //                        obj.ColorM =Convert.ToInt32( (m - k) / (1 - k) );
        //                        obj.ColorY =Convert.ToInt32( (y - k) / (1 - k) );
        //                        obj.ColorK =Convert.ToInt32( k );
        //                    }
        //                    //int Red = rgb.R;
        //                    //int Green = rgb.G;
        //                    //int Blue = rgb.B;
        //                    //int Black = Math.Min(1 - Red,Math.Min( 1 - Green, 1 - Blue));
        //                    //int Cyan    = (1-Red-Black)/(1-Black);
        //                    //int Magenta = (1-Green-Black)/(1-Black);
        //                    //int Yellow  = (1-Blue-Black)/(1-Black) ;

        //                    //ColorDataList oData = new ColorDataList();
        //                    //obj.ColorHex = oData.getColorHex(Cyan,Magenta,Yellow,Black);
        //                }

        //            }

        //        }
        //        else if (text.RenderingMode == PdfTextRenderingMode.Stroke)
        //        {
        //            PdfColor clr = text.Pen.Color;

        //        }

        //        obj.RotationAngle = 0;
        //        obj.IsFontCustom = true;
        //        obj.ColorType = 3;
        //        obj.Tint = 0;
        //        obj.IsSpotColor = false;
        //        obj.ContentCaseType = 0;
        //        obj.IsRequireNumericValue = false;
        //        obj.RColor = 0;
        //        obj.GColor = 0;
        //        obj.BColor = 0;
        //        obj.IsSpotColor = false;
        //        obj.ProductPageId = productPageID;

        //        db.TemplateObjects.AddObject(obj);
        //        db.SaveChanges();

        //    }


        //    return 1;
        //}
        //private int SaveImageObject(PdfPaintedImage image, int productID, int displayOrderPDF, int productPageID)
        //{
        //    TemplateObjects obj = new TemplateObjects();
        //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //    {
        //        string imagePath ="Designer/Products/"+ productID.ToString()  + "/" + displayOrderPDF.ToString() + ".jpeg";
        //        obj.ObjectType = 3;
        //        obj.Name = "Image";
        //        obj.IsEditable = true;
        //        obj.IsHidden = false;
        //        obj.IsMandatory = false;
        //        obj.PositionX = image.Position.X;
        //        obj.PositionY = image.Position.Y;
        //        obj.MaxHeight = image.Bounds.Height;
        //        obj.MaxWidth = image.Bounds.Width;
        //        obj.FontName = "";   
        //        obj.FontSize = 0;
        //        obj.IsBold = null;  
        //        obj.IsItalic = null;
        //        obj.Allignment = null;
        //        obj.VAllignment = 0;
        //        obj.ContentString = imagePath;
        //        obj.ProductID = productID;
        //        obj.DisplayOrderPdf = displayOrderPDF;
        //        obj.IsPositionLocked = false;
        //        obj.IsQuickText = false;
        //        obj.IsTextEditable = null;
        //        obj.Opacity = 1;
        //        obj.LineSpacing = null;
        //        obj.ColorC = 0;///  Color C M Y K 
        //        obj.ColorM = 0;
        //        obj.ColorK = 0;
        //        obj.ColorY = 0;
        //        obj.ColorHex = "#FFF"; ///  colorhex 
        //        obj.RotationAngle = 0;
        //        obj.IsFontCustom = true;
        //        obj.ColorType = 0;
        //        obj.Tint = 0;
        //        obj.IsSpotColor = false;
        //        obj.ContentCaseType = 0;
        //        obj.IsRequireNumericValue = false;
        //        obj.RColor = 0;
        //        obj.GColor = 0;
        //        obj.BColor = 0;
        //        obj.IsSpotColor = false;
        //        obj.ProductPageId = productPageID;
        //        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Designer/Products/" + productID.ToString())))
        //        {
        //            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Designer/Products/" + productID.ToString()));
        //        }
        //        image.SaveAsPainted(HttpContext.Current.Server.MapPath("~/Designer/Products/" + productID.ToString() + "/" + displayOrderPDF.ToString() + ".jpeg"),PdfExtractedImageFormat.Jpeg);
        //        db.TemplateObjects.AddObject(obj);
        //        var bgImg = new TemplateBackgroundImages();
        //        bgImg.Name = productID.ToString() + "/" + displayOrderPDF.ToString() + ".jpeg";
        //        bgImg.ImageName = productID.ToString() + "/" + displayOrderPDF.ToString() + ".jpeg";
        //        bgImg.ProductID = productID;

        //        bgImg.ImageWidth =Convert.ToInt32( image.Bounds.Width);
        //        bgImg.ImageHeight = Convert.ToInt32(image.Bounds.Height) ;

        //        bgImg.ImageType = 2;

        //        db.TemplateBackgroundImages.AddObject(bgImg);
        //        db.SaveChanges();
        //    }


        //    return 1;
        //}

        //private List<TemplateFonts> GetUserFonts(int ProductId, int CustomerId)
        //{
        //    List<TemplateFonts> lFont = new List<TemplateFonts>();
        //    List<TemplateFonts> returnFonts = new List<TemplateFonts>();
        //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //    {

        //        lFont = db.TemplateFonts.Where(g => g.IsPrivateFont == false || g.CustomerID == CustomerId).ToList();
        //        foreach (var objFonts in lFont)
        //        {
        //            string path = "";
        //            if (objFonts.FontPath != null)
        //            {
        //                path = objFonts.FontPath;
        //            }
        //            else
        //            {
        //                path = "PrivateFonts/FontFace/";
        //            }

        //            objFonts.FontFile = "Designer/" + path + objFonts.FontFile;
        //            returnFonts.Add(objFonts);
        //        }
        //    }
        //    return returnFonts;
        //}

        //private void generatePdfSides(int templateID)
        //{
        //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //    {
        //        var oTemplatePages = db.TemplatePages.Where(g => g.ProductID == templateID).ToList();
        //        var objProduct = db.Templates.Where(g => g.ProductID == templateID).SingleOrDefault();
        //        string targetFolder = "";
        //        targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
        //        TemplateSvc oSvc = new TemplateSvc();

        //        foreach (TemplatePages objPage in oTemplatePages)
        //        {

        //            byte[] PDFFile = oSvc.generatePDF(objProduct, objPage, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, false, false, false);
        //            //writing the PDF to FS

        //            System.IO.File.WriteAllBytes(targetFolder + templateID + "/Side" + objPage.PageNo.ToString()  + ".pdf", PDFFile);
        //        }

        //        var objs = db.TemplateObjects.Where(g => g.ProductID == templateID).ToList();
        //        foreach (var obj in objs)
        //        {
        //            db.TemplateObjects.DeleteObject(obj);
        //        }
        //        db.SaveChanges();
        //    }

        //}



        /* used to import pdf as image for designer*/
        public List<TemplateBackgroundImages> CovertPdfToBackgroundDesigner(string physicalPath, int templateID, string uploadPath)
        {
            List<TemplateBackgroundImages> objs = generatePdfAsBackgroundDesigner(physicalPath, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + templateID.ToString() + "/"), 0, templateID, uploadPath);
            return objs;
        }
        private List<TemplateBackgroundImages> generatePdfAsBackgroundDesigner(string PDFDoc, string savePath, double CuttingMargin, int TemplateID, string rootPath)
        {
            List<TemplateBackgroundImages> objs = new List<TemplateBackgroundImages>();
            XSettings.License = "810-031-225-276-0715-601";
            Doc theDoc = new Doc();
            try
            {
                string ThumbnailFileName = "Conv_" + DateTime.Now.ToString();
                ThumbnailFileName = ThumbnailFileName.Replace("/", "_");
                ThumbnailFileName = ThumbnailFileName.Replace(" ", "_");
                ThumbnailFileName = ThumbnailFileName.Replace(":", "_");
                theDoc.Read(PDFDoc);
                for (int i = 1; i <= theDoc.PageCount; i++)
                {
                    theDoc.PageNumber = i;
                    theDoc.Rect.String = theDoc.CropBox.String;
                    theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                    if (System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Designer/Products/" + TemplateID.ToString() + "/")) == false)
                    {
                        System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Designer/Products/" + TemplateID.ToString() + "/"));
                    }
                    string imgpath = rootPath + "/" + ThumbnailFileName + i.ToString() + ".jpg";
                    int res = 150;
                    if (System.Configuration.ConfigurationManager.AppSettings["RenderingDotsPerInch"] != null)
                    {
                        res = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RenderingDotsPerInch"]);
                    }

                    theDoc.Rendering.DotsPerInch = res;
                    theDoc.Rendering.Save(HttpContext.Current.Server.MapPath(imgpath));


                    string uploadPath = HttpContext.Current.Server.MapPath(imgpath);
                    System.Drawing.Image objImage = System.Drawing.Image.FromFile(uploadPath);
                    int ImageWidth = objImage.Width;
                    int ImageHeight = objImage.Height;
                    objImage.Dispose();


                    var bgImg = new TemplateBackgroundImages();
                    bgImg.Name = ThumbnailFileName + i.ToString() + ".jpg";
                    bgImg.ImageName = ThumbnailFileName + i.ToString() + ".jpg";
                    bgImg.ProductID = TemplateID;

                    bgImg.ImageWidth = ImageWidth;
                    bgImg.ImageHeight = ImageHeight;
                    objs.Add(bgImg);
                    //saveGeneratedImg(TemplateID, ThumbnailFileName + i.ToString() + ".jpg", ImageWidth, ImageHeight);

                }
                theDoc.Dispose();

                return objs;

            }
            catch (Exception ex)
            {
                throw new Exception("GenerateTemplateThumbnail PDFExtractor -- pdf to image designer function", ex);
            }
            finally
            {
                if (theDoc != null)
                    theDoc.Dispose();
                if (File.Exists(PDFDoc))
                {
                    File.Delete(PDFDoc);
                }
            }
        }
        //private void saveGeneratedImg(int productid, string fileName, int ImageWidth, int ImageHeight)
        //{
        //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //    {
        //        string Imname = productid + "/" + fileName;
        //        var backgrounds = db.TemplateBackgroundImages.Where(g => g.ImageName == Imname).SingleOrDefault();

        //        if (backgrounds != null)
        //        {
        //            if (backgrounds.ImageHeight == ImageHeight && backgrounds.ImageWidth == ImageWidth)
        //            {
        //            }
        //        }
        //        else
        //        {
        //            var bgImg = new TemplateBackgroundImages();
        //            bgImg.Name = productid + "/" + fileName;
        //            bgImg.ImageName = productid + "/" + fileName;
        //            bgImg.ProductID = productid;
        //            bgImg.ImageWidth = ImageWidth;
        //            bgImg.ImageHeight = ImageHeight;
        //            bgImg.ImageType = 2;
        //            db.TemplateBackgroundImages.AddObject(bgImg);
        //            db.SaveChanges();
        //        }


        //    }
        //}

        ///////////////////////////////////////// Business functions /////////////////////////////////////////////

        //public byte[] generatePDF( string filePath, int pageNo)
        //{
        //        Doc doc = new Doc();
        //        try
        //        {
        //            XSettings.License = "810-031-225-276-0715-601";

        //            doc.TopDown = true;

        //            XReadOptions opt = new XReadOptions();
        //            if (File.Exists(filePath))
        //                doc.Read(filePath);
        //            doc.PageNumber = pageNo;
        //            return doc.GetData();
        //        }
        //        catch (Exception ex)
        //        {
        //            Util.LogException(ex);
        //            throw new Exception("ShowPDF", ex);
        //        }
        //        finally
        //        {
        //            doc.Dispose();
        //        }
        //}

        // used to create pdf files from pdf in designer 
        private int UpdateTemplateDesigner(int templateID, double mediaBoxWidth, double mediaBoxHeight)
        {
            Templates objTemplate = new Templates();

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                objTemplate = db.Templates.Where(g => g.ProductID == templateID).SingleOrDefault();
                if (objTemplate != null)
                {
                    objTemplate.PDFTemplateWidth = mediaBoxWidth;
                    objTemplate.PDFTemplateHeight = mediaBoxHeight;
                    objTemplate.CuttingMargin = 14.173228345;
                    //  db.SaveChanges();
                }
               
                db.SaveChanges();
            }
            return templateID;
        }
        public int generatePdfAsBackgroundDesigner(string physicalPath, int TemplateID)
        {
            int count = 0;
            XSettings.License = "810-031-225-276-0715-601";
            using (Doc theDoc = new Doc())
            {
                try
                {
                    theDoc.Read(physicalPath);
                    int tID = UpdateTemplateDesigner(TemplateID, theDoc.MediaBox.Width, theDoc.MediaBox.Height);
                    int srcPagesID = theDoc.GetInfoInt(theDoc.Root, "Pages");
                    int srcDocRot = theDoc.GetInfoInt(srcPagesID, "/Rotate");
                    for (int i = 1; i <= theDoc.PageCount; i++)
                    {
                        theDoc.PageNumber = i;
                        theDoc.Rect.String = theDoc.CropBox.String;
                        theDoc.Rect.Inset(0, 0);
                        //check if folder exist
                        if (System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Designer/Products/" + TemplateID.ToString() + "/")) == false)
                        {
                            System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Designer/Products/" + TemplateID.ToString() + "/"));
                        }
                        // generate image 
                        theDoc.Rendering.DotsPerInch = 150;
                        if (System.Configuration.ConfigurationManager.AppSettings["RenderingDotsPerInch"] != null)
                        {
                            theDoc.Rendering.DotsPerInch = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RenderingDotsPerInch"]);
                        }
                        string imgpath = "~/Designer/Products/" + TemplateID.ToString() + "/templatImgBk" + i.ToString() + ".jpg";
                        theDoc.Rendering.Save(HttpContext.Current.Server.MapPath(imgpath));

                        // save pdf 
                        Doc singlePagePdf = new Doc();
                        try
                        {
                            singlePagePdf.Rect.String = singlePagePdf.MediaBox.String = theDoc.MediaBox.String;
                            singlePagePdf.AddPage();
                            singlePagePdf.AddImageDoc(theDoc, i, null);
                            singlePagePdf.FrameRect();

                            int srcPageRot = theDoc.GetInfoInt(theDoc.Page, "/Rotate");
                            if (srcDocRot != 0)
                            {
                                singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcDocRot);
                            }
                            if (srcPageRot != 0)
                            {
                                singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcPageRot);
                            }
                            string targetFolder = "";
                            targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                            if (File.Exists(targetFolder + TemplateID + "/Side" + i.ToString() + ".pdf"))
                            {
                                File.Delete(targetFolder + TemplateID + "/Side" + i.ToString() + ".pdf");
                            }
                            singlePagePdf.Save(targetFolder + TemplateID + "/Side" + i.ToString() + ".pdf");
                            singlePagePdf.Clear();
                            count++;
                        }
                        catch (Exception e)
                        {
                            throw new Exception("GenerateTemplateBackground", e);
                        }
                        finally
                        {
                            if (singlePagePdf != null)
                                singlePagePdf.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("GeneratePDfPreservingObjects", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                }
            }
            return count;

        }
    }
}