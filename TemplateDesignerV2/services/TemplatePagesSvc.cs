using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TemplateDesignerV2.Services
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default,0 a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class TemplatePagesSvc
    {
        [WebGet(UriTemplate = "{TemplateID}")]
        public Stream GetTemplatePages(string TemplateID)
        {
            int ProductId = int.Parse(TemplateID);
            // TODO: Replace the current implementation to return a collection of SampleItem instances
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.ContextOptions.LazyLoadingEnabled = false;
                db.ContextOptions.ProxyCreationEnabled = false;
                var result = db.TemplatePages.Where(g => g.ProductID == ProductId).OrderBy( g => g.PageNo).ToList();

                foreach (var objPage in result)
                {
                    string targetFolder = "";
                    targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                    if (objPage.BackGroundType != 3)
                    {
                        if (File.Exists(targetFolder + TemplateID + "/templatImgBk" + objPage.PageNo.ToString() + ".jpg"))
                        {
                            objPage.BackgroundFileName = "Designer/Products/" + TemplateID + "/templatImgBk" + objPage.PageNo.ToString() + ".jpg";
                        }
                        else
                        {
                            objPage.BackgroundFileName = "";
                        }
                    }
                }

                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));


            }

        }
    }
}