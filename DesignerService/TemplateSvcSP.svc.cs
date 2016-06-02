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
using DesignerService.Utilities;
using System.Net;



namespace DesignerService
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
			Doc pdfDoc = new Doc();
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
				pdfDoc.Dispose();
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
                    return db.TemplatePages.Where(g => g.ProductID == TemplateID).ToList();
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
        /// Return Template Images library
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

                        var backgrounds = db.TemplateBackgroundImages.Where(g => g.ProductID == TemplateID).ToList();


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


		public List<Templates> GetTemplates(string keywords, int ProductCategoryID, int PageNo, int PageSize, bool callbind, int status, int UserID, string Role, out int PageCount)
		{
			using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
			{

				try
				{
					db.ContextOptions.LazyLoadingEnabled = false;


					int vUserID = UserID;

					var predicate = PredicateBuilder.True<Templates>();


					predicate = predicate.Or(p => p.ProductName.Contains(keywords));
					if (ProductCategoryID != 0)
					{
						predicate = predicate.And(p => p.ProductCategoryID == ProductCategoryID);
					}

					if (keywords != string.Empty)
					{
						predicate = predicate.And(p => p.ProductName.Contains(keywords));
					}

					if (Role.ToString().ToLower() == "admin")
					{

						switch (status)
						{
							case 0: predicate = predicate.And(p => p.Status != 1); break;
							case 1: predicate = predicate.And(p => p.Status != 1); break;
							case 2: predicate = predicate.And(p => p.Status == 2); break;
							case 3: predicate = predicate.And(p => p.Status == 3); break;
							case 4: predicate = predicate.And(p => p.Status == 4); break;

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
																			 

						PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(db.Templates.AsExpandable().Where(predicate).Count() / PageSize)));

						var temps = db.Templates.AsExpandable().Where(predicate).OrderBy(g => g.ProductName).Skip((PageNo) * PageSize).Take(PageSize);
					


					foreach (var item in temps)
					{
						if (item.Thumbnail == null || item.Thumbnail == string.Empty)
							item.Thumbnail = "cardgeneral.jpg";
					}

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
		
		private TemplateObjects ReturnObject(string Name, string Content, double PositionX, double PositionY, int ProductID, int DisplayOrder, int CtrlID, double FontSize, bool IsBold)
		{
			TemplateObjects oTempObject = new TemplateObjects();
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
			oTempObject.ProductPageId = 0;
			oTempObject.ParentId = 0;
			oTempObject.OffsetX = 0;
			oTempObject.OffsetY = 0;
			oTempObject.IsNewLine = false;
			oTempObject.TCtlName = "CtlTxtContent_" + CtrlID.ToString();
			oTempObject.ExField1 = "";
			oTempObject.ExField2 = "";

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
        public bool SaveTemplates(Templates oTemplate, ObservableCollection<TemplateIndustryTags> lstIndustryTags, ObservableCollection<TemplateThemeTags> lstThemeTags, bool IsAdd, out int NewTemplateID, bool IsCatChanged)
        {
            try
            {
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
                            oTemplate.PDFTemplateHeight = 500;
                            oTemplate.PDFTemplateWidth = 500;
                        }

                        //var maxq = (from tab1 in db.Templates
                        //            select tab1.Code).Max();

                        //int code = 0;
                        //if (maxq != null)
                        //    code = Convert.ToInt32(maxq) +1;

                        //oTemplate.Code = "00" + code.ToString();

                        db.SaveChanges();

                        CreateBlankBackgroundPDFs(oTemplate.ProductID, Util.MMToPoint(SelectedProductCategory.HeightRestriction.Value), Util.MMToPoint(SelectedProductCategory.WidthRestriction.Value), oTemplate.Orientation.Value);



                        oTemplate.LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + "Side1.pdf";
                        oTemplate.Side2LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + "Side2.pdf";

                        db.TemplateObjects.AddObject(ReturnObject("CompanyName", "Your Company Name", 50, 40, oTemplate.ProductID, 401, 1, 14, true));
                        db.TemplateObjects.AddObject(ReturnObject("CompanyMessage", "Your Company Message", 50, 50, oTemplate.ProductID, 402, 2, 10, false));
                        db.TemplateObjects.AddObject(ReturnObject("Name", "Your Name", 50, 60, oTemplate.ProductID, 403, 3, 12, true));
                        db.TemplateObjects.AddObject(ReturnObject("Title", "Your Title", 50, 70, oTemplate.ProductID, 404, 4, 10, false));
                        db.TemplateObjects.AddObject(ReturnObject("AddressLine1", "Address Line 1", 50, 80, oTemplate.ProductID, 405, 5, 10, false));
                        //db.TemplateObjects.AddObject(ReturnObject("AddressLine2", "Address Line 2", 50, 90, oTemplate.ProductID, 406, 6,10,false));
                        //db.TemplateObjects.AddObject(ReturnObject("AddressLine3", "Address Line 3", 50, 100, oTemplate.ProductID, 407, 7,10,false));
                        db.TemplateObjects.AddObject(ReturnObject("Phone", "Telephone / Other", 50, 110, oTemplate.ProductID, 408, 8, 10, false));
                        db.TemplateObjects.AddObject(ReturnObject("Fax", "Fax / Other", 50, 120, oTemplate.ProductID, 409, 9, 10, false));
                        db.TemplateObjects.AddObject(ReturnObject("Email", "Email address / Other", 50, 130, oTemplate.ProductID, 410, 10, 10, false));
                        db.TemplateObjects.AddObject(ReturnObject("Website", "Website address", 50, 140, oTemplate.ProductID, 411, 11, 10, false));

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

                        string basePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + oTemplate.ProductID.ToString() + "/");

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
                        tmpTemplate.SubmitDate = oTemplate.SubmitDate;
                        tmpTemplate.SubmittedBy = oTemplate.SubmittedBy;
                        tmpTemplate.SubmittedByName = oTemplate.SubmittedByName;
                        tmpTemplate.ApprovalDate = oTemplate.ApprovalDate;
                        tmpTemplate.RejectionReason = oTemplate.RejectionReason;
                        tmpTemplate.ApprovedBy = oTemplate.ApprovedBy;
                        tmpTemplate.ApprovedByName = oTemplate.ApprovedByName;
                        tmpTemplate.Status = oTemplate.Status;
                        tmpTemplate.MPCRating = oTemplate.MPCRating;
                        tmpTemplate.MatchingSetID = oTemplate.MatchingSetID;
                        tmpTemplate.Orientation = oTemplate.Orientation;
                        tmpTemplate.Code = oTemplate.Code;

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


                        db.SaveChanges();

                        //create the blank background if does not exists
                        if (oTemplate.LowResPDFTemplates != oTemplate.ProductID.ToString() + "/" + "Side1.pdf")
                        {

                            var SelectedProductCategory = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();

                            CreateBlankBackgroundPDFs(tmpTemplate.ProductID, Util.MMToPoint(SelectedProductCategory.HeightRestriction.Value), Util.MMToPoint(SelectedProductCategory.WidthRestriction.Value), oTemplate.Orientation.Value);

                            tmpTemplate.LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + "Side1.pdf";
                            tmpTemplate.Side2LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + "Side2.pdf";
                        }

                        if (IsCatChanged)
                        {
                            var SelectedProductCategory = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();
                            tmpTemplate.PDFTemplateHeight = Util.MMToPoint(SelectedProductCategory.HeightRestriction.Value);
                            tmpTemplate.PDFTemplateWidth = Util.MMToPoint(SelectedProductCategory.WidthRestriction.Value);
                            CreateBlankBackgroundPDFs(tmpTemplate.ProductID, Util.MMToPoint(SelectedProductCategory.HeightRestriction.Value), Util.MMToPoint(SelectedProductCategory.WidthRestriction.Value), oTemplate.Orientation.Value);


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

                        NewTemplateID = tmpTemplate.ProductID;
                    }

                }
                return true;
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


					//copy the background PDF Templates
					Templates oTemplate = db.Templates.Where(g => g.ProductID == result).Single();
					//copy side 1
					File.Copy(Path.Combine(BasePath, oTemplate.LowResPDFTemplates), BasePath + result.ToString() + "/" + oTemplate.LowResPDFTemplates.Substring(oTemplate.LowResPDFTemplates.IndexOf("/"), oTemplate.LowResPDFTemplates.Length - oTemplate.LowResPDFTemplates.IndexOf("/")));
					oTemplate.LowResPDFTemplates = result.ToString() + "/" + oTemplate.LowResPDFTemplates.Substring(oTemplate.LowResPDFTemplates.IndexOf("/"), oTemplate.LowResPDFTemplates.Length - oTemplate.LowResPDFTemplates.IndexOf("/"));



					File.Copy(Path.Combine(BasePath, oTemplate.Side2LowResPDFTemplates), BasePath + result.ToString() + "/" + oTemplate.Side2LowResPDFTemplates.Substring(oTemplate.Side2LowResPDFTemplates.IndexOf("/"), oTemplate.Side2LowResPDFTemplates.Length - oTemplate.Side2LowResPDFTemplates.IndexOf("/")));
					oTemplate.Side2LowResPDFTemplates = result.ToString() + "/" + oTemplate.Side2LowResPDFTemplates.Substring(oTemplate.Side2LowResPDFTemplates.IndexOf("/"), oTemplate.Side2LowResPDFTemplates.Length - oTemplate.Side2LowResPDFTemplates.IndexOf("/"));

					foreach (var item in db.TemplateObjects.Where( g => g.ProductID == result && g.ObjectType == 3))
					{
						string filepath = item.ContentString.Substring(item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length) );
						item.ContentString = "Designer/Products/" + result.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
					}

					//

					/// todo
					//copy the background images

					var backimgs = db.TemplateBackgroundImages.Where(g => g.ProductID == result);

					foreach (TemplateBackgroundImages item in backimgs)
					{

						string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + item.ImageName);
						string filename;

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



		private bool CreateBlankBackgroundPDFs(int TemplateID, double height, double width, int Orientation)
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
                oTags =  oContext.Tags.ToList();
            }

            return oTags;
        }



        //webstore specific functions //////////////////////////////////////////////////////////////////////////

        //public QuickText GetContactQuickTextFields(int CustomerID, int ContactID)
        //{

        //    QuickText oQuickText = null;
        //    Web2Print.DAL.tbl_contactcompanies curCustomer = CustomerManager.GetCustomer(CustomerID);
        //    if (curCustomer != null)
        //    {
        //        Web2Print.DAL.tbl_contacts oContact = curCustomer.tbl_contacts.Where(c => c.ContactID == ContactID).Single();
        //        if (oContact != null)
        //        {

        //            oQuickText = new QuickText();
        //            oQuickText.Address1 = oContact.quickAddress1;
        //            oQuickText.Address2 = oContact.quickAddress2;
        //            oQuickText.Address3 = oContact.quickAddress3;
        //            oQuickText.Company = oContact.quickCompanyName;
        //            oQuickText.CompanyMessage = oContact.quickCompMessage;
        //            oQuickText.Email = oContact.quickEmail;
        //            oQuickText.Fax = oContact.quickFax;
        //            oQuickText.Name = oContact.quickFullName;
        //            oQuickText.Telephone = oContact.quickPhone;
        //            oQuickText.Title = oContact.quickTitle;
        //            oQuickText.Website = oContact.quickWebsite;
        //            oQuickText.LogoPath = curCustomer.Image;

        //        }

        //    }
        //    else
        //        throw new Exception("Customer not Found with specified ID");


        //    return oQuickText;
        //}

        //public bool UpdateQuickText(QuickText oQuickTextData, int ContactID)
        //{
        //    using (Web2Print.DAL.MPCEntities dbContext = new Web2Print.DAL.MPCEntities())
        //    {
        //        Web2Print.DAL.tbl_contacts currContact = dbContext.tbl_contacts.Where(c => c.ContactID == ContactID).Single();
        //        if (currContact != null)
        //        {

        //            currContact.quickAddress1 = oQuickTextData.Address1;
        //            currContact.quickAddress2 = oQuickTextData.Address2;
        //            currContact.quickAddress3 = oQuickTextData.Address3;
        //            currContact.quickCompanyName = oQuickTextData.Company;
        //            currContact.quickCompMessage = oQuickTextData.CompanyMessage;
        //            currContact.quickEmail = oQuickTextData.Email;
        //            currContact.quickFax = oQuickTextData.Fax;
        //            currContact.quickFullName = oQuickTextData.Name;
        //            currContact.quickPhone = oQuickTextData.Telephone;
        //            currContact.quickTitle = oQuickTextData.Title;
        //            currContact.quickWebsite = oQuickTextData.Website;

        //            dbContext.SaveChanges();
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //}

        public List<Templates> GetTemplatesbyCategory(string GlobalCategoryName, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            var predicate = PredicateBuilder.True<Templates>();
            try
            {
                TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities();
                oContext.ContextOptions.LazyLoadingEnabled = false;
                if (Keywords != string.Empty)
                {
                    predicate = predicate.And(p => p.ProductName.Contains(Keywords));
                }

                //main query + category
                var result = (from T in oContext.Templates
                              join PC in oContext.tbl_ProductCategory
                                          on T.ProductCategoryID equals PC.ProductCategoryID
                              where PC.CategoryName == GlobalCategoryName
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

                predicate = predicate.And(p => p.Status == 3 || p.Status == 2);

                //result = (from PC in result
                //          where PC.CategoryName == GlobalCategoryName && T.Thumbnail != null

                result = (from T in result
                          select T).AsExpandable().Where(predicate).OrderBy(g => g.ProductName);


                SearchCount = result.Count();
                PageCount = Convert.ToInt32(Math.Ceiling(SearchCount / Convert.ToDecimal(PageSize)));


                int skip = PageNo * PageSize;

                //if (skip == PageSize)
                //    skip = 0;

                return result.Skip(skip).Take(PageSize).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // Add By Sajid Ali on 05/23/2012
        public List<Templates> GetTemplatesbyProductIds(int[] ProductIds, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            var predicate = PredicateBuilder.True<Templates>();
            try
            {
                TemplateDesignerV2Entities oContext = new TemplateDesignerV2Entities();

                if (Keywords != string.Empty)
                {
                    predicate = predicate.And(p => p.ProductName.Contains(Keywords));
                }

                //main query + ProductIds
                var result = (from T in oContext.Templates
                              where ProductIds.Contains(T.ProductID)
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

                predicate = predicate.And(p => p.Status == 3 || p.Status == 2);

                //result = (from PC in result
                //          where PC.CategoryName == GlobalCategoryName && T.Thumbnail != null

                result = (from T in result
                          select T).AsExpandable().Where(predicate).OrderBy(g => g.ProductName);


                SearchCount = result.Count();
                PageCount = Convert.ToInt32(Math.Ceiling(SearchCount / Convert.ToDecimal(PageSize)));


                int skip = PageNo * PageSize;

                //if (skip == PageSize)
                //    skip = 0;

                return result.Skip(skip).Take(PageSize).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int SaveTemplateLocally(Templates oTemplate, List<TemplatePages> oTemplatePages, List<TemplateObjects> oTemplateObjects, List<TemplateBackgroundImages> oTemplateImages, string RemoteUrlBasePath)
        {

            oTemplate.EntityKey = null;
            int newProductID = 0;
            int newPageID = 0;
            int oldPageID = 0;
            string BasePath = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/");


            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    
                    db.Templates.AddObject(oTemplate);

                    db.SaveChanges();

                    newProductID = oTemplate.ProductID;

                    foreach (var oPage in oTemplatePages)
                    {
                        oldPageID = oPage.ProductPageID;
                        oPage.ProductID = newProductID;
                        db.TemplatePages.AddObject(oPage);
                        db.SaveChanges();
                        newPageID = oPage.ProductPageID;


                        foreach (var item in oPage.TemplateObjects.Where( g=> g.ProductPageId == oldPageID))
                        {

                            item.ProductPageId = newPageID;
                            item.ProductID = newProductID;

                            //updating the path if it is an image.
                            if (item.ObjectType == 3)
                            {
                                string filepath = item.ContentString.Substring(item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length));
                                item.ContentString = "Designer/Products/" + newProductID.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
                            }
                            db.TemplateObjects.AddObject(item);
                        }

                    }


                    

                    string targetFolder = System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/" + newProductID.ToString());
                    if (!System.IO.Directory.Exists(targetFolder))
                    {
                        System.IO.Directory.CreateDirectory(targetFolder);
                    }


                    //copy side 1
                    DownloadFile(RemoteUrlBasePath + oTemplate.LowResPDFTemplates, BasePath + newProductID.ToString() + "/" + oTemplate.LowResPDFTemplates.Substring(oTemplate.LowResPDFTemplates.IndexOf("/"), oTemplate.LowResPDFTemplates.Length - oTemplate.LowResPDFTemplates.IndexOf("/")));
                    oTemplate.LowResPDFTemplates = newProductID.ToString() + "/" + oTemplate.LowResPDFTemplates.Substring(oTemplate.LowResPDFTemplates.IndexOf("/"), oTemplate.LowResPDFTemplates.Length - oTemplate.LowResPDFTemplates.IndexOf("/"));


                    //side 2
                    DownloadFile(RemoteUrlBasePath + oTemplate.Side2LowResPDFTemplates, BasePath + newProductID.ToString() + "/" + oTemplate.Side2LowResPDFTemplates.Substring(oTemplate.Side2LowResPDFTemplates.IndexOf("/"), oTemplate.Side2LowResPDFTemplates.Length - oTemplate.Side2LowResPDFTemplates.IndexOf("/")));
                    oTemplate.Side2LowResPDFTemplates = newProductID.ToString() + "/" + oTemplate.Side2LowResPDFTemplates.Substring(oTemplate.Side2LowResPDFTemplates.IndexOf("/"), oTemplate.Side2LowResPDFTemplates.Length - oTemplate.Side2LowResPDFTemplates.IndexOf("/"));


                    foreach (TemplateBackgroundImages item in oTemplateImages)
                    {
                        
                        item.ProductID = newProductID;
                        string NewLocalFileName = newProductID.ToString() + "/" + Path.GetFileName(item.ImageName);
                        DownloadFile(RemoteUrlBasePath + item.ImageName, System.Web.HttpContext.Current.Server.MapPath("~/Designer/Products/") + NewLocalFileName);

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


        private bool DownloadFile(string SourceURL, string DestinationBasePath)
        {
            Stream stream = null;
            MemoryStream memStream = new MemoryStream();
            try
            {
                WebRequest req = WebRequest.Create(SourceURL);
                WebResponse response = req.GetResponse();
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
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Clean up
                stream.Close();
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


}
