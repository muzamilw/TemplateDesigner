using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using webprintDesigner.ProductServiceReference;
using Telerik.Windows.Controls;
using System.Linq;
using System.Linq.Expressions;

namespace webprintDesigner
{
    public partial class TemplatesList : UserControl
    {
        bool SearchEventMode = false;
        int CurrentPage = -1;
        int PageSize = 12;

        Point _clickPosition;
        public DateTime _lastClick = DateTime.Now;
        private bool _firstClickDone = false;

        public TemplatesList()
        {
            // Required to initialize variables
            InitializeComponent();
            this.Loaded += OnLoad;

            radDataPager1.PageSize = PageSize;
            radDataPager1.PageIndexChanged += new EventHandler<PageIndexChangedEventArgs>(radDataPager1_PageIndexChanged);
        }

        void radDataPager1_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            CurrentPage = e.NewPageIndex;
            GetTemplates();
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            ClearSearchButton.Visibility = System.Windows.Visibility.Collapsed;
            LoadStatus();
            if (DictionaryManager.AppObjects["role"].ToString().ToLower() == "admin")
            {
                btnAdmin.Visibility = Visibility.Visible;
                cboStatus.SelectedIndex = 2;
            }
            else
            {
                //btnAdmin.Visibility = Visibility.Collapsed;
                cboStatus.SelectedIndex = 1;
            }

            LoadCategories();
            lblProductName.Text = "Templates";


            PanelCategories.Visibility = System.Windows.Visibility.Visible;
            PanelMatchingSet.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void LayoutMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                listTemplates.Width = LayoutMain.ActualWidth;
                listTemplates.Height = LayoutMain.ActualHeight;
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

        private void LayoutMain_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        void LoadCategories()
        {
            try
            {
                ProductServiceReference.ProductServiceClient objSrv = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                objSrv.GetCategoriesCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetCategoriesCompletedEventArgs>(objSrv_GetCategoriesCompleted);
                objSrv.GetCategoriesAsync(App.DesignerMode);
            }
            catch (Exception ex)
            {
                MessageBox.Show("::LoadCategories::" + ex.ToString());
            }
        }

        void objSrv_GetCategoriesCompleted(object sender, GetCategoriesCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    List<ProductServiceReference.vw_ProductCategoriesLeafNodes> lstProductCategory = new List<ProductServiceReference.vw_ProductCategoriesLeafNodes>(e.Result);
                    vw_ProductCategoriesLeafNodes node = new vw_ProductCategoriesLeafNodes();
                    node.ParentCategoryID= 0;
                    node.ProductCategoryID= 0;
                    node.CategoryName = "All";
                    //lstProductCategory.Insert(0,node);
                    cboCategories.ItemsSource = lstProductCategory;
                    cboCategories.DisplayMemberPath = "CategoryName";
                    cboCategories.SelectedValuePath = "ProductCategoryID";
                    

                    //repplaying the search criteria if available

                    if (DictionaryManager.SearchMode == 1)// matching set
                    {
                        rbtnViewMatchingSets.IsChecked = true;
                    }
                    else if ( DictionaryManager.SearchMode == 2) //template view
                    {
                        rbtnViewTemplates.IsChecked = true;
                    }


                    if (DictionaryManager.SearchStatus != 0)
                    {
                        cboStatus.SelectedIndex = DictionaryManager.SearchStatus;
                    }
                    
                    if (DictionaryManager.SearchCategory != 0)
                    {
                        cboCategories.SelectedValue = DictionaryManager.SearchCategory;
                    }
                    else
                    {
                        cboCategories.SelectedIndex = 0;
                    }

                    if (DictionaryManager.SearchKeyword != null)
                    {
                        txtSearch.Text = DictionaryManager.SearchKeyword;
                    }

                    if (DictionaryManager.CurrentPage != -1)
                    {
                        CurrentPage = DictionaryManager.CurrentPage;
                        radDataPager1.PageIndex = DictionaryManager.CurrentPage;
                    }
                    else
                    {
                        CurrentPage = 0;
                        radDataPager1.PageIndex = 0;
                    }


                    getMatchingSets();
                   
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("::objSrv_GetCategoriesCompleted::" + ex.ToString());
            }
        }
        

