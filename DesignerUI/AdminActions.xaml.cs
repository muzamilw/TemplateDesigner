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
using webprintDesigner.ProductServiceReference;
using Telerik.Windows.Controls;
using System.Linq;
using System.Linq.Expressions;

namespace webprintDesigner
{
    public partial class AdminActions : ChildWindow
    {
        public AdminActions()
        {
            InitializeComponent();

            LoadCategories();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
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
                  
                    
                    cboSourceCategories.ItemsSource = lstProductCategory;
                    cboSourceCategories.DisplayMemberPath = "CategoryName";
                    cboSourceCategories.SelectedValuePath = "ProductCategoryID";


                    cboDestinationCategories.ItemsSource = lstProductCategory;
                    cboDestinationCategories.DisplayMemberPath = "CategoryName";
                    cboDestinationCategories.SelectedValuePath = "ProductCategoryID";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("::objSrv_GetCategoriesCompleted::" + ex.ToString());
            }
        }
        
    }
}

