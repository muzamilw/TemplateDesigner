using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;

using WebSupergoo.ABCpdf7;
//using TemplateDesigner.Models;
using System.Linq.Expressions;
using System.Linq;
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
using TemplateDesignerModelTypesV2;
using System.Runtime.InteropServices;
using System.ServiceModel.Web;
using TemplateDesignerV2.Services.Utilities;
using System.Net;
using System.Web;
using System.Data.Objects;


namespace TemplateDesignerV2.Services
{


    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class TemplateSvcSP : ITemplateSvcSP
    {
        public List<MatchingSets> GetMatchingSets()
        {
            using (TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities())
            {
                return oContext.MatchingSets.ToList();
            }

        }


        public MatchingSets GetMatchingSetbyID(int MatchingSetID)
        {
            try
            {
                using (TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities())
                {
                    return oContext.MatchingSets.Where(g => g.MatchingSetID == MatchingSetID).Single();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public List<TemplateColorStyles> GetColorStyle(int ProductId)
        {
            //List<TemplateColorStyles> lstColorStyle = new List<TemplateColorStyles>();
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                try
                {
                    return db.TemplateColorStyles.Where(g => g.ProductID == ProductId || g.ProductID == null).ToList();


                }
                catch (Exception ex)
                {
                    Util.LogException(ex);
                    throw ex;
                }
            }
        }


        public string GetProductBackgroundImg(int ProductId, string BkImg, bool IsSide2, int PageNo)
        {
            string RetVal = "";// "AppData/Products/" + BkImg;
            if (PageNo < 1)
                PageNo = 1;

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                //printdesignBLL.Products.Products dbProduct = new printdesignBLL.Products.Products();
                //dbProduct.LoadByPrimaryKey(ProductId);
                var dbProduct = db.Templates.Where(g => g.ProductID == ProductId).SingleOrDefault();

                if (dbProduct != null)
                {
                    if (dbProduct.IsUsePDFFile != null && dbProduct.IsUsePDFFile == true)
                    {
                        if (IsSide2 && dbProduct.IsDoubleSide == true && dbProduct.Side2LowResPDFTemplates != null && System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/") + "\\" + dbProduct.Side2LowResPDFTemplates))
                        {
                            RetVal = "Designer/Products/BackgroundImages/tmp/" + GenerateProductBackground(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.Side2LowResPDFTemplates.ToString()), 1, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/BackgroundImages/tmp/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + BkImg), PageNo);
                        }
                        else if (!IsSide2 && dbProduct.LowResPDFTemplates != null && System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/") + "\\" + dbProduct.LowResPDFTemplates))
                        {
                            RetVal = "Designer/Products/BackgroundImages/tmp/" + GenerateProductBackground(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.LowResPDFTemplates.ToString()), 1, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/BackgroundImages/tmp/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + BkImg), PageNo);
                        }
                    }
                }
            }
            return RetVal;
        }

