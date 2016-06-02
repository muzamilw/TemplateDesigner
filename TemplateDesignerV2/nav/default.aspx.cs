using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using TemplateDesignerModelTypesV2;

namespace TemplateDesignerV2.nav
{
    public partial class _default : System.Web.UI.Page
    {
        public int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] != null)
                {
                    return Convert.ToInt32(ViewState["CurrentPage"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        public int TotalPages
        {
            get
            {
                if (ViewState["TotalPages"] != null)
                {
                    return Convert.ToInt32(ViewState["TotalPages"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["TotalPages"] = value;
                
            }
        }
        //int CurrentPage = 0;
        int PageSize = 12;
        //int PageCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
                if (Request.Cookies["selType"] != null)
                    cmbType.SelectedIndex =Convert.ToInt32( Request.Cookies["selType"].Value);

                if (Request.Cookies["selRegion"] != null)
                    cmbRegions.SelectedIndex =Convert.ToInt32(  Request.Cookies["selRegion"].Value);


                LoadDropDowns();

                if (Request.Cookies["selstatus"] != null)
                    cmbStatus.SelectedValue = Request.Cookies["selstatus"].Value;

                if (Request.Cookies["selkeyword"] != null)
                    txtKeyword.Text = Request.Cookies["selkeyword"].Value;

                if (Request.Cookies["selcategory"] != null)
                    cmbCategories.SelectedIndex =Convert.ToInt32(  Request.Cookies["selcategory"].Value);

                if (Request.Cookies["selcurpage"] != null)
                    this.CurrentPage = Convert.ToInt32(Request.Cookies["selcurpage"].Value);
                GetTemplates();
                if (cmbCategories.SelectedItem != null)
                    lblCategoryName.Text = cmbCategories.SelectedItem.Text;
                //
                
            }

            int userRole = getCustomerRole();             
            if (userRole == 1)
            {
                // mpc users    

            }
            else
            { // users
                divCategoryType.Visible = false;

                LoginStatus st = (LoginStatus)LoginView1.FindControl("LoginStatusDefault");
                st.LogoutPageUrl = "http://www.myprintcloud.com";
                st.LogoutAction = LogoutAction.Redirect;
            }
        }
        

        private void LoadDropDowns()
        {

            try
            {

                lblUser.Text = string.Format("welcome {0} , Role {1}", Request.Cookies["fullname"].Value, Request.Cookies["role"].Value);

                List<Status> lstItems = new List<Status>();
                //lstItems.Add("All");
              //  Status objApproved = new Status("Approved", "Approved");
                Status objDraft = new Status("Published", "Draft");
             ///   Status objRejected = new Status("Rejected", "Rejected");
             //   Status objSubmitted = new Status("Submitted", "Submitted");
              //  lstItems.Add(objApproved);
                lstItems.Add(objDraft);
                //if (Request.Cookies["role"].Value != null && Request.Cookies["role"].Value.ToLower() == "admin")
                //{
                //    lstItems.Remove("Draft");
                //}
            //    lstItems.Add(objRejected);
             //   lstItems.Add(objSubmitted);

                
                cmbStatus.DataSource = lstItems;
                cmbStatus.DataTextField = "Name";
                cmbStatus.DataValueField = "Value";
                cmbStatus.DataBind();

                //loading regions 
                if (Session["dropdownRegions"] == null)
                {
                   // int userRole = getCustomerRole();
                   // int privateCutCat = getPrivateCustomerCategory();
                    List<CategoryRegions> oRegions = new List<CategoryRegions>();
                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        oRegions = db.CategoryRegions.ToList();
                    }
                    Session["dropdownRegions"] = oRegions;
                    cmbRegions.DataSource = oRegions;
                    cmbRegions.DataTextField ="RegionName" ;
                    cmbRegions.DataValueField ="RegionID" ;
                    cmbRegions.DataBind();

                    //cmbRegions.SelectedIndex = 0;


                }
                else
                {

                    cmbRegions.DataSource = Session["dropdownRegions"];
                    cmbRegions.DataTextField = "RegionName";
                    cmbRegions.DataValueField = "RegionID";
                    cmbRegions.DataBind();

                }

                //loading types 
                if (Session["dropdownTypes"] == null)
                {
                    int userRole = getCustomerRole();
                    int privateCutCat = getPrivateCustomerCategory();
                    List<CategoryTypes> listType = new List<CategoryTypes>();
                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {
                        if (userRole == 1)
                        {
                            // mpc users
                            listType = db.CategoryTypes.Where(g => g.TypeID != privateCutCat).ToList();
                        }
                        else
                        { // users
                            listType = db.CategoryTypes.Where(g => g.TypeID == privateCutCat).ToList();
                            divCategoryType.Visible = false;
                        }
                    }
                    Session["dropdownTypes"] = listType;
                    cmbType.DataSource = listType;
                    cmbType.DataTextField = "TypeName";
                    cmbType.DataValueField = "TypeID";
                    cmbType.DataBind();

                   // cmbType.SelectedIndex = 0;


                }
                else
                {

                    cmbType.DataSource = Session["dropdownTypes"];
                    cmbType.DataTextField = "TypeName";
                    cmbType.DataValueField = "TypeID";
                    cmbType.DataBind();

                }

                //loading categories
                if (Session["cats"] == null)
                {
                    Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();
                    List<TemplateDesignerModelTypesV2.vw_ProductCategoriesLeafNodes> oAll = oSVC.GetCategories();
                    Session["cats"] = oAll;
                }

                List<TemplateDesignerModelTypesV2.vw_ProductCategoriesLeafNodes> oCatsAll = Session["cats"] as List<vw_ProductCategoriesLeafNodes>;
                List<TemplateDesignerModelTypesV2.vw_ProductCategoriesLeafNodes> oCats = new List<vw_ProductCategoriesLeafNodes>();
                int userRoleforCat = getCustomerRole();
                int privateCutCatforCat = getPrivateCustomerCategory();

                if (userRoleforCat == 1)
                {
                    // mpc users
                    foreach (var objCat in oCatsAll)
                    {
                        if ( objCat.CatagoryTypeID == Convert.ToInt32(cmbType.SelectedValue) && objCat.Height != null && objCat.Width!= null && objCat.RegionID == Convert.ToInt32(cmbRegions.SelectedValue)  ) 
                        {
                            oCats.Add(objCat);
                        }
                    }
                }
                else
                { // users
                    int customerid = Convert.ToInt32(Request.Cookies["customerid"].Value);
                    foreach (var objCat in oCatsAll)
                    {
                        if (objCat.CatagoryTypeID == privateCutCatforCat && objCat.CreatedBy == customerid && objCat.Height != null && objCat.Width != null && objCat.RegionID == Convert.ToInt32(cmbRegions.SelectedValue)) 
                        {
                            oCats.Add(objCat);
                        }
                    }
                }
                        
                cmbCategories.DataSource = oCats;
                cmbCategories.DataTextField = "CategoryName";
                cmbCategories.DataValueField = "ProductCategoryID";
                cmbCategories.DataBind();

                cmbCategories.SelectedIndex = 0;
                   






            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private void GetTemplates()
        {


            Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();


            int status = 0;
            if (cmbStatus != null)
            {
                if (cmbStatus.SelectedValue != null)
                {
                    switch (cmbStatus.SelectedValue.ToString())
                    {
                        case "All":

                            status = 0;

                            break;

                        case "Draft":

                            status = 1;

                            break;

                        case "Submitted":

                            status = 2;

                            break;
                        case "Approved":

                            status = 3;

                            break;
                        case "Rejected":

                            status = 4;

                            break;
                    }
                }
                else
                {
                    status = 0;
                }




                int userId = Convert.ToInt32(Request.Cookies["userid"].Value);

                int PageCount = 0;


                int customerid = Convert.ToInt32(Request.Cookies["customerid"].Value);

                string usertype = Request.Cookies["usertype"].Value;
                List<TemplateDesignerModelTypesV2.Templates> lTemplates = new List<Templates>();

                if (cmbCategories.SelectedValue != "")
                {
                    lTemplates = oSVC.GetTemplates(txtKeyword.Text, Convert.ToInt32(cmbCategories.SelectedValue), this.CurrentPage, PageSize, false, status, userId, Request.Cookies["role"].Value, out PageCount, customerid, usertype);
                }
                else
                {
                    lTemplates = null;
                }
                if (PageCount != 0)
                {
                    this.TotalPages = PageCount;
                    lblPager.Text = string.Format("Page {0} of Total {1} Pages ", CurrentPage + 1, PageCount);

                    // enabling next and  previous buttons
                    if (CurrentPage == 0)
                    {
                        btnPrviousPage.Enabled = false;
                        btnFirstPage.Enabled = false;
                    }
                    else
                    {
                        btnPrviousPage.Enabled = true;
                        btnFirstPage.Enabled = true;
                    }
                    if (CurrentPage + 1 == PageCount)
                    {
                        btnNextPage.Enabled = false;
                        btnLastPage.Enabled = false;
                    }
                    else
                    {
                        btnNextPage.Enabled = true;
                        btnLastPage.Enabled = true;
                    }



                    Random oRnd = new Random();

                    foreach (var item in lTemplates)
                    {
                        if (item.Thumbnail != null)
                        {
                            item.Thumbnail = "../designer/products/" + item.Thumbnail + "?rnd=" + oRnd.Next(1, 9999);
                            //if (!File.Exists(item.Thumbnail))
                            //{
                            //    item.Thumbnail = "~/assets/preview-not-available.jpg";
                            //}
                        }
                        else
                        {
                            item.Thumbnail = "~/assets/preview-not-available.jpg";
                        }
                    }
                    listTemplates.Visible = true;
                    msgBox.Visible = false;
                    listTemplates.DataSource = lTemplates;
                    listTemplates.DataBind();

                    
                }
                else
                {
                    lblPager.Text = string.Format("Page {0} of Total {1} Pages ", CurrentPage , PageCount);
                    msgBox.Visible = true;
                    listTemplates.Visible = false;
                    // enabling next and  previous buttons
                    if (CurrentPage == 0)
                    {
                        btnPrviousPage.Enabled = false;
                        btnFirstPage.Enabled = false;                  
                        btnNextPage.Enabled = false;
                        btnLastPage.Enabled = false;
                    }

                }
                //radDataPager1.ItemCount = (e.PageCount + 1) * PageSize;
                //radDataPager1.PageIndex = CurrentPage;

                // turning on the visisblity of previous and next btns for first time 
                if (btnNextPage.Visible == false)
                {
                    btnNextPage.Visible = true;
                }
                if (btnPrviousPage.Visible == false)
                {
                    btnPrviousPage.Visible = true;
                }
                if (btnFirstPage.Visible == false)
                {
                    btnFirstPage.Visible = true;
                }
                if (btnLastPage.Visible == false)
                {
                    btnLastPage.Visible = true;
                }


                HttpCookie cookie = new HttpCookie("selstatus", cmbStatus.SelectedValue.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                cookie = new HttpCookie("selkeyword", txtKeyword.Text);
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                cookie = new HttpCookie("selcategory", cmbCategories.SelectedIndex.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                cookie = new HttpCookie("selRegion", cmbRegions.SelectedIndex.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                cookie = new HttpCookie("selType", cmbType.SelectedIndex.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);


                cookie = new HttpCookie("selcurpage", this.CurrentPage.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);
                if(cmbCategories.SelectedItem != null)
                    lblCategoryName.Text = cmbCategories.SelectedItem.Text;
            }
        }


        //filter /search btn click 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            

            // setting the current page to 0 for every search
            this.CurrentPage = 0;
            GetTemplates();

           
        }

        protected void BtnPreviousPageClick(object sender, EventArgs e)
        {
            this.CurrentPage = this.CurrentPage - 1;
            GetTemplates();
        }

        protected void BtnNextPageClick(object sender, EventArgs e)
        {
            this.CurrentPage = this.CurrentPage + 1;
            GetTemplates();
        }

        protected void clickCreateTemplateBtn(object sender, EventArgs e)
        {
           Response.Redirect("~/nav/EditTemplate.aspx?mode=new");
        }
        protected void clickCreateCatBtn(object sender, EventArgs e)
        {
            Response.Redirect("~/nav/EditCategory.aspx");
        }


        protected void BtnCopyTemplateClick( string ProductIdArg)
        {
          
            //if (rbtnViewTemplates.IsChecked.Value == true)
            //{

                //ProductServiceReference.ProductServiceClient objSrv = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();
                //oSVC.CopyTemplateCompleted += new EventHandler<CopyTemplateCompletedEventArgs>(objSrv_CopyTemplateCompleted);
                oSVC.CopyTemplate(Convert.ToInt32(ProductIdArg), Convert.ToInt32(Request.Cookies["userid"].Value), Request.Cookies["fullname"].Value);
            //}
            //else if (rbtnViewMatchingSets.IsChecked.Value == true)
            //{

            //    if (cboMatchingSets.SelectedIndex != 0)
            //    {


            //        ProductServiceReference.ProductServiceClient objSrv = new webprintDesigner.ProductServiceReference.ProductServiceClient();
            //        objSrv.CopyMatchingSetCompleted += new EventHandler<CopyMatchingSetCompletedEventArgs>(objSrv_CopyMatchingSetCompleted);
            //        objSrv.CopyMatchingSetAsync(Convert.ToInt32(((Image)sender).Tag.ToString()), Convert.ToInt32(DictionaryManager.AppObjects["userid"]), DictionaryManager.AppObjects["fullname"].ToString());

            //    }

            //}
                Response.Redirect(Request.Url.AbsoluteUri);

        }

        protected void listTemplates_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Copy")
            {
               BtnCopyTemplateClick( (String)e.CommandArgument);

            }
            if (e.CommandName == "load")
            {
                LoadTemplatePage((String)e.CommandArgument);
            }
        }

        protected void BtnLastPageClick(object sender, EventArgs e)
        {
            this.CurrentPage = this.TotalPages-1;
            GetTemplates();
        }

        protected void BtnFirstPageClick(object sender, EventArgs e)
        {
            this.CurrentPage = 0;
            GetTemplates();
        }

        protected void LoadTemplatePage(string productID)
        {
            Response.Redirect("~/nav/EditTemplate.aspx?mode=edit&TemplateId=" + productID);
        }

        protected void listTemplates_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label LblID = e.Item.FindControl("lblRating") as Label;
                HtmlSelect dr = e.Item.FindControl("ratingControl") as HtmlSelect;
                if (LblID.Text != "")
                {
                    dr.SelectedIndex = Convert.ToInt32(LblID.Text);
                }
                //int itemNum = e.Item.ItemIndex + 1;

                //HtmlGenericControl divClear = e.Item.FindControl("divClear") as HtmlGenericControl;
                //// item Num = 3 6 9 12 15
                //// attach divClear class = clear
                //if (itemNum % 3 == 0)
                //{
                //    divClear.Style.Add("clear", "both");
                //}
            }
        }




        public string PubPrivIcon(object status)
        {
            if (status != null)
            {
                if (Convert.ToBoolean( status) == true)
                    return "../assets/Blue-Private-icon.png";
                else
                    return "../assets/Blue-Public-icon.png";
            }
            else
                return "../assets/Blue-Public-icon.png";
        }

        private int getPrivateCustomerCategory()
        {
            int CategoryTypeID = 0;
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                CategoryTypes catType = db.CategoryTypes.Where(g => g.TypeName == "Private Customers").SingleOrDefault();
                if (catType != null)
                {
                    CategoryTypeID = catType.TypeID;
                }
            }
            return CategoryTypeID;
        }
        private int getCustomerRole()
        {

            string role = "";
            if (Request.Cookies["role"].Value != null)
            {
                role = Request.Cookies["role"].Value;
            }
            if (role == "Admin" || role == "Designer")
            {
                return 1; // admin or mpc users
            }
            else if (role == "Customer")
            {
                return 2; // customers
            }
            return 0;
        }

        protected void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["cats"] == null)
            {
                Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();
                List<TemplateDesignerModelTypesV2.vw_ProductCategoriesLeafNodes> oAll = oSVC.GetCategories();
                Session["cats"] = oAll;
            }
            List<TemplateDesignerModelTypesV2.vw_ProductCategoriesLeafNodes> oCatsAll = Session["cats"] as List<vw_ProductCategoriesLeafNodes>;
            List<TemplateDesignerModelTypesV2.vw_ProductCategoriesLeafNodes> oCats = new List<vw_ProductCategoriesLeafNodes>();
            int userRoleforCat = getCustomerRole();
            int privateCutCatforCat = getPrivateCustomerCategory();

            if (userRoleforCat == 1)
            {
                // mpc users
                foreach (var objCat in oCatsAll)
                {
                    if (objCat.CatagoryTypeID == Convert.ToInt32(cmbType.SelectedValue) && objCat.Height != null && objCat.Width != null && objCat.RegionID == Convert.ToInt32(cmbRegions.SelectedValue))
                    {
                        oCats.Add(objCat);
                    }
                }
            }
            else
            { // users
                int customerid = Convert.ToInt32(Request.Cookies["customerid"].Value);
                foreach (var objCat in oCatsAll)
                {
                    if (objCat.CatagoryTypeID == privateCutCatforCat && objCat.CreatedBy == customerid && objCat.Height != null && objCat.Width != null && objCat.RegionID == Convert.ToInt32(cmbRegions.SelectedValue))
                    {
                        oCats.Add(objCat);
                    }
                }
            }

            cmbCategories.DataSource = oCats;
            cmbCategories.DataTextField = "CategoryName";
            cmbCategories.DataValueField = "ProductCategoryID";
            cmbCategories.DataBind();
            if (cmbCategories.Items.Count != 0)
            {
                cmbCategories.SelectedIndex = 0;
              
            }

            GetTemplates();
        }

    }
    public class Status
    {
        public string Value { get; set; }
        public string Name { get; set; }

        public Status(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}