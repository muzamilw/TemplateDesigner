using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Authentication;
using System.Web.Security;

namespace TemplateDesignerV2.nav
{
    public partial class weblogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //check querystring for three parameters.
            //1 - encoded service path
            //2 - token
            //3
            if (Session["cats"] != null)
            {
                Session["cats"] = null;
            }
            if (Session["dropdownTypes"] != null)
            {
                Session["dropdownTypes"] = null;
            }
            if (Session["dropdownRegions"] != null)
            {
                Session["dropdownRegions"] = null;
            }
             HttpCookie cookies = new HttpCookie("selRegion", "0");
             cookies.Expires = DateTime.Now.AddDays(1);
             Response.AppendCookie(cookies);

             cookies = new HttpCookie("selType", "0");
             cookies.Expires = DateTime.Now.AddDays(1);
             Response.AppendCookie(cookies);

            if (!string.IsNullOrEmpty(Request.QueryString["p"]) && !string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                string token = Request.QueryString["t"];
                string path = Request.QueryString["p"];

                path += "services/AuthSvc.svc";

                MISAuthenticationSvc.AuthSvcClient oSvc = new MISAuthenticationSvc.AuthSvcClient("BasicHttpBinding_IAuthSvc",path);


                var result = oSvc.Login(token);
                if (result != null)
                {
                    if (result.tbl_company_sites.CustomerID != null)
                    {

                        HttpCookie cookie = new HttpCookie("username", result.UserName);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                        cookie = new HttpCookie("userid", result.SystemUserID.ToString());
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                        cookie = new HttpCookie("fullname", result.FullName);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                        cookie = new HttpCookie("role", "Customer");
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                        cookie = new HttpCookie("customerid", result.tbl_company_sites.CustomerID.ToString());
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                        if (result.tbl_company_sites.CompanySiteName != null)
                        {
                            cookie = new HttpCookie("customername", result.tbl_company_sites.CompanySiteName.ToString());
                            cookie.Expires = DateTime.Now.AddDays(1);
                            Response.AppendCookie(cookie);
                        }

                        cookie = new HttpCookie("usertype", "CustomerUser");
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);


                        cookie = new HttpCookie("canpublishdesigns", "true");
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);


                        FormsAuthentication.SetAuthCookie(txtUsername.Text, true);
                        Response.Redirect("~/nav/default.aspx");
                        //FormsAuthentication.RedirectFromLoginPage(result.UserName, true);
                    }
                    else
                    {
                        divLoginBoxContainer.Visible = false;
                        divInvalidParameteredLogin.Visible = true;
                    }
                }
                else
                {
                    divLoginBoxContainer.Visible = false;
                    divInvalidParameteredLogin.Visible = true;
                    //message invalid or expired token and also hide login control box
                }



            }


            

            if (Request.Url.Host == "plocalhost")
            {
                LoginSVC.LoginServiceClient oClient = new LoginSVC.LoginServiceClient();

                LoginSVC.LoginInfo objLogin = oClient.Login("ahmad", "p@ssw0rd");

                if (objLogin != null)
                {
                    
                    

                    HttpCookie cookie = new HttpCookie("username",objLogin.UserName);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie); 

                    cookie = new HttpCookie("userid",objLogin.UserID.ToString());
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie); 

                    cookie = new HttpCookie("fullname",objLogin.FullName);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie); 

                    cookie = new HttpCookie("role",objLogin.RoleName);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie);

                    cookie = new HttpCookie("customerid", objLogin.CustomerID.ToString());
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie);

                    cookie = new HttpCookie("customername", objLogin.CustomerName.ToString());
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie);


                    cookie = new HttpCookie("usertype", objLogin.UserType.ToString());
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie);


                    cookie = new HttpCookie("canpublishdesigns", objLogin.CanPublishDesigns.ToString());
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie); 


                    //FormsAuthentication.SetAuthCookie("ahmad", true);
                    FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, true);
                }
                else
                {
                    lblMessage.Text = "Either username or Password does not match";
                }
            }

        }

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {

            LoginSVC.LoginServiceClient oClient = new LoginSVC.LoginServiceClient();

            LoginSVC.LoginInfo objLogin = oClient.Login(txtUsername.Text, txtPassword.Text);

            if (objLogin != null)
            {

                HttpCookie cookie = new HttpCookie("username", objLogin.UserName);
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                cookie = new HttpCookie("userid", objLogin.UserID.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                cookie = new HttpCookie("fullname", objLogin.FullName);
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                cookie = new HttpCookie("role", objLogin.RoleName);
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                cookie = new HttpCookie("customerid", objLogin.CustomerID.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                if (objLogin.CustomerName != null)
                {
                    cookie = new HttpCookie("customername", objLogin.CustomerName.ToString());
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie);
                }

                cookie = new HttpCookie("usertype", objLogin.UserType.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);


                cookie = new HttpCookie("canpublishdesigns", objLogin.CanPublishDesigns.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie); 


                //FormsAuthentication.SetAuthCookie(txtUsername.Text, true);
                FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, true);
            }
            else
            {
                lblMessage.Text = "Either username or Password does not match";
            }
        }

       
    }
}