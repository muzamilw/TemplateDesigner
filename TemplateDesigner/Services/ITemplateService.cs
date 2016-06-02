using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using TemplateDesigner.Models;
using TemplateDesignerModelTypes;

namespace TemplateDesigner.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITemplateService" in both code and config file together.
    [ServiceContract]
    public interface ITemplateService
    {


        [OperationContract]
        List<Tags> GetTags();

        [OperationContract]
        List<BaseColors> GetBaseColors();

        [OperationContract]
        List<vw_getChildCategoriesWithTemplates> GetCategories1();


        //[OperationContract]
        //List<Templates> GetTemplatesbyCategory(int CategoryID);

        //[OperationContract]
        //List<Templates> GetTemplatesbyCategory(int CategoryID, int PageNo, int PageSize);
        //[OperationContract(Name = "GetTemplatesbyCategoryName")] 
        [OperationContract]
        List<Templates> GetTemplatesbyCategory(string GlobalCategoryName, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount);

        [OperationContract]

        List<Templates> GetTemplatesbyTemplateIds(int[] TemplateIds, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount);

        [OperationContract]

        List<Templates> GetTemplatesbyProductIds(int[] ProductIds, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount);

        [OperationContract]

        List<Templates> GetTemplateThumbnailsbyCategory(int CategoryID);

        [OperationContract]
        List<Templates> GetTemplatebyProductTemplateID(int TemplateID);

        [OperationContract]
        int go();

       
    }
}
