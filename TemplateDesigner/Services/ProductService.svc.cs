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
using TemplateDesignerModelTypes;
using System.Runtime.InteropServices;
using System.ServiceModel.Web;



namespace TemplateDesigner.Services
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductService" in code, svc and config file together.

	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class ProductService : IProductService
	{
		public List<MatchingSets> GetMatchingSets()
		{
			using (TemplateDesignerEntities oContext = new TemplateDesignerEntities())
			{
				return oContext.MatchingSets.ToList();
			}

		}


		public MatchingSets GetMatchingSetbyID(int MatchingSetID)
		{
			try
			{
				using (TemplateDesignerEntities oContext = new TemplateDesignerEntities())
			{
				return oContext.MatchingSets.Where(g => g.MatchingSetID == MatchingSetID).Single();
			}
			}
			catch (Exception ex)
			{
				
				throw ex;
			}
		}

		//TemplateDesignerEntities db = new TemplateDesignerEntities();

		public List<TemplateFonts> GetFontList(int ProductId, bool ReturnFontFiles, FontLoadModes mode )
		{
			List<TemplateFonts> lstFont = new List<TemplateFonts>();
			//ProductFonts objFonts = new ProductFonts();

			try
			{
				using (TemplateDesignerEntities db = new TemplateDesignerEntities())
				{

					List<TemplateFonts> lFonts = null;

					switch (mode)
					{
						case FontLoadModes.All:
							lFonts = db.TemplateFonts.Where( g => g.IsEnable == true).OrderBy(g => g.FontDisplayName).ToList();//.Where(g => g.ProductId == ProductId || g.ProductId == null);
							break;
						case FontLoadModes.SystemOnly:
							lFonts = db.TemplateFonts.Where(g => g.IsPrivateFont == false && g.IsEnable == true).OrderBy(g => g.FontDisplayName).ToList();
							break;
						case FontLoadModes.PrivateOnly:
                            lFonts = db.TemplateFonts.Where(g => g.IsPrivateFont == true && g.IsEnable == true).OrderBy(g => g.FontDisplayName).ToList();
							break;
						case FontLoadModes.SystemAndUsed:

						   lFonts=  db.sp_GetUsedFonts(ProductId).ToList();
							////////var selectedFontNames = db.TemplateObjects.Where(g => g.ProductID == ProductId).Select(g => g.FontName);

							////////var SelectedFonts = db.TemplateFonts.Where (g =>  selectedFontNames.Contains( g.FontName)).Select(g=> new TemplateFonts {ProductFontId = g.ProductFontId,  ProductId  = g.ProductId, FontName = g.FontName,FontDisplayName = g.FontDisplayName, FontFile = g.FontFile, DisplayIndex = g.DisplayIndex, IsPrivateFont = g.IsPrivateFont,IsEnable = g.IsEnable });

							//////////lFonts = db.TemplateFonts.Select(g=> new TemplateFonts {ProductFontId = g.ProductFontId,  ProductId  = g.ProductId, FontName = g.FontName,FontDisplayName = g.FontDisplayName, FontFile = g.FontFile, DisplayIndex = g.DisplayIndex, IsPrivateFont = g.IsPrivateFont,IsEnable = g.IsEnable }).ToList();

							////////var fonts = db.TemplateFonts.Where(g => g.IsPrivateFont == false).Select(g => new TemplateFonts { ProductFontId = g.ProductFontId, ProductId = g.ProductId, FontName = g.FontName, FontDisplayName = g.FontDisplayName, FontFile = g.FontFile, DisplayIndex = g.DisplayIndex, IsPrivateFont = g.IsPrivateFont, IsEnable = g.IsEnable });
								
							////////lFonts =     fonts.Union(SelectedFonts).OrderBy(g => g.FontDisplayName).ToList();
							break;
						default:
							break;
					}

					//printdesignBLL.Products.ProductFonts objFonts = new printdesignBLL.Products.ProductFonts();
					//objFonts = new printdesignBLL.Products.ProductFonts();
					//objFonts.LoadProductAndCommonFont(ProductId);
					foreach (var objFonts in lFonts)
					{
						//if (!objFonts.IsColumnNull("FontName") && !objFonts.IsColumnNull("FontDisplayName") && !objFonts.IsColumnNull("IsPrivateFont") && objFonts.FontName != "" && objFonts.FontDisplayName != "")
						//{

						if (ReturnFontFiles)
						{
							if (objFonts.IsPrivateFont == false)
							{
								//lstFont.Add(new ProductFonts { ProductFontId = objFonts.ProductFontId, ProductId = objFonts.ProductId, FontName = objFonts.FontName, FontDisplayName = objFonts.FontDisplayName, FontFile = "", IsPrivateFont = false });
								lstFont.Add(objFonts);
							}
							else if (objFonts.FontFile != null && objFonts.FontFile != "")
							{
								string FontFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "Designer\\PrivateFonts\\" + objFonts.FontFile;
								if (System.IO.File.Exists(FontFilePath))
								{
									System.IO.FileStream strmFont = new System.IO.FileStream(FontFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
									System.IO.BinaryReader brFont = new System.IO.BinaryReader(strmFont);
									long numBytes = new System.IO.FileInfo(FontFilePath).Length;
									byte[] buffFont = brFont.ReadBytes((int)numBytes);
									objFonts.FontBytes = buffFont;
									lstFont.Add(objFonts);
								}
							}
						}
						else
						{
							lstFont.Add(objFonts);
						}
					}
				}
			}


			catch (Exception ex)
			{
				AppCommon.LogException(ex);
				throw ex;
			}
			return lstFont;
		}


      
		public List<TemplateBackgroundImages> GetProductBackgroundImages(int ProductId)
		{

			//return null;

			try
			{
				if (ProductId != 0)
				{

					using (TemplateDesignerEntities db = new TemplateDesignerEntities())
					{

						db.ContextOptions.LazyLoadingEnabled = false;
						//printdesignBLL.Products.ProductBackgroundImages objBackground = new printdesignBLL.Products.ProductBackgroundImages();
						//objBackground.LoadByProductId(ProductId);

						var backgrounds = db.TemplateBackgroundImages.Where(g => g.ProductID == ProductId).ToList();


						string imgPath = System.AppDomain.CurrentDomain.BaseDirectory + ("Designer\\Products\\");
						string imgUrl = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
						Uri objUri = new Uri(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"));

						foreach (var objBackground in backgrounds)
						{
							if (objBackground.ImageName != null && objBackground.ImageName != "")
							{
								if (System.IO.File.Exists(imgUrl + objBackground.ImageName))
								{
									//string ImgExt = System.IO.Path.GetExtension(objBackground.ImageName).ToLower();
									//if (ImgExt == ".jpg" || ImgExt == ".png")
									//{

									//lstProductBackground.Add(new PrintFlow.Model.Products.ProductBackgroundImages { BackgroundImageName = ((!objBackground.IsColumnNull("Name")) ? objBackground.Name : ""), 

									//objBackground.BackgroundImageAbsolutePath = objUri.OriginalString + objBackground.ImageName, 
									//ImgRelativePath = new Uri(@"Designer/Products/" + objBackground.ImageName, UriKind.Relative), 
									//BackgroundImageRelativePath = "Designer/Products/" + objBackground.ImageName });

									objBackground.BackgroundImageAbsolutePath = objUri.OriginalString + objBackground.ImageName;
									//}
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
				AppCommon.LogException(ex);
				throw ex;
				//throw new Exception(ex.ToString());
			}

			return null;
		}


		public bool DeleteProductBackgroundImage(int ProductId, int BackgroundImageID)
		{
			try
			{

				using (TemplateDesignerEntities db = new TemplateDesignerEntities())
				{

					var obj = db.TemplateBackgroundImages.Where(g => g.ID == BackgroundImageID).Single();
					string sfilePath =  System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + obj.ImageName);
					db.DeleteObject(obj);

					//delete the actual image as well
					if (System.IO.File.Exists(sfilePath))
						System.IO.File.Delete(sfilePath);

					db.SaveChanges();

					return true;
				}
				
			}
			catch (Exception ex)
			{
				AppCommon.LogException(ex);
				throw ex;
			}
		}

		public List<TemplateColorStyles> GetColorStyle(int ProductId)
		{
			//List<TemplateColorStyles> lstColorStyle = new List<TemplateColorStyles>();
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
			{
				try
				{
					return db.TemplateColorStyles.Where(g => g.ProductID == ProductId || g.ProductID == null).ToList();

					//printdesignBLL.Products.TemplateColorStyles objColor = new printdesignBLL.Products.TemplateColorStyles();
					//objColor.LoadProductTemplateColorStyles(0);
					//while (!objColor.EOF)
					//{
					//    TemplateColorStyles objTemplateColorStyles = new TemplateColorStyles();
					//    objTemplateColorStyles.ColorName = ((!objColor.IsColumnNull("Name")) ? objColor.Name : "");
					//    objTemplateColorStyles.ProductID = objColor.ProductID;
					//    objTemplateColorStyles.PelleteID = objColor.PelleteID;
					//    objTemplateColorStyles.ColorC = ((!objColor.IsColumnNull("ColorC")) ? Convert.ToInt32(objColor.ColorC) : 0);
					//    objTemplateColorStyles.ColorM = ((!objColor.IsColumnNull("ColorM")) ? Convert.ToInt32(objColor.ColorM) : 0);
					//    objTemplateColorStyles.ColorY = ((!objColor.IsColumnNull("ColorY")) ? Convert.ToInt32(objColor.ColorY) : 0);
					//    objTemplateColorStyles.ColorK = ((!objColor.IsColumnNull("ColorK")) ? Convert.ToInt32(objColor.ColorK) : 0);
					//    objTemplateColorStyles.SpotColor = ((!objColor.IsColumnNull("SpotColor")) ? objColor.SpotColor : "");
					//    objTemplateColorStyles.IsSpotColor = ((!objColor.IsColumnNull("IsSpotColor")) ? objColor.IsSpotColor : false);
					//    lstColorStyle.Add(objTemplateColorStyles);
					//    objColor.MoveNext();
					//}
					//objColor = new printdesignBLL.Products.TemplateColorStyles();
					//objColor.LoadProductTemplateColorStyles(ProductId);
					//while (!objColor.EOF)
					//{
					//    TemplateColorStyles objTemplateColorStyles = new TemplateColorStyles();
					//    objTemplateColorStyles.ColorName = ((!objColor.IsColumnNull("Name")) ? objColor.Name : "");
					//    objTemplateColorStyles.ProductID = objColor.ProductID;
					//    objTemplateColorStyles.PelleteID = objColor.PelleteID;
					//    objTemplateColorStyles.ColorC = ((!objColor.IsColumnNull("ColorC")) ? Convert.ToInt32(objColor.ColorC) : 0);
					//    objTemplateColorStyles.ColorM = ((!objColor.IsColumnNull("ColorM")) ? Convert.ToInt32(objColor.ColorM) : 0);
					//    objTemplateColorStyles.ColorY = ((!objColor.IsColumnNull("ColorY")) ? Convert.ToInt32(objColor.ColorY) : 0);
					//    objTemplateColorStyles.ColorK = ((!objColor.IsColumnNull("ColorK")) ? Convert.ToInt32(objColor.ColorK) : 0);
					//    objTemplateColorStyles.SpotColor = ((!objColor.IsColumnNull("SpotColor")) ? objColor.SpotColor : "");
					//    objTemplateColorStyles.IsSpotColor = ((!objColor.IsColumnNull("IsSpotColor")) ? objColor.IsSpotColor : false);
					//    lstColorStyle.Add(objTemplateColorStyles);
					//    objColor.MoveNext();
					//}
				}
				catch (Exception ex)
				{
					AppCommon.LogException(ex);
					throw ex;
				}
			}
		}


		public string GetProductBackgroundImg(int ProductId, string BkImg, bool IsSide2, int PageNo)
		{
			string RetVal = "";// "AppData/Products/" + BkImg;
			if (PageNo < 1)
				PageNo = 1;

			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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


		public Templates GetProductById(int ProductId)
		{
			TemplateDesignerEntities db = new TemplateDesignerEntities();


			Templates dbProduct = null;
			Doc pdfDoc = new Doc();
			//Products objProduct = new Products();
			try
			{
				db.ContextOptions.LazyLoadingEnabled = false;

				dbProduct = db.Templates.Where(g => g.ProductID == ProductId).Single();
				if (dbProduct != null)
				{

                    if (dbProduct.Orientation == 2) //rotating the canvas in case of vert orientation
                    {
                        double tmp = dbProduct.PDFTemplateHeight.Value;
                        dbProduct.PDFTemplateHeight = dbProduct.PDFTemplateWidth;
                        dbProduct.PDFTemplateWidth = tmp;
                    }
					//objProduct.ProductId = dbProduct.ProductID;
					//objProduct.CustomerId = (!dbProduct.IsColumnNull("OfficeID")) ? dbProduct.OfficeID : 0;
					//objProduct.ProductName = (!dbProduct.IsColumnNull("ProductName")) ? dbProduct.ProductName : "";
					//objProduct.ProductCode = (!dbProduct.IsColumnNull("ProductCode")) ? dbProduct.ProductCode : "";
					//objProduct.Description = (!dbProduct.IsColumnNull("Description")) ? dbProduct.Description : "";
					//objProduct.ProductCategoryID = (!dbProduct.IsColumnNull("ProductCategoryID")) ? dbProduct.ProductCategoryID : 0;
					//objProduct.IsProductEditable = (!dbProduct.IsColumnNull("IsProductEditable")) ? dbProduct.IsProductEditable : false;
					//objProduct.BackgroundArtwork = (!dbProduct.IsColumnNull("BackgroundArtwork")) ? "Designer/Products/" + dbProduct.BackgroundArtwork : "";
					if (dbProduct.BackgroundArtwork != null)
						dbProduct.BackgroundArtwork = "Designer/Products/" + dbProduct.BackgroundArtwork;
					else
						dbProduct.BackgroundArtwork = "";

					//objProduct.IsFromBottomToTop = (!dbProduct.IsColumnNull("IsFromBottomToTop")) ? dbProduct.IsFromBottomToTop : false;
					//objProduct.IsDoubleSide = (!dbProduct.IsColumnNull("IsDoubleSide")) ? dbProduct.IsDoubleSide : false;
					//objProduct.IsUsePDFFile = (!dbProduct.IsColumnNull("IsUsePDFFile")) ? dbProduct.IsUsePDFFile : false;
					//objProduct.Side2BackgroundArtwork = (objProduct.IsDoubleSide == true && !dbProduct.IsColumnNull("Side2BackgroundArtwork")) ? "Designer/Products/" + dbProduct.Side2BackgroundArtwork : "";

					if (dbProduct.Side2BackgroundArtwork != null)
						dbProduct.Side2BackgroundArtwork = "Designer/Products/" + dbProduct.Side2BackgroundArtwork;
					else
						dbProduct.Side2BackgroundArtwork = "";

					if (dbProduct.IsUsePDFFile == true && dbProduct.LowResPDFTemplates != null)
					{
						XSettings.License = "393-927-439-276-6036-693";

						string PdfFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/") + "\\" + dbProduct.LowResPDFTemplates;
						if (System.IO.File.Exists(PdfFile))
						{
							pdfDoc.Read(PdfFile);
							dbProduct.PDFTemplateHeight = pdfDoc.Rect.Height;
							dbProduct.PDFTemplateWidth = pdfDoc.Rect.Width;
							dbProduct.BackgroundArtwork = "Designer/Products/BackgroundImages/tmp/" + GenerateProductBackground(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.LowResPDFTemplates.ToString()), 1, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/BackgroundImages/tmp/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.BackgroundArtwork), 1);
						}
						if (dbProduct.IsDoubleSide == true)
						{
							if (System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/") + "\\" + dbProduct.Side2LowResPDFTemplates))
								dbProduct.Side2BackgroundArtwork = "Designer/Products/BackgroundImages/tmp/" + GenerateProductBackground(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.Side2LowResPDFTemplates.ToString()), 1, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/BackgroundImages/tmp/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + dbProduct.Side2BackgroundArtwork), 1);
						}
					}
					else
					{
						if (dbProduct.PDFTemplateWidth == null)
							dbProduct.PDFTemplateWidth = 0;

						if (dbProduct.PDFTemplateHeight == null)
							dbProduct.PDFTemplateHeight = 0;

						//dbProduct.PDFTemplateWidth = (!dbProduct.IsColumnNull("PDFTemplateWidth")) ? dbProduct.PDFTemplateWidth : 0;
						//dbProduct.PDFTemplateHeight = (!dbProduct.IsColumnNull("PDFTemplateHeight")) ? dbProduct.PDFTemplateHeight : 0;
					}
				}
			}

			catch (Exception ex)
			{
				AppCommon.LogException(ex);
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
		/// Returns the list of fold lines for the product category and also outputs if fold lines are to be applied. if ApplyFoldLines is false then return list is null
		/// </summary>
		/// <param name="ProductCategoryID"></param>
		/// <returns></returns>
		public List<tbl_ProductCategoryFoldLines> GetFoldLinesByProductCategoryID(int ProductCategoryID, out bool ApplyFoldLines)
		{
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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
						//    item.FoldLineOffsetFromOrigin = AppCommon.MMToPoint(item.FoldLineOffsetFromOrigin.Value);
						//}
						return FoldLines.ToList();
					}
					else
						return null;
				}
				catch (Exception ex)
				{
					AppCommon.LogException(ex);
					throw ex;
				}
			}
		}


		public List<TemplateObjects> GetProductObjects(int ProductId, DesignerModes Mode)
		{
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
			{

				//List<ProductsObjects> lstProObject = new List<ProductsObjects>();
				try
				{
					db.ContextOptions.LazyLoadingEnabled = false;
					//printdesignBLL.Products.Objects dbObjects = new printdesignBLL.Products.Objects();
					//printdesignBLL.Products.Objects objObjectsValue = new printdesignBLL.Products.Objects();
					//dbObjects.LoadByProduct(ProductId);


					//dbObjects.Sort = "DisplayOrderTxtControl";
					//if (System.Web.HttpContext.Current.Session["objValueC"] != null)
					//    objObjectsValue = (printdesignBLL.Products.Objects)System.Web.HttpContext.Current.Session["objValueC"];

					var objectsList = db.TemplateObjects.Where(g => g.ProductID == ProductId);

					foreach (var dbObject in objectsList)
					{
						if (dbObject.ObjectType != null && dbObject.Name != null)
						{
							//ProductsObjects objObjects = new ProductsObjects();
							//objObjects.ObjectId = dbObject.ObjectID;
							//objObjects.ObjectType = dbObject.ObjectType;
							//objObjects.Name = dbObject.Name;


							if (dbObject.ContentString != null && (dbObject.ObjectType == 1 || dbObject.ObjectType == 2 || dbObject.ObjectType == 4))
								dbObject.ContentString = System.Web.HttpUtility.HtmlEncode(dbObject.ContentString);
							////two lines commented mz - else
							////    dbObject.ContentString = string.Empty;


							//if (dbObject.ObjectType == 1 || dbObject.ObjectType == 2 || dbObject.ObjectType == 4)
							//    objObjects.ContentString = (!dbObject.IsColumnNull("ContentString")) ? System.Web.HttpUtility.HtmlEncode(dbObject.ContentString) : "";
							//else
							//    objObjects.ContentString = (!dbObject.IsColumnNull("ContentString")) ? System.Web.HttpUtility.HtmlEncode(dbObject.ContentString) : "";

							//objObjects.PageNo = (!dbObject.IsColumnNull("PageNo") && dbObject.PageNo != 0) ? dbObject.PageNo : 1;
							if (dbObject.PageNo == null)
								dbObject.PageNo = 1;

							//objObjects.IsEditable = (!dbObject.IsColumnNull("IsEditable")) ? dbObject.IsEditable : false;
							//objObjects.IsHidden = (!dbObject.IsColumnNull("IsHidden")) ? dbObject.IsHidden : false;
							//objObjects.IsMandatory = (!dbObject.IsColumnNull("IsMandatory")) ? dbObject.IsMandatory : false;
							//objObjects.PositionX = (!dbObject.IsColumnNull("PositionX")) ? dbObject.PositionX : 0;
							//objObjects.PositionY = (!dbObject.IsColumnNull("PositionY")) ? dbObject.PositionY : 0;
							//objObjects.MaxHeight = (!dbObject.IsColumnNull("MaxHeight")) ? dbObject.MaxHeight : 0;
							//objObjects.MaxWidth = (!dbObject.IsColumnNull("MaxWidth")) ? dbObject.MaxWidth : 0;
							//objObjects.MaxCharacters = (!dbObject.IsColumnNull("MaxCharacters")) ? dbObject.MaxCharacters : 0;
							//objObjects.RotationAngle = (!dbObject.IsColumnNull("RotationAngle")) ? dbObject.RotationAngle : 0;
							//objObjects.IsFontCustom = (!dbObject.IsColumnNull("IsFontCustom")) ? dbObject.IsFontCustom : false;
							//objObjects.IsFontNamePrivate = (!dbObject.IsColumnNull("IsFontNamePrivate")) ? dbObject.IsFontNamePrivate : false;
							//objObjects.FontName = (!dbObject.IsColumnNull("FontName")) ? dbObject.FontName : "";
							//objObjects.FontSize = (!dbObject.IsColumnNull("FontSize")) ? dbObject.FontSize : 0;
							//objObjects.FontStyleID = (!dbObject.IsColumnNull("FontStyleID")) ? dbObject.FontStyleID : 0;

							//objObjects.IsBold = (!dbObject.IsColumnNull("IsBold")) ? dbObject.IsBold : false;
							//objObjects.IsItalic = (!dbObject.IsColumnNull("IsItalic")) ? dbObject.IsItalic : false;
							//objObjects.Allignment = (!dbObject.IsColumnNull("Allignment")) ? dbObject.Allignment : 0;
							//objObjects.VAllignment = (!dbObject.IsColumnNull("VAlignment")) ? dbObject.VAlignment : 0;
							//objObjects.Indent = (!dbObject.IsColumnNull("Indent")) ? dbObject.Indent : 0;
							//objObjects.IsUnderlinedText = (!dbObject.IsColumnNull("IsUnderlinedText")) ? dbObject.IsUnderlinedText : false;
							//objObjects.ContentCaseType = (!dbObject.IsColumnNull("ContentCaseType")) ? dbObject.ContentCaseType : 0;
							//objObjects.ColorType = (!dbObject.IsColumnNull("ColorType")) ? dbObject.ColorType : 0;
							//objObjects.ColorStyleID = (!dbObject.IsColumnNull("ColorStyleID")) ? dbObject.ColorStyleID : 0;
							//objObjects.PalleteID = (!dbObject.IsColumnNull("PalleteID")) ? dbObject.PalleteID : 0;
							//objObjects.ColorName = (!dbObject.IsColumnNull("ColorName")) ? dbObject.ColorName : "";
							//objObjects.ColorC = (!dbObject.IsColumnNull("ColorC")) ? dbObject.ColorC : 0;
							//objObjects.ColorM = (!dbObject.IsColumnNull("ColorM")) ? dbObject.ColorM : 0;
							//objObjects.ColorY = (!dbObject.IsColumnNull("ColorY")) ? dbObject.ColorY : 0;
							//objObjects.ColorK = (!dbObject.IsColumnNull("ColorK")) ? dbObject.ColorK : 0;
							//objObjects.Tint = (!dbObject.IsColumnNull("Tint")) ? dbObject.Tint : 0;
							//objObjects.IsSpotColor = (!dbObject.IsColumnNull("IsSpotColor")) ? dbObject.IsSpotColor : false;
							//objObjects.SpotColorName = (!dbObject.IsColumnNull("SpotColorName")) ? dbObject.SpotColorName : "";


							//objObjects.ProductID = dbObject.ProductID;
							//objObjects.DisplayOrderPdf = (!dbObject.IsColumnNull("DisplayOrderPdf")) ? dbObject.DisplayOrderPdf : 0;
							//objObjects.DisplayOrderTxtControl = (!dbObject.IsColumnNull("DisplayOrderTxtControl")) ? dbObject.DisplayOrderTxtControl : 0;
							//objObjects.IsRequireNumericValue = (!dbObject.IsColumnNull("IsRequireNumericValue")) ? dbObject.IsRequireNumericValue : false;
							//objObjects.IsSide2Object = (!dbObject.IsColumnNull("isSide2Object")) ? dbObject.IsSide2Object : false;
							//objObjects.LineSpacing = (!dbObject.IsColumnNull("LineSpacing")) ? dbObject.LineSpacing : 0;
							//objObjects.ProductPageId = (!dbObject.IsColumnNull("ProductPageId")) ? dbObject.ProductPageId : 0;

							//objObjects.ParentId = (!dbObject.IsColumnNull("ParentId")) ? dbObject.ParentId : 0;
							//objObjects.OffsetX = (!dbObject.IsColumnNull("OffsetX")) ? dbObject.OffsetX : 0;
							//objObjects.OffsetY = (!dbObject.IsColumnNull("OffsetY")) ? dbObject.OffsetY : 0;
							//objObjects.IsNewLine = (!dbObject.IsColumnNull("IsNewLine")) ? dbObject.IsNewLine : false;

							//dbObject.TCtlName = "";
							//dbObject.ExField1 = "";
							//dbObject.ExField2 = "";


							//enable this for end users mode where address information is automatically replaced
							//if (Mode != 2)
							//    dbObject.ContentString = AppCommon.ReplaceObjectContentString(dbObject.ContentString, System.Web.HttpContext.Current);


							//get it working to see what happens
							////if (objObjectsValue != null && objObjectsValue.RowCount > 0)
							////{
							////    objObjectsValue.Rewind();
							////    while (!objObjectsValue.EOF)
							////    {

							////        bool IsSide2Obj = (!objObjectsValue.IsColumnNull("IsSide2Object")) ? objObjectsValue.IsSide2Object : false;
							////        if (objObjects.ObjectType != 4)
							////        {
							////            if (objObjects.Name == objObjectsValue.Name && IsSide2Obj == objObjects.IsSide2Object)
							////            {
							////                objObjects.ContentString = objObjectsValue.ContentString;
							////            }
							////        }
							////        objObjectsValue.MoveNext();
							////    }
							////}

							//lstProObject.Add(objObjects);
						}
						// dbObject.MoveNext();
					}

					return objectsList.ToList();
				}
				catch (Exception ex)
				{
					AppCommon.LogException(ex);
					throw ex;
					// throw new Exception(ex.ToString());
				}

				return null;

			}
		}
		public string SaveObjectsAndGenratePDF(int ProductId, string Bkimg, string Bkimg2, ObservableCollection<TemplateObjects> objObjectsList, DesignerModes Mode, SaveOperationTypes SaveOperationType)
		{

            
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
			{
				string RetVal = "";
				bool IsDoublSide = false;
				try
				{
					System.Web.HttpContext.Current.Session["PDFFileS"] = null;
					System.Web.HttpContext.Current.Session["PDFFile"] = null;
					System.Web.HttpContext.Current.Session["PDFFileSide2S"] = null;
					System.Web.HttpContext.Current.Session["PDFFileSide2"] = null;
					System.Web.HttpContext.Current.Session["objValue"] = null;
					System.Web.HttpContext.Current.Session["PDFFileAdmin"] = null;
					System.Web.HttpContext.Current.Session["PDFFileSide2Admin"] = null;
					System.Web.HttpContext.Current.Session["objValueC"] = null;
					if (ProductId != 0)
					{
						

							//printdesignBLL.Products.Products dbProduct = new printdesignBLL.Products.Products();
							//dbProduct.LoadByPrimaryKey(ProductId);

							var dbProduct = db.Templates.Where(g => g.ProductID == ProductId).SingleOrDefault();

							if (dbProduct != null)
							{
								if (Bkimg != "")
								{
									dbProduct.BackgroundArtwork = Bkimg;
								}
								if (Bkimg2 != "")
									dbProduct.Side2BackgroundArtwork = Bkimg2;

								db.SaveChanges();
								db.AcceptAllChanges();

								//printdesignBLL.Products.Objects dbObjects = new printdesignBLL.Products.Objects();
								//dbObjects.LoadByProductID(dbProduct.ProductID);
								//dbObjects.DeleteAll();
								//dbObjects.Save();

								foreach (TemplateObjects c in db.TemplateObjects.Where(g => g.ProductID == dbProduct.ProductID))
								{
									db.DeleteObject(c);
								}
								db.SaveChanges();
								db.AcceptAllChanges();




								//dbObjects = new printdesignBLL.Products.Objects();
								//printdesignBLL.Products.Objects.SaveObjects(dbProduct.ProductID, objObjectsList);

								foreach (var oObject in objObjectsList)
								{
									oObject.MarkAsAdded();
									oObject.ProductID = dbProduct.ProductID;
									db.TemplateObjects.AddObject(oObject);
								}
								db.SaveChanges();


								
								

									//printdesignBLL.Products.Products objPro = new printdesignBLL.Products.Products();
									///System.Web.HttpContext.Current.Session["PDFFileAdmin"] = objPro.ShowPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/AppData/Products/"), ref dbObjects, true, false, false, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/AppData/PrivateFonts/"), false);
									byte[] PDFFileAdmin = GenrateStationeryProductPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), false, false, false, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), false,false);
									byte[] PDFFileAdminSide2 = null;

									System.Web.HttpContext.Current.Session["PDFFileAdmin"] = PDFFileAdmin;
									if (dbProduct.IsDoubleSide)
									{

										System.Web.HttpContext.Current.Session["PDFFileSide2Admin"] = PDFFileAdminSide2 = GenrateStationeryProductPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), false, false, true, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), false,false);
									}

									string targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");

									if (Directory.Exists(System.IO.Path.Combine(targetFolder, dbProduct.ProductID.ToString() + "/")) == false)
									{
										Directory.CreateDirectory(System.IO.Path.Combine(targetFolder, dbProduct.ProductID.ToString() + "/"));
									}

									//generating preview image of side1/page1
									GenerateTemplatePagePreview(PDFFileAdmin, targetFolder, dbProduct.ProductID.ToString() + "/p1", dbProduct.CuttingMargin.Value);
									//generating preview image of side2/page2
									if (dbProduct.IsDoubleSide)
									{
										GenerateTemplatePagePreview(PDFFileAdminSide2, targetFolder, dbProduct.ProductID.ToString() + "/p2", dbProduct.CuttingMargin.Value);
									}

                                    //redrawing objects to show the hiddeen objects for thumbnail
                                    PDFFileAdmin = GenrateStationeryProductPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), false, false, false, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), false, true);

									dbProduct.Thumbnail = GenerateTemplateThumbnail(PDFFileAdmin, targetFolder, dbProduct.ProductID.ToString() + "/TemplateThumbnail", dbProduct.CuttingMargin.Value);

									db.SaveChanges();



									if (SaveOperationType == SaveOperationTypes.SaveGeneratePDFAttachMode)
									{
										//special working for attaching the PDF
									}





								RetVal = "Success";
							}
						//}
						//else if (Mode == 1 || Mode == 0)
						//{
							//int UsrId = 0;
							//string UserName = "";
							//if (System.Web.HttpContext.Current.Session["GlobalData"] != null)
							//{
							//    AppGlobalData globalData = (AppGlobalData)System.Web.HttpContext.Current.Session["GlobalData"];
							//    if (globalData != null && globalData.user != null && globalData.user.RowCount > 0)
							//    {
							//        UsrId = globalData.user.UserID;
							//        UserName = (globalData.user.IsColumnNull("Username")) ? globalData.user.Username : "";
							//    }
							//}
							//if (UsrId != 0)
							//{
							//    printdesignBLL.Products.Products dbProduct = new printdesignBLL.Products.Products();
							//    dbProduct.LoadByPrimaryKey(ProductId);
							//    if (dbProduct.RowCount > 0)
							//    {
							//        if (dbProduct.IsColumnNull("PTempId") || dbProduct.PTempId == 0)
							//        {
							//            dbProduct = new printdesignBLL.Products.Products();
							//            int PId = dbProduct.CopyUserProduct(UsrId, ProductId, "John");
							//            dbProduct = new printdesignBLL.Products.Products();
							//            dbProduct.LoadByPrimaryKey(PId);
							//        }
							//    }
							//    if (dbProduct.RowCount > 0)
							//    {
							//        IsDoublSide = (!dbProduct.IsColumnNull("IsDoubleSide")) ? dbProduct.IsDoubleSide : false;
							//        if (Bkimg != "")
							//        {
							//            dbProduct.BackgroundArtwork = Bkimg;
							//        }
							//        if (!dbProduct.IsColumnNull("IsDisabled") && dbProduct.IsDisabled)
							//        {
							//            if (Bkimg2 != "")
							//                dbProduct.Side2BackgroundArtwork = Bkimg2;
							//        }
							//        dbProduct.Save();
							//        printdesignBLL.Products.Objects dbObjects = new printdesignBLL.Products.Objects();
							//        dbObjects = new printdesignBLL.Products.Objects();
							//        dbObjects.LoadByProductID(dbProduct.ProductID);
							//        dbObjects.DeleteAll();
							//        dbObjects.Save();
							//        dbObjects = new printdesignBLL.Products.Objects();
							//        printdesignBLL.Products.Objects.SaveObjects(dbProduct.ProductID, objObjectsList);
							//        printdesignBLL.Products.Products product = new printdesignBLL.Products.Products();
							//        System.Web.HttpContext.Current.Session["PDFFileS"] = product.GenrateStationeryProductPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), true, false, false, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), true);
							//        product = new printdesignBLL.Products.Products();
							//        System.Web.HttpContext.Current.Session["PDFFile"] = product.GenrateStationeryProductPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), true, false, false, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), false);
							//        if (IsDoublSide)
							//        {
							//            product = new printdesignBLL.Products.Products();
							//            System.Web.HttpContext.Current.Session["PDFFileSide2S"] = product.GenrateStationeryProductPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), true, false, true, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), true);
							//            product = new printdesignBLL.Products.Products();
							//            System.Web.HttpContext.Current.Session["PDFFileSide2"] = product.GenrateStationeryProductPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), true, false, true, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), false);
							//        }
							//        product = new printdesignBLL.Products.Products();
							//        RetVal = printdesignBLL.CMS.Pages.PageRedirect.GetUrlById2(21, int.Parse(System.Web.HttpContext.Current.Session["WebSiteId"].ToString())) + "&pid=" + dbProduct.ProductID.ToString() + "&ProductSide=1&mode=1";
							//    }

							//}
						//}
						//else if (Mode == 0)
						//{
							//    printdesignBLL.Products.Products dbProduct = new printdesignBLL.Products.Products();
							//    dbProduct.LoadByPrimaryKey(ProductId);
							//    if (dbProduct.RowCount > 0)
							//    {
							//        IsDoublSide = (!dbProduct.IsColumnNull("IsDoubleSide")) ? dbProduct.IsDoubleSide : false;
							//        printdesignBLL.Products.Objects dbObjects = new printdesignBLL.Products.Objects();
							//        dbObjects.LoadByProductID(dbProduct.ProductID);
							//        while (!dbObjects.EOF)
							//        {
							//            bool IsSide2Obj = false;
							//            if (IsDoublSide)
							//                IsSide2Obj = (!dbObjects.IsColumnNull("IsSide2Object")) ? dbObjects.IsSide2Object : false;
							//            if (dbObjects.ObjectType != 4)
							//            {
							//                foreach (ProductsObjects objObject in objObjectsList)
							//                {
							//                    if (objObject.ObjectType != 4)
							//                    {
							//                        if (objObject.Name == dbObjects.Name && IsSide2Obj == objObject.IsSide2Object)
							//                        {
							//                            dbObjects.ContentString = objObject.ContentString;
							//                        }
							//                    }
							//                }
							//            }
							//            dbObjects.MoveNext();
							//        }
							//        printdesignBLL.Products.Products product = new printdesignBLL.Products.Products();
							//        System.Web.HttpContext.Current.Session["PDFFileS"] = product.ShowPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), ref dbObjects, true, false, false, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), true);
							//        product = new printdesignBLL.Products.Products();
							//        System.Web.HttpContext.Current.Session["PDFFile"] = product.ShowPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), ref dbObjects, true, false, false, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), false);
							//        if (IsDoublSide)
							//        {
							//            product = new printdesignBLL.Products.Products();
							//            System.Web.HttpContext.Current.Session["PDFFileSide2S"] = product.ShowPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), ref dbObjects, true, false, true, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), true);
							//            product = new printdesignBLL.Products.Products();
							//            System.Web.HttpContext.Current.Session["PDFFileSide2"] = product.ShowPDF(dbProduct.ProductID, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"), ref dbObjects, true, false, true, System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/", System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/PrivateFonts/"), false);
							//        }
							//        System.Web.HttpContext.Current.Session["objValueC"] = dbObjects;
							//        product = new printdesignBLL.Products.Products();
							//        RetVal = printdesignBLL.CMS.Pages.PageRedirect.GetUrlById2(21, int.Parse(System.Web.HttpContext.Current.Session["WebSiteId"].ToString())) + "&pid=" + dbProduct.ProductID.ToString() + "&ProductSide=1&mode=1";
							//    }


						
					}
				}
				catch (Exception ex)
				{

                    
					AppCommon.LogException(ex);
					throw ex;
					//RetVal = "Error";
					// throw new Exception(ex.ToString());
				}
				return RetVal;
			}
		}

		# region "PDF Generation"

		public byte[] GenrateStationeryProductPDF(int ProductID, string ProductFolder, bool ShowHighResPDF, bool prePrintPDF, bool showSide2PDF, string logoPath, string fontPath, bool DrawBKText, bool DrawHiddenObjects)
		{

			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
			{
				Doc doc = new Doc();
				try
				{
					bool ObjSide2 = false;

					var objProduct = db.Templates.Where(g => g.ProductID == ProductID).Single();
					//this.Where.ProductID.Value = ProductID;
					//this.Query.Load();

					//XSettings.License = "322-594-815";
					XSettings.License = "810-031-225-276-0715-601";


					//if (objProduct.IsFromBottomToTop != null)
					//{
					//    if (this.IsFromBottomToTop == false)
					//        doc.TopDown = true;
					//    else
					//        doc.TopDown = false;
					//}
					//else
					//    doc.TopDown = true;

					doc.TopDown = true;

					try
					{
						if (objProduct.IsUsePDFFile != null && objProduct.IsUsePDFFile == true)
						{
							if (objProduct.IsDoubleSide == true && showSide2PDF == true)
							{
								ObjSide2 = true;
								if (prePrintPDF == true)
								{
									doc.Read(ProductFolder + objProduct.Side2PrePrintPDFTemplates);
								}
								else
								{
									if (ShowHighResPDF == true)
										doc.Read(ProductFolder + objProduct.Side2PDFTemplate);
									else
										doc.Read(ProductFolder + objProduct.Side2LowResPDFTemplates);
								}
							}
							else
							{
								ObjSide2 = false;
								if (prePrintPDF == true)
								{
									doc.Read(ProductFolder + objProduct.PrePrintPDFTemplates);
								}
								else
								{
									if (ShowHighResPDF == true)
										doc.Read(ProductFolder + objProduct.PDFTemplate);
									else
										doc.Read(ProductFolder + objProduct.LowResPDFTemplates);
								}
							}
						}
						else if (objProduct.IsUseBackGroundColor.Value == true || objProduct.IsUseSide2BackGroundColor.Value == true)
						{

							doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
							doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
							doc.AddPage();

							LoadBackColor(ref doc, objProduct, showSide2PDF);

						}
						else
						{
							if (objProduct.IsMultiPage != null && objProduct.IsMultiPage == true && objProduct.TotelPage != null && objProduct.TotelPage > 1)
							{
								for (int i = 1; i <= objProduct.TotelPage; i++)
								{
									doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
									doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
									doc.AddPage();
								}
							}
							else
							{
								doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
								doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
								doc.AddPage();

								LoadBackGroundArtWork(ref doc, objProduct, showSide2PDF, ProductFolder);
							}
						}
					}
					catch (Exception ex)
					{
                        AppCommon.LogException(ex);
						throw ex;
					}
					//  ShowGrid(ref doc);

					if (objProduct.IsDoubleSide == true && showSide2PDF == true)
					{

						//LoadMultiPageBackGroundArtWork(ref doc, objProduct.ProductID, true, ProductFolder);
						ObjSide2 = true;
					}
					else
					{
						//LoadMultiPageBackGroundArtWork(ref doc, objProduct.ProductID, false, ProductFolder);
						ObjSide2 = false;
					}

					//new function


                    

					
					double YFactor = 0;
					double XFactor = 0;
					int RowCount = 0;



					//load parent objects

					//printdesignBLL.Products.Objects objObjects = new Objects();
					//objObjects.Where.ProductID.Value = this.ProductID;
					//objObjects.Where.IsSide2Object.Value = showSide2PDF;
					//objObjects.Where.ParentId.Value = 0;
					//objObjects.Query.Load();
					//objObjects.Sort = "DisplayOrderPdf";

                    List<TemplateObjects> oParentObjects = null;

                    if (DrawHiddenObjects)
                    {
                        oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.isSide2Object == showSide2PDF && g.ParentId == 0).OrderBy(g => g.DisplayOrderPdf).ToList();

                    }
                    else
                    {
                        oParentObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.isSide2Object == showSide2PDF && g.ParentId == 0 && g.IsHidden == DrawHiddenObjects).OrderBy(g => g.DisplayOrderPdf).ToList();
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
							//loading child objects

							//printdesignBLL.Products.Objects objObjectsChld = new Objects();
							//objObjectsChld.Where.ProductID.Value = this.ProductID;
							//objObjectsChld.Where.IsSide2Object.Value = showSide2PDF;
							//objObjectsChld.Where.ParentId.Value = objObjects.ObjectID;
							//objObjectsChld.Query.Load();
							//objObjectsChld.Sort = "DisplayOrderPdf";

							var ChildObjects = db.TemplateObjects.Where(g => g.ProductID == objProduct.ProductID && g.isSide2Object == showSide2PDF && g.ParentId == objObjects.ObjectID).OrderBy(g => g.DisplayOrderPdf);

							int VAlign = 1, HAlign = 1;
							//if (!objObjects.IsColumnNull("Allignment"))
							HAlign = objObjects.Allignment;
							//if (!objObjects.IsColumnNull("VAlignment"))
							VAlign = objObjects.VAllignment;
							if (ChildObjects.Count() == 0)
							{
								double currentX = objObjects.PositionX, currentY = objObjects.PositionY;
								//ADDSingleLineText(ref objObjects, ref doc, false, objObjects.ContentString, fontPath + "/");
								//if (HAlign == 2)
									//currentX = objObjects.PositionX - (objObjects.MaxWidth / 2);
								//else if (HAlign == 3)
									//currentX = objObjects.PositionX - objObjects.MaxWidth;

								if (VAlign == 1 || VAlign == 2)
									currentY = objObjects.PositionY + objObjects.MaxHeight;

								ADDTextObject(objObjects, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth, objObjects.MaxHeight);
							}
							else
							{

								#region  left , top
								if (HAlign == 1 && VAlign == 1)
								{
									double currentX = objObjects.PositionX, currentY = objObjects.PositionY, objOffsetX = 0, objOffsetY = 0, LrgHeight = 0;


									if (objObjects.ContentString != "" && !objObjects.IsHidden)
									{
										objOffsetX = objObjects.OffsetX; //(!objObjects.IsColumnNull("OffsetX")) ? objObjects.OffsetX : 0;
										objOffsetY = objObjects.OffsetY; //  (!objObjects.IsColumnNull("OffsetY")) ? objObjects.OffsetY : 0;
										currentX += objOffsetX;
										currentY += objOffsetY;
										ADDTextObject(objObjects, ref doc, fontPath, currentX, currentY + objObjects.MaxHeight, objObjects.MaxWidth, objObjects.MaxHeight);
										LrgHeight = objObjects.MaxHeight;
										currentX += objObjects.MaxWidth;
									}

									foreach (var objObjectsChld in ChildObjects)
									{
										if (objObjectsChld.ContentString != "" && !objObjectsChld.IsHidden)
										{
											objOffsetX = objObjectsChld.OffsetX;// (!objObjectsChld.IsColumnNull("OffsetX")) ? objObjectsChld.OffsetX : 0;
											objOffsetY = objObjectsChld.OffsetY;// (!objObjectsChld.IsColumnNull("OffsetY")) ? objObjectsChld.OffsetY : 0;
											currentX += objOffsetX;
											currentY += objOffsetY;
											//if (!objObjectsChld.IsColumnNull("IsNewLine"))
											//{
											if (objObjectsChld.IsNewLine)
											{
												currentX = objObjects.PositionX + objOffsetX;
												currentY += objOffsetY + LrgHeight;
											}
											//}
											ADDTextObject(objObjectsChld, ref doc, fontPath, currentX, currentY + objObjectsChld.MaxHeight, objObjectsChld.MaxWidth, objObjectsChld.MaxHeight);
											currentX += objObjectsChld.MaxWidth;
											if (objObjectsChld.MaxHeight > LrgHeight)
												LrgHeight = objObjectsChld.MaxHeight;
										}
										// objObjectsChld.MoveNext();
									}
								}
								#endregion
								#region  right , top
								else if (HAlign == 3 && VAlign == 1)
								{
									double currentX = objObjects.PositionX, currentY = objObjects.PositionY, objOffsetX = 0, objOffsetY = 0, LrgHeight = 0;

									//double MxWidth = 0, MxHeight = 0;
									//if (!objObjects.IsColumnNull("ContentString") && objObjects.ContentString != "" && !objObjects.IsColumnNull("IsHidden") && !objObjects.IsHidden)
									//{
									//    MxWidth = objObjects.MaxWidth;
									//    MxHeight = objObjects.MaxHeight;
									//}
									//GetMaxWdHtObject(objObjects.ObjectID,ref MxWidth, ref MxHeight);
									//currentX += MxWidth;
									if (objObjects.ContentString != "" && !objObjects.IsHidden)
									{
										objOffsetX = objObjects.OffsetX;// (!objObjects.IsColumnNull("OffsetX")) ? objObjects.OffsetX : 0;
										objOffsetY = objObjects.OffsetY;// (!objObjects.IsColumnNull("OffsetY")) ? objObjects.OffsetY : 0;
										currentX -= objOffsetX;
										currentY += objOffsetY;
										currentX -= objObjects.MaxWidth;
										ADDTextObject(objObjects, ref doc, fontPath, currentX, currentY + objObjects.MaxHeight, objObjects.MaxWidth, objObjects.MaxHeight);
										LrgHeight = objObjects.MaxHeight;
									}

									foreach (var objObjectsChld in ChildObjects)
									{
										if (objObjectsChld.ContentString != "" && !objObjectsChld.IsHidden)
										{
											objOffsetX = objObjectsChld.OffsetX;// (!objObjectsChld.IsColumnNull("OffsetX")) ? objObjectsChld.OffsetX : 0;
											objOffsetY = objObjectsChld.OffsetY;// (!objObjectsChld.IsColumnNull("OffsetY")) ? objObjectsChld.OffsetY : 0;
											currentX -= objOffsetX;
											currentY += objOffsetY;
											//if (!objObjectsChld.IsColumnNull("IsNewLine"))
											//{
											if (objObjectsChld.IsNewLine)
											{
												currentX = (objObjects.PositionX) - objOffsetX;
												currentY += objOffsetY + LrgHeight;
											}
											//}
											currentX -= objObjectsChld.MaxWidth;
											ADDTextObject(objObjectsChld, ref doc, fontPath, currentX, currentY + objObjectsChld.MaxHeight, objObjectsChld.MaxWidth, objObjectsChld.MaxHeight);

											if (objObjectsChld.MaxHeight > LrgHeight)
												LrgHeight = objObjectsChld.MaxHeight;
										}
										//objObjectsChld.MoveNext();
									}
								}
								#endregion
								#region  Center , top
								else if (HAlign == 2 && VAlign == 1)
								{
									double currentX = objObjects.PositionX, currentY = objObjects.PositionY, objOffsetX = 0, objOffsetY = 0, LrgHeight = 0;

									double MxLineWidth = 0;
									int CrtIdx = 0;

									if (objObjects.ContentString != "" && !objObjects.IsHidden)
									{
										objOffsetX = objObjects.OffsetX;// (!objObjects.IsColumnNull("OffsetX")) ? objObjects.OffsetX : 0;
										objOffsetY = objObjects.OffsetY; //(!objObjects.IsColumnNull("OffsetY")) ? objObjects.OffsetY : 0;
										currentX += objOffsetX;
										currentY += objOffsetY;

										//if (!objObjectsChld.EOF && !objObjectsChld.IsColumnNull("IsNewLine"))
										if (ChildObjects.Count() > 0)
										{
											if (ChildObjects.First().IsNewLine)
											{
												MxLineWidth = 0;
											}
											else
												MxLineWidth = GetMaxLineWidth(CrtIdx, ChildObjects.ToList());
										}
										else
											MxLineWidth = GetMaxLineWidth(CrtIdx, ChildObjects.ToList());


										currentX = objOffsetX + (objObjects.PositionX - ((MxLineWidth + objObjects.MaxWidth) / 2));
										ADDTextObject(objObjects, ref doc, fontPath, currentX, currentY + objObjects.MaxHeight, objObjects.MaxWidth, objObjects.MaxHeight);
										LrgHeight = objObjects.MaxHeight;
										currentX += objObjects.MaxWidth;
									}

									foreach (var objObjectsChld in ChildObjects)
									{
										if (objObjectsChld.ContentString != "" && !objObjectsChld.IsHidden)
										{
											objOffsetX = objObjectsChld.OffsetX;// (!objObjectsChld.IsColumnNull("OffsetX")) ? objObjectsChld.OffsetX : 0;
											objOffsetY = objObjectsChld.OffsetY;// (!objObjectsChld.IsColumnNull("OffsetY")) ? objObjectsChld.OffsetY : 0;
											currentX += objOffsetX;
											currentY += objOffsetY;
											//if (!objObjectsChld.IsColumnNull("IsNewLine"))
											//{
											if (objObjectsChld.IsNewLine)
											{
												MxLineWidth = GetMaxLineWidth(CrtIdx, ChildObjects.ToList());
												currentX = objOffsetX + (objObjects.PositionX - (MxLineWidth / 2));
												currentY += objOffsetY + LrgHeight;
											}
											//}
											ADDTextObject(objObjectsChld, ref doc, fontPath, currentX, currentY + objObjectsChld.MaxHeight, objObjectsChld.MaxWidth, objObjectsChld.MaxHeight);
											currentX += objObjectsChld.MaxWidth;
											if (objObjectsChld.MaxHeight > LrgHeight)
												LrgHeight = objObjectsChld.MaxHeight;
										}
										CrtIdx++;
										//objObjectsChld.MoveNext();
									}
								}
								#endregion
								#region  left , bottom
								else if (HAlign == 1 && VAlign == 3)
								{
									double currentX = objObjects.PositionX, currentY = objObjects.PositionY, objOffsetX = 0, objOffsetY = 0, LrgHeight = 0;


									if (objObjects.ContentString != "" && !objObjects.IsHidden)
									{
										objOffsetX = objObjects.OffsetX;// (!objObjects.IsColumnNull("OffsetX")) ? objObjects.OffsetX : 0;
										objOffsetY = objObjects.OffsetY;// (!objObjects.IsColumnNull("OffsetY")) ? objObjects.OffsetY : 0;
										currentX += objOffsetX;
										currentY -= objOffsetY;
										ADDTextObject(objObjects, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth, objObjects.MaxHeight);
										LrgHeight = objObjects.MaxHeight;
										currentX += objObjects.MaxWidth;
									}

									foreach (var objObjectsChld in ChildObjects)
									{
										if (objObjectsChld.ContentString != "" && !objObjectsChld.IsHidden)
										{
											objOffsetX = objObjectsChld.OffsetX;// (!objObjectsChld.IsColumnNull("OffsetX")) ? objObjectsChld.OffsetX : 0;
											objOffsetY = objObjectsChld.OffsetY;// (!objObjectsChld.IsColumnNull("OffsetY")) ? objObjectsChld.OffsetY : 0;
											currentX += objOffsetX;
											currentY -= objOffsetY;
											//if (!objObjectsChld.IsColumnNull("IsNewLine"))
											//{
											if (objObjectsChld.IsNewLine)
											{
												currentX = objObjects.PositionX + objOffsetX;
												currentY -= objOffsetY - LrgHeight;
											}
											//}
											ADDTextObject(objObjectsChld, ref doc, fontPath, currentX, currentY + objObjectsChld.MaxHeight, objObjectsChld.MaxWidth, objObjectsChld.MaxHeight);
											currentX += objObjectsChld.MaxWidth;
											if (objObjectsChld.MaxHeight > LrgHeight)
												LrgHeight = objObjectsChld.MaxHeight;
										}
										//objObjectsChld.MoveNext();
									}
								}
								#endregion
								#region  right , bottom
								else if (HAlign == 3 && VAlign == 3)
								{
									double currentX = objObjects.PositionX, currentY = objObjects.PositionY, objOffsetX = 0, objOffsetY = 0, LrgHeight = 0;


									if (objObjects.ContentString != "" && !objObjects.IsHidden)
									{
										objOffsetX = objObjects.OffsetX;// (!objObjects.IsColumnNull("OffsetX")) ? objObjects.OffsetX : 0;
										objOffsetY = objObjects.OffsetY;// (!objObjects.IsColumnNull("OffsetY")) ? objObjects.OffsetY : 0;
										currentX -= objOffsetX;
										currentY -= objOffsetY;
										currentX -= objObjects.MaxWidth;
										ADDTextObject(objObjects, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth, objObjects.MaxHeight);
										LrgHeight = objObjects.MaxHeight;
									}
									foreach (var objObjectsChld in ChildObjects)
									{
										if (objObjectsChld.ContentString != "" && !objObjectsChld.IsHidden)
										{
											objOffsetX = objObjectsChld.OffsetX;// (!objObjectsChld.IsColumnNull("OffsetX")) ? objObjectsChld.OffsetX : 0;
											objOffsetY = objObjectsChld.OffsetY;// (!objObjectsChld.IsColumnNull("OffsetY")) ? objObjectsChld.OffsetY : 0;
											currentX -= objOffsetX;
											currentY -= objOffsetY;
											//if (!objObjectsChld.IsColumnNull("IsNewLine"))
											//{
											if (objObjectsChld.IsNewLine)
											{
												currentX = (objObjects.PositionX) - objOffsetX;
												currentY -= objOffsetY - LrgHeight;
											}
											//}
											currentX -= objObjectsChld.MaxWidth;
											ADDTextObject(objObjectsChld, ref doc, fontPath, currentX, currentY + objObjectsChld.MaxHeight, objObjectsChld.MaxWidth, objObjectsChld.MaxHeight);

											if (objObjectsChld.MaxHeight > LrgHeight)
												LrgHeight = objObjectsChld.MaxHeight;
										}
										//objObjectsChld.MoveNext();
									}
								}
								#endregion
								#region  Center , top
								else if (HAlign == 2 && VAlign == 3)
								{
									double currentX = objObjects.PositionX, currentY = objObjects.PositionY, objOffsetX = 0, objOffsetY = 0, LrgHeight = 0;

									double MxLineWidth = 0;
									int CrtIdx = 0;

									if (objObjects.ContentString != "" && !objObjects.IsHidden)
									{
										objOffsetX = objObjects.OffsetX;// (!objObjects.IsColumnNull("OffsetX")) ? objObjects.OffsetX : 0;
										objOffsetY = objObjects.OffsetY;// (!objObjects.IsColumnNull("OffsetY")) ? objObjects.OffsetY : 0;
										currentX += objOffsetX;
										currentY += objOffsetY;
										//if (!objObjectsChld.EOF && !objObjectsChld.IsColumnNull("IsNewLine"))
										if (ChildObjects.Count() > 0)
										{
											if (ChildObjects.First().IsNewLine)
											{
												MxLineWidth = 0;
											}
											else
												MxLineWidth = GetMaxLineWidth(CrtIdx, ChildObjects.ToList());
										}
										else
											MxLineWidth = GetMaxLineWidth(CrtIdx, ChildObjects.ToList());

										currentX = objOffsetX + (objObjects.PositionX - (MxLineWidth + objObjects.MaxWidth / 2));
										ADDTextObject(objObjects, ref doc, fontPath, currentX, currentY + objObjects.MaxHeight, objObjects.MaxWidth, objObjects.MaxHeight);
										LrgHeight = objObjects.MaxHeight;
										currentX += objObjects.MaxWidth;
									}


									foreach (var objObjectsChld in ChildObjects)
									{
										if (objObjectsChld.ContentString != "" && !objObjectsChld.IsHidden)
										{
											objOffsetX = objObjectsChld.OffsetX;// (!objObjectsChld.IsColumnNull("OffsetX")) ? objObjectsChld.OffsetX : 0;
											objOffsetY = objObjectsChld.OffsetY;// (!objObjectsChld.IsColumnNull("OffsetY")) ? objObjectsChld.OffsetY : 0;
											currentX += objOffsetX;
											currentY += objOffsetY;

											//if (!objObjectsChld.IsColumnNull("IsNewLine"))
											//{
											if (objObjectsChld.IsNewLine)
											{
												MxLineWidth = GetMaxLineWidth(CrtIdx, ChildObjects.ToList());
												currentX = objOffsetX + (objObjects.PositionX - (MxLineWidth / 2));
												currentY += objOffsetY + LrgHeight;
											}
											//}

											ADDTextObject(objObjectsChld, ref doc, fontPath, currentX, currentY + objObjectsChld.MaxHeight, objObjectsChld.MaxWidth, objObjectsChld.MaxHeight);
											currentX += objObjectsChld.MaxWidth;
											if (objObjectsChld.MaxHeight > LrgHeight)
												LrgHeight = objObjectsChld.MaxHeight;
										}
										CrtIdx++;
										//objObjectsChld.MoveNext();
									}
								}
								#endregion
							}
						}
                        else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8) //3 = image and 8 = Logo
						{
							LoadImage(ref doc, objObjects, logoPath);
						}
						else if (objObjects.ObjectType == 5)    //line vector
						{
							DrawVectorLine(ref doc, objObjects);
						}
                        else if (objObjects.ObjectType == 6)    //line vector
                        {
                            DrawVectorRectangle(ref doc, objObjects);
                        }
                        else if (objObjects.ObjectType == 7)    //line vector
                        {
                            DrawVectorEllipse(ref doc, objObjects);
                        }
						//objObjects.MoveNext();
					}

                    //crop marks or margins
                    if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0)
                    {
                        //doc.CropBox.Height = doc.MediaBox.Height;
                        //doc.CropBox.Width = doc.MediaBox.Width;


                        doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left+AppCommon.MMToPoint(5)).ToString() + " " + (doc.MediaBox.Top+AppCommon.MMToPoint(5)).ToString() + " " + (doc.MediaBox.Width-AppCommon.MMToPoint(5)).ToString() + " " + (doc.MediaBox.Height-AppCommon.MMToPoint(5)).ToString());

                        DrawCuttingLine(ref doc, objProduct.CuttingMargin.Value, 1);
                    }

                    if (DrawBKText == true)
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
                    AppCommon.LogException(ex);
					throw new Exception("ShowPDF", ex);
				}
				finally
				{
					doc.Dispose();
				}
			}
		}


		private void LoadMultiPageBackGroundArtWork(ref Doc oPdf, int pid, bool IsSide2, string imgPath)
		{
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
			{
				try
				{
					oPdf.Rect.Left = oPdf.MediaBox.Left;
					oPdf.Rect.Top = oPdf.MediaBox.Top;
					oPdf.Rect.Right = oPdf.MediaBox.Right;
					oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
					int theID = 0;
					for (int i = 1; i <= oPdf.PageCount; i++)
					{
						//this.Where.ProductId.Value = ProId;
						//this.Where.PageNo.Value = PageNo;
						//this.Where.IsSide2.Value = IsSide2;
						TemplatePages objPages = db.TemplatePages.Where(g => g.ProductId == pid && g.PageNo == i && g.IsSide2 == IsSide2).SingleOrDefault();
						//objPages.LoadByProductIdPageNo(pid, i, IsSide2);
						if (objPages != null)
						{
							if (objPages.BackgroundImg != null && objPages.BackgroundImg != "" && System.IO.File.Exists(imgPath + objPages.BackgroundImg) == true)
							{
								oPdf.PageNumber = i;
								oPdf.Layer = oPdf.LayerCount + 1;
								theID = oPdf.AddImageFile(imgPath + objPages.BackgroundImg, 1);
							}
						}
					}
				}
				catch (Exception ex)
				{
					throw new Exception("LoadBackGroundArtWork", ex);
				}
			}
		}

		private void LoadBackGroundArtWork(ref Doc oPdf, Templates oTemplate, bool showSide2PDF, string imgPath)
		{

			try
			{
				oPdf.Rect.Left = oPdf.MediaBox.Left;
				oPdf.Rect.Top = oPdf.MediaBox.Top;
				oPdf.Rect.Right = oPdf.MediaBox.Right;
				oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;

				if (showSide2PDF == false)
				{
					oPdf.PageNumber = 1;
					oPdf.Layer = oPdf.LayerCount + 1;
					oPdf.AddImageFile(imgPath + oTemplate.BackgroundArtwork, 1);
				}
				else
				{
					oPdf.PageNumber = 1;
					oPdf.Layer = oPdf.LayerCount + 1;
					oPdf.AddImageFile(imgPath + oTemplate.Side2BackgroundArtwork, 1);
				}

			}
			catch (Exception ex)
			{
				throw new Exception("LoadBackGroundArtWork", ex);
			}

		}


		private void LoadBackColor(ref Doc oPdf, Templates oTemplate, bool showSide2PDF)
		{

			try
			{
				oPdf.Rect.Left = oPdf.MediaBox.Left;
				oPdf.Rect.Top = oPdf.MediaBox.Top;
				oPdf.Rect.Right = oPdf.MediaBox.Right;
				oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;

				if (showSide2PDF == false)
				{
					oPdf.PageNumber = 0;
					oPdf.Layer = oPdf.LayerCount + 1;
					//oPdf.AddImageFile(imgPath + oTemplate.BackgroundArtwork, 1);

					oPdf.Color.Alpha = 255;
					oPdf.Color.Red = oTemplate.BgR.Value;
					oPdf.Color.Green = oTemplate.BgG.Value;
					oPdf.Color.Blue = oTemplate.BgB.Value;

					oPdf.FillRect();
				}
				else
				{
					oPdf.PageNumber = 0;
					oPdf.Layer = oPdf.LayerCount + 1;
					//oPdf.AddImageFile(imgPath + oTemplate.Side2BackgroundArtwork, 1);

					oPdf.Color.Alpha = 255;
					oPdf.Color.Red = oTemplate.Side2BgR.Value;
					oPdf.Color.Green = oTemplate.Side2BgG.Value;
					oPdf.Color.Blue = oTemplate.Side2BgB.Value;

					oPdf.FillRect();
				}

			}
			catch (Exception ex)
			{
				throw new Exception("LoadBackColor", ex);
			}

		}

		private void DrawCuttingLine(ref Doc oPdf, double mrg, int PageNo)
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

		private void ADDTextObject(TemplateObjects ooBject, ref Doc oPdf, string Font, double OPosX, double OPosY, double OWidth, double OHeight)
		{
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
			{
				try
				{
					oPdf.TextStyle.Outline = 0;
					oPdf.TextStyle.Strike = false;
					oPdf.TextStyle.Bold = false;
					oPdf.TextStyle.Italic = false;
					oPdf.TextStyle.CharSpacing = 0;
					oPdf.PageNumber = ooBject.PageNo;// (ooBject.IsColumnNull("PageNo")) ? ooBject.PageNo : 1;
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

						var pFont = db.TemplateFonts.Where(g => g.FontName == ooBject.FontName).SingleOrDefault();

						if (pFont != null)
						{
							if (pFont.IsPrivateFont == true)
							{
								if (System.IO.File.Exists(Font + pFont.FontFile))
									FontID = oPdf.EmbedFont(Font + pFont.FontFile);
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





                    if (ooBject.LineSpacing > 0)
                        oPdf.TextStyle.LineSpacing = ( ooBject.LineSpacing - ooBject.FontSize) / 1.7;
                    else
                        oPdf.TextStyle.LineSpacing = 0;


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

					oPdf.AddHtml("<p>"+sNewLineNormalized+"</p>");
					oPdf.Transform.Reset();


				}
				catch (Exception ex)
				{
					throw new Exception("ADDSingleLineText", ex);
				}
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

        private void LoadImage(ref Doc oPdf, TemplateObjects oobject, string logoPath)
        {
            XImage oImg = new XImage();
            try
            {
                oPdf.PageNumber = oobject.PageNo;


                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oobject.ObjectType == 8)
                {
                    logoPath = ""; //since path is already in filenm
                    FilePath = System.Web.Hosting.HostingEnvironment.MapPath(oobject.ContentString);
                    bFileExists = System.IO.File.Exists(FilePath);

                }
                else
                {
                    if (oobject.ContentString != "")
                        FilePath = oobject.ContentString;
                    FilePath = logoPath + "/" + FilePath;
                    bFileExists = System.IO.File.Exists(FilePath);
                }
                //  else
                //     filNm = oobject.LogoName;

                if (bFileExists)
                {

                    oImg.SetFile(FilePath);

                    oPdf.Rect.Position(oobject.PositionX, oobject.PositionY);
                    oPdf.Rect.Resize(oobject.MaxWidth, oobject.MaxHeight);

                    if (oobject.RotationAngle != null)
                    {
                        double _XPosition = 0, _YPostion = 0;

                        if (oobject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(oobject.RotationAngle, oobject.PositionX + oobject.MaxWidth / 2, oPdf.MediaBox.Height - oobject.PositionY + oobject.MaxHeight / 2);


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
		private void DrawVectorLine(ref Doc oPdf, TemplateObjects oobject)
		{
		   
			try
			{
				oPdf.PageNumber = oobject.PageNo;

			   

					

                        if (oobject.ColorType == 3)
                        {
                            if (oobject.IsSpotColor == true)
                            {
                                oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oobject.SpotColorName, oobject.ColorC.ToString() + " " + oobject.ColorM.ToString() + " " + oobject.ColorY.ToString() + " " + oobject.ColorK.ToString());
                            }
                            oPdf.Color.String = oobject.ColorC.ToString() + " " + oobject.ColorM.ToString() + " " + oobject.ColorY.ToString() + " " + oobject.ColorK.ToString();
                            //if (!ooBject.IsColumnNull("Tint"))
                            oPdf.Color.Alpha = Convert.ToInt32((100 - oobject.Tint) * 2.5);
                        }
                        else if (oobject.ColorType == 4) // For RGB Colors
                        {
                            oPdf.Color.String = oobject.RColor.ToString() + " " + oobject.GColor.ToString() + " " + oobject.BColor.ToString();
                        }


                        oPdf.Width = oobject.MaxHeight;
						oPdf.Rect.Position(oobject.PositionX, oobject.PositionY);
						oPdf.Rect.Resize(oobject.MaxWidth, oobject.MaxHeight);


						if (oobject.RotationAngle != null)
						{
						
							if (oobject.RotationAngle != 0)
							{
                                oPdf.Transform.Reset();
                                oPdf.Transform.Rotate(oobject.RotationAngle, oobject.PositionX + oobject.MaxWidth / 2, oPdf.MediaBox.Height - oobject.PositionY);
							}


						}

						// oPdf.AddImageObject(oImg,false);
						//oPdf.AddImage ((oImg);
                        oPdf.AddLine(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight/2);
						oPdf.Transform.Reset();
					
			}
			
			catch (Exception ex)
			{
				throw new Exception("LoadImage", ex);
			}
			
		}

        //vector rectangle drawing in PDF
        private void DrawVectorRectangle(ref Doc oPdf, TemplateObjects oobject)
        {

            try
            {
                oPdf.PageNumber = oobject.PageNo;

                if (oobject.ColorType == 3)
                {
                    if (oobject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oobject.SpotColorName, oobject.ColorC.ToString() + " " + oobject.ColorM.ToString() + " " + oobject.ColorY.ToString() + " " + oobject.ColorK.ToString());
                    }
                    oPdf.Color.String = oobject.ColorC.ToString() + " " + oobject.ColorM.ToString() + " " + oobject.ColorY.ToString() + " " + oobject.ColorK.ToString();
                    //if (!ooBject.IsColumnNull("Tint"))
                    oPdf.Color.Alpha = Convert.ToInt32((100 - oobject.Tint) * 2.5);
                }
                else if (oobject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oobject.RColor.ToString() + " " + oobject.GColor.ToString() + " " + oobject.BColor.ToString();
                }


                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oobject.PositionX, oobject.PositionY + oobject.MaxHeight);
                oPdf.Rect.Resize(oobject.MaxWidth, oobject.MaxHeight);


                if (oobject.RotationAngle != null)
                {

                    if (oobject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(oobject.RotationAngle, oobject.PositionX + oobject.MaxWidth / 2, oPdf.MediaBox.Height - oobject.PositionY - oobject.MaxHeight/2);
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
                throw new Exception("LoadImage", ex);
            }

        }

        //vector Ellipse drawing in PDF
        private void DrawVectorEllipse(ref Doc oPdf, TemplateObjects oobject)
        {

            try
            {
                oPdf.PageNumber = oobject.PageNo;

                if (oobject.ColorType == 3)
                {
                    if (oobject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oobject.SpotColorName, oobject.ColorC.ToString() + " " + oobject.ColorM.ToString() + " " + oobject.ColorY.ToString() + " " + oobject.ColorK.ToString());
                    }
                    oPdf.Color.String = oobject.ColorC.ToString() + " " + oobject.ColorM.ToString() + " " + oobject.ColorY.ToString() + " " + oobject.ColorK.ToString();
                    //if (!ooBject.IsColumnNull("Tint"))
                    oPdf.Color.Alpha = Convert.ToInt32((100 - oobject.Tint) * 2.5);
                }
                else if (oobject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oobject.RColor.ToString() + " " + oobject.GColor.ToString() + " " + oobject.BColor.ToString();
                }

               


                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oobject.PositionX, oobject.PositionY + oobject.MaxHeight);
                oPdf.Rect.Resize(oobject.MaxWidth, oobject.MaxHeight);


                if (oobject.RotationAngle != null)
                {

                    if (oobject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(oobject.RotationAngle, oobject.PositionX + oobject.MaxWidth / 2, oPdf.MediaBox.Height - oobject.PositionY - oobject.MaxHeight / 2);
                    }


                }

                oPdf.FillRect(oobject.MaxWidth / 2, oobject.MaxHeight / 2);
                
                //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("LoadImage", ex);
            }

        }

		//private void resolveOffsetAfterRotation(int XPos, int YPos, int Angle)
		//{

		//    //Double NewAngle = Radians * (180 / Math.PI);
		//    double Radians = Angle * (Math.PI / 180);
		//    Math.Asin()

		//    Point CenterLoc = new Point(
		//            (Double)c.GetValue(Canvas.LeftProperty),
		//            (Double)c.GetValue(Canvas.TopProperty)
		//            );
		//    CenterLoc.X += Dimension.Width;
		//    CenterLoc.Y += Dimension.Height;
		//    Double Radians = Math.Atan2(MouseLoc.X - CenterLoc.X, MouseLoc.Y - CenterLoc.Y);
			
		//}

		#endregion

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

		public string GenerateTemplatePagePreview(byte[] PDFDoc, string savePath, string PreviewFileName, double CuttingMargin)
		{

			
			XSettings.License = "810-031-225-276-0715-601";
			Doc theDoc = new Doc();
			
			
			
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

				theDoc.Rendering.DotsPerInch = 150;
				theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png");
				theDoc.Dispose();




				return PreviewFileName + ".png";

			}
			catch (Exception ex)
			{
				throw new Exception("GenerateTemplatePagePreview", ex);
			}
			finally
			{
			   

				if (theDoc != null)
					theDoc.Dispose();

			 
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
				AppCommon.LogException(ex);
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

		public List<tbl_ProductCategory> GetCategoriesByMatchingSetID(int MatchingSetID)
		{
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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

		public List<sp_GetMatchingSetTemplateView_Result> GetMatchingSetTemplates(string keywords, int MatchingSetID, int PageNo, int PageSize, bool callbind, int status, int UserID, string Role, out int PageCount)
		{
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
			{

				try
				{
					db.ContextOptions.LazyLoadingEnabled = false;


					var parameter = new System.Data.Objects.ObjectParameter("TotalRows", typeof(int));


					

					var result =  db.sp_GetMatchingSetTemplateView(status, MatchingSetID, keywords, PageSize, PageNo, parameter,Role,UserID).ToList();

					PageCount = Convert.ToInt32(parameter.Value) / PageSize;

					foreach (var item in result)
					{
						if (item.Thumbnail == null || item.Thumbnail == string.Empty)
							item.Thumbnail = "cardgeneral.jpg";

						
					}

					return result.ToList();



					

					//return temps.ToList();
				}
				catch (Exception ex)
				{
					throw ex;
					// throw new Exception(ex.ToString());
				}

			}
		}


		public List<sp_GetMatchingSetTemplatesList_Result> GetMatchingSetTemplatesList(int MatchingSetID, string ProductName)
		{
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
			{
				try
				{
					return db.sp_GetMatchingSetTemplatesList(MatchingSetID, ProductName).OrderBy( g=> g.CategoryName).ToList();
					
				}
				catch (Exception ex)
				{

					throw ex;
				}
			}
		}

		public List<BaseColors> GetBaseColors()
		{
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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
		public bool SaveTemplates(Templates oTemplate, ObservableCollection<TemplateIndustryTags> lstIndustryTags, ObservableCollection<TemplateThemeTags> lstThemeTags, bool IsAdd, out int NewTemplateID, bool IsCatChanged)
		{
			try
			{
				if (IsAdd == true)
				{
					using (TemplateDesignerEntities db = new TemplateDesignerEntities())
					{
						db.Templates.AddObject(oTemplate);


						//getting the selected category dimensions  and see if they are to be applied.

						var SelectedProductCategory = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();


						if (SelectedProductCategory.ApplySizeRestrictions.Value)
						{
							oTemplate.PDFTemplateHeight = AppCommon.MMToPoint(SelectedProductCategory.HeightRestriction.Value);
							oTemplate.PDFTemplateWidth = AppCommon.MMToPoint(SelectedProductCategory.WidthRestriction.Value);
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

						CreateBlankBackgroundPDFs(oTemplate.ProductID, AppCommon.MMToPoint(SelectedProductCategory.HeightRestriction.Value), AppCommon.MMToPoint(SelectedProductCategory.WidthRestriction.Value), oTemplate.Orientation.Value);

                        

						oTemplate.LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + "Side1.pdf";
						oTemplate.Side2LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + "Side2.pdf";

						db.TemplateObjects.AddObject(ReturnObject("CompanyName", "Your Company Name", 50, 40, oTemplate.ProductID, 401, 1,14,true));
						db.TemplateObjects.AddObject(ReturnObject("CompanyMessage", "Your Company Message", 50, 50, oTemplate.ProductID, 402, 2,10,false));
						db.TemplateObjects.AddObject(ReturnObject("Name", "Your Name", 50, 60, oTemplate.ProductID, 403, 3,12,true));
						db.TemplateObjects.AddObject(ReturnObject("Title", "Your Title", 50, 70, oTemplate.ProductID, 404, 4,10,false));
						db.TemplateObjects.AddObject(ReturnObject("AddressLine1", "Address Line 1", 50, 80, oTemplate.ProductID, 405, 5,10,false));
						//db.TemplateObjects.AddObject(ReturnObject("AddressLine2", "Address Line 2", 50, 90, oTemplate.ProductID, 406, 6,10,false));
						//db.TemplateObjects.AddObject(ReturnObject("AddressLine3", "Address Line 3", 50, 100, oTemplate.ProductID, 407, 7,10,false));
						db.TemplateObjects.AddObject(ReturnObject("Phone", "Telephone / Other", 50, 110, oTemplate.ProductID, 408, 8,10,false));
						db.TemplateObjects.AddObject(ReturnObject("Fax", "Fax / Other", 50, 120, oTemplate.ProductID, 409, 9,10,false));
						db.TemplateObjects.AddObject(ReturnObject("Email", "Email address / Other", 50, 130, oTemplate.ProductID, 410, 10,10,false));
						db.TemplateObjects.AddObject(ReturnObject("Website", "Website address", 50, 140, oTemplate.ProductID, 411, 11,10,false));

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
					using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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

                            CreateBlankBackgroundPDFs(tmpTemplate.ProductID, AppCommon.MMToPoint(SelectedProductCategory.HeightRestriction.Value), AppCommon.MMToPoint(SelectedProductCategory.WidthRestriction.Value), oTemplate.Orientation.Value);

                            tmpTemplate.LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + "Side1.pdf";
                            tmpTemplate.Side2LowResPDFTemplates = oTemplate.ProductID.ToString() + "/" + "Side2.pdf";
                        }

                        if (IsCatChanged)
                        {
                            var SelectedProductCategory = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();
                            tmpTemplate.PDFTemplateHeight = AppCommon.MMToPoint(SelectedProductCategory.HeightRestriction.Value);
                            tmpTemplate.PDFTemplateWidth = AppCommon.MMToPoint(SelectedProductCategory.WidthRestriction.Value);
                            CreateBlankBackgroundPDFs(tmpTemplate.ProductID, AppCommon.MMToPoint(SelectedProductCategory.HeightRestriction.Value), AppCommon.MMToPoint(SelectedProductCategory.WidthRestriction.Value), oTemplate.Orientation.Value);


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
				AppCommon.LogException(ex);
				throw ex;
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
			oTempObject.PageNo = 1;
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
			using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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
				AppCommon.LogException(ex);
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
				using (TemplateDesignerEntities db = new TemplateDesignerEntities())
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
				AppCommon.LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary>
		/// Copies all templates in a matching set.
		/// </summary>
		/// <param name="MatchingSetID"></param>
		/// <param name="ProductName"></param>
		/// <param name="SubmittedBy"></param>
		/// <param name="SubmittedByName"></param>
		/// <returns></returns>
		public int CopyMatchingSet(int ProductID, int SubmittedBy, string SubmittedByName)
		{


			int MatchingSetID = 0; string ProductName = string.Empty;

			int result = 0;
			List<sp_GetMatchingSetTemplatesList_Result> oMatchingSetProducts = null;
			try
			{
				using (TemplateDesignerEntities db = new TemplateDesignerEntities())
				{
					Templates oTemplate = db.Templates.Where(g => g.ProductID == ProductID).Single();
					MatchingSetID = oTemplate.MatchingSetID.Value;
					ProductName = oTemplate.ProductName;

					oMatchingSetProducts = db.sp_GetMatchingSetTemplatesList(MatchingSetID, ProductName).ToList();
				}

				//copy each product :)

				foreach (var item in oMatchingSetProducts)
				{
					if (item.ProductID != null)
					{
						result = CopyTemplate(item.ProductID.Value, SubmittedBy, SubmittedByName);
					}
				}
			}
			catch (Exception ex)
			{
				AppCommon.LogException(ex);
				throw ex;
			}

			return result;
		}


		public bool 
            UpdateMatchingSetTemplates(List<int> ProductIDs, string ProductName, string NarrativeTag, int? BaseColorID, List<int> IndustryTagIDs, List<int> ThemeTagIDs, int action, int userId, string fullname, string rejectReason, int? AdminRating)
		{
			try
			{

				using (TemplateDesignerEntities db = new TemplateDesignerEntities())
				{
					foreach (var ProductID in ProductIDs )
					{
						Templates oTemplate = db.Templates.Where(g => g.ProductID == ProductID).Single();




						if (action == 2) //submitt
						{
							oTemplate.Status = 2;
							oTemplate.SubmittedByName = fullname;
							oTemplate.SubmitDate = DateTime.Now;
							
						}
						else if (action == 3)
						{
							oTemplate.Status = 3;
							oTemplate.ApprovedBy = userId;
							oTemplate.ApprovedByName = fullname;
							oTemplate.ApprovalDate = DateTime.Now;
							
						}
						else if (action == 4)
						{
							oTemplate.Status = 4;
							oTemplate.ApprovedBy = userId;
							oTemplate.ApprovedByName = fullname;
							oTemplate.RejectionReason = rejectReason;
							oTemplate.ApprovalDate = DateTime.Now;
							
						}

						if (AdminRating != null)
							oTemplate.MPCRating = AdminRating;



						oTemplate.ProductName = ProductName;
						oTemplate.Description = NarrativeTag;
						oTemplate.BaseColorID = BaseColorID;

						db.TemplateIndustryTags.Where(w => w.ProductID == oTemplate.ProductID).ToList().ForEach(db.DeleteObject);
						db.TemplateThemeTags.Where(w => w.ProductID == oTemplate.ProductID).ToList().ForEach(db.DeleteObject);

						foreach (var TagID in IndustryTagIDs)
						{
							TemplateIndustryTags oTag=  new TemplateIndustryTags();
							oTag.ProductID = oTemplate.ProductID;
							oTag.TagID = TagID;
							db.TemplateIndustryTags.AddObject(oTag);
						}

						foreach (var TagID in ThemeTagIDs)
						{
							TemplateThemeTags oTag = new TemplateThemeTags();
							oTag.ProductID = oTemplate.ProductID;
							oTag.TagID = TagID;
							db.TemplateThemeTags.AddObject(oTag);

						}

						db.SaveChanges();
					   

					}

				}
				return true;
			   
			}
			catch (Exception ex)
			{
				AppCommon.LogException(ex);
				return false;
			}
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
       


		public QuickText GetContactQuickTextFields(int CustomerID, int ContactID)
		{
			//in temp designer no functionality happens
			QuickText oQuickText = null;
			return oQuickText;
		}

		public bool UpdateQuickText(QuickText oQuickTextData, int ContactID)
		{
			return true;
		}





		//////////////////////////////////////////////////////////////////////////


	   
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
