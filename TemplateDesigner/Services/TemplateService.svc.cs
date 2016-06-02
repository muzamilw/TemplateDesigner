using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using TemplateDesigner.Models;
using System.ServiceModel.Activation;
using TemplateDesignerModelTypes;
using LinqKit;

namespace TemplateDesigner.Services
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TemplateService" in code, svc and config file together.
			[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class TemplateService : ITemplateService
	{

				public int go()
				{
					return 1;
				}

			   public List<Tags> GetTags()
			   {
				   using ( TemplateDesignerEntities oContext = new TemplateDesignerEntities())
				   {
					   return oContext.Tags.ToList();
				   }
			   }

			   public List<BaseColors> GetBaseColors()
			   {
				   using (TemplateDesignerEntities oContext = new TemplateDesignerEntities())
				   {
					   return oContext.BaseColors.OrderBy ( g => g.Color).ToList();
				   }
			   }

			   public List<vw_getChildCategoriesWithTemplates> GetCategories1()
			   {
				   using (TemplateDesignerEntities oContext = new TemplateDesignerEntities())
				   {
					   oContext.ContextOptions.LazyLoadingEnabled = false;
					   return oContext.vw_getChildCategoriesWithTemplates.OrderBy(g => g.CategoryName).ToList();

					   //var result = oContext.Templates.Distinct().Select (;


					   //var result = (from PC in oContext.tbl_ProductCategory
					   //              join T in oContext.Templates
					   //                          on PC.ProductCategoryID equals T.ProductCategoryID
					   //              where PC.ProductCategoryID == T.ProductCategoryID
					   //              select PC).Where(g => g.ParentCategoryID != null).OrderBy(g => g.CategoryName);

					   //return result.ToList();
				   }
			   }


				public List<Templates> GetTemplatesbyCategory(string GlobalCategoryName, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount) 
				{
					System.Net.ServicePointManager.Expect100Continue = false;
					var predicate = PredicateBuilder.True<Templates>();
					try
					{
						TemplateDesignerEntities oContext = new TemplateDesignerEntities();

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
						if ( IndustryID != 0)
						{
							result = (from TT in result
							join IT in oContext.TemplateIndustryTags 
										on TT.ProductID equals IT.ProductID
										where  IT.TagID == IndustryID
										 select TT);
						}

						//style 
						if ( ThemeStyleID != 0)
						{
							result = (from TTT in result
							join TT in oContext.TemplateThemeTags 
										on TTT.ProductID equals TT.ProductID
										where  TT.TagID == ThemeStyleID
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

						predicate = predicate.And(p => p.Status == 3 || p.Status == 2 );

						//result = (from PC in result
						//          where PC.CategoryName == GlobalCategoryName && T.Thumbnail != null

						result = (from T in result
							 select T).AsExpandable().Where(predicate).OrderBy( g=> g.ProductName);


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


				public List<Templates> GetTemplatesbyTemplateIds(int[] TemplateIds, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount)
				{
					System.Net.ServicePointManager.Expect100Continue = false;
					var predicate = PredicateBuilder.True<Templates>();
					try
					{
						TemplateDesignerEntities oContext = new TemplateDesignerEntities();

						if (Keywords != string.Empty)
						{
							predicate = predicate.And(p => p.ProductName.Contains(Keywords));
						}

						//main query + category
						var result = (from T in oContext.Templates
									  where TemplateIds.Contains(T.ProductID)
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
						TemplateDesignerEntities oContext = new TemplateDesignerEntities();

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


				public List<Templates> GetTemplateThumbnailsbyCategory(int CategoryID)
				{
					try
					{
						TemplateDesignerEntities oContext = new TemplateDesignerEntities();
						//var result = oContext.Templates.Where(g => g.ProductCategoryID == CategoryID)
						//    ;  //.t
						//return result.ToList();
						var result = from temps in oContext.Templates.AsEnumerable()
									 orderby temps.ProductName ascending
									 where temps.ProductCategoryID == CategoryID
									select new Templates
									{
										Thumbnail = temps.Thumbnail,
									   ProductName = temps.ProductName,
									   ProductID = temps.ProductID
									};
						return result.ToList();

					}
					catch (Exception ex)
					{

						throw ex;
					}

				}
				public List<Templates> GetTemplatebyProductTemplateID(int TemplateID)
				{
					try
					{
						TemplateDesignerEntities oContext = new TemplateDesignerEntities();
						//var result = oContext.Templates.Where(g => g.ProductCategoryID == CategoryID)
						//    ;  //.t
						//return result.ToList();
						var result = from temps in oContext.Templates.AsEnumerable()
									 orderby temps.ProductName ascending
									 where temps.ProductID == TemplateID
									 select new Templates
									 {
										 Thumbnail = temps.Thumbnail,
										 ProductName = temps.ProductName,
										 ProductID = temps.ProductID
									 };
						return result.ToList();

					}
					catch (Exception ex)
					{

						throw ex;
					}

				}

				
				

				
	}
}