        public void getMatchingSets()
        {
            ProductServiceReference.ProductServiceClient objSrv = new webprintDesigner.ProductServiceReference.ProductServiceClient();
            objSrv.GetMatchingSetsCompleted += new EventHandler<GetMatchingSetsCompletedEventArgs>(objSrv_GetMatchingSetsCompleted);
            objSrv.GetMatchingSetsAsync();
        }

        void objSrv_GetMatchingSetsCompleted(object sender, GetMatchingSetsCompletedEventArgs e)
        {

            MatchingSets onode = new MatchingSets();
            onode.MatchingSetID = 0;
            onode.MatchingSetName = "None";

            List<MatchingSets> oList = e.Result.ToList();
            oList.Insert(0, onode);


            cboMatchingSets.ItemsSource = oList;

            cboMatchingSets.DisplayMemberPath = "MatchingSetName";
            cboMatchingSets.SelectedValuePath = "MatchingSetID";
            cboMatchingSets.SelectedIndex = 1;


            GetTemplates();
            SearchEventMode = true;
        }


        private void GetTemplates()
        {
            ProgressBar1.IsIndeterminate = true;
            ProgessTxt.Text = "Loading Data";
            ProgressPanel.Visibility = Visibility.Visible;

            ProductServiceReference.ProductServiceClient objSrv = new webprintDesigner.ProductServiceReference.ProductServiceClient();
           

            int status = 0;
            if (cboStatus != null)
            {
                if (cboStatus.SelectedValue != null)
                {
                    switch (cboStatus.SelectedValue.ToString())
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

                if (rbtnViewTemplates.IsChecked.Value)
                {
                    objSrv.GetTemplatesCompleted += new EventHandler<GetTemplatesCompletedEventArgs>(objSrv_GetTemplatesCompleted);
                    objSrv.GetTemplatesAsync(txtSearch.Text, Convert.ToInt32(cboCategories.SelectedValue), CurrentPage, PageSize, false, status, Convert.ToInt32(DictionaryManager.AppObjects["userid"].ToString()), DictionaryManager.AppObjects["role"].ToString());
                }
                else if (rbtnViewMatchingSets.IsChecked.Value)
                {
                    objSrv.GetMatchingSetTemplatesCompleted += new EventHandler<GetMatchingSetTemplatesCompletedEventArgs>(objSrv_GetMatchingSetTemplatesCompleted);
                    objSrv.GetMatchingSetTemplatesAsync(txtSearch.Text, Convert.ToInt32(cboMatchingSets.SelectedValue), CurrentPage, PageSize, false, status, Convert.ToInt32(DictionaryManager.AppObjects["userid"].ToString()), DictionaryManager.AppObjects["role"].ToString());
                }
            }
        }

        void objSrv_GetMatchingSetTemplatesCompleted(object sender, GetMatchingSetTemplatesCompletedEventArgs e)
        {
          

            lblPager.Content = string.Format("Page {0} of Total {1} Pages ", CurrentPage + 1, e.PageCount + 1);

            if (CurrentPage == 0)
                btnPagePrevioust.IsEnabled = false;
            else
                btnPagePrevioust.IsEnabled = true;

            if (CurrentPage == e.PageCount)
                btnPageNext.IsEnabled = false;
            else
                btnPageNext.IsEnabled = true;



            List<sp_GetMatchingSetTemplateView_Result> lTemplates = new List<sp_GetMatchingSetTemplateView_Result>(e.Result);

           

            foreach (var item in lTemplates)
            {
                if (item.Thumbnail != null)
                {
                    item.Thumbnail = "/designer/products/" + item.Thumbnail;
                }
                else
                {
                    item.Thumbnail = "/images/nothumb.png";
                }
            }

            listTemplates.ItemsSource = lTemplates;

          
            ProgressBar1.IsIndeterminate = false;
            ProgressPanel.Visibility = Visibility.Collapsed;
        }

        void objSrv_GetTemplatesCompleted(object sender, GetTemplatesCompletedEventArgs e)
        {
            try
            {

                lblPager.Content = string.Format("Page {0} of Total {1} Pages ", CurrentPage+1, e.PageCount+1);

                if (CurrentPage == 0)
                    btnPagePrevioust.IsEnabled = false;
                else
                    btnPagePrevioust.IsEnabled = true;

                if (CurrentPage == e.PageCount)
                    btnPageNext.IsEnabled = false;
                else
                    btnPageNext.IsEnabled = true;



                List<Templates> lTemplates = new List<Templates>(e.Result);

                foreach (var item in lTemplates)
                {
                    if (item.Thumbnail != null)
                    {
                        item.Thumbnail = "/designer/products/" + item.Thumbnail;
                    }
                    else
                    {
                        item.Thumbnail = "/images/nothumb.png";
                    }
                }

                listTemplates.ItemsSource = lTemplates;


                radDataPager1.ItemCount = (e.PageCount + 1) * PageSize;
                radDataPager1.PageIndex = CurrentPage;

                ProgressBar1.IsIndeterminate = false;
                ProgressPanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("::objSrv_GetCategoriesCompleted::" + ex.ToString());
            }
        }

        private void LoadStatus()
        {
            List<string> lstItems = new List<string>();
            //lstItems.Add("All");
            lstItems.Add("Approved");
            if (DictionaryManager.AppObjects["role"].ToString().ToLower() != "admin")
            {
                lstItems.Add("Draft");
            }
            lstItems.Add("Rejected");
            lstItems.Add("Submitted");
            cboStatus.ItemsSource = lstItems;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            GetTemplates();
        }

        private void hyperlinkButton1_Click(object sender, RoutedEventArgs e)
        {
            DictionaryManager.LogOut();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DictionaryManager.CreateNewTemplate();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            DictionaryManager.Home();
        }

        private void btnGotoProfile_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult oResult =  MessageBox.Show("Are you sure you want to logout ?", "Logout", MessageBoxButton.OKCancel);

            if (oResult == MessageBoxResult.OK)
            {
                DictionaryManager.LogOut();
            }
        }
               
