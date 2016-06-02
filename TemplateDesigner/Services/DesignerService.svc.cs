using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;


using WebSupergoo.ABCpdf7;
using System.Linq.Expressions;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using System.Text.RegularExpressions;
using LinqKit;
using TemplateDesignerModelTypes;
using System.Runtime.InteropServices;
using System.ServiceModel.Web;

namespace TemplateDesigner.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DesignerService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class DesignerService : IDesignerService
    {
        public void DoWork()
        {
        }

        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "Categories")]
        public List<vw_ProductCategoriesLeafNodes> GetCategories()
        {
            using (TemplateDesignerEntities db = new TemplateDesignerEntities())
            {

                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;

                    var objectsList = db.vw_ProductCategoriesLeafNodes.OrderBy(g => g.CategoryName).ToList();
                    return objectsList;
                }
                catch (Exception ex)
                {
                    throw ex;
                    // throw new Exception(ex.ToString());
                }

            }
        }
    }
}
