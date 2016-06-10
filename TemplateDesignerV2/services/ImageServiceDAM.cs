using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.IO;
using TemplateDesignerModelV2;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;


namespace TemplateDesignerV2.Services
{
    /////////////////////////////////////////////////////////////
    // if 1 then caller is global Template Designer  Called From
    // if 2 then caller is coorporate backend (MIS)
    // if 3 then caller is retail end user 
    // if 4 then caller is coorporate end user
    ////////////////////////////////////////////////////////////
    // if image type 1 == new template images 
    // if image type 2 == old template image 
    // if image type 4 == old template background image
    // if image type 3 == background image 
    ////////////////////////////////////////////////////////////
    // image set type 1 = Template Images 
    // image set type 12 = Template Background Images 
    // image set type 2 = Coorporate Images
    // image set type 3 = Coorporate Background Images
    // image set type 4 = Coorporate Personal Images End User Mode 
    // image set type 5 = Coorporate Personal Images End User Mode Backgrounds 
    // image set type 6 = Global Images 
    // image set type 7 = Global Images Backgrounds
    // image set type 8 = Personal Images Retail End User 
    // image set type 9 = Personal Images Retail End User Backgrounds
    // image set type 10 = Personal Images designer v2 customer End User 
    // image set type 11 = Personal Images designer v2 customer Backgrounds
    // image set type 14 = global logos
    // image set type 13 = global shapes/icons
    // image set type 15 = personal logos
    // image set type 16 = coorporate Shapes
    // image Set type 17 coorporate logos
    // image set type 18 = global Illustrations
    // image set type 19 = global Frames
    // image set type 20 = global Banners
    //////////////////////////////////////////////////////////
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ImageServiceDAM
    {


        [WebGet(UriTemplate = "{isCalledFromSt},{imageSetTypeSt},{templateIDSt},{contactCompanyIDSt},{contactIDSt},{territorySt},{pageNumnerSt},{SearchKeyword}")]
        private Stream getImages(string isCalledFromSt, string imageSetTypeSt, string templateIDSt, string contactCompanyIDSt, string contactIDSt, string territorySt, string pageNumnerSt,string SearchKeyword)
        {
            try
            {

                int isCalledFrom = Convert.ToInt32(isCalledFromSt);
                int imageSetType = Convert.ToInt32(imageSetTypeSt);
                int templateID = Convert.ToInt32(templateIDSt);
                int contactCompanyID = Convert.ToInt32(contactCompanyIDSt);
                int contactID = Convert.ToInt32(contactIDSt);
                int territory = Convert.ToInt32(territorySt);
                int pageNumner = Convert.ToInt32(pageNumnerSt);
                if (SearchKeyword == "___notFound")
                {
                    SearchKeyword = "";
                }
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    //listImages = db.sp_TemplateImages(isCalledFrom, imageSetType, templateID, contactCompanyID, contactID, territory, pageNumner, 10, "").ToList(); 
                    var imgCount = new ObjectParameter("imageCount", typeof(int));
                    imgCount.Value = 0;
                    var result = db.sp_GetTemplateImages(isCalledFrom, imageSetType, templateID, contactCompanyID, contactID, territory, pageNumner, 20, "", SearchKeyword, imgCount).ToList();

                    string imgPath = System.AppDomain.CurrentDomain.BaseDirectory + ("Designer\\Products\\");
                    string imgUrl = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");

                    Uri objUri = new Uri(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"));

                    foreach (var objBackground in result)
                    {
                        string url1 = Path.Combine(imgUrl, objBackground.ImageName);
                        if (objBackground.ImageName != null && objBackground.ImageName != "")
                        {
                            objBackground.BackgroundImageRelativePath = "Designer/Products/" + objBackground.ImageName;
                        }

                    }
                    ImageWrapper objWrapper = new ImageWrapper();
                    objWrapper.ImageCount = Convert.ToInt32(imgCount.Value);
                    objWrapper.objsBackground = result;
                    JsonSerializerSettings oset = new JsonSerializerSettings();
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(objWrapper, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            //if (isCalledFrom == 1) // global template Designer
            //{
            //    if (imageSetType == 1)  // old template images
            //    {

            //    }
            //    else
            //    {
            //        if (contactCompanyID != -999)
            //        {  // logged in from MIS

            //        }
            //        else
            //        {
            //            if (imageSetType == 6)
            //            {
            //                // gloabl images
            //            }

            //        }
            //    }
            //}
            //else if(isCalledFrom == 2)  // MIS 
            //{
            //    if (imageSetType == 1)  // old template images
            //    {

            //    }
            //    else if (imageSetType == 2 || imageSetType == 3)
            //    {
            //        // show all images against contact Company ID 
            //    }
            //}
            //else if (isCalledFrom == 3)  // retail end user
            //{
            //    if (imageSetType == 1)  // old template images
            //    {

            //    }
            //    else if (imageSetType == 6 || imageSetType == 7)
            //    {  // show global free images

            //    }
            //    else if (imageSetType == 8 || imageSetType == 9)
            //    {  // show Retail user images

            //    }
            //}
            //else if (isCalledFrom == 4)  // Corp end user
            //{
            //    if (imageSetType == 1)  // old template images
            //    {

            //    }
            //    else if (imageSetType == 2 || imageSetType == 3)
            //    {  // show Corp images of that territory

            //    }
            //    else if (imageSetType == 4 || imageSetType == 5)
            //    {  // show  user images

            //    }
            //}
         
        }


        [WebGet(UriTemplate = "{imgID}")]
        private Stream getImage(string imgID)
        {
            int imageID = Convert.ToInt32(imgID);
         
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var img = db.TemplateBackgroundImages.Where(g => g.ID == imageID).SingleOrDefault();
                if (img != null)
                {
                    if (img.ImageName != null && img.ImageName != "")
                    {
                        img.BackgroundImageRelativePath = "Designer/Products/" + img.ImageName;
                    }
                    if (img.ImageTitle == null)
                    {
                        string[] imgs = img.ImageName.Split('/');
                        img.ImageTitle = imgs[imgs.Length-1];
                        
                    }
                }
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(img, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
            }


        }

        [WebGet(UriTemplate = "{imgID},{imgType},{imgTitle},{imgDescription},{imgKeywords}")]
        private Stream UpdateImg(string imgID,string imgType, string imgTitle, string imgDescription, string imgKeywords)
        {
            int imageID = Convert.ToInt32(imgID);
            int imType = Convert.ToInt32(imgType);
            imgTitle = imgTitle.Replace("___", "/");
            imgDescription = imgDescription.Replace("___", "/");
            imgKeywords = imgKeywords.Replace("___", "/");
            imgKeywords = imgKeywords.Replace("__", ",");
            imgTitle = imgTitle.Replace("__", ",");
            imgDescription = imgDescription.Replace("__", ",");
            
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var img = db.TemplateBackgroundImages.Where(g => g.ID == imageID).SingleOrDefault();
                if (img != null)
                {
                    img.ImageTitle = imgTitle;
                    img.ImageDescription = imgDescription;
                    img.ImageKeywords = imgKeywords;
                    if (imType != 0)
                    {
                        img.ImageType = imType;
                    }
                    db.SaveChanges();
                }
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(img, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
            }


        }


        [WebGet(UriTemplate = "{imgID},{territory}")]
        private Stream UpdateImgTerritories(string imgID, string territory)
        {
            string result = "true";
            int imageID = Convert.ToInt32(imgID);
            string[] territories = territory.Split('_'); 

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                int ImageID = Convert.ToInt32(imgID);
                List<ImagePermissions> oldPermissions = db.ImagePermissions.Where(g => g.ImageID == ImageID).ToList();
                foreach (var obj in oldPermissions)
                {
                    db.ImagePermissions.Remove(obj);
                }
                foreach (string obj in territories)
                {
                    if (obj != "")
                    {
                        ImagePermissions objPermission = new ImagePermissions();
                        objPermission.ImageID = Convert.ToInt32(imgID);
                        objPermission.TerritoryID = Convert.ToInt32(obj);
                        db.ImagePermissions.Add(objPermission);
                    }
                }
                db.SaveChanges();
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
            }


        }

        [WebGet(UriTemplate = "getTerritories/{imgID}")]
        private Stream getTerritories(string imgID)
        {
            int imageID = Convert.ToInt32(imgID);

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                List<ImagePermissions> listObj = db.ImagePermissions.Where(g => g.ImageID == imageID).ToList();
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(listObj, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
            }


        }


    }
    public class ImageWrapper
    {
        public int ImageCount = 0;
        public List<sp_GetTemplateImages_Result> objsBackground = null;
    }
}