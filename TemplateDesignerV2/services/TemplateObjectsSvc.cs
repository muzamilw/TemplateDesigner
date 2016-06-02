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
using TemplateDesignerV2.Services.Utilities;

namespace TemplateDesignerV2.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TemplateObjectsSvc
    {
        [WebGet(UriTemplate = "{TemplateID}")]
        public Stream GetProductObjects(string TemplateID)
        {
            // values used to change the display order
            int productPageId = -1;
            int DisplayOrderCounter = 0;


            CMYKtoRGBConverter oColorConv = new CMYKtoRGBConverter();
            int productid = int.Parse(TemplateID);
            // TODO: Replace the current implementation to return a collection of SampleItem instances
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.ContextOptions.LazyLoadingEnabled = false;
                db.ContextOptions.ProxyCreationEnabled = false;
                List<TemplateObjects> result = db.TemplateObjects.Where( g=> g.ProductID == productid).ToList();
               
                result = result.OrderBy(g => g.DisplayOrderPdf).ToList();
                result = result.OrderBy(g => g.ProductPageId).ToList();
                foreach (var item in result)
                {
                    item.PositionX = Util.PointToPixel(item.PositionX.Value);
                    item.PositionY = Util.PointToPixel(item.PositionY.Value);
                    item.FontSize = Util.PointToPixel(item.FontSize.Value);
                    item.MaxWidth = Util.PointToPixel(item.MaxWidth.Value);
                    item.MaxHeight = Util.PointToPixel(item.MaxHeight.Value);
                    if (item.CharSpacing != null)
                    {
                        item.CharSpacing = Convert.ToDouble(Util.PointToPixel(Convert.ToDouble(item.CharSpacing.Value)));
                    }
                    item.ColorHex = oColorConv.getColorHex(item.ColorC.Value, item.ColorM.Value, item.ColorY.Value, item.ColorK.Value);
                    
                    // used to create page objects display order so that it can be used in objects display order
                    if (productPageId == -1)
                    {
                        productPageId = Convert.ToInt32( item.ProductPageId);
                        item.DisplayOrderPdf = DisplayOrderCounter;
                        DisplayOrderCounter++;

                    }
                    else if (productPageId == Convert.ToInt32(item.ProductPageId))
                    {
                        item.DisplayOrderPdf = DisplayOrderCounter;
                        DisplayOrderCounter++;
                    }
                    else
                    {
                        productPageId = Convert.ToInt32(item.ProductPageId);
                        item.DisplayOrderPdf = 0;
                        DisplayOrderCounter = 1;
                    }


                    if (item.IsQuickText == null)
                    {
                        item.IsQuickText = false;
                    }
                }

                

                TemplateObjects oTempItem = new TemplateObjects();
                oTempItem.ProductID = productid;
                oTempItem.ObjectID = -999;
                oTempItem.ObjectType = 2;
                oTempItem.Name = "Dummy Object";
                oTempItem.ContentString = "Dummy Object";
                oTempItem.MaxWidth = 50;
                oTempItem.MaxHeight = 100;
                oTempItem.PositionX = 10;
                oTempItem.PositionY = 10;
                oTempItem.FontName = "Arial";
                oTempItem.FontSize = 10;
                oTempItem.DisplayOrderPdf = 100;
                oTempItem.ColorC = 0;
                oTempItem.ColorM = 100;
                oTempItem.ColorY = 100;
                oTempItem.ColorK = 20;
                oTempItem.IsEditable = true;
                oTempItem.IsHidden = false;
                oTempItem.IsMandatory = false;
                oTempItem.IsPositionLocked = false;
                oTempItem.AutoShrinkText = false;
                oTempItem.RotationAngle = 0;
                oTempItem.ColorType = 3;
                oTempItem.IsSpotColor = false;
                oTempItem.VAllignment = 1;
                oTempItem.Allignment = 1;
                oTempItem.ColorName = "";
                oTempItem.SpotColorName = "";
                oTempItem.TCtlName = "";
                oTempItem. ExField1 ="";
                oTempItem.ExField2 = "";
                oTempItem.Opacity = 1;
                oTempItem.IsQuickText = false;
                oTempItem.QuickTextOrder = 0;
                oTempItem.CharSpacing = 0;
                oTempItem.ColorHex = oColorConv.getColorHex(0, 100, 100, 20);
                oTempItem.textCase = 0;
                result.Insert(0,oTempItem);


                JsonSerializerSettings oset = new JsonSerializerSettings();
                

                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Formatting.Indented)));


            }
        }

    }
}