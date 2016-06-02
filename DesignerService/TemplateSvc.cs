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
using DesignerService.Utilities;
using WebSupergoo.ABCpdf8;
using System.Text.RegularExpressions;
using System.Transactions;

namespace DesignerService
{
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
        public Stream GetProduct( string TemplateID)
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
                    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result)));


                }
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                return null;
            }
        }

        [OperationContract, WebInvoke(UriTemplate = "update/", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]
        public string Save(Stream data)
        {
            try
            {


                int TemplateID = 0;

                StreamReader reader = new StreamReader(data);
                string res = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                JsonSerializerSettings oSettings = new JsonSerializerSettings();
                oSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                oSettings.NullValueHandling = NullValueHandling.Ignore;
                oSettings.ObjectCreationHandling = ObjectCreationHandling.Auto;


                List<TemplateObjects> lstTemplatesObjects = JsonConvert.DeserializeObject<List<TemplateObjects>>(res,oSettings);

                if (lstTemplatesObjects.Count > 0)
                {

                    TemplateID = lstTemplatesObjects[0].ProductID;
                    using (TransactionScope scope = new TransactionScope())
                    {

                        using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                        {

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
                                    oObject.PositionX = Util.PixelToPoint(oObject.PositionX);
                                    oObject.PositionY = Util.PixelToPoint(oObject.PositionY);
                                    oObject.FontSize = Util.PixelToPoint(oObject.FontSize);
                                    oObject.MaxWidth = Util.PixelToPoint(oObject.MaxWidth);
                                    oObject.MaxHeight = Util.PixelToPoint(oObject.MaxHeight);



                                    oObject.ProductID = TemplateID;
                                    db.TemplateObjects.AddObject(oObject);
                                }
                            }
                            db.SaveChanges(false);
                            scope.Complete();
                            db.AcceptAllChanges(); 


                        }
                    }
                }


                return "true";
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                return ex.ToString();
            }
        }


        [OperationContract, WebInvoke(UriTemplate = "preview/", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]
        public string Preview(Stream data)
        {
            try
            {


                int TemplateID = 0;

                StreamReader reader = new StreamReader(data);
                string res = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();


                List<TemplateObjects> lstTemplatesObjects = JsonConvert.DeserializeObject<List<TemplateObjects>>(res);

                if (lstTemplatesObjects.Count > 0)
                {

                    TemplateID = lstTemplatesObjects[0].ProductID;
                    var objProduct = new Templates();
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
                                    oObject.PositionX = Util.PixelToPoint(oObject.PositionX);
                                    oObject.PositionY = Util.PixelToPoint(oObject.PositionY);
                                    oObject.FontSize = Util.PixelToPoint(oObject.FontSize);
                                    oObject.MaxWidth = Util.PixelToPoint(oObject.MaxWidth);
                                    oObject.MaxHeight = Util.PixelToPoint(oObject.MaxHeight);

                                    oObject.ProductID = TemplateID;
                                    db.TemplateObjects.AddObject(oObject);
                                }
                            }
                            db.SaveChanges(false);
                            db.AcceptAllChanges();
                        }
                        scope.Complete();
                    }

                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        string targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/../Designer/Products/");
                        foreach (TemplatePages objPage in db.TemplatePages.Where(g => g.ProductID == TemplateID))
                        {
                            byte[] PDFFile = generatePDF(objProduct, objPage, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/../Designer/PrivateFonts/FontFace/"), false, false);
                            generatePagePreview(PDFFile, targetFolder, TemplateID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value);
                        }

                    }

                        
                    return "true";
                }
                else
                    return "false";
            }
            catch (Exception ex)
            {

                Util.LogException(ex);
                return ex.ToString();
            }
        }




        ///////////////////////////////////////// Business functions /////////////////////////////////////////////

        public byte[] generatePDF(Templates objProduct, TemplatePages objProductPage, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects)
        {

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                Doc doc = new Doc();
                try
                {
                    bool ObjSide2 = false;

                    //var objProduct = db.Templates.Where(g => g.ProductID == ProductID).Single();
                    //var objProductPage = db.TemplatePages.Where(g => g.ProductPageID == ProductPageID).Single();
                    var FontsList = db.TemplateFonts.ToList();

                 
                    XSettings.License = "810-031-225-276-0715-601";

                    doc.TopDown = true;

                    try
                    {


                        if (objProductPage.BackGroundType == 1)  //PDF background
                        {
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
                            LoadBackColor(ref doc, objProductPage);

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

                                LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath);
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Util.LogException(ex);
                        throw ex;
                    }
                   

                    double YFactor = 0;
                    double XFactor = 0;
                    int RowCount = 0;




                    List<TemplateObjects> oParentObjects = null;

                    if (IsDrawHiddenObjects)
                    {
                        oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && g.ParentId == 0).OrderBy(g => g.DisplayOrderPdf).ToList();

                    }
                    else
                    {
                        oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.ProductPageId == objProductPage.ProductPageID && g.ParentId == 0 && g.IsHidden == IsDrawHiddenObjects).OrderBy(g => g.DisplayOrderPdf).ToList();
                    }

                    foreach (var objObjects in oParentObjects)
                    {
                        if (XFactor != objObjects.PositionX)
                        {
                            if (objObjects.ContentString == "")
                                YFactor = objObjects.PositionY - 7;
                            else
                                YFactor = 0;
                            XFactor = objObjects.PositionX;
                        }



                        if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
                        {
                            

                            int VAlign = 1, HAlign = 1;
                            
                            HAlign = objObjects.Allignment;
                            
                            VAlign = objObjects.VAllignment;
                            
                            double currentX = objObjects.PositionX, currentY = objObjects.PositionY;
                               

                            if (VAlign == 1 || VAlign == 2)
                                currentY = objObjects.PositionY + objObjects.MaxHeight;

                            AddTextObject(objObjects,objProductPage.PageNo.Value,FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth, objObjects.MaxHeight);
                            
                            
                            
                        }
                        else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8) //3 = image and 8 = Logo
                        {
                            LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
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
                            DrawSVGVectorPath(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                        }
                        
                    }

                    //crop marks or margins
                    if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0)
                    {
                        //doc.CropBox.Height = doc.MediaBox.Height;
                        //doc.CropBox.Width = doc.MediaBox.Width;


                        doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left + Util.MMToPoint(5)).ToString() + " " + (doc.MediaBox.Top + Util.MMToPoint(5)).ToString() + " " + (doc.MediaBox.Width - Util.MMToPoint(5)).ToString() + " " + (doc.MediaBox.Height - Util.MMToPoint(5)).ToString());

                        DrawCuttingLines(ref doc, objProduct.CuttingMargin.Value, 1);
                    }

                    if (IsDrawBGText == true)
                    {
                        DrawBackgrounText(ref doc);
                    }

                    doc.Rendering.DotsPerInch = 200;

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


        private void LoadBackColor(ref Doc oPdf, TemplatePages oTemplate)
        {

            try
            {
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
               
                oPdf.PageNumber = 0;
                oPdf.Layer = oPdf.LayerCount + 1;

                oPdf.Color.Alpha = 255;
                oPdf.Color.Red = oTemplate.BgR.Value;
                oPdf.Color.Green = oTemplate.BgG.Value;
                oPdf.Color.Blue = oTemplate.BgB.Value;

                oPdf.FillRect();
               

            }
            catch (Exception ex)
            {
                throw new Exception("LoadBackColor", ex);
            }

        }


        private void LoadBackGroundImage(ref Doc oPdf, TemplatePages oTemplate, string imgPath)
        {

            try
            {
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;

               
                oPdf.PageNumber = 1;
                oPdf.Layer = oPdf.LayerCount + 1;
                oPdf.AddImageFile(imgPath + oTemplate.BackgroundFileName, 1);
               

            }
            catch (Exception ex)
            {
                throw new Exception("LoadBackGroundArtWork", ex);
            }

        }


        private void AddTextObject(TemplateObjects ooBject, int PageNo,List<TemplateFonts> oFonts, ref Doc oPdf, string Font, double OPosX, double OPosY, double OWidth, double OHeight)
        {
           
                try
                {
                    oPdf.TextStyle.Outline = 0;
                    oPdf.TextStyle.Strike = false;
                    oPdf.TextStyle.Bold = false;
                    oPdf.TextStyle.Italic = false;
                    oPdf.TextStyle.CharSpacing = 0;
                    oPdf.PageNumber = PageNo;// (ooBject.IsColumnNull("PageNo")) ? ooBject.PageNo : 1;
                    double yRPos = 0;
                    yRPos = ooBject.PositionY;
                    if (oPdf.TopDown == true)
                        yRPos = oPdf.MediaBox.Height - ooBject.PositionY;
                    if (ooBject.ColorType == 3)
                    {
                        if (ooBject.IsSpotColor == true)
                        {
                            oPdf.ColorSpace = oPdf.AddColorSpaceSpot(ooBject.SpotColorName, ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
                        }
                        oPdf.Color.String = ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString();
                        //if (!ooBject.IsColumnNull("Tint"))
                        oPdf.Color.Alpha = Convert.ToInt32((100 - ooBject.Tint) * 2.5);
                    }
                    else if (ooBject.ColorType == 4) // For RGB Colors
                    {
                        oPdf.Color.String = ooBject.RColor.ToString() + " " + ooBject.GColor.ToString() + " " + ooBject.BColor.ToString();
                    }


                    int FontID = 0;

                    if (ooBject.IsFontNamePrivate == true)
                    {
                        //printdesignBLL.Products.ProductFonts pFont = new ProductFonts();
                        //pFont.Where.FontName.Value = ooBject.FontName;
                        //pFont.Query.Load();

                        var pFont = oFonts.Where(g => g.FontName == ooBject.FontName).SingleOrDefault();

                        if (pFont != null)
                        {
                            if (pFont.IsPrivateFont == true)
                            {
                                if (System.IO.File.Exists(Font + pFont.FontFile + ".ttf"))
                                    FontID = oPdf.EmbedFont(Font + pFont.FontFile + ".ttf");
                            }
                            else
                                FontID = oPdf.AddFont(pFont.FontName);
                        }
                    }
                    else
                        FontID = oPdf.AddFont(ooBject.FontName, "", true);

                    oPdf.Font = FontID;
                    oPdf.TextStyle.Size = ooBject.FontSize;
                    //if (ooBject.IsColumnNull("TrackingValue") == false && ooBject.TrackingValue != "")
                    //    oPdf.TextStyle.CharSpacing = Convert.ToDouble(ooBject.TrackingValue);
                    //else
                    //    oPdf.TextStyle.CharSpacing = 0;
                    //if (!ooBject.IsColumnNull("Indent"))
                    oPdf.TextStyle.Indent = ooBject.Indent;
                    //if (!ooBject.IsColumnNull("IsUnderlinedText"))
                    oPdf.TextStyle.Underline = ooBject.IsUnderlinedText;
                    //if (ooBject.IsColumnNull("IsBold") == false)
                    oPdf.TextStyle.Bold = ooBject.IsBold;
                    //else
                    //    oPdf.TextStyle.Bold = false;
                    //if (ooBject.IsColumnNull("IsItalic") == false)
                    oPdf.TextStyle.Italic = ooBject.IsItalic;
                    //else
                    //    oPdf.TextStyle.Italic = false;





                    //if (ooBject.LineSpacing > 0)
                    //    oPdf.TextStyle.LineSpacing = ooBject.LineSpacing * 1.7;
                    //else
                    //    oPdf.TextStyle.LineSpacing = 1;

                    oPdf.TextStyle.LineSpacing = (oPdf.GetInfoDouble(oPdf.Font, "Ascent") - oPdf.GetInfoDouble(oPdf.Font, "Descent") - 1000) / (10 * ooBject.LineSpacing * ooBject.FontSize);
                        //Util.PixelToPoint(ooBject.LineSpacing) * ooBject.FontSize;// oPdf.GetInfoDouble(oPdf.Font, "LineSpacing") * oPdf.TextStyle.Size / 1000;

                                
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

                    oPdf.Rect.Position(OPosX, OPosY);
                    oPdf.Rect.Resize(OWidth, OHeight);

                    if (ooBject.RotationAngle != 0)
                    {

                        oPdf.Transform.Reset();
                        //oPdf.Transform.Rotate(ooBject.RotationAngle, OPosX + (OWidth - OWidth / 2), OPosY - (OHeight + OHeight / 2));
                        oPdf.Transform.Rotate(ooBject.RotationAngle, OPosX + (OWidth / 2), oPdf.MediaBox.Height - OPosY);
                        //oPdf.Transform.Rotate(ooBject.RotationAngle, ooBject.PositionX + ooBject.MaxWidth / 2, oPdf.MediaBox.Height - ooBject.PositionY);
                    }

                    string sNewLineNormalized = Regex.Replace(ooBject.ContentString, @"\r(?!\n)|(?<!\r)\n", "<BR>");

                    oPdf.AddHtml("<p>" + sNewLineNormalized + "</p>");
                    oPdf.Transform.Reset();


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
                    objOffsetX = row.OffsetX;//(row["OffsetX"] != DBNull.Value) ? Convert.ToDouble(row["OffsetX"]) : 0;
                    MaxWd += objOffsetX + row.MaxWidth; //Convert.ToDouble(row["MaxWidth"]);

                    if (oIdx + 1 >= dtObject.Count)
                        break;
                    else
                    {
                        TemplateObjects row2 = dtObject[oIdx + 1];
                        if (row2.IsNewLine != null)
                        {
                            if (row2.IsNewLine)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return MaxWd;
        }

        private void LoadImage(ref Doc oPdf, TemplateObjects oObject, string logoPath, int PageNo)
        {


            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/../");
            XImage oImg = new XImage();
            try
            {
                oPdf.PageNumber = PageNo;


                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ObjectType == 8)
                {
                    logoPath = ""; //since path is already in filenm
                    FilePath = System.Web.Hosting.HostingEnvironment.MapPath(oObject.ContentString);
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

                    oImg.SetFile(FilePath);

                    var posY =oObject.PositionY +  oObject.MaxHeight;

                    oPdf.Rect.Position(oObject.PositionX, posY);
                    oPdf.Rect.Resize(oObject.MaxWidth, oObject.MaxHeight);

                    if (oObject.RotationAngle != null)
                    {
                        

                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(oObject.RotationAngle, oObject.PositionX + oObject.MaxWidth / 2, oPdf.MediaBox.Height - posY + oObject.MaxHeight / 2);


                        }


                    }
                    
                    //oPdf.FrameRect();

                    oPdf.AddImageObject(oImg, true);
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
                    oPdf.Color.Alpha = Convert.ToInt32((100 - oObject.Tint) * 2.5);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }


                oPdf.Width = oObject.MaxHeight;
                oPdf.Rect.Position(oObject.PositionX, oObject.PositionY);
                oPdf.Rect.Resize(oObject.MaxWidth, oObject.MaxHeight);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(oObject.RotationAngle, oObject.PositionX + oObject.MaxWidth / 2, oPdf.MediaBox.Height - oObject.PositionY);
                    }


                }

                // oPdf.AddImageObject(oImg,false);
                //oPdf.AddImage ((oImg);
                oPdf.AddLine(oObject.PositionX, oObject.PositionY + oObject.MaxHeight / 2, oObject.PositionX + oObject.MaxWidth, oObject.PositionY + oObject.MaxHeight / 2);
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
                    //if (!ooBject.IsColumnNull("Tint"))
                    oPdf.Color.Alpha = Convert.ToInt32((100 - oObject.Tint) * 2.5);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }


                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oObject.PositionX, oObject.PositionY + oObject.MaxHeight);
                oPdf.Rect.Resize(oObject.MaxWidth, oObject.MaxHeight);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(oObject.RotationAngle, oObject.PositionX + oObject.MaxWidth / 2, oPdf.MediaBox.Height - oObject.PositionY - oObject.MaxHeight / 2);
                    }


                }

                // oPdf.AddImageObject(oImg,false);
                //oPdf.AddImage ((oImg);
                oPdf.FillRect();
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
                    //if (!ooBject.IsColumnNull("Tint"))
                    oPdf.Color.Alpha = Convert.ToInt32((100 - oObject.Tint) * 2.5);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }




                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oObject.PositionX, oObject.PositionY + oObject.MaxHeight);
                oPdf.Rect.Resize(oObject.MaxWidth, oObject.MaxHeight);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(oObject.RotationAngle, oObject.PositionX + oObject.MaxWidth / 2, oPdf.MediaBox.Height - oObject.PositionY - oObject.MaxHeight / 2);
                    }


                }

                oPdf.FillRect(oObject.MaxWidth / 2, oObject.MaxHeight / 2);

                //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorEllipse", ex);
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
                    oRect.Resize(oObject.MaxWidth, oObject.MaxHeight*2);

                    oImportSVG.MediaBox.Height = oObject.MaxHeight * 3;
                    oImportSVG.MediaBox.Width = oObject.MaxWidth;


                    oPdf.Rect.Position(oObject.PositionX, oObject.PositionY);
                    oPdf.Rect.Resize(oObject.MaxWidth, oObject.MaxHeight);
                    oImportSVG.FillRect();
                    //oPdf = oImportSVG;
                    oPdf.AddImageDoc(oImportSVG, PageNo,oRect);
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

        private void DrawCuttingLines(ref Doc oPdf, double mrg, int PageNo)
        {
            try
            {
                oPdf.Color.String = "0 0 255";

                oPdf.Layer = oPdf.LayerCount - 1;
                oPdf.PageNumber = PageNo;
                oPdf.Width = 0.5;
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
                double pgWidth = oPdf.MediaBox.Width;
                double pgHeight = oPdf.MediaBox.Height;
                for (int i = 1; i <= oPdf.PageCount; i++)
                {
                    oPdf.PageNumber = i;
                    oPdf.AddLine(mrg + 3, 0, mrg + 3, mrg);
                    oPdf.AddLine(0, mrg + 3, mrg, mrg + 3);
                    oPdf.AddLine(oPdf.MediaBox.Width - mrg - 3, 0, oPdf.MediaBox.Width - mrg - 3, mrg);
                    oPdf.AddLine(oPdf.MediaBox.Width - mrg, mrg + 3, oPdf.MediaBox.Width, mrg + 3);
                    oPdf.AddLine(0, oPdf.MediaBox.Height - mrg - 3, mrg, oPdf.MediaBox.Height - mrg - 3);
                    oPdf.AddLine(mrg + 3, oPdf.MediaBox.Height - mrg, mrg + 3, oPdf.MediaBox.Height);
                    oPdf.AddLine(oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height - mrg - 3, oPdf.MediaBox.Width, oPdf.MediaBox.Height - mrg - 3); //----
                    oPdf.AddLine(oPdf.MediaBox.Width - mrg - 3, oPdf.MediaBox.Height - mrg, oPdf.MediaBox.Width - mrg - 3, oPdf.MediaBox.Height); //|
                    //Top middle lines
                    oPdf.AddLine(pgWidth / 2, 0, pgWidth / 2, mrg);
                    oPdf.AddLine(pgWidth / 2 - mrg - 3, mrg / 2, pgWidth / 2 + mrg + 3, mrg / 2);
                    oPdf.Rect.Position(pgWidth / 2 - 3, mrg / 2 + 3);
                    oPdf.Rect.Resize(6, 6);
                    oPdf.AddPie(0, 360, false);
                    //Left middle lines
                    oPdf.AddLine(0, pgHeight / 2, mrg, pgHeight / 2);
                    oPdf.AddLine(mrg / 2, pgHeight / 2 - mrg - 3, mrg / 2, pgHeight / 2 + mrg + 3);
                    oPdf.Rect.Position(mrg / 2 - 3, pgHeight / 2 + 3);
                    oPdf.Rect.Resize(6, 6);
                    oPdf.AddPie(0, 360, false);
                    //right middle lines
                    oPdf.AddLine(pgWidth - mrg, pgHeight / 2, pgWidth, pgHeight / 2);
                    oPdf.AddLine(pgWidth - mrg / 2, pgHeight / 2 - mrg - 3, pgWidth - mrg / 2, pgHeight / 2 + mrg + 3);
                    oPdf.Rect.Position(pgWidth - mrg / 2 - 3, pgHeight / 2 + 3);
                    oPdf.Rect.Resize(6, 6);
                    oPdf.AddPie(0, 360, false);
                    //Bottom middle lines
                    oPdf.AddLine(pgWidth / 2, pgHeight - mrg, pgWidth / 2, pgHeight);
                    oPdf.AddLine(pgWidth / 2 - mrg - 3, pgHeight - mrg / 2, pgWidth / 2 + mrg + 3, pgHeight - mrg / 2);
                    oPdf.Rect.Position(pgWidth / 2 - 3, pgHeight - mrg / 2 + 3);
                    oPdf.Rect.Resize(6, 6);
                    oPdf.AddPie(0, 360, false);
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

        public string generatePagePreview(byte[] PDFDoc, string savePath, string PreviewFileName, double CuttingMargin)
        {


            //XSettings.License = "810-031-225-276-0715-601";
            Doc theDoc = new Doc();

            try
            {
                theDoc.Read(PDFDoc);
                theDoc.PageNumber = 1;
                theDoc.Rect.String = theDoc.CropBox.String;
                //theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                if (System.IO.Directory.Exists(savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }

                theDoc.Rendering.DotsPerInch = 150;
                theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png");
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


        //[WebGet(UriTemplate = "{keywords},{ProductCategoryID},{PageNo},{PageSize},{callbind},{status},{UserID},{Role},{PageCount}")]
        //public List<Templates> GetTemplates(string keywords, string ProductCategoryID, string PageNo, string PageSize, string callbind, string status, string UserID, string Role, string PageCount)
        //{
        //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //    {

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

    }
}
