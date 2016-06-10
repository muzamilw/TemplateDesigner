using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplateDesignerModelV2;
using System.Drawing;

namespace TemplateDesignerV2.nav
{
    public partial class EditCategory : System.Web.UI.Page
    {
        public List<tbl_ProductCategory> CategoriesList
        {
            get
            {
                if (ViewState["CategoriesList"] != null)
                {
                    return ViewState["CategoriesList"] as List<tbl_ProductCategory>;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["CategoriesList"] = value;
            }
        }

        public List<CategoryRegions> CategoryRegionsList
        {
            get
            {
                if (ViewState["CategoryRegionsList"] != null)
                {
                    return ViewState["CategoryRegionsList"] as List<CategoryRegions>;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["CategoryRegionsList"] = value;
            }
        }
        public List<CategoryTypes> CategoryTypeList
        {
            get
            {
                if (ViewState["CategoryTypeList"] != null)
                {
                    return ViewState["CategoryTypeList"] as List<CategoryTypes>;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["CategoryTypeList"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCatagory();
                GetRegions();
                GetCategoryTypes();
                lblUser.Text = string.Format("welcome {0} , Role {1}", Request.Cookies["fullname"].Value, Request.Cookies["role"].Value);

                textboxZoomFactor.Attributes.Add("onblur", "updateValue()");
                textBoxWidth.Attributes.Add("onblur", "updateValue()");
                textboxHeight.Attributes.Add("onblur", "updateValue()");
                if (getCustomerRole() == 2)
                {
                    LoginStatus st = (LoginStatus)LoginView1.FindControl("LoginStatus1");
                    st.LogoutPageUrl = "http://www.myprintcloud.com";
                    st.LogoutAction = LogoutAction.Redirect;
                }
            }
        }
        
        private void GetRegions()
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                List<CategoryRegions> reg = db.CategoryRegions.ToList();
                dropDownRegion.DataSource = reg;
                dropDownRegion.DataTextField = "RegionName";
                dropDownRegion.DataValueField = "RegionID";
                dropDownRegion.DataBind();
            }
        }

        private void GetCategoryTypes()
        {
            // 1 for customer 
            // 2 for users
            int type = getCustomerRole();
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                List<CategoryTypes> types = new List<CategoryTypes>();
                if (type == 1)
                {
                    int privateCat = getPrivateCustomerCategory();
                    types = db.CategoryTypes.Where(g => g.TypeID != privateCat).ToList();
                }
                else
                {
                    types = db.CategoryTypes.ToList();
                }
                dropDownCategoryType.DataSource = types;
                dropDownCategoryType.DataTextField = "TypeName";
                dropDownCategoryType.DataValueField = "TypeID";
                dropDownCategoryType.DataBind();
            }
        }

        private void GetCatagory()
        {
            TreeViewCategories.Nodes.Clear();
            int role = getCustomerRole();
            int customerid = Convert.ToInt32(Request.Cookies["customerid"].Value);
            List<tbl_ProductCategory> ctr;
           
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                int privateCustomerCatID = getPrivateCustomerCategory();
                if (role == 1)
                {
                    
                    ctr = db.tbl_ProductCategory.Where(g => g.ParentCategoryID == null && g.CatagoryTypeID != privateCustomerCatID).OrderBy(c => c.CategoryName).ToList();
                }
                else
                {
                    ctr = db.tbl_ProductCategory.Where(g => g.CreatedBy == customerid && g.ParentCategoryID == null && g.CatagoryTypeID == privateCustomerCatID).OrderBy(c => c.CategoryName).ToList();
                }

                if (ctr.Count == 0)
                {
                    lblNoCategories.Visible = true;
                }
                else
                {
                    foreach (var obj in ctr)
                    {
                        TreeNode root = new TreeNode();
                        root.Text = obj.CategoryName;
                        root.Value = obj.ProductCategoryID.ToString();
                        List<TreeNode> childs = GetCategoryChilds(obj.ProductCategoryID);
                        foreach (var child in childs)
                        {
                            root.ChildNodes.Add(child);
                        }
                        TreeViewCategories.Nodes.Add(root);

                    }
                    TreeViewCategories.CollapseAll();
                }
            }
        }
        //recursive function to get category Child Nodes
        private List<TreeNode> GetCategoryChilds(int parentCategoryID)
        {
            List<TreeNode> listToReturn = new List<TreeNode>();
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                List<tbl_ProductCategory> childs = db.tbl_ProductCategory.Where(g => g.ParentCategoryID == parentCategoryID).OrderBy(c => c.CategoryName).ToList();
                foreach (var child in childs)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = child.CategoryName;
                    childNode.Value = child.ProductCategoryID.ToString();
                    List<TreeNode> childLeafs = GetCategoryChilds(child.ProductCategoryID);
                    if (childLeafs != null)
                    {
                        foreach (var objChildLeafs in childLeafs)
                        {
                            childNode.ChildNodes.Add(objChildLeafs);
                        }
                    }
                    listToReturn.Add(childNode);
                }
            }

