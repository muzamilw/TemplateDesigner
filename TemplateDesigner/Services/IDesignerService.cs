using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDesignerService" in both code and config file together.
    [ServiceContract]
    public interface IDesignerService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<vw_ProductCategoriesLeafNodes> GetCategories();
    }
}
