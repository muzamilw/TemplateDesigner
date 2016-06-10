using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplateDesignerModelV2;
using System.Collections.ObjectModel;
using System.Web.UI.HtmlControls;

namespace TemplateDesignerV2.nav
{
    public partial class EditTemplate : System.Web.UI.Page
    {
        Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();
       
        public int ProductID
        {
            get
            {
                if (ViewState["ProductID"] != null)
                {
                    return Convert.ToInt32(ViewState["ProductID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["ProductID"] = value;
            }
        }

        // getter or setter for view state template pages list 
        public List<TemplatePages> pages
        {
            get
            {
                if (ViewState["pages"] != null)
                {
                    return ViewState["pages"] as List<TemplatePages>;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["pages"] = value;
            }
        }

        public int ProductPageID
        {
            get
            {
                if (ViewState["ProductPageID"] != null)
                {
                    return Convert.ToInt32(ViewState["ProductPageID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["ProductPageID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // setting the value of product ID
                ProductID = Convert.ToInt32(HttpContext.Current.Request.QueryString["TemplateID"]);


                string usertype = Request.Cookies["usertype"].Value;
                if (usertype == "CustomerUser")
                {
                    rbtnTempPrivate.Visible = false;
                    rbtnTempPublic.Visible = false;
                    lblrbtn.Visible = false;


                    lblSLCode.Visible = false;
                    txtTemplateCode.Visible = false;
                  //  FVTemplateCode.Visible = false;
                    
                }
                else
                {
                    rbtnTempPrivate.Visible = false;
                    rbtnTempPublic.Visible = false;
                    lblrbtn.Visible = false;


                    lblSLCode.Visible = true;
                    txtTemplateCode.Visible = true;
                   // FVTemplateCode.Visible = true;
                }

                 loadBaseColors(0);
                GetCatagory();
                if (ProductID != 0)
                    LoadTemplate();
                else
                {
                    setupEmpty();
                }
               
                loadIndustryTags();
                LoadPages();


            }

            DropDownColor_ApplyStyle();

            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, "init"))
            {
                String cstext1 = "var TemplateID = " + this.ProductID;
                cs.RegisterStartupScript(cstype, "init", cstext1, true);
            }

        }


        private void setupEmpty()
        {
            btnSubmit.Visible = false;
            BtnSaveTemplate.Visible = true;
            btnApprove.Visible = false;
            BtnDeleteTemplate.Visible = false;
            btnReject.Visible = false;

            lblApprovedByLabel.Visible = false;
            lblApproveLabel2.Visible = false;
            lblRejectLabel.Visible = false;
            LblTemplateStatusLabel.Visible = false;
            LblTemplateStatus.Visible = false;

            lblSubmittedLabel2.Visible = false;

            lblRejectLabel2.Visible = false;
            lblnoSides.Visible = true;
            
            

        }


        protected void BtnDeleteTemplateClick(object sender, EventArgs e)
        {
         
                    int CategoryID;
                   
                    oSVC.DeleteTemplate(this.ProductID, out CategoryID);
                 

                    Response.Redirect("default.aspx");
        }
       
        
      
        protected void LoadTemplate()
        {
            
            Templates oTemplate = oSVC.GetTemplate(Convert.ToInt32(this.ProductID));
            
            switch (oTemplate.Status)
            {
                case 1:
                    {
                        if (Request.Cookies["role"] != null && Request.Cookies["role"].Value.ToLower() == "admin")
                        {

                             editorControl.Visible = true;
                        }
                        LblTemplateStatus.InnerText = "Published";
                        BtnEditTemplate.Visible = btnSubmit.Visible  = true;
                        lblSubmittedLabel2.Visible = lblSubtmittedByLabel.Visible = false;
                        lblApproveLabel2.Visible = lblApprovedByLabel.Visible = false;
                        lblRejectLabel2.Visible = lblRejectLabel.Visible = false;
                        //rdrAdminRating2.Visible = lblAdminRating2.Visible = false;

                       

                        break;
                    }
                case 2:
                    {
                        LblTemplateStatus.InnerText = "Submitted";

                        if (Request.Cookies["role"] != null && Request.Cookies["role"].Value.ToLower() == "admin" )
                        {
                           btnApprove.Visible = btnReject.Visible = lblSubmittedLabel2.Visible = lblSubtmittedByLabel.Visible = editorControl.Visible =true;
                            lblSubtmittedByLabel.InnerText = oTemplate.SubmittedByName; BtnSaveTemplate.Visible =true;
                            lblApproveLabel2.Visible = lblApprovedByLabel.Visible = false;
                            btnSubmit.Visible =  lblRejectLabel2.Visible = lblRejectLabel.Visible = false;

                        }
                        else if (Request.Cookies["usertype"].Value == "CustomerUser" && Convert.ToBoolean(Request.Cookies["canpublishdesigns"].Value) && oTemplate.IsPrivate == true)
                        {
                            btnApprove.Visible = btnReject.Visible = lblSubmittedLabel2.Visible = lblSubtmittedByLabel.Visible = true;
                            lblSubtmittedByLabel.InnerText = oTemplate.SubmittedByName; BtnSaveTemplate.Visible = true;
                            lblApproveLabel2.Visible = lblApprovedByLabel.Visible = false;
                            btnSubmit.Visible = lblRejectLabel2.Visible = lblRejectLabel.Visible = false;
                        }
                        else
                        {
                            BtnSaveTemplate.Visible = btnSubmit.Visible = BtnDeleteTemplate.Visible =  btnApprove.Visible = btnReject.Visible = BtnEditTemplate.Visible = false;
                            //rdrAdminRating2.Visible = lblAdminRating2.Visible = false;
                            lblApproveLabel2.Visible = lblApprovedByLabel.Visible = false;
                            lblSubmittedLabel2.Visible = lblSubtmittedByLabel.Visible = false;
                            lblRejectLabel2.Visible = lblRejectLabel.Visible = false;
                        }

                        lblSubtmittedByLabel.InnerHtml = oTemplate.SubmittedByName;
                        break;
                    }
                case 3:
                    {
                        LblTemplateStatus.InnerHtml = "Approved";
                        if (Request.Cookies["role"] != null && Request.Cookies["role"].Value.ToLower() == "admin")
                        {

                            btnReject.Visible = lblSubmittedLabel2.Visible = lblSubtmittedByLabel.Visible = editorControl.Visible = true;

                            lblApproveLabel2.Visible = lblApprovedByLabel.Visible = BtnEditTemplate.Visible = BtnSaveTemplate.Visible =true;

                            btnSubmit.Visible = btnApprove.Visible = lblRejectLabel2.Visible = lblRejectLabel.Visible = false;
                        }
                        else if (Request.Cookies["usertype"].Value == "CustomerUser" && Convert.ToBoolean(Request.Cookies["canpublishdesigns"].Value) && oTemplate.IsPrivate == true)
                        {
                            btnReject.Visible = lblSubmittedLabel2.Visible = lblSubtmittedByLabel.Visible = true;

                            lblApproveLabel2.Visible = lblApprovedByLabel.Visible = BtnEditTemplate.Visible = BtnSaveTemplate.Visible = true;

                            btnSubmit.Visible = btnApprove.Visible = lblRejectLabel2.Visible = lblRejectLabel.Visible = false;
                        }
                        else
                        {
                            BtnSaveTemplate.Visible = btnSubmit.Visible = BtnDeleteTemplate.Visible = BtnEditTemplate.Visible = false;

                            lblApproveLabel2.Visible = lblApprovedByLabel.Visible = true;
                            lblSubmittedLabel2.Visible = lblSubtmittedByLabel.Visible = true;
                            lblRejectLabel2.Visible = lblRejectLabel.Visible = false;
                            // rdrAdminRating2.IsReadOnly = true;
                        }
                       // rdrAdminRating2.Visible = lblAdminRating2.Visible =true;
                        lblSubtmittedByLabel.InnerHtml = oTemplate.SubmittedByName;
                        lblApprovedByLabel.InnerHtml = oTemplate.ApprovedByName;
                        break;
                    }
                case 4:
                    {
                        LblTemplateStatus.InnerHtml = "Rejected";
                        if (Request.Cookies["role"] != null && Request.Cookies["role"].Value.ToLower() == "admin")
                        {

                            lblSubmittedLabel2.Visible = lblSubtmittedByLabel.Visible = editorControl.Visible = true;

                            lblApproveLabel2.Visible = lblApprovedByLabel.Visible =true;



                            btnSubmit.Visible = false;
                            btnApprove.Visible = BtnSaveTemplate.Visible =true;
                        }
                        else
                        {
                            BtnSaveTemplate.Visible = btnApprove.Visible = true;
                            btnApprove.Visible = btnReject.Visible = false;

                            lblApproveLabel2.Visible = lblApprovedByLabel.Visible =true;
                            lblSubmittedLabel2.Visible = lblSubtmittedByLabel.Visible =true;

                           // rdrAdminRating2.IsReadOnly = true;
                        }
                        lblRejectLabel2.Visible = lblRejectLabel.Visible =true;
                        //rdrAdminRating2.Visible = lblAdminRating2.Visible =true;
                        lblSubtmittedByLabel.InnerHtml = oTemplate.SubmittedByName;
                        lblApproveLabel2.InnerHtml = "Rejected By";
                        lblApprovedByLabel.InnerHtml = oTemplate.ApprovedByName;
                        lblRejectLabel.InnerText = oTemplate.RejectionReason;
                        
                        //lblRejectLabel.TextWrapping = TextWrapping.Wrap;
                        break;
                    }
                default:
                    break;
            }
            TextBoxMatchingSet.Text = oTemplate.ProductName;

            txtNarrativeTag2.Text = oTemplate.Description;
            txtTemplateCode.Text = oTemplate.Code;


          
            TextBoxMatchingSet.Text =  oTemplate.ProductName;
            TextBoxMatchingSet.Text = oTemplate.ProductName;
            cboCategory.SelectedValue = oTemplate.ProductCategoryID.ToString();

            ViewState["OldCategoryID"] = oTemplate.ProductCategoryID.ToString();

            if (oTemplate.TemplateOwner != null)
            {

                if (oTemplate.IsPrivate.Value)
                    rbtnTempPrivate.Checked = true;
                else
                    rbtnTempPublic.Checked = true;

            }


            imgFront.ImageUrl = "../designer/products/" + oTemplate.ProductID + "/p1.png" ;
            imgBack.ImageUrl = "../designer/products/" + oTemplate.ProductID + "/p2.png";
            if (oTemplate.BaseColorID.HasValue)
            {
                loadBaseColors(Convert.ToInt32(oTemplate.BaseColorID));
                DropDownColor.SelectedValue = oTemplate.BaseColorID.ToString();
            }

            if (oTemplate.FullView != string.Empty)
                imgFullView.ImageUrl = "../designer/products/" +  oTemplate.FullView;
            else
                imgFullView.ImageUrl = "../assets/preview-not-available.jpg";



            if (oTemplate.SLThumbnail != string.Empty)
                imgSLThumbNail.ImageUrl = "../designer/products/" + oTemplate.SLThumbnail;
            else
                imgSLThumbNail.ImageUrl = "../assets/preview-not-available.jpg";



            if (oTemplate.SuperView != string.Empty)

                imgSuperView.ImageUrl = "../designer/products/" + oTemplate.SuperView;
            else
                imgSuperView.ImageUrl = "../assets/preview-not-available.jpg";



            if (oTemplate.isEditorChoice != null && oTemplate.isEditorChoice.Value == true)
            {
                RadioBtnEditorYes.Checked = true;
            }
            else
            {
                RadioBtnEditorNo.Checked = true;
            }
                        
                ratingControl.SelectedIndex = Convert.ToInt32(oTemplate.MPCRating.Value);
                ratingControlUser.SelectedIndex = Convert.ToInt32(oTemplate.UserRating.Value);
            //if (this.ProductID != null)
            //{
            //    oSVC.GetTemplateIndustryTags(this.ProductID);
            //}
            //else
            //{
            //    oSVC.GetTemplateIndustryTags(0);
            //}

           // txtTemplateName2.Text = e.Result.ProductName;



                btnApprove.Visible = false;
                btnSubmit.Visible = false;
            
            
              
        }

        protected void BtnRejectTemplateClick(object sender, EventArgs e)
        {
            SaveTemplate(SaveAction.Reject);
            Response.Redirect("default.aspx");
        }
        enum SaveAction
        {
            Save = 1,
            Submit = 2,
            Approve = 3,
            Reject = 4
        }

        private void SaveTemplate(SaveAction Action)
        {


            //update page changes if any


            //foreach (var item in RepeaterPages.Items)
            //{
            //    PageStateChanged(item, null);
            //}

            


            try
            {

                int newTemplateId = 0;
                bool EditModeCatChanged = false;
                string szMessage = string.Empty;

                bool bVaalidate1 = false;
                bool bVaalidate2 = false;
                string usertype = Request.Cookies["usertype"].Value;

                

                if (cboCategory.SelectedItem != null)
                {
                    bVaalidate1 = true;
                }

                if (TextBoxMatchingSet.Text != string.Empty)
                {
                    bVaalidate2 = true;
                }

                if (TextBoxMatchingSet.Text != string.Empty)
                {
                    bVaalidate2 = true;
                }


                if (bVaalidate1 && bVaalidate2)
                {

                

                    string ErrorMessage = string.Empty;
                    string ErrorDesc = string.Empty;
                    Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();


                    if (Request.QueryString["mode"] == "new" || Request.QueryString["mode"] == "setnew")
                    {

                        TemplateDesignerModelV2.Templates oTemplate = new TemplateDesignerModelV2.Templates();

                        if (RadioBtnEditorYes.Checked)
                        {
                            oTemplate.isEditorChoice = true;
                        }
                        
                        
                        oTemplate.MPCRating = Convert.ToInt32(ratingControl.SelectedIndex);
                        oTemplate.Status = 1;

                        oTemplate.Code = txtTemplateCode.Text;
                        oTemplate.ProductName = TextBoxMatchingSet.Text;

                        oTemplate.CuttingMargin = Services.Utilities.Util.MMToPoint(5);
                        //cannot be null
                        oTemplate.ProductCategoryID = Convert.ToInt32(cboCategory.SelectedValue);
                        oTemplate.UsedCount = 0;
                        oTemplate.UserRating = 0;



                        oTemplate.Description = txtNarrativeTag2.Text;
                        oTemplate.IsDoubleSide = true;

                        oTemplate.PDFTemplateWidth = 0;
                        oTemplate.PDFTemplateHeight = 0;

                        // now pick value from hidden feild which contains the selected radio button color id if selected...
                       // if (DropDownColor.SelectedValue != null)
                        //{
                        if (!string.IsNullOrEmpty(hfBaseColorID.Value))
                        {
                            oTemplate.BaseColorID = Convert.ToInt32(hfBaseColorID.Value);
                        }
                        //Convert.ToInt32(DropDownColor.SelectedValue);
                       // }

                        
                        if (usertype == "CustomerUser")
                        {
                            oTemplate.SubmittedBy = Convert.ToInt32(Request.Cookies["customerid"].Value);
                           
                            oTemplate.TemplateOwner = Convert.ToInt32(Request.Cookies["customerid"].Value);
                            if (Request.Cookies["customername"] != null)
                                oTemplate.TemplateOwnerName = Request.Cookies["customername"].Value;


                            if (rbtnTempPrivate.Checked)
                                oTemplate.IsPrivate = true;
                            else
                                oTemplate.IsPrivate = false;

                        }
                        else
                        {

                            oTemplate.SubmittedBy = Convert.ToInt32(Request.Cookies["userid"].Value);
                        }



                        //new thumbnails

                        if (FileSLThumbNail.HasFile)
                        {
                            //string ext = System.IO.Path.GetExtension(System.IO.Path.GetFileName(FileSLThumbNail.PostedFile.FileName));
                            oTemplate.SLThumbnail = "SLThumbnail_" + System.IO.Path.GetFileName(FileSLThumbNail.PostedFile.FileName);
                            //oTemplate.SLThumbnail = System.IO.Path.GetFileName(FileSLThumbNail.PostedFile.FileName);
                            oTemplate.SLThumbnaillByte = FileSLThumbNail.FileBytes;
                        }

                        if (FileFullView.HasFile)
                        {
                            //string ext = System.IO.Path.GetExtension(System.IO.Path.GetFileName(FileFullView.PostedFile.FileName));
                            oTemplate.FullView = "FullView_" + System.IO.Path.GetFileName(FileFullView.PostedFile.FileName);
                            //oTemplate.FullView = System.IO.Path.GetFileName(FileFullView.PostedFile.FileName);
                            oTemplate.FullViewByte = FileFullView.FileBytes;
                        }

                        if (FileSuperView.HasFile)
                        {
                            //string ext = System.IO.Path.GetExtension(System.IO.Path.GetFileName(FileSuperView.PostedFile.FileName));
                            oTemplate.SuperView = "SuperView_" + System.IO.Path.GetFileName(FileSuperView.PostedFile.FileName);
                            //oTemplate.SuperView = System.IO.Path.GetFileName(FileSuperView.PostedFile.FileName);
                            oTemplate.SuperViewByte = FileSuperView.FileBytes;
                        }


                        

                        List<TemplateDesignerModelV2.TemplateIndustryTags> lstIndustry = new List<TemplateDesignerModelV2.TemplateIndustryTags>();
                        List<TemplateDesignerModelV2.TemplateThemeTags> lstTheme = new List<TemplateDesignerModelV2.TemplateThemeTags>();

                        
                        foreach (ListItem item in ListIndustryTags.Items)
                        {
                            if (item.Selected)
                            {
                                TemplateDesignerModelV2.TemplateIndustryTags tagItem = new TemplateDesignerModelV2.TemplateIndustryTags();
                                tagItem.TagID = Convert.ToInt32(item.Value);
                                lstIndustry.Add(tagItem);

                            }

                        }

                        if (pages.Count == 0)
                        {
                            lblError.Text = "Please add atleast one page to save template";
                        }

                        ProductID = oSVC.SaveTemplates(oTemplate, pages, lstIndustry, lstTheme, true, out newTemplateId, EditModeCatChanged);

                    }
                    else if (Request.QueryString["mode"] == "edit" || Request.QueryString["mode"] == "setedit")
                    {

                        //following condition detects if category is changed in edit mode and sets the flag 
                        if (ViewState["OldCategoryID"] != null && ViewState["OldCategoryID"].ToString() != string.Empty)
                        {
                            if (ViewState["OldCategoryID"].ToString() != cboCategory.SelectedValue)
                                EditModeCatChanged = true;

                        }

                        TemplateDesignerModelV2.Templates oTemplate = new TemplateDesignerModelV2.Templates();
                        
                        int TemplateID = 0;
                        int.TryParse(Request.QueryString["templateid"].ToString(), out TemplateID);

                        if (oTemplate != null)
                        {
                            if (Action == SaveAction.Submit)
                            {
                                oTemplate.Status = 2;

                                if (usertype == "CustomerUser")
                                {
                                    oTemplate.SubmittedBy = Convert.ToInt32(Request.Cookies["customerid"].Value);
                                    oTemplate.SubmittedByName = Request.Cookies["customername"].Value;
                                }
                                else
                                {
                                    oTemplate.SubmittedBy = Convert.ToInt32(Request.Cookies["userid"].Value);
                                    oTemplate.SubmittedByName = Request.Cookies["fullname"].Value;
                                }
                                oTemplate.SubmitDate = DateTime.Now;
                                szMessage = "";
                            }
                            else if (Action == SaveAction.Approve)
                            {
                                oTemplate.Status = 3;
                                if (usertype == "CustomerUser")
                                {
                                    oTemplate.ApprovedBy = Convert.ToInt32(Request.Cookies["customerid"].Value);
                                    oTemplate.ApprovedByName = Request.Cookies["customername"].Value; 
                                }
                                else
                                {
                                    oTemplate.ApprovedBy = Convert.ToInt32(Request.Cookies["userid"].Value);
                                    oTemplate.ApprovedByName = Request.Cookies["fullname"].Value;
                                }
                               
                                oTemplate.ApprovalDate = DateTime.Now;
                                szMessage = "";
                            }
                            else if (Action == SaveAction.Reject)
                            {
                                oTemplate.Status = 4;
                                if (usertype == "CustomerUser")
                                {
                                    oTemplate.ApprovedBy = Convert.ToInt32(Request.Cookies["customerid"].Value);
                                    oTemplate.ApprovedByName = Request.Cookies["customername"].Value; 
                                }
                                else
                                {
                                    oTemplate.ApprovedBy = Convert.ToInt32(Request.Cookies["userid"].Value);
                                    oTemplate.ApprovedByName = Request.Cookies["fullname"].Value;
                                }
                                oTemplate.RejectionReason = RejectionReason.Value;
                                oTemplate.ApprovalDate = DateTime.Now;
                                szMessage = "";
                            }
                            else if (Action == SaveAction.Save)
                            {
                                oTemplate.Status = 1;

                            }

                            if (RadioBtnEditorYes.Checked)
                            {
                                oTemplate.isEditorChoice = true;
                            }
                        
                                 oTemplate.MPCRating = Convert.ToInt32(ratingControl.SelectedIndex);


                            oTemplate.ProductID = TemplateID;
                            oTemplate.ProductName = TextBoxMatchingSet.Text;
                            oTemplate.Description = txtNarrativeTag2.Text;

                            oTemplate.ProductCategoryID = Convert.ToInt32(cboCategory.SelectedValue);

                            // if (cboMatchingSet.SelectedIndex == 0)
                            //    oTemplate.MatchingSetID = null;
                            //else
                            //    oTemplate.MatchingSetID = Convert.ToInt32(cboMatchingSet.SelectedValue);

                            //getting the selected category dimensions  and see if they are to be applied.

                            //var SelectedProductCategory = lstProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();


                            oTemplate.Code = txtTemplateCode.Text;

                            //if (DropDownColor.SelectedValue != null)
                            //{
                            if (!string.IsNullOrEmpty(hfBaseColorID.Value)) 
                            {
                                oTemplate.BaseColorID = Convert.ToInt32(hfBaseColorID.Value);
                            }
                             //Convert.ToInt32(DropDownColor.SelectedValue);
                           // }

                           
                            if (usertype == "CustomerUser")
                            {
                                
                                oTemplate.TemplateOwner = Convert.ToInt32(Request.Cookies["customerid"].Value);
                                if (Request.Cookies["customername"] != null)
                                    oTemplate.TemplateOwnerName = Request.Cookies["customername"].Value;


                                if (rbtnTempPrivate.Checked)
                                    oTemplate.IsPrivate = true;
                                else
                                    oTemplate.IsPrivate = false;

                            }

                            if (FileSLThumbNail.HasFile)
                            {
                                //string ext = System.IO.Path.GetExtension(System.IO.Path.GetFileName(FileSLThumbNail.PostedFile.FileName));
                                oTemplate.SLThumbnail = "SLThumbnail_" + System.IO.Path.GetFileName(FileSLThumbNail.PostedFile.FileName);
                                //oTemplate.SLThumbnail = System.IO.Path.GetFileName( FileSLThumbNail.PostedFile.FileName);
                                oTemplate.SLThumbnaillByte = FileSLThumbNail.FileBytes;
                            }

                            if (FileFullView.HasFile)
                            {
                                //oTemplate.FullView = System.IO.Path.GetFileName(FileFullView.PostedFile.FileName);
                                //string ext = System.IO.Path.GetExtension(System.IO.Path.GetFileName(FileFullView.PostedFile.FileName));
                                oTemplate.FullView = "FullView_" + System.IO.Path.GetFileName(FileFullView.PostedFile.FileName);
                                oTemplate.FullViewByte = FileFullView.FileBytes;
                            }

                            if (FileSuperView.HasFile)
                            {
                                //oTemplate.SuperView = System.IO.Path.GetFileName(FileSuperView.PostedFile.FileName);
                               // string ext = System.IO.Path.GetExtension(System.IO.Path.GetFileName(FileSuperView.PostedFile.FileName));
                                oTemplate.SuperView = "SuperView_" + System.IO.Path.GetFileName(FileSuperView.PostedFile.FileName);
                                oTemplate.SuperViewByte = FileSuperView.FileBytes;
                            }


                            List<TemplateDesignerModelV2.TemplateIndustryTags> lstIndustry = new List<TemplateDesignerModelV2.TemplateIndustryTags>();
                            List<TemplateDesignerModelV2.TemplateThemeTags> lstTheme = new List<TemplateDesignerModelV2.TemplateThemeTags>();


                            foreach (ListItem item in ListIndustryTags.Items)
                            {
                                if (item.Selected)
                                {
                                    TemplateDesignerModelV2.TemplateIndustryTags tagItem = new TemplateDesignerModelV2.TemplateIndustryTags();
                                    tagItem.TagID = Convert.ToInt32(item.Value);
                                    lstIndustry.Add(tagItem);

                                }

                            }

                            //for (int i = 0; i < lstbxStyleTags.Items.Count; i++)
                            //{
                            //    TemplateDesignerModelV2.sp_GetTemplateThemeTags_Result o = (TemplateDesignerModelV2.sp_GetTemplateThemeTags_Result)lstbxStyleTags.Items[i];

                            //    if (o.selected == 1)
                            //    {
                            //        TemplateDesignerModelV2.TemplateThemeTags item = new TemplateDesignerModelV2.TemplateThemeTags();
                            //        item.TagID = o.TagID;
                            //        lstTheme.Add(item);
                            //    }

                            //}

                            if (pages.Count == 0)
                            {
                                lblError.Text = "Please add atleast one page to save template";
                            }


                              oSVC.SaveTemplates(oTemplate,pages, lstIndustry, lstTheme, false,out newTemplateId, EditModeCatChanged);
                            

                        }
                    }
                }
                else
                {
                    //if (bVaalidate1 == false && bVaalidate2 == true)
                    //    MessageBox.Show("Please select a category to continue", "Validation Error", MessageBoxButton.OK);
                    //else if (bVaalidate2 == false && bVaalidate1 == true)
                    //    MessageBox.Show("Please enter template name to continue", "Validation Error", MessageBoxButton.OK);
                    //else
                    //{
                    //    MessageBox.Show("Please select a category and enter template name to continue", "Validation Error", MessageBoxButton.OK);
                    //}

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void BtnSaveTemplateClick(object sender, EventArgs e)
        {
            SaveTemplate(SaveAction.Save);
            Button obtn = (Button)sender;
            if (obtn.ID == "BtnUploadThumbnails")
            {
                //ProductID = Convert.ToInt32(HttpContext.Current.Request.QueryString["TemplateID"]);
                Response.Redirect("EditTemplate.aspx?TemplateId=" + ProductID.ToString() + "&mode=edit");
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }

        protected void BtnEditTemplateClick(object sender, EventArgs e)
        {
            
            if (pages.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "alert('" + "Please add atleast one side to continue." + "');", true); ;
            }
            else
            {
                SaveTemplate(SaveAction.Save);
                Response.Redirect("../designer.aspx?TemplateID=" + ProductID.ToString());
            }
        }
        //protected void GetMatchingSet(int MatchingSetID)
        //{
        //    MatchingSets ms = new MatchingSets();
        //    Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();
        //    ms = oSVC.GetMatchingSetbyID(MatchingSetID);
        //    cboMatchingSet.Text = ms.MatchingSetName;
        //}
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
        protected void GetCatagory()
        {

            List<TemplateDesignerModelV2.vw_ProductCategoriesLeafNodes> oCatsAll = new List<vw_ProductCategoriesLeafNodes>();
            List<vw_ProductCategoriesLeafNodes> ctr = new List<vw_ProductCategoriesLeafNodes>(); ;

            oCatsAll = oSVC.GetCategories();


            int userRole = getCustomerRole();
            int privateCutCat = getPrivateCustomerCategory();

            if (userRole == 1)
            {
                // mpc users
                foreach (var objCat in oCatsAll)
                {
                    if (objCat.CatagoryTypeID != privateCutCat && objCat.Height != null && objCat.Width != null) 
                    {
                        ctr.Add(objCat);
                    }
                }
            }
            else
            { // users
                int customerid = Convert.ToInt32(Request.Cookies["customerid"].Value);
                foreach (var objCat in oCatsAll)
                {
                    if (objCat.CatagoryTypeID == privateCutCat && objCat.CreatedBy == customerid && objCat.Height != null && objCat.Width != null)
                    {
                        ctr.Add(objCat);
                    }
                }
            }

           
            if (ctr != null)
            {
                cboCategory.DataSource = ctr;
                cboCategory.DataTextField = "CategoryName";
                cboCategory.DataValueField = "ProductCategoryID";
                cboCategory.DataBind();
                
                //  lstProductCategory = new List<ProductServiceReference.vw_ProductCategoriesLeafNodes>(e.Result);



              //  clProduct.GetMatchingSetThemeCompleted += new EventHandler<ProductServiceReference.GetMatchingSetThemeCompletedEventArgs>(clProduct_GetMatchingSetThemeCompleted);
                //clProduct.GetMatchingSetThemeAsync();
            }
        }

        protected void loadBaseColors(int BaseClrId)
        {
            Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();
          
            List<BaseColors> Bcolor = new List<BaseColors>();
            Bcolor = oSVC.GetBaseColors();
            DropDownColor.DataSource = Bcolor;
            DropDownColor.DataValueField = "BaseColorID";
            DropDownColor.DataTextField = "HEX";
            DropDownColor.DataBind();

            int Colorcount = 1;
            string BaseClrHtml = "";
            foreach (var clr in Bcolor)
            {
                var id = "rdBaseColr" + Colorcount;

                if (clr.BaseColorID == BaseClrId)
                {
                    BaseClrHtml += "<div class='BaseColrs rounded_corners ShadowToBaseColor'><div onclick=BaseColorOnClick('" + id + "'); class='InnerBaseColrsContainer rounded_corners' style=background-color:#" + clr.HEX + ";><input id=rdBaseColr" + Colorcount + " runat=server  data-BaseColorID=" + clr.BaseColorID + " type=radio name=BaseColorsRadioList style=opacity:0 /><label for=rdBaseColr" + Colorcount + "  style=display:inline; ></label></div></div> ";
                }
                else 
                {
                    BaseClrHtml += "<div class='BaseColrs rounded_corners'><div onclick=BaseColorOnClick('" + id + "'); class='InnerBaseColrsContainer rounded_corners' style=background-color:#" + clr.HEX + ";><input id=rdBaseColr" + Colorcount + " runat=server  data-BaseColorID=" + clr.BaseColorID + " type=radio name=BaseColorsRadioList style=opacity:0 /><label for=rdBaseColr" + Colorcount + "  style=display:inline; ></label></div></div> ";
                }

                

                Colorcount += 1;
            }
            BaseColorsContainer.InnerHtml = BaseClrHtml;


            //List<CategoryRegions> oreg = oSVC.getCategoryRegions();
            //List<CategoryTypes> oCatTypes = oSVC.getCategoryTypes();

        }

        protected void loadIndustryTags()
        {
            List<sp_GetTemplateIndustryTags_Result> tags;
            
            tags= oSVC.GetTemplateIndustryTags(this.ProductID);

            
            ListIndustryTags.DataSource = tags;
            ListIndustryTags.DataTextField = "TagName";
            ListIndustryTags.DataValueField = "TagID";
            
            ListIndustryTags.DataBind();

            for(int i = 0; i < ListIndustryTags.Items.Count; i++)
            {
                if(tags[i].selected == 1)
                {
                    ListIndustryTags.Items[i].Selected = true ;
                }
                else if (tags[i].selected == 0)
                {
                    ListIndustryTags.Items[i].Selected = false;
                }
                
            }
        }

        protected void ListIndustryTags_DataBound(object sender, EventArgs e)
        {
            
        }

        protected void Pages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
          
            if (e.CommandName == "MovePageUp")
            {
                List<TemplatePages> tpages = this.pages;
                foreach (var page in tpages)
                {
                   // TemplatePages page = db.TemplatePages.Where(g => g.ProductPageID == pageId).Single();
                    if (page.PageNo == Convert.ToInt32((String)e.CommandArgument))
                    {
                        int UperPageNo = Convert.ToInt32(page.PageNo) - 1;

                        foreach (var Pageup in tpages)
                        {
                            if (Pageup.ProductID == this.ProductID && Pageup.PageNo == UperPageNo)
                            {
                                Pageup.PageNo += 1;
                                page.PageNo -= 1;
                            }
                        }
                       // TemplatePages Pageup = db.TemplatePages.Where(g => g.ProductID == TemplateID && g.PageNo == UperPageNo).Single();
                        //db.TemplatePages.Remove(Pageup);
                        //db.TemplatePages.Remove(page);

                       
                        //db.TemplatePages.Add(page);
                        //db.TemplatePages.Add(Pageup);
                       // db.SaveChanges();

                    }
                }
               // List<TemplatePages> tpages =  oSVC.UpdateTemplatePages(this.ProductID,Convert.ToInt32( (String)e.CommandArgument),"MovePageUp");
                tpages = tpages.OrderBy(c => c.PageNo).ToList();
                this.pages = tpages;
                RepeaterPages.DataSource = tpages;
                RepeaterPages.DataBind();
            }
            else if (e.CommandName == "MovePageDown")
            {
               // List<TemplatePages> tpages = oSVC.UpdateTemplatePages(this.ProductID, Convert.ToInt32((String)e.CommandArgument), "MovePageDown");
                var tpages = this.pages;
                foreach (var page  in tpages)
                {
                    //TemplatePages page = db.TemplatePages.Where(g => g.ProductPageID == pageId).Single();
                    if (page.PageNo == Convert.ToInt32((String)e.CommandArgument))
                    {
                        int UperPageNo = Convert.ToInt32(page.PageNo) + 1;

                        foreach (var PageUp in tpages)
                        {
                            //TemplatePages Pageup = db.TemplatePages.Where(g => g.ProductID == TemplateID && g.PageNo == UperPageNo).Single();
                            //db.TemplatePages.Remove(Pageup);
                            //db.TemplatePages.Remove(page);
                            if (PageUp.ProductID == this.ProductID && PageUp.PageNo == UperPageNo)
                            {
                                PageUp.PageNo -= 1;
                                page.PageNo += 1;
                            }
                            //db.TemplatePages.Add(page);
                            //db.TemplatePages.Add(Pageup);DeletePage
                            //db.SaveChanges();
                        }
                    }
                }
                tpages = tpages.OrderBy(c => c.PageNo).ToList();
                this.pages = tpages;
                RepeaterPages.DataSource = tpages;
                RepeaterPages.DataBind();
                // BtnCopyTemplateClick((String)e.CommandArgument);

            }
            else if (e.CommandName == "DeletePage")
            {
                //List<TemplatePages> tpages = oSVC.UpdateTemplatePages(this.ProductID, Convert.ToInt32((String)e.CommandArgument), "DeletePage");
                var tpages = this.pages;
                //TemplatePages page = db.TemplatePages.Where(g => g.ProductPageID == pageId).Single();
                for (int i = 0; i < tpages.Count; i++)
                {
                    if (tpages[i].PageNo == Convert.ToInt32((String)e.CommandArgument))
                    {
                        tpages.Remove(tpages[i]);
                    }
                }


                int pagecounter = 1;
                foreach (var page in tpages.OrderBy( c=> c.PageNo))
                {
                    page.PageNo = pagecounter;
                    pagecounter++;
                }



                tpages = tpages.OrderBy(c => c.PageNo).ToList();
                RepeaterPages.DataSource = tpages;
                this.pages = tpages;
                RepeaterPages.DataBind(); // BtnCopyTemplateClick((String)e.CommandArgument);

            }
            else if (e.CommandName == "UploadBackground")
            {
                SaveFiles();
                //DropDownList drBkground = e.Item.FindControl("DropDownBackground") as DropDownList;
                //FileUpload fUpload = e.Item.FindControl("UploaderBkType") as FileUpload;
                //string fileExt =System.IO.Path.GetExtension( fUpload.FileName);
                //string fileName =this.ProductID.ToString() + fUpload.FileName;
                
                //if (drBkground.SelectedIndex == 0)
                //{
                //    if (fUpload.HasFile)
                //    {

                //        if (fileExt == ".pdf")
                //        {
                //            string uploadPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                //            uploadPath = System.IO.Path.Combine(uploadPath, this.ProductID.ToString());
                          
                //            if (!System.IO.Directory.Exists(uploadPath))
                //            {
                //                System.IO.Directory.CreateDirectory(uploadPath);
                //            }
                //            uploadPath = System.IO.Path.Combine(uploadPath, fUpload.FileName);
                                
                //            fUpload.SaveAs(uploadPath);
                           
                //            var tpage = this.pages;
                            
                //            foreach(var page in tpage)
                //            {
                //                if(page.ProductPageID == Convert.ToInt32( e.CommandArgument.ToString()))
                //                {
                //                    page.BackGroundType = Convert.ToInt32("1");
                //                    page.BackgroundFileName = this.ProductID.ToString() + "/" + fileName;
                //                }
                //            }
                //            this.pages = tpage;
                                    
                //        }
                //        else
                //        {
                //            DiverrorMessage.Visible = true;
                //        }
                //    }
                //}
                //else if (drBkground.SelectedIndex == 2)
                //{
                //    // color
                //}
                //else if (drBkground.SelectedIndex == 1)
                //{
                //    if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".jpeg")
                //    {
                //        string uploadPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                //        uploadPath = System.IO.Path.Combine(uploadPath, this.ProductID.ToString());
                     
                //        if (!System.IO.Directory.Exists(uploadPath))
                //        {
                //            System.IO.Directory.CreateDirectory(uploadPath);
                //        }
                //        uploadPath = System.IO.Path.Combine(uploadPath, fUpload.FileName);

                //        fUpload.SaveAs(uploadPath);

                //        var tpage = this.pages;

                //        foreach (var page in tpage)
                //        {
                //            if (page.ProductPageID == Convert.ToInt32(e.CommandArgument.ToString()))
                //            {
                //                page.BackGroundType = Convert.ToInt32("3");
                //                page.BackgroundFileName = this.ProductID.ToString() + "/" + fileName;
                //            }
                //        }
                //        this.pages = tpage;
                                    
                //    }
                //    else
                //    {
                //        DiverrorMessage.Visible = true;
                //    }
                //}
            
            
            }
          
        }
   
        // save all uploaded files and write there names in list of template pages 
        protected void SaveFiles()
        {
            foreach (var item in RepeaterPages.Items)
            {
                RepeaterItem row = item as RepeaterItem;
                DropDownList drBkground = row.FindControl("DropDownBackground") as DropDownList;
                FileUpload fUpload = row.FindControl("UploaderBkType") as FileUpload;
                //getting page id 
                Label LblID = row.FindControl("PageID") as Label;
                int pageID = Convert.ToInt32(LblID.Text);

                string fileExt = System.IO.Path.GetExtension(fUpload.FileName);
                string fileName = this.ProductID.ToString() + fUpload.FileName;

                if (drBkground.SelectedIndex == 0)
                {
                    if (fUpload.HasFile)
                    {

                        if (fileExt == ".pdf")
                        {
                            string uploadPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                            uploadPath = System.IO.Path.Combine(uploadPath, this.ProductID.ToString());

                            if (!System.IO.Directory.Exists(uploadPath))
                            {
                                System.IO.Directory.CreateDirectory(uploadPath);
                            }
                            uploadPath = System.IO.Path.Combine(uploadPath, fUpload.FileName);

                            fUpload.SaveAs(uploadPath);

                            var tpage = this.pages;

                            foreach (var page in tpage)
                            {
                                if (page.ProductPageID == pageID)
                                {
                                    page.BackGroundType = Convert.ToInt32("1");
                                    page.BackgroundFileName = this.ProductID.ToString() + "/" + fileName;
                                }
                            }
                            this.pages = tpage;

                        }
                        else
                        {
                            DiverrorMessage.Visible = true;
                        }
                    }
                }
                else if (drBkground.SelectedIndex == 2)
                {
                    // color
                }
                else if (drBkground.SelectedIndex == 1)
                {
                    if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".jpeg")
                    {
                        string uploadPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                        uploadPath = System.IO.Path.Combine(uploadPath, this.ProductID.ToString());

                        if (!System.IO.Directory.Exists(uploadPath))
                        {
                            System.IO.Directory.CreateDirectory(uploadPath);
                        }
                        uploadPath = System.IO.Path.Combine(uploadPath, fUpload.FileName);

                        fUpload.SaveAs(uploadPath);

                        var tpage = this.pages;

                        foreach (var page in tpage)
                        {
                            if (page.ProductPageID == pageID)
                            {
                                page.BackGroundType = Convert.ToInt32("3");
                                page.BackgroundFileName = this.ProductID.ToString() + "/" + fileName;
                            }
                        }
                        this.pages = tpage;

                    }
                    else
                    {
                        DiverrorMessage.Visible = true;
                    }
                }
            
            }
        }

        protected void LoadPages()
        {
            List<TemplatePages> tpages = null;
           
            tpages = oSVC.GetTemplatePages(this.ProductID);

            // saving list in view state 
            this.pages = tpages;

            RepeaterPages.DataSource = tpages;
            RepeaterPages.DataBind();

       }

        protected void RepeaterPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //getting page id 
                Label LblID = e.Item.FindControl("PageID") as Label;
                int pageID = Convert.ToInt32(LblID.Text);

                Label lblPageNO = e.Item.FindControl("lblPageNo") as Label;
                int PageNo = Convert.ToInt32(lblPageNO.Text);

                //  adding dropdown  values 

                DropDownList dr = e.Item.FindControl("DropDownPageType") as DropDownList;
                dr.Items.Add(new ListItem("Front", "1"));
                dr.Items.Add(new ListItem("Back", "2"));
                dr.Items.Add(new ListItem("Cover", "3"));
                dr.Items.Add(new ListItem("Back Cover", "4"));
                dr.Items.Add(new ListItem("Inside Front", "5"));
                dr.Items.Add(new ListItem("Inside Back", "6"));
                dr.Items.Add(new ListItem("Internal Page", "7"));

                dr.Items.Add(new ListItem("Mug Front", "8"));
                dr.Items.Add(new ListItem("Mug Back", "9"));
                dr.Items.Add(new ListItem("T-shirt Front", "10"));
                dr.Items.Add(new ListItem("T-shirt Back", "11"));


                dr.Items.Add(new ListItem("Calendar Cover", "12"));
                dr.Items.Add(new ListItem("Calendar back", "13"));
                dr.Items.Add(new ListItem("Jan", "14"));
                dr.Items.Add(new ListItem("Feb", "15"));
                dr.Items.Add(new ListItem("Mar", "16"));
                dr.Items.Add(new ListItem("Apr", "17"));
                dr.Items.Add(new ListItem("May", "18"));
                dr.Items.Add(new ListItem("Jun", "19"));
                dr.Items.Add(new ListItem("jul", "20"));
                dr.Items.Add(new ListItem("Aug", "21"));
                dr.Items.Add(new ListItem("Sep", "22"));
                dr.Items.Add(new ListItem("Oct", "23"));
                dr.Items.Add(new ListItem("Nov", "24"));
                dr.Items.Add(new ListItem("Dec", "25"));

                for (int i = 1; i <= 500; i++)
                {
                    dr.Items.Add(new ListItem(i.ToString(), (i+25).ToString()));
                }

                // adding dropdown background values 
                //  adding dropdown  values 

                DropDownList drBackground = e.Item.FindControl("DropDownBackground") as DropDownList;
                drBackground.Items.Add("PDF");
                drBackground.Items.Add("Image");
                drBackground.Items.Add("Color");
                // getting orientation control 
                RadioButton rdLandScape = e.Item.FindControl("RadioBtnLandScape") as RadioButton;
                RadioButton rdPortrait = e.Item.FindControl("RadioButtonPortrait") as RadioButton;
                // getting move up and down buttons 
                ImageButton BtnPageMoveUp = e.Item.FindControl("BtnMovePageUp") as ImageButton;
                ImageButton BtnPageMoveDown = e.Item.FindControl("BtnMovePageDown") as ImageButton;


                List<TemplatePages> tpages = pages;

                //tpages = oSVC.GetTemplatePages(this.ProductID);

                foreach (var page in tpages)
                {
                    if (page.PageNo == PageNo)
                    {
                        //if (page.PageName == "Front")
                        //{
                        //    dr.SelectedIndex = 0;
                        //}
                        //else if (page.PageName == "Back")
                        //{
                        //    dr.SelectedIndex = 1;
                        //}

                        dr.SelectedValue = page.PageType.ToString();


                        // showing background type 
                        int bkgroundtype = Convert.ToInt32(page.BackGroundType);
                        if (bkgroundtype == 1)
                        {
                            drBackground.SelectedIndex = 0;
                        }
                        else if (bkgroundtype == 2)
                        {
                            drBackground.SelectedIndex = 2;
                        }
                        else if (bkgroundtype == 3)
                        {
                            drBackground.SelectedIndex = 1;
                        }
                        // disabling enabling move page down button 
                        if (page.PageNo == tpages.Count)
                        {
                            BtnPageMoveDown.Visible = false;
                        }
                        else
                        {
                            BtnPageMoveDown.Visible = true;
                        }
                        // disabling enabling move page up button
                        if (page.PageNo == 1)
                        {
                            BtnPageMoveUp.Visible = false;
                        }
                        else
                        {
                            BtnPageMoveUp.Visible = true;
                        }


                        // selecting orientation property 

                        if (page.Orientation == 1 || page.Orientation == 0)
                        {
                            rdPortrait.Checked = true;
                        }
                        else if (page.Orientation == 2)
                        {
                            rdLandScape.Checked = true;
                        }
                    }


                }
                //this.pages = tpages;
            }

        }
        
        protected void PageStateChanged(object sender, EventArgs e)
        {
            // orientation 
            int orientation = 0;
            //string page type 
             RepeaterItem row = null;
            string pageType = string.Empty;
            string pageName = string.Empty;
            string pageBackgroundType = string.Empty;
            if (sender is RadioButton)
            {
               row = (sender as RadioButton).NamingContainer as RepeaterItem;
            }
            else if (sender is DropDownList)
            {
                row = (sender as DropDownList).NamingContainer as RepeaterItem;
            }
            else if (sender is RepeaterItem)
            {
                row = sender as RepeaterItem;
            }
           
            //getting page id 
            Label LblID = row.FindControl("PageID") as Label;
            int pageID = Convert.ToInt32(LblID.Text);

            Label lblPageNo = row.FindControl("lblPageNo") as Label;
            int PageNo = Convert.ToInt32(lblPageNo.Text);



            RadioButton rdLandScape = row.FindControl("RadioBtnLandScape") as RadioButton;
            RadioButton rdPortrait = row.FindControl("RadioButtonPortrait") as RadioButton;
            DropDownList drBkground = row.FindControl("DropDownBackground") as DropDownList;
            if (rdPortrait.Checked == true)
            {
                orientation = 1;
            }
            else if (rdLandScape.Checked == true)
            {
                orientation = 2;
            }
            DropDownList dr = row.FindControl("DropDownPageType") as DropDownList;

            pageType = dr.SelectedValue;
            pageName = dr.SelectedItem.Text;
           
               
            
            // enablinng upload image and pdf div..
            if (drBkground.SelectedIndex == 0)  //|| drBkground.SelectedIndex == 1
            {
                HtmlGenericControl divUploadImg = row.FindControl("DivFileUploader") as HtmlGenericControl;
                //divUploadImg.Visible = true;
                pageBackgroundType = "1";
               // Div;
            }
            else if (drBkground.SelectedIndex == 1)
            {
                HtmlGenericControl divUploadImg = row.FindControl("DivFileUploader") as HtmlGenericControl;
                divUploadImg.Visible = false;
                //DivColorPalletNav.Style.Add("display", "block");
                this.ProductPageID = pageID;
                pageBackgroundType = "3";
                //  pageType = "Back";
            }
            else
            {
                pageBackgroundType = "2";
            }
          
            //Services.TemplateSvcSP oSVC = new Services.TemplateSvcSP();
          //  oSVC.UpdateTemplatePage(this.ProductID, pageType, orientation.ToString());
            //TemplatePages tpage = db.TemplatePages.Where(g => g.ProductPageID == pageId).Single();
            var tpagesList = this.pages;
            foreach (var tpage in tpagesList)
            {
                if (tpage.PageNo == PageNo)
                {
                    tpage.PageName = pageName;

                    tpage.PageType = Convert.ToInt32( pageType);
                    tpage.BackGroundType = Convert.ToInt32(pageBackgroundType);
                    
                    tpage.Orientation = Convert.ToInt32(orientation);
                    //db.SaveChanges();
                }
            }

            this.pages = tpagesList;
        }

        protected void BtnAddNewPage_Click(object sender, EventArgs e)
        {
           
            TemplatePages tpage = new TemplatePages();
            tpage.Orientation = 1;
            tpage.PageType = 1;
            tpage.PageNo = this.pages.Count + 1;
            tpage.ProductID = this.ProductID;
            tpage.BackGroundType = 1;
            tpage.PageName = "Front";

            var templatePages = this.pages;
            templatePages.Add(tpage);
            this.pages = templatePages;
            RepeaterPages.DataSource = templatePages;
            RepeaterPages.DataBind();
        }

        protected void BtnAdvanceColorPicker_Click(object sender, EventArgs e)
        {
            DivAdvanceColorPanel1.Style.Add("display", "block");
        }


        protected void SaveColrData_Click(object sender, EventArgs e)
        {
            string color = RGBVal.Value;
            string[] RGB = color.Split(',');
            var tpages = this.pages;
            foreach (var page in tpages)
            {
                if (page.ProductPageID == this.ProductPageID)
                {
                    page.BackGroundType = 2;
                    //page.BgR =Convert.ToInt32( RGB[0]);
                    //page.BgG = Convert.ToInt32(RGB[1]);
                    //page.BgB = Convert.ToInt32(RGB[2]);
                }
            }
            this.pages = tpages;
        }

        private void DropDownColor_ApplyStyle()
        {
            for (int i = 0; i < DropDownColor.Items.Count; i++)
            {
                DropDownColor.Items[i].Attributes.Add("style", "background-color: #" + DropDownColor.Items[i].Text);
                
                //DropDownColor.Items[i].Text = "";
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveTemplate(SaveAction.Submit);
            Response.Redirect("default.aspx");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            SaveTemplate(SaveAction.Approve);
            Response.Redirect("default.aspx");
        }

 
    }
};