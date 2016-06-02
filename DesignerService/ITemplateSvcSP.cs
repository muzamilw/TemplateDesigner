using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
//using TemplateDesigner.Models;
using System.Collections.ObjectModel;
using TemplateDesignerModelTypesV2;





namespace DesignerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductService" in both code and config file together.
    [ServiceContract]
    public interface ITemplateSvcSP
    {
        [OperationContract]
        Templates GetTemplate(int TemplateID);
        [OperationContract]
        List<TemplatePages> GetTemplatePages(int TemplateID);
        [OperationContract]
        List<TemplateObjects> GetTemplateObjects(int TemplateID);
        [OperationContract]
        List<TemplateBackgroundImages> GettemplateImages(int TemplateID);
      




        //[OperationContract]
        //List<MatchingSets> GetMatchingSets();

        //[OperationContract]
        //MatchingSets GetMatchingSetbyID(int MatchingSetID);

        //[OperationContract]
        //List<TemplateFonts> GetFontList(int ProductId, bool ReturnFontFiles, FontLoadModes mode);
    

        //[OperationContract]
        //List<TemplateBackgroundImages> GetProductBackgroundImages(int ProductId);
        //[OperationContract]
        //string GetProductBackgroundImg(int ProductId, string BkImg, bool IsSide2, int PageNo);

        //[OperationContract]
        //bool DeleteProductBackgroundImage(int ProductId, int BackgroundImageID);

        //[OperationContract]
        //List<TemplateColorStyles> GetColorStyle(int ProductId);

       
        //[OperationContract]
        //string SaveObjectsAndGenratePDF(int ProductId, string Bkimg, string Bkimg2, ObservableCollection<TemplateObjects> objObjectsList, DesignerModes Mode, SaveOperationTypes SaveOperationType);

      
        [OperationContract]
        List<tbl_ProductCategory> GetCategoriesByMatchingSetID(int MatchingSetID);

        [OperationContract]
        List<BaseColors> GetBaseColors();

        //[OperationContract]
        //List<String> GetMatchingSetTheme();

        [OperationContract]
        List<sp_GetTemplateThemeTags_Result> GetTemplateThemeTags(int? ProductID);
        [OperationContract]
        List<sp_GetTemplateIndustryTags_Result> GetTemplateIndustryTags(int? ProductID);
        [OperationContract]
        List<Templates> GetTemplates(string keywords, int ProductCategoryID, int PageNo, int PageSize, bool callbind, int status,int UserID,string Role, out int PageCount);

       

        [OperationContract]
        List<tbl_ProductCategoryFoldLines> GetFoldLinesByProductCategoryID(int ProductCategoryID, out bool ApplyFoldLines);

        [OperationContract]
        bool SaveTemplates(Templates oTemplate, ObservableCollection<TemplateIndustryTags> lstIndustryTags, ObservableCollection<TemplateThemeTags> lstThemeTags, bool IsAdd, out int NewTemplateID, bool IsCatChanged);
        
        [OperationContract]
        bool DeleteTemplate(int ProductID, out int CategoryID);

        [OperationContract]
        int CopyTemplate(int ProductID, int SubmittedBy, string SubmittedByName);

        //[OperationContract]
        //int CopyMatchingSet(int ProductID, int SubmittedBy, string SubmittedByName);

        //[OperationContract]
        //bool UpdateMatchingSetTemplates(List<int> ProductIDs, string ProductName, string NarrativeTag, int? BaseColorID, List<int> IndustryTagIDs, List<int> ThemeTagIDs, int action, int userId, string fullname, string rejectReason, int? AdminRating);


        //[OperationContract]
        //TemplateBackgroundImages CropImage(string ImgName, int ImgX, int ImgY, int ImgWidth, int ImgHeight, int Mode);

        [OperationContract]
        bool CropImage(string ImgName, int ImgX, int ImgY, int ImgWidth, int ImgHeight);


        [OperationContract]
        List<Tags> GetTags();

        //webstore functions

        //[OperationContract]
        //QuickText GetContactQuickTextFields(int CustomerID, int ContactID);

        //[OperationContract]
        //bool UpdateQuickText(QuickText oQuickTextData, int ContactID);

        [OperationContract]
        List<Templates> GetTemplatesbyCategory(string GlobalCategoryName, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount);

        //used by webstore to get favorite templates
        [OperationContract]
        List<Templates> GetTemplatesbyProductIds(int[] ProductIds, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount);

        [OperationContract]
        int SaveTemplateLocally(Templates oTemplate, List<TemplatePages> oTemplatePages, List<TemplateObjects> oTemplateObjects, List<TemplateBackgroundImages> oTemplateImages,string RemoteUrlBasePath);

        // MIS specific functions

        
        [OperationContract]
        List<vw_ProductCategoriesLeafNodes> GetCategories();
    }

}

