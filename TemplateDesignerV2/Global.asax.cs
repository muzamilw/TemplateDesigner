using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace TemplateDesignerV2
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("services/TemplateSvc", new WebServiceHostFactory(), typeof(Services.TemplateSvc)));
            RouteTable.Routes.Add(new ServiceRoute("services/imageSvc", new WebServiceHostFactory(), typeof(Services.imageSvc)));
            RouteTable.Routes.Add(new ServiceRoute("services/fontSvc", new WebServiceHostFactory(), typeof(Services.fontSvc)));
            RouteTable.Routes.Add(new ServiceRoute("services/TemplateObjectsSvc", new WebServiceHostFactory(), typeof(Services.TemplateObjectsSvc)));
            RouteTable.Routes.Add(new ServiceRoute("services/TemplatePagesSvc", new WebServiceHostFactory(), typeof(Services.TemplatePagesSvc)));
            RouteTable.Routes.Add(new ServiceRoute("services/PdfExtractor", new WebServiceHostFactory(), typeof(Services.PdfExtractor)));
            RouteTable.Routes.Add(new ServiceRoute("services/imageSvcDam", new WebServiceHostFactory(), typeof(Services.ImageServiceDAM)));
            RouteTable.Routes.Add(new ServiceRoute("services/layoutSvc", new WebServiceHostFactory(), typeof(Services.LayoutSvc)));
        }

       
    }
}