        /// <summary>
        /// Returns a template
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public Templates GetTemplate(int TemplateID)
        {




            TemplateDesignerV2Entities db = new TemplateDesignerV2Entities();


            Templates dbProduct = null;
           // Doc pdfDoc = new Doc();
            //Products objProduct = new Products();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = false;


                dbProduct = db.Templates.Where(g => g.ProductID == TemplateID).Single();


                if (dbProduct != null)
                {

                    if (dbProduct.Orientation == 2) //rotating the canvas in case of vert orientation
                    {
                        double tmp = dbProduct.PDFTemplateHeight.Value;
                        dbProduct.PDFTemplateHeight = dbProduct.PDFTemplateWidth;
                        dbProduct.PDFTemplateWidth = tmp;
                    }




                    //if (dbProduct.BackgroundArtwork != null)
                    //    dbProduct.BackgroundArtwork = "Designer/Products/" + dbProduct.BackgroundArtwork;
                    //else
                    //    dbProduct.BackgroundArtwork = "";

                    //if (dbProduct.Side2BackgroundArtwork != null)
                    //    dbProduct.Side2BackgroundArtwork = "Designer/Products/" + dbProduct.Side2BackgroundArtwork;
                    //else
                    //    dbProduct.Side2BackgroundArtwork = "";

                    //if (dbProduct.IsUsePDFFile == true && dbProduct.LowResPDFTemplates != null)
                    //{
                    //    XSettings.License = "393-927-439-276-6036-693";

                    //    string PdfFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/") + "\\" + dbProduct.LowResPDFTemplates;
                    //    if (System.IO.File.Exists(PdfFile))
                    //    {
                    //        pdfDoc.Read(PdfFile);
                    //        dbProduct.PDFTemplateHeight = pdfDoc.Rect.Height;
                    //        dbProduct.PDFTemplateWidth = pdfDoc.Rect.Width;
                    //        dbProduct.BackgroundArtwork = "Designer/Products/BackgroundImages/tmp/" + GenerateProductBackground(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.LowResPDFTemplates.ToString()), 1, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/BackgroundImages/tmp/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.BackgroundArtwork), 1);
                    //    }
                    //    if (dbProduct.IsDoubleSide == true)
                    //    {
                    //        if (System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/") + "\\" + dbProduct.Side2LowResPDFTemplates))
                    //            dbProduct.Side2BackgroundArtwork = "Designer/Products/BackgroundImages/tmp/" + GenerateProductBackground(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.Side2LowResPDFTemplates.ToString()), 1, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/BackgroundImages/tmp/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.Side2BackgroundArtwork), 1);
                    //    }
                    //}
                    //else
                    //{
                    //    if (dbProduct.PDFTemplateWidth == null)
                    //        dbProduct.PDFTemplateWidth = 0;

                    //    if (dbProduct.PDFTemplateHeight == null)
                    //        dbProduct.PDFTemplateHeight = 0;

                    //}
                }
            }

            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
                // throw new Exception(ex.ToString());
            }
            finally
            {
            //    pdfDoc.Dispose();
            }
            return dbProduct;
        }


        /// <summary>
        /// Returns a template for webstore with user count
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public Templates GetTemplateWebStore(int TemplateID)
        {



            Templates dbProduct = null;
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {



              //  Doc pdfDoc = new Doc();
                //Products objProduct = new Products();
                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;


                    dbProduct = db.Templates.Where(g => g.ProductID == TemplateID).Single();


                    if (dbProduct != null)
                    {

                        if (dbProduct.Orientation == 2) //rotating the canvas in case of vert orientation
                        {
                            double tmp = dbProduct.PDFTemplateHeight.Value;
                            dbProduct.PDFTemplateHeight = dbProduct.PDFTemplateWidth;
                            dbProduct.PDFTemplateWidth = tmp;
                        }


                        dbProduct.UsedCount += 1;
                        if (dbProduct.UsedCount < 20)
                        {
                            dbProduct.UserRating = 0;
                        }
                        else if (dbProduct.UsedCount < 40)
                        {
                            dbProduct.UserRating = 1;
                        }
                        else if (dbProduct.UsedCount < 60)
                        {
                            dbProduct.UserRating = 2;
                        }
                        else if (dbProduct.UsedCount < 80)
                        {
                            dbProduct.UserRating = 3;
                        }
                        else
                        {
                            dbProduct.UserRating = 4;
                        }
                        //if (dbProduct.BackgroundArtwork != null)
                        //    dbProduct.BackgroundArtwork = "Designer/Products/" + dbProduct.BackgroundArtwork;
                        //else
                        //    dbProduct.BackgroundArtwork = "";

                        //if (dbProduct.Side2BackgroundArtwork != null)
                        //    dbProduct.Side2BackgroundArtwork = "Designer/Products/" + dbProduct.Side2BackgroundArtwork;
                        //else
                        //    dbProduct.Side2BackgroundArtwork = "";

                        //if (dbProduct.IsUsePDFFile == true && dbProduct.LowResPDFTemplates != null)
                        //{
                        //    XSettings.License = "393-927-439-276-6036-693";

                        //    string PdfFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/") + "\\" + dbProduct.LowResPDFTemplates;
                        //    if (System.IO.File.Exists(PdfFile))
                        //    {
                        //        pdfDoc.Read(PdfFile);
                        //        dbProduct.PDFTemplateHeight = pdfDoc.Rect.Height;
                        //        dbProduct.PDFTemplateWidth = pdfDoc.Rect.Width;
                        //        dbProduct.BackgroundArtwork = "Designer/Products/BackgroundImages/tmp/" + GenerateProductBackground(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.LowResPDFTemplates.ToString()), 1, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/BackgroundImages/tmp/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.BackgroundArtwork), 1);
                        //    }
                        //    if (dbProduct.IsDoubleSide == true)
                        //    {
                        //        if (System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/") + "\\" + dbProduct.Side2LowResPDFTemplates))
                        //            dbProduct.Side2BackgroundArtwork = "Designer/Products/BackgroundImages/tmp/" + GenerateProductBackground(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.Side2LowResPDFTemplates.ToString()), 1, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/BackgroundImages/tmp/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.Side2BackgroundArtwork), 1);
                        //    }
                        //}
                        //else
                        //{
                        //    if (dbProduct.PDFTemplateWidth == null)
                        //        dbProduct.PDFTemplateWidth = 0;

                        //    if (dbProduct.PDFTemplateHeight == null)
                        //        dbProduct.PDFTemplateHeight = 0;

                        //}

                        db.SaveChanges();
                    }

                }

                catch (Exception ex)
                {
                    Util.LogException(ex);
                    throw ex;
                    // throw new Exception(ex.ToString());
                }
                finally
                {
                 //   pdfDoc.Dispose();
                }
            }
            return dbProduct;
        }


        /// <summary>
        /// Returns Template Fonts
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public List<TemplateFonts> GetTemplateFonts(int TemplateID)
        {
            TemplateDesignerV2Entities db = new TemplateDesignerV2Entities();
            List<TemplateFonts> dbProduct = null;
            try
            {
                db.ContextOptions.LazyLoadingEnabled = false;
                dbProduct = db.sp_GetUsedFonts(TemplateID).ToList();
            }

            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
            }
            return dbProduct;
        }


        /// <summary>
        /// Returns Template Pages
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public List<TemplatePages> GetTemplatePages(int TemplateID)
        {
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.ContextOptions.LazyLoadingEnabled = false;
                    return db.TemplatePages.Where(g => g.ProductID == TemplateID).OrderBy(g => g.PageNo).ToList();
                }

            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
                // throw new Exception(ex.ToString());
            }

        }

        /// <summary>
        /// Returns updated Template Pages 
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public List<TemplatePages> UpdateTemplatePages(int TemplateID, int pageId, string operation)
        {
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {

                    if (operation == "MovePageUp")
                    {
                        TemplatePages page = db.TemplatePages.Where(g => g.ProductPageID == pageId).Single();
                        if (page != null)
                        {
                            int UperPageNo = Convert.ToInt32(page.PageNo) - 1;

                            TemplatePages Pageup = db.TemplatePages.Where(g => g.ProductID == TemplateID && g.PageNo == UperPageNo).Single();
                            //db.TemplatePages.DeleteObject(Pageup);
                            //db.TemplatePages.DeleteObject(page);

                            Pageup.PageNo += 1;
                            page.PageNo -= 1;
                            //db.TemplatePages.AddObject(page);
                            //db.TemplatePages.AddObject(Pageup);
                            db.SaveChanges();

                        }
                    }
                    else if (operation == "MovePageDown")
                    {
                        TemplatePages page = db.TemplatePages.Where(g => g.ProductPageID == pageId).Single();
                        if (page != null)
                        {
                            int UperPageNo = Convert.ToInt32(page.PageNo) + 1;

                            TemplatePages Pageup = db.TemplatePages.Where(g => g.ProductID == TemplateID && g.PageNo == UperPageNo).Single();
                            //db.TemplatePages.DeleteObject(Pageup);
                            //db.TemplatePages.DeleteObject(page);

                            Pageup.PageNo -= 1;
                            page.PageNo += 1;
                            //db.TemplatePages.AddObject(page);
                            //db.TemplatePages.AddObject(Pageup);DeletePage
                            db.SaveChanges();
                        }
                    }
                    else if (operation == "DeletePage")
                    {
                        TemplatePages page = db.TemplatePages.Where(g => g.ProductPageID == pageId).Single();
                        if (page != null)
                        {
                            List<TemplateObjects> objs = db.TemplateObjects.Where(g => g.ProductPageId == page.ProductPageID).ToList();
                            foreach (var obj in objs)
                            {
                                db.TemplateObjects.DeleteObject(obj);
                            }
                            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/" + page.BackgroundFileName)))
                            {
                                File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~" + page.BackgroundFileName));
                            }
                            db.TemplatePages.DeleteObject(page);
                            //db.TemplatePages.DeleteObject(page);
                            //db.TemplatePages.AddObject(page);
                            //db.TemplatePages.AddObject(Pageup);DeletePage
                            db.SaveChanges();
                        }
                    }

                    db.ContextOptions.LazyLoadingEnabled = false;
                    return db.TemplatePages.Where(g => g.ProductID == TemplateID).OrderBy(g => g.PageNo).ToList();
                }

            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
                // throw new Exception(ex.ToString());
            }

        }

        /// <summary>
        /// Returns true after updating a Template Page 
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public bool UpdateTemplatePage(int pageId, string PageName, string Orientation)
        {
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    TemplatePages tpage = db.TemplatePages.Where(g => g.ProductPageID == pageId).Single();
                    tpage.PageName = PageName;
                    if (PageName == "Front")
                    {
                        tpage.PageType = 1;
                    }
                    else if (PageName == "Back")
                    {
                        tpage.PageType = 2;
                    }
                    tpage.Orientation = Convert.ToInt32(Orientation);
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
                // throw new Exception(ex.ToString());
            }

        }

        /// <summary>
        /// Returns true after updating page background image or file
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public bool UpdateTemplatePageBackground(int productID, int PageID, string path, string backgroundtype)
        {
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    TemplatePages tpage = db.TemplatePages.Where(g => g.ProductPageID == PageID).Single();
                    //tpage
                    tpage.BackGroundType = Convert.ToInt32(backgroundtype);
                    tpage.BackgroundFileName = productID.ToString() + "/" + path;
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
                // throw new Exception(ex.ToString());
            }

        }
        /// <summary>
        /// add new template page
        /// </summary>
        public void AddNewPage(int templateID)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                List<TemplatePages> templatePages = db.TemplatePages.Where(g => g.ProductID == templateID).ToList();
                int pageNO = templatePages.Count;
                string backgroundfile = templatePages + "/" + (pageNO + 1) + ".pdf";
                TemplatePages tpage = new TemplatePages();
                tpage.ProductID = templateID;
                tpage.PageNo = pageNO + 1;
                tpage.PageType = 1;
                tpage.PageName = "Front";
                tpage.BackGroundType = 2;

                tpage.ColorC = 0;
                tpage.ColorM = 0;
                tpage.ColorY = 0;
                tpage.ColorK = 0;

                tpage.BackgroundFileName = backgroundfile;
                db.TemplatePages.AddObject(tpage);
                db.SaveChanges();
                TemplatePages tpageUpdated = db.TemplatePages.Where(g => g.ProductID == templateID && g.BackgroundFileName == backgroundfile).Single();
                UpdateBackgroundPDF(tpageUpdated.ProductPageID);
            }


        }
        /// <summary>
        /// Returns Template Objects
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public List<TemplateObjects> GetTemplateObjects(int TemplateID)
        {
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.ContextOptions.LazyLoadingEnabled = false;
                    return db.TemplateObjects.Where(g => g.ProductID == TemplateID).ToList();
                }

            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
                // throw new Exception(ex.ToString());
            }

        }

        /// <summary>
        /// Return Template Images library ,called from webstore
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public List<TemplateBackgroundImages> GettemplateImages(int TemplateID)
        {

            try
            {
                if (TemplateID != 0)
                {

                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {

                        db.ContextOptions.LazyLoadingEnabled = false;
                        //printdesignBLL.Products.ProductBackgroundImages objBackground = new printdesignBLL.Products.ProductBackgroundImages();
                        //objBackground.LoadByProductId(ProductId);

                        var backgrounds = db.TemplateBackgroundImages.Where(g => g.ProductID == TemplateID && (g.ImageType == 2 || g.ImageType == 4)).ToList();


                        string imgPath = System.AppDomain.CurrentDomain.BaseDirectory + ("Designer\\Products\\");
                        string imgUrl = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                        Uri objUri = new Uri(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"));

                        foreach (var objBackground in backgrounds)
                        {
                            if (objBackground.ImageName != null && objBackground.ImageName != "")
                            {
                                if (System.IO.File.Exists(imgUrl + objBackground.ImageName))
                                {


                                    objBackground.BackgroundImageAbsolutePath = objUri.OriginalString + objBackground.ImageName;

                                    objBackground.BackgroundImageRelativePath = "Designer/Products/" + objBackground.ImageName;
                                }
                            }

                        }

                        return backgrounds.ToList(); ;
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;

            }

            return null;
        }


        /// <summary>
        /// Returns the list of fold lines for the product category and also outputs if fold lines are to be applied. if ApplyFoldLines is false then return list is null
        /// </summary>
        /// <param name="ProductCategoryID"></param>
        /// <returns></returns>
        public List<tbl_ProductCategoryFoldLines> GetFoldLinesByProductCategoryID(int ProductCategoryID, out bool ApplyFoldLines)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.ContextOptions.LazyLoadingEnabled = false;

                try
                {
                    ApplyFoldLines = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == ProductCategoryID).Single().ApplyFoldLines.Value;

                    if (ApplyFoldLines)
                    {
                        var FoldLines = db.tbl_ProductCategoryFoldLines.Where(g => g.ProductCategoryID == ProductCategoryID).OrderBy(g => g.FoldLineOffsetFromOrigin);
                        //foreach (var item in FoldLines)
                        //{
                        //    item.FoldLineOffsetFromOrigin = Util.MMToPoint(item.FoldLineOffsetFromOrigin.Value);
                        //}
                        return FoldLines.ToList();
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    Util.LogException(ex);
                    throw ex;
                }
            }
        }



        public string GenerateTemplateThumbnail(byte[] PDFDoc, string savePath, string ThumbnailFileName, double CuttingMargin)
        {

            Stream ThumbnailImage = Stream.Null;
            XSettings.License = "810-031-225-276-0715-601";
            Doc theDoc = new Doc();
            System.Drawing.Image origImage = null;
            Graphics oGraphic = null;
            System.Drawing.Image origThumbnail = null;
            try
            {
                int ThumbnailSizeWidth = 400;
                int ThumbnailSizeHeight = 400;


                theDoc.Read(PDFDoc);
                theDoc.PageNumber = 1;
                theDoc.Rect.String = theDoc.CropBox.String;
                theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                if (System.IO.Directory.Exists(savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }

                theDoc.Rendering.DotsPerInch = 300;
                theDoc.Rendering.Save(System.IO.Path.Combine(savePath, ThumbnailFileName) + ".jpg");
                theDoc.Dispose();


                origImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(savePath, ThumbnailFileName) + ".jpg");


                float WidthPer, HeightPer;


                int NewWidth, NewHeight;

                if (origImage.Width > origImage.Height)
                {
                    NewWidth = ThumbnailSizeWidth;
                    WidthPer = (float)ThumbnailSizeWidth / origImage.Width;
                    NewHeight = Convert.ToInt32(origImage.Height * WidthPer);
                }
                else
                {
                    NewHeight = ThumbnailSizeHeight;
                    HeightPer = (float)ThumbnailSizeHeight / origImage.Height;
                    NewWidth = Convert.ToInt32(origImage.Width * HeightPer);
                }

                origThumbnail = new Bitmap(NewWidth, NewHeight, origImage.PixelFormat);
                oGraphic = Graphics.FromImage(origThumbnail);
                oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle oRectangle = new Rectangle(0, 0, NewWidth, NewHeight);
                oGraphic.DrawImage(origImage, oRectangle);


                origThumbnail.Save(System.IO.Path.Combine(savePath, ThumbnailFileName) + "1.jpg", ImageFormat.Jpeg);

                if (origImage != null)
                    origImage.Dispose();

                System.IO.File.Delete(System.IO.Path.Combine(savePath, ThumbnailFileName) + ".jpg");

                return ThumbnailFileName + "1.jpg";

            }
            catch (Exception ex)
            {
                throw new Exception("GenerateTemplateThumbnail", ex);
            }
            finally
            {
                if (origThumbnail != null)
                    origThumbnail.Dispose();

                if (oGraphic != null)
                    oGraphic.Dispose();

                if (ThumbnailImage != null)
                    ThumbnailImage.Dispose();

                if (theDoc != null)
                    theDoc.Dispose();

                if (origImage != null)
                    origImage.Dispose();
            }
        }



        public string GenerateProductBackground(string PDFPath, int index, string savePath, string BKImg, int PageNo)
        {
            Doc theDoc = new Doc();
            try
            {
                XSettings.License = "810-031-225-276-0715-601";

                theDoc.Read(PDFPath);
                theDoc.PageNumber = PageNo;
                theDoc.Rect.String = theDoc.CropBox.String;

                if (BKImg != "")
                {
                    if (File.Exists(BKImg) == true)
                    {
                        theDoc.Layer = theDoc.LayerCount + 1;
                        theDoc.AddImageFile(BKImg, 1);
                    }
                }
                string PDFDirectory = savePath;//"C:/PrintFlow_productthumbnails/";//Request.QueryString["dir"].ToString();

                #region "Rendering"
                if (System.IO.Directory.Exists(savePath) == false)
                //if (System.IO.Directory.Exists("C:/PrintFlow_productthumbnails/") == false)
                {
                    System.IO.Directory.CreateDirectory(savePath);
                    //System.IO.Directory.CreateDirectory("C:/PrintFlow_productthumbnails/");
                }
                #endregion

                string NextIndex = getNextFileName(savePath);
                //theDoc.Rendering.Save(PDFDirectory + PDFFile + "_" + Convert.ToInt32(index+1).ToString() +".jpg");
                //theDoc.Rendering.DotsPerInch=120;
                theDoc.Rendering.DotsPerInch = 72;
                theDoc.Rendering.Save(PDFDirectory + NextIndex + ".jpg");
                return NextIndex + ".jpg";
                //imgPreview.ImageUrl=ConfigurationSettings.AppSettings["RootPath"]+"cart/common/pdfimages/"+PDFFile + "_" + Convert.ToInt32(index+1).ToString() +".jpg";
                //imgPreview.ImageUrl = ConfigurationSettings.AppSettings["RootPath"] + "cart/common/pdfimages/" + NextIndex + ".jpg";
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw;
            }
            finally
            {
                theDoc.Dispose();
            }
        }

        public string getNextFileName(string dirPath)
        {
            string[] strArr;
            string str = "", strNextName = "";
            int MaxFile = 0;
            try
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);
                FileInfo[] fisub = di.GetFiles("*.*");
                if (fisub != null)
                {
                    foreach (FileInfo fi in fisub)
                    {
                        str = fi.Name;
                        if (str == "Thumbs.db")
                        {
                            try
                            {
                                fi.Delete();
                            }
                            catch (Exception ex)
                            {
                                ex = null;
                            }

                            str = strNextName;
                            //							strArr=str.Split('.');
                            //							MaxFile = Convert.ToInt32(strArr[0].ToString());
                            //							str =MaxFile.ToString(); 
                        }
                        else
                        {
                            int nextFile;
                            strArr = str.Split('.');
                            if (IsNumeric(strArr[0]))
                            {
                                nextFile = Convert.ToInt32(strArr[0].ToString()) + 1;
                            }
                            else
                            {
                                nextFile = 1;
                            }
                            if (nextFile > MaxFile)
                            {
                                MaxFile = nextFile;
                                strNextName = MaxFile.ToString();
                                str = strNextName;
                            }
                            else
                            {
                                strNextName = MaxFile.ToString();
                                str = strNextName;
                                strArr = str.Split('.');
                                MaxFile = Convert.ToInt32(strArr[0].ToString());
                                str = MaxFile.ToString();
                            }
                        }
                    }

                    if (str.Equals("") && strNextName.Equals(""))
                        return "1";
                    else
                        return str;
                }
                else
                    return "1";
            }
            catch (Exception ex)
            {
                throw new Exception("getNextFileName", ex);
            }
            finally
            {

                dirPath = null;
                strNextName = null;
                str = null;
                strArr = null;
            }
        }
        public bool IsNumeric(object value)
        {
            try
            {
                double d = System.Double.Parse(value.ToString(),
                    System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }



        public List<tbl_ProductCategory> GetCategoriesByMatchingSetID(int MatchingSetID)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;

                    var objectsList = from p in db.tbl_ProductCategory
                                      join msp in db.MatchingSetCategories on p.ProductCategoryID equals msp.ProductCategoryID
                                      where msp.MatchingSetID == MatchingSetID
                                      orderby p.CategoryName
                                      select p;

                    return objectsList.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                    // throw new Exception(ex.ToString());
                }

            }
        }


        public List<Templates> GetTemplates(string keywords, int ProductCategoryID, int PageNo, int PageSize, bool callbind, int status, int UserID, string Role, out int PageCount, int TemplateOwnerID, string userType)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;


                    int vUserID = UserID;

                    var predicate = PredicateBuilder.True<Templates>();

                    if (keywords != string.Empty)
                        predicate = predicate.Or(p => p.ProductName.Contains(keywords));

                    if (ProductCategoryID != 0)
                    {
                        predicate = predicate.And(p => p.ProductCategoryID == ProductCategoryID);
                    }

                    if (keywords != string.Empty)
                    {
                        predicate = predicate.And(p => p.ProductName.Contains(keywords));
                    }

                    if (Role.ToString().ToLower() == "nonaadmin")
                    {

                        switch (status)
                        {
                            case 0: predicate = predicate.And(p => p.Status != 1); break;
                            case 1: predicate = predicate.And(p => p.Status != 1); break;
                            case 2: predicate = predicate.And(p => p.Status == 2); break;
                            case 3: predicate = predicate.And(p => p.Status == 3); break;
                            case 4: predicate = predicate.And(p => p.Status == 4); break;

                        }

                        if (userType == "MPCAdmin") //admin user
                        {
                            //predicate = predicate.And(p => p.TemplateOwner == null);
                            //predicate = predicate.And(p => p.IsPrivate == false);
                        }
                    }
                    else
                    {

                        //if (vUserID != 0)
                        //{
                        //    predicate = predicate.And(p => p.SubmittedBy == vUserID);
                        //}


                        switch (status)
                        {
                            case 1: predicate = predicate.And(p => p.Status == 1); break;
                            case 2: predicate = predicate.And(p => p.Status == 2); break;
                            case 3: predicate = predicate.And(p => p.Status == 3); break;
                            case 4: predicate = predicate.And(p => p.Status == 4); break;
                        }
                    }


                    predicate = predicate.And(p => p.MatchingSetID == null);



                    if (userType == "MPCDesigner") //designers
                    {
                        predicate = predicate.And(p => p.TemplateOwner == null);
                    }
                    else if (userType == "CustomerUser") //private customer
                    {
                        predicate = predicate.And(p => p.TemplateOwner == TemplateOwnerID);
                    }




                    PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(db.Templates.AsExpandable().Where(predicate).Count()) / PageSize));

                    var temps = db.Templates.AsExpandable().Where(predicate).OrderBy(g => g.ProductName).Skip((PageNo) * PageSize).Take(PageSize);



                    foreach (var item in temps)
                    {
                        if (item.Thumbnail == null || item.Thumbnail == string.Empty)
                            item.Thumbnail = "cardgeneral.jpg";
                    }
                    // temps = temps.OrderByDescending(g => g.MPCRating);
                    return temps.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                    // throw new Exception(ex.ToString());
                }

            }
        }


        public List<BaseColors> GetBaseColors()
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;

                    var objectsList = db.BaseColors.OrderBy(g => g.Color).ToList();
                    return objectsList;
                }
                catch (Exception ex)
                {
                    throw ex;
                    // throw new Exception(ex.ToString());
                }

            }
        }
        public List<String> GetMatchingSetTheme()
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;

                    var objectsList = db.Templates //.Where(G => G.MatchingSetTheme != null && G.MatchingSetTheme != "")
                            .OrderBy(a => a.ProductName)
                            .Select(g => g.ProductName)
                            .Distinct();
                    return objectsList.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                    // throw new Exception(ex.ToString());
                }

            }
        }
        public List<sp_GetTemplateThemeTags_Result> GetTemplateThemeTags(int? ProductID)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;

                    var objectsList = db.sp_GetTemplateThemeTags(ProductID).ToList();
                    return objectsList;
                }
                catch (Exception ex)
                {
                    throw ex;
                    // throw new Exception(ex.ToString());
                }

            }
        }
        public List<sp_GetTemplateIndustryTags_Result> GetTemplateIndustryTags(int? ProductID)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;

                    var objectsList = db.sp_GetTemplateIndustryTags(ProductID).ToList();
                    return objectsList;
                }
                catch (Exception ex)
                {
                    throw ex;
                    // throw new Exception(ex.ToString());
                }

            }
        }

        private TemplateObjects ReturnObject(string Name, string Content, double PositionX, double PositionY, int ProductID, int DisplayOrder, int CtrlID, double FontSize, bool IsBold, int ProductPageID, int QtextOrder)
        {
            TemplateObjects oTempObject = new TemplateObjects();
            oTempObject.ProductPageId = ProductPageID;
            oTempObject.ObjectType = 2;
            oTempObject.Name = Name;
            oTempObject.IsEditable = true;
            oTempObject.IsHidden = false;
            oTempObject.IsMandatory = false;
            oTempObject.PositionX = PositionX;
            oTempObject.PositionY = PositionY;
            oTempObject.MaxHeight = 13;
            oTempObject.MaxWidth = 280;
            oTempObject.MaxCharacters = 0;
            oTempObject.RotationAngle = 0;
            oTempObject.IsFontCustom = false;
            oTempObject.IsFontNamePrivate = true;
            oTempObject.FontName = "Trebuchet MS";
            oTempObject.FontSize = FontSize;
            oTempObject.FontStyleID = 0;
            oTempObject.IsBold = IsBold;
            oTempObject.IsItalic = false;
            oTempObject.Allignment = 1;
            oTempObject.VAllignment = 1;
            oTempObject.Indent = 0;
            oTempObject.IsUnderlinedText = false;
            oTempObject.ColorType = 3;
            oTempObject.ColorStyleID = 0;
            oTempObject.PalleteID = 0;
            oTempObject.ColorName = "";
            oTempObject.ColorC = 0;
            oTempObject.ColorM = 0;
            oTempObject.ColorY = 0;
            oTempObject.ColorK = 100;
            oTempObject.Tint = 0;
            oTempObject.IsSpotColor = false;
            oTempObject.SpotColorName = "";
            oTempObject.ContentString = Content;
            oTempObject.ContentCaseType = 0;
            oTempObject.ProductID = ProductID;
            oTempObject.DisplayOrderPdf = DisplayOrder;
            oTempObject.DisplayOrderTxtControl = 0;
            oTempObject.IsRequireNumericValue = false;
            oTempObject.RColor = 0;
            oTempObject.GColor = 0;
            oTempObject.BColor = 0;
            oTempObject.isSide2Object = false;
            oTempObject.LineSpacing = 0;

            oTempObject.ParentId = 0;
            //oTempObject.OffsetX = 0;
            //oTempObject.OffsetY = 0;
            oTempObject.Opacity = 1;
            oTempObject.IsNewLine = false;
            oTempObject.TCtlName = "CtlTxtContent_" + CtrlID.ToString();
            oTempObject.ExField1 = "";
            oTempObject.ExField2 = "";
            oTempObject.IsQuickText = true;
            oTempObject.QuickTextOrder = QtextOrder;
            oTempObject.IsTextEditable = false;
            oTempObject.IsPositionLocked = false;

            return oTempObject;

        }

        /// <summary>
        /// Saves a template, edit and add scenarios
        /// </summary>
        /// <param name="oTemplate"></param>
        /// <param name="lstIndustryTags"></param>
        /// <param name="lstThemeTags"></param>
        /// <param name="IsAdd"></param>
        /// <param name="NewTemplateID"></param>
        /// <param name="IsCatChanged"></param>
        /// <returns></returns>
        public int SaveTemplates(Templates oTemplate, List<TemplatePages> lstTemplatePages, List<TemplateIndustryTags> lstIndustryTags, List<TemplateThemeTags> lstThemeTags, bool IsAdd, out int NewTemplateID, bool IsCatChanged)
        {
            try
            {

                TemplatePages oFirstPage = null;
                if (IsAdd == true)
                {
                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        db.Templates.AddObject(oTemplate);


                        //getting the selected category dimensions  and see if they are to be applied.

                        var SelectedProductCategory = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();


                        if (SelectedProductCategory.ApplySizeRestrictions.Value)
                        {
                            oTemplate.PDFTemplateHeight = Util.MMToPoint(SelectedProductCategory.HeightRestriction.Value);
                            oTemplate.PDFTemplateWidth = Util.MMToPoint(SelectedProductCategory.WidthRestriction.Value);
                        }
                        else
                        {
                            ////added by saqib
                            if (SelectedProductCategory.HeightRestriction == null)
                            {
                                oTemplate.PDFTemplateHeight = 500;
                            }
                            else
                            {
                                oTemplate.PDFTemplateHeight = Util.MMToPoint(SelectedProductCategory.HeightRestriction.Value);
                            }
                            if (SelectedProductCategory.WidthRestriction == null)
                            {
                                oTemplate.PDFTemplateWidth = 500;
                            }
                            else
                            {
                                oTemplate.PDFTemplateWidth = Util.MMToPoint(SelectedProductCategory.WidthRestriction.Value);
                            }
                        }

                        //var maxq = (from tab1 in db.Templates
                        //            select tab1.Code).Max();

                        //int code = 0;
                        //if (maxq != null)
                        //    code = Convert.ToInt32(maxq) +1;

                        //oTemplate.Code = "00" + code.ToString();

                        db.SaveChanges();


                        foreach (var oPage in lstTemplatePages)
                        {
                            oPage.ProductID = oTemplate.ProductID;

                            oPage.BackgroundFileName = CreatePageBlankBackgroundPDFs(oTemplate.ProductID, oPage, Util.MMToPoint(SelectedProductCategory.HeightRestriction.Value), Util.MMToPoint(SelectedProductCategory.WidthRestriction.Value));
                            db.TemplatePages.AddObject(oPage);
                        }

                        db.SaveChanges();

                        int ProductPageID = db.TemplatePages.Where(g => g.ProductID == oTemplate.ProductID && g.PageNo == 1).Single().ProductPageID;

                        db.TemplateObjects.AddObject(ReturnObject("CompanyName", "Your Company Name", 50, 40, oTemplate.ProductID, 401, 1, 12, true, ProductPageID, 1));
                        db.TemplateObjects.AddObject(ReturnObject("CompanyMessage", "Your Company Message", 50, 50, oTemplate.ProductID, 402, 2, 10, false, ProductPageID, 2));
                        db.TemplateObjects.AddObject(ReturnObject("Name", "Your Name", 50, 60, oTemplate.ProductID, 403, 3, 12, true, ProductPageID, 3));
                        db.TemplateObjects.AddObject(ReturnObject("Title", "Your Title", 50, 70, oTemplate.ProductID, 404, 4, 10, false, ProductPageID, 4));
                        db.TemplateObjects.AddObject(ReturnObject("AddressLine1", "Address Line 1", 50, 80, oTemplate.ProductID, 405, 5, 10, false, ProductPageID, 5));
                        //db.TemplateObjects.AddObject(ReturnObject("AddressLine2", "Address Line 2", 50, 90, oTemplate.ProductID, 406, 6,10,false));
                        //db.TemplateObjects.AddObject(ReturnObject("AddressLine3", "Address Line 3", 50, 100, oTemplate.ProductID, 407, 7,10,false));
                        db.TemplateObjects.AddObject(ReturnObject("Phone", "Telephone / Other", 50, 110, oTemplate.ProductID, 408, 8, 10, false, ProductPageID, 6));
                        db.TemplateObjects.AddObject(ReturnObject("Fax", "Fax / Other", 50, 120, oTemplate.ProductID, 409, 9, 10, false, ProductPageID, 7));
                        db.TemplateObjects.AddObject(ReturnObject("Email", "Email address / Other", 50, 130, oTemplate.ProductID, 410, 10, 10, false, ProductPageID, 8));
                        db.TemplateObjects.AddObject(ReturnObject("Website", "Website address", 50, 140, oTemplate.ProductID, 411, 11, 10, false, ProductPageID, 9));






                        for (int i = 0; i < lstIndustryTags.Count; i++)
                        {
                            lstIndustryTags[i].ProductID = oTemplate.ProductID;
                            db.TemplateIndustryTags.AddObject(lstIndustryTags[i]);
                        }

                        for (int i = 0; i < lstThemeTags.Count; i++)
                        {
                            lstThemeTags[i].ProductID = oTemplate.ProductID;
                            db.TemplateThemeTags.AddObject(lstThemeTags[i]);
                        }


                        System.IO.Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + oTemplate.ProductID.ToString() + "/"));

                        string basePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");

                        if (oTemplate.SLThumbnaillByte != null && oTemplate.SLThumbnaillByte.LongLength > 0)
                        {

                            oTemplate.SLThumbnail = oTemplate.ProductID.ToString() + "/" + oTemplate.SLThumbnail;
                            File.WriteAllBytes(Path.Combine(basePath, oTemplate.SLThumbnail), oTemplate.SLThumbnaillByte);
                        }


                        if (oTemplate.FullViewByte != null && oTemplate.FullViewByte.LongLength > 0)
                        {
                            oTemplate.FullView = oTemplate.ProductID.ToString() + "/" + oTemplate.FullView;
                            File.WriteAllBytes(Path.Combine(basePath, oTemplate.FullView), oTemplate.FullViewByte);
                        }

                        if (oTemplate.SuperViewByte != null && oTemplate.SuperViewByte.LongLength > 0)
                        {
                            oTemplate.SuperView = oTemplate.ProductID.ToString() + "/" + oTemplate.SuperView;
                            File.WriteAllBytes(Path.Combine(basePath, oTemplate.SuperView), oTemplate.SuperViewByte);
                        }

                        db.SaveChanges();

                        oFirstPage = lstTemplatePages.Where(g => g.PageNo == 1).Single();

                        NewTemplateID = oTemplate.ProductID;


                    }
                }
                else //edit mode
                {
                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        Templates tmpTemplate = db.Templates.Where(g => g.ProductID == oTemplate.ProductID).Single();

                        tmpTemplate.ProductName = oTemplate.ProductName;
                        tmpTemplate.ProductCategoryID = oTemplate.ProductCategoryID;
                        tmpTemplate.Description = oTemplate.Description;
                        tmpTemplate.MatchingSetTheme = oTemplate.MatchingSetTheme;
                        tmpTemplate.BaseColorID = oTemplate.BaseColorID;

                        if (oTemplate.Status == 2)
                        {
                            tmpTemplate.SubmitDate = oTemplate.SubmitDate;
                            tmpTemplate.SubmittedBy = oTemplate.SubmittedBy;
                            tmpTemplate.SubmittedByName = oTemplate.SubmittedByName;
                        }
                        tmpTemplate.ApprovalDate = oTemplate.ApprovalDate;
                        tmpTemplate.RejectionReason = oTemplate.RejectionReason;
                        tmpTemplate.ApprovedBy = oTemplate.ApprovedBy;
                        tmpTemplate.ApprovedByName = oTemplate.ApprovedByName;
                        tmpTemplate.Status = oTemplate.Status;
                        tmpTemplate.isEditorChoice = oTemplate.isEditorChoice;
                        tmpTemplate.MPCRating = oTemplate.MPCRating;
                        tmpTemplate.isEditorChoice = oTemplate.isEditorChoice;
                        tmpTemplate.MatchingSetID = oTemplate.MatchingSetID;

                        if (oTemplate.Orientation.HasValue == false)
                            tmpTemplate.Orientation = 1;
                        else
                            tmpTemplate.Orientation = oTemplate.Orientation;

                        tmpTemplate.Code = oTemplate.Code;

                        if (oTemplate.TemplateOwner != 0)
                        {
                            tmpTemplate.TemplateOwner = oTemplate.TemplateOwner;
                            tmpTemplate.TemplateOwnerName = oTemplate.TemplateOwnerName;

                            tmpTemplate.IsPrivate = oTemplate.IsPrivate;

                        }


                        string basePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");

                        if (oTemplate.SLThumbnaillByte != null && oTemplate.SLThumbnaillByte.LongLength > 0)
                        {
                            tmpTemplate.SLThumbnail = oTemplate.ProductID.ToString() + "/" + oTemplate.SLThumbnail;
                            File.WriteAllBytes(Path.Combine(basePath, tmpTemplate.SLThumbnail), oTemplate.SLThumbnaillByte);
                        }


                        if (oTemplate.FullViewByte != null && oTemplate.FullViewByte.LongLength > 0)
                        {
                            tmpTemplate.FullView = oTemplate.ProductID.ToString() + "/" + oTemplate.FullView;
                            File.WriteAllBytes(Path.Combine(basePath, tmpTemplate.FullView), oTemplate.FullViewByte);
                        }

                        if (oTemplate.SuperViewByte != null && oTemplate.SuperViewByte.LongLength > 0)
                        {
                            tmpTemplate.SuperView = oTemplate.ProductID.ToString() + "/" + oTemplate.SuperView;
                            File.WriteAllBytes(Path.Combine(basePath, tmpTemplate.SuperView), oTemplate.SuperViewByte);


                        }


                        var SelectedProductCategory = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();
                        tmpTemplate.PDFTemplateHeight = Util.MMToPoint(SelectedProductCategory.HeightRestriction.Value);
                        tmpTemplate.PDFTemplateWidth = Util.MMToPoint(SelectedProductCategory.WidthRestriction.Value);
                        oTemplate.PDFTemplateHeight = tmpTemplate.PDFTemplateHeight;
                        oTemplate.PDFTemplateWidth = tmpTemplate.PDFTemplateWidth;
                        db.SaveChanges();

                        var lstExistingPages = db.TemplatePages.Where(g => g.ProductID == oTemplate.ProductID).ToList();

                        //adding newly created pages by taking difference
                        PageComparer ocomp = new PageComparer();
                        //List<TemplatePages> oDiffPages = lstExistingPages.Except(lstTemplatePages, ocomp).ToList();


                        //var oDiffPages = lstExistingPages.Where(x => !lstTemplatePages.Any(x1 => x1.ProductPageID == x.ProductPageID));

                        //                        .Union(lstTemplatePages.Where(x => !lstExistingPages.Any(x1 => x1.ProductPageID == x.ProductPageID))); 
                        var oDiffPages = lstTemplatePages.Where(x => !lstExistingPages.Any(x1 => x1.ProductPageID == x.ProductPageID));
                        foreach (var item in oDiffPages)
                        {
                            db.TemplatePages.AddObject(item);
                        }


                        //logic to update and delete the existing pages.
                        foreach (var oExistingPage in lstExistingPages)
                        {
                            bool ismatched = false;
                            bool isdeleted = false;

                            foreach (var oPage in lstTemplatePages)
                            {

                                if (oExistingPage.ProductPageID == oPage.ProductPageID)  //matching
                                {
                                    if (oPage.BackGroundType == 1)
                                    {
                                        oPage.BackgroundFileName = CreatePageBlankBackgroundPDFs(oTemplate.ProductID, oPage, Util.MMToPoint(SelectedProductCategory.HeightRestriction.Value), Util.MMToPoint(SelectedProductCategory.WidthRestriction.Value));
                                        oExistingPage.BackgroundFileName = oPage.BackgroundFileName;
                                        oExistingPage.BackGroundType = oPage.BackGroundType;
                                    }
                                    oExistingPage.PageType = oPage.PageType;
                                    oExistingPage.PageNo = oPage.PageNo;
                                    oExistingPage.PageName = oPage.PageName;
                                    oExistingPage.Orientation = oPage.Orientation;
                                    ismatched = true;
                                    break;
                                }
                            }

                            if (ismatched == false)
                            {
                                isdeleted = true;


                                foreach (TemplateObjects c in db.TemplateObjects.Where(g => g.ProductPageId == oExistingPage.ProductPageID))
                                {
                                    db.DeleteObject(c);
                                }

                                db.TemplatePages.DeleteObject(oExistingPage);

                            }

                        }






                        db.TemplateIndustryTags.Where(w => w.ProductID == oTemplate.ProductID).ToList().ForEach(db.DeleteObject);
                        db.TemplateThemeTags.Where(w => w.ProductID == oTemplate.ProductID).ToList().ForEach(db.DeleteObject);

                        for (int i = 0; i < lstIndustryTags.Count; i++)
                        {
                            lstIndustryTags[i].ProductID = oTemplate.ProductID;
                            db.TemplateIndustryTags.AddObject(lstIndustryTags[i]);
                        }

                        for (int i = 0; i < lstThemeTags.Count; i++)
                        {
                            lstThemeTags[i].ProductID = oTemplate.ProductID;
                            db.TemplateThemeTags.AddObject(lstThemeTags[i]);
                        }



                        db.SaveChanges();

                        oFirstPage = lstExistingPages.Where(g => g.PageNo == 1).Single();

                        NewTemplateID = tmpTemplate.ProductID;

                        TemplateSvc oTemplateSvc = new TemplateSvc();
                        bool hasOverlayObject = false;
                        byte[] oPDF = oTemplateSvc.generatePDF(oTemplate, oFirstPage, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, false, false, out hasOverlayObject,false);
                        //if (hasOverlayObject)
                        //{
                        //    // generate overlay PDF 
                        //    byte[] overlayPDFFile = generatePDF(objProduct, objPage, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, objSettings.printCropMarks, objSettings.printWaterMarks, out hasOverlayObject, true);
                        //    System.IO.File.WriteAllBytes(targetFolder + TemplateID + "/p" + objPage.PageNo + "overlay.pdf", PDFFile);
                        //    generatePagePreview(PDFFile, targetFolder, TemplateID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, objSettings.isRoundCornerrs);
                        //}
                        tmpTemplate.Thumbnail = GenerateTemplateThumbnail(oPDF, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), NewTemplateID.ToString() + "/TemplateThumbnail", (tmpTemplate.CuttingMargin.Value));
                        db.SaveChanges();

                    }

                }


                return NewTemplateID;
            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Deletes the template along with all resources.
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public bool DeleteTemplate(int ProductID, out int CategoryID)
        {
            try
            {


                CategoryID = 0;
                bool result = false;
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    //deleting objects
                    foreach (TemplateObjects c in db.TemplateObjects.Where(g => g.ProductID == ProductID))
                    {
                        db.DeleteObject(c);
                    }

                    //theme tags
                    foreach (TemplateThemeTags c in db.TemplateThemeTags.Where(g => g.ProductID == ProductID))
                    {
                        db.DeleteObject(c);
                    }

                    //industry tags
                    foreach (TemplateIndustryTags c in db.TemplateIndustryTags.Where(g => g.ProductID == ProductID))
                    {
                        db.DeleteObject(c);
                    }

                    //background Images
                    foreach (TemplateBackgroundImages c in db.TemplateBackgroundImages.Where(g => g.ProductID == ProductID))
                    {

                        db.DeleteObject(c);
                    }
                    //delete template pages
                    foreach (TemplatePages c in db.TemplatePages.Where(g => g.ProductID == ProductID))
                    {

                        db.DeleteObject(c);
                    }
                    //deleting the template
                    foreach (Templates c in db.Templates.Where(g => g.ProductID == ProductID))
                    {
                        CategoryID = c.ProductCategoryID.Value;
                        db.DeleteObject(c);
                    }

                    if (System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/designer/products/" + ProductID.ToString())))
                    {

                        foreach (string item in System.IO.Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/designer/products/" + ProductID.ToString())))
                        {
                            System.IO.File.Delete(item);
                        }

                        Directory.Delete(System.Web.HttpContext.Current.Server.MapPath("~/designer/products/" + ProductID.ToString()));
                    }


                    db.SaveChanges();


                    result = true;
                }
                return result;

            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Deletes the template file only added by zohaib.
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        /// 
        public bool DeleteTemporaryFiles(int ProductID)
        {
            try
            {
             
                bool result = false;

                if (System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/designer/products/" + ProductID.ToString())))
                {

                    foreach (string item in System.IO.Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/designer/products/" + ProductID.ToString())))
                    {
                        System.IO.File.Delete(item);
                    }

                    Directory.Delete(System.Web.HttpContext.Current.Server.MapPath("~/designer/products/" + ProductID.ToString()));
                }

               
                result = true;
            
                return result;

            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
            }
        }
        /// <summary>
        /// Deletes the template fonts only added by zohaib.
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        /// 
        public void DeleteTemplateFonts(int Companyid)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                List<TemplateFonts> delFonts = db.TemplateFonts.Where(c => c.CustomerID == Companyid).ToList();
                if (delFonts != null && delFonts.Count > 0)
                {
                    foreach (TemplateFonts tf in delFonts)
                    {
                        List<string> paths = new List<string>();
                        string directory = System.Web.HttpContext.Current.Server.MapPath("~/designer/");
                        paths.Add(directory + tf.FontPath + tf.FontFile + ".eot");
                        paths.Add(directory + tf.FontPath + tf.FontFile + ".ttf");
                        paths.Add(directory + tf.FontPath + tf.FontFile + ".woff");

                        if (paths != null)
                        {
                            foreach (string ss in paths)
                            {
                                if (ss != "")
                                {
                                    if (File.Exists(ss))
                                        File.Delete(ss);

                                }
                            }
                        }
                    }
                }

                if (System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/designer/products/UserImgs/" + Companyid.ToString())))
                {

                    foreach (string item in System.IO.Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/designer/products/UserImgs/" + Companyid.ToString())))
                    {
                        System.IO.File.Delete(item);
                    }

                    Directory.Delete(System.Web.HttpContext.Current.Server.MapPath("~/designer/products/UserImgs/" + Companyid.ToString()));
                }
            }



        }
        public bool DeleteCategory(int productCatID)
        {
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    List<tbl_ProductCategoryFoldLines> foldLines = db.tbl_ProductCategoryFoldLines.Where(g => g.ProductCategoryID == productCatID).ToList();
                    foreach (var line in foldLines)
                    {
                        db.tbl_ProductCategoryFoldLines.DeleteObject(line);
                    }
                    tbl_ProductCategory cat = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == productCatID).SingleOrDefault();
                    db.tbl_ProductCategory.DeleteObject(cat);
                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        /// <summary>
        /// Copies the template along with all resources etc.
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="SubmittedBy"></param>
        /// <param name="SubmittedByName"></param>
        /// <returns></returns>
        public int CopyTemplate(int ProductID, int SubmittedBy, string SubmittedByName)
        {
            int result = 0;
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {

                    string BasePath = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/");
                    result = db.sp_cloneTemplate(ProductID, SubmittedBy, SubmittedByName).First().Value;


                    string targetFolder = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + result.ToString());
                    if (!System.IO.Directory.Exists(targetFolder))
                    {
                        System.IO.Directory.CreateDirectory(targetFolder);
                    }


                    //copy the background of pages
                    foreach (TemplatePages oTemplatePage in db.TemplatePages.Where(g => g.ProductID == result))
                    {

                        if (oTemplatePage.BackGroundType == 1 || oTemplatePage.BackGroundType == 3)
                        {
                            string path = Path.Combine(BasePath + result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/")));
                            if (!File.Exists(path))
                            {
                                //copy side 1
                                File.Copy(Path.Combine(BasePath, oTemplatePage.BackgroundFileName), BasePath + result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/")));
                            }
                            // copy side 1 image file if exist in case of pdf template
                            if (File.Exists(BasePath + ProductID.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg"))
                            {
                                File.Copy(BasePath + ProductID.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg", BasePath + result.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg", true);
                            }
                            oTemplatePage.BackgroundFileName = result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/"));

                        }

                    }
                    // copy thumbnails

                    Templates objTemplate = db.Templates.Where(g => g.ProductID == result).FirstOrDefault();

                    if (objTemplate != null)
                    {
                        if (objTemplate.SLThumbnail != null)
                        {
                            File.Copy(Path.Combine(BasePath, objTemplate.SLThumbnail), BasePath + result.ToString() + "/" + objTemplate.SLThumbnail.Substring(objTemplate.SLThumbnail.IndexOf("/"), objTemplate.SLThumbnail.Length - objTemplate.SLThumbnail.IndexOf("/")), true);
                        }
                        if (objTemplate.FullView != null)
                        {
                            File.Copy(Path.Combine(BasePath, objTemplate.FullView), BasePath + result.ToString() + "/" + objTemplate.FullView.Substring(objTemplate.FullView.IndexOf("/"), objTemplate.FullView.Length - objTemplate.FullView.IndexOf("/")), true);
                        }
                        if (objTemplate.SuperView != null)
                        {
                            File.Copy(Path.Combine(BasePath, objTemplate.SuperView), BasePath + result.ToString() + "/" + objTemplate.SuperView.Substring(objTemplate.SuperView.IndexOf("/"), objTemplate.SuperView.Length - objTemplate.SuperView.IndexOf("/")), true);
                        }
                    }

                    foreach (var item in db.TemplateObjects.Where(g => g.ProductID == result && g.ObjectType == 3))
                    {
                        string filepath = item.ContentString.Substring(item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length));
                        // skip placeholder images 
                        if (!item.ContentString.Contains("assets/Imageplaceholder"))
                        {
                            item.ContentString = "Designer/Products/" + result.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
                        }
                    }


                    foreach (var item in db.TemplateObjects.Where(g => g.ProductID == result))
                    {
                        if (item.IsPositionLocked == null)
                        {
                            item.IsPositionLocked = false;
                        }
                        if (item.IsHidden == null)
                        {
                            item.IsHidden = false;
                        }
                        if (item.IsEditable == null)
                        {
                            item.IsEditable = true;
                        }
                        if (item.IsTextEditable == null)
                        {
                            item.IsTextEditable = true;
                        }
                    }

                    //

                    /// todo
                    //copy the background images

                    var backimgs = db.TemplateBackgroundImages.Where(g => g.ProductID == result);

                    foreach (TemplateBackgroundImages item in backimgs)
                    {

                        string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + item.ImageName);
                        string filename;

                        string ext = Path.GetExtension(item.ImageName);

                        // generate thumbnail 
                        if (!ext.Contains("svg"))
                        {
                            string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                            string destPath = results[0] + "_thumb" + ext;
                            string ThumbPath = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + destPath);
                            FileInfo oFileThumb = new FileInfo(ThumbPath);
                            if (oFileThumb.Exists)
                            {
                                string oThumbName = oFileThumb.Name;
                                oFileThumb.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + result.ToString() + "/" + oThumbName), true);
                            }
                            //  objSvc.GenerateThumbNail(sourcePath, destPath, 98);
                        }



                        FileInfo oFile = new FileInfo(filePath);

                        if (oFile.Exists)
                        {
                            filename = oFile.Name;
                            item.ImageName = result.ToString() + "/" + oFile.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + result.ToString() + "/" + filename), true).Name;
                        }
                    }

                    db.SaveChanges();


                }

            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
            }

            return result;
        }


        /// <summary>
        /// Copies the template list along with all resources etc. // added by nauman
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="SubmittedBy"></param>
        /// <param name="SubmittedByName"></param>
        /// <returns></returns>
        public List<int?> CopyTemplateList(List<int?> productIDList, int SubmittedBy, string SubmittedByName)
        {
            int result = 0;
            List<int?> newTemplateList = new List<int?>();
            try
            {

                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    foreach (int? ProductID in productIDList)
                    {
                        if (ProductID != null && ProductID.HasValue)
                        {
                            string BasePath = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/");
                            int? test = db.sp_cloneTemplate(ProductID, SubmittedBy, SubmittedByName).First();

                            if (test.HasValue)
                            {
                                result = test.Value;
                                newTemplateList.Add(result);

                                string targetFolder = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + result.ToString());
                                if (!System.IO.Directory.Exists(targetFolder))
                                {
                                    System.IO.Directory.CreateDirectory(targetFolder);
                                }


                                //copy the background of pages
                                foreach (TemplatePages oTemplatePage in db.TemplatePages.Where(g => g.ProductID == result))
                                {

                                    if (oTemplatePage.BackGroundType == 1 || oTemplatePage.BackGroundType == 3)
                                    {
                                        string path = Path.Combine(BasePath + result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/")));
                                        if (File.Exists(Path.Combine(BasePath, oTemplatePage.BackgroundFileName)))
                                        {
                                            if (!File.Exists(path))
                                            {
                                                //copy side 1
                                                File.Copy(Path.Combine(BasePath, oTemplatePage.BackgroundFileName), BasePath + result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/")));
                                            }
                                        }
                                        
                                        // copy side 1 image file if exist in case of pdf template
                                        if (File.Exists(BasePath + ProductID.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg"))
                                        {
                                            File.Copy(BasePath + ProductID.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg", BasePath + result.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg", true);
                                        }
                                        oTemplatePage.BackgroundFileName = result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/"));

                                    }

                                }
                                // copy thumbnails

                                Templates objTemplate = db.Templates.Where(g => g.ProductID == result).FirstOrDefault();

                                if (objTemplate != null)
                                {
                                    if (objTemplate.SLThumbnail != null)
                                    {
                                        File.Copy(Path.Combine(BasePath, objTemplate.SLThumbnail), BasePath + result.ToString() + "/" + objTemplate.SLThumbnail.Substring(objTemplate.SLThumbnail.IndexOf("/"), objTemplate.SLThumbnail.Length - objTemplate.SLThumbnail.IndexOf("/")), true);
                                    }
                                    if (objTemplate.FullView != null)
                                    {
                                        File.Copy(Path.Combine(BasePath, objTemplate.FullView), BasePath + result.ToString() + "/" + objTemplate.FullView.Substring(objTemplate.FullView.IndexOf("/"), objTemplate.FullView.Length - objTemplate.FullView.IndexOf("/")), true);
                                    }
                                    if (objTemplate.SuperView != null)
                                    {
                                        File.Copy(Path.Combine(BasePath, objTemplate.SuperView), BasePath + result.ToString() + "/" + objTemplate.SuperView.Substring(objTemplate.SuperView.IndexOf("/"), objTemplate.SuperView.Length - objTemplate.SuperView.IndexOf("/")), true);
                                    }
                                }

                                foreach (var item in db.TemplateObjects.Where(g => g.ProductID == result && g.ObjectType == 3))
                                {
                                    string filepath = item.ContentString.Substring(item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length));
                                     // skip placeholder images 
                                    if (!item.ContentString.Contains("assets/Imageplaceholder"))
                                    {
                                        item.ContentString = "Designer/Products/" + result.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
                                    }
                                }


                                foreach (var item in db.TemplateObjects.Where(g => g.ProductID == result))
                                {
                                    if (item.IsPositionLocked == null)
                                    {
                                        item.IsPositionLocked = false;
                                    }
                                    if (item.IsHidden == null)
                                    {
                                        item.IsHidden = false;
                                    }
                                    if (item.IsEditable == null)
                                    {
                                        item.IsEditable = true;
                                    }
                                    if (item.IsTextEditable == null)
                                    {
                                        item.IsTextEditable = true;
                                    }
                                }

                                //

                                /// todo
                                //copy the background images

                                var backimgs = db.TemplateBackgroundImages.Where(g => g.ProductID == result);

                                foreach (TemplateBackgroundImages item in backimgs)
                                {

                                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + item.ImageName);
                                    string filename;

                                    string ext = Path.GetExtension(item.ImageName);

                                    // generate thumbnail 
                                    if (!ext.Contains("svg"))
                                    {
                                        string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                                        string destPath = results[0] + "_thumb" + ext;
                                        string ThumbPath = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + destPath);
                                        FileInfo oFileThumb = new FileInfo(ThumbPath);
                                        if (oFileThumb.Exists)
                                        {
                                            string oThumbName = oFileThumb.Name;
                                            oFileThumb.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + result.ToString() + "/" + oThumbName), true);
                                        }
                                        //  objSvc.GenerateThumbNail(sourcePath, destPath, 98);
                                    }

                                    FileInfo oFile = new FileInfo(filePath);

                                    if (oFile.Exists)
                                    {
                                        filename = oFile.Name;
                                        item.ImageName = result.ToString() + "/" + oFile.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + result.ToString() + "/" + filename), true).Name;
                                    }
                                }
                            }
                            else
                            {
                                newTemplateList.Add(null);
                            }
                        }
                        else
                        {
                            newTemplateList.Add(null);
                        }
                    }
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Util.LogException(ex);
                throw ex;
            }

            return newTemplateList;
        }



        public bool CreateBlankBackgroundPDFs(int TemplateID, double height, double width, int Orientation)
        {

            try
            {

                using (Doc theDoc = new Doc())
                {
                    string basePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + TemplateID.ToString() + "/");


                    Directory.CreateDirectory(basePath);

                    if (Orientation == 1)  //horizontal
                    {

                        theDoc.MediaBox.Height = height;
                        theDoc.MediaBox.Width = width;
                    }
                    else
                    {
                        theDoc.MediaBox.Height = width;
                        theDoc.MediaBox.Width = height;

                    }

                    theDoc.Save(basePath + "Side1.pdf");
                    theDoc.Clear();

                    File.Copy(basePath + "Side1.pdf", basePath + "Side2.pdf", true);

                    return true;
                }



            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

        public bool CreateBlankBackgroundPDFsByPages(int TemplateID, double height, double width, int Orientation, List<TemplatePages> PagesList)
        {

            try
            {


                string basePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + TemplateID.ToString() + "/");


                Directory.CreateDirectory(basePath);

                if (PagesList.Count > 0)
                {
                    for (int i = 0; i <= PagesList.Count - 1; i++)
                    {
                        Doc theDoc = new Doc();
                        if (PagesList[i].Orientation == 1)  //horizontal
                        {
                            theDoc.MediaBox.Height = height;
                            theDoc.MediaBox.Width = width;
                        }
                        else
                        {
                            theDoc.MediaBox.Height = width;
                            theDoc.MediaBox.Width = height;
                        }
                        //if (File.Exists(basePath + "/templatImgBk" + (i + 1).ToString() + ".jpg"))
                        //{
                        //    File.Delete(basePath + "/templatImgBk" + (i + 1).ToString() + ".jpg");
                        //}
                        theDoc.Save(basePath + "Side" + (i + 1).ToString() + ".pdf");
                        theDoc.Clear();
                        //File.Copy(basePath + "Side1.pdf", basePath + "Side" + PagesList[i].PageNo + ".pdf", true);
                    }
                }
                return true;
                //}
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }



        public string CreatePageBlankBackgroundPDFs(int TemplateID, TemplatePages oPage, double height, double width)
        {

            try
            {

                using (Doc theDoc = new Doc())
                {
                    string basePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + TemplateID.ToString() + "/");


                    Directory.CreateDirectory(basePath);

                    if (oPage.Orientation == 1)  //horizontal
                    {

                        theDoc.MediaBox.Height = height;
                        theDoc.MediaBox.Width = width;
                    }
                    else
                    {
                        theDoc.MediaBox.Height = width;
                        theDoc.MediaBox.Width = height;

                    }
                    //if (File.Exists(basePath + "/templatImgBk" + oPage.PageName.ToString() + ".jpg"))
                    //{
                    //    File.Delete(basePath + "/templatImgBk" + (i + 1).ToString() + ".jpg");
                    //}
                    theDoc.Save(basePath + oPage.PageName + oPage.PageNo.ToString() + ".pdf");
                    theDoc.Clear();



                    return TemplateID.ToString() + "/" + oPage.PageName + oPage.PageNo.ToString() + ".pdf";
                }



            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

        private bool UpdateBackgroundPDF(int productPageID)
        {

            try
            {

                using (Doc theDoc = new Doc())
                {
                    int Orientation;
                    double height;
                    double width;
                    int TemplateID;
                    int pageNo;
                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        TemplatePages tpages = db.TemplatePages.Where(g => g.ProductPageID == productPageID).Single();
                        Orientation = Convert.ToInt32(tpages.Orientation);
                        TemplateID = Convert.ToInt32(tpages.ProductID);
                        pageNo = Convert.ToInt32(tpages.PageNo);

                        var oTemplate = db.Templates.Where(g => g.ProductID == TemplateID).Single();

                        var SelectedProductCategory = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();
                        height = Convert.ToDouble(SelectedProductCategory.HeightRestriction);
                        width = Convert.ToDouble(SelectedProductCategory.WidthRestriction);

                    }
                    string basePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + TemplateID.ToString() + "/");


                    Directory.CreateDirectory(basePath);

                    if (Orientation == 1)  //horizontal
                    {

                        theDoc.MediaBox.Height = height;
                        theDoc.MediaBox.Width = width;
                    }
                    else
                    {
                        theDoc.MediaBox.Height = width;
                        theDoc.MediaBox.Width = height;

                    }

                    theDoc.Save(basePath + pageNo + ".pdf");
                    theDoc.Clear();

                    // File.Copy(basePath + "Side1.pdf", basePath + "Side2.pdf", true);

                    return true;
                }



            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

        public bool CropImage(string ImgName, int ImgX, int ImgY, int ImgWidth, int ImgHeight)
        {
            bool result = false;
            try
            {
                ImgName = System.Web.Hosting.HostingEnvironment.MapPath("~/" + ImgName);

                System.Drawing.Image img = System.Drawing.Image.FromFile(ImgName);
                Bitmap bm;
                //if (img.PixelFormat == PixelFormat.Format8bppIndexed)
                //{
                //    bm = CropImg2(img, ImgX, ImgY, ImgWidth, ImgHeight);
                //}
                //else
                bm = CropImg1(img, ImgX, ImgY, ImgWidth, ImgHeight);

                img.Dispose();
                MemoryStream mm = new MemoryStream();

                string fname = Path.GetFileNameWithoutExtension(ImgName);
                string ext = Path.GetExtension(ImgName).ToLower();

                //string ImgPath= SavePath + arr[0];
                if (ext == ".jpg")
                {
                    bm.Save(mm, ImageFormat.Jpeg);
                }
                else if (ext == ".png")
                {
                    bm.Save(mm, ImageFormat.Png);
                }
                else if (ext == ".gif")
                {
                    bm.Save(mm, ImageFormat.Gif);
                }


                File.WriteAllBytes(ImgName, mm.GetBuffer());

                img.Dispose();
                bm.Dispose();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;

        }
        public Bitmap CropImg1(System.Drawing.Image img, int xSize, int ySize, int wd, int ht)
        {
            if (img.Width < wd)
                wd = img.Width;
            if (img.Height < ht)
                ht = img.Height;
            ImageFormat fmt = img.RawFormat;
            Bitmap bImg = new Bitmap(wd, ht, img.PixelFormat);
            Graphics grImg = Graphics.FromImage(bImg);
            grImg.SmoothingMode = SmoothingMode.AntiAlias;
            grImg.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grImg.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grImg.DrawImage(img, new Rectangle(0, 0, wd, ht), xSize, ySize, wd, ht, GraphicsUnit.Pixel);
            img.Dispose();
            grImg.Dispose();
            return bImg;
        }
        public Bitmap CropImg2(System.Drawing.Image img, int xSize, int ySize, int wd, int ht)
        {
            if (img.Width < wd)
                wd = img.Width;
            if (img.Height < ht)
                ht = img.Height;

            Bitmap bm = new Bitmap(wd, ht, img.PixelFormat);

            BitmapData src = ((Bitmap)img).LockBits(new Rectangle(xSize, ySize, wd, ht), ImageLockMode.ReadOnly, img.PixelFormat);
            bm.Palette = img.Palette;
            BitmapData dst = bm.LockBits(new Rectangle(0, 0, wd, ht), ImageLockMode.WriteOnly, bm.PixelFormat);
            for (int y = 0; y < ht; ++y)
            {
                for (int x = 0; x < wd; ++x)
                {
                    Marshal.WriteByte(dst.Scan0, dst.Stride * y + x, Marshal.ReadByte(src.Scan0, src.Stride * y + x));
                }
            }
            ((Bitmap)img).UnlockBits(src);
            bm.UnlockBits(dst);
            img.Dispose();
            return bm;
        }

        public List<Tags> GetTags()
        {

            List<Tags> oTags = null;
            using (TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities())
            {
                oContext.ContextOptions.LazyLoadingEnabled = false;
                oTags = oContext.Tags.ToList();
            }

            return oTags;
        }



        //webstore specific functions //////////////////////////////////////////////////////////////////////////



        public List<Templates> GetTemplatesbyCategory(string GlobalCategoryName, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount)
        {
            int CustomerID = 0;
            if (GlobalCategoryName.Contains(":"))
            {
                string[] splitted = GlobalCategoryName.Split(':');
                GlobalCategoryName = splitted[0];
                CustomerID = Convert.ToInt32(splitted[1]);
            }

            System.Net.ServicePointManager.Expect100Continue = false;
            var predicate = PredicateBuilder.True<Templates>();
            try
            {
                TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities();
                oContext.ContextOptions.LazyLoadingEnabled = false;
                if (Keywords != string.Empty)
                {
                    predicate = predicate.And(p => p.ProductName.Contains(Keywords));
                    //    predicate = predicate.Or(p => p.TemplateIndustryTags.Where(p.TemplateIndustryTags.Where(g => g.Tags.TagName == Keywords)));
                }

                //main query + category
                var result = (from T in oContext.Templates
                              join PC in oContext.tbl_ProductCategory on T.ProductCategoryID equals PC.ProductCategoryID
                              join pp in oContext.TemplatePages on T.ProductID equals pp.ProductID
                              //join pd in oContext.TemplateIndustryTags on T.ProductID equals pd.ProductID into tags1 from pd in tags1.DefaultIfEmpty()

                              //join ptags in oContext.Tags on pd.TagID equals ptags.TagID into tags2 from ptags in tags2.DefaultIfEmpty()


                              where PC.CategoryName == GlobalCategoryName && pp.PageNo == 1
                              // && (ptags.TagName.Contains(Keywords))



                              select T);






                //industry
                if (IndustryID != 0)
                {
                    result = (from TT in result
                              join IT in oContext.TemplateIndustryTags
                                          on TT.ProductID equals IT.ProductID
                              where IT.TagID == IndustryID
                              select TT);
                }
                // tags 
                //  predicate = predicate.Or(p =>p.);  
                //style 
                if (ThemeStyleID != 0)
                {
                    result = (from TTT in result
                              join TT in oContext.TemplateThemeTags
                                          on TTT.ProductID equals TT.ProductID
                              where TT.TagID == ThemeStyleID
                              select TTT);
                }

                predicate = predicate.And(p => p.SubmittedBy != 16);

                //base colors
                if (BaseColors != null)
                {

                    foreach (var item in BaseColors)
                    {
                        predicate = predicate.And(p => p.BaseColorID == item);
                    }

                }


                if (CustomerID == 0) //only show templates which are not owned by 
                {

                    var inner = PredicateBuilder.False<Templates>();
                    inner = inner.Or(p => p.TemplateOwner == null);
                    inner = inner.Or(p => p.IsPrivate == false);


                    predicate = predicate.And(inner.Expand());


                }
                else
                {


                    var inner = PredicateBuilder.False<Templates>();
                    inner = inner.Or(p => p.TemplateOwner == null);
                    inner = inner.Or(p => p.TemplateOwner == CustomerID);


                    predicate = predicate.And(inner.Expand());


                }


                //predicate = predicate.And(p => p.Status == 3 || p.Status == 2);

                //result = (from PC in result
                //          where PC.CategoryName == GlobalCategoryName && T.Thumbnail != null

                result = (from T in result
                          select T).AsExpandable().Where(predicate).OrderBy(g => g.ProductName);


                SearchCount = result.Count();
                PageCount = Convert.ToInt32(Math.Ceiling(SearchCount / Convert.ToDecimal(PageSize)));


                int skip = PageNo * PageSize;

                //if (skip == PageSize)
                //    skip = 0;

                result = result.OrderByDescending(g => g.MPCRating);
                var nResult = result.Skip(skip).Take(PageSize).ToList();
                //nResult = nResult.OrderByDescending(g => g.MPCRating).ToList();
                return nResult;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }





        //
        public List<sp_SearchTemplate_Result> GetTemplatesbyCategoryAndMultipleIndustryIds(string GlobalCategoryName, int PageNo, int PageSize, string Keywords, string IndustryID, int ThemeStyleID, string BaseColors, out int PageCount, out int SearchCount)
        {
            int CustomerID = 0;

            if (BaseColors == "0")
                BaseColors = "";

            if (IndustryID == "0")
                IndustryID = "";

            if (GlobalCategoryName.Contains(":"))
            {
                string[] splitted = GlobalCategoryName.Split(':');
                GlobalCategoryName = splitted[0];
                CustomerID = Convert.ToInt32(splitted[1]);
            }
            try
            {
                var outSearchCOunt = new ObjectParameter("SearchCount", typeof(int));
                var outPageCOunt = new ObjectParameter("PageCount", typeof(int));
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    var result = db.sp_SearchTemplates(Convert.ToString(GlobalCategoryName), Convert.ToInt32(PageNo), Convert.ToInt32(CustomerID), Convert.ToInt32(PageSize), Convert.ToString(Keywords), IndustryID, Convert.ToInt32(ThemeStyleID), BaseColors, outPageCOunt, outSearchCOunt).OrderByDescending(i => i.MPCRating).ToList();//(isCalledFrom, imageSetType, templateID, contactCompanyID, contactID, territory, pageNumner, 10, "", SearchKeyword).ToList();
                    PageCount = Convert.ToInt16(outPageCOunt.Value);
                    SearchCount = Convert.ToInt16(outSearchCOunt.Value);
                    return result;
                }
                //System.Net.ServicePointManager.Expect100Continue = false;
                //var predicate = PredicateBuilder.True<Templates>();

                //    TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities();
                //    oContext.ContextOptions.LazyLoadingEnabled = false;
                //    if (Keywords != string.Empty)
                //    {
                //        predicate = predicate.And(p => p.ProductName.Contains(Keywords));
                //        //    predicate = predicate.Or(p => p.TemplateIndustryTags.Where(p.TemplateIndustryTags.Where(g => g.Tags.TagName == Keywords)));
                //    }


                //    var result = (from T in oContext.Templates.Include("TemplatePages")
                //                  join PC in oContext.tbl_ProductCategory on T.ProductCategoryID equals PC.ProductCategoryID
                //                  join pp in oContext.TemplatePages on T.ProductID equals pp.ProductID

                //                  where PC.CategoryName == GlobalCategoryName && pp.PageNo == 1 
                //                  select T);


                ////main query + category
                //var result = (from T in oContext.Templates
                //              join PC in oContext.tbl_ProductCategory on T.ProductCategoryID equals PC.ProductCategoryID
                //              join pp in oContext.TemplatePages on T.ProductID equals pp.ProductID

                //              where PC.CategoryName == GlobalCategoryName && pp.PageNo == 1
                //              select new Templates
                //              {
                //                  ProductID = T.ProductID,
                //                  Code = T.Code,
                //                  ProductName = T.ProductName,
                //                  Description = T.Description,
                //                  ProductCategoryID = T.ProductCategoryID,
                //                  LowResPDFTemplates = T.LowResPDFTemplates,
                //                  BackgroundArtwork = T.BackgroundArtwork,
                //                  Side2LowResPDFTemplates = T.Side2LowResPDFTemplates,
                //                  Side2BackgroundArtwork = T.Side2BackgroundArtwork,
                //                  Thumbnail = T.Thumbnail,
                //                  Image = T.Image,
                //                  IsDisabled = T.IsDisabled,
                //                  PTempId = T.PTempId,
                //                  IsDoubleSide = T.IsDoubleSide,
                //                  IsUsePDFFile = T.IsUsePDFFile,
                //                  PDFTemplateWidth = T.PDFTemplateWidth,
                //                  PDFTemplateHeight = T.PDFTemplateHeight,
                //                  IsUseBackGroundColor = T.IsUseBackGroundColor,

                //                  BgR = T.BgR,
                //                  BgG = T.BgG,
                //                  BgB = T.BgB,
                //                  IsUseSide2BackGroundColor = T.IsUseSide2BackGroundColor,
                //                  Side2BgR = T.Side2BgR,
                //                  Side2BgG = T.Side2BgG,
                //                  Side2BgB = T.Side2BgB,
                //                  CuttingMargin = T.CuttingMargin,
                //                  MultiPageCount = T.MultiPageCount,
                //                  Orientation = pp.Orientation,
                //                  MatchingSetTheme = T.MatchingSetTheme,
                //                  BaseColorID = T.BaseColorID,
                //                  SubmittedBy = T.SubmittedBy,
                //                  SubmittedByName = T.SubmittedByName,
                //                  SubmitDate = T.SubmitDate,
                //                  Status = T.Status,
                //                  ApprovedBy = T.ApprovedBy,
                //                  ApprovedByName = T.ApprovedByName,

                //                  UserRating = T.UserRating,
                //                  UsedCount = T.UsedCount,
                //                  MPCRating = T.MPCRating,
                //                  RejectionReason = T.RejectionReason,
                //                  ApprovalDate = T.ApprovalDate,
                //                  TempString = T.TempString,
                //                  MatchingSetID = T.MatchingSetID,
                //                  SLThumbnail = T.SLThumbnail,
                //                  FullView = T.FullView,
                //                  SuperView = T.SuperView,
                //                  ColorHex = T.ColorHex,
                //                  TemplateOwner = T.TemplateOwner,
                //                  TemplateOwnerName = T.TemplateOwnerName,
                //                  IsPrivate = T.IsPrivate,
                //                  ApprovedDate = T.ApprovedDate,
                //                  IsCorporateEditable = T.IsCorporateEditable,
                //                  TemplateType = T.TemplateType,
                //                  isWatermarkText = T.isWatermarkText,
                //                  isSpotTemplate = T.isSpotTemplate
                //              });
                //              //select T);




                //var temppageg = from t in result
                //                select t.TemplatePages;

                ////industry
                //if (IndustryID != "0" && !string.IsNullOrEmpty(IndustryID))
                //{
                //    char[] separator = new char[] { '|' };
                //    List<string> idList = IndustryID.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
                //    List<int> IndustryIds = new List<int>();
                //    foreach (var i in idList) 
                //    {
                //        IndustryIds.Add(Convert.ToInt32(i));
                //    }
                //    result = (from TT in result
                //              join IT in oContext.TemplateIndustryTags
                //                          on TT.ProductID equals IT.ProductID
                //              where  IndustryIds.Contains(IT.TagID ?? 0)
                //              select TT);
                //}

                // tags 
                //  predicate = predicate.Or(p =>p.);  
                //style 
                //if (ThemeStyleID != 0)
                //{
                //    result = (from TTT in result
                //              join TT in oContext.TemplateThemeTags
                //                          on TTT.ProductID equals TT.ProductID
                //              where TT.TagID == ThemeStyleID
                //              select TTT);
                //}

                // predicate = predicate.And(p => p.SubmittedBy != 16);

                //base colors
                //if (BaseColors != null)
                //{

                //    foreach (var item in BaseColors)
                //    {
                //        predicate = predicate.And(p => p.BaseColorID == item);
                //    }

                //}


                //if (CustomerID == 0) //only show templates which are not owned by 
                //{

                //    var inner = PredicateBuilder.False<Templates>();
                //    inner = inner.Or(p => p.TemplateOwner == null);
                //    inner = inner.Or(p => p.IsPrivate == false);


                //    predicate = predicate.And(inner.Expand());


                //}
                //else
                //{


                //    var inner = PredicateBuilder.False<Templates>();
                //    inner = inner.Or(p => p.TemplateOwner == null);
                //    inner = inner.Or(p => p.TemplateOwner == CustomerID);


                //    predicate = predicate.And(inner.Expand());


                //}


                //predicate = predicate.And(p => p.Status == 3 || p.Status == 2);

                //result = (from PC in result
                //          where PC.CategoryName == GlobalCategoryName && T.Thumbnail != null

                //result = (from T in result
                //          select T).AsExpandable().Where(predicate).OrderBy(g => g.ProductName);


                //SearchCount = result.Count();
                //PageCount = Convert.ToInt32(Math.Ceiling(SearchCount / Convert.ToDecimal(PageSize)));


                //int skip = PageNo * PageSize;

                ////if (skip == PageSize)
                ////    skip = 0;

                //result = result.OrderByDescending(g => g.MPCRating);
                //var nResult = result.Skip(skip).Take(PageSize).ToList();
                ////nResult = nResult.OrderByDescending(g => g.MPCRating).ToList();
                // return nResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        // Add By Sajid Ali on 05/23/2012
        public List<Templates> GetTemplatesbyProductIds(int[] ProductIds, int PageNo, int PageSize, out int PageCount, out int SearchCount)
        {

            System.Net.ServicePointManager.Expect100Continue = false;
            var predicate = PredicateBuilder.True<Templates>();
            try
            {
                TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities();

                oContext.ContextOptions.LazyLoadingEnabled = false;

                //main query + ProductIds
                var result = (from T in oContext.Templates
                              where ProductIds.Contains(T.ProductID)
                              select T);


                result = (from T in result
                          select T).AsExpandable().Where(predicate).OrderBy(g => g.ProductName);


                SearchCount = result.Count();
                PageCount = Convert.ToInt32(Math.Ceiling(SearchCount / Convert.ToDecimal(PageSize)));

                int skip = PageNo * PageSize;

                return result.Skip(skip).Take(PageSize).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Get Matching Sets for the web store

        public List<vw_WebStore_MatchingSets> GetTemplatesbyTemplateName(string TemplateName, string[] CategoryNames, int PageNo, int PageSize, out int PageCount, out int SearchCount)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            var predicate = PredicateBuilder.True<vw_WebStore_MatchingSets>();

            int CustomerID = 0;
            if (TemplateName.Contains(":"))
            {
                string[] splitted = TemplateName.Split(':');
                TemplateName = splitted[0];
                CustomerID = Convert.ToInt32(splitted[1]);
            }

            try
            {
                TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities();
                oContext.ContextOptions.LazyLoadingEnabled = false;

                predicate = predicate.And(p => p.ProductName == TemplateName && CategoryNames.Contains(p.CategoryName));


                //main query + ProductIds
                var result = (from T in oContext.vw_WebStore_MatchingSets

                              select T);

                predicate = predicate.And(p => p.SubmittedBy != 16);



                if (CustomerID == 0) //only show templates which are not owned by 
                {

                    var inner = PredicateBuilder.False<vw_WebStore_MatchingSets>();
                    inner = inner.Or(p => p.TemplateOwner == null);
                    inner = inner.Or(p => p.IsPrivate == false);


                    predicate = predicate.And(inner.Expand());


                }
                else
                {


                    var inner = PredicateBuilder.False<vw_WebStore_MatchingSets>();
                    inner = inner.Or(p => p.TemplateOwner == null);
                    inner = inner.Or(p => p.TemplateOwner == CustomerID);


                    predicate = predicate.And(inner.Expand());


                }


                result = (from T in result
                          select T).AsExpandable().Where(predicate).OrderBy(g => g.ProductName);


                SearchCount = result.Count();
                PageCount = Convert.ToInt32(Math.Ceiling(SearchCount / Convert.ToDecimal(PageSize)));


                int skip = PageNo * PageSize;


                return result.ToList(); //.Skip(skip).Take(PageSize).

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Get Editors Choice Templates for webstore for the web store

        public List<vw_WebStore_MatchingSets> GetEditorsChoiceTemplates(string[] CategoryNames, int CustomerID, int PageNo, int PageSize, out int PageCount, out int SearchCount)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            var predicate = PredicateBuilder.True<vw_WebStore_MatchingSets>();


            try
            {
                TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities();
                oContext.ContextOptions.LazyLoadingEnabled = false;

                predicate = predicate.And(p => p.isEditorChoice == true && CategoryNames.Contains(p.CategoryName));


                //main query + ProductIds
                var result = (from T in oContext.vw_WebStore_MatchingSets

                              select T);

                predicate = predicate.And(p => p.SubmittedBy != 16);



                if (CustomerID == 0) //only show templates which are not owned by 
                {

                    var inner = PredicateBuilder.False<vw_WebStore_MatchingSets>();
                    inner = inner.Or(p => p.TemplateOwner == null);
                    inner = inner.Or(p => p.IsPrivate == false);


                    predicate = predicate.And(inner.Expand());


                }
                else
                {


                    var inner = PredicateBuilder.False<vw_WebStore_MatchingSets>();
                    inner = inner.Or(p => p.TemplateOwner == null);
                    inner = inner.Or(p => p.TemplateOwner == CustomerID);


                    predicate = predicate.And(inner.Expand());


                }


                result = (from T in result
                          select T).AsExpandable().Where(predicate).OrderBy(g => g.ProductName);


                SearchCount = result.Count();
                PageCount = Convert.ToInt32(Math.Ceiling(SearchCount / Convert.ToDecimal(PageSize)));


                int skip = PageNo * PageSize;


                return result.ToList(); //.Skip(skip).Take(PageSize).

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveTemplateLocally(Templates oTemplate, List<TemplatePages> oTemplatePages, List<TemplateObjects> oTemplateObjects, List<TemplateBackgroundImages> oTemplateImages, List<TemplateFonts> oTemplateFonts, string RemoteUrlBasePath, string BasePath)
        {

            oTemplate.EntityKey = null;
            int newProductID = 0;
            int newPageID = 0;
            int oldPageID = 0;
            //string BasePath = System.Web.HttpContext.Current.Server.MapPath("../Designer/Products/");


            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    int i = 1;
                    db.Templates.AddObject(oTemplate);

                    db.SaveChanges();

                    newProductID = oTemplate.ProductID;

                    string targetFolder = BasePath + "products\\" + newProductID.ToString(); //System.Web.HttpContext.Current.Server.MapPath("../Designer/Products/" + newProductID.ToString());
                    if (!System.IO.Directory.Exists(targetFolder))
                    {
                        System.IO.Directory.CreateDirectory(targetFolder);
                    }

                    foreach (var oPage in oTemplatePages)
                    {
                        oldPageID = oPage.ProductPageID;
                        oPage.ProductID = newProductID;
                        db.TemplatePages.AddObject(oPage);
                        db.SaveChanges();
                        newPageID = oPage.ProductPageID;


                        //      DownloadFile(RemoteUrlBasePath + "products/" + oPage.ProductID + "/p" + i + ".png", BasePath + "products\\" + newProductID.ToString() + "/");

                        foreach (var item in oTemplateObjects.Where(g => g.ProductPageId == oldPageID))
                        {

                            item.ProductPageId = newPageID;
                            item.ProductID = newProductID;

                            //updating the path if it is an image.
                            if (item.ObjectType == 3)
                            {
                                string filepath = item.ContentString.Substring(item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length));


                                //skip concatinating the path if its a placeholder, cuz place holder is kept in a different path and doesnt need to be copied.
                                if (!item.ContentString.Contains("assets/Imageplaceholder"))
                                {
                                    item.ContentString = "Designer/Products/" + newProductID.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
                                }
                            }
                            db.TemplateObjects.AddObject(item);
                        }

                        //page
                        if (oPage.BackGroundType == 1 || oPage.BackGroundType == 3)
                        {
                            DownloadFile(RemoteUrlBasePath + "products/" + oPage.BackgroundFileName, BasePath + "products\\" + newProductID.ToString() + "/" + oPage.BackgroundFileName.Substring(oPage.BackgroundFileName.IndexOf("/"), oPage.BackgroundFileName.Length - oPage.BackgroundFileName.IndexOf("/")));
                            oPage.BackgroundFileName = newProductID.ToString() + "/" + oPage.BackgroundFileName.Substring(oPage.BackgroundFileName.IndexOf("/"), oPage.BackgroundFileName.Length - oPage.BackgroundFileName.IndexOf("/"));

                        }



                    }
                    db.SaveChanges();

                    List<TemplateFonts> oLocalFonts = db.TemplateFonts.ToList();
                    foreach (var objFont in oTemplateFonts)
                    {
                        bool found = false;
                        foreach (var objLocal in oLocalFonts)
                        {
                            if (objLocal.CustomerID == objFont.CustomerID && objLocal.FontName == objFont.FontName && objLocal.FontFile == objFont.FontFile)
                            {
                                // checking if font exists
                                found = true;
                            }
                        }

                        if (!found)
                        {
                            try
                            {
                                // font not found
                                // adding font instance to db 
                                TemplateFonts newObjFont = new TemplateFonts();
                                newObjFont.CustomerID = objFont.CustomerID;
                                newObjFont.DisplayIndex = objFont.DisplayIndex;
                                newObjFont.FontBytes = objFont.FontBytes;
                                newObjFont.FontDisplayName = objFont.FontDisplayName;
                                newObjFont.FontFile = objFont.FontFile;
                                newObjFont.FontName = objFont.FontName;
                                newObjFont.IsEnable = objFont.IsEnable;
                                newObjFont.IsPrivateFont = objFont.IsPrivateFont;
                                newObjFont.ProductFontId = objFont.ProductFontId;
                                newObjFont.ProductId = objFont.ProductId;
                                newObjFont.FontPath = objFont.FontPath;
                                db.TemplateFonts.AddObject(newObjFont);
                                // downloading font file



                                string path = "";
                                if (objFont.FontPath == null)
                                {
                                    // mpc designers fonts or system fonts 
                                    path = "PrivateFonts/FontFace/";//+ objFont.FontFile;
                                }
                                else
                                {  // customer fonts 

                                    path = objFont.FontPath;
                                }

                                if (!Directory.Exists(BasePath + path))
                                {
                                    Directory.CreateDirectory((BasePath + path));
                                }
                                // downloading ttf file 
                                string RemoteURl = RemoteUrlBasePath + path + objFont.FontFile + ".ttf";
                                string DestURL = BasePath + path + objFont.FontFile + ".ttf";
                                DownloadFile(RemoteURl, DestURL);
                                // downloading woff file 
                                RemoteURl = RemoteUrlBasePath + path + objFont.FontFile + ".woff";
                                DestURL = BasePath + path + objFont.FontFile + ".woff";
                                DownloadFile(RemoteURl, DestURL);
                                // downloading eot file 
                                RemoteURl = RemoteUrlBasePath + path + objFont.FontFile + ".eot";
                                DestURL = BasePath + path + objFont.FontFile + ".eot";
                                DownloadFile(RemoteURl, DestURL);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }
                    }
                    db.SaveChanges();
                    //page
                    //DownloadFile(RemoteUrlBasePath + oTemplate.LowResPDFTemplates, BasePath + newProductID.ToString() + "/" + oTemplate.LowResPDFTemplates.Substring(oTemplate.LowResPDFTemplates.IndexOf("/"), oTemplate.LowResPDFTemplates.Length - oTemplate.LowResPDFTemplates.IndexOf("/")));
                    //oTemplate.LowResPDFTemplates = newProductID.ToString() + "/" + oTemplate.LowResPDFTemplates.Substring(oTemplate.LowResPDFTemplates.IndexOf("/"), oTemplate.LowResPDFTemplates.Length - oTemplate.LowResPDFTemplates.IndexOf("/"));


                    //side 2
                    //DownloadFile(RemoteUrlBasePath + oTemplate.Side2LowResPDFTemplates, BasePath + newProductID.ToString() + "/" + oTemplate.Side2LowResPDFTemplates.Substring(oTemplate.Side2LowResPDFTemplates.IndexOf("/"), oTemplate.Side2LowResPDFTemplates.Length - oTemplate.Side2LowResPDFTemplates.IndexOf("/")));
                    //oTemplate.Side2LowResPDFTemplates = newProductID.ToString() + "/" + oTemplate.Side2LowResPDFTemplates.Substring(oTemplate.Side2LowResPDFTemplates.IndexOf("/"), oTemplate.Side2LowResPDFTemplates.Length - oTemplate.Side2LowResPDFTemplates.IndexOf("/"));


                    foreach (TemplateBackgroundImages item in oTemplateImages)
                    {
                        string ext = Path.GetExtension(item.ImageName);
                        // generate thumbnail 
                        if (!ext.Contains("svg"))
                        {
                            string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                            string destPath = results[0] + "_thumb" + ext;
                            string localThumbnail = newProductID.ToString() + "/" + Path.GetFileName(destPath);
                            DownloadFile(RemoteUrlBasePath + "products/" + destPath, BasePath + "products\\" + localThumbnail);
                        }
                        item.ProductID = newProductID;
                        string NewLocalFileName = newProductID.ToString() + "/" + Path.GetFileName(item.ImageName);
                        DownloadFile(RemoteUrlBasePath + "products/" + item.ImageName, BasePath + "products\\" + NewLocalFileName);

                        item.ImageName = NewLocalFileName;
                        db.TemplateBackgroundImages.AddObject(item);
                    }


                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return newProductID;
        }

        public int MergeTemplateLocally(Templates oTemplate, List<TemplatePages> oTemplatePages, List<TemplateObjects> oTemplateObjects, List<TemplateBackgroundImages> oTemplateImages, List<TemplateFonts> oTemplateFonts, string RemoteUrlBasePath, string BasePath,int localTemplateID)
        {

            oTemplate.EntityKey = null;
         //   int newProductID = 0;
            int newPageID = 0;
            int oldPageID = 0;
            //string BasePath = System.Web.HttpContext.Current.Server.MapPath("../Designer/Products/");


            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    int i = 1;
                    var template = db.Templates.Where(g => g.ProductID == localTemplateID).SingleOrDefault();
                    if (template !=  null)
                    {
                        template.Orientation = oTemplate.Orientation;
                        template.PDFTemplateHeight = oTemplate.PDFTemplateHeight;
                        template.PDFTemplateWidth = oTemplate.PDFTemplateWidth;
                        
                    }
                    db.SaveChanges();
                    string targetFolder = BasePath + "products\\" + localTemplateID.ToString(); //System.Web.HttpContext.Current.Server.MapPath("../Designer/Products/" + newProductID.ToString());
                    if (!System.IO.Directory.Exists(targetFolder))
                    {
                        System.IO.Directory.CreateDirectory(targetFolder);
                    }
                    // delete old pages 
                    List<TemplatePages> oldPages = db.TemplatePages.Where(g => g.ProductID == localTemplateID).ToList();
                    foreach (var page in oldPages)
                    {
                        db.TemplatePages.DeleteObject(page);
                    }
                    // delete old objects 
                    List<TemplateObjects> oldObjects = db.TemplateObjects.Where(g => g.ProductID == localTemplateID).ToList();
                    foreach (var obj in oldObjects)
                    {
                        db.TemplateObjects.DeleteObject(obj);
                    }
                    //delete old images (to be done)

                    foreach (var oPage in oTemplatePages)
                    {
                        oldPageID = oPage.ProductPageID;
                        oPage.ProductID = localTemplateID;
                        db.TemplatePages.AddObject(oPage);
                        db.SaveChanges();
                        newPageID = oPage.ProductPageID;
                        foreach (var item in oTemplateObjects.Where(g => g.ProductPageId == oldPageID))
                        {

                            item.ProductPageId = newPageID;
                            item.ProductID = localTemplateID;

                            //updating the path if it is an image.
                            if (item.ObjectType == 3)
                            {
                                string filepath = item.ContentString.Substring(item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length));


                                //skip concatinating the path if its a placeholder, cuz place holder is kept in a different path and doesnt need to be copied.
                                if (!item.ContentString.Contains("assets/Imageplaceholder"))
                                {
                                    item.ContentString = "Designer/Products/" + localTemplateID.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
                                }
                            }
                            db.TemplateObjects.AddObject(item);
                        }

                        //page
                        if (oPage.BackGroundType == 1 || oPage.BackGroundType == 3)
                        {
                            DownloadFile(RemoteUrlBasePath + "products/" + oPage.BackgroundFileName, BasePath + "products\\" + localTemplateID.ToString() + "/" + oPage.BackgroundFileName.Substring(oPage.BackgroundFileName.IndexOf("/"), oPage.BackgroundFileName.Length - oPage.BackgroundFileName.IndexOf("/")));
                            oPage.BackgroundFileName = localTemplateID.ToString() + "/" + oPage.BackgroundFileName.Substring(oPage.BackgroundFileName.IndexOf("/"), oPage.BackgroundFileName.Length - oPage.BackgroundFileName.IndexOf("/"));

                        }



                    }
                    db.SaveChanges();

                    List<TemplateFonts> oLocalFonts = db.TemplateFonts.ToList();
                    foreach (var objFont in oTemplateFonts)
                    {
                        bool found = false;
                        foreach (var objLocal in oLocalFonts)
                        {
                            if (objLocal.CustomerID == objFont.CustomerID && objLocal.FontName == objFont.FontName && objLocal.FontFile == objFont.FontFile)
                            {
                                // checking if font exists
                                found = true;
                            }
                        }

                        if (!found)
                        {
                            try
                            {
                                // font not found
                                // adding font instance to db 
                                TemplateFonts newObjFont = new TemplateFonts();
                                newObjFont.CustomerID = objFont.CustomerID;
                                newObjFont.DisplayIndex = objFont.DisplayIndex;
                                newObjFont.FontBytes = objFont.FontBytes;
                                newObjFont.FontDisplayName = objFont.FontDisplayName;
                                newObjFont.FontFile = objFont.FontFile;
                                newObjFont.FontName = objFont.FontName;
                                newObjFont.IsEnable = objFont.IsEnable;
                                newObjFont.IsPrivateFont = objFont.IsPrivateFont;
                                newObjFont.ProductFontId = objFont.ProductFontId;
                                newObjFont.ProductId = objFont.ProductId;
                                newObjFont.FontPath = objFont.FontPath;
                                db.TemplateFonts.AddObject(newObjFont);
                                // downloading font file



                                string path = "";
                                if (objFont.FontPath == null)
                                {
                                    // mpc designers fonts or system fonts 
                                    path = "PrivateFonts/FontFace/";//+ objFont.FontFile;
                                }
                                else
                                {  // customer fonts 

                                    path = objFont.FontPath;
                                }

                                if (!Directory.Exists(BasePath + path))
                                {
                                    Directory.CreateDirectory((BasePath + path));
                                }
                                // downloading ttf file 
                                string RemoteURl = RemoteUrlBasePath + path + objFont.FontFile + ".ttf";
                                string DestURL = BasePath + path + objFont.FontFile + ".ttf";
                                DownloadFile(RemoteURl, DestURL);
                                // downloading woff file 
                                RemoteURl = RemoteUrlBasePath + path + objFont.FontFile + ".woff";
                                DestURL = BasePath + path + objFont.FontFile + ".woff";
                                DownloadFile(RemoteURl, DestURL);
                                // downloading eot file 
                                RemoteURl = RemoteUrlBasePath + path + objFont.FontFile + ".eot";
                                DestURL = BasePath + path + objFont.FontFile + ".eot";
                                DownloadFile(RemoteURl, DestURL);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }
                    }
                    db.SaveChanges();
                    foreach (TemplateBackgroundImages item in oTemplateImages)
                    {
                        string ext = Path.GetExtension(item.ImageName);
                        // generate thumbnail 
                        if (!ext.Contains("svg"))
                        {
                            string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                            string destPath = results[0] + "_thumb" + ext;
                            string localThumbnail = localTemplateID.ToString() + "/" + Path.GetFileName(destPath);
                            DownloadFile(RemoteUrlBasePath + "products/" + destPath, BasePath + "products\\" + localThumbnail);
                        }
                        item.ProductID = localTemplateID;
                        string NewLocalFileName = localTemplateID.ToString() + "/" + Path.GetFileName(item.ImageName);
                        DownloadFile(RemoteUrlBasePath + "products/" + item.ImageName, BasePath + "products\\" + NewLocalFileName);

                        item.ImageName = NewLocalFileName;
                        db.TemplateBackgroundImages.AddObject(item);
                    }


                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return localTemplateID;
        }


        private bool DownloadFile(string SourceURL, string DestinationBasePath)
        {
            Stream stream = null;
            MemoryStream memStream = new MemoryStream();
            try
            {
                WebRequest req = WebRequest.Create(SourceURL);
                WebResponse response = req.GetResponse();
                if (response != null)
                {
                    stream = response.GetResponseStream();


                    byte[] downloadedData = new byte[0];

                    byte[] buffer = new byte[1024];

                    //Get Total Size
                    int dataLength = (int)response.ContentLength;


                    while (true)
                    {
                        //Try to read the data
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                            break;
                        else
                            memStream.Write(buffer, 0, bytesRead);
                    }

                    File.WriteAllBytes(DestinationBasePath, memStream.ToArray());
                }
                else
                    return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Clean up
                if (stream != null)
                    stream.Close();

                if (memStream != null)
                    memStream.Close();
            }
            return true;
        }


        // MIS specific functions 

        //returns the leafnode categories in which template exisits.
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "Categories")]
        public List<vw_ProductCategoriesLeafNodes> GetCategories()
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;

                    var objectsList = db.vw_ProductCategoriesLeafNodes.OrderBy(g => g.CategoryName).ToList();

                    foreach (var item in objectsList)
                    {
                        item.CategoryName = HttpUtility.HtmlDecode(item.CategoryName);
                    }
                    return objectsList;
                }
                catch (Exception ex)
                {
                    throw ex;
                    // throw new Exception(ex.ToString());
                }

            }
        }


        public List<vw_ProductCategoriesLeafNodesWithRes> GetCategoriesWithResolution()
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {

                try
                {
                    db.ContextOptions.LazyLoadingEnabled = false;

                    var objectsList = db.vw_ProductCategoriesLeafNodesWithRes.OrderBy(g => g.CategoryName).ToList();
                    return objectsList;
                }
                catch (Exception ex)
                {
                    throw ex;
                    // throw new Exception(ex.ToString());
                }

            }
        }


        public int AddEditTemplate(int? TemplateId, string ProductName, int ProductCategoryId, double Height, double Width, int Orientation, bool IsdoubleSided)
        {
            int returnId = 0;

            Height = Util.MMToPoint(Height);
            Width = Util.MMToPoint(Width);

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                Templates template;

                if (TemplateId == null)
                {
                    template = new Templates();

                    var maxq = (from tab1 in db.Templates
                                select tab1.Code).Max();

                    //int code = 0;
                    //if (maxq != null)
                    //    code = Convert.ToInt32(maxq) + 1;

                    //template.Code = "00" + code.ToString();

                    template.ProductName = ProductName;
                    template.ProductCategoryID = ProductCategoryId;
                    template.PDFTemplateHeight = Height;
                    template.PDFTemplateWidth = Width;
                    template.CuttingMargin = Util.MMToPoint(5);
                    template.Orientation = Orientation;
                    template.IsDoubleSide = IsdoubleSided;
                    template.IsUsePDFFile = true;

                    db.Templates.AddObject(template);

                    db.SaveChanges();

                    returnId = template.ProductID;

                    CreateBlankBackgroundPDFs(template.ProductID, Height, Width, Orientation);

                    template.LowResPDFTemplates = returnId.ToString() + "////" + "Side1.pdf";
                    template.Side2LowResPDFTemplates = returnId.ToString() + "////" + "Side2.pdf";

                    //adding multi pages
                    if (IsdoubleSided)
                    {
                        TemplatePages oPage1 = new TemplatePages();
                        oPage1.ProductID = returnId;
                        oPage1.BackGroundType = 1;
                        oPage1.BackgroundFileName = returnId.ToString() + "////" + "Side1.pdf";
                        oPage1.Orientation = Orientation;
                        oPage1.PageName = "Front";
                        oPage1.PageNo = 1;
                        oPage1.PageType = 1;

                        db.TemplatePages.AddObject(oPage1);


                        TemplatePages oPage2 = new TemplatePages();
                        oPage2.ProductID = returnId;
                        oPage2.BackGroundType = 1;
                        oPage2.BackgroundFileName = returnId.ToString() + "////" + "Side2.pdf";
                        oPage2.Orientation = Orientation;
                        oPage2.PageName = "Back";
                        oPage2.PageNo = 2;
                        oPage2.PageType = 2;

                        db.TemplatePages.AddObject(oPage2);
                    }
                    else
                    {
                        TemplatePages oPage1 = new TemplatePages();
                        oPage1.ProductID = returnId;
                        oPage1.BackGroundType = 1;
                        oPage1.BackgroundFileName = returnId.ToString() + "////" + "Side1.pdf";
                        oPage1.Orientation = Orientation;
                        oPage1.PageName = "Front";
                        oPage1.PageNo = 1;
                        oPage1.PageType = 1;

                        db.TemplatePages.AddObject(oPage1);
                    }

                    db.SaveChanges();


                }
                else
                {
                    template = db.Templates.Where(c => c.ProductID == TemplateId.Value).Single();

                    if (template.PDFTemplateHeight != Height || template.PDFTemplateWidth != Width || template.Orientation != Orientation || template.IsDoubleSide != IsdoubleSided)
                    {
                        template.ProductName = ProductName;
                        template.ProductCategoryID = ProductCategoryId;
                        template.PDFTemplateHeight = Height;
                        template.PDFTemplateWidth = Width;
                        template.Orientation = Orientation;
                        template.IsDoubleSide = IsdoubleSided;

                        CreateBlankBackgroundPDFs(template.ProductID, Height, Width, Orientation);

                        db.SaveChanges();
                        returnId = template.ProductID;
                    }
                }

                return returnId;
            }
        }


        public List<CategoryTypes> getCategoryTypes()
        {
            List<CategoryTypes> list = new List<CategoryTypes>();

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.ContextOptions.LazyLoadingEnabled = false;
                list = db.CategoryTypes.ToList();
            }

            return list;
        }


        public List<CategoryRegions> getCategoryRegions()
        {
            List<CategoryRegions> list = new List<CategoryRegions>();

            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                db.ContextOptions.LazyLoadingEnabled = false;
                list = db.CategoryRegions.ToList();
            }

            return list;
        }

        /// <summary>
        /// Called from webstore to regenerate template pdfs used to generate and copy pdf for production
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="printCuttingMargins"></param>
        /// <returns></returns>
        public bool regeneratePDFs(int templateID, bool printCuttingMargins,bool drawBleedArea,bool isMultipageMode)
        {
            try
            {
                List<TemplatePages> oTemplatePages = null;
                Templates objProduct = null;
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    TemplateSvc objSvc = new TemplateSvc();
                    db.ContextOptions.LazyLoadingEnabled = false;
                    string targetFolder = "";
                    targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                    objProduct = db.Templates.Where(g => g.ProductID == templateID).SingleOrDefault();
                    oTemplatePages = db.TemplatePages.Where(g => g.ProductID == templateID).ToList();
                    if (isMultipageMode)
                    {
                        bool hasOverlayObject = false;
                        byte[] PDFFile = objSvc.generatePDF(objProduct, oTemplatePages, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, false, printCuttingMargins, false, out hasOverlayObject, false,drawBleedArea);
                        //writing the PDF to FS
                        System.IO.File.WriteAllBytes(targetFolder + templateID + "/pages.pdf", PDFFile);
                        if (hasOverlayObject)
                        {
                            byte[] overlayPDFFile = objSvc.generatePDF(objProduct, oTemplatePages, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, printCuttingMargins, false, out hasOverlayObject, true,drawBleedArea);
                            System.IO.File.WriteAllBytes(targetFolder + templateID + "/pagesoverlay.pdf", overlayPDFFile);
                       //     objSvc.generatePagePreview(overlayPDFFile, targetFolder, templateID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, false);
                        }
                    }
                    else
                    {
                        foreach (TemplatePages objPage in oTemplatePages)
                        {
                            bool hasOverlayObject = false;
                            byte[] PDFFile = objSvc.generatePDF(objProduct, objPage, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, false, printCuttingMargins, false, out hasOverlayObject, false);
                            //writing the PDF to FS
                            System.IO.File.WriteAllBytes(targetFolder + templateID + "/p" + objPage.PageNo + ".pdf", PDFFile);
                            //gernating 
                            objSvc.generatePagePreview(PDFFile, targetFolder, templateID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, false);

                            if (hasOverlayObject)
                            {
                                objPage.hasOverlayObjects = true;
                                // generate overlay PDF 
                                byte[] overlayPDFFile = objSvc.generatePDF(objProduct, objPage, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, printCuttingMargins, false, out hasOverlayObject, true);
                                System.IO.File.WriteAllBytes(targetFolder + templateID + "/p" + objPage.PageNo + "overlay.pdf", overlayPDFFile);
                                objSvc.generatePagePreview(overlayPDFFile, targetFolder, templateID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, false);
                            }
                            else
                            {
                                objPage.hasOverlayObjects = false;
                            }

                        }
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
                //return false;
            }
        }

        ///
        ///  called from mis for generating template from pdf 
        ///  
        public bool generateTemplateFromPDF(string filePhysicalPath, int mode, int templateID, int CustomerID)
        {
            try
            {
                PdfExtractor ext = new PdfExtractor();
                if (mode == 2)
                {
                    // ext.ConvertPdfToObj(filePhysicalPath,templateID,CustomerID);
                    ext.CovertPdfToBackgroundWithObjects(filePhysicalPath, templateID);
                }
                else
                {
                    ext.CovertPdfToBackground(filePhysicalPath, templateID);
                }
                if (File.Exists(filePhysicalPath))
                {
                    File.Delete(filePhysicalPath);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            //PdfExtractor.
            return true;
        }


        public bool DeleteBlankBackgroundPDFsByPages(int TemplateID, List<TemplatePages> PagesList)
        {

            try
            {
                string basePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + TemplateID.ToString() + "/");
                if (Directory.Exists(basePath))
                {
                    foreach (var objPage in PagesList)
                    {
                        if (File.Exists(basePath + "Side" + objPage.PageNo.ToString() + ".pdf"))
                        {
                            File.Delete(basePath + "Side" + objPage.PageNo.ToString() + ".pdf");
                        }
                        if (File.Exists(basePath + "templatImgBk" + objPage.PageNo.ToString() + ".jpg"))
                        {
                            File.Delete(basePath + "templatImgBk" + objPage.PageNo.ToString() + ".jpg");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

        public void processTemplatePDF(int TemplateID, bool printCropMarks, bool printWaterMarks, bool isroundCorners)
        {
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    string targetFolder = "";
                    targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                    var objProduct = db.Templates.Where(g => g.ProductID == TemplateID).SingleOrDefault();
                    List<TemplatePages> oTemplatePages = db.TemplatePages.Where(g => g.ProductID == TemplateID).ToList();
                    foreach (TemplatePages objPage in oTemplatePages)
                    {
                        TemplateSvc objSvc = new TemplateSvc();
                        bool hasOverlayObject = false;
                        byte[] PDFFile = objSvc.generatePDF(objProduct, objPage, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, printCropMarks, printWaterMarks, out hasOverlayObject, false);
                        //writing the PDF to FS
                        System.IO.File.WriteAllBytes(targetFolder + TemplateID + "/p" + objPage.PageNo + ".pdf", PDFFile);
                        //gernating 
                        objSvc.generatePagePreview(PDFFile, targetFolder, TemplateID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, isroundCorners);
                        if (hasOverlayObject)
                        {
                            // generate overlay PDF 
                            byte[] overlayPDFFile = objSvc.generatePDF(objProduct, objPage, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, printCropMarks, printWaterMarks, out hasOverlayObject, true);
                            System.IO.File.WriteAllBytes(targetFolder + TemplateID + "/p" + objPage.PageNo + "overlay.pdf", overlayPDFFile);
                            objSvc.generatePagePreview(overlayPDFFile, targetFolder, TemplateID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, isroundCorners);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }





    public enum DesignerModes
    {
        SimpleEndUser = 1,
        AdvancedEndUser = 3,
        CreatorMode = 2,
        AnnanomousMode = 4,
        CorporateMode = 5
    }


    public enum FontLoadModes
    {
        All = 1,
        SystemOnly = 2,
        PrivateOnly = 3,
        SystemAndUsed = 4

    }

    public enum SaveOperationTypes
    {
        SaveReturnToDetails = 1,
        SaveGenerateOpenPDFPreview = 2,
        SaveGeneratePDFAttachMode = 3


    }


    class PageComparer : IEqualityComparer<TemplatePages>
    {
        #region IEqualityComparer<Contact> Members

        public bool Equals(TemplatePages x, TemplatePages y)
        {
            if (x.ProductPageID == null)
                return false;
            else if (y.ProductPageID == null)
                return false;
            else
            {
                return x.ProductPageID.Equals(y.ProductPageID);
            }
        }

        public int GetHashCode(TemplatePages obj)
        {
            return obj.ProductPageID.GetHashCode();
        }

        #endregion
    }
    //class TemplateList
    //{
    //    public int ProductID { get; set; }
    //    public string? Code { get; set; }
    //    public string? ProductName { get; set; }
    //    public string? Description { get; set; }
    //    public int? ProductCategoryID { get; set; }
    //    public string? LowResPDFTemplates { get; set; }
    //    public string? BackgroundArtwork { get; set; }
    //    public string? Side2LowResPDFTemplates { get; set; }
    //    public string? Side2BackgroundArtwork { get; set; }
    //    public string? Thumbnail { get; set; }
    //    public string? Image { get; set; }
    //    public bool? IsDisabled { get; set; }
    //    public int? PTempId { get; set; }
    //    public bool? IsDoubleSide { get; set; }
    //    public bool? IsUsePDFFile { get; set; }
    //    public double? PDFTemplateWidth { get; set; }
    //    public double? PDFTemplateHeight { get; set; }
    //    public bool? IsUseBackGroundColor { get; set; }

    //    public int? BgR { get; set; }
    //    public int? BgG { get; set; }
    //    public int? BgB { get; set; }
    //    public bool? IsUseSide2BackGroundColor { get; set; }
    //    public int? Side2BgR { get; set; }
    //    public int? Side2BgG { get; set; }
    //    public int? Side2BgB { get; set; }
    //    public double? CuttingMargin { get; set; }
    //    public int? MultiPageCount { get; set; }
    //    public int? Orientation { get; set; }
    //    public string? MatchingSetTheme { get; set; }
    //    public int? BaseColorID { get; set; }
    //    public int? SubmittedBy { get; set; }
    //    public string? SubmittedByName { get; set; }
    //    public DateTime? SubmitDate { get; set; }
    //    public int? Status { get; set; }
    //    public int? ApprovedBy { get; set; }
    //    public string? ApprovedByName { get; set; }

    //    public int? UserRating { get; set; }
    //    public int? UsedCount { get; set; }
    //    public int? MPCRating { get; set; }
    //    public string? RejectionReason { get; set; }
    //    public DateTime? ApprovalDate { get; set; }
    //    public string? TempString { get; set; }
    //    public int? MatchingSetID { get; set; }
    //    public string? SLThumbnail { get; set; }
    //    public string? FullView { get; set; }
    //    public string? SuperView { get; set; }
    //    public string? ColorHex { get; set; }
    //    public int? TemplateOwner { get; set; }
    //    public string? TemplateOwnerName { get; set; }
    //    public bool? IsPrivate { get; set; }
    //    public DateTime? ApprovedDate { get; set; }
    //    public bool? IsCorporateEditable { get; set; }
    //    public int? TemplateType { get; set; }
    //    public bool? isWatermarkText { get; set; }
    //    public bool? isSpotTemplate { get; set; }
    //}
}
