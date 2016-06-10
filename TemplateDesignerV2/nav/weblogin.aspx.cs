using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Authentication;
using System.Web.Security;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;


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

           



            


            

            if (Request.Url.Host == "plocalhost")
            {


                LoginInfo objLogin = null;// oClient.Login("ahmad", "p@ssw0rd");

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
            LoginInfo objLogin = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://myprintcloud.com/api/login");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "?username=" + txtUsername.Text + "&password=" + txtPassword.Text;
                string responsestr = "";
                var response = client.GetAsync(url);
                if (response.Result.IsSuccessStatusCode)
                {
                    responsestr = response.Result.Content.ReadAsStringAsync().Result;
                    objLogin = JsonConvert.DeserializeObject<LoginInfo>(responsestr);
                }

            }

            //LoginInfo objLogin = Login(txtUsername.Text, txtPassword.Text);

           

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


    public class LoginInfo
    {
        public LoginInfo()
        {

        }

        internal LoginInfo(string UserID, string UserName, string FullName, string RoleName, LoginUserType type, int CID, string CName, bool? canPublish)
        {
            this._UserID = UserID;
            this._username = UserName;
            this._FullName = FullName;
            this._RoleName = RoleName;
            this._userType = type;
            this._CustomerID = CID;
            this._CustomerName = CName;
            if (canPublish.HasValue && canPublish.Value == true)
                this._CanPublishDesigns = true;
            else
                this._CanPublishDesigns = false;
        }


        private int _CustomerID;
        public int CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }

        private string _CustomerName;
        public string CustomerName { get { return _CustomerName; } set { _CustomerName = value; } }


        private LoginUserType _userType;
        public LoginUserType UserType { get { return _userType; } set { _userType = value; } }


        private bool _CanPublishDesigns;
        public bool CanPublishDesigns { get { return _CanPublishDesigns; } set { _CanPublishDesigns = value; } }


        public enum LoginUserType
        {
            MPCDesigner = 1,
            MPCAdmin = 2,
            CustomerUser = 3


        }

        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _FullName;

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        private string _RoleName;

        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }





    }

    public class ValidationInfo
    {
        public string userId { get; set; }
        public string CustomerID { get; set; }


        public string FullName { get; set; }

        public string Plan { get; set; }

        public string Email { get; set; }

        public Boolean IsTrial { get; set; }

        public int TrialCount { get; set; }

    }
}