using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using TemplateDesignerModelTypesV2;
using LinqKit;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DesignerService
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file


    public class imageSvc
    {

        [WebGet(UriTemplate = "{productID}")]

        public Stream GetProductBackgroundImages(string productID)
        {

            int productId = Convert.ToInt32(productID);
            //return null;

            try
            {
                if (productId != 0)
                {

                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {

                        db.ContextOptions.LazyLoadingEnabled = false;
                        //printdesignBLL.Products.ProductBackgroundImages objBackground = new printdesignBLL.Products.ProductBackgroundImages();
                        //objBackground.LoadByProductId(ProductId);

                        var backgrounds = db.TemplateBackgroundImages.Where(g => g.ProductID == productId).ToList();

                      
                        string imgPath = System.AppDomain.CurrentDomain.BaseDirectory + ("Designer\\Products\\");
                        string imgUrl = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                        
                        Uri objUri = new Uri(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"));

                        foreach (var objBackground in backgrounds)
                        {
                            string url1 = Path.Combine(imgUrl,objBackground.ImageName);
                            if (objBackground.ImageName != null && objBackground.ImageName != "")
                            {
                                //if (System.IO.File.Exists(url1))
                                //{
                                    //string ImgExt = System.IO.Path.GetExtension(objBackground.ImageName).ToLower();
                                    //if (ImgExt == ".jpg" || ImgExt == ".png")
                                    //{

                                    //lstProductBackground.Add(new PrintFlow.Model.Products.ProductBackgroundImages { BackgroundImageName = ((!objBackground.IsColumnNull("Name")) ? objBackground.Name : ""), 

                                    //objBackground.BackgroundImageAbsolutePath = objUri.OriginalString + objBackground.ImageName, 
                                    //ImgRelativePath = new Uri(@"Designer/Products/" + objBackground.ImageName, UriKind.Relative), 
                                    //BackgroundImageRelativePath = "Designer/Products/" + objBackground.ImageName });

                                    //objBackground.BackgroundImageAbsolutePath = objUri.OriginalString + objBackground.ImageName;
                                    //}
                                    objBackground.BackgroundImageRelativePath = "Designer/Products/" + objBackground.ImageName;
                               // }
                            }

                        }
                        JsonSerializerSettings oset = new JsonSerializerSettings();


                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                        return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(backgrounds.ToList(), Formatting.Indented)));
                      
                    }
                }
            }
            catch (Exception ex)
            {
              //  AppCommon.LogException(ex);
                throw ex;
                //throw new Exception(ex.ToString());
            }

            return null;
        }

        [WebGet(UriTemplate = "{productID},{ImageID}")]


        //function to delete background image from Database and from physical location 

        public string DeleteProductBackgroundImage(string productID, string ImageID)
		{
			try
			{
                int BackgroundImageID = Convert.ToInt32(ImageID);
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    
                    var obj = db.TemplateBackgroundImages.Where(g => g.ID == BackgroundImageID).Single();
                    if (obj != null)
                    {
                        string sfilePath = System.Web.Hosting.HostingEnvironment.MapPath("/templatedesignerv2/designer/products/" + obj.ImageName);
                        db.DeleteObject(obj);

                        //delete the actual image as well
                        if (System.IO.File.Exists(sfilePath))
                            System.IO.File.Delete(sfilePath);

                        db.SaveChanges();


                        return true.ToString();
                    }
                }
                return false.ToString();
				
			}
			catch (Exception ex)
			{
			//	AppCommon.LogException(ex);
				throw ex;
			}
		}


    }
}