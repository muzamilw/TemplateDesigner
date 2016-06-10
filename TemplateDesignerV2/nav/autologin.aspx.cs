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
    public partial class autologin : System.Web.UI.Page
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

             if (!string.IsNullOrEmpty(Request["username"]) && !string.IsNullOrEmpty(Request["userid"]) && !string.IsNullOrEmpty(Request["fullname"]) && !string.IsNullOrEmpty(Request["organisationid"]) && !string.IsNullOrEmpty(Request["customername"]))
            {

                 string usernmae = Request["username"];
                 string userid = Request["userid"];
                 string fullname = Request["fullname"];
                 string organisationid = Request["organisationid"];
                 string customername = Request["customername"];


                        HttpCookie cookie = new HttpCookie("username", usernmae);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                        cookie = new HttpCookie("userid", userid);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                        cookie = new HttpCookie("fullname", fullname);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                        cookie = new HttpCookie("role", "Customer");
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                        cookie = new HttpCookie("customerid", organisationid);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);

                       
                        cookie = new HttpCookie("customername", customername);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);
                        

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
                    //message invalid or expired token and also hide login control box
                }



            

            

            
            }

    

       
    }
}