using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using TemplateDesignerModelV2;
using LinqKit;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TemplateDesignerV2.Services
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file


    public class fontSvc
    {
        //[WebGet(UriTemplate = "{TempParameter}")]

        //public Stream GetProductBackgroundImages(string TempParameter)
        //{

        //    int temp = Convert.ToInt32(TempParameter);
        //    //return null;

        //    try
        //    {
        //        if (temp != 0)
        //        {

        //            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //            {

        //                db.Configuration.LazyLoadingEnabled = false;
        //                //printdesignBLL.Products.ProductBackgroundImages objBackground = new printdesignBLL.Products.ProductBackgroundImages();
        //                //objBackground.LoadByProductId(ProductId);

        //                var fonts = db.TemplateFonts.ToList();

        //                JsonSerializerSettings oset = new JsonSerializerSettings();


        //                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
        //                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(fonts.ToList(), Formatting.Indented)));



        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        //  AppCommon.LogException(ex);
        //        throw ex;
        //        //throw new Exception(ex.ToString());
        //    }

        //    return null;
        //}

        [WebGet(UriTemplate = "{ProductId1},{CustomerId1}")]


        public Stream GetFontList(string ProductId1, string CustomerId1)
        {

            List<TemplateFonts> lstFont = new List<TemplateFonts>();
            int ProductId = Convert.ToInt32(ProductId1);
            int CustomerId = Convert.ToInt32(CustomerId1);
            if (CustomerId < 0)
            {
                string role = HttpContext.Current.Request.Cookies["role"].Value;
                if (role != null)
                {
                    if (role == "Designer" || role =="Admin")
                    {
                        CustomerId = -999;
                    }
                    else if (role == "Customer")
                    {
                        string Customer = HttpContext.Current.Request.Cookies["customerid"].Value;
                        CustomerId = Convert.ToInt32(Customer);
                    }
                }
            }
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.Configuration.LazyLoadingEnabled = false;

                    List<TemplateFonts> lFont = new List<TemplateFonts>();
                    lFont = db.sp_GetUsedFontsUpdated(ProductId, CustomerId).ToList();
                    foreach (var objFonts in lFont)
                    {
                        string path = "";
                        if (objFonts.FontPath != null)
                        {
                            path = objFonts.FontPath;
                        }
                        else
                        {
                            path = "PrivateFonts/FontFace/";
                        }

                        objFonts.FontFile ="Designer/" +path  + objFonts.FontFile;
                        lstFont.Add(objFonts);
                    }
                }
            }
            catch (Exception ex)
            {
                //pCommon.LogException(ex);
                throw ex;
            }
           
            JsonSerializerSettings oset = new JsonSerializerSettings();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(lstFont, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));

        }


        //[WebGet(UriTemplate = "{ProductId1},{mode1},")] // old logic fonts

        //public Stream GetFontList(string ProductId1, string mode1)
        //{
        //    List<TemplateFonts> lstFont = new List<TemplateFonts>();
        //    //ProductFonts objFonts = new ProductFonts();

        //    int ProductId = Convert.ToInt32(ProductId1);
        //    bool ReturnFontFiles = false;
        //    int mode = Convert.ToInt32(mode1);
        //    try
        //    {
        //        using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //        {

        //            List<TemplateFonts> lFonts = null;

        //            switch (mode1)
        //            {
        //                case "1":
        //                    lFonts = db.TemplateFonts.Where(g => g.IsEnable == true).OrderBy(g => g.FontDisplayName).ToList();//.Where(g => g.ProductId == ProductId || g.ProductId == null);
        //                    break;
        //                case "2":
        //                    lFonts = db.TemplateFonts.Where(g => g.IsPrivateFont == false && g.IsEnable == true).OrderBy(g => g.FontDisplayName).ToList();
        //                    break;
        //                case "3":
        //                    lFonts = db.TemplateFonts.Where(g => g.IsPrivateFont == true && g.IsEnable == true).OrderBy(g => g.FontDisplayName).ToList();
        //                    break;
        //                case "4":

        //                    lFonts = db.sp_GetUsedFonts(ProductId).ToList();
        //                    break;
        //                default:
        //                    break;
        //            }


        //            foreach (var objFonts in lFonts)
        //            {
        //                //if (!objFonts.IsColumnNull("FontName") && !objFonts.IsColumnNull("FontDisplayName") && !objFonts.IsColumnNull("IsPrivateFont") && objFonts.FontName != "" && objFonts.FontDisplayName != "")
        //                //{

        //                if (ReturnFontFiles)
        //                {
        //                    if (objFonts.IsPrivateFont == false)
        //                    {
        //                        //lstFont.Add(new ProductFonts { ProductFontId = objFonts.ProductFontId, ProductId = objFonts.ProductId, FontName = objFonts.FontName, FontDisplayName = objFonts.FontDisplayName, FontFile = "", IsPrivateFont = false });
        //                        lstFont.Add(objFonts);
        //                    }
        //                    else if (objFonts.FontFile != null && objFonts.FontFile != "")
        //                    {
        //                        string FontFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "Designer\\PrivateFonts\\" + objFonts.FontFile;
        //                        if (System.IO.File.Exists(FontFilePath))
        //                        {
        //                            System.IO.FileStream strmFont = new System.IO.FileStream(FontFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        //                            System.IO.BinaryReader brFont = new System.IO.BinaryReader(strmFont);
        //                            long numBytes = new System.IO.FileInfo(FontFilePath).Length;
        //                            byte[] buffFont = brFont.ReadBytes((int)numBytes);
        //                            objFonts.FontBytes = buffFont;
        //                            lstFont.Add(objFonts);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    lstFont.Add(objFonts);
        //                }
        //            }
        //        }
        //    }


        //    catch (Exception ex)
        //    {
        //        //pCommon.LogException(ex);
        //        throw ex;
        //    }

        //    JsonSerializerSettings oset = new JsonSerializerSettings();


        //    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
        //    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(lstFont, Formatting.Indented)));

        //}

    }
}