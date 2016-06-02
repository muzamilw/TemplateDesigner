using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace DesignerService
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            // Edit the base address of Service1 by replacing the "Service1" string below
            RouteTable.Routes.Add(new ServiceRoute("TemplateSvc", new WebServiceHostFactory(), typeof(TemplateSvc)));
            RouteTable.Routes.Add(new ServiceRoute("imageSvc", new WebServiceHostFactory(), typeof(imageSvc)));
            RouteTable.Routes.Add(new ServiceRoute("fontSvc", new WebServiceHostFactory(), typeof(fontSvc)));
            RouteTable.Routes.Add(new ServiceRoute("TemplateObjectsSvc", new WebServiceHostFactory(), typeof(TemplateObjectsSvc)));
            RouteTable.Routes.Add(new ServiceRoute("TemplatePagesSvc", new WebServiceHostFactory(), typeof(TemplatePagesSvc)));

            
        }

       
    }
}
