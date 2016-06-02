using System;
using System.Windows;
using System.Collections.Generic;


public static class DictionaryManager
{
    private static Dictionary<string, object> appobjects = new Dictionary<string, object>();
    public static Dictionary<string, object> AppObjects
    {
        get { return DictionaryManager.appobjects; }
        set { DictionaryManager.appobjects = value; }
    }

    private static webprintDesigner.EditTemplate frmEdit = null;
    private static webprintDesigner.TemplatesList frmListView = null;
    private static webprintDesigner.Page frmDesigner = null;
    private static webprintDesigner.Login frmLogin = null;

   public static webprintDesigner.EditTemplate EditTemplate
   {
       get {

           if (DictionaryManager.frmEdit == null)
           {
               frmEdit = new webprintDesigner.EditTemplate();
           }
           return DictionaryManager.frmEdit; 
       
       }
   }

   public static webprintDesigner.TemplatesList ListView
   {
       get {

           if (DictionaryManager.frmListView == null)
           {
               frmListView = new webprintDesigner.TemplatesList();
           }
           
           return DictionaryManager.frmListView; 
       
       }
   }

   public static webprintDesigner.Page Designer
   {
       get {

           if (DictionaryManager.frmDesigner == null)
           {
               frmDesigner = new webprintDesigner.Page();
           }
           
           return DictionaryManager.frmDesigner; 
       
       }
   }

   public static webprintDesigner.Login LoginControl
   {
       
       
       get {

           if (DictionaryManager.frmLogin == null)
           {
               frmLogin = new webprintDesigner.Login();
           }

           return DictionaryManager.frmLogin; 
       
       }
   }


   #region "Search Criteria"

   private static int _SearchMode;

   public static int SearchMode
   {
       get { return _SearchMode; }
       set { _SearchMode = value; }
   }
   

   private static int _SearchStatus;

   public static  int SearchStatus
   {
       get { return _SearchStatus; }
       set { _SearchStatus = value; }
   }

   private static int _SearchCategory;

   public static int SearchCategory
   {
       get { return _SearchCategory; }
       set { _SearchCategory = value; }
   }

   private static string _SearchKeyword;

   public static string SearchKeyword
   {
       get { return _SearchKeyword; }
       set { _SearchKeyword = value; }
   }

   private static int _CurrentPage;

   public static int CurrentPage
   {
       get { return _CurrentPage; }
       set { _CurrentPage = value; }
   }
   
   
   
   #endregion
 
   

   public static void ClearOthers(Pages p)
   {
       switch (p)
       { 
           case Pages.Designer:
               frmListView = null;
               frmLogin = null;
               frmEdit = null;
               break;
           case Pages.ListView:
               frmDesigner = null;
               frmLogin = null;
               frmEdit = null;
               break;
           case Pages.Login:
               frmListView = null;
               frmDesigner = null;
               frmEdit = null;
               break;
           case Pages.EditTemplate:
               frmListView = null;
               frmLogin = null;
               frmDesigner = null;
               frmEdit= null;
               break;
       }
   }
   public enum Pages
   { 
    Login,
    Designer,
    EditTemplate,
    ListView
   }

   public static void LogOut()
   {
      DictionaryManager.AppObjects.Clear();
      ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.LoginControl,Pages.Login);
   }
   public static void CreateNewTemplate()
   {
       DictionaryManager.AppObjects["mode"] = "new";
       DictionaryManager.AppObjects.Remove("productid");
       ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate,Pages.EditTemplate);
   }
   public static void Home()
   {
       if (AppObjects["mode"] != null)
       {
           AppObjects.Remove("mode");
       }
       ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.ListView,Pages.ListView);
   }
   public static void Initialize()
   {
      frmLogin = new webprintDesigner.Login();
   }

}

