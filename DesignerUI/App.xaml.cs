using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace webprintDesigner
{
    public partial class App : Application
    {
        public static webprintDesigner.ProductServiceReference.DesignerModes DesignerMode = 0;
        
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            

            if (e.InitParams != null && e.InitParams.Count > 0)
            {
                if (e.InitParams["xp"] != null && e.InitParams["xp"].ToString() == "2")
                {
                    DesignerMode = ProductServiceReference.DesignerModes.CreatorMode;
                    //opening up the login page
                    this.RootVisual = new UserControlContainer();
                    DictionaryManager.Initialize();
                    ((UserControlContainer)this.RootVisual).SwitchControl(DictionaryManager.LoginControl, DictionaryManager.Pages.Login);
                }
                else if (e.InitParams["xp"] != null && e.InitParams["xp"].ToString() == "1")
                {
                    DesignerMode = ProductServiceReference.DesignerModes.SimpleEndUser;
                    //showing directly the designer
                    this.RootVisual = new Page(e.InitParams);
                }
                else if (e.InitParams["xp"] != null && e.InitParams["xp"].ToString() == "3")
                {
                    DesignerMode = ProductServiceReference.DesignerModes.AdvancedEndUser;
                    //showing directly the designer
                    this.RootVisual = new Page(e.InitParams);
                }

                else if (e.InitParams["xp"] != null && e.InitParams["xp"].ToString() == "5")   //corporate user
                {
                    DesignerMode = ProductServiceReference.DesignerModes.CorporateMode;
                    //showing directly the designer
                    this.RootVisual = new Page(e.InitParams);
                }
                else if (e.InitParams["xp"] != null && e.InitParams["xp"].ToString() == "4")   //annonemous user mode
                {
                    DesignerMode = ProductServiceReference.DesignerModes.AnnanomousMode;
                    //opening up the templatest list without login in readonly mode.
                    this.RootVisual = new UserControlContainer();
                    DictionaryManager.Initialize();
                    ((UserControlContainer)this.RootVisual).SwitchControl(DictionaryManager.ListView, DictionaryManager.Pages.ListView);
                }

            }

        }
         

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }


    //public enum DesignerModes
    //{
    //    SimpleEndUser = 1,
    //    AdvancedEndUser = 3,
    //    CreatorMode = 2,
    //    AnnanomousMode = 4,
    //    CorporateMode = 5
    //}
}
