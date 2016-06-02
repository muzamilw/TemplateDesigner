using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using TemplateDesignerModelTypesV2;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TemplateDesignerV2.Services.Utilities;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Drawing;


namespace TemplateDesignerV2.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class LayoutSvc
    {
        [OperationContract]
        [WebGet(UriTemplate = "{productCategoryID}")]
        public Stream GetProduct(string productCategoryID)
        {
            try
            {


                int CatID = int.Parse(productCategoryID);
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.ContextOptions.LazyLoadingEnabled = false;
                    db.ContextOptions.ProxyCreationEnabled = false;
                    var result = db.CategoryLayouts.Include("LayoutAttributes").Where(g => g.ProductCategoryID == CatID).OrderBy(g=>g.Orientation).ToList();
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));


                }
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(ex.ToString()));
            }
        }
        [OperationContract, WebInvoke(UriTemplate = "delete/{layoutID}", Method = "GET", BodyStyle = WebMessageBodyStyle.Bare)]
        public string DeleteLayout(string layoutID)
        {
            bool status = false;
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.ContextOptions.LazyLoadingEnabled = false;
                    db.ContextOptions.ProxyCreationEnabled = false;
                    int LID = Convert.ToInt32(layoutID);
                    var attr = db.LayoutAttributes.Where(g => g.LayoutID == LID).ToList();
                    foreach (var obj in attr)
                    {
                        db.LayoutAttributes.DeleteObject(obj);
                    }
                    var objLayout = db.CategoryLayouts.Where(g => g.LayoutID == LID).SingleOrDefault();
                    if (objLayout != null)
                    {
                        db.CategoryLayouts.DeleteObject(objLayout);
                    }
                    db.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return status.ToString() ;

        }

        [OperationContract, WebInvoke(UriTemplate = "update/", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]
        public int update(Stream data)
        {

            return Save(data, 1);
        }
        [OperationContract, WebInvoke(UriTemplate = "addNew/", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]
        public int AddNew(Stream data)
        {

            return Save(data, 2);
        }
        public int Save(Stream data,int mode)
        {
            int id = 0;
            try 
            {
               StreamReader reader = new StreamReader(data);
               string res = reader.ReadToEnd();
               reader.Close();
               reader.Dispose();
               objLayouts objSettings = JsonConvert.DeserializeObject<objLayouts>(res);
                if (objSettings != null)
                {
                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        if (mode == 1)
                        {
                            if (objSettings.obj != null && objSettings.objsAttr != null)
                            {
                                int objID = objSettings.obj.LayoutID;
                                var attr = db.LayoutAttributes.Where(g => g.LayoutID == objID).ToList();
                                foreach (var obj in attr)
                                {
                                    db.LayoutAttributes.DeleteObject(obj);
                                }
                                var objLayout = db.CategoryLayouts.Where(g => g.LayoutID == objID).SingleOrDefault();
                                if (objLayout != null)
                                {
                                    objLayout.ImageLogoType = objSettings.obj.ImageLogoType;
                                    objLayout.Orientation = objSettings.obj.Orientation;
                                    objLayout.ProductCategoryID = objSettings.obj.ProductCategoryID;
                                    objLayout.Title = objSettings.obj.Title;
                                }
                                id = objID;
                            }
                        }
                        else
                        {
                            CategoryLayouts objLayout = new CategoryLayouts();
                            objLayout.ImageLogoType = objSettings.obj.ImageLogoType;
                            objLayout.Orientation = objSettings.obj.Orientation;
                            objLayout.ProductCategoryID = objSettings.obj.ProductCategoryID;
                            objLayout.Title = objSettings.obj.Title;
                            db.CategoryLayouts.AddObject(objLayout);
                            id = objLayout.LayoutID;
                        }
                        foreach (var item in objSettings.objsAttr)
                        {
                            LayoutAttributes objAttr = new LayoutAttributes();
                            objAttr.FeildName = item.FeildName;
                            objAttr.fontSize = item.fontSize;
                            objAttr.fontWeight = item.fontWeight;
                            objAttr.LeftPos = item.LeftPos;
                            objAttr.maxHeight = item.maxHeight;
                            objAttr.maxWidth = item.maxWidth;
                            objAttr.textAlign = item.textAlign;
                            objAttr.topPos = item.topPos;
                            objAttr.LayoutID = id;
                            db.LayoutAttributes.AddObject(objAttr);
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           return id;
        }
    }

    public class objLayouts
    {
        public CategoryLayouts obj = null;
        public List<LayoutAttributes> objsAttr = null; 
    }

}