            return listToReturn;
        }
       
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        private tbl_ProductCategory GetCategoryDetail(int productCatID)
        {
            tbl_ProductCategory detail = new tbl_ProductCategory();
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                detail = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == productCatID).SingleOrDefault();
            }
            
            return detail;
        }

        private void showParentCategories(int productCatID)
        {
            int role = getCustomerRole();

            int customerid = Convert.ToInt32(Request.Cookies["customerid"].Value);
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                int privateCustomerCatID = getPrivateCustomerCategory();
                List<tbl_ProductCategory> allProducts = new List<tbl_ProductCategory>();
                if (role == 1)
                {
                    allProducts = db.tbl_ProductCategory.Where(g => g.CatagoryTypeID != privateCustomerCatID).OrderBy(c => c.CategoryName).ToList();
                }
                else
                {
                    allProducts = db.tbl_ProductCategory.Where(g => g.CreatedBy == customerid && g.CatagoryTypeID == privateCustomerCatID).OrderBy(c => c.CategoryName).ToList();
                }


                List<tbl_ProductCategory> canBeParents = new List<tbl_ProductCategory>();

                // adding no parent node 
                tbl_ProductCategory objectNoParent = new tbl_ProductCategory();

                objectNoParent.CategoryName = "No Parent ";
                objectNoParent.ProductCategoryID = -1;

                canBeParents.Add(objectNoParent);

                foreach (var obj in allProducts)
                {
                    if (obj.ProductCategoryID != productCatID && !chkIfIsChild(allProducts,obj,productCatID))
                    {
                        canBeParents.Add(obj);
                    }
                }

                dropDownParentCategory.DataSource = canBeParents;
                dropDownParentCategory.DataTextField = "CategoryName";
                dropDownParentCategory.DataValueField = "ProductCategoryID";
                dropDownParentCategory.DataBind();
            }
        }

        protected bool chkIfIsChild(List<tbl_ProductCategory> listCat,tbl_ProductCategory  categoryToChk, int parentCatId)
        {
            if (categoryToChk.ParentCategoryID == parentCatId)
            {
                return true;
            }
            else if (categoryToChk.ParentCategoryID == null)
            {
                return false;
            }
            else
            {
                foreach(var obj in listCat)
                {
                    if (obj.ProductCategoryID == categoryToChk.ParentCategoryID)
                    {
                        return chkIfIsChild(listCat, obj, parentCatId);                        
                    }
                }
                
            }
            return true;
        }

        //bool Block = false;
        //protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        //{
        //    if (Block == true)  // stop recursive re-entrancy
        //        return;

        //    Block = true;
        //    TreeViewCategories.CollapseAll();
        //    TreeViewCategories.HoverNodeStyle.ForeColor =Color.Firebrick;
        //    e.Node.Expand();
        //    Block = false;
        //}
        protected void TreeViewCategories_SelectedNodeChanged(object sender, EventArgs e)
        {
            int role = getCustomerRole();
            int productCategoryID =Convert.ToInt32( TreeViewCategories.SelectedNode.Value);
            divSavedSucessMsg.Visible = false;
            divMessageDel.Visible = false;
            lblFoldLineMsg.Visible = false;
            btnAddCategory.Visible = false;
            btnUpdateCategory.Visible = false;
            btnAddCategory.Visible = false;
            btnDeleteCategory.Visible = true;
            btnCancelCategoryUpdate.Visible = true;
            btnEditCategory.Visible = true;
            tbl_ProductCategory objCat = GetCategoryDetail(productCategoryID);
            if (objCat != null)
            {
                txtCategoryName.Text = objCat.CategoryName;
                dropDownCategoryType.SelectedValue = objCat.CatagoryTypeID.ToString();
                dropDownRegion.SelectedValue = objCat.RegionID.ToString();
                if (objCat.ApplySizeRestrictions == true)
                {
                    radioBtnApplySizeFalse.Checked = false;
                    radioBtnApplySizeTrue.Checked = true;

                }
                else
                {
                    radioBtnApplySizeTrue.Checked = false;
                    radioBtnApplySizeFalse.Checked = true;
                }
                lblCategoryID.Text = objCat.ProductCategoryID.ToString();
                textboxHeight.Text = objCat.HeightRestriction.ToString();
                textBoxWidth.Text = objCat.WidthRestriction.ToString();
                textboxZoomFactor.Text = objCat.ScaleFactor.ToString();

                if (objCat.HeightRestriction != null && objCat.WidthRestriction != null)
                {
                    textBoxPDFWidth.Text = (objCat.WidthRestriction * Convert.ToDouble(objCat.ScaleFactor)).ToString();
                    textBoxPDFHeight.Text = (objCat.HeightRestriction * Convert.ToDouble(objCat.ScaleFactor)).ToString();
                }
                else
                {
                    textBoxPDFWidth.Text = "";
                    textBoxPDFHeight.Text = "";
                }
                //binding parentCategories
                showParentCategories(productCategoryID);
                //selecting parent categories 
                if (objCat.ParentCategoryID == null)
                {
                    dropDownParentCategory.SelectedValue = "-1";
                }
                else
                {
                    dropDownParentCategory.SelectedValue = objCat.ParentCategoryID.ToString();
                }
                if (objCat.ApplyFoldLines == true)
                {
                    radioBtnApplyFoldNo.Checked = false;
                    radioBtnApplyFoldYes.Checked = true;
                }
                else
                {
                    radioBtnApplyFoldYes.Checked = false;
                    radioBtnApplyFoldNo.Checked = true;
                }
                //textboxCategoryID.Text = objCat.ProductCategoryID.ToString();
                
                // disabling all controls 
                txtCategoryName.Enabled = false;
                dropDownParentCategory.Enabled = false;
                dropDownCategoryType.Enabled = false;
                dropDownRegion.Enabled = false;
                textBoxPDFWidth.Enabled = false;
                textBoxPDFHeight.Enabled = false;
                radioBtnApplySizeFalse.Enabled = false;
                radioBtnApplySizeTrue.Enabled = false;
                textBoxWidth.Enabled = false;
                textboxHeight.Enabled = false;
                textboxZoomFactor.Enabled = false;
                radioBtnApplyFoldNo.Enabled = false;
                radioBtnApplyFoldYes.Enabled = false;
                btnEditCategory.Visible = true;
                btnUpdateCategory.Visible = false;
                addFoldLinesData(objCat.ProductCategoryID);
                TreeViewCategories.SelectedNodeStyle.ForeColor = Color.Firebrick;
                divCategoryDetail.Visible = true;
                divRepeaterContainer.Visible = false;
                divTrifoldLinesTxt.Visible = false;

                if (role != 1)
                {
                    divCatType.Visible = false;
                }
            }
        }
        
        protected void addFoldLinesData(int productCatID)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                List<tbl_ProductCategoryFoldLines> listFoldLines = db.tbl_ProductCategoryFoldLines.Where(g => g.ProductCategoryID == productCatID).ToList();
                repeaterFoldLines.DataSource = listFoldLines;
                repeaterFoldLines.DataBind();
            }
        }
        protected void Pages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "updateFoldLine")
            {
                int FoldLineID = Convert.ToInt32((String)e.CommandArgument);
                DropDownList dropDownOrientation = e.Item.FindControl("dropDownOrientation") as DropDownList;
                TextBox txtBoxOffsetFromMargin = e.Item.FindControl("txtBoxOffsetFromMargin") as TextBox;
                 double temp;
                 if (double.TryParse(txtBoxOffsetFromMargin.Text, out temp))
                 {
                     using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                     {
                         tbl_ProductCategoryFoldLines objFold = db.tbl_ProductCategoryFoldLines.Where(g => g.FoldLineID == FoldLineID).SingleOrDefault();
                         if (objFold != null)
                         {
                             objFold.FoldLineOffsetFromOrigin = Convert.ToDouble(txtBoxOffsetFromMargin.Text);
                             if (dropDownOrientation.SelectedValue == "Horizontal")
                             {
                                 objFold.FoldLineOrientation = false;
                             }
                             else
                             {
                                 objFold.FoldLineOrientation = true;
                             }
                             db.SaveChanges();
                         }
                     }
                     addFoldLinesData(Convert.ToInt32(lblCategoryID.Text));
                     ShowMessageOnClient("Updated Successfully");
                 }
                 else
                 {
                     ShowMessageOnClient("Invalid Offset Value");
                 }
            } else if  (e.CommandName == "deleteFoldline")
            {
                int FoldLineID = Convert.ToInt32((String)e.CommandArgument);
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    tbl_ProductCategoryFoldLines objFold = db.tbl_ProductCategoryFoldLines.Where(g => g.FoldLineID == FoldLineID).SingleOrDefault();
                    if (objFold != null)
                    {
                        db.tbl_ProductCategoryFoldLines.Remove(objFold);
                        db.SaveChanges();
                    }
                }
                addFoldLinesData(Convert.ToInt32(lblCategoryID.Text));
            }  
          
        }
   
        protected void repeaterFoldLines_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblFoldLineOrientation = e.Item.FindControl("lblFoldLineOrientation") as Label;

                //RadioButton radioBtnVertical = e.Item.FindControl("radioBtnVertical") as RadioButton;
                //RadioButton radioBtnHorizontal = e.Item.FindControl("radioBtnHorizontal") as RadioButton;
                DropDownList dr = e.Item.FindControl("dropDownOrientation") as DropDownList;
                dr.Items.Add("Verticle");
                dr.Items.Add("Horizontal");
                if (lblFoldLineOrientation.Text == "False")
                {
                    dr.SelectedValue = "Horizontal";
                }
                else
                {
                    dr.SelectedValue = "Verticle";
                }
             
            }

        }
        
        protected void btnCancelCategoryUpdate_Click(object sender, EventArgs e)
        {
            divCategoryDetail.Visible = false;
            divSavedSucessMsg.Visible = false;
            if (TreeViewCategories.SelectedNode != null)
            {
                TreeViewCategories.SelectedNode.Selected = false;
            }
        }

        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int productCatID =Convert.ToInt32( lblCategoryID.Text);
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                //int templateCount = db.Templates.Count(g => g.ProductCategoryID == productCatID);
                int catCount = db.tbl_ProductCategory.Count(g => g.ParentCategoryID == productCatID);

                if (catCount == 0)
                {
                    tbl_ProductCategory cat = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == productCatID).SingleOrDefault();
                    if (cat != null)
                    {
                        List<Templates> listTemplates = db.Templates.Where(g => g.ProductCategoryID == productCatID).ToList();
                        Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();
                        foreach (var template in listTemplates)
                        {
                            int CategoryID;

                            oSVC.DeleteTemplate(template.ProductID, out CategoryID);
                        }
                        oSVC.DeleteCategory(productCatID);
                       // db.tbl_ProductCategory.Remove(cat);
                       // db.SaveChanges();
                        divCategoryDetail.Visible = false;
                        divMessageDel.InnerText = "Category Deleted Sucessfully.";
                        divMessageDel.Visible = true;
                    }
                    
                    // update tree 
                    GetCatagory();

                    if (Session["cats"] != null)
                    {
                        Session["cats"] = null;
                    }
                }
                else
                {
                   // if (templateCount > 0 && catCount > 0)
                    //{
                    //    divMessageDel.InnerText = "Category Cannot be deleted because it has templates and sub categories in it.";
                    //}
                    //else if (templateCount > 0)
                    //{
                    //    divMessageDel.InnerText = "Category Cannot be deleted because it has templates in it.";
                    //}
                    //else if (catCount > 0)
                    //{
                        divMessageDel.InnerText = "Category Cannot be deleted because it has sub categories in it.";
                    //}
                    divMessageDel.Visible = true;
                }

            }
        }

        protected void btnEditCategory_Click(object sender, EventArgs e)
        {
            string role = "";
            if (Request.Cookies["role"].Value != null)
            {
                role = Request.Cookies["role"].Value;
            }
            divSavedSucessMsg.Visible = false;
            divMessageDel.Visible = false;
            lblFoldLineMsg.Visible = false;
            txtCategoryName.Enabled = true;
            dropDownParentCategory.Enabled = true;
            if (role == "Designer")
            {
                dropDownCategoryType.Enabled = true;
            }
            textBoxPDFWidth.Enabled = true;
            textBoxPDFHeight.Enabled = true;
            dropDownRegion.Enabled = true;
            radioBtnApplySizeFalse.Enabled = true;
            radioBtnApplySizeTrue.Enabled = true;
            //if (radioBtnApplySizeTrue.Checked == true)
            //{
                textBoxWidth.Enabled = true;
                textboxHeight.Enabled = true;
            //}
            textboxZoomFactor.Enabled = true;
            radioBtnApplyFoldNo.Enabled = true;
            radioBtnApplyFoldYes.Enabled = true;
            btnEditCategory.Visible = false;
            btnUpdateCategory.Visible = true;
            divRepeaterContainer.Visible = true;
            divTrifoldLinesTxt.Visible = true;
            if (radioBtnApplyFoldYes.Checked == true)
            {
                foldLinesContainer.Style.Add("display", "block");
            }
            else
            {
                foldLinesContainer.Style.Add("display", "none");
            }
        }

        protected void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            bool chk = true;
            if (textBoxWidth.Text == "" || textboxHeight.Text == "")
            {
                chk = false;
            }
            if (radioBtnApplySizeFalse.Checked == true)
            {
                chk = true;
            }
            if (chk)
            {
                int catID = Convert.ToInt32(lblCategoryID.Text);

                string oldName = "";
                string oldParentCatID = "";
                string newParentCatID = "";
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    tbl_ProductCategory objCat = db.tbl_ProductCategory.Where(g => g.ProductCategoryID == catID).SingleOrDefault();
                    if (objCat != null)
                    {
                        oldName = objCat.CategoryName;
                        if (objCat.ParentCategoryID == null)
                        {
                            oldParentCatID = "-1";
                        }
                        else
                        {
                            oldParentCatID = objCat.ParentCategoryID.ToString();
                        }

                        objCat.CategoryName = txtCategoryName.Text;
                        objCat.CatagoryTypeID = Convert.ToInt32(dropDownCategoryType.SelectedValue);
                        objCat.RegionID = Convert.ToInt32(dropDownRegion.SelectedValue);
                        if (radioBtnApplySizeTrue.Checked == true)
                        {
                            objCat.ApplySizeRestrictions = true;
                        }
                        if (radioBtnApplySizeFalse.Checked == true)
                        {
                            objCat.ApplySizeRestrictions = false;
                        }
                        double temp;
                        if (double.TryParse(textboxHeight.Text, out temp))
                        {
                            objCat.HeightRestriction = Convert.ToDouble(textboxHeight.Text);
                        }
                        else
                        {
                            objCat.HeightRestriction = null;
                        }
                        if (double.TryParse(textBoxWidth.Text, out temp))
                        {
                            objCat.WidthRestriction = Convert.ToDouble(textBoxWidth.Text);
                        }
                        else
                        {
                            objCat.WidthRestriction = null;
                        }
                        if (double.TryParse(textboxZoomFactor.Text, out temp))
                        {
                            objCat.ScaleFactor =Math.Round( Convert.ToDecimal(textboxZoomFactor.Text),2);
                        }
                        else
                        {
                            objCat.ScaleFactor = 1;
                        }

                        if (dropDownParentCategory.SelectedValue == "-1")
                        {
                            objCat.ParentCategoryID = null;
                            newParentCatID = "-1";
                        }
                        else
                        {
                            objCat.ParentCategoryID = Convert.ToInt32(dropDownParentCategory.SelectedValue);
                            newParentCatID = dropDownParentCategory.SelectedValue;
                        }
                        if (radioBtnApplyFoldYes.Checked == true)
                        {
                            objCat.ApplyFoldLines = true;

                        }
                        if (radioBtnApplyFoldNo.Checked == true)
                        {
                            objCat.ApplyFoldLines = false;
                        }
                        divSavedSucessMsg.Visible = true;
                        db.SaveChanges();
                    }
                    if (oldName != objCat.CategoryName || newParentCatID != oldParentCatID)
                    {
                        // update tree 
                        GetCatagory();
                        divCategoryDetail.Visible = false;
                    }


                    if (Session["cats"] != null)
                    {
                        Session["cats"] = null;
                    }
                }
                // disabling all controls 
                txtCategoryName.Enabled = false;
                dropDownParentCategory.Enabled = false;
                dropDownCategoryType.Enabled = false;
                dropDownRegion.Enabled = false;
                radioBtnApplySizeFalse.Enabled = false;
                radioBtnApplySizeTrue.Enabled = false;
                textBoxWidth.Enabled = false;
                textboxHeight.Enabled = false;
                textboxZoomFactor.Enabled = false;
                radioBtnApplyFoldNo.Enabled = false;
                radioBtnApplyFoldYes.Enabled = false;
                btnEditCategory.Visible = true;
                btnUpdateCategory.Visible = false;
                textBoxPDFWidth.Enabled = false;
                textBoxPDFHeight.Enabled = false;
                TreeViewCategories.SelectedNodeStyle.ForeColor = Color.Firebrick;
                divCategoryDetail.Visible = true;
                divRepeaterContainer.Visible = false;
                divTrifoldLinesTxt.Visible = false;


            }  
            else
            {
                ShowMessageOnClient("You must enter height and width if you want to apply size restriction");
            }
        }

        protected void btnCancelFoldLineClick_click(object sender, EventArgs e)
        {
            txtBoxOffsetFromMarginNew.Text = "";
            dropDownOrientationNewFold.SelectedIndex = 0;
            divAddFoldLine.Visible = false;
            btnAddnewFoldLine.Visible = true;
        }

        protected void btnAddnewFoldLine_clicked(object sender, EventArgs e)
        {
            divAddFoldLine.Visible = true;
            btnAddnewFoldLine.Visible = false;
            dropDownOrientationNewFold.Items.Clear();
            dropDownOrientationNewFold.Items.Add("Verticle");
            dropDownOrientationNewFold.Items.Add("Horizontal");
        }

        protected void btnSaveNewFoldLine_click(object sender, EventArgs e)
        {
            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            {
                int catID = Convert.ToInt32(lblCategoryID.Text);
                tbl_ProductCategoryFoldLines objLine = new tbl_ProductCategoryFoldLines();
                objLine.ProductCategoryID = catID;
                objLine.FoldLineOffsetFromOrigin = Convert.ToDouble(txtBoxOffsetFromMarginNew.Text);

                if (dropDownOrientationNewFold.SelectedValue == "Horizontal")
                {
                    objLine.FoldLineOrientation = false;
                }
                else
                {
                    objLine.FoldLineOrientation = true;
                }
                db.tbl_ProductCategoryFoldLines.Add(objLine);
                db.SaveChanges();
                addFoldLinesData(Convert.ToInt32(lblCategoryID.Text));
                divAddFoldLine.Visible = false;
                btnAddnewFoldLine.Visible = true;
                txtBoxOffsetFromMarginNew.Text = "";
                dropDownOrientationNewFold.SelectedIndex = 0;
            }
            
        }

        public  void ShowMessageOnClient(string message) 
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "alert('"+ message+"');", true); 
        }

        protected void BtnAddNewCategory_click(object sender, EventArgs e)
        {
            lblNoCategories.Visible = false;
            int role = getCustomerRole();
            divSavedSucessMsg.Visible = false;
            divMessageDel.Visible = false;

            txtCategoryName.Text ="";
            if (role == 1)
            {
                dropDownCategoryType.SelectedIndex = 0;
            }
            else
            {
                dropDownCategoryType.SelectedIndex = 2;
                divCatType.Visible = false;
                //dropDownCategoryType.Enabled = false;
            }
            dropDownRegion.SelectedIndex = 0;
            
            radioBtnApplySizeTrue.Checked = false;
            radioBtnApplySizeFalse.Checked = true;
            
            lblCategoryID.Text = "-1";
            textboxHeight.Text = "";
            textBoxWidth.Text = "";
            textboxZoomFactor.Text = "1";
            //binding parentCategories
            showParentCategories(-1);
            //selecting parent categories 

            dropDownParentCategory.SelectedValue = "-1";
           
            radioBtnApplyFoldYes.Checked = false;
            radioBtnApplyFoldNo.Checked = true;
            //textboxCategoryID.Text = objCat.ProductCategoryID.ToString();

            // disabling all controls 
            txtCategoryName.Enabled = true;
            dropDownParentCategory.Enabled = true;
            dropDownCategoryType.Enabled = true;
            dropDownRegion.Enabled = true;
            radioBtnApplySizeFalse.Enabled = true;
            radioBtnApplySizeTrue.Enabled = true;
            textBoxWidth.Enabled = true;
            textboxHeight.Enabled = true;
            textboxZoomFactor.Enabled = true;
            radioBtnApplyFoldNo.Enabled = true;
            radioBtnApplyFoldYes.Enabled = true;
            lblFoldLineMsg.Visible = true;
            btnEditCategory.Visible = false;
            btnUpdateCategory.Visible = false;
            btnAddCategory.Visible = true;
            textBoxPDFWidth.Enabled = false;
            textBoxPDFHeight.Enabled = false;
            TreeViewCategories.SelectedNodeStyle.ForeColor = Color.Firebrick;
            divCategoryDetail.Visible = true;
            divRepeaterContainer.Visible = false;
            divTrifoldLinesTxt.Visible = false;
            btnDeleteCategory.Visible = false;
           // btnCancelCategoryUpdate.Visible = false;
            
        }

        protected void btnCreateCat_click(object sender, EventArgs e)
        {
            bool chk = true;
            if (textBoxWidth.Text == "" || textboxHeight.Text == "")
            {
                chk = false;
            }
            if (radioBtnApplySizeFalse.Checked == true)
            {
                chk = true;
            }
            if (chk)
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    tbl_ProductCategory objCat = new tbl_ProductCategory();

                    objCat.CategoryName = txtCategoryName.Text;
                    objCat.CatagoryTypeID = Convert.ToInt32(dropDownCategoryType.SelectedValue);
                    objCat.RegionID = Convert.ToInt32(dropDownRegion.SelectedValue);
                    if (radioBtnApplySizeTrue.Checked == true)
                    {
                        objCat.ApplySizeRestrictions = true;
                    }
                    if (radioBtnApplySizeFalse.Checked == true)
                    {
                        objCat.ApplySizeRestrictions = false;
                    }
                    double temp;
                    if (double.TryParse(textboxHeight.Text, out temp))
                    {
                        objCat.HeightRestriction = Convert.ToDouble(textboxHeight.Text);
                    }
                    else
                    {
                        objCat.HeightRestriction = null;
                    }
                    if (double.TryParse(textBoxWidth.Text, out temp))
                    {
                        objCat.WidthRestriction = Convert.ToDouble(textBoxWidth.Text);
                    }
                    else
                    {
                        objCat.WidthRestriction = null;
                    }
                    if (double.TryParse(textboxZoomFactor.Text, out temp))
                    {
                        objCat.ScaleFactor = Convert.ToDecimal(textboxZoomFactor.Text);
                    }
                    else
                    {
                        objCat.ScaleFactor = 1;
                    }

                    if (dropDownParentCategory.SelectedValue == "-1")
                    {
                        objCat.ParentCategoryID = null;
                    }
                    else
                    {
                        objCat.ParentCategoryID = Convert.ToInt32(dropDownParentCategory.SelectedValue);
                    }
                    if (radioBtnApplyFoldYes.Checked == true)
                    {
                        objCat.ApplyFoldLines = true;

                    }
                    if (radioBtnApplyFoldNo.Checked == true)
                    {
                        objCat.ApplyFoldLines = false;
                    }
                    int CustomerID = -1;
                    if (Request.Cookies["customerid"].Value != null)
                    {
                        CustomerID = Convert.ToInt32(Request.Cookies["customerid"].Value);
                    }
                    if (CustomerID != -1)
                    {
                        objCat.CreatedBy = CustomerID;
                    }
                    divSavedSucessMsg.Visible = true;
                    db.tbl_ProductCategory.Add(objCat);
                    db.SaveChanges();
                }
                // update tree 
                GetCatagory();

                //show screen 
                divCategoryDetail.Visible = false;

                if (Session["cats"] != null)
                {
                    Session["cats"] = null;
                }
            }
            else
            {
                ShowMessageOnClient("You must enter height and width if you want to apply size restriction");
            }
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

        protected void radioBtnApplyFoldYes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnApplyFoldYes.Checked == true)
            {
                foldLinesContainer.Style.Add("display", "block");
            }
            else
            {
                foldLinesContainer.Style.Add("display", "none");
            }
        }

        protected void radioBtnApplyFoldNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnApplyFoldNo.Checked == true)
            {
                foldLinesContainer.Style.Add("display", "none");
            }
            else
            {
                foldLinesContainer.Style.Add("display", "block");
            }
        }
    }
}