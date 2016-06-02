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
using Telerik.Windows.Controls;
using Telerik.Windows.Input;
using System.Windows.Browser;
using webprintDesigner.ProductServiceReference;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace webprintDesigner
{
    public partial class EditTemplate : UserControl
    {
        List<webprintDesigner.ProductServiceReference.Templates> lstTemplates = null;
        List<webprintDesigner.ProductServiceReference.vw_ProductCategoriesLeafNodes> lstProductCategory = null;
        webprintDesigner.ProductServiceReference.Templates oTemplate = null;
        bool DataLoading = false;
        bool EditModeCatChanged = false;


        string szMessage = "";

        string SLThumbnailFileName;
        byte[] SLThumbnail;

        string FullViewFileName;
        byte[] FullView;

        string SuperViewFileName;
        byte[] SuperView;


        ProductServiceReference.ProductServiceClient clProduct = new ProductServiceReference.ProductServiceClient();

        public EditTemplate()
        {
            InitializeComponent();

            ProgressBar1.IsIndeterminate = true;
            ProgessTxt.Text = "Loading Data";
            ProgressPanel.Visibility = Visibility.Visible;
            ProgressBorder.Visibility = System.Windows.Visibility.Visible;


            if (DictionaryManager.AppObjects.ContainsKey("mode") && DictionaryManager.AppObjects["mode"] != null && DictionaryManager.AppObjects["mode"].ToString() == "matchingset")
            {
                grdProducts.Visibility = System.Windows.Visibility.Collapsed;
                grdMatchingSets.Visibility = System.Windows.Visibility.Visible;

                LoadMatchingSetDetails();

            }
            else if (DictionaryManager.AppObjects.ContainsKey("mode") && DictionaryManager.AppObjects["mode"] != null && (DictionaryManager.AppObjects["mode"].ToString() == "new" || DictionaryManager.AppObjects["mode"].ToString() == "setnew"))
            {
                grdProducts.Visibility = System.Windows.Visibility.Visible;
                grdMatchingSets.Visibility = System.Windows.Visibility.Collapsed;

                tbStatus.Text = "Draft";
                lblProductName.Text = "Create New Template";
                btnSave.Visibility = System.Windows.Visibility.Visible;
               
                btnEdit.Visibility = System.Windows.Visibility.Collapsed;
                imgThumb.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/nothumb.png", UriKind.RelativeOrAbsolute));

                lblSubmittedLabel.Visibility = lblSubtmittedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                lblApproveLabel.Visibility = lblApprovedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                lblRejectLabel.Visibility = lblRejectedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                rdrAdminRating.Visibility = lblAdminRating.Visibility = System.Windows.Visibility.Collapsed;

                FillControls();

            }

            else if (DictionaryManager.AppObjects.ContainsKey("mode") && DictionaryManager.AppObjects["mode"] != null && (DictionaryManager.AppObjects["mode"].ToString() == "edit" || DictionaryManager.AppObjects["mode"].ToString() == "setedit") && DictionaryManager.AppObjects.ContainsKey("productid") && DictionaryManager.AppObjects["productid"] != null)
            {
                grdProducts.Visibility = System.Windows.Visibility.Visible;
                grdMatchingSets.Visibility = System.Windows.Visibility.Collapsed;

                btnEdit.Visibility = System.Windows.Visibility.Visible;
               

                FillControls();
            }
            else
            {
                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.ListView, DictionaryManager.Pages.ListView);
            }

            

        }

        private void LoadMatchingSetDetails()
        {

            clProduct.GetBaseColorsCompleted += new EventHandler<ProductServiceReference.GetBaseColorsCompletedEventArgs>(clProduct_GetBaseColorsCompleted);
            clProduct.GetBaseColorsAsync();
          
          
        }

        void clProduct_GetMatchingSetbyIDCompleted(object sender, GetMatchingSetbyIDCompletedEventArgs e)
        {
            

            txtMatchingSet.Text = e.Result.MatchingSetName;

          
           
        }


        void clProduct_GetProductByIdCompletedSimple(object sender, ProductServiceReference.GetProductByIdCompletedEventArgs e)
        {
            if (e.Result != null)
            {

                oTemplate = e.Result;
                switch (oTemplate.Status)
                {
                    case 1:
                        {
                            tbStatus.Text = "Draft";
                            btnSave.Visibility = btnSubmit.Visibility =  System.Windows.Visibility.Visible;
                            lblSubmittedLabel2.Visibility = lblSubtmittedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                            lblApproveLabel2.Visibility = lblApprovedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                            lblRejectLabel2.Visibility = lblRejectedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                            rdrAdminRating2.Visibility = lblAdminRating2.Visibility = System.Windows.Visibility.Collapsed;

                            btnDelete.Visibility = btnEdit.Visibility = System.Windows.Visibility.Collapsed;

                            break;
                        }
                    case 2:
                        {
                            tbStatus.Text = "Submitted";

                            if (DictionaryManager.AppObjects.ContainsKey("role") && DictionaryManager.AppObjects["role"].ToString().ToLower() == "admin")
                            {
                                btnApprove.Visibility = btnReject.Visibility = lblSubmittedLabel2.Visibility = lblSubtmittedByLabel2.Visibility = System.Windows.Visibility.Visible;
                                lblSubtmittedByLabel2.Text = oTemplate.SubmittedByName; btnSave.Visibility = System.Windows.Visibility.Visible;
                                lblApproveLabel2.Visibility = lblApprovedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                                lblRejectLabel2.Visibility = lblRejectedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                            }
                            else
                            {
                                btnSave.Visibility = btnApprove.Visibility = btnReject.Visibility = btnEdit.Visibility = System.Windows.Visibility.Collapsed;
                                rdrAdminRating2.Visibility = lblAdminRating2.Visibility = System.Windows.Visibility.Collapsed;
                                lblApproveLabel2.Visibility = lblApprovedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                                lblSubmittedLabel2.Visibility = lblSubtmittedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                                lblRejectLabel2.Visibility = lblRejectedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                            }

                            lblSubtmittedByLabel2.Text = oTemplate.SubmittedByName;
                            break;
                        }
                    case 3:
                        {
                            tbStatus.Text = "Approved";
                            if (DictionaryManager.AppObjects.ContainsKey("role") && DictionaryManager.AppObjects["role"].ToString().ToLower() == "admin")
                            {

                                btnReject.Visibility = lblSubmittedLabel2.Visibility = lblSubtmittedByLabel2.Visibility = System.Windows.Visibility.Visible;

                                lblApproveLabel2.Visibility = lblApprovedByLabel2.Visibility = btnEdit.Visibility = btnSave.Visibility = System.Windows.Visibility.Visible;

                                btnApprove.Visibility = lblRejectLabel2.Visibility = lblRejectedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                            }
                            else
                            {
                                btnSave.Visibility = btnEdit.Visibility = System.Windows.Visibility.Collapsed;

                                lblApproveLabel2.Visibility = lblApprovedByLabel2.Visibility = System.Windows.Visibility.Visible;
                                lblSubmittedLabel2.Visibility = lblSubtmittedByLabel2.Visibility = System.Windows.Visibility.Visible;
                                lblRejectLabel2.Visibility = lblRejectedByLabel2.Visibility = System.Windows.Visibility.Collapsed;
                                rdrAdminRating2.IsReadOnly = true;
                            }
                            rdrAdminRating2.Visibility = lblAdminRating2.Visibility = System.Windows.Visibility.Visible;
                            lblSubtmittedByLabel2.Text = oTemplate.SubmittedByName;
                            lblApprovedByLabel2.Text = oTemplate.ApprovedByName;
                            break;
                        }
                    case 4:
                        {
                            tbStatus.Text = "Rejected";
                            if (DictionaryManager.AppObjects.ContainsKey("role") && DictionaryManager.AppObjects["role"].ToString().ToLower() == "admin")
                            {

                                lblSubmittedLabel2.Visibility = lblSubtmittedByLabel2.Visibility = System.Windows.Visibility.Visible;

                                lblApproveLabel2.Visibility = lblApprovedByLabel2.Visibility = System.Windows.Visibility.Visible;




                                btnApprove.Visibility = btnSave.Visibility = System.Windows.Visibility.Visible;
                            }
                            else
                            {
                                btnSave.Visibility = btnSubmit.Visibility = System.Windows.Visibility.Visible;
                                btnApprove.Visibility = btnReject.Visibility = System.Windows.Visibility.Collapsed;

                                lblApproveLabel2.Visibility = lblApprovedByLabel2.Visibility = System.Windows.Visibility.Visible;
                                lblSubmittedLabel2.Visibility = lblSubtmittedByLabel2.Visibility = System.Windows.Visibility.Visible;

                                rdrAdminRating2.IsReadOnly = true;
                            }
                            lblRejectLabel2.Visibility = lblRejectedByLabel2.Visibility = System.Windows.Visibility.Visible;
                            rdrAdminRating2.Visibility = lblAdminRating2.Visibility = System.Windows.Visibility.Visible;
                            lblSubtmittedByLabel2.Text = oTemplate.SubmittedByName;
                            lblApproveLabel2.Text = "Rejected By";
                            lblApprovedByLabel2.Text = oTemplate.ApprovedByName;
                            lblRejectedByLabel2.Text = oTemplate.RejectionReason;
                            lblRejectedByLabel2.TextWrapping = TextWrapping.Wrap;
                            break;
                        }
                    default:
                        break;
                }

                txtTemplateName.Text = lblProductName.Text = oTemplate.ProductName;
                //cboCategory.SelectedValue = oTemplate.ProductCategoryID;

                if (oTemplate.MatchingSetID != null)
                {
                    cboMatchingSet.SelectedValue = oTemplate.MatchingSetID;
                }
                else
                {
                    DataLoading = true;
                    cboMatchingSet.SelectedValue = 0;
                    DataLoading = false;
                }

                if (oTemplate.BaseColorID.HasValue)
                {
                    cboBaseColors2.SelectedValue = oTemplate.BaseColorID;
                }
                txtNarrativeTag2.Text = oTemplate.Description;

                //if (oTemplate.Thumbnail != null)
                //{
                //    oTemplate.Thumbnail = "/designer/products/" + oTemplate.Thumbnail;
                //}
                //else
                //{
                //    oTemplate.Thumbnail = "/images/nothumb.png";
                //}

                //imgThumb.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(oTemplate.Thumbnail, UriKind.Relative));

                rdrAdminRating2.Value = Convert.ToDouble(oTemplate.MPCRating.Value);



                clProduct.GetTemplateIndustryTagsCompleted += new EventHandler<ProductServiceReference.GetTemplateIndustryTagsCompletedEventArgs>(clProduct_GetTemplateIndustryTagsCompleted);

                if (DictionaryManager.AppObjects.ContainsKey("productid") && DictionaryManager.AppObjects["productid"] != null)
                {
                    clProduct.GetTemplateIndustryTagsAsync(Convert.ToInt32(DictionaryManager.AppObjects["productid"]));
                }
                else
                {
                    clProduct.GetTemplateIndustryTagsAsync(0);
                }

                txtTemplateName2.Text = e.Result.ProductName;

                 clProduct.GetMatchingSetbyIDCompleted += new EventHandler<GetMatchingSetbyIDCompletedEventArgs>(clProduct_GetMatchingSetbyIDCompleted);
                 clProduct.GetMatchingSetbyIDAsync(Convert.ToInt32(DictionaryManager.AppObjects["MatchingSetID"]));

                 clProduct.GetMatchingSetTemplatesListCompleted += new EventHandler<GetMatchingSetTemplatesListCompletedEventArgs>(clProduct_GetMatchingSetTemplatesListCompleted);
                 clProduct.GetMatchingSetTemplatesListAsync(Convert.ToInt32(DictionaryManager.AppObjects["MatchingSetID"]), e.Result.ProductName);

               

            }
        }

      

        void clProduct_GetMatchingSetTemplatesListCompleted(object sender, GetMatchingSetTemplatesListCompletedEventArgs e)
        {
            
            datagrdMatchingSetDetails.ItemsSource = e.Result;

            ProgressBar1.IsIndeterminate = false;
            ProgressPanel.Visibility = Visibility.Collapsed;
            ProgressBorder.Visibility = System.Windows.Visibility.Collapsed;

            
        }


       

        private void FillControls()
        {

            clProduct.GetMatchingSetsCompleted += new EventHandler<ProductServiceReference.GetMatchingSetsCompletedEventArgs>(clProduct_GetMatchingSetsCompleted);
            clProduct.GetMatchingSetsAsync();
            
          

         
        }

        void clProduct_GetMatchingSetsCompleted(object sender, ProductServiceReference.GetMatchingSetsCompletedEventArgs e)
        {
            try
            {
                MatchingSets onode = new MatchingSets();
                onode.MatchingSetID = 0;
                onode.MatchingSetName = "None";

                List<MatchingSets> oList = e.Result.ToList();
                oList.Insert(0, onode);

                DataLoading = true;

                cboMatchingSet.ItemsSource = oList;

                cboMatchingSet.DisplayMemberPath = "MatchingSetName";
                cboMatchingSet.SelectedValuePath = "MatchingSetID";

                cboMatchingSet.SelectedIndex = 0;

                DataLoading = false;

                clProduct.GetCategoriesCompleted += new EventHandler<ProductServiceReference.GetCategoriesCompletedEventArgs>(clProduct_GetCategoriesCompleted);
                clProduct.GetCategoriesAsync();
            }

            catch (Exception ex)
            {
                MessageBox.Show("::objSrv_GetCategoriesCompleted::" + ex.ToString());
            }
        }

        void clProduct_GetProductByIdCompleted(object sender, ProductServiceReference.GetProductByIdCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                
                oTemplate = e.Result;
                switch (oTemplate.Status)
                {
                    case 1:
                        {
                            tbStatus.Text = "Draft";
                            btnSave.Visibility = btnEdit.Visibility = btnDelete.Visibility = System.Windows.Visibility.Visible;
                            lblSubmittedLabel.Visibility = lblSubtmittedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                            lblApproveLabel.Visibility = lblApprovedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                            lblRejectLabel.Visibility = lblRejectedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                            rdrAdminRating.Visibility = lblAdminRating.Visibility = System.Windows.Visibility.Collapsed;
                            btnSubmit.Visibility = System.Windows.Visibility.Visible;
                            break;
                        }
                    case 2:
                        {
                            tbStatus.Text = "Submitted";

                            if (DictionaryManager.AppObjects.ContainsKey("role") && DictionaryManager.AppObjects["role"].ToString().ToLower() == "admin")
                            {
                                btnApprove.Visibility = btnReject.Visibility = lblSubmittedLabel.Visibility = lblSubtmittedByLabel.Visibility = System.Windows.Visibility.Visible;
                                lblSubtmittedByLabel.Text = oTemplate.SubmittedByName; btnEdit.Visibility = btnSave.Visibility = System.Windows.Visibility.Visible;
                                lblApproveLabel.Visibility = lblApprovedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                                lblRejectLabel.Visibility = lblRejectedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                            }
                            else
                            {
                                btnSave.Visibility = btnApprove.Visibility = btnReject.Visibility = btnEdit.Visibility = System.Windows.Visibility.Collapsed;
                                rdrAdminRating.Visibility = lblAdminRating.Visibility = System.Windows.Visibility.Collapsed;
                                lblApproveLabel.Visibility = lblApprovedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                                lblSubmittedLabel.Visibility = lblSubtmittedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                                lblRejectLabel.Visibility = lblRejectedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                            }
                            
                             
                            break;
                        }
                    case 3:
                        {
                            tbStatus.Text = "Approved";
                            if (DictionaryManager.AppObjects.ContainsKey("role") && DictionaryManager.AppObjects["role"].ToString().ToLower() == "admin")
                            {
                               
                                btnReject.Visibility = lblSubmittedLabel.Visibility = lblSubtmittedByLabel.Visibility = System.Windows.Visibility.Visible;
                               
                                lblApproveLabel.Visibility = lblApprovedByLabel.Visibility = btnEdit.Visibility = btnSave.Visibility = System.Windows.Visibility.Visible;
                                
                                btnApprove.Visibility = lblRejectLabel.Visibility = lblRejectedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                            }
                            else
                            {
                                btnSave.Visibility = btnEdit.Visibility = System.Windows.Visibility.Collapsed;
                                
                                lblApproveLabel.Visibility = lblApprovedByLabel.Visibility = System.Windows.Visibility.Visible;
                                lblSubmittedLabel.Visibility = lblSubtmittedByLabel.Visibility = System.Windows.Visibility.Visible;
                                lblRejectLabel.Visibility = lblRejectedByLabel.Visibility = System.Windows.Visibility.Collapsed;
                                rdrAdminRating.IsReadOnly = true;
                            }
                            rdrAdminRating.Visibility = lblAdminRating.Visibility = System.Windows.Visibility.Visible;
                            lblSubtmittedByLabel.Text = oTemplate.SubmittedByName;
                            lblApprovedByLabel.Text = oTemplate.ApprovedByName;
                            break;
                        }
                    case 4:
                        {
                            tbStatus.Text = "Rejected";
                            if (DictionaryManager.AppObjects.ContainsKey("role") && DictionaryManager.AppObjects["role"].ToString().ToLower() == "admin")
                            {

                                lblSubmittedLabel.Visibility = lblSubtmittedByLabel.Visibility = System.Windows.Visibility.Visible;

                                lblApproveLabel.Visibility = lblApprovedByLabel.Visibility = System.Windows.Visibility.Visible;

                                
                           

                                btnApprove.Visibility =  btnEdit.Visibility = btnSave.Visibility = System.Windows.Visibility.Visible;
                            }
                            else
                            {
                                btnSave.Visibility = btnSubmit.Visibility  = btnEdit.Visibility = System.Windows.Visibility.Visible;
                                btnApprove.Visibility= btnReject.Visibility = System.Windows.Visibility.Collapsed;

                                lblApproveLabel.Visibility = lblApprovedByLabel.Visibility = System.Windows.Visibility.Visible;
                                lblSubmittedLabel.Visibility = lblSubtmittedByLabel.Visibility = System.Windows.Visibility.Visible;
                               
                                rdrAdminRating.IsReadOnly = true;
                            }
                            lblRejectLabel.Visibility = lblRejectedByLabel.Visibility = System.Windows.Visibility.Visible;
                            rdrAdminRating.Visibility = lblAdminRating.Visibility = System.Windows.Visibility.Visible;
                            lblSubtmittedByLabel.Text = oTemplate.SubmittedByName;
                            lblApproveLabel.Text = "Rejected By";
                            lblApprovedByLabel.Text = oTemplate.ApprovedByName;
                            lblRejectedByLabel.Text = oTemplate.RejectionReason;
                            lblRejectedByLabel.TextWrapping = TextWrapping.Wrap;
                            break;
                        }
                    default:
                        break;
                }

                txtTemplateCode.Text = oTemplate.Code;
                txtTemplateName.Text = lblProductName.Text = oTemplate.ProductName;
                cboCategory.SelectedValue = oTemplate.ProductCategoryID;

                if (oTemplate.MatchingSetID != null)
                {
                    cboMatchingSet.SelectedValue = oTemplate.MatchingSetID;
                }
                //else
                //{
                //    cboMatchingSet.SelectedValue = 0;
                //}
                
                if (oTemplate.BaseColorID.HasValue)
                {
                    cboBaseColors.SelectedValue = oTemplate.BaseColorID;
                }
                txtNarrativeTag.Text = oTemplate.Description;


                if (oTemplate.Orientation == 1)
                {
                    rbtnOrientationHorz.IsChecked = true;
                }
                else if (oTemplate.Orientation == 2)
                {
                    rbtnOrientationVert.IsChecked = true;
                }
                else
                {
                    rbtnOrientationHorz.IsChecked = true;
                }

                if (oTemplate.Thumbnail != null)
                {
                    oTemplate.Thumbnail = "/designer/products/" + oTemplate.Thumbnail;
                }
                else
                {
                   oTemplate.Thumbnail = "/images/nothumb.png";
                }


                if (oTemplate.SLThumbnail != null)
                {
                    oTemplate.SLThumbnail = "/designer/products/" + oTemplate.SLThumbnail;
                    ImgSLThumbnail.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(oTemplate.SLThumbnail, UriKind.Relative));
                }

                if (oTemplate.SuperView != null)
                {
                    oTemplate.SuperView = "/designer/products/" + oTemplate.SuperView;
                    ImgSuperView.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(oTemplate.SuperView, UriKind.Relative));
                }

                if (oTemplate.FullView != null)
                {
                    oTemplate.FullView = "/designer/products/" + oTemplate.FullView;
                    ImgFullView.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(oTemplate.FullView, UriKind.Relative));
                }

                imgThumb.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(oTemplate.Thumbnail, UriKind.Relative));
                
                rdrAdminRating.Value = Convert.ToDouble(oTemplate.MPCRating.Value);

                

                clProduct.GetTemplateIndustryTagsCompleted += new EventHandler<ProductServiceReference.GetTemplateIndustryTagsCompletedEventArgs>(clProduct_GetTemplateIndustryTagsCompleted);

                if (DictionaryManager.AppObjects.ContainsKey("productid") && DictionaryManager.AppObjects["productid"] != null)
                {
                    clProduct.GetTemplateIndustryTagsAsync(Convert.ToInt32(DictionaryManager.AppObjects["productid"]));
                }
                else
                {
                    clProduct.GetTemplateIndustryTagsAsync(0);
                }

                EditModeCatChanged = false;

            }
        }
        
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DictionaryManager.CreateNewTemplate();
        }

        void clProduct_GetTemplatesCompleted(object sender, ProductServiceReference.GetTemplatesCompletedEventArgs e)
        {
            try
            {
                lstTemplates = new List<ProductServiceReference.Templates>(e.Result);


                if (DictionaryManager.AppObjects.ContainsKey("productid") && DictionaryManager.AppObjects["productid"] != null) //edit template case
                {

                    clProduct.GetProductByIdCompleted += new EventHandler<ProductServiceReference.GetProductByIdCompletedEventArgs>(clProduct_GetProductByIdCompleted);
                    clProduct.GetProductByIdAsync(Convert.ToInt32(DictionaryManager.AppObjects["productid"]));
                }

                else //new template case
                {
                    clProduct.GetTemplateIndustryTagsCompleted += new EventHandler<ProductServiceReference.GetTemplateIndustryTagsCompletedEventArgs>(clProduct_GetTemplateIndustryTagsCompleted);
                    clProduct.GetTemplateIndustryTagsAsync(0);
                }

               
            }

            catch (Exception ex)
            {
                MessageBox.Show("::objSrv_GetCategoriesCompleted::" + ex.ToString());
            }
        }

        void clProduct_GetTemplateThemeTagsCompleted(object sender, ProductServiceReference.GetTemplateThemeTagsCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    if (DictionaryManager.AppObjects.ContainsKey("mode") && DictionaryManager.AppObjects["mode"] != null && DictionaryManager.AppObjects["mode"].ToString() == "matchingset")
                    {
                        lstbxStyleTags2.ItemsSource = e.Result;
                    }
                    else
                        lstbxStyleTags.ItemsSource = e.Result;


                    //this is the case of creating a new template with matching set information. so apply the following values on UI for auto populate.
                    if (DictionaryManager.AppObjects.ContainsKey("MatchingSetID") && DictionaryManager.AppObjects["MatchingSetID"] != null)
                    {
                        cboMatchingSet.SelectedValue = DictionaryManager.AppObjects["MatchingSetID"];
                    }

                    if (DictionaryManager.AppObjects.ContainsKey("productname") &&  DictionaryManager.AppObjects["productname"] != null)
                    {
                        txtTemplateName.Text = DictionaryManager.AppObjects["productname"].ToString();
                        DictionaryManager.AppObjects.Remove("productname");
                    }

                    if (DictionaryManager.AppObjects.ContainsKey("productcategoryid") && DictionaryManager.AppObjects["productcategoryid"] != null)
                    {
                        cboCategory.SelectedValue = DictionaryManager.AppObjects["productcategoryid"];
                        DictionaryManager.AppObjects.Remove("productcategoryid");
                    }

                    if (DictionaryManager.AppObjects.ContainsKey("narrativetag") && DictionaryManager.AppObjects["narrativetag"] != null)
                    {
                        txtNarrativeTag.Text = DictionaryManager.AppObjects["narrativetag"].ToString();
                        DictionaryManager.AppObjects.Remove("narrativetag");

                    }

                    if (DictionaryManager.AppObjects.ContainsKey("basecolorid") && DictionaryManager.AppObjects["basecolorid"] != null)
                    {
                        cboBaseColors.SelectedValue = DictionaryManager.AppObjects["basecolorid"];
                        DictionaryManager.AppObjects.Remove("basecolorid");
                    }


                    if (DictionaryManager.AppObjects.ContainsKey("industrytagids") && DictionaryManager.AppObjects["industrytagids"] != null)
                    {
                        ObservableCollection<int> IndustryTagIDs = (ObservableCollection<int>)DictionaryManager.AppObjects["industrytagids"];

                        foreach (sp_GetTemplateIndustryTags_Result item in lstbxIndustryTags.Items)
                        {
                            if (IndustryTagIDs.Contains(item.TagID))
                            {
                                item.selected = 1;
                            }
                        }

                        DictionaryManager.AppObjects.Remove("industrytagids");
                    }

                    if (DictionaryManager.AppObjects.ContainsKey("themetagids") && DictionaryManager.AppObjects["themetagids"] != null)
                    {
                        ObservableCollection<int> ThemeTagIDs = (ObservableCollection<int>)DictionaryManager.AppObjects["themetagids"];

                        foreach (sp_GetTemplateThemeTags_Result item in lstbxStyleTags.Items)
                        {
                            if (ThemeTagIDs.Contains(item.TagID))
                            {
                                item.selected = 1;
                            }
                        }
                        DictionaryManager.AppObjects.Remove("themetagids");
                    }


                    btnSave.IsEnabled = true;
                    btnEdit.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                    btnReject.IsEnabled = true;
                    btnSubmit.IsEnabled = true;
                    

                   
                        ProgressBar1.IsIndeterminate = false;
                        ProgressPanel.Visibility = Visibility.Collapsed;
                        ProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
                   
                   
                   
                }

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        void clProduct_GetTemplateIndustryTagsCompleted(object sender, ProductServiceReference.GetTemplateIndustryTagsCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    if (DictionaryManager.AppObjects.ContainsKey("mode") && DictionaryManager.AppObjects["mode"] != null && DictionaryManager.AppObjects["mode"].ToString() == "matchingset")
                    {
                        lstbxIndustryTags2.ItemsSource = e.Result;
                    }
                    else
                    {
                        lstbxIndustryTags.ItemsSource = e.Result;
                    }

                    clProduct.GetTemplateThemeTagsCompleted += new EventHandler<ProductServiceReference.GetTemplateThemeTagsCompletedEventArgs>(clProduct_GetTemplateThemeTagsCompleted);

                    if (DictionaryManager.AppObjects.ContainsKey("productid") && DictionaryManager.AppObjects["productid"] != null)
                    {

                        clProduct.GetTemplateThemeTagsAsync(Convert.ToInt32(DictionaryManager.AppObjects["productid"]));
                    }
                    else
                    {
                       
                        clProduct.GetTemplateThemeTagsAsync(0);
                    }
                }
            }
            catch (Exception Ex)
            {

                throw;
            }
        }

        void clProduct_GetBaseColorsCompleted(object sender, ProductServiceReference.GetBaseColorsCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    if (DictionaryManager.AppObjects.ContainsKey("mode") && DictionaryManager.AppObjects["mode"] != null && DictionaryManager.AppObjects["mode"].ToString() == "matchingset")
                    {

                        cboBaseColors2.ItemsSource = e.Result;
                        //cboBaseColors2.DisplayMemberPath = "Color";
                        cboBaseColors2.SelectedValuePath = "BaseColorID";

                        clProduct.GetProductByIdCompleted += new EventHandler<GetProductByIdCompletedEventArgs>(clProduct_GetProductByIdCompletedSimple);
                        clProduct.GetProductByIdAsync(Convert.ToInt32(DictionaryManager.AppObjects["productid"]));
                    }
                    else
                    {
                        cboBaseColors.ItemsSource = e.Result;
                        //cboBaseColors.DisplayMemberPath = "Color";
                        cboBaseColors.SelectedValuePath = "BaseColorID";

                        clProduct.GetTemplatesCompleted += new EventHandler<ProductServiceReference.GetTemplatesCompletedEventArgs>(clProduct_GetTemplatesCompleted);
                        clProduct.GetTemplatesAsync("", 0, 0, 12, false, 0, Convert.ToInt32(DictionaryManager.AppObjects["userid"].ToString()), DictionaryManager.AppObjects["role"].ToString());
                    }

                   

                    
                }
            }
            catch (Exception Ex)
            {

                throw;
            }
        }

        void clProduct_GetMatchingSetThemeCompleted(object sender, ProductServiceReference.GetMatchingSetThemeCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    txtTemplateName.ItemsSource = e.Result;
                    //cboMatchingSetName.ValueMemberPath = "MatchingSetTheme";
                    //cboMatchingSetName.DisplayMemberPath = "MatchingSetTheme";
                    //cboMatchingSetName.SelectedValuePath = "MatchingSetTheme";

                    
                    clProduct.GetBaseColorsCompleted += new EventHandler<ProductServiceReference.GetBaseColorsCompletedEventArgs>(clProduct_GetBaseColorsCompleted);
                    clProduct.GetBaseColorsAsync();
                }
            }
            catch (Exception Ex)
            {

                throw;
            }
        }
        void clProduct_GetCategoriesCompleted(object sender, ProductServiceReference.GetCategoriesCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    cboCategory.ItemsSource = e.Result;
                    cboCategory.DisplayMemberPath = "CategoryName";
                    cboCategory.SelectedValuePath = "ProductCategoryID";

                    lstProductCategory = new List<ProductServiceReference.vw_ProductCategoriesLeafNodes>(e.Result);


                    
                    clProduct.GetMatchingSetThemeCompleted += new EventHandler<ProductServiceReference.GetMatchingSetThemeCompletedEventArgs>(clProduct_GetMatchingSetThemeCompleted);
                    clProduct.GetMatchingSetThemeAsync();
                }
            }
            catch (Exception Ex)
            {

                throw;
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveTemplate(SaveAction.Save);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.ListView, DictionaryManager.Pages.ListView);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            //int MatchingSetTotalCount = datagrdMatchingSetDetails.Items.Count;
            //int MatchingSetActualTemplatesCount = 0;

            //foreach (sp_GetMatchingSetTemplatesList_Result item in datagrdMatchingSetDetails.ItemsSource)
            //{
            //    if (item.ProductID != null)
            //    {
            //        MatchingSetActualTemplatesCount++;
            //    }
            //}

            //if (MatchingSetTotalCount == MatchingSetActualTemplatesCount)
            //{


            //    if (System.Windows.MessageBox.Show("Are you sure you want to submit this Matchingset ?", "Confirm Submission", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            //    {
            //        SaveTemplate(SaveAction.Submit);
            //    }
            //}
            //else
            //{
            //    System.Windows.MessageBox.Show("Please create the missing templates in this matching set before it can be submitted", "Matchingset Incomplete", MessageBoxButton.OK);
            //}

             if (System.Windows.MessageBox.Show("Are you sure you want to submit this template ?", "Confirm Submission", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
             {
                 SaveTemplate(SaveAction.Submit);
             }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (System.Windows.MessageBox.Show("Are you sure you want to remove this template ?", "Confirm Removal", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (DictionaryManager.AppObjects.ContainsKey("productid") && DictionaryManager.AppObjects["productid"] != null)
                {
                    
                    webprintDesigner.ProductServiceReference.ProductServiceClient clproduct = new ProductServiceReference.ProductServiceClient();
                    clproduct.DeleteTemplateCompleted += new EventHandler<ProductServiceReference.DeleteTemplateCompletedEventArgs>(clproduct_DeleteTemplateCompleted);
                    clproduct.DeleteTemplateAsync(Convert.ToInt32(DictionaryManager.AppObjects["productid"]));
                }
            }
        }

        void clproduct_DeleteTemplateCompleted(object sender, ProductServiceReference.DeleteTemplateCompletedEventArgs e)
        {
            try
            {
                if (e.Result == true)
                {
                    System.Windows.MessageBox.Show("Template has been deleted.");

                    if (DictionaryManager.AppObjects["mode"].ToString() == "setedit" || DictionaryManager.AppObjects["mode"].ToString() == "setnew")
                    {
                        if (DictionaryManager.AppObjects.ContainsKey("productidtemp") && DictionaryManager.AppObjects["productidtemp"] != null)
                        {
                            DictionaryManager.AppObjects["productid"] = DictionaryManager.AppObjects["productidtemp"];
                        }

                        DictionaryManager.AppObjects["mode"] = "matchingset";
                        ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);
                    }
                    else
                    {
                        ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.ListView, DictionaryManager.Pages.ListView);
                    }
                    
                }
                else
                {
                    System.Windows.MessageBox.Show("The action could not be completed.");
                }
            }
            catch (Exception ex )
            {
                throw;
            }
        }

        bool isEditDesign = false;

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            isEditDesign = true;
            SaveTemplate(SaveAction.Save);
            
        }

        private void SaveTemplate(SaveAction Action)
        {
            try
            {
                


                bool bVaalidate1 = false;
                bool bVaalidate2 = false;

                if (cboCategory.SelectedItem != null)
                {
                    bVaalidate1 = true;
                }

                if (txtTemplateName.Text != string.Empty)
                {
                    bVaalidate2 = true;
                }

                if (DictionaryManager.AppObjects.ContainsKey("mode") && DictionaryManager.AppObjects["mode"] != null && DictionaryManager.AppObjects["mode"].ToString() == "matchingset")
                {
                    bVaalidate1 = true;

                    if (txtTemplateName2.Text != string.Empty)
                    {
                        bVaalidate2 = true;
                    }
                }


                if (bVaalidate1 && bVaalidate2)
                {

                    ProgressBar1.IsIndeterminate = true;
                    ProgessTxt.Text = "Saving Data";
                    ProgressPanel.Visibility = Visibility.Visible;
                    ProgressBorder.Visibility = System.Windows.Visibility.Visible;

                    string ErrorMessage = string.Empty;
                    string ErrorDesc = string.Empty;
                    webprintDesigner.ProductServiceReference.ProductServiceClient clProduct = new ProductServiceReference.ProductServiceClient();


                    if (DictionaryManager.AppObjects.ContainsKey("mode") && DictionaryManager.AppObjects["mode"] != null && DictionaryManager.AppObjects["mode"].ToString() == "matchingset")
                    {

                        ObservableCollection<int> productIDs = new ObservableCollection<int>();
                        ObservableCollection<int> IndustryTagIDs = new ObservableCollection<int>();
                        ObservableCollection<int> ThemeTagIDs = new ObservableCollection<int>();

                        foreach (sp_GetMatchingSetTemplatesList_Result item in datagrdMatchingSetDetails.ItemsSource)
                        {
                            if (item.ProductID != null)
                            {
                                productIDs.Add(item.ProductID.Value);
                            }
                        }

                        foreach (sp_GetTemplateIndustryTags_Result item in lstbxIndustryTags2.Items)
                        {
                            if (item.selected == 1)
                            {
                                IndustryTagIDs.Add(item.TagID);
                            }
                        }



                        foreach (sp_GetTemplateThemeTags_Result item in lstbxStyleTags2.Items)
                        {
                            if (item.selected == 1)
                            {
                                ThemeTagIDs.Add(item.TagID);
                            }
                        }

                        int? basecolor = 0;
                        if (cboBaseColors2.SelectedValue != null)
                            basecolor = Convert.ToInt32(cboBaseColors2.SelectedValue);
                        else
                            basecolor = null;


                        int? Rating = 0;

                        if (Convert.ToInt32(rdrAdminRating2.Value) != 0)
                            Rating = Convert.ToInt32(rdrAdminRating2.Value);
                        else
                            Rating = null;

                        clProduct.UpdateMatchingSetTemplatesCompleted += new EventHandler<UpdateMatchingSetTemplatesCompletedEventArgs>(clProduct_UpdateMatchingSetTemplatesCompleted);
                        clProduct.UpdateMatchingSetTemplatesAsync(productIDs, txtTemplateName2.Text, txtNarrativeTag2.Text, basecolor, IndustryTagIDs, ThemeTagIDs, Convert.ToInt32(Action), Convert.ToInt32(DictionaryManager.AppObjects["userid"]), DictionaryManager.AppObjects["fullname"].ToString(), txtRejectionReason.Text, Rating);

                        //call the service

                       


                       
                    }
                    else if (DictionaryManager.AppObjects.ContainsKey("mode") && (DictionaryManager.AppObjects["mode"].ToString() == "new" || DictionaryManager.AppObjects["mode"].ToString() == "setnew"))
                    {

                        webprintDesigner.ProductServiceReference.Templates oTemplate = new ProductServiceReference.Templates();

                        

                        oTemplate.SubmittedBy = Convert.ToInt32(DictionaryManager.AppObjects["userid"]);
                        oTemplate.MPCRating = Convert.ToInt32(rdrAdminRating.Value);
                        oTemplate.Status = 1;

                        oTemplate.Code = txtTemplateCode.Text;
                        oTemplate.ProductName = txtTemplateName.Text;

                        oTemplate.CuttingMargin = Common.MMToPoint(5);
                        //cannot be null
                        oTemplate.ProductCategoryID = Convert.ToInt32(cboCategory.SelectedValue);

                        if (cboMatchingSet.SelectedIndex == 0)
                            oTemplate.MatchingSetID = null;
                        else
                            oTemplate.MatchingSetID = Convert.ToInt32(cboMatchingSet.SelectedValue);


                        oTemplate.Description = txtNarrativeTag.Text;
                        oTemplate.IsDoubleSide = true;
                        oTemplate.Type = 1;
                        oTemplate.PDFTemplateWidth = 0;
                        oTemplate.PDFTemplateHeight = 0;

                        oTemplate.Type = 1;
                        oTemplate.IsPrePrint = false;
                        oTemplate.IsMultiPage = false;
                        oTemplate.TotelPage = 1;

                        if (rbtnOrientationHorz.IsChecked.Value == true)
                            oTemplate.Orientation = 1;
                        else
                            oTemplate.Orientation = 2;

                        oTemplate.IsNotUseDesigner = false;
                        oTemplate.IsUsePDFFile = true;
                        oTemplate.IsUseSide2BackGroundColor = false;
                        oTemplate.IsUseBackGroundColor = false;
                        //cannot be null

                        if (cboBaseColors.SelectedValue != null)
                        {
                            oTemplate.BaseColorID = Convert.ToInt32(cboBaseColors.SelectedValue);
                        }


                        //new thumbnails

                        if (SLThumbnail != null && SLThumbnail.Length > 0)
                        {
                            oTemplate.SLThumbnail = SLThumbnailFileName;
                            oTemplate.SLThumbnaillByte = SLThumbnail;
                        }

                        if (FullView != null && FullView.Length > 0)
                        {
                            oTemplate.FullView = FullViewFileName;
                            oTemplate.FullViewByte = FullView;
                        }

                        if (SuperView != null && SuperView.Length > 0)
                        {
                            oTemplate.SuperView = SuperViewFileName;
                            oTemplate.SuperViewByte = SuperView;
                        }

                        System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateIndustryTags> lstIndustry = new System.Collections.ObjectModel.ObservableCollection<ProductServiceReference.TemplateIndustryTags>();
                        System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateThemeTags> lstTheme = new System.Collections.ObjectModel.ObservableCollection<ProductServiceReference.TemplateThemeTags>();

                        for (int i = 0; i < lstbxIndustryTags.Items.Count; i++)
                        {
                            webprintDesigner.ProductServiceReference.sp_GetTemplateIndustryTags_Result o = (webprintDesigner.ProductServiceReference.sp_GetTemplateIndustryTags_Result)lstbxIndustryTags.Items[i];

                            if (o.selected == 1)
                            {
                                ProductServiceReference.TemplateIndustryTags item = new ProductServiceReference.TemplateIndustryTags();
                                item.TagID = o.TagID;
                                lstIndustry.Add(item);
                            }
                        }

                        for (int i = 0; i < lstbxStyleTags.Items.Count; i++)
                        {
                            webprintDesigner.ProductServiceReference.sp_GetTemplateThemeTags_Result o = (webprintDesigner.ProductServiceReference.sp_GetTemplateThemeTags_Result)lstbxStyleTags.Items[i];

                            if (o.selected == 1)
                            {
                                ProductServiceReference.TemplateThemeTags item = new ProductServiceReference.TemplateThemeTags();
                                item.TagID = o.TagID;
                                lstTheme.Add(item);
                            }
                        }

                        //set tags
                        clProduct.SaveTemplatesCompleted += new EventHandler<ProductServiceReference.SaveTemplatesCompletedEventArgs>(clProduct_SaveTemplatesCompleted);
                        clProduct.SaveTemplatesAsync(oTemplate, lstIndustry, lstTheme, true,EditModeCatChanged);

                    }
                    else if (DictionaryManager.AppObjects.ContainsKey("mode") && DictionaryManager.AppObjects.ContainsKey("productid") && DictionaryManager.AppObjects["productid"].ToString() != string.Empty && (DictionaryManager.AppObjects["mode"].ToString() == "edit" || DictionaryManager.AppObjects["mode"].ToString() == "setedit"))
                    {

                        int TemplateID = 0;
                        int.TryParse(DictionaryManager.AppObjects["productid"].ToString(), out TemplateID);

                        if (oTemplate != null)
                        {
                            if (Action == SaveAction.Submit)
                            {
                                oTemplate.Status = 2;
                                oTemplate.SubmittedByName = DictionaryManager.AppObjects["fullname"].ToString();
                                oTemplate.SubmitDate = DateTime.Now;
                                szMessage = "";
                            }
                            else if (Action == SaveAction.Approve)
                            {
                                oTemplate.Status = 3;
                                oTemplate.ApprovedBy = Convert.ToInt32(DictionaryManager.AppObjects["userid"]);
                                oTemplate.ApprovedByName = DictionaryManager.AppObjects["fullname"].ToString();
                                oTemplate.ApprovalDate = DateTime.Now;
                                szMessage = "";
                            }
                            else if (Action == SaveAction.Reject)
                            {
                                oTemplate.Status = 4;
                                oTemplate.ApprovedBy = Convert.ToInt32(DictionaryManager.AppObjects["userid"]);
                                oTemplate.ApprovedByName = DictionaryManager.AppObjects["fullname"].ToString();
                                oTemplate.RejectionReason = txtRejectionReason.Text;
                                oTemplate.ApprovalDate = DateTime.Now;
                                szMessage = "";
                            }


                            oTemplate.MPCRating = Convert.ToInt32(rdrAdminRating.Value);


                            oTemplate.ProductName = txtTemplateName.Text;
                            oTemplate.Description = txtNarrativeTag.Text;

                            oTemplate.ProductCategoryID = Convert.ToInt32(cboCategory.SelectedValue);

                            if (cboMatchingSet.SelectedIndex == 0)
                                oTemplate.MatchingSetID = null;
                            else
                                oTemplate.MatchingSetID = Convert.ToInt32(cboMatchingSet.SelectedValue);

                            //getting the selected category dimensions  and see if they are to be applied.

                            //var SelectedProductCategory = lstProductCategory.Where(g => g.ProductCategoryID == oTemplate.ProductCategoryID).Single();

                            oTemplate.Type = 1;
                            oTemplate.Code = txtTemplateCode.Text;

                            if (cboBaseColors.SelectedValue != null)
                            {
                                oTemplate.BaseColorID = Convert.ToInt32(cboBaseColors.SelectedValue);
                            }


                            if (rbtnOrientationHorz.IsChecked.Value == true)
                                oTemplate.Orientation = 1;
                            else
                                oTemplate.Orientation = 2;

                            //new thumbnails

                            if (SLThumbnail != null && SLThumbnail.Length > 0)
                            {
                                oTemplate.SLThumbnail = SLThumbnailFileName;
                                oTemplate.SLThumbnaillByte = SLThumbnail;
                            }

                            if (FullView != null && FullView.Length > 0)
                            {
                                oTemplate.FullView = FullViewFileName;
                                oTemplate.FullViewByte = FullView;
                            }

                            if (SuperView != null && SuperView.Length > 0)
                            {
                                oTemplate.SuperView = SuperViewFileName;
                                oTemplate.SuperViewByte = SuperView;
                            }


                           

                            System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateIndustryTags> lstIndustry = new System.Collections.ObjectModel.ObservableCollection<ProductServiceReference.TemplateIndustryTags>();
                            System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateThemeTags> lstTheme = new System.Collections.ObjectModel.ObservableCollection<ProductServiceReference.TemplateThemeTags>();


                            for (int i = 0; i < lstbxIndustryTags.Items.Count; i++)
                            {
                                webprintDesigner.ProductServiceReference.sp_GetTemplateIndustryTags_Result o = (webprintDesigner.ProductServiceReference.sp_GetTemplateIndustryTags_Result)lstbxIndustryTags.Items[i];

                                if (o.selected == 1)
                                {
                                    ProductServiceReference.TemplateIndustryTags item = new ProductServiceReference.TemplateIndustryTags();
                                    item.TagID = o.TagID;
                                    lstIndustry.Add(item);
                                }
                            }

                            for (int i = 0; i < lstbxStyleTags.Items.Count; i++)
                            {
                                webprintDesigner.ProductServiceReference.sp_GetTemplateThemeTags_Result o = (webprintDesigner.ProductServiceReference.sp_GetTemplateThemeTags_Result)lstbxStyleTags.Items[i];

                                if (o.selected == 1)
                                {
                                    ProductServiceReference.TemplateThemeTags item = new ProductServiceReference.TemplateThemeTags();
                                    item.TagID = o.TagID;
                                    lstTheme.Add(item);
                                }

                            }

                            //set tags
                            clProduct.SaveTemplatesCompleted += new EventHandler<ProductServiceReference.SaveTemplatesCompletedEventArgs>(clProduct_SaveTemplatesCompleted);
                            clProduct.SaveTemplatesAsync(oTemplate, lstIndustry, lstTheme, false, EditModeCatChanged);

                        }
                    }
                }
                else
                {
                    if (bVaalidate1 == false && bVaalidate2 == true)
                        MessageBox.Show("Please select a category to continue", "Validation Error", MessageBoxButton.OK);
                    else if (bVaalidate2 == false && bVaalidate1 == true)
                        MessageBox.Show("Please enter template name to continue", "Validation Error", MessageBoxButton.OK);
                    else
                    {
                        MessageBox.Show("Please select a category and enter template name to continue", "Validation Error", MessageBoxButton.OK);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        void clProduct_UpdateMatchingSetTemplatesCompleted(object sender, UpdateMatchingSetTemplatesCompletedEventArgs e)
        {
            if (e.Result == true)
            {
                if (szMessage != string.Empty)
                {
                    System.Windows.MessageBox.Show(szMessage);
                }
                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.ListView, DictionaryManager.Pages.ListView);
            }
            else
            {
                System.Windows.MessageBox.Show("Your action could not be completed. ");
            }
        }

        void clProduct_SaveTemplatesCompleted(object sender, ProductServiceReference.SaveTemplatesCompletedEventArgs e)
        {
            if (e.Result == true)
            {
                if (szMessage != string.Empty)
                {
                    System.Windows.MessageBox.Show(szMessage);
                }

                if (DictionaryManager.AppObjects["mode"].ToString() == "setedit" || DictionaryManager.AppObjects["mode"].ToString() == "setnew")
                {
                    if (DictionaryManager.AppObjects.ContainsKey("productidtemp") && DictionaryManager.AppObjects["productidtemp"] != null)
                    {
                        DictionaryManager.AppObjects["productid"] = DictionaryManager.AppObjects["productidtemp"];
                    }

                    DictionaryManager.AppObjects["mode"] = "matchingset";
                    ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);
                }
                else
                {
                    if (isEditDesign)
                        ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.Designer, DictionaryManager.Pages.Designer);
                    else
                        ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.ListView, DictionaryManager.Pages.ListView);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Your action could not be completed. ");
            }
        }

        enum SaveAction
        {
            Save = 1,
            Submit = 2,
            Approve = 3,
            Reject = 4
        }

        private void LayoutMain_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        { }

        private void LayoutMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                grdProducts.Width = LayoutMain.ActualWidth;
                grdProducts.Height = LayoutMain.ActualHeight - 15;
                double PBPox = 0;
                PBPox = (LayoutMain.ActualWidth / 2) - (ProgressPanel.Width / 2);
                if (PBPox > 0)
                    ProgressPanel.SetValue(Canvas.LeftProperty, PBPox);
                PBPox = 0;
                PBPox = (LayoutMain.ActualHeight / 2) - (ProgressPanel.Height / 2);
                if (PBPox > 0)
                    ProgressPanel.SetValue(Canvas.TopProperty, PBPox);
            }
            catch (Exception ex)
            {
                //if (ShowException)
                    MessageBox.Show("::LayoutMain_SizeChanged::" + ex.ToString());
            }
        }
        private void btnGotoProfile_Click(object sender, RoutedEventArgs e)
        {

            if (DictionaryManager.AppObjects["mode"].ToString() == "setedit" || DictionaryManager.AppObjects["mode"].ToString() == "setnew")
            {
                DictionaryManager.AppObjects["productid"] = DictionaryManager.AppObjects["productidtemp"];
                DictionaryManager.AppObjects["mode"] = "matchingset";
                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);
            }
            else
            {
                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.ListView, DictionaryManager.Pages.ListView);
            }
            
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {

            
            //if (System.Windows.MessageBox.Show("Are you sure you want to reject this template ?", "Confirm Rejection", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            //{
            //   
            //}
            
            RejectWindow.IsOpened = true;
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure you want to approve this template ?", "Confirm Approval", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                SaveTemplate(SaveAction.Approve);
            }
        }

        private void imgThumb_ImageOpened(object sender, RoutedEventArgs e)
        {
            ProgressBar1.IsIndeterminate = false;
            ProgressPanel.Visibility = Visibility.Collapsed;
            ProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnRejectionOK_Click(object sender, RoutedEventArgs e)
        {
            RejectWindow.IsOpened = false;
            SaveTemplate(SaveAction.Reject);
        }

        private void btnRejectionCancel_Click(object sender, RoutedEventArgs e)
        {
            RejectWindow.IsOpened = false;
        }

        private void lnkTemplateAction_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock tBlock = (TextBlock)sender;
            sp_GetMatchingSetTemplatesList_Result oItem =  (sp_GetMatchingSetTemplatesList_Result)tBlock.Tag;
            if (oItem.ProductID != null) //edit mode
            {
                ////DictionaryManager.AppObjects["productidtemp"] = DictionaryManager.AppObjects["productid"];
                DictionaryManager.AppObjects["productid"] = oItem.ProductID;
                
                ////DictionaryManager.AppObjects["mode"] = "setedit";
                ////((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);

                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.Designer, DictionaryManager.Pages.Designer);
            }
            else //new temp
            {
                //DictionaryManager.AppObjects["mode"] = "setnew";
                //DictionaryManager.AppObjects["productidtemp"] = DictionaryManager.AppObjects["productid"];
                //DictionaryManager.AppObjects.Remove("productid");


                ////tBlock.d

                //DictionaryManager.AppObjects["productname"] = txtTemplateName2.Text;
                //DictionaryManager.AppObjects["productcategoryid"] = oItem.ProductCategoryID;
                //DictionaryManager.AppObjects["narrativetag"] = txtNarrativeTag2.Text;
                //DictionaryManager.AppObjects["basecolorid"] = cboBaseColors2.SelectedValue;

                //ObservableCollection<int> IndustryTagIDs = new ObservableCollection<int>();
                //ObservableCollection<int> ThemeTagIDs = new ObservableCollection<int>();

                //foreach (sp_GetTemplateIndustryTags_Result item in lstbxIndustryTags2.Items)
                //{
                //    if (item.selected == 1)
                //    {
                //        IndustryTagIDs.Add(item.TagID);
                //    }
                //}

                //DictionaryManager.AppObjects["industrytagids"] = IndustryTagIDs;

                //foreach (sp_GetTemplateThemeTags_Result item in lstbxStyleTags2.Items)
                //{
                //    if (item.selected == 1)
                //    {
                //        ThemeTagIDs.Add(item.TagID);
                //    }
                //}

                //DictionaryManager.AppObjects["themetagids"] = ThemeTagIDs;

                //((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);

                //creating new template
                webprintDesigner.ProductServiceReference.Templates oTemplate = new ProductServiceReference.Templates();

               

                oTemplate.SubmittedBy = Convert.ToInt32(DictionaryManager.AppObjects["userid"]);
                oTemplate.MPCRating = Convert.ToInt32(rdrAdminRating.Value);
                oTemplate.Status = 1;

                
                oTemplate.ProductName = txtTemplateName2.Text;

                oTemplate.CuttingMargin = Common.MMToPoint(5);
                //cannot be null
                oTemplate.ProductCategoryID = oItem.ProductCategoryID;


                oTemplate.MatchingSetID = Convert.ToInt32(DictionaryManager.AppObjects["MatchingSetID"]);


                oTemplate.Description = txtNarrativeTag2.Text;
                oTemplate.IsDoubleSide = true;
                oTemplate.Type = 1;
                oTemplate.PDFTemplateWidth = 0;
                oTemplate.PDFTemplateHeight = 0;

                
                oTemplate.IsPrePrint = false;
                oTemplate.IsMultiPage = false;
                oTemplate.TotelPage = 1;
                oTemplate.Orientation = 1;
                oTemplate.IsNotUseDesigner = false;
                oTemplate.IsUsePDFFile = true;
                oTemplate.IsUseSide2BackGroundColor = false;
                oTemplate.IsUseBackGroundColor = false;
                //cannot be null

                if (cboBaseColors.SelectedValue != null)
                {
                    oTemplate.BaseColorID = Convert.ToInt32(cboBaseColors2.SelectedValue);
                }



                System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateIndustryTags> lstIndustry = new System.Collections.ObjectModel.ObservableCollection<ProductServiceReference.TemplateIndustryTags>();
                System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateThemeTags> lstTheme = new System.Collections.ObjectModel.ObservableCollection<ProductServiceReference.TemplateThemeTags>();

                for (int i = 0; i < lstbxIndustryTags2.Items.Count; i++)
                {
                    webprintDesigner.ProductServiceReference.sp_GetTemplateIndustryTags_Result o = (webprintDesigner.ProductServiceReference.sp_GetTemplateIndustryTags_Result)lstbxIndustryTags2.Items[i];

                    if (o.selected == 1)
                    {
                        ProductServiceReference.TemplateIndustryTags item = new ProductServiceReference.TemplateIndustryTags();
                        item.TagID = o.TagID;
                        lstIndustry.Add(item);
                    }
                }

                for (int i = 0; i < lstbxStyleTags2.Items.Count; i++)
                {
                    webprintDesigner.ProductServiceReference.sp_GetTemplateThemeTags_Result o = (webprintDesigner.ProductServiceReference.sp_GetTemplateThemeTags_Result)lstbxStyleTags2.Items[i];

                    if (o.selected == 1)
                    {
                        ProductServiceReference.TemplateThemeTags item = new ProductServiceReference.TemplateThemeTags();
                        item.TagID = o.TagID;
                        lstTheme.Add(item);
                    }
                }

                
                clProduct.SaveTemplatesCompleted += new EventHandler<ProductServiceReference.SaveTemplatesCompletedEventArgs>(clProduct_SaveTemplatesCompletedMatchingSet);
                clProduct.SaveTemplatesAsync(oTemplate, lstIndustry, lstTheme, true,EditModeCatChanged);



            }
        }

        void clProduct_SaveTemplatesCompletedMatchingSet(object sender, ProductServiceReference.SaveTemplatesCompletedEventArgs e)
        {
            if (e.Result == true)
            {
                if (szMessage != string.Empty)
                {
                    System.Windows.MessageBox.Show(szMessage);
                }

                DictionaryManager.AppObjects["productid"] = e.NewTemplateID;
                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.Designer, DictionaryManager.Pages.Designer);
            }
            else
            {
                System.Windows.MessageBox.Show("Your action could not be completed. ");
            }
        }

        private void cboMatchingSet_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!DataLoading)
            {
                ProgressBar1.IsIndeterminate = true;
                ProgessTxt.Text = "Loading Data";
                ProgressPanel.Visibility = Visibility.Visible;
                ProgressBorder.Visibility = System.Windows.Visibility.Visible;

                if (Convert.ToInt32( cboMatchingSet.SelectedValue) != 0)//get matching set categories
                {
                    clProduct = new ProductServiceClient();
                    clProduct.GetCategoriesByMatchingSetIDCompleted += new EventHandler<GetCategoriesByMatchingSetIDCompletedEventArgs>(clProduct_GetCategoriesByMatchingSetIDCompleted);
                    clProduct.GetCategoriesByMatchingSetIDAsync(Convert.ToInt32(cboMatchingSet.SelectedValue));
                }
                else //get all leaf nodes
                {
                    clProduct.GetCategoriesCompleted += new EventHandler<GetCategoriesCompletedEventArgs>(clProduct_GetCategoriesCompleted2);
                    clProduct.GetCategoriesAsync();
                }
            }

        }

        void clProduct_GetCategoriesByMatchingSetIDCompleted(object sender, GetCategoriesByMatchingSetIDCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    
                    cboCategory.ItemsSource = e.Result;
                    cboCategory.DisplayMemberPath = "CategoryName";
                    cboCategory.SelectedValuePath = "ProductCategoryID";
                    cboCategory.UpdateLayout();

                    ProgressBar1.IsIndeterminate = false;
                    ProgressPanel.Visibility = Visibility.Collapsed;
                    ProgressBorder.Visibility = System.Windows.Visibility.Collapsed;


                }
            }
            catch (Exception Ex)
            {

                throw;
            }
        }

        void clProduct_GetCategoriesCompleted2(object sender, ProductServiceReference.GetCategoriesCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    cboCategory.ItemsSource = null;
                    cboCategory.ItemsSource = e.Result;
                    cboCategory.DisplayMemberPath = "CategoryName";
                    cboCategory.SelectedValuePath = "ProductCategoryID";
                    cboCategory.UpdateLayout();

                    ProgressBar1.IsIndeterminate = false;
                    ProgressPanel.Visibility = Visibility.Collapsed;
                    ProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
                   
                }
            }
            catch (Exception Ex)
            {

                throw;
            }

        }

        private void cboCategory_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EditModeCatChanged = true;
        }

        private void rbtnOrientation_Checked(object sender, RoutedEventArgs e)
        {
            EditModeCatChanged = true;
        }

        private void btnSLThumbnail_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Image Files (*.jpg)|*.jpg|Png Image|*.png";
            if (ofd.ShowDialog().Value)
            {

                System.Windows.Media.Imaging.BitmapImage bitmap = new System.Windows.Media.Imaging.BitmapImage();

                FileStream oFS = ofd.File.OpenRead();
                FileInfo file = ofd.File;
                BinaryReader binaryReader = new BinaryReader(oFS);
                Byte[] fileBuffer = binaryReader.ReadBytes((int)oFS.Length);

                SLThumbnailFileName = file.ToString();
                SLThumbnail = fileBuffer;
                bitmap.SetSource(oFS);
                ImgSLThumbnail.Source = bitmap;
                oFS.Close();


            }
        }

        private void btnFullView_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Image Files (*.jpg)|*.jpg|Png Image|*.png";
            if (ofd.ShowDialog().Value)
            {

                System.Windows.Media.Imaging.BitmapImage bitmap = new System.Windows.Media.Imaging.BitmapImage();

                FileStream oFS = ofd.File.OpenRead();
                FileInfo file = ofd.File;
                BinaryReader binaryReader = new BinaryReader(oFS);
                Byte[] fileBuffer = binaryReader.ReadBytes((int)oFS.Length);

                FullViewFileName = file.ToString();
                FullView = fileBuffer;
                bitmap.SetSource(oFS);
                ImgFullView.Source = bitmap;
                oFS.Close();

            }
            
        }

        private void btnSuperView_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Image Files (*.jpg)|*.jpg|Png Image|*.png";
            if (ofd.ShowDialog().Value)
            {

                System.Windows.Media.Imaging.BitmapImage bitmap = new System.Windows.Media.Imaging.BitmapImage();
              
                FileStream oFS = ofd.File.OpenRead();
                FileInfo file = ofd.File;
                BinaryReader binaryReader = new BinaryReader(oFS);
                Byte[] fileBuffer = binaryReader.ReadBytes((int)oFS.Length);
                             
                SuperViewFileName = file.ToString();
                SuperView = fileBuffer;
                bitmap.SetSource(oFS);
                ImgSuperView.Source = bitmap;
                oFS.Close();

            }
        }

       


        
    }
}

