using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


namespace TemplateDesigner
{
    public partial class Weblogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            LoginSvc.LoginServiceClient oClient = new LoginSvc.LoginServiceClient();
            LoginSvc.LoginInfo objLogin = oClient.Login(txtUserName.Text, txtPassword.Text);
            if (objLogin != null)
            {
                Session["username"] = objLogin.UserName;
                Session["userid"] = objLogin.UserID;
                Session["fullname"] = objLogin.FullName;
                Session["role"] = objLogin.RoleName;
                FormsAuthentication.RedirectFromLoginPage(objLogin.UserName, true);
            }
            else
            {

                lblError.Text = "Either username or Password does not match";
            }

        }
    }
}