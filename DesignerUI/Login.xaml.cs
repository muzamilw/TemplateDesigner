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
using System.Windows.Navigation;
using System.Windows.Browser;

namespace webprintDesigner
{
    public partial class Login : UserControl
    {
        public bool LoginStatus = false;
        public Login()
        {
            InitializeComponent();
            //PwdbAccessCode.Text = "ali";
            //pwdbVerifyCode.Password = "muz30";

            ProgressBar1.IsIndeterminate = false;
            ProgressPanel.Visibility = Visibility.Collapsed;



            PwdbAccessCode.Focus();
            Dispatcher.BeginInvoke(() => { PwdbAccessCode.Focus(); });

            if (HtmlPage.Document.DocumentUri.Host == "localhost")
            {
                LoginSVC.LoginServiceClient oClient = new LoginSVC.LoginServiceClient();
                oClient.LoginCompleted += new EventHandler<LoginSVC.LoginCompletedEventArgs>(oClient_LoginCompleted);
                oClient.LoginAsync("ahmad", "p@ssw0rd");
            }
           
        }

        private void UserBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            var text = PwdbAccessCode.Text;
            if (!string.IsNullOrEmpty(text) && text.Contains(";"))
            {
                var verifyCode = text.Substring(text.IndexOf(';') + 1);
                var accessCode = text.Remove(text.IndexOf(';'));
                PwdbAccessCode.Text = accessCode;
                pwdbVerifyCode.Password = verifyCode;
            }
        }

        private void btnSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProgressBar1.IsIndeterminate = true;
                ProgressPanel.Visibility = Visibility.Visible;

                LoginSVC.LoginServiceClient oClient = new LoginSVC.LoginServiceClient();
                oClient.LoginCompleted += new EventHandler<LoginSVC.LoginCompletedEventArgs>(oClient_LoginCompleted);
                oClient.LoginAsync(PwdbAccessCode.Text, pwdbVerifyCode.Password);
                
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        void oClient_LoginCompleted(object sender, LoginSVC.LoginCompletedEventArgs e)
        {
            try
            {
                ProgressBar1.IsIndeterminate = false;
                ProgressPanel.Visibility = Visibility.Collapsed;

                if (e.Result != null)
                {
                    webprintDesigner.LoginSVC.LoginInfo objLogin = e.Result;
                    DictionaryManager.AppObjects["username"] = objLogin.UserName;
                    DictionaryManager.AppObjects["userid"] = objLogin.UserID;
                    DictionaryManager.AppObjects["fullname"] = objLogin.FullName;
                    DictionaryManager.AppObjects["role"] = objLogin.RoleName;

                    ((UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.ListView,DictionaryManager.Pages.ListView);
                }
                else
                {
                    tbLoginMessage.Text = "Either username or Password does not match";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("::objSrv_GetCategoriesCompleted::" + ex.ToString());
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProgressBar1.IsIndeterminate = true;
                ProgressPanel.Visibility = Visibility.Visible;

                LoginSVC.LoginServiceClient oClient = new LoginSVC.LoginServiceClient();
                oClient.LoginCompleted += new EventHandler<LoginSVC.LoginCompletedEventArgs>(oClient_LoginCompleted);
                oClient.LoginAsync(PwdbAccessCode.Text, pwdbVerifyCode.Password);    
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button sb = (Button)sender;
            PreviewPane oPreview = null;

            switch (sb.Content.ToString())
            {
                case "card": oPreview = new PreviewPane(1615);break;
                case "comp": oPreview = new PreviewPane(1614); break;
                case "letterh": oPreview = new PreviewPane(1462); break;
                case "brochure": oPreview = new PreviewPane(1560); break;

                default:
                    break;
            }
            
            //Application.Current.Host.Content.IsFullScreen = !Application.Current.Host.Content.IsFullScreen;
            oPreview.Show();
        }
    }
}