        private void cboCategories_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (SearchEventMode == true)
            {
                CurrentPage = 0;
                radDataPager1.PageIndex = 0;
                GetTemplates();
            }
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = 0;
            txtSearch.Text = "";
            radDataPager1.PageIndex = 0;
            GetTemplates();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DictionaryManager.CreateNewTemplate();
        }

        private void cboStatus_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SearchEventMode == true)
            {
                CurrentPage = 0;
                radDataPager1.PageIndex = 0;
                GetTemplates();
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchEventMode == true)
            {
                //CurrentPage = 0;
                //radDataPager1.PageIndex = 0;
                if (txtSearch.Text == string.Empty)
                {
                    ClearSearchButton.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    ClearSearchButton.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DictionaryManager.AppObjects["MatchingSetID"] = null;
            //UIElement element = sender as UIElement;
            //DateTime clickTime = DateTime.Now;

            //TimeSpan span = clickTime - _lastClick;

            //if (span.TotalMilliseconds > 300 || _firstClickDone == false)
            //{
            //    _clickPosition = e.GetPosition(element);
            //    _firstClickDone = true;
            //    _lastClick = DateTime.Now;
            //}
            //else
            //{
            //    Point position = e.GetPosition(element);
            //    if (Math.Abs(_clickPosition.X - position.X) < 4 && Math.Abs(_clickPosition.Y - position.Y) < 4) //mouse didn't move => Valid double click
            //    {

            DictionaryManager.AppObjects["productid"] = ((Image)sender).Tag.ToString();
            
            DictionaryManager.SearchKeyword = txtSearch.Text;
            DictionaryManager.SearchCategory = Convert.ToInt32( cboCategories.SelectedValue);
            DictionaryManager.SearchStatus = cboStatus.SelectedIndex;
            DictionaryManager.CurrentPage = CurrentPage;

            if (rbtnViewMatchingSets.IsChecked == true)// matching set
            {

                if (cboMatchingSets.SelectedIndex == 0)
                {
                    DictionaryManager.AppObjects["mode"] = "edit";
                    DictionaryManager.SearchMode = 1;
                }
                else
                {
                    DictionaryManager.AppObjects["MatchingSetID"] = cboMatchingSets.SelectedValue;
                    DictionaryManager.AppObjects["mode"] = "matchingset";
                    DictionaryManager.SearchMode = 1;
                }
            }
            else if (rbtnViewTemplates.IsChecked == true) //template view
            {
                DictionaryManager.AppObjects["mode"] = "edit";
                DictionaryManager.SearchMode = 2;
            }


            ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);

            //}
            //else
            //{
            //    // double click got but mouse moved.
            //}
            //_firstClickDone = false;
        }

        private void imgCopyTemplate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rbtnViewTemplates.IsChecked.Value == true)
            {

                ProductServiceReference.ProductServiceClient objSrv = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                objSrv.CopyTemplateCompleted += new EventHandler<CopyTemplateCompletedEventArgs>(objSrv_CopyTemplateCompleted);
                objSrv.CopyTemplateAsync(Convert.ToInt32(((Image)sender).Tag.ToString()), Convert.ToInt32(DictionaryManager.AppObjects["userid"]), DictionaryManager.AppObjects["fullname"].ToString());
            }
            else if (rbtnViewMatchingSets.IsChecked.Value == true)
            {

                if (cboMatchingSets.SelectedIndex != 0)
                {
                    

                    ProductServiceReference.ProductServiceClient objSrv = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                    objSrv.CopyMatchingSetCompleted += new EventHandler<CopyMatchingSetCompletedEventArgs>(objSrv_CopyMatchingSetCompleted);
                    objSrv.CopyMatchingSetAsync(Convert.ToInt32(((Image)sender).Tag.ToString()), Convert.ToInt32(DictionaryManager.AppObjects["userid"]), DictionaryManager.AppObjects["fullname"].ToString());
                   
                }

            }


        }

        void objSrv_CopyMatchingSetCompleted(object sender, CopyMatchingSetCompletedEventArgs e)
        {
            if (e.Result != 0)
            {
                DictionaryManager.AppObjects["MatchingSetID"] = cboMatchingSets.SelectedValue;
                DictionaryManager.AppObjects["mode"] = "matchingset";
                DictionaryManager.SearchMode = 1;

                DictionaryManager.SearchKeyword = txtSearch.Text;
                DictionaryManager.SearchCategory = Convert.ToInt32(cboCategories.SelectedValue);
                DictionaryManager.SearchStatus = cboStatus.SelectedIndex;
                DictionaryManager.CurrentPage = CurrentPage;

                DictionaryManager.AppObjects["productid"] = e.Result.ToString();
                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);
            }
        }

        void objSrv_CopyTemplateCompleted(object sender, CopyTemplateCompletedEventArgs e)
        {
            if (e.Result != 0)
            {
                DictionaryManager.AppObjects["productid"] = e.Result.ToString();
                DictionaryManager.AppObjects["mode"] = "edit";
                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);
            }
        }           
        

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetTemplates();
            }
        }

        private void tbProductName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DictionaryManager.AppObjects["productid"] = ((TextBlock)sender).Tag.ToString();
            DictionaryManager.AppObjects["mode"] = "edit";
            ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);
        }

        private void btnPageNext_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage++;
            GetTemplates();
        }

        private void btnPagePrevioust_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage--;
            GetTemplates();
        }

        private void rbtnViewMatchingSets_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void rbtnViewMatchingSets_Checked(object sender, RoutedEventArgs e)
        {
            int TagVal = Convert.ToInt32(((RadioButton)sender).Tag);

            if (TagVal == 1) //matching set mode
            {
                PanelCategories.Visibility = System.Windows.Visibility.Collapsed;
                PanelMatchingSet.Visibility = System.Windows.Visibility.Visible;

                if (SearchEventMode == true)
                {
                    CurrentPage = 0;
                    radDataPager1.PageIndex = 0;
                    GetTemplates();
                }
            }
            else if (TagVal == 2)  //template search mode
            {
                PanelCategories.Visibility = System.Windows.Visibility.Visible;
                PanelMatchingSet.Visibility = System.Windows.Visibility.Collapsed;

                if (SearchEventMode == true)
                {
                    CurrentPage = 0;
                    radDataPager1.PageIndex = 0;
                    GetTemplates();
                }
            }
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminActions oAc = new AdminActions();
            oAc.Show();

        }

        
    }
}

