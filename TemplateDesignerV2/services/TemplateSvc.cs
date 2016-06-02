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
using WebSupergoo.ABCpdf8;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Drawing;
using Svg;
using WebSupergoo.ABCpdf8.Objects;
using System.Xml;
using System.Data.Objects;
using TemplateDesignerV2.services;

namespace TemplateDesignerV2.Services
{

    public class Settings
    {
        public bool printCropMarks = false;
        public bool printWaterMarks = false;
        public List<TemplateObjects> objects = null;
        public string orderCode = null;
        public string CustomerName = null;
        public List<TemplatePages> objPages = null;
        public bool isRoundCornerrs = false;
        public bool isMultiPageProduct = false;
    }
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default,0 a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class TemplateSvc
    {
        // TODO: Implement the collection resource that will contain the SampleItem instances



        [OperationContract]
        [WebGet(UriTemplate = "{TemplateID}")]
        public Stream GetProduct(string TemplateID)
        {

            try
            {


                int ProductId = int.Parse(TemplateID);
                // TODO: Replace the current implementation to return a collection of SampleItem instances
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.ContextOptions.LazyLoadingEnabled = false;
                    db.ContextOptions.ProxyCreationEnabled = false;
                    var result = db.Templates.Where(g => g.ProductID == ProductId).Single();

                    result.PDFTemplateHeight = Utilities.Util.PointToPixel(result.PDFTemplateHeight.Value);
                    result.PDFTemplateWidth = Utilities.Util.PointToPixel(result.PDFTemplateWidth.Value);

                    result.CuttingMargin = Utilities.Util.PointToPixel(result.CuttingMargin.Value);


                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
                    //return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result)));


                }
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(ex.ToString()));
            }
        }


        [OperationContract, WebInvoke(UriTemplate = "update/", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]
        public string update(Stream data)
        {

            return Save(data, 1);
        }

        [OperationContract, WebInvoke(UriTemplate = "preview/", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]
        public string preview(Stream data)
        {
            return Save(data, 2);

        }

        [OperationContract, WebInvoke(UriTemplate = "savecontinue/", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]
        public string savecontine(Stream data)
        {

            return Save(data, 3);
        }

        [OperationContract]
        [WebGet(UriTemplate = "GetFolds/{TemplateID}")]
        public Stream GetFoldLine(string TemplateID)
        {
            int Templateid = Convert.ToInt32(TemplateID);

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                db.ContextOptions.LazyLoadingEnabled = false;
                db.ContextOptions.ProxyCreationEnabled = false;

                List<tbl_ProductCategoryFoldLines> foldLines = null;

                var template = db.Templates.Where(g => g.ProductID == Templateid).SingleOrDefault();
                foldLines = (from c in db.tbl_ProductCategoryFoldLines
                             where c.ProductCategoryID == template.ProductCategoryID
                             select c).ToList();

                if (foldLines != null)
                {
                    foreach (tbl_ProductCategoryFoldLines obj in foldLines)
                    {
                        double marginInMM = Convert.ToDouble(obj.FoldLineOffsetFromOrigin);
                        marginInMM = Utilities.Util.MMToPoint(marginInMM);
                        marginInMM = Utilities.Util.PointToPixel(marginInMM);
                        obj.FoldLineOffsetFromOrigin = marginInMM;
                    }
                    JsonSerializerSettings oset = new JsonSerializerSettings();


                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(foldLines, Newtonsoft.Json.Formatting.Indented)));
                }
                else
                {
                    return null;
                }
                //FoldLineOrientation == true for verticle lines and false for horizontal lines




            }


        }


        public string Save(Stream data, int Mode)
        {

            //return "true";
            try
            {
                //modes  1, save only, 2 with preview, 3 save and continue, 4 save with thumbnail

                int TemplateID = 0;

                StreamReader reader = new StreamReader(data);
                string res = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                Settings objSettings = JsonConvert.DeserializeObject<Settings>(res);
                //List<TemplateObjects> lstTemplatesObjects = JsonConvert.DeserializeObject<List<TemplateObjects>>(res);
                List<TemplateObjects> lstTemplatesObjects = objSettings.objects;
                if (lstTemplatesObjects.Count > 0)
                {

                    TemplateID = lstTemplatesObjects[0].ProductID.Value;
                    var objProduct = new Templates();
                    List<TemplatePages> oTemplatePages = null;
                    using (TransactionScope scope = new TransactionScope())
                    {


                        using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                        {
                            objProduct = db.Templates.Where(g => g.ProductID == TemplateID).Single();

                            foreach (TemplateObjects c in db.TemplateObjects.Where(g => g.ProductID == TemplateID))
                            {
                                db.DeleteObject(c);
                            }
                            db.SaveChanges(false);
                            db.AcceptAllChanges();

                            //dbObjects = new printdesignBLL.Products.Objects();
                            //printdesignBLL.Products.Objects.SaveObjects(dbProduct.ProductID, objObjectsList);

                            foreach (var oObject in lstTemplatesObjects)
                            {
                                if (oObject.ObjectID != -999)
                                {
                                    oObject.PositionX =Math.Round( Util.PixelToPoint(oObject.PositionX.Value),6);
                                    oObject.PositionY =Math.Round(  Util.PixelToPoint(oObject.PositionY.Value),6);
                                    oObject.FontSize =Math.Round(  Util.PixelToPoint(oObject.FontSize.Value),6);
                                    oObject.MaxWidth = Math.Round( Util.PixelToPoint(oObject.MaxWidth.Value),6);
                                    oObject.MaxHeight = Math.Round(Util.PixelToPoint(oObject.MaxHeight.Value), 6);
                                    if (oObject.CharSpacing != null)
                                    {
                                        oObject.CharSpacing = Convert.ToDouble(Util.PixelToPoint(Convert.ToDouble(oObject.CharSpacing.Value)));
                                    }
                                    //                                    oObject.CharSpacing = Util.PixelToPoint(conve);
                                    if (oObject.ObjectType == 3)
                                    {
                                        if (oObject.ContentString.Contains("__clip_mpc"))
                                        {
                                             oObject.hasClippingPath = true;
                                        }
                                        else
                                        {
                                            oObject.hasClippingPath = false;
                                        }
                                    }
                                    oObject.ProductID = TemplateID;
                                    db.TemplateObjects.AddObject(oObject);
                                }
                            }
                            db.SaveChanges(false);
                            db.AcceptAllChanges();
                        }
                        scope.Complete();
                    }



                    if (Mode == 2 || Mode == 3)   //generating preview
                    {
                       
                        using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                        {
                            db.ContextOptions.LazyLoadingEnabled = false;
                            string targetFolder = "";
                            //if ( Mode == 3)
                            targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                            //else
                            //    targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                            oTemplatePages = db.TemplatePages.Where(g => g.ProductID == TemplateID).ToList();
                            //updating color for template pages // mod 
                            if (objSettings.objPages != null)
                            {
                                foreach (TemplatePages obj in objSettings.objPages)
                                {
                                    for (int i = 0; i < oTemplatePages.Count; i++)
                                    {
                                        if (oTemplatePages[i].ProductPageID == obj.ProductPageID)
                                        {
                                            oTemplatePages[i].ColorC = obj.ColorC;
                                            oTemplatePages[i].ColorM = obj.ColorM;
                                            oTemplatePages[i].ColorY = obj.ColorY;
                                            oTemplatePages[i].ColorK = obj.ColorK;
                                            oTemplatePages[i].BackGroundType = obj.BackGroundType;
                                            oTemplatePages[i].IsPrintable = obj.IsPrintable;
                                            if (obj.BackgroundFileName != "")
                                            {
                                                string ext = System.IO.Path.GetExtension(obj.BackgroundFileName);
                                                if (obj.BackGroundType == 1 && ext.Contains("jpg"))
                                                {
                                                    oTemplatePages[i].BackgroundFileName = TemplateID + "/" + "Side" + oTemplatePages[i].PageNo + ".pdf"; 
                                                }
                                                else
                                                {
                                                    oTemplatePages[i].BackgroundFileName = obj.BackgroundFileName;
                                                }

                                            }
                                            break;
                                        }
                                    }
                                }
                                db.SaveChanges();
                            }
                            if (objSettings.isMultiPageProduct)
                            {
                                bool hasOverlayObject = false;
                                byte[] PDFFile = generatePDF(objProduct, oTemplatePages, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, false, objSettings.printCropMarks,objSettings.printWaterMarks, out hasOverlayObject, false, true);
                                //writing the PDF to FS
                                System.IO.File.WriteAllBytes(targetFolder + TemplateID + "/pages.pdf", PDFFile);
                                //gernating 
                                generatePagePreviewMultiplage(PDFFile, targetFolder + TemplateID + "/", objProduct.CuttingMargin.Value, 150, objSettings.isRoundCornerrs);
                                if (hasOverlayObject)
                                {
                                    byte[] overlayPDFFile = generatePDF(objProduct, oTemplatePages, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, objSettings.printCropMarks,objSettings.printWaterMarks, out hasOverlayObject, true, true);
                                    System.IO.File.WriteAllBytes(targetFolder + TemplateID + "/pagesoverlay.pdf", overlayPDFFile);
                                    generatePagePreviewMultiplage(overlayPDFFile, targetFolder + TemplateID + "/", objProduct.CuttingMargin.Value, 150, objSettings.isRoundCornerrs);
                                }
                            }
                            else
                            {
                                foreach (TemplatePages objPage in oTemplatePages)
                                {
                                    bool hasOverlayObject = false;
                                    byte[] PDFFile = generatePDF(objProduct, objPage, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, objSettings.printCropMarks, objSettings.printWaterMarks, out hasOverlayObject, false);
                                    //writing the PDF to FS
                                    System.IO.File.WriteAllBytes(targetFolder + TemplateID + "/p" + objPage.PageNo + ".pdf", PDFFile);
                                    //gernating 
                                    generatePagePreview(PDFFile, targetFolder, TemplateID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, objSettings.isRoundCornerrs);
                                    List<TemplateObjects> clippingPaths = lstTemplatesObjects.Where(g => g.ProductPageId == objPage.ProductPageID && g.hasClippingPath == true && g.IsOverlayObject != true).ToList();
                                    if (clippingPaths.Count > 0)
                                    {
                                        ClippingPathService objService = new ClippingPathService();
                                        double height, width = 0;
                                        if (objPage.Orientation == 1) //standard 
                                        {
                                            height = objProduct.PDFTemplateHeight.Value;
                                            width = objProduct.PDFTemplateWidth.Value;

                                        }
                                        else
                                        {
                                            height = objProduct.PDFTemplateWidth.Value;
                                           width = objProduct.PDFTemplateHeight.Value;

                                        }
                                        objService.generateClippingPaths(targetFolder + TemplateID + "/p" + objPage.PageNo + ".pdf", clippingPaths, targetFolder + TemplateID + "/p" + objPage.PageNo + "clip.pdf",width,height);
                                        File.Copy(targetFolder + TemplateID + "/p" + objPage.PageNo + "clip.pdf", targetFolder + TemplateID + "/p" + objPage.PageNo + ".pdf", true);
                                        File.Delete(targetFolder + TemplateID + "/p" + objPage.PageNo + "clip.pdf");
                                        generatePagePreview(targetFolder + TemplateID + "/p" + objPage.PageNo + ".pdf", targetFolder, TemplateID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, objSettings.isRoundCornerrs);
                                    }
                                    
                                    if (hasOverlayObject)
                                    {
                                        objPage.hasOverlayObjects = true;
                                        // generate overlay PDF 
                                        byte[] overlayPDFFile = generatePDF(objProduct, objPage, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, objSettings.printCropMarks, objSettings.printWaterMarks, out hasOverlayObject, true);
                                        System.IO.File.WriteAllBytes(targetFolder + TemplateID + "/p" + objPage.PageNo + "overlay.pdf", overlayPDFFile);
                                        generatePagePreview(overlayPDFFile, targetFolder, TemplateID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, objSettings.isRoundCornerrs);
                                        List<TemplateObjects> overlayClippingPaths = lstTemplatesObjects.Where(g => g.ProductPageId == objPage.ProductPageID && g.hasClippingPath == true && g.IsOverlayObject == true).ToList();
                                        if (clippingPaths.Count > 0)
                                        {
                                            ClippingPathService objService = new ClippingPathService();
                                            double height, width = 0;
                                            if (objPage.Orientation == 1) //standard 
                                            {
                                                height = objProduct.PDFTemplateHeight.Value;
                                                width = objProduct.PDFTemplateWidth.Value;

                                            }
                                            else
                                            {
                                                height = objProduct.PDFTemplateWidth.Value;
                                                width = objProduct.PDFTemplateHeight.Value;

                                            }
                                            objService.generateClippingPaths(targetFolder + TemplateID + "/p" + objPage.PageNo + "overlay.pdf", overlayClippingPaths, targetFolder + TemplateID + "/p" + objPage.PageNo + "clipoverlay.pdf",width,height);
                                            File.Copy(targetFolder + TemplateID + "/p" + objPage.PageNo + "clipoverlay.pdf", targetFolder + TemplateID + "/p" + objPage.PageNo + "overlay.pdf",true);
                                            File.Delete(targetFolder + TemplateID + "/p" + objPage.PageNo + "clipoverlay.pdf");
                                            generatePagePreview(targetFolder + TemplateID + "/p" + objPage.PageNo + "overlay.pdf", targetFolder, TemplateID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, objSettings.isRoundCornerrs);
                                        }
                                    }
                                    else
                                    {
                                        objPage.hasOverlayObjects = false;
                                    }
                                    db.SaveChanges();
                                }
                            }
                        }

                        if (Mode == 3)
                        {
                            //using (WebStoreSvc.WebstoreClient oWebstoreSVC = new WebStoreSvc.WebstoreClient())
                            //{

                            //    //oWebstoreSVC.SaveDesignAttachments(TemplateID, oTemplatePages.ToArray());
                            //    //return oWebstoreSVC.testing(TemplateID, "hoola");
                            //}

                        }


                    }

                    return "true";
                }
                else

                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return "false";
            }
            catch (Exception ex)
            {

                Util.LogException(ex);
                return ex.ToString();
            }
        }

        
        public void generateRoundCorners(string physicalPath, string pathToSave, Stream str)
        {
            string path = physicalPath;
            int roundedDia = 30;

            Bitmap bitmap = null;
            using (System.Drawing.Image imgin = System.Drawing.Image.FromStream(str))//FromFile(path)
            {
                bitmap = new Bitmap(imgin.Width, imgin.Height);
                try
                {
                    Graphics g = Graphics.FromImage(bitmap);
                    g.Clear(Color.Transparent);
                    g.SmoothingMode = (System.Drawing.Drawing2D.SmoothingMode.AntiAlias);
                    Brush brush = new System.Drawing.TextureBrush(imgin);
                    FillRoundedRectangle(g, new Rectangle(0, 0, imgin.Width, imgin.Height), roundedDia, brush);
                    g.Dispose();
                    if (File.Exists(pathToSave))
                    {
                        File.Delete(pathToSave);
                    }
                    bitmap.Save(pathToSave, System.Drawing.Imaging.ImageFormat.Png);

                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    bitmap.Dispose();
                }
            }


        }

        public static void FillRoundedRectangle(Graphics g, Rectangle r, int d, Brush b)
        {

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);

            g.FillPath(b, gp);
        }

        ///////////////////////////////////////// Business functions /////////////////////////////////////////////

        public byte[] generatePDF(Templates objProduct, TemplatePages objProductPage, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode)
        {

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                Doc doc = new Doc();
                try
                {
                    //  bool ObjSide2 = false;

                    //var objProduct = db.Templates.Where(g => g.ProductID == ProductID).Single();
                    //var objProductPage = db.TemplatePages.Where(g => g.ProductPageID == ProductPageID).Single();
                    var FontsList = db.TemplateFonts.ToList();


                    //  XSettings.License = "810-031-225-276-0715-601";

                    doc.TopDown = true;

                    try
                    {

                        if (!isoverLayMode)
                        {
                            if (objProductPage.BackGroundType == 1)  //PDF background
                            {
                                if (File.Exists(ProductFolderPath + objProductPage.BackgroundFileName))
                                    doc.Read(ProductFolderPath + objProductPage.BackgroundFileName);
                            }
                            else if (objProductPage.BackGroundType == 2) //background color
                            {
                                if (objProductPage.Orientation == 1) //standard 
                                {
                                    doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                    doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                                }
                                else
                                {
                                    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                                }
                                doc.AddPage();
                                LoadBackColor(ref doc, objProductPage,doc.PageNumber);
                            }
                            else if (objProductPage.BackGroundType == 3) //background Image
                            {

                                if (objProductPage.Orientation == 1) //standard 
                                {
                                    doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                    doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                                }
                                else
                                {
                                    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                                }
                                doc.AddPage();
                                LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath,doc.PageNumber);
                            }
                        }
                        else
                        {
                            if (objProductPage.Orientation == 1) //standard 
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                            }
                            else
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                            }
                            doc.AddPage();
                        }
                    }
                    catch (Exception ex)
                    {
                        Util.LogException(ex);
                        throw ex;
                    }


                    double YFactor = 0;
                    double XFactor = 0;
                    // int RowCount = 0;




                    List<TemplateObjects> oParentObjects = null;

                    if (IsDrawHiddenObjects)
                    {
                        if (isoverLayMode == true)
                        {
                            oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && (g.IsOverlayObject == true) && (g.hasClippingPath == false|| g.hasClippingPath == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                        else
                        {
                            oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && (g.IsOverlayObject == false || g.IsOverlayObject == null) && (g.hasClippingPath == false || g.hasClippingPath == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                    }
                    else
                    {
                        if (isoverLayMode == true)
                        {
                            oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == true) && (g.hasClippingPath == false || g.hasClippingPath == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                        else
                        {
                            oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == false || g.IsOverlayObject == null) && (g.hasClippingPath == false || g.hasClippingPath == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                    }
                    int count =  db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && (g.IsOverlayObject == true)).Count();
                    hasOverlayObject = false;
                    if (count > 0)
                    {
                        hasOverlayObject = true;
                    }
                    foreach (var objObjects in oParentObjects)
                    {
                       
                        if (XFactor != objObjects.PositionX)
                        {
                            if (objObjects.ContentString == "")
                                YFactor = objObjects.PositionY.Value - 7;
                            else
                                YFactor = 0;
                            XFactor = objObjects.PositionX.Value;
                        }



                        if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
                        {


                            int VAlign = 1, HAlign = 1;

                            HAlign = objObjects.Allignment.Value;

                            VAlign = objObjects.VAllignment.Value;

                            double currentX = objObjects.PositionX.Value, currentY = objObjects.PositionY.Value;


                            if (VAlign == 1 || VAlign == 2)
                                currentY = objObjects.PositionY.Value + objObjects.MaxHeight.Value;
                            bool isTemplateSpot = false;
                            if (objProduct.isSpotTemplate.HasValue == true && objProduct.isSpotTemplate.Value == true)
                                isTemplateSpot = true;

                            AddTextObject(objObjects, objProductPage.PageNo.Value, FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth.Value, objObjects.MaxHeight.Value, isTemplateSpot);



                        }
                            // object type 13 real state property image 

                        else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8 || objObjects.ObjectType == 12 || objObjects.ObjectType == 13) //3 = image and (8 ) =  Company Logo   12  = contactperson logo 13 = real state image placeholders 
                        {
                            //if (objObjects.ObjectType == 8 || objObjects.ObjectType == 12)
                           // {
                            if (!objObjects.ContentString.Contains("assets/Imageplaceholder") && !objObjects.ContentString.Contains("{{"))
                                {
                                    if (objObjects.ClippedInfo == null)
                                    {
                                        LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                    }
                                    else
                                    {
                                        LoadCroppedImg(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                    }
                                }
                          //  }
                          //  else
                           // {
                           //     LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                           // }
                        }
                        else if (objObjects.ObjectType == 5)    //line vector
                        {
                            DrawVectorLine(ref doc, objObjects, objProductPage.PageNo.Value);
                        }
                        else if (objObjects.ObjectType == 6)    //line vector
                        {
                            DrawVectorRectangle(ref doc, objObjects, objProductPage.PageNo.Value);
                        }
                        else if (objObjects.ObjectType == 7)    //line vector
                        {
                            DrawVectorEllipse(ref doc, objObjects, objProductPage.PageNo.Value);
                        }
                        else if (objObjects.ObjectType == 9)    //svg Path
                        {
                            GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                            //DrawSVGVectorPath(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                        }

                    }

                    //crop marks or margins
                    if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0)
                    {
                        //doc.CropBox.Height = doc.MediaBox.Height;
                        //doc.CropBox.Width = doc.MediaBox.Width;


                        bool isWaterMarkText = objProduct.isWatermarkText ?? true;
                        double TrimBoxSize = 5;
                        if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null)
                        {
                            TrimBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]);
                        }
                        doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left + Util.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Top + Util.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Width - Util.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Height - Util.MMToPoint(TrimBoxSize)).ToString());
                        if (System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"] != null)
                        {
                            double ArtBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"]);
                            doc.SetInfo(doc.Page, "/ArtBox:Rect", (doc.MediaBox.Left + Util.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Top + Util.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Width - Util.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Height - Util.MMToPoint(ArtBoxSize)).ToString());

                        }
                        if (System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"] != null)
                        {
                            double BleedBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"]);
                            doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + Util.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Top + Util.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Width - Util.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Height - Util.MMToPoint(BleedBoxSize)).ToString());
                        }
                            int FontID = 0;
                        var pFont = FontsList.Where(g => g.FontName == "Arial Black").FirstOrDefault();

                        if (pFont != null)
                        {
                            string path = "";
                            if (pFont.FontPath == null)
                            {
                                // mpc designers fonts or system fonts 
                                path = "PrivateFonts/FontFace/";//+ objFont.FontFile;
                            }
                            else
                            {  // customer fonts 

                                path = pFont.FontPath;
                            }
                            if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
                                FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");
                        }

                        doc.Font = FontID;
                        DrawCuttingLines(ref doc, objProduct.CuttingMargin.Value, 1, objProductPage.PageName, objProduct.TempString, drawCuttingMargins, drawWaterMark, isWaterMarkText, objProduct.PDFTemplateHeight.Value, objProduct.PDFTemplateWidth.Value);
                        // doc.Rect.String = doc.CropBox.String;
                        // added by saqib for triming obj
                        // doc.Rect.Inset(Convert.ToDouble(objProduct.CuttingMargin), Convert.ToDouble(objProduct.CuttingMargin));
                    }

                    if (IsDrawBGText == true)
                    {
                        DrawBackgrounText(ref doc);
                    }
                    int res = 300;
                    if (System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"] != null)
                    {
                        res = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"]);
                    }
                    doc.Rendering.DotsPerInch = res;

                    //if (ShowHighResPDF == false)
                    //    opage.Session["PDFFile"] = doc.GetData();
                    //OpenPage(opage, "Admin/Products/ViewPdf.aspx");

                    return doc.GetData();
                }
                catch (Exception ex)
                {
                    Util.LogException(ex);
                    throw new Exception("ShowPDF", ex);
                }
                finally
                {
                    doc.Dispose();
                }
            }
        }


        public byte[] generatePDF(Templates objProduct, List<TemplatePages> objProductPages, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode,bool drawBleedArea)
        {
            hasOverlayObject = false;
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                Doc doc = new Doc();
                try
                {
                    //  bool ObjSide2 = false;

                    //var objProduct = db.Templates.Where(g => g.ProductID == ProductID).Single();
                    //var objProductPage = db.TemplatePages.Where(g => g.ProductPageID == ProductPageID).Single();
                    var FontsList = db.TemplateFonts.ToList();

                    doc.TopDown = true;
                    foreach (var objProductPage in objProductPages)
                    {
                        try
                        {
                            if (!isoverLayMode)
                            {
                                if (objProductPage.BackGroundType == 1)  //PDF background
                                {

                                    if (File.Exists(ProductFolderPath + objProductPage.BackgroundFileName))
                                    {
                                        using (var cPage = new Doc())
                                        {
                                            try
                                            {
                                                cPage.Read(ProductFolderPath + objProductPage.BackgroundFileName);
                                                doc.Append(cPage);
                                                doc.PageNumber = objProductPage.PageNo.Value;
                                            }
                                            catch (Exception ex)
                                            {
                                                Util.LogException(ex);
                                                throw new Exception("Appedning", ex);
                                            }
                                            finally
                                            {
                                                cPage.Dispose();
                                            }
                                            
                                        }
                                    }
                                }
                                else if (objProductPage.BackGroundType == 2) //background color
                                {
                                    if (objProductPage.Orientation == 1) //standard 
                                    {
                                        doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                        doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                                    }
                                    else
                                    {
                                        doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                        doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                                    }
                                    doc.AddPage();
                                    doc.PageNumber = objProductPage.PageNo.Value;
                                    LoadBackColor(ref doc, objProductPage,doc.PageNumber);
                                }
                                else if (objProductPage.BackGroundType == 3) //background Image
                                {

                                    if (objProductPage.Orientation == 1) //standard 
                                    {
                                        doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                        doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                                    }
                                    else
                                    {
                                        doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                        doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                                    }
                                    doc.AddPage();
                                    doc.PageNumber = objProductPage.PageNo.Value;
                                    LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath,doc.PageNumber);
                                }
                            }
                            else
                            {
                                if (objProductPage.Orientation == 1) //standard 
                                {
                                    doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                    doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                                }
                                else
                                {
                                    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                                }
                                doc.AddPage();
                            }

                        }
                        catch (Exception ex)
                        {
                            Util.LogException(ex);
                            throw ex;
                        }


                        double YFactor = 0;
                        double XFactor = 0;
                        // int RowCount = 0;




                        List<TemplateObjects> oParentObjects = null;

                        if (IsDrawHiddenObjects)
                        {
                            if (isoverLayMode == true)
                            {
                                oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
                            }
                            else
                            {
                                oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                            }
                        }
                        else
                        {
                            if (isoverLayMode == true)
                            {
                                oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
                            }
                            else
                            {
                                oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                            }
                        }
                        int count = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && (g.IsOverlayObject == true)).Count();

                        if (count > 0)
                        {
                            hasOverlayObject = true;
                        }
                        foreach (var objObjects in oParentObjects)
                        {

                            if (XFactor != objObjects.PositionX)
                            {
                                if (objObjects.ContentString == "")
                                    YFactor = objObjects.PositionY.Value - 7;
                                else
                                    YFactor = 0;
                                XFactor = objObjects.PositionX.Value;
                            }



                            if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
                            {


                                int VAlign = 1, HAlign = 1;

                                HAlign = objObjects.Allignment.Value;

                                VAlign = objObjects.VAllignment.Value;

                                double currentX = objObjects.PositionX.Value, currentY = objObjects.PositionY.Value;


                                if (VAlign == 1 || VAlign == 2)
                                    currentY = objObjects.PositionY.Value + objObjects.MaxHeight.Value;
                                bool isTemplateSpot = false;
                                if (objProduct.isSpotTemplate.HasValue == true && objProduct.isSpotTemplate.Value == true)
                                    isTemplateSpot = true;

                                AddTextObject(objObjects, objProductPage.PageNo.Value, FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth.Value, objObjects.MaxHeight.Value, isTemplateSpot);



                            }
                            // object type 13 real state property image 

                            else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8 || objObjects.ObjectType == 12 || objObjects.ObjectType == 13) //3 = image and (8 ) =  Company Logo   12  = contactperson logo 13 = real state image placeholders 
                            {
                                //if (objObjects.ObjectType == 8 || objObjects.ObjectType == 12)
                                // {
                                if (!objObjects.ContentString.Contains("assets/Imageplaceholder") && !objObjects.ContentString.Contains("{{"))
                                {
                                    if (objObjects.ClippedInfo == null)
                                    {
                                        LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                    }
                                    else
                                    {
                                        LoadCroppedImg(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                    }
                                }
                                //  }
                                //  else
                                // {
                                //     LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                // }
                            }
                            else if (objObjects.ObjectType == 5)    //line vector
                            {
                                DrawVectorLine(ref doc, objObjects, objProductPage.PageNo.Value);
                            }
                            else if (objObjects.ObjectType == 6)    //line vector
                            {
                                DrawVectorRectangle(ref doc, objObjects, objProductPage.PageNo.Value);
                            }
                            else if (objObjects.ObjectType == 7)    //line vector
                            {
                                DrawVectorEllipse(ref doc, objObjects, objProductPage.PageNo.Value);
                            }
                            else if (objObjects.ObjectType == 9)    //svg Path
                            {
                                GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                //DrawSVGVectorPath(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                            }

                        }
                        if (drawBleedArea)
                        {
                            double TrimBoxSize = 5;
                            if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null)
                            {
                                TrimBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]);
                            }
                            doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left + Util.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Top + Util.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Width - Util.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Height - Util.MMToPoint(TrimBoxSize)).ToString());
                            if (System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"] != null)
                            {
                                double ArtBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"]);
                                doc.SetInfo(doc.Page, "/ArtBox:Rect", (doc.MediaBox.Left + Util.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Top + Util.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Width - Util.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Height - Util.MMToPoint(ArtBoxSize)).ToString());

                            }
                            if (System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"] != null)
                            {
                                double BleedBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"]);
                                doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + Util.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Top + Util.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Width - Util.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Height - Util.MMToPoint(BleedBoxSize)).ToString());
                            }
                        }
                        //crop marks or margins
                        if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0 && drawCuttingMargins)
                        {
                            //doc.CropBox.Height = doc.MediaBox.Height;
                            //doc.CropBox.Width = doc.MediaBox.Width;


                            bool isWaterMarkText = objProduct.isWatermarkText ?? true;
                          
                            int FontID = 0;
                            var pFont = FontsList.Where(g => g.FontName == "Arial Black").FirstOrDefault();

                            if (pFont != null)
                            {
                                string path = "";
                                if (pFont.FontPath == null)
                                {
                                    // mpc designers fonts or system fonts 
                                    path = "PrivateFonts/FontFace/";//+ objFont.FontFile;
                                }
                                else
                                {  // customer fonts 

                                    path = pFont.FontPath;
                                }
                                if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
                                    FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");
                            }

                            doc.Font = FontID;
                            DrawCuttingLines(ref doc, objProduct.CuttingMargin.Value, 1, objProductPage.PageName, objProduct.TempString, drawCuttingMargins, drawWaterMark, isWaterMarkText, objProduct.PDFTemplateHeight.Value, objProduct.PDFTemplateWidth.Value);
                            // doc.Rect.String = doc.CropBox.String;
                            // added by saqib for triming obj
                            // doc.Rect.Inset(Convert.ToDouble(objProduct.CuttingMargin), Convert.ToDouble(objProduct.CuttingMargin));
                        }

                        if (IsDrawBGText == true)
                        {
                            DrawBackgrounText(ref doc);
                        }
                        int res = 300;
                        if (System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"] != null)
                        {
                            res = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"]);
                        }
                        doc.Rendering.DotsPerInch = res;

                        //if (ShowHighResPDF == false)
                        //    opage.Session["PDFFile"] = doc.GetData();
                        //OpenPage(opage, "Admin/Products/ViewPdf.aspx");

                    }
                    return doc.GetData();
                }
                catch (Exception ex)
                {
                    Util.LogException(ex);
                    throw new Exception("ShowPDF", ex);
                }
                finally
                {
                    doc.Dispose();
                }

            }
        }



        //public string Save(Stream data)
        //{
        //    try
        //    {


        //        int TemplateID = 0;

        //        StreamReader reader = new StreamReader(data);
        //        string res = reader.ReadToEnd();
        //        reader.Close();
        //        reader.Dispose();

        //        JsonSerializerSettings oSettings = new JsonSerializerSettings();
        //        oSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
        //        oSettings.NullValueHandling = NullValueHandling.Ignore;
        //        oSettings.ObjectCreationHandling = ObjectCreationHandling.Auto;


        //        List<TemplateObjects> lstTemplatesObjects = JsonConvert.DeserializeObject<List<TemplateObjects>>(res,oSettings);

        //        if (lstTemplatesObjects.Count > 0)
        //        {

        //            TemplateID = lstTemplatesObjects[0].ProductID;
        //            using (TransactionScope scope = new TransactionScope())
        //            {

        //                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //                {

        //                    foreach (TemplateObjects c in db.TemplateObjects.Where(g => g.ProductID == TemplateID))
        //                    {
        //                        db.DeleteObject(c);
        //                    }
        //                    db.SaveChanges(false);
        //                    db.AcceptAllChanges(); 

        //                    //dbObjects = new printdesignBLL.Products.Objects();
        //                    //printdesignBLL.Products.Objects.SaveObjects(dbProduct.ProductID, objObjectsList);

        //                    foreach (var oObject in lstTemplatesObjects)
        //                    {
        //                        if (oObject.ObjectID != -999)
        //                        {
        //                            oObject.PositionX = Util.PixelToPoint(oObject.PositionX);
        //                            oObject.PositionY = Util.PixelToPoint(oObject.PositionY);
        //                            oObject.FontSize = Util.PixelToPoint(oObject.FontSize);
        //                            oObject.MaxWidth = Util.PixelToPoint(oObject.MaxWidth);
        //                            oObject.MaxHeight = Util.PixelToPoint(oObject.MaxHeight);



        //                            oObject.ProductID = TemplateID;
        //                            db.TemplateObjects.AddObject(oObject);
        //                        }
        //                    }
        //                    db.SaveChanges(false);
        //                    scope.Complete();
        //                    db.AcceptAllChanges(); 


        //                }
        //            }
        //        }


        //        return "true";
        //    }
        //    catch (Exception ex)
        //    {
        //        Util.LogException(ex);
        //        return ex.ToString();
        //    }
        //}


        private void LoadBackColor(ref Doc oPdf, TemplatePages oTemplate,int pageNo)
        {

            try
            {
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;

                oPdf.PageNumber = pageNo;
                oPdf.Layer = oPdf.LayerCount + 1;

                //oPdf.Color.Alpha = 255;
                oPdf.Color.Cyan = oTemplate.ColorC.Value;
                oPdf.Color.Magenta = oTemplate.ColorM.Value;
                oPdf.Color.Yellow = oTemplate.ColorY.Value;
                oPdf.Color.Black = oTemplate.ColorK.Value;

                oPdf.FillRect();


            }
            catch (Exception ex)
            {
                throw new Exception("LoadBackColor", ex);
            }

        }


        private void LoadBackGroundImage(ref Doc oPdf, TemplatePages oTemplate, string imgPath,int pageNo)
        {

            try
            {
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;


                oPdf.PageNumber = pageNo;
                oPdf.Layer = oPdf.LayerCount + 1;
                // oPdf.AddImageFile(imgPath + oTemplate.BackgroundFileName,1);
                string logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                XImage oImg = new XImage();
                bool bFileExists = false;
                string FilePath = imgPath + oTemplate.BackgroundFileName;
                bFileExists = System.IO.File.Exists(FilePath);

                if (bFileExists)
                {

                    oImg.SetFile(FilePath);
                    //oPdf.FrameRect();
                    oPdf.AddImageObject(oImg, true);
                    oPdf.Transform.Reset();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("LoadBackGroundArtWork", ex);
            }

        }
        private objTextStyles GetStyleByCharIndex(int index, List<objTextStyles> objStyles)
        {
            foreach (var obj in objStyles)
            {
                if (obj.characterIndex == index.ToString())
                {
                    return obj;
                }
            }
            return null;
        }

        private void AddTextObject(TemplateObjects ooBject, int PageNo, List<TemplateFonts> oFonts, ref Doc oPdf, string Font, double OPosX, double OPosY, double OWidth, double OHeight, bool isTemplateSpot)
        {

            try
            {
                oPdf.TextStyle.Outline = 0;
                oPdf.TextStyle.Strike = false;
                oPdf.TextStyle.Bold = false;
                oPdf.TextStyle.Italic = false;
                oPdf.TextStyle.CharSpacing = 0;
                oPdf.PageNumber = PageNo;
                if (ooBject.CharSpacing != null)
                {
                    oPdf.TextStyle.CharSpacing = Convert.ToDouble(ooBject.CharSpacing.Value);
                }

                //    OPosY  =OPosY - (ooBject.FontSize.Value / 21);
                double yRPos = 0;
                if (oPdf.TopDown == true)
                    yRPos = oPdf.MediaBox.Height - ooBject.PositionY.Value;
                if (ooBject.ColorType.Value == 3)
                {
                    if (isTemplateSpot)
                    {
                        if (ooBject.IsSpotColor.HasValue == true && ooBject.IsSpotColor.Value == true)
                        {
                            oPdf.ColorSpace = oPdf.AddColorSpaceSpot(ooBject.SpotColorName, ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
                            oPdf.Color.Gray = 255;
                        }
                    }
                    else
                    {
                        oPdf.Color.String = ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString();
                    }

                    //if (!ooBject.IsColumnNull("Tint"))
                    if (ooBject.Tint.HasValue)
                    {
                        oPdf.Color.Alpha = Convert.ToInt32((100 - ooBject.Tint) * 2.55);
                    }
                }
                else if (ooBject.ColorType.Value == 4) // For RGB Colors
                {
                    oPdf.Color.String = ooBject.RColor.ToString() + " " + ooBject.GColor.ToString() + " " + ooBject.BColor.ToString();
                }

                //for commented code see change book or commit before 3rd march
                int FontID = 0;
                var pFont = oFonts.Where(g => g.FontName == ooBject.FontName).FirstOrDefault();
                string path = "";
                if (pFont != null)
                {
                    
                    if (pFont.FontPath == null)
                    {
                        // mpc designers fonts or system fonts 
                        path = "PrivateFonts/FontFace/";//+ objFont.FontFile;
                    }
                    else
                    {  // customer fonts 

                        path = pFont.FontPath;
                    }
                    if (System.IO.File.Exists(Font + path + pFont.FontFile + ".ttf"))
                        FontID = oPdf.EmbedFont(Font + path + pFont.FontFile + ".ttf");
                }

                oPdf.Font = FontID;
                oPdf.TextStyle.Size = ooBject.FontSize.Value;
                if (ooBject.IsUnderlinedText.HasValue)
                    oPdf.TextStyle.Underline = ooBject.IsUnderlinedText.Value;
                oPdf.TextStyle.Bold = ooBject.IsBold.Value;

                oPdf.TextStyle.Italic = ooBject.IsItalic.Value;
                double linespacing = ooBject.LineSpacing.Value - 1;
                linespacing = (linespacing * ooBject.FontSize.Value);
                oPdf.TextStyle.LineSpacing = linespacing;
                if (ooBject.Allignment == 1)
                {
                    oPdf.HPos = 0.0;
                }
                else if (ooBject.Allignment == 2)
                {
                    oPdf.HPos = 0.5;
                }
                else if (ooBject.Allignment == 3)
                {
                    oPdf.HPos = 1.0;
                }

                if (ooBject.RotationAngle != 0)
                {

                    oPdf.Transform.Reset();
                    oPdf.Transform.Rotate(360 - ooBject.RotationAngle.Value, OPosX + (OWidth / 2), oPdf.MediaBox.Height - OPosY);
                }
                List<objTextStyles> styles = new List<objTextStyles>();
                if (ooBject.textStyles != null)
                {
                    styles = JsonConvert.DeserializeObject<List<objTextStyles>>(ooBject.textStyles);
                }
                string StyledHtml = "<p>";
                if (styles.Count != 0)
                {
                    styles = styles.OrderBy(g => g.characterIndex).ToList();
                    for (int i = 0; i < ooBject.ContentString.Length; i++)
                    {
                        objTextStyles objStyle = GetStyleByCharIndex(i, styles);
                        if (objStyle != null && ooBject.ContentString[i] != '\n')
                        {
                            if (objStyle.fontName == null && objStyle.fontSize == null && objStyle.fontStyle == null && objStyle.fontWeight == null && objStyle.textColor == null)
                            {
                                StyledHtml += ooBject.ContentString[i];
                            }
                            else
                            {
                                string toApplyStyle = ooBject.ContentString[i].ToString();
                                string fontTag = "<font";
                                string fontSize = "";
                                string pid = "";
                                if (objStyle.fontName != null)
                                {
                                    var oFont = oFonts.Where(g => g.FontName == objStyle.fontName).FirstOrDefault();
                                    if (System.IO.File.Exists(Font + path + oFont.FontFile + ".ttf"))
                                        FontID = oPdf.EmbedFont(Font + path + oFont.FontFile + ".ttf");
                                   // fontTag += " face='" + objStyle.fontName + "' embed= "+ FontID+" ";
                                    pid = "pid ='" + FontID.ToString() +"' ";
                                }
                                string lineSpacingString = "";
                                if (ooBject.LineSpacing != null)
                                {
                                    lineSpacingString = " linespacing= " + (ooBject.LineSpacing * ooBject.FontSize.Value) + " ";
                                }
                                if (objStyle.fontSize != null)
                                {
                                    lineSpacingString = " linespacing= " + (ooBject.LineSpacing * Convert.ToInt32(Util.PixelToPoint(Convert.ToDouble(objStyle.fontSize)))) + " ";
                                    fontSize += "<StyleRun fontsize='" + Convert.ToInt32(Util.PixelToPoint(Convert.ToDouble(objStyle.fontSize))) + "' " + pid + lineSpacingString + ">";
                                    fontTag += " fontsize='" + Convert.ToInt32(Util.PixelToPoint(Convert.ToDouble(objStyle.fontSize))) + "' " + lineSpacingString + " ";
                                }
                               
                                if (objStyle.fontStyle != null)
                                {
                                    fontTag += " font-style='" + objStyle.fontStyle + "'";
                                }
                                if (objStyle.fontWeight != null)
                                {
                                    fontTag += " font-weight='" + objStyle.fontWeight + "'";
                                }
                                if (objStyle.textColor != null)
                                {
                                    if (objStyle.textCMYK != null)
                                    {
                                        string[] vals = objStyle.textCMYK.Split(' ');
                                        string hexC = Convert.ToInt32(vals[0]).ToString("X");
                                        if (hexC.Length == 1)
                                            hexC = "0" + hexC;
                                        string hexM = Convert.ToInt32(vals[1]).ToString("X");
                                        if (hexM.Length == 1)
                                            hexM = "0" + hexM;
                                        string hexY = Convert.ToInt32(vals[2]).ToString("X");
                                        if (hexY.Length == 1)
                                            hexY = "0" + hexY;
                                        string hexK = Convert.ToInt32(vals[3]).ToString("X");
                                        if (hexK.Length == 1)
                                            hexK = "0" + hexK;
                                        string hex = "#" + hexC + hexM + hexY + hexK;
                                       // int csInlineID = oPdf.AddColorSpaceSpot("InlineStyle" + i.ToString(), objStyle.textCMYK);
                                        //oPdf.Color.Gray = 255;
                                       // fontTag += " color='#FF' csid=" + csInlineID;
                                        fontTag += " color='"+hex+"' ";
                                    }
                                    else
                                    {
                                        fontTag += " color='" + objStyle.textColor + "'";
                                    }
                                }
                                else
                                {
                                    if (objStyle.fontName != null)
                                    {
                                     //   Utilities.CMYKtoRGBConverter objCData = new Utilities.CMYKtoRGBConverter();
                                       // string colorHex = objCData.getColorHex();
                                       // int csInlineID = oPdf.AddColorSpaceSpot("InlineStyle" + i.ToString(), ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
                                        string hexC = Convert.ToInt32(ooBject.ColorC).ToString("X");
                                        if (hexC.Length == 1)
                                            hexC = "0" + hexC;
                                        string hexM = Convert.ToInt32(ooBject.ColorM).ToString("X");
                                        if (hexM.Length == 1)
                                            hexM = "0" + hexM;
                                        string hexY = Convert.ToInt32(ooBject.ColorY).ToString("X");
                                        if (hexY.Length == 1)
                                            hexY = "0" + hexY;
                                        string hexK = Convert.ToInt32(ooBject.ColorK).ToString("X");
                                        if (hexK.Length == 1)
                                            hexK = "0" + hexK;
                                        string hex = "#" + hexC + hexM + hexY + hexK;

                                        fontTag += " color='" + hex + "' ";
                                    }
                                    //fontTag += " color='" + objStyle.textColor + "'";
                                }
                                if (fontSize != "")
                                {
                                    toApplyStyle = fontTag + " >" + fontSize + toApplyStyle + "</StyleRun></font>";
                                } 
                                else
                                {
                                    if (objStyle.fontName != null)
                                    {
                                        fontSize += "<StyleRun " + pid + lineSpacingString + ">";
                                        toApplyStyle = fontTag + " >" + fontSize + toApplyStyle + "</StyleRun></font>";
                                    }
                                    else
                                    {
                                        toApplyStyle = fontTag + " >" + toApplyStyle + "</font>";
                                    }
                                    
                                }
                                StyledHtml += toApplyStyle;
                            }
                        }
                        else
                        {
                            StyledHtml += ooBject.ContentString[i];
                        }
                    }

                }
                else
                {
                    StyledHtml += ooBject.ContentString;
                }
                StyledHtml += "</p>";
                string sNewLineNormalized = Regex.Replace(StyledHtml, @"\r(?!\n)|(?<!\r)\n", "<BR>");
                sNewLineNormalized = sNewLineNormalized.Replace("  ", "&nbsp;&nbsp;");
                
                if (ooBject.AutoShrinkText == true)
                {
                    oPdf.Rect.Position(OPosX, OPosY);
                    oPdf.Rect.Resize(OWidth, OHeight);
                    int theID = oPdf.AddHtml(sNewLineNormalized);
                    while (oPdf.Chainable(theID))
                    {
                        oPdf.Delete(theID);
                        oPdf.FontSize--;
                        oPdf.Rect.String = oPdf.Rect.String; // reset Doc.Pos without having to save its initial value
                        theID = oPdf.AddHtml(sNewLineNormalized);
                    }
                }
                else
                {
                    oPdf.Rect.Position(OPosX, OPosY);
                    oPdf.Rect.Resize(OWidth, OHeight);
                    oPdf.AddHtml(sNewLineNormalized);
                }
                oPdf.Transform.Reset();
                // for commented old styles algo see commit of 13 april 2014



            }
            catch (Exception ex)
            {
                throw new Exception("ADDSingleLineText", ex);
            }

        }


        private double GetMaxLineWidth(int StIndx, List<TemplateObjects> dtObject)
        {
            double MaxWd = 0, objOffsetX = 0;
            for (int oIdx = StIndx; oIdx < dtObject.Count; oIdx++)
            {
                TemplateObjects row = dtObject[oIdx];
                if (row.ContentString != null && row.ContentString != string.Empty && row.IsHidden == false)
                {
                    objOffsetX = 0;//(row["OffsetX"] != DBNull.Value) ? Convert.ToDouble(row["OffsetX"]) : 0;
                    MaxWd += objOffsetX + row.MaxWidth.Value; //Convert.ToDouble(row["MaxWidth"]);

                    if (oIdx + 1 >= dtObject.Count)
                        break;
                    else
                    {
                        //TemplateObjects row2 = dtObject[oIdx + 1];
                        //if (row2.IsNewLine != null)
                        //{
                        //    if (fa)
                        //    {
                        //        break;
                        //    }
                        //}
                    }
                }
            }
            return MaxWd;
        }

        private void LoadCroppedImg(ref Doc oPdf, TemplateObjects oObject, string logoPath, int PageNo)
        {


            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            XImage oImg = new XImage();
            Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;
                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ObjectType == 8 || oObject.ObjectType == 12)
                {
                    logoPath = ""; 
                    string[] vals;
                    FilePath = "";
                    if (oObject.ContentString.Contains("StoredImages/"))
                    {
                        vals = oObject.ContentString.Split(new string[] { "StoredImages/" }, StringSplitOptions.None);
                        FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/../StoredImages/" + vals[vals.Length - 1]);
                    }
                    bFileExists = System.IO.File.Exists(FilePath);
                }
                else
                {
                    if (oObject.ContentString != "")
                        FilePath = oObject.ContentString;
                    FilePath = logoPath + "/" + FilePath;
                    bFileExists = System.IO.File.Exists(FilePath);
                }
                if (bFileExists)
                {
                    img = new Bitmap(System.Drawing.Image.FromFile(FilePath, true));
                    if (oObject.Opacity != null)
                    {
                        if (oObject.Opacity != 1)
                        {
                            img = Utilities.ImageTrasparency.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
                        }
                    }
                    oImg.SetData(Utilities.SvgParser.ImageToByteArraybyImageConverter(img));
                    
                    var posY = oObject.PositionY + oObject.MaxHeight;
                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);
                  
                    if (oObject.RotationAngle != null)
                    {
                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);
                        }
                    }
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(oObject.ClippedInfo);
                    string xpath = "Cropped";
                    var nodes = xmlDoc.SelectNodes(xpath);
                    double sx, sy, swidth, sheight;
                    foreach (XmlNode childrenNode in nodes)
                    {
                        sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
                        sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
                        swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
                        sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
                        oImg.Selection.Inset(sx, sy);
                        oImg.Selection.Height = sheight;
                        oImg.Selection.Width = swidth;
                    }
                    int id = oPdf.AddImageObject(oImg, true);
                    oPdf.Transform.Reset();
                }


            }
            catch (Exception ex)
            {
                throw new Exception("LoadImage", ex);
            }
            finally
            {
                oImg.Dispose();
                if (img != null)
                    img.Dispose();
            }
        }
        private void LoadImage(ref Doc oPdf, TemplateObjects oObject, string logoPath, int PageNo)
        {


            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            XImage oImg = new XImage();
            Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;


                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ObjectType == 8 || oObject.ObjectType == 12)
                {
                    logoPath = ""; //since path is already in filenm
                    string[] vals;
                    FilePath = "";
                    if (oObject.ContentString.Contains("StoredImages/"))
                    {
                        vals = oObject.ContentString.Split(new string[] { "StoredImages/" }, StringSplitOptions.None);
                        FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/../StoredImages/" + vals[vals.Length - 1]);
                    }

                    bFileExists = System.IO.File.Exists(FilePath);

                }
                else
                {
                    if (oObject.ContentString != "")
                        FilePath = oObject.ContentString;
                    FilePath = logoPath + "/" + FilePath;
                    bFileExists = System.IO.File.Exists(FilePath);
                }
                //  else
                //     filNm = oobject.LogoName;

                if (bFileExists)
                {
                    img = new Bitmap(System.Drawing.Image.FromFile(FilePath, true));
                    
                    if (oObject.Opacity != null)
                    {
                        // float opacity =float.Parse( oObject.Tint.ToString()) /100;
                        if (oObject.Opacity != 1)
                        {
                            img = Utilities.ImageTrasparency.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
                        }
                    }
                    oImg.SetData(Utilities.SvgParser.ImageToByteArraybyImageConverter(img));
                  //  oImg.SetFile(FilePath);
                    
                    var posY = oObject.PositionY + oObject.MaxHeight;

                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);
                  
                    if (oObject.RotationAngle != null)
                    {


                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);


                        }


                    }

                    //oPdf.FrameRect();

                    int id = oPdf.AddImageObject(oImg, true);
                    //if (oObject.Tint != null)
                    //{
                    //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                    //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                    //}

                    oPdf.Transform.Reset();
                }


            }
            catch (Exception ex)
            {
                throw new Exception("LoadImage", ex);
            }
            finally
            {
                oImg.Dispose();
                if (img != null)
                    img.Dispose();
            }
        }


        //vector line drawing in PDF
        private void DrawVectorLine(ref Doc oPdf, TemplateObjects oObject, int PageNo)
        {

            try
            {
                oPdf.PageNumber = PageNo;





                if (oObject.ColorType == 3)
                {
                    if (oObject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                    }
                    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();
                    //if (!ooBject.IsColumnNull("Tint"))
                    oPdf.Color.Alpha = Convert.ToInt32(( oObject.Tint) * 2.55);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }


                oPdf.Width = oObject.MaxHeight.Value;
                oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value);
                oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value);
                    }


                }

                // oPdf.AddImageObject(oImg,false);
                //oPdf.AddImage ((oImg);
                oPdf.AddLine(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value / 2, oObject.PositionX.Value + oObject.MaxWidth.Value, oObject.PositionY.Value + oObject.MaxHeight.Value / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorLine", ex);
            }

        }

        //vector rectangle drawing in PDF
        private void DrawVectorRectangle(ref Doc oPdf, TemplateObjects oObject, int PageNo)
        {

            try
            {
                oPdf.PageNumber = PageNo;

                if (oObject.ColorType == 3)
                {
                    if (oObject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                    }
                    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();
                    if (oObject.Opacity != null)
                        oPdf.Color.Alpha = Convert.ToInt32((100 * oObject.Opacity) * 2.55);
                    //if (!ooBject.IsColumnNull("Tint"))
                    //oPdf.Color.Alpha = 0;//Convert.ToInt32((100 - oObject.Tint) * 2.5);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }


                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value);
                oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value - oObject.MaxHeight.Value / 2);
                    }


                }

                // oPdf.AddImageObject(oImg,false);
                //oPdf.AddImage ((oImg);
                int id = oPdf.FillRect();
                //if (oObject.Tint != null)
                //{
                //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                //}
                //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorRectangle", ex);
            }

        }

        //vector Ellipse drawing in PDF
        private void DrawVectorEllipse(ref Doc oPdf, TemplateObjects oObject, int PageNo)
        {

            try
            {
                oPdf.PageNumber = PageNo;

                if (oObject.ColorType == 3)
                {
                    if (oObject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                    }
                    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();

                    if (oObject.Opacity != null)
                        oPdf.Color.Alpha = Convert.ToInt32((100 * oObject.Opacity) * 2.55);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }




                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value);
                oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value - oObject.MaxHeight.Value / 2);
                    }


                }

                int id = oPdf.FillRect(oObject.MaxWidth.Value / 2, oObject.MaxHeight.Value / 2);
                //if (oObject.Tint != null)
                //{
                //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                //}
                //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorEllipse", ex);
            }

        }

        private void GetSVGAndDraw(ref Doc oPdf, TemplateObjects oObject, string logoPath, int PageNo)
        {

            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            XImage oImg = new XImage();
             Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;
                bool bFileExists = false;
                string FilePath = string.Empty;
                    if (oObject.ContentString != "")
                        FilePath = oObject.ContentString;
                    FilePath = logoPath + "/" + FilePath;
                    bFileExists = System.IO.File.Exists(FilePath);
                if (bFileExists)
                {
                    Utilities.SvgParser.MaximumSize = new Size(Convert.ToInt32( oObject.MaxWidth), Convert.ToInt32( oObject.MaxHeight));
                    img = Utilities.SvgParser.GetBitmapFromSVG(FilePath, oObject.ColorHex);
                    if (oObject.Opacity != null)
                    {
                       // float opacity =float.Parse( oObject.Tint.ToString()) /100;
                        if (oObject.Opacity != 1)
                        {
                            img = Utilities.ImageTrasparency.ChangeOpacity(img,float.Parse( oObject.Opacity.ToString()));
                        }
                    }
                    oImg.SetData(Utilities.SvgParser.ImageToByteArraybyImageConverter(img));

                    var posY = oObject.PositionY + oObject.MaxHeight;

                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

                    if (oObject.RotationAngle != null)
                    {
                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);
                        }
                    }
                    int id = oPdf.AddImageObject(oImg, true);
                    //if (oObject.Tint != null)
                    //{
                    //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                       
                    //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                    //}
                    oPdf.Transform.Reset();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LoadSvg", ex);
            }
            finally
            {
                oImg.Dispose();
                if (img != null)
                    img.Dispose();
            }
        }
        //vector Ellipse drawing in PDF
        private void DrawSVGVectorPath(ref Doc oPdf, TemplateObjects oObject, string ProductFolderPath, int PageNo)
        {

            try
            {
                if (oObject.ContentString != string.Empty)
                {
                    Doc oImportSVG = new Doc();

                    string sheader = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
                    sheader += "<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1 Tiny//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11-tiny.dtd\">";
                    sheader += "<svg xmlns:svg=\"http://www.w3.org/2000/svg\"    xmlns=\"http://www.w3.org/2000/svg\" >";
                    sheader += oObject.ContentString;
                    sheader += " </svg>";

                    //oPdf.AddImage(d
                    XReadOptions opt = new XReadOptions();
                    opt.ReadModule = ReadModuleType.Svg;
                    oImportSVG.TopDown = true;
                    //oImportSVG.Read(Encoding.ASCII.GetBytes(sheader), opt);

                    XRect oRect = new XRect();

                    oRect.Position(0, 0);
                    oRect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value * 2);

                    oImportSVG.MediaBox.Height = oObject.MaxHeight.Value * 3;
                    oImportSVG.MediaBox.Width = oObject.MaxWidth.Value;


                    oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);
                    oImportSVG.FillRect();
                    //oPdf = oImportSVG;
                    oPdf.AddImageDoc(oImportSVG, PageNo, oRect);
                    oImportSVG.Dispose();
                }
                //oPdf.PageNumber = PageNo;

                //if (oObject.ColorType == 3)
                //{
                //    if (oObject.IsSpotColor == true)
                //    {
                //        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                //    }
                //    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();
                //    //if (!ooBject.IsColumnNull("Tint"))
                //    oPdf.Color.Alpha = Convert.ToInt32((100 - oObject.Tint) * 2.5);
                //}
                //else if (oObject.ColorType == 4) // For RGB Colors
                //{
                //    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                //}




                ////oPdf.Width = oobject.MaxHeight;
                //oPdf.Rect.Position(oObject.PositionX, oObject.PositionY + oObject.MaxHeight);
                //oPdf.Rect.Resize(oObject.MaxWidth, oObject.MaxHeight);


                //if (oObject.RotationAngle != null)
                //{

                //    if (oObject.RotationAngle != 0)
                //    {
                //        oPdf.Transform.Reset();
                //        oPdf.Transform.Rotate(oObject.RotationAngle, oObject.PositionX + oObject.MaxWidth / 2, oPdf.MediaBox.Height - oObject.PositionY - oObject.MaxHeight / 2);
                //    }


                //}

                //oPdf.FillRect(oObject.MaxWidth / 2, oObject.MaxHeight / 2);

                ////oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
                //oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorEllipse", ex);
            }

        }

        private void DrawCuttingLines(ref Doc oPdf, double mrg, int PageNo, string pageName, string waterMarkTxt, bool drawCuttingMargins, bool drawWatermark, bool isWaterMarkText, double pdfTemplateHeight, double pdfTemplateWidth)
        {
            try
            {
                oPdf.Color.String = "100 100 100 100";
 
             //   oPdf.ColorSpace = oPdf.AddColorSpaceSpot("Registration", "100 100 100 100");
              //  oPdf.Color.Gray = 255;
                if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null) // for digital central 
                {
                    mrg = Util.MMToPoint(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]));
                }
                double offset = 0;
                if (System.Configuration.ConfigurationManager.AppSettings["BleedOffset"] != null) // for digital central 
                {
                    offset = Util.MMToPoint(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["BleedOffset"]));
                }
                //mrg = 5.98110236159; // global change on request of digital central to make crop marks 2.11 mm
                oPdf.Layer = oPdf.LayerCount - 1;
                oPdf.PageNumber = PageNo;
                //oPdf.Width = 0.5;
                oPdf.Width = 0.25;
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
                double pgWidth = oPdf.MediaBox.Width;
                double pgHeight = oPdf.MediaBox.Height;
                for (int i = 1; i <= oPdf.PageCount; i++)
                {
                    oPdf.PageNumber = i;
                    if (drawCuttingMargins)
                    {
                        oPdf.Layer = 1;
                        //oPdf.AddLine(mrg + 3, 0, mrg + 3, mrg);   // commented to make box 5 mm exact on request of crystal media 
                        //oPdf.AddLine(0, mrg + 3, mrg, mrg + 3); 
                        //oPdf.AddLine(oPdf.MediaBox.Width - mrg - 3, 0, oPdf.MediaBox.Width - mrg - 3, mrg);
                        //oPdf.AddLine(oPdf.MediaBox.Width - mrg, mrg + 3, oPdf.MediaBox.Width, mrg + 3);
                        //oPdf.AddLine(0, oPdf.MediaBox.Height - mrg - 3, mrg, oPdf.MediaBox.Height - mrg - 3);
                        //oPdf.AddLine(mrg + 3, oPdf.MediaBox.Height - mrg, mrg + 3, oPdf.MediaBox.Height);
                        //oPdf.AddLine(oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height - mrg - 3, oPdf.MediaBox.Width, oPdf.MediaBox.Height - mrg - 3); //----
                        //oPdf.AddLine(oPdf.MediaBox.Width - mrg - 3, oPdf.MediaBox.Height - mrg, oPdf.MediaBox.Width - mrg - 3, oPdf.MediaBox.Height); //|
                        oPdf.AddLine(mrg, 0, mrg, mrg - offset);
                        oPdf.AddLine(0, mrg, mrg - offset, mrg);
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg, 0, oPdf.MediaBox.Width - mrg, mrg - offset);
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg + offset, mrg, oPdf.MediaBox.Width, mrg);
                        oPdf.AddLine(0, oPdf.MediaBox.Height - mrg, mrg - offset, oPdf.MediaBox.Height - mrg);
                        oPdf.AddLine(mrg, oPdf.MediaBox.Height - mrg + offset, mrg, oPdf.MediaBox.Height);
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg + offset, oPdf.MediaBox.Height - mrg, oPdf.MediaBox.Width, oPdf.MediaBox.Height - mrg); //----
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height - mrg + offset, oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height); //|
                        //Top middle line

                        //oPdf.Layer = 1;
                        //oPdf.AddLine(pgWidth / 2, 0, pgWidth / 2, mrg);
                        //oPdf.AddLine(pgWidth / 2 - mrg - 3, mrg / 2, pgWidth / 2 + mrg + 3, mrg / 2);
                        //oPdf.Rect.Position(pgWidth / 2 - 3, mrg / 2 + 3);
                        //oPdf.Rect.Resize(6, 6);
                        //oPdf.AddPie(0, 360, false);
                        ////Left middle lines
                        //oPdf.Layer = 1;
                        //oPdf.AddLine(0, pgHeight / 2, mrg, pgHeight / 2);
                        //oPdf.AddLine(mrg / 2, pgHeight / 2 - mrg - 3, mrg / 2, pgHeight / 2 + mrg + 3);
                        //oPdf.Rect.Position(mrg / 2 - 3, pgHeight / 2 + 3);
                        //oPdf.Rect.Resize(6, 6);
                        //oPdf.AddPie(0, 360, false);
                        ////right middle lines
                        //oPdf.Layer = 1;
                        //oPdf.AddLine(pgWidth - mrg, pgHeight / 2, pgWidth, pgHeight / 2);
                        //oPdf.AddLine(pgWidth - mrg / 2, pgHeight / 2 - mrg - 3, pgWidth - mrg / 2, pgHeight / 2 + mrg + 3);
                        //oPdf.Rect.Position(pgWidth - mrg / 2 - 3, pgHeight / 2 + 3);
                        //oPdf.Rect.Resize(6, 6);
                        //oPdf.AddPie(0, 360, false);
                        ////Bottom middle lines
                        //oPdf.Layer = 1;
                        //oPdf.AddLine(pgWidth / 2, pgHeight - mrg, pgWidth / 2, pgHeight);
                        //oPdf.AddLine(pgWidth / 2 - mrg - 3, pgHeight - mrg / 2, pgWidth / 2 + mrg + 3, pgHeight - mrg / 2);
                        //oPdf.Rect.Position(pgWidth / 2 - 3, pgHeight - mrg / 2 + 3);
                        //oPdf.Rect.Resize(6, 6);
                        //oPdf.AddPie(0, 360, false);
                        // adding date time stamp
                        oPdf.Layer = 1;
                        oPdf.TextStyle.Outline = 0;
                        oPdf.TextStyle.Strike = false;
                       // oPdf.TextStyle.Bold = true;
                        oPdf.TextStyle.Italic = false;
                        oPdf.TextStyle.CharSpacing = 0;
                        oPdf.TextStyle.Size = 6;
                        oPdf.Rect.Position(((pgWidth / 2) -20), pgHeight + 5);
                        oPdf.Rect.Resize(200, 10);
                        oPdf.AddHtml("" + pageName + " " + DateTime.Now.ToString());
                        oPdf.Transform.Reset();
                    }
                    // water mark 
                    if (drawWatermark)
                    {
                        if (waterMarkTxt != null && waterMarkTxt != "")
                        {
                            if (isWaterMarkText)
                            {
                               oPdf.Color.String = "16 12 13 0";
                               // oPdf.Color.String = "100 100 100 100";
                               // oPdf.ColorSpace = oPdf.AddColorSpaceSpot("Registration", "100 100 100 100");
                            //    oPdf.Color.Gray = 255;
                               // oPdf.Color.String = "211 211 211";
                                oPdf.Color.Alpha = 220;
                                //oPdf.Font = FontID;
                                oPdf.TextStyle.Size = 30;
                                //oPdf.TextStyle.CharSpacing = 2;
                                //oPdf.TextStyle.Bold = true;
                                //oPdf.TextStyle.Italic = false;
                                oPdf.Layer = 1;
                                oPdf.HPos = 0.5;
                                oPdf.VPos = 0.5;
                                oPdf.TextStyle.Outline = 2;
                                oPdf.Rect.Position(0, oPdf.MediaBox.Height);
                                oPdf.Rect.Resize(oPdf.MediaBox.Width, oPdf.MediaBox.Height);
                                // oPdf.FrameRect();
                                oPdf.Transform.Reset();
                                oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);
                                oPdf.AddHtml(waterMarkTxt);
                                oPdf.Transform.Reset();
                            }
                            else
                            {
                                string FilePath = string.Empty;
                                XImage oImg = new XImage();
                                System.Drawing.Image objImage = null;
                                try
                                {
                                    oPdf.PageNumber = PageNo;


                                    bool bFileExists = false;
                                    FilePath = waterMarkTxt;
                                    bFileExists = System.IO.File.Exists(FilePath);

                                    if (bFileExists)
                                    {
                                        objImage = System.Drawing.Image.FromFile(FilePath);
                                        oImg.SetFile(FilePath);
                                        double height = Utilities.Util.PixelToPoint(objImage.Height);
                                        double width = Utilities.Util.PixelToPoint(objImage.Width);
                                        if (height > pdfTemplateHeight)
                                        {
                                            height = pdfTemplateHeight;
                                        }
                                        if (width > pdfTemplateWidth)
                                        {
                                            width = pdfTemplateWidth;
                                        }

                                        double posX = (oPdf.MediaBox.Width - width) / 2;
                                        double posY = (oPdf.MediaBox.Height - height) / 2 + height;


                                        oPdf.Layer = 1;
                                        oPdf.Rect.Position(posX, posY);
                                        oPdf.Rect.Resize(width, height);


                                        oPdf.AddImageObject(oImg, true);
                                        oPdf.Transform.Reset();
                                    }


                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("LoadWaterMarkImage", ex);
                                }
                                finally
                                {
                                    oImg.Dispose();
                                    if (objImage != null)
                                        objImage.Dispose();
                                    oPdf.Transform.Reset();
                                }
                                // image
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DrawCuttingLine", ex);
            }
        }

        private void DrawBackgrounText(ref Doc oPdf)
        {
            int FontID = oPdf.AddFont("Arial");
            for (int i = 1; i <= oPdf.PageCount; i++)
            {
                oPdf.PageNumber = i;
                oPdf.Color.String = "211 211 211";
                //oPdf.Color.Alpha = 60;
                oPdf.Font = FontID;
                oPdf.TextStyle.Size = 40;
                //oPdf.TextStyle.CharSpacing = 2;
                //oPdf.TextStyle.Bold = true;
                //oPdf.TextStyle.Italic = false;
                oPdf.HPos = 0.5;
                oPdf.VPos = 0.5;
                oPdf.TextStyle.Outline = 2;
                oPdf.Rect.Position(0, oPdf.MediaBox.Height);
                oPdf.Rect.Resize(oPdf.MediaBox.Width, oPdf.MediaBox.Height);
                // oPdf.FrameRect();
                oPdf.Transform.Reset();
                oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);
                oPdf.AddHtml("MPC Systems");
            }
            oPdf.HPos = 0;
            oPdf.VPos = 0;
            oPdf.TextStyle.Outline = 0;
            oPdf.TextStyle.Strike = false;
            oPdf.TextStyle.Bold = false;
            oPdf.TextStyle.Italic = false;
            oPdf.TextStyle.CharSpacing = 0;
            oPdf.Transform.Reset();
            oPdf.Transform.Rotate(0, 0, 0);
            oPdf.Transform.Reset();
        }

        public bool generatePagePreviewMultiplage(byte[] PDFDoc, string savePath, double CuttingMargin, int DPI, bool RoundCorners)
        {


            //XSettings.License = "810-031-225-276-0715-601";
            using (Doc theDoc = new Doc())
            {

                try
                {
                    theDoc.Read(PDFDoc);
                    for (int i = 1; i <= theDoc.PageCount; i++)
                    {


                        theDoc.PageNumber = i;
                        theDoc.Rect.String = theDoc.CropBox.String;
                        theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                        if (System.IO.Directory.Exists(savePath) == false)
                        {
                            System.IO.Directory.CreateDirectory(savePath);
                        }

                        theDoc.Rendering.DotsPerInch = DPI;
                        string fileName = "p" + i + ".png";
                        if (RoundCorners)
                        {
                            Stream str = new MemoryStream();
                            
                            theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName), str);
                            generateRoundCorners(System.IO.Path.Combine(savePath, fileName), System.IO.Path.Combine(savePath, fileName) , str);

                        }
                        else
                        {
                            theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName));
                        }
                    }

                    theDoc.Dispose();

                    return true;



                }
                catch (Exception ex)
                {
                    throw new Exception("generatePagePreview", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                }
            }
        }
        public string generatePagePreview(byte[] PDFDoc, string savePath, string PreviewFileName, double CuttingMargin, int DPI, bool RoundCorners)
        {


            //XSettings.License = "810-031-225-276-0715-601";
            using (Doc theDoc = new Doc())
            {

                try
                {
                    theDoc.Read(PDFDoc);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;
                    theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                    if (System.IO.Directory.Exists(savePath) == false)
                    {
                        System.IO.Directory.CreateDirectory(savePath);
                    }

                    theDoc.Rendering.DotsPerInch = DPI;
                    if (RoundCorners)
                    {
                        Stream str = new MemoryStream();
                        theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png", str);
                        generateRoundCorners(System.IO.Path.Combine(savePath, PreviewFileName) + ".png", System.IO.Path.Combine(savePath, PreviewFileName) + ".png", str);

                    }
                    else
                    {
                        theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png");
                    }

                    theDoc.Dispose();

                    return PreviewFileName + ".png";



                }
                catch (Exception ex)
                {
                    throw new Exception("generatePagePreview", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                }
            }
        }

        public string generatePagePreview(string PDFDoc, string savePath, string PreviewFileName, double CuttingMargin, int DPI, bool RoundCorners)
        {


            //XSettings.License = "810-031-225-276-0715-601";
            using (Doc theDoc = new Doc())
            {

                try
                {
                    theDoc.Read(PDFDoc);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;
                    theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                    if (System.IO.Directory.Exists(savePath) == false)
                    {
                        System.IO.Directory.CreateDirectory(savePath);
                    }

                    theDoc.Rendering.DotsPerInch = DPI;
                    if (RoundCorners)
                    {
                        Stream str = new MemoryStream();
                        theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png", str);
                        generateRoundCorners(System.IO.Path.Combine(savePath, PreviewFileName) + ".png", System.IO.Path.Combine(savePath, PreviewFileName) + ".png", str);

                    }
                    else
                    {
                        theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png");
                    }

                    theDoc.Dispose();

                    return PreviewFileName + ".png";



                }
                catch (Exception ex)
                {
                    throw new Exception("generatePagePreview", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                }
            }
        }
        //[WebGet(UriTemplate = "{keywords},{ProductCategoryID},{PageNo},{PageSize},{callbind},{status},{UserID},{Role},{PageCount}")]
        //public List<Templates> GetTemplates(string keywords, string ProductCategoryID, string PageNo, string PageSize, string callbind, string status, string UserID, string Role, string PageCount)
        //{
        //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //    {cr

        //        try
        //        {
        //            db.ContextOptions.LazyLoadingEnabled = false;


        //            int vUserID = int.Parse( UserID);

        //            var predicate = PredicateBuilder.True<Templates>();


        //            predicate = predicate.Or(p => p.ProductName.Contains(keywords));
        //            if (ProductCategoryID != string.Empty)
        //            {
        //                predicate = predicate.And(p => p.ProductCategoryID == int.Parse(ProductCategoryID));
        //            }

        //            if (keywords != string.Empty)
        //            {
        //                predicate = predicate.And(p => p.ProductName.Contains(keywords));
        //            }

        //            if (Role.ToString().ToLower() == "admin")
        //            {

        //                switch (status)
        //                {
        //                    case "0": predicate = predicate.And(p => p.Status != 1); break;
        //                    case "1": predicate = predicate.And(p => p.Status != 1); break;
        //                    case "2": predicate = predicate.And(p => p.Status == 2); break;
        //                    case "3": predicate = predicate.And(p => p.Status == 3); break;
        //                    case "4": predicate = predicate.And(p => p.Status == 4); break;

        //                }
        //            }
        //            else
        //            {

        //                //if (vUserID != 0)
        //                //{
        //                //    predicate = predicate.And(p => p.SubmittedBy == vUserID);
        //                //}


        //                switch (status)
        //                {
        //                    case "1": predicate = predicate.And(p => p.Status == 1); break;
        //                    case "2": predicate = predicate.And(p => p.Status == 2); break;
        //                    case "3": predicate = predicate.And(p => p.Status == 3); break;
        //                    case "4": predicate = predicate.And(p => p.Status == 4); break;
        //                }
        //            }


        //            predicate = predicate.And(p => p.MatchingSetID == null);


        //            PageCount = Math.Ceiling(Convert.ToDouble(db.Templates.AsExpandable().Where(predicate).Count() / double.Parse(PageSize))).ToString();

        //            var temps = db.Templates.AsExpandable().Where(predicate).OrderBy(g => g.ProductName).Skip((int.Parse(PageNo)) * int.Parse(PageSize)).Take(int.Parse(PageSize));



        //            foreach (var item in temps)
        //            {
        //                if (item.Thumbnail == null || item.Thumbnail == string.Empty)
        //                    item.Thumbnail = "cardgeneral.jpg";
        //            }

        //            return temps.ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //            // throw new Exception(ex.ToString());
        //        }

        //    }
        //}

        //[WebInvoke(UriTemplate = "", Method = "POST")]
        //public Templates Create(Templates instance)
        //{
        //    // TODO: Add the new instance of SampleItem to the collection
        //    throw new NotImplementedException();
        //}

        //[WebGet(UriTemplate = "{id}")]
        //public Templates Get(string id)
        //{
        //    // TODO: Return the instance of SampleItem with the given id
        //    throw new NotImplementedException();
        //}

        //[WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        //public Templates Update(string id, Templates instance)
        //{
        //    // TODO: Update the given instance of SampleItem in the collection
        //    throw new NotImplementedException();
        //}

        //[WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        //public void Delete(string id)
        //{
        //    // TODO: Remove the instance of SampleItem with the given id from the collection
        //    throw new NotImplementedException();
        //}

        [OperationContract, WebInvoke(UriTemplate = "GetColor/{ProductId1},{CustomerID1}", Method = "GET", BodyStyle = WebMessageBodyStyle.Bare)]
        //[WebGet(UriTemplate = "{ProductId1}")]
        public Stream GetColorStyle(string ProductId1, string CustomerID1)
        {
            int ProductId = Convert.ToInt32(ProductId1);
            int CustomerID = Convert.ToInt32(CustomerID1);
            //List<TemplateColorStyles> lstColorStyle = new List<TemplateColorStyles>();
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.ContextOptions.LazyLoadingEnabled = false;
                try
                {
                    JsonSerializerSettings oset = new JsonSerializerSettings();


                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    if (CustomerID == 0)
                    {
                        return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(db.TemplateColorStyles.Where(g => (g.ProductID == ProductId || g.ProductID == null) && g.CustomerID == null).ToList(), Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
                    }
                    else
                    {
                        return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(db.TemplateColorStyles.Where(g => g.CustomerID == CustomerID).ToList(), Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
                    }
                }
                catch (Exception ex)
                {
                    //AppCommon.LogException(ex);
                    throw ex;
                }
            }
        }


        [OperationContract, WebInvoke(UriTemplate = "SaveCorpColor/{C},{M},{Y},{K},{Name},{CustomerID}", Method = "GET", BodyStyle = WebMessageBodyStyle.Bare)]
        public string SaveCorpColor(string C, string M, string Y, string K, string Name, string CustomerID)
        {
            string result = "";
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    TemplateColorStyles obj = new TemplateColorStyles();
                    obj.ColorC = Convert.ToInt32(C);
                    obj.ColorM = Convert.ToInt32(M);
                    obj.ColorY = Convert.ToInt32(Y);
                    obj.ColorK = Convert.ToInt32(K);
                    obj.IsSpotColor = true;
                    obj.SpotColor = Name;
                    obj.IsColorActive = true;
                    obj.CustomerID = Convert.ToInt32(CustomerID);

                    db.TemplateColorStyles.AddObject(obj);
                    db.SaveChanges();
                    result = obj.PelleteID.ToString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;

        }

        [OperationContract, WebInvoke(UriTemplate = "UpdateCorpColor/{id},{type}", Method = "GET", BodyStyle = WebMessageBodyStyle.Bare)]
        public string UpdateCorpColor(string id, string type)
        {
            string result = "";
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    int pId = Convert.ToInt32(id);
                    var obj = db.TemplateColorStyles.Where(g => g.PelleteID == pId).SingleOrDefault();
                    if (obj != null)
                    {
                        if (type == "DeActive")
                        {
                            obj.IsColorActive = false;
                        }
                        else
                        {
                            obj.IsColorActive = true;
                        }

                        db.SaveChanges();
                    }


                    result = "saved";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;

        }


        [OperationContract]
        [WebGet(UriTemplate = "GetCategoryV2/{CategoryIDStr}")]
        public Stream GetCategoryV2(string CategoryIDStr)
        {

            try
            {
                int CategoryId = int.Parse(CategoryIDStr);
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.ContextOptions.LazyLoadingEnabled = false;
                    db.ContextOptions.ProxyCreationEnabled = false;
                    var result = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == CategoryId).Single();
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
                }
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(ex.ToString()));
            }
        }

        [OperationContract]
        [WebGet(UriTemplate = "TemplateV2/{TemplateID},{CategoryIDStr},{heightStr},{widthStr}")]
        public Stream GetProductV2(string TemplateID,string CategoryIDStr,string heightStr, string widthStr)
        {

            try
            {
                int ProductId = int.Parse(TemplateID);
                int categoryID = int.Parse(CategoryIDStr);
                double height = Convert.ToDouble(heightStr);
                double width = Convert.ToDouble(widthStr);
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.ContextOptions.LazyLoadingEnabled = false;
                    db.ContextOptions.ProxyCreationEnabled = false;

                    Templates result = null;
                    if (ProductId == 0)
                    {
                        Templates oTemplate = new Templates();
                        oTemplate.Status = 1;
                        oTemplate.ProductName = "Untitled design";
                        oTemplate.ProductID = 0;
                        oTemplate.ProductCategoryID = categoryID;
                        oTemplate.CuttingMargin = Utilities.Util.PointToPixel(Utilities.Util.MMToPoint(5));
                        oTemplate.PDFTemplateHeight = Utilities.Util.PointToPixel(Util.MMToPoint(height));
                        oTemplate.PDFTemplateWidth = Utilities.Util.PointToPixel(Util.MMToPoint(width));

                        TemplatePages tpage = new TemplatePages();
                        tpage.Orientation = 1;
                        tpage.PageType = 1;
                        tpage.PageNo = 1;
                        tpage.ProductID = 0;
                        tpage.BackGroundType = 1;
                        tpage.PageName = "Front";

                        oTemplate.TemplatePages.Add(tpage);
                        result = oTemplate;

                    }
                    else
                    {
                        CMYKtoRGBConverter oColorConv = new CMYKtoRGBConverter();
                        int productPageId = -1;
                        int DisplayOrderCounter = 0;
                        result = db.Templates.Include("TemplatePages").Where(g => g.ProductID == ProductId).Single();
                        result.PDFTemplateHeight = Utilities.Util.PointToPixel(result.PDFTemplateHeight.Value);
                        result.PDFTemplateWidth = Utilities.Util.PointToPixel(result.PDFTemplateWidth.Value);
                        result.CuttingMargin = Utilities.Util.PointToPixel(result.CuttingMargin.Value);


                    }
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
                    //return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result)));


                }
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(ex.ToString()));
            }
        }

        [OperationContract]
        [WebGet(UriTemplate = "GetCatList/{CategoryIDStr},{pageNoStr},{pageSizeStr}")]
        public Stream GetCatListV2(string CategoryIDStr, string pageNoStr, string pageSizeStr)
        {
            int CategoryID = Convert.ToInt32(CategoryIDStr);
            int PageNo = Convert.ToInt32(pageNoStr);
            int PageSize = Convert.ToInt32(pageSizeStr);
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.ContextOptions.LazyLoadingEnabled = false;
//                    var query = from b in db.Templates.AsExpandable().Where(c => c.ProductCategoryID == CategoryID && c.Status == 1 && c.SubmittedBy != 16).OrderBy(g => g.ProductName).Skip((PageNo) * PageSize).Take(PageSize) select b;
//                    List<Dictionary<int, string>> result = new List<Dictionary<int, string>>();
//                    foreach (var item in query)
//                    {
//                        Dictionary<int,string> obj = new Dictionary<int,string>();
//                        obj.Add(item.ProductID,item.ProductName);
//                        result.Add(obj);
////                        objD.Add(new Dictionary<int,string>(, item.ProductName));
//                    }
                    var result = (from temp in db.Templates
                                  join es in db.TemplatePages on temp.ProductID equals es.ProductID
                                  where (temp.ProductCategoryID == CategoryID && temp.Status == 1 && temp.SubmittedBy != 16 && es.PageNo == 1)
                                  orderby temp.ProductName, temp.MPCRating
                                  select new
                                  {
                                      ProductID = temp.ProductID,
                                      ProductName = temp.ProductName,
                                      Orientation = es.Orientation,
                                      PDFTemplateHeight  = temp.PDFTemplateHeight,
                                      PDFTemplateWidth = temp.PDFTemplateWidth

                                  }).Skip((PageNo) * PageSize).Take(PageSize);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(ex.ToString()));
            }
        }

    }
    public class objTextStyles
    {
        public string textColor { get; set; }
        public string fontName { get; set; }
        public string fontSize { get; set; }
        public string fontWeight { get; set; }
        public string fontStyle { get; set; }
        public string characterIndex { get; set; }
        public string textCMYK { get; set; }
    }


}
