using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplateDesignerModelTypesV2;

namespace TemplateDesignerV2
{
    // template id = t // 0 in case of new 
    //category = c

    public partial class DesignerV2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = "";
                //if (System.Configuration.ConfigurationManager.AppSettings["IsPinkCards"] != null)
                //{
                //    str += " isPinkCards=" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["IsPinkCards"]) + ";";
                //}
                //if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null)
                //{
                //    double udMargin = Services.Utilities.Util.MMToPoint(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]));
                //    udMargin = Services.Utilities.Util.PointToPixel(udMargin);
                //    str += " udCutMar=" + udMargin + ";";
                //}
                if (System.Configuration.ConfigurationManager.AppSettings["highlightEditableText"] != null)
                {
                    str += " highlightEditableText=" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["highlightEditableText"]) + ";";
                }
                if (System.Configuration.ConfigurationManager.AppSettings["showEditBtn"] != null)
                {
                    str += " showEBtn=" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["showEditBtn"]) + ";";
                }
                if (System.Configuration.ConfigurationManager.AppSettings["showInlinePanel"] != null)
                {
                    str += " panelMode=" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["showInlinePanel"]) + ";";
                }
                //if (HttpContext.Current.Request.QueryString["CustomerID"] != null)
                //{
                //    if (IsCalledFrom == 2)
                //    {
                //        str += " CustomerID=" + HttpContext.Current.Request.QueryString["CustomerID"] + ";";
                //    }
                //}
                //if (IsCalledFrom == 1)
                //{
                //    if (Request.Cookies["customerid"].Value != "0")
                //    {
                //        str += " CustomerID=" + Request.Cookies["customerid"].Value + ";";
                //        if (Request.Cookies["userid"] != null)
                //        {
                //            str += " ContactID=" + Request.Cookies["userid"].Value + ";";
                //        }
                //    }
                //    else
                //    {
                //        str += " CustomerID=-999;";
                //    }

                //}

                if (HttpContext.Current.Request.QueryString["PC"] != null)
                {

                    str += " printCropMarks=" + HttpContext.Current.Request.QueryString["PC"].ToLower() + ";";
                }
                if (HttpContext.Current.Request.QueryString["PM"] != null)
                {

                    str += " printWaterMarks=" + HttpContext.Current.Request.QueryString["PM"].ToLower() + ";";
                }
                if (HttpContext.Current.Request.QueryString["OC"] != null)
                {

                    str += " orderCode=" + HttpContext.Current.Request.QueryString["OC"].ToLower() + ";";
                }
                if (HttpContext.Current.Request.QueryString["CN"] != null)
                {

                    str += " CustomerName='" + HttpContext.Current.Request.QueryString["CN"].ToLower() + "';";
                }
                //str += " Territory='" + TerritoryID.ToString() + "';";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;

                if (!cs.IsStartupScriptRegistered(cstype, "init"))
                {
                    String cstext1 = "var PType = 1; " + str;
                    cs.RegisterStartupScript(cstype, "init", cstext1, true);
                }

               
            }
        }
        #region "Get IDs"

        [WebMethod]
        public static string GetIDs()
        {
            //This is strictly here just so you can see how you can grab ID's and make plupload aware of them
            //This is useful for data driven application. Example, need the ID of the user who is uploading a file
            //so you can pull that data out later when the user logs in.

            //default is pipe delimited for js file in function OnGetIDSucceeded
            //IDOfObject|IDOfObject2
            string IDs = "0|0";

            //These IDs could be pulled from anywhere (i.e, Session, DB, XML file)

            IDs = string.Format("{0}|{1}", "10", "11"); //"10" and "11" and filler so you can have data to mess around with

            return IDs;
        }

        #endregion

        #region "Insert Attachment"

        [WebMethod]
        public static string InsertFileRecord(string idOfObject1, string idOfObject2, string fileID, string fileName, string productID, string uploadedFrom, string imageType, string contactCompanyID, string contactID)
        {
            string result = _InsertAttachment(idOfObject1, idOfObject2, fileID, fileName, uploadedFrom, imageType, contactCompanyID, contactID);
            return result;
        }

        [WebMethod]
        public static void InsertFontRecord(string fontNName, string fileName, string CustomerID)
        {
            _InsertFontAttachment(fontNName, fileName, CustomerID);
        }
        #endregion

        #region "Private _InsertAttachment"

        private static string _InsertAttachment(string idOfObject1, string idOfObject2, string fileID, string fileName, string uploadedFrom, string imageType, string contactCompanyID, string contactID)
        {

            string result = "true";
            System.Drawing.Image objImage = null;
            // fileName = fileID;
            try
            {
                string product = idOfObject1;
                string ext = System.IO.Path.GetExtension(fileName);
                fileID += ext;
                fileName = fileName.Replace("%20", " ");
                bool isUploadedPDF = false;
                List<TemplateBackgroundImages> uploadedPdfRecords = null;

                if (product != null)
                {
                    int productid = Convert.ToInt32(product);
                    int ImageWidth = 0;
                    int ImageHeight = 0;

                    string imgpath = "~/Designer/Products/" + productid;
                    if (uploadedFrom == "1" || uploadedFrom == "2")
                    {
                        imgpath = "~/Designer/Products/UserImgs/" + contactCompanyID.ToString();
                    }
                    else if (uploadedFrom == "3" || uploadedFrom == "4")
                    {
                        imgpath = "~/Designer/Products/UserImgs/Retail/" + contactCompanyID.ToString();
                    }
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(imgpath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(imgpath));
                    }
                    string RootPath = imgpath;
                    imgpath += "/" + fileID;
                    string uploadPath = HttpContext.Current.Server.MapPath(imgpath);

                    if (Path.GetExtension(uploadPath).Contains("pdf"))
                    {
                        Services.PdfExtractor obj = new Services.PdfExtractor();
                        uploadedPdfRecords = obj.CovertPdfToBackgroundDesigner(uploadPath, productid, RootPath);
                        isUploadedPDF = true;
                    }

                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        string UploadPathForPDF = productid + "/";
                        string Imname = productid + "/" + fileID;
                        if (uploadedFrom == "1" || uploadedFrom == "2")
                        {
                            Imname = "UserImgs/" + contactCompanyID.ToString() + "/" + fileID;
                            UploadPathForPDF = "UserImgs/" + contactCompanyID.ToString() + "/";
                        }
                        else if (uploadedFrom == "3" || uploadedFrom == "4")
                        {
                            Imname = "UserImgs/Retail/" + contactCompanyID.ToString() + "/" + fileID;
                            UploadPathForPDF = "UserImgs/Retail/" + contactCompanyID.ToString() + "/";
                        }
                        //var backgrounds = db.TemplateBackgroundImages.Where(g => g.ImageName == Imname).SingleOrDefault();
                        if (isUploadedPDF)
                        {
                            foreach (TemplateBackgroundImages obj in uploadedPdfRecords)
                            {
                                var bgImg = new TemplateBackgroundImages();
                                bgImg.Name = UploadPathForPDF + obj.Name;
                                bgImg.ImageName = UploadPathForPDF + obj.Name;
                                bgImg.ProductID = productid;

                                bgImg.ImageWidth = obj.ImageWidth;
                                bgImg.ImageHeight = obj.ImageHeight;

                                bgImg.ImageType = Convert.ToInt32(imageType);
                                bgImg.ImageTitle = fileName;
                                bgImg.UploadedFrom = Convert.ToInt32(uploadedFrom);
                                bgImg.ContactCompanyID = Convert.ToInt32(contactCompanyID);
                                bgImg.ContactID = Convert.ToInt32(contactID);
                                db.TemplateBackgroundImages.AddObject(bgImg);
                                // result = bgImg.ID.ToString();
                                result = "IsUploadedPDF";
                                // generate thumbnail 
                                string imgExt = Path.GetExtension(obj.Name);
                                Services.imageSvc objSvc = new Services.imageSvc();
                                string sourcePath = HttpContext.Current.Server.MapPath("Designer/Products/" + UploadPathForPDF + obj.Name);
                                //string ext = Path.GetExtension(uploadPath);
                                string[] results = sourcePath.Split(new string[] { imgExt }, StringSplitOptions.None);
                                string res = results[0];
                                string destPath = res + "_thumb" + imgExt;
                                objSvc.GenerateThumbNail(sourcePath, destPath, 98);

                            }
                            db.SaveChanges();
                        }
                        else
                        {
                            if (!Path.GetExtension(uploadPath).Contains("svg"))
                            {
                                using (objImage = System.Drawing.Image.FromFile(uploadPath))
                                {
                                    float res = objImage.HorizontalResolution;
                                    if (res < 96)
                                    {
                                        result = fileName;
                                    }
                                    ImageWidth = objImage.Width;
                                    ImageHeight = objImage.Height;
                                }
                            }
                            var bgImg = new TemplateBackgroundImages();
                            bgImg.Name = Imname;
                            bgImg.ImageName = Imname;
                            bgImg.ProductID = productid;

                            bgImg.ImageWidth = ImageWidth;
                            bgImg.ImageHeight = ImageHeight;

                            bgImg.ImageType = Convert.ToInt32(imageType);
                            bgImg.ImageTitle = fileName;
                            bgImg.UploadedFrom = Convert.ToInt32(uploadedFrom);
                            bgImg.ContactCompanyID = Convert.ToInt32(contactCompanyID);
                            bgImg.ContactID = Convert.ToInt32(contactID);


                            db.TemplateBackgroundImages.AddObject(bgImg);
                            db.SaveChanges();
                            result = bgImg.ID.ToString();

                            // generate thumbnail 
                            if (!ext.Contains("svg"))
                            {
                                Services.imageSvc objSvc = new Services.imageSvc();
                                string sourcePath = uploadPath;
                                //string ext = Path.GetExtension(uploadPath);
                                string[] results = sourcePath.Split(new string[] { ext }, StringSplitOptions.None);
                                string destPath = results[0] + "_thumb" + ext;
                                objSvc.GenerateThumbNail(sourcePath, destPath, 98);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //AppCommon.LogException(ex);
                throw ex;
            }
            finally
            {
                if (objImage != null)
                {
                    objImage.Dispose();
                }

            }
            return result;
        }

        private static void _InsertFontAttachment(string fontName, string fileName, string CustomerID)
        {

            try
            {
                int CustomerId = Convert.ToInt32(CustomerID);
                string path = "PrivateFonts/FontFace/";
                string CustomerType = "Loc";
                if (CustomerId < 0)
                {
                    try
                    {
                        string role = HttpContext.Current.Request.Cookies["role"].Value;
                        if (role != null)
                        {
                            if (role == "Designer")
                            {
                                CustomerId = -999;
                            }
                            else if (role == "Customer")
                            {
                                string Customer = HttpContext.Current.Request.Cookies["customerid"].Value;
                                CustomerType = "Glo";
                                CustomerId = Convert.ToInt32(Customer);
                            }
                        }
                    }
                    catch
                    {

                    }
                }

                if (CustomerId == -999 || CustomerId == null)
                {
                    // mpc designers
                    path = "PrivateFonts/FontFace/";
                }
                else
                {
                    if (CustomerType == "Glo")
                    {
                        path = "PrivateFonts/FontFace/Glo" + CustomerId.ToString() + "/";
                    }
                    else
                    {
                        path = "PrivateFonts/FontFace/" + CustomerId.ToString() + "/";
                    }
                }
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    string FontName = fileName;
                    var Font = db.TemplateFonts.Where(g => g.FontName == fontName && g.CustomerID == CustomerId).SingleOrDefault();

                    if (Font != null)
                    {

                    }
                    else
                    {
                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                        var FontObj = new TemplateFonts();
                        FontObj.FontName = fontName;
                        FontObj.FontFile = fileNameWithoutExt;
                        FontObj.FontDisplayName = fontName;
                        FontObj.CustomerID = CustomerId;
                        // can be changed
                        FontObj.IsPrivateFont = true;
                        FontObj.IsEnable = true;
                        FontObj.FontPath = path;
                        db.TemplateFonts.AddObject(FontObj);
                        db.SaveChanges();

                    }


                }
            }
            catch (Exception ex)
            {
                //AppCommon.LogException(ex);
                throw ex;
            }
        }
        #endregion
    }
}