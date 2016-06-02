using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Resources;
using webprintDesigner.EnhancedControls;
using System.Windows.Controls.Primitives;
using System.IO;
using webprintDesigner.Helpers;
using System.Windows.Media.Imaging;
using webprintDesigner.ProductServiceReference;
using System.IO.IsolatedStorage;
using System.Windows.Interop;

namespace webprintDesigner
{
    public partial class Page : UserControl
    {
        private int CustomerID = 0;
        private int ContactID = 0;
        private bool ShowException = true;
        private int SelObjCnt = 0;
        private int ProductId = 0;
        private bool SelObjPro = false;
        private bool ClrDwn = false;
        private bool FntDwn = false;
        private bool IsUserLogin = false;
        private SaveOperationTypes SaveOperationType = 0;
        private bool IsResUsrLogin = false;
        private bool IsFontChange = false;
        private bool IsLineHChange = false;
        private bool IsSnapToGrid = true;
        int CtlIdx = 0;
        private bool IsShowGrid = false;
        ProgressSource progressSource = 0;
        private ZoomState zoomState;
        private FontSources FontSource;
        private int FontsCount = 0;
        private int FontsLoaded = 0;

        private int PDFSideUploading = 0;
        bool DragablLogo = false;
        private bool ApplyFoldLines = false;
        private List<webprintDesigner.ProductServiceReference.tbl_ProductCategoryFoldLines> FoldLines = new List<ProductServiceReference.tbl_ProductCategoryFoldLines>();

        private int UserImagesCount = 0;
        private int UserImagesLoadedCount = 0;

        private IDictionary<string, string> InitParam = new Dictionary<string, string>();
        webprintDesigner.ProductServiceReference.Templates objProduct;
        PageContainer SelectedPage;
        PageTxtControl SelectedTxtCtls;
        System.ServiceModel.BasicHttpBinding BindProSrv;
        System.ServiceModel.EndpointAddress EPProSrv;
        System.ServiceModel.BasicHttpBinding BindUsrSrv;
        System.ServiceModel.EndpointAddress EPUsrSrv;
        private string UriPrefix = "";
        private string tempDeleteImagePath = string.Empty;

        private int imgCropCounter = 0;

        public Page()
        {
            InitializeComponent();
            this.Loaded += OnLoad;
        }
        public Page(IDictionary<string, string> IntPrm)
        {
            InitializeComponent();
            this.Loaded += OnLoad;
            InitParam = IntPrm;
        }
        void OnLoad(object sender, RoutedEventArgs e)
        {
            if (HtmlPage.Document.QueryString.Keys.Contains("ShowException"))
            {
                if (HtmlPage.Document.QueryString["templateid"].ToString() != "")
                {
                    ShowException = false;
                }

                if (HtmlPage.Document.QueryString["ShowException"] != "")
                {
                    ShowException = true;
                }
            }

            try
            {


                App.Current.Host.Content.Resized += new EventHandler(Content_Resized);        


                DesignerDisabl2.Visibility = Visibility.Visible;
                ProgressBar1.IsIndeterminate = true;
                ProgessTxt.Text = "Loading Data";
                ProgressPanel.Visibility = Visibility.Visible;
                CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
               
                    UriPrefix = "";
                
               
                DesignerControlReset();
                //MessageBox.Show(" w:" + ProgressBar1.GetValue(Canvas.TopProperty).ToString() + " h:" + ProgressBar1.GetValue(Canvas.LeftProperty).ToString());
                //InitializeWCFBinding();
                //SelObjCnt = 0;
                //EnableProButton(null);
                btnToggleSides.IsChecked = true;
                
                SelEnbProButton(null);

                //ProductId = 0;
                //App.DesignMode = 2;
                //ProductId = 1077;

                if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.SimpleEndUser)  //simple or advanced end user mode look for query string
                {
                    if (HtmlPage.Document.QueryString.Keys.Contains("templateid"))
                    {
                        if (HtmlPage.Document.QueryString["templateid"].ToString() != "")
                        {
                            ProductId = Convert.ToInt32(HtmlPage.Document.QueryString["templateid"].ToString());
                        }
                    }

                   

                }
                else if (App.DesignerMode == DesignerModes.CreatorMode || App.DesignerMode == DesignerModes.CorporateMode)
                {
                    if (DictionaryManager.AppObjects["productid"] != null)
                    {
                        if (DictionaryManager.AppObjects["productid"].ToString() != "")
                        {
                            ProductId = Convert.ToInt32(DictionaryManager.AppObjects["productid"].ToString());
                        }
                    }
                }
            

                if (ProductId == 0)
                {
                    if (HtmlPage.Document.QueryString.Keys.Contains("templateid"))
                    {
                        if (HtmlPage.Document.QueryString["templateid"].ToString() != "")
                        {
                            ProductId = Convert.ToInt32(HtmlPage.Document.QueryString["templateid"].ToString());
                        }
                    }
                }


                ctlImagesUpload.UploadUrl = new Uri(HtmlPage.Document.DocumentUri, UriPrefix + "FileUpload/FileUploadHandler.ashx");
                ctlImagesUpload.ProductID = ProductId;
                ctlImagesUpload.FileType = 2;
                ctlImagesUpload.UploadComplete_Event += new FileUploadControl.UploadComplete_EventHandler(ctlImagesUpload_UploadComplete_Event);
                ctlImagesUpload.SelectButtonContent = "Browse Images to Upload";
                ctlImagesUpload.MaximumUpload = 8194304;

                if (ProductId != 0)
                    WcfClient();


                //ServiceReference1.ServiceClient objSrv = new webprintDesigner.ServiceReference1.ServiceClient();
                //objSrv.GetFontListCompleted += new EventHandler<webprintDesigner.ServiceReference1.GetFontListCompletedEventArgs>(objSrvCompleted);
                //objSrv.GetFontListAsync();

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::OnLoad::" + ex.ToString());
            }

        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //if (SelectedPage != null)
            //    lblMousePointer.Content = SelectedPage.pt.ToString();
        }

        void Content_Resized(object sender, EventArgs e)
        {

            zoomPanel.SetValue(Canvas.LeftProperty, tbDesignPages.ActualWidth - 150);



         
        }

        void ctlImagesUpload_UploadComplete_Event(object sender, EventArgs e)
        {
            try
            {
                ctlImagesUpload.ClearAndCancelControl();
                lstImagesList.ItemsSource = null;
                ProductServiceReference.ProductServiceClient objSrvBk = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                objSrvBk.GetProductBackgroundImagesCompleted += new EventHandler<ProductServiceReference.GetProductBackgroundImagesCompletedEventArgs>(objSrvBk_GetProductBackgroundImagesCompleted);
                objSrvBk.GetProductBackgroundImagesAsync(ProductId, 1);

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ctlImagesUpload_UploadComplete_Event::" + ex.ToString());
            }
        }

       

        void objSrvBk_GetProductBackgroundImagesCompleted(object sender, ProductServiceReference.GetProductBackgroundImagesCompletedEventArgs e)
        {
            try
            {
                
                lstImagesList.ItemsSource = e.Result.Where(g => g.ImageType == 2).ToList();
                UserImagesCount = lstImagesList.Items.Count;
                UserImagesLoadedCount = 0;
                if (UserImagesCount > 0)
                    brdLstImagesLoader.Visibility = System.Windows.Visibility.Visible;

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ctlImagesUpload_UploadComplete_Event::" + ex.ToString());
            }
        }


        private void lstImagesListDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult oResult =  MessageBox.Show("Are you sure that you want to delete this image? Any instances of this image on canvas will also be deleted and will require saving of design.","Confirm delete action", MessageBoxButton.OKCancel);
            if (oResult == MessageBoxResult.OK)
            {
                ProductServiceReference.ProductServiceClient objSrvBk = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                objSrvBk.DeleteProductBackgroundImageCompleted += new EventHandler<ProductServiceReference.DeleteProductBackgroundImageCompletedEventArgs>(objSrvBk_DeleteProductBackgroundImageCompleted);
                objSrvBk.DeleteProductBackgroundImageAsync(ProductId, Convert.ToInt32(((Button)sender).Tag));


                BitmapImage  osrc =  (BitmapImage)((Image)((StackPanel)((Button)sender).Parent).Children[0]).Source;
                tempDeleteImagePath = osrc.UriSource.ToString();


            }
        }

        void objSrvBk_DeleteProductBackgroundImageCompleted(object sender, ProductServiceReference.DeleteProductBackgroundImageCompletedEventArgs e)
        {
            try
            {
                if (e.Result == true)
                {
                    //refresh the images list
                    lstImagesList.ItemsSource = null;
                    ProductServiceReference.ProductServiceClient objSrvBk = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                    objSrvBk.GetProductBackgroundImagesCompleted += new EventHandler<ProductServiceReference.GetProductBackgroundImagesCompletedEventArgs>(objSrvBk_GetProductBackgroundImagesCompleted);
                    objSrvBk.GetProductBackgroundImagesAsync(ProductId, 1);



                    TabItem tbItem = null;
                    tbItem = (TabItem)tbDesignPages.Items[0];
                    PageContainer objPage = (PageContainer)tbItem.Content;


                    //sync all existing instances of same image
                    foreach (UIElement el in objPage.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer toc = (ObjectContainer)el;
                            if (toc.ContainerType == ObjectContainer.ContainerTypes.Image)
                            {
                                string imgpath = ((BitmapImage)toc.ContainerImage.Source).UriSource.OriginalString;
                                if ((imgpath == "/" + tempDeleteImagePath || imgpath == tempDeleteImagePath || imgpath == HtmlPage.Document.DocumentUri.ToString() + tempDeleteImagePath))
                                {

                                    objPage.RemoveZIdx((int)toc.GetValue(Canvas.ZIndexProperty));
                                    try
                                    {
                                        objPage.DesignArea.Children.Remove(el);
                                    }
                                    catch (Exception)
                                    {
                                        
                                      //
                                    }
                                    break;
                                   
                                }
                            }
                        }
                    }

                    tbItem = null;
                    tbItem = (TabItem)tbDesignPages.Items[1];
                    objPage = (PageContainer)tbItem.Content;


                    //sync all existing instances of same image
                    foreach (UIElement el in objPage.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer toc = (ObjectContainer)el;
                            if (!toc.Selected && toc.ContainerType == ObjectContainer.ContainerTypes.Image)
                            {
                                string imgpath = ((BitmapImage)toc.ContainerImage.Source).UriSource.OriginalString;
                                if ((imgpath == "/" + tempDeleteImagePath || imgpath == tempDeleteImagePath || imgpath == HtmlPage.Document.DocumentUri.ToString() + tempDeleteImagePath))
                                {

                                    objPage.RemoveZIdx((int)toc.GetValue(Canvas.ZIndexProperty));
                                    try
                                    {
                                        objPage.DesignArea.Children.Remove(el);
                                    }
                                    catch (Exception)
                                    {

                                        //
                                    }

                                    break;
                                }
                            }
                        }
                    }




                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ctlImagesUpload_UploadComplete_Event::" + ex.ToString());
            }
        }

        //set the designer UI for embedded more
        private void setDesignerEmbedded()
        {
            
            LayoutRoot.Margin = new Thickness(0);
            BorderTop.Visibility = System.Windows.Visibility.Collapsed;
            BorderLeftRight.Padding = new Thickness(0);
            BorderBottom.Background = new SolidColorBrush(Color.FromArgb(255,173,171,171)); //#FFADABAB
            btnClosePanel.Visibility = System.Windows.Visibility.Collapsed;
            lblTemplateSize.Visibility = System.Windows.Visibility.Collapsed;

        }

        void DesignerControlReset()
        {
            

            if (App.DesignerMode  == DesignerModes.SimpleEndUser)
            {

                setDesignerEmbedded();

                //AdvancedToolsPanel.Visibility = System.Windows.Visibility.Collapsed;
                ddpAddTxtObject.Visibility = Visibility.Collapsed;
               
                //ExpanderProp.IsExpanded = false;
                
                //ExpanderProp.IsEnabled = false;
                
                btnGenratePDF.Content = "Save & Continue";
             
                foreach (UIElement el in grTxtCtrls.Children)
                {
                    if (el.GetType().Name == "PageTxtControl")
                    {
                        (el as PageTxtControl).UpdateCtrlsList();
                    }
                }
            }
            else if (App.DesignerMode == DesignerModes.AdvancedEndUser)
            {

                setDesignerEmbedded();
                //AdvancedToolsPanel.Visibility = System.Windows.Visibility.Collapsed;
                ddpAddTxtObject.Visibility = Visibility.Visible;
             
                
                rdoLable.Visibility = Visibility.Collapsed;
               

                btnGenratePDF.Content = "Save & Continue";
                foreach (UIElement el in grTxtCtrls.Children)
                {
                    if (el.GetType().Name == "PageTxtControl")
                    {
                        (el as PageTxtControl).UpdateCtrlsList();
                    }
                }
            }
            else if (App.DesignerMode == DesignerModes.CreatorMode || App.DesignerMode == DesignerModes.CorporateMode)
            {
                pnlBtnPreview.Visibility = System.Windows.Visibility.Collapsed;
                //AdvancedToolsPanel.Visibility = System.Windows.Visibility.Collapsed;
                ddpAddTxtObject.Visibility = Visibility.Visible;
              
                
                //rdoLable.Visibility = Visibility.Visible;
                

                btnGenratePDF.Content = "Save & Preview PDF";
                foreach (UIElement el in grTxtCtrls.Children)
                {
                    if (el.GetType().Name == "PageTxtControl")
                    {
                        (el as PageTxtControl).UpdateCtrlsList();
                    }
                }
            }
        }
        #region wcf download data
        void InitializeWCFBinding()
        {
            try
            {

                
                BindProSrv = new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.None);
                BindProSrv.CloseTimeout = new TimeSpan(00, 10, 00);
                BindProSrv.OpenTimeout = new TimeSpan(00, 10, 00);
                BindProSrv.ReceiveTimeout = new TimeSpan(00, 10, 00);
                BindProSrv.SendTimeout = new TimeSpan(00, 10, 00);
                BindProSrv.MaxBufferSize = 2147483647;
                BindProSrv.MaxReceivedMessageSize = 2147483647;
                //BindProSrv.Security = httpScrty;
                //EPProSrv = new System.ServiceModel.EndpointAddress(new Uri(HtmlPage.Document.DocumentUri, "PrintFlowInterface/ProductService.svc"));
                EPProSrv = new System.ServiceModel.EndpointAddress(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + "services/ProductService.svc"));

                BindUsrSrv = new System.ServiceModel.BasicHttpBinding();
                BindUsrSrv.CloseTimeout = new TimeSpan(00, 10, 00);
                BindUsrSrv.OpenTimeout = new TimeSpan(00, 10, 00);
                BindUsrSrv.ReceiveTimeout = new TimeSpan(00, 10, 00);
                BindUsrSrv.SendTimeout = new TimeSpan(00, 10, 00);
                BindUsrSrv.MaxBufferSize = 2147483647;
                BindUsrSrv.MaxReceivedMessageSize = 2147483647;
                EPUsrSrv = new System.ServiceModel.EndpointAddress(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + "Services/UserService.svc"));
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::InitializeWCFBinding::" + ex.ToString());
            }
        }
        void WcfClient()
        {
            try
            {
                if (ProductId != 0)
                {




                    //UserServiceReference.UserServiceClient objSrv = new webprintDesigner.UserServiceReference.UserServiceClient();
                    //objSrv.UserLoginCompleted += new EventHandler<webprintDesigner.UserServiceReference.UserLoginCompletedEventArgs>(objSrv_UserLoginCompleted);
                    //objSrv.UserLoginAsync("", "", App.DesignerMode);

                    //objSrv.IsUserLoginedCompleted += new EventHandler<webprintDesigner.UserServiceReference.IsUserLoginedCompletedEventArgs>(objSrv_IsUserLoginedCompleted);
                    //objSrv.IsUserLoginedAsync(App.DesignMode);

                    ProductServiceReference.ProductServiceClient objProSrv = new webprintDesigner.ProductServiceReference.ProductServiceClient();//(BindProSrv, EPProSrv);
                    objProSrv.GetFontListCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetFontListCompletedEventArgs>(objProSrv_GetFontListCompleted);


                    FontLoadModes ofontLoadingMode;

                    if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.SimpleEndUser || App.DesignerMode == DesignerModes.AnnanomousMode)
                        ofontLoadingMode = FontLoadModes.SystemAndUsed;
                    else
                        ofontLoadingMode = FontLoadModes.All;
                            



                    if (IsolatedStorageFile.IsEnabled)
                    {
                        FontSource = FontSources.IsoLatedStorage;
                        objProSrv.GetFontListAsync(ProductId, false, ofontLoadingMode);
                    }
                    else
                    {
                        FontSource = FontSources.MemoryStream;
                        objProSrv.GetFontListAsync(ProductId, true, ofontLoadingMode);
                    }

                   
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::WcfClient::" + ex.ToString());
            }
        }

        


        void objSrv_IsUserLoginedCompleted(object sender, webprintDesigner.UserServiceReference.IsUserLoginedCompletedEventArgs e)
        {
            try
            {
                if ((bool)e.Result == true)
                {
                    lstImagesList.Visibility = Visibility.Visible;
                   
                    //GetUserImages();
                    IsUserLogin = true;
                    IsResUsrLogin = true;

                }
                else
                {
                    lstImagesList.Visibility = Visibility.Collapsed;
                    
                    IsUserLogin = false;
                    IsResUsrLogin = true;
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objSrv_IsUserLoginedCompleted::" + ex.ToString());
            }
        }
       


        


        void objProSrv_GetFontListCompleted(object sender, webprintDesigner.ProductServiceReference.GetFontListCompletedEventArgs e)
        {
            try
            {

                if (FontSource == FontSources.IsoLatedStorage) //fonts cached i isolated storage 
                {

                    cmbFontList.ItemsSource = e.Result.ToList();

                    if (e.Result.ToList().Count > 0)
                    {
                        cmbFontList.SelectedIndex = 0;
                        if (SelObjCnt > 0)
                            cmbFontList.IsEnabled = true;
                        FntDwn = true;
                    }

                    FontsCount = e.Result.Where(g => g.IsPrivateFont == true).Count();
                    FontsLoaded = 0;

                    //proceed to next step in ansync calls using the loaded count logic

                    int FontsToBeLoaded = 0;

                   
                        foreach (var item in e.Result)
                        {
                            if (item.IsPrivateFont)
                            {

                                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                                {
                                    if (!isf.FileExists(item.FontFile))
                                    {

                                        HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + "designer/privatefonts/" + item.FontFile));
                                        webrequest.BeginGetResponse(new AsyncCallback(request_GetResponse), webrequest);

                                        FontsToBeLoaded++;
                                        Thread.SpinWait(50);
                                    }
                                    //break;
                                }
                            }
                        }
                    

                    if (FontsToBeLoaded == 0) //all fonts already exit in isolated storage hence explicity go to next step 
                    {
                        foreach (var item in cmbFontList.Items)
                        {
                            if (((TemplateFonts)item).IsPrivateFont)
                            {

                                ((TemplateFonts)item).FontBytes = getFontFromLocalStorage(((TemplateFonts)item).FontFile);
                            }
                        }

                        ProductServiceReference.ProductServiceClient objSrvBk = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                        objSrvBk.GetProductBackgroundImagesCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetProductBackgroundImagesCompletedEventArgs>(objSrv_GetProductBackgroundImagesCompleted);
                        objSrvBk.GetProductBackgroundImagesAsync(ProductId, 1);
                    }
                    


                }
                else  ///font loaded by WCF call into memory
                {

                    cmbFontList.ItemsSource = e.Result.ToList();
                    if (e.Result.ToList().Count > 0)
                    {
                        cmbFontList.SelectedIndex = 0;
                        if (SelObjCnt > 0)
                            cmbFontList.IsEnabled = true;
                        FntDwn = true;
                    }

                    ProductServiceReference.ProductServiceClient objSrvBk = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                    objSrvBk.GetProductBackgroundImagesCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetProductBackgroundImagesCompletedEventArgs>(objSrv_GetProductBackgroundImagesCompleted);
                    objSrvBk.GetProductBackgroundImagesAsync(ProductId, 1);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objProSrv_GetFontListCompleted::" + ex.ToString());
            }
        }

        void ProceedNextStepAfterFontLoading()
        {
            if (FontsCount == FontsLoaded)
            {

                foreach (var item in cmbFontList.Items)
                {
                    if (((TemplateFonts)item).IsPrivateFont)
                    {

                        ((TemplateFonts)item).FontBytes = getFontFromLocalStorage(((TemplateFonts)item).FontFile);
                    }
                }

                ProductServiceReference.ProductServiceClient objSrvBk = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                objSrvBk.GetProductBackgroundImagesCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetProductBackgroundImagesCompletedEventArgs>(objSrv_GetProductBackgroundImagesCompleted);
                objSrvBk.GetProductBackgroundImagesAsync(ProductId, 1);
            }
        }

        void request_GetResponse(IAsyncResult asynchronousResult)
        {
            
            
            HttpWebRequest webrequest = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)webrequest.EndGetResponse(asynchronousResult);

            Stream oStream =  response.GetResponseStream();
            byte[] oFont = new byte[oStream.Length];

            oStream.Read(oFont, 0, Convert.ToInt32(oStream.Length));

            SaveFontToLocalStorage(oFont, System.IO.Path.GetFileName(webrequest.RequestUri.OriginalString));

            //foreach (var item in cmbFontList.Items)
            //{
            //    if (((TemplateFonts)item).FontFile == System.IO.Path.GetFileName(webrequest.RequestUri.OriginalString))
            //    {
            //        ((TemplateFonts)item).FontBytes = getFontFromLocalStorage(((TemplateFonts)item).FontFile);
            //        break;
            //    }
            //}

            FontsLoaded++;

            if (FontsCount == FontsLoaded)
            {
                //do the next work in main thread and continue to next steps of loading.
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.BeginInvoke(() => this.ProceedNextStepAfterFontLoading());
                }
          
            }
           
        }


        object flag = new object(); 

        private void SaveFontToLocalStorage(byte[] data, string fileName)
        {
            lock (flag)
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {

                    if (!isf.DirectoryExists("PrivateFonts"))
                        isf.CreateDirectory("PrivateFonts");

                    if (isf.Quota < 10485760)
                        isf.IncreaseQuotaTo(10485760);

                    if (!isf.FileExists(fileName))
                    {
                        using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(fileName, FileMode.Create, isf))
                        {
                            using (StreamWriter sw = new StreamWriter(isfs)) { sw.Write(data); sw.Close(); }
                        }
                    }
                }
            }
        }

        private byte[] getFontFromLocalStorage(string fileName)
        {
            try
            {

            
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {

                using (IsolatedStorageFileStream isfs = isf.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                {
                    
                    byte[] oFont = new byte[isfs.Length];
                    isfs.Read(oFont, 0, Convert.ToInt32( isfs.Length));
                    return oFont;
                }
            }
            }
            catch (Exception)
            {

                return null;
            }
        }

        void objProSrv_GetProductByIdCompleted(object sender, webprintDesigner.ProductServiceReference.GetProductByIdCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    objProduct = (webprintDesigner.ProductServiceReference.Templates)e.Result;
                    if (objProduct != null)
                    {
                        ProductId = objProduct.ProductID;
                        lblProductName.Text = "Edit Artwork : " + objProduct.ProductName + " " + objProduct.ProductID.ToString();
                        lblTemplateSize.Content = "Product Size (Excluding Bleed ) : " + (Common.PointToMM(objProduct.PDFTemplateWidth.Value)-10).ToString("n0") + "mm X " + (Common.PointToMM(objProduct.PDFTemplateHeight.Value) - 10 ).ToString("n0")+"mm";
                        

                        btnGenratePDF.IsEnabled = true;
                        
                        

                        ProductServiceReference.ProductServiceClient objSrvObjects = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                        objSrvObjects.GetFoldLinesByProductCategoryIDCompleted +=new EventHandler<ProductServiceReference.GetFoldLinesByProductCategoryIDCompletedEventArgs>(objSrvObjects_GetFoldLinesByProductCategoryIDCompleted);
                        objSrvObjects.GetFoldLinesByProductCategoryIDAsync(objProduct.ProductCategoryID.Value);


                        //next call from fold lines function moved the objects loading function there
                        

                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objProSrv_GetProductByIdCompleted::" + ex.ToString());
            }
        }

        void objSrvObjects_GetFoldLinesByProductCategoryIDCompleted(object sender, ProductServiceReference.GetFoldLinesByProductCategoryIDCompletedEventArgs e)
        {
            ApplyFoldLines = e.ApplyFoldLines;
            if (e.ApplyFoldLines)
            {
                FoldLines = e.Result.ToList();

                foreach (var item in FoldLines)
                {
                    item.FoldLineOffsetFromOrigin =  webprintDesigner.Common.PointToPixel( webprintDesigner.Common.MMToPoint( item.FoldLineOffsetFromOrigin.Value));
                }
            }

            ProductServiceReference.ProductServiceClient objSrvObjects = new webprintDesigner.ProductServiceReference.ProductServiceClient();
            objSrvObjects.GetProductObjectsCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetProductObjectsCompletedEventArgs>(objSrvObjects_GetProductObjectsProductCompleted);
            objSrvObjects.GetProductObjectsAsync(objProduct.ProductID, App.DesignerMode);
            
        }

        

        void objSrvObjects_GetProductObjectsProductCompleted(object sender, webprintDesigner.ProductServiceReference.GetProductObjectsCompletedEventArgs e)
        {
            try
            {
                List<ProductServiceReference.TemplateObjects> lstProductObects = e.Result.ToList(); //.ToList();
                //MessageBox.Show(lstProductObects.Count.ToString());
                if (objProduct != null)
                {
                    Size PageSize = new Size();

                  
                        PageSize = new Size(webprintDesigner.Common.PointToPixel(objProduct.PDFTemplateWidth.Value), webprintDesigner.Common.PointToPixel(objProduct.PDFTemplateHeight.Value));
                    

                    
                    if (objProduct.IsDoubleSide == true)
                    {
                        TabItem tbFItem = new TabItem();
                        tbFItem.FontSize = 18;
                        tbFItem.Header = "Side 1";
                        PageContainer ProductFPage = new PageContainer(PageSize, "ProductFrontPage", "ProductFrontCtl", false, 1, objProduct.CuttingMargin.Value, ApplyFoldLines, FoldLines, this);
                        ProductFPage.ObjectSelect_Event += new PageContainer.ObjectSelected_EventHandler(ProductFPage_ObjectSelect_Event);


                       
                            ProductFPage.TemplateHeight = "Canvas Height " + Common.PointToMM(objProduct.PDFTemplateHeight.Value).ToString("n0") + "mm";
                            ProductFPage.TemplateWidth = "Canvas Width " + Common.PointToMM(objProduct.PDFTemplateWidth.Value).ToString("n0") + "mm";
                       


                     
                        ProductFPage.ToggleGuides(false);

                        PageTxtControl ProductFCtl = new PageTxtControl("ProductFrontPage", "ProductFrontCtl", ProductFPage);
                        ProductFPage.ShowException = ShowException;
                        ProductFCtl.ShowException = ShowException;
                        ProductFPage.objPageTxtControl = ProductFCtl;
                        SelectedPage = ProductFPage;
                        //SelectedTxtCtls = ProductFCtl;
                        if (objProduct.BackgroundArtwork != "")
                        {
                            System.Windows.Media.Imaging.BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objProduct.BackgroundArtwork));
                            bimg.CreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions.IgnoreImageCache;
                            ImageBrush imgBk = new ImageBrush();
                            imgBk.Stretch = Stretch.Fill;
                            imgBk.ImageSource = bimg;
                            ProductFPage.brdDesign.Background = imgBk;
                            Side1PDFImage.Source = bimg;
                        }
                        else if (objProduct.IsUseBackGroundColor.Value == true)
                        {

                            SolidColorBrush oBgColor = new SolidColorBrush();
                            oBgColor.Color = Color.FromArgb(Convert.ToByte(255), Convert.ToByte(objProduct.BgR.Value), Convert.ToByte(objProduct.BgG.Value), Convert.ToByte(objProduct.BgB.Value));
                            ProductFPage.brdDesign.Background = oBgColor;


                        }


                        //side 2 objects
                        var objParentObject = from dbObject in lstProductObects orderby dbObject.DisplayOrderPdf where dbObject.ParentId == 0 && dbObject.isSide2Object == false && dbObject.PageNo == 1 select dbObject;
                        foreach (ProductServiceReference.TemplateObjects objObects in objParentObject)
                        {
                            if (objObects.PageNo == 1 && !objObects.isSide2Object)
                            {
                                if (objObects.ObjectType == 1 || objObects.ObjectType == 2 || objObects.ObjectType == 4)
                                {
                                    webprintDesigner.ProductServiceReference.TemplateFonts ObjectFont = FindFont(objObects.FontName);
                                    objObects.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                                    objObects.ExField1 = "CtlContainerTxt_" + CtlIdx.ToString();
                                    ObjectContainer oc = ProductFPage.AddObjects("CtlContainerTxt_" + CtlIdx.ToString(), "CtlTxtContent_" + CtlIdx.ToString(), objObects, ObjectFont);
                                    //ProductFCtl.AddObjects("CtlContainerTxt_" + CtlIdx.ToString(), "CtlTxtContent_" + CtlIdx.ToString(), objObects);
                                    CtlIdx++;
                                    if (oc != null)
                                    {
                                        var objChildObjectList = from dbObject in lstProductObects orderby dbObject.DisplayOrderPdf where dbObject.ParentId == objObects.ObjectID && dbObject.isSide2Object == false && dbObject.PageNo == 1 select dbObject;
                                        foreach (ProductServiceReference.TemplateObjects objChildObject in objChildObjectList)
                                        {
                                            if (objChildObject.ObjectType == 1 || objChildObject.ObjectType == 2 || objChildObject.ObjectType == 4)
                                            {
                                                webprintDesigner.ProductServiceReference.TemplateFonts ObjectChldFont = FindFont(objChildObject.FontName);
                                                objChildObject.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                                                oc.AddChildCtrol(objChildObject, ObjectChldFont);
                                                objChildObject.ExField1 = oc.ContainerName;
                                                //ProductFCtl.AddObjects(oc.ContainerName, "CtlTxtContent_" + CtlIdx.ToString(), objChildObject);
                                                CtlIdx++;
                                            }
                                        }
                                    }
                                }
                                else if (objObects.ObjectType == 3)  //image
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY) - webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    BitmapImage bimg=  new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objObects.ContentString));
                                    bimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                    ProductFPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, bimg, objObects.ContentString, ImgPos, ImgSize, objObects, false);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 5) //vector line
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));


                                    ProductFPage.AddVectorLine("CtlContainerTxt_" + CtlIdx.ToString(), "Line_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, ImgPos.X, ImgPos.Y, ImgSize.Height, objObects);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 6) //vector line
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));


                                    ProductFPage.AddVectorRectangle("CtlContainerTxt_" + CtlIdx.ToString(), "Rectangle_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, ImgPos.X, ImgPos.Y, objObects);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 7) //vector line
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));


                                    ProductFPage.AddVectorEllipse("CtlContainerTxt_" + CtlIdx.ToString(), "Ellipse_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, ImgPos.X, ImgPos.Y, objObects);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 8)  //Logo Image
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY) - webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objObects.ContentString));
                                    bimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                    ProductFPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Logo_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, bimg, objObects.ContentString, ImgPos, ImgSize, objObects, true);
                                    CtlIdx++;
                                }
                            }
                        }
                        var lstTxtObjects = from dbObject in lstProductObects orderby dbObject.DisplayOrderTxtControl where dbObject.isSide2Object == false && dbObject.PageNo == 1 && (dbObject.ObjectType == 1 || dbObject.ObjectType == 2 || dbObject.ObjectType == 4) select dbObject;
                        foreach (ProductServiceReference.TemplateObjects objObects in lstTxtObjects)
                        {
                            if (objObects.TCtlName != "")
                                ProductFCtl.AddObjects(objObects.ExField1, objObects.TCtlName, objObects);
                        }
                        ProductFCtl.UpdateCtrlsList();
                        ProductFCtl.Visibility = Visibility.Visible;
                        ProductFPage.flexCanvas.Width = (SelectedPage.DesignArea.ActualWidth + 90);
                        ProductFPage.flexCanvas.Height = (SelectedPage.DesignArea.ActualHeight + 50);
                        tbFItem.Content = ProductFPage;
                        
                        tbDesignPages.Items.Add(tbFItem);
                        grTxtCtrls.Children.Add(ProductFCtl);
                        TabItem tbBItem = new TabItem();
                        tbBItem.Header = "Side 2";
                        tbBItem.FontSize = 18;
                        PageContainer ProductBPage = new PageContainer(PageSize, "ProductBackPage", "ProductBackCtl", true, 1, objProduct.CuttingMargin.Value, ApplyFoldLines, FoldLines,this);
                        ProductBPage.ToggleGuides(false);
                        PageTxtControl ProductBCtl = new PageTxtControl("ProductBackPage", "ProductBackCtl", ProductBPage);
                        ProductBPage.ObjectSelect_Event += new PageContainer.ObjectSelected_EventHandler(ProductFPage_ObjectSelect_Event);



                     
                            ProductBPage.TemplateHeight = "Canvas Height " + Common.PointToMM(objProduct.PDFTemplateHeight.Value).ToString("n0") + "mm";
                            ProductBPage.TemplateWidth = "Canvas Width " + Common.PointToMM(objProduct.PDFTemplateWidth.Value).ToString("n0") + "mm";
                      



                      

                        ProductBPage.ShowException = ShowException;
                        ProductBCtl.ShowException = ShowException;
                        ProductBPage.objPageTxtControl = ProductBCtl;
                        if (objProduct.Side2BackgroundArtwork != "")
                        {
                            System.Windows.Media.Imaging.BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objProduct.Side2BackgroundArtwork));
                            bimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            ImageBrush imgBk = new ImageBrush();
                            imgBk.Stretch = Stretch.Fill;
                            imgBk.ImageSource = bimg;
                            ProductBPage.brdDesign.Background = imgBk;
                            if (bimg != null)
                                Side2PDFImage.Source = bimg;
                        }
                        else if (objProduct.IsUseSide2BackGroundColor.Value == true)
                        {
                            SolidColorBrush oBgColor = new SolidColorBrush();
                            oBgColor.Color = Color.FromArgb(Convert.ToByte(255), Convert.ToByte(objProduct.Side2BgR.Value), Convert.ToByte(objProduct.Side2BgG.Value), Convert.ToByte(objProduct.Side2BgB.Value));
                            ProductBPage.brdDesign.Background = oBgColor;
                            
                                
                        }


                        //side 2 objects
                        var objParentObject2 = from dbObject in lstProductObects orderby dbObject.DisplayOrderPdf where dbObject.ParentId == 0 && dbObject.isSide2Object == true && dbObject.PageNo == 1 select dbObject;
                        foreach (ProductServiceReference.TemplateObjects objObects in objParentObject2)
                        {
                            if (objObects.PageNo == 1 && objObects.isSide2Object)
                            {
                                if (objObects.ObjectType == 1 || objObects.ObjectType == 2 || objObects.ObjectType == 4)
                                {
                                    webprintDesigner.ProductServiceReference.TemplateFonts ObjectFont = FindFont(objObects.FontName);
                                    objObects.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                                    objObects.ExField1 = "CtlContainerTxt_" + CtlIdx.ToString();
                                    ObjectContainer oc = ProductBPage.AddObjects("CtlContainerTxt_" + CtlIdx.ToString(), "CtlTxtContent_" + CtlIdx.ToString(), objObects, ObjectFont);
                                    //ProductBCtl.AddObjects("CtlContainerTxt_" + CtlIdx.ToString(), objObects.TCtlName, objObects);
                                    CtlIdx++;
                                    if (oc != null)
                                    {
                                        var objChildObjectList = from dbObject in lstProductObects orderby dbObject.DisplayOrderPdf where dbObject.ParentId == objObects.ObjectID && dbObject.isSide2Object == true && dbObject.PageNo == 1 select dbObject;
                                        foreach (ProductServiceReference.TemplateObjects objChildObject in objChildObjectList)
                                        {
                                            if (objChildObject.ObjectType == 1 || objChildObject.ObjectType == 2 || objChildObject.ObjectType == 4)
                                            {
                                                webprintDesigner.ProductServiceReference.TemplateFonts ObjectChldFont = FindFont(objChildObject.FontName);
                                                objChildObject.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                                                oc.AddChildCtrol(objChildObject, ObjectChldFont);
                                                objChildObject.ExField1 = oc.ContainerName;
                                                //ProductBCtl.AddObjects(oc.ContainerName, "CtlTxtContent_" + CtlIdx.ToString(), objChildObject);
                                                CtlIdx++;
                                            }
                                        }
                                    }
                                }
                                else if (objObects.ObjectType == 3)  //image
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY) - webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objObects.ContentString));
                                    bimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                    ProductBPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), objObects.DisplayOrderPdf,bimg , objObects.ContentString, ImgPos, ImgSize,objObects,false);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 5) //vector line
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));


                                    ProductBPage.AddVectorLine("CtlContainerTxt_" + CtlIdx.ToString(), "Line_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, ImgPos.X, ImgPos.Y, ImgSize.Height, objObects);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 6) //vector rectangle
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));


                                    ProductBPage.AddVectorRectangle("CtlContainerTxt_" + CtlIdx.ToString(), "Rectangle_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, ImgPos.X, ImgPos.Y, objObects);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 7) //vector ellipse
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));


                                    ProductBPage.AddVectorEllipse("CtlContainerTxt_" + CtlIdx.ToString(), "Ellipse_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, ImgPos.X, ImgPos.Y, objObects);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 8)  //image
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY) - webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objObects.ContentString));
                                    bimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                    ProductBPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Logo_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, bimg, objObects.ContentString, ImgPos, ImgSize, objObects, true);
                                    CtlIdx++;
                                }
                            }
                        }
                        var lstTxtObjects2 = from dbObject in lstProductObects orderby dbObject.DisplayOrderTxtControl where dbObject.isSide2Object == true && dbObject.PageNo == 1 && (dbObject.ObjectType == 1 || dbObject.ObjectType == 2 || dbObject.ObjectType == 4) select dbObject;
                        foreach (ProductServiceReference.TemplateObjects objObects in lstTxtObjects2)
                        {
                            if (objObects.TCtlName != "")
                                ProductBCtl.AddObjects(objObects.ExField1, objObects.TCtlName, objObects);
                        }
                        ProductBCtl.UpdateCtrlsList();
                        ProductBCtl.Visibility = Visibility.Collapsed;
                        ProductBPage.flexCanvas.Width = (SelectedPage.DesignArea.ActualWidth + 90);
                        ProductBPage.flexCanvas.Height = (SelectedPage.DesignArea.ActualHeight + 50);
                        tbBItem.Content = ProductBPage;
                        tbDesignPages.Items.Add(tbBItem);
                        grTxtCtrls.Children.Add(ProductBCtl);
                    }
                    else
                    {
                        TabItem tbFItem = new TabItem();
                        tbFItem.FontSize = 18;
                        tbFItem.Header = null;
                        PageContainer ProductFPage = new PageContainer(PageSize, "ProductFrontPage", "ProductFrontCtl", false, 1,objProduct.CuttingMargin.Value,ApplyFoldLines,FoldLines, this);
                        ProductFPage.ToggleGuides(false);
                        ProductFPage.ObjectSelect_Event += new PageContainer.ObjectSelected_EventHandler(ProductFPage_ObjectSelect_Event);
                        PageTxtControl ProductFCtl = new PageTxtControl("ProductFrontPage", "ProductFrontCtl", ProductFPage);

                      
                            ProductFPage.TemplateHeight = "Canvas Height " + Common.PointToMM(objProduct.PDFTemplateHeight.Value).ToString("n0") + "mm";
                            ProductFPage.TemplateWidth = "Canvas Width " + Common.PointToMM(objProduct.PDFTemplateWidth.Value).ToString("n0") + "mm";
                       
                        
                        ProductFPage.ShowException = ShowException;
                        ProductFCtl.ShowException = ShowException;
                        ProductFPage.objPageTxtControl = ProductFCtl;
                        SelectedPage = ProductFPage;
                        //SelectedTxtCtls = ProductFCtl;

                        if (objProduct.BackgroundArtwork != "")
                        {
                            System.Windows.Media.Imaging.BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objProduct.BackgroundArtwork));
                            bimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            ImageBrush imgBk = new ImageBrush();
                            imgBk.Stretch = Stretch.Fill;
                            imgBk.ImageSource = bimg;
                            ProductFPage.brdDesign.Background = imgBk;
                            Side1PDFImage.Source = bimg;
                        }
                        else if (objProduct.IsUseBackGroundColor.Value == true)
                        {
                            SolidColorBrush oBgColor = new SolidColorBrush();
                            oBgColor.Color = Color.FromArgb(Convert.ToByte(255), Convert.ToByte(objProduct.BgR.Value), Convert.ToByte(objProduct.BgG.Value), Convert.ToByte(objProduct.BgB.Value));
                            ProductFPage.brdDesign.Background = oBgColor;
                                


                        }

                        var objParentObject = from dbObject in lstProductObects orderby dbObject.DisplayOrderPdf where dbObject.ParentId == 0 && dbObject.isSide2Object == false && dbObject.PageNo == 1 select dbObject;
                        foreach (ProductServiceReference.TemplateObjects objObects in objParentObject)
                        {
                            if (objObects.PageNo == 1 && !objObects.isSide2Object)
                            {
                                if (objObects.ObjectType == 1 || objObects.ObjectType == 2 || objObects.ObjectType == 4)
                                {
                                    webprintDesigner.ProductServiceReference.TemplateFonts ObjectFont = FindFont(objObects.FontName);

                                    objObects.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                                    objObects.ExField1 = "CtlContainerTxt_" + CtlIdx.ToString();
                                    ObjectContainer oc = ProductFPage.AddObjects("CtlContainerTxt_" + CtlIdx.ToString(), "CtlTxtContent_" + CtlIdx.ToString(), objObects, ObjectFont);
                                    //ProductFCtl.AddObjects("CtlContainerTxt_" + CtlIdx.ToString(), "CtlTxtContent_" + CtlIdx.ToString(), objObects);
                                    CtlIdx++;
                                    if (oc != null)
                                    {
                                        var objChildObjectList = from dbObject in lstProductObects orderby dbObject.DisplayOrderPdf where dbObject.ParentId == objObects.ObjectID && dbObject.isSide2Object == false && dbObject.PageNo == 1 select dbObject;
                                        foreach (ProductServiceReference.TemplateObjects objChildObject in objChildObjectList)
                                        {
                                            if (objChildObject.ObjectType == 1 || objChildObject.ObjectType == 2 || objChildObject.ObjectType == 4)
                                            {
                                                webprintDesigner.ProductServiceReference.TemplateFonts ObjectChldFont = FindFont(objChildObject.FontName);
                                                objChildObject.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                                                oc.AddChildCtrol(objChildObject, ObjectChldFont);
                                                objChildObject.ExField1 = oc.ContainerName;
                                                //ProductFCtl.AddObjects(oc.ContainerName, "CtlTxtContent_" + CtlIdx.ToString(), objChildObject);
                                                CtlIdx++;
                                            }
                                        }
                                    }
                                }
                                else if (objObects.ObjectType == 3)  //image
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY) - webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objObects.ContentString));
                                    bimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                    ProductFPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), objObects.DisplayOrderPdf,bimg , objObects.ContentString, ImgPos, ImgSize, objObects,false);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 5) //vector line
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));


                                    ProductFPage.AddVectorLine("CtlContainerTxt_" + CtlIdx.ToString(), "Line_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, ImgPos.X, ImgPos.Y, ImgSize.Height, objObects);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 6) //vector Rectangle
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));


                                    ProductFPage.AddVectorRectangle("CtlContainerTxt_" + CtlIdx.ToString(), "Rectangle_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, ImgPos.X, ImgPos.Y, objObects);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 7) //vector Ellipse
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));


                                    ProductFPage.AddVectorEllipse("CtlContainerTxt_" + CtlIdx.ToString(), "Ellipse_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, ImgPos.X, ImgPos.Y, objObects);
                                    CtlIdx++;
                                }
                                else if (objObects.ObjectType == 8)  //image
                                {
                                    Point ImgPos = new Point(webprintDesigner.Common.PointToPixel(objObects.PositionX), webprintDesigner.Common.PointToPixel(objObects.PositionY) - webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    Size ImgSize = new Size(webprintDesigner.Common.PointToPixel(objObects.MaxWidth), webprintDesigner.Common.PointToPixel(objObects.MaxHeight));
                                    BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objObects.ContentString));
                                    bimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                    ProductFPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Logo_" + CtlIdx.ToString(), objObects.DisplayOrderPdf, bimg, objObects.ContentString, ImgPos, ImgSize, objObects,true);
                                    CtlIdx++;
                                }
                            }
                        }
                        var lstTxtObjects = from dbObject in lstProductObects orderby dbObject.DisplayOrderTxtControl where dbObject.isSide2Object == false && dbObject.PageNo == 1 && (dbObject.ObjectType == 1 || dbObject.ObjectType == 2 || dbObject.ObjectType == 4) select dbObject;
                        foreach (ProductServiceReference.TemplateObjects objObects in lstTxtObjects)
                        {
                            if (objObects.TCtlName != "")
                                ProductFCtl.AddObjects(objObects.ExField1, objObects.TCtlName, objObects);
                        }
                        ProductFCtl.UpdateCtrlsList();
                        ProductFCtl.Visibility = Visibility.Visible;
                        ProductFPage.flexCanvas.Width = (SelectedPage.DesignArea.ActualWidth + 90) ;
                        ProductFPage.flexCanvas.Height = (SelectedPage.DesignArea.ActualHeight + 50) ;
                        tbFItem.Content = ProductFPage;
                        tbFItem.BorderThickness = new Thickness(0, 0, 0, 0);
                        tbFItem.BorderBrush = new SolidColorBrush(Colors.White);
                        tbDesignPages.Items.Add(tbFItem);
                        tbDesignPages.BorderThickness = new Thickness(0, 0, 0, 0);
                        grTxtCtrls.Children.Add(ProductFCtl);
                    }

                }

                //product loading complete
                zoomPanel.Visibility = System.Windows.Visibility.Visible;
                zoomPanel.SetValue(Canvas.LeftProperty, tbDesignPages.ActualWidth - 150);
                //zoomPanel.SetValue(Canvas.LeftProperty, tbDesignPages.ActualWidth - 50);







                pnlQuickTextButton.Visibility = System.Windows.Visibility.Visible;


                //opening the quick text bar if it is 


                if (App.DesignerMode == DesignerModes.SimpleEndUser || App.DesignerMode == DesignerModes.AdvancedEndUser)
                {
                    pnlLogo.Visibility = System.Windows.Visibility.Collapsed;

                    pnlQuickTextButton.Visibility = System.Windows.Visibility.Visible;

                    if (HtmlPage.Document.QueryString.Keys.Contains("CustomerID"))
                    {
                        if (HtmlPage.Document.QueryString["CustomerID"].ToString() != "")
                        {
                            CustomerID = Convert.ToInt32(HtmlPage.Document.QueryString["CustomerID"]);
                        }
                    }


                    if (HtmlPage.Document.QueryString.Keys.Contains("ContactID"))
                    {
                        if (HtmlPage.Document.QueryString["ContactID"].ToString() != "")
                        {
                            ContactID = Convert.ToInt32(HtmlPage.Document.QueryString["ContactID"]);
                        }
                    }

                    ///if user is logged in
                    if (CustomerID != 0 && ContactID != 0)
                    {

                        ProductServiceClient oSvc = new ProductServiceClient();
                        oSvc.GetContactQuickTextFieldsCompleted += new EventHandler<GetContactQuickTextFieldsCompletedEventArgs>(oSvc_GetContactQuickTextFieldsCompleted);
                        oSvc.GetContactQuickTextFieldsAsync(CustomerID, ContactID);
                    }
                    else //embedded mode but user not logged in hence execution ends
                    {
                        //show logo placerholder image
                        BitmapImage oLogoImage = new BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + "designer/upload-Logo.png"));
                        oLogoImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        oLogoImage.ImageOpened += new EventHandler<RoutedEventArgs>(imgLogo_ImageOpened);
                        imgLogo.Source = oLogoImage;


                        brdLogoLoader.Visibility = System.Windows.Visibility.Visible;
                        ProgressBarLogo.IsIndeterminate = true;
                        lblLogoProgress.Text = "Loading Logo";

                        imgLogo.MouseLeftButtonDown -= new MouseButtonEventHandler(imgLogo_MouseLeftButtonDown);
                        btnLogoUpload.Visibility = System.Windows.Visibility.Collapsed;
                        lblLogoMessage.Text = "Please login to access your profile logo";
                        lblLogoMessage2.Visibility = System.Windows.Visibility.Collapsed;
                        lblLogoMessage3.Visibility = System.Windows.Visibility.Collapsed;

                        btnSnapToggle.IsChecked = true;
                        btnShowHideGuides.IsChecked = false;
                        DesignerDisabl2.Visibility = Visibility.Collapsed;
                        ProgressBar1.IsIndeterminate = false;
                        ProgressPanel.Visibility = Visibility.Collapsed;

                        //show quick text window
                        DesignerDisabl2.Visibility = Visibility.Visible;

                        winQuickText.SetValue(Canvas.LeftProperty, LayoutMain.ActualWidth / 2 - (winQuickText.ActualWidth / 2));  //e.X + ContainerSize.Width
                        winQuickText.SetValue(Canvas.TopProperty, (LayoutMain.ActualHeight / 2) - (winQuickText.ActualHeight / 3));
                       
                        winQuickText.IsOpened = true;
                    }

                }
                else  //not embedded mode.. execution ends.
                {

                    pnlLogo.Visibility = System.Windows.Visibility.Collapsed;
                    btnSnapToggle.IsChecked = true;
                    btnShowHideGuides.IsChecked = false;
                    DesignerDisabl2.Visibility = Visibility.Collapsed;
                    ProgressBar1.IsIndeterminate = false;
                    ProgressPanel.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objSrvObjects_GetProductObjectsProductCompleted::" + ex.ToString());
            }
        }

        

        void oSvc_GetContactQuickTextFieldsCompleted(object sender, GetContactQuickTextFieldsCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                DictionaryManager.AppObjects["QuickText"] = e.Result;

                if (e.Result.Address1 != null )
                    txtQAddress1.Text = e.Result.Address1;

                if (e.Result.Address2 != null)
                    txtQAddress2.Text = e.Result.Address2;

                if (e.Result.Address3 != null)
                    txtQAddress3.Text = e.Result.Address3;

                if (e.Result.Company != null)
                    txtQCompany.Text = e.Result.Company;

                if (e.Result.CompanyMessage != null)
                    txtQCompanyMessage.Text = e.Result.CompanyMessage;

                if (e.Result.Email != null)
                    txtQEmail.Text = e.Result.Email;

                if (e.Result.Fax != null)
                    txtQFax.Text = e.Result.Fax;

                if (e.Result.Name != null)
                    txtQName.Text = e.Result.Name;

                if (e.Result.Telephone != null)
                    txtQTelephone.Text = e.Result.Telephone;

                if (e.Result.Title != null)
                    txtQTitle.Text = e.Result.Title;

                if (e.Result.Website != null)
                    txtQWebsite.Text = e.Result.Website;

                if (e.Result.LogoPath != null && e.Result.LogoPath != string.Empty)
                {
                    imgLogo.Source = new BitmapImage(new Uri(HtmlPage.Document.DocumentUri, e.Result.LogoPath.Substring(1)));
                    lblLogoMessage.Text = "Drag the logo on canvas to insert";
                    btnLogoUpload.Content = "Change Logo";
                }
                else
                {
                    imgLogo.MouseLeftButtonDown -= new MouseButtonEventHandler(imgLogo_MouseLeftButtonDown);   //disabling drag drop
                    lblLogoMessage.Text = "Upload logo image to insert into canvas";
                    btnLogoUpload.Content = "Upload Logo";
                }
                
            }

            //hide progress panel
            btnShowHideGuides.IsChecked = true;
            DesignerDisabl2.Visibility = Visibility.Collapsed;
            ProgressBar1.IsIndeterminate = false;
            ProgressPanel.Visibility = Visibility.Collapsed;


            //show quick text window
            DesignerDisabl2.Visibility = Visibility.Visible;
            winQuickText.SetValue(Canvas.LeftProperty, LayoutMain.ActualWidth / 2 - (winQuickText.ActualWidth / 2));  //e.X + ContainerSize.Width
            winQuickText.SetValue(Canvas.TopProperty, (LayoutMain.ActualHeight / 2) - (winQuickText.ActualHeight / 3));
            winQuickText.IsOpened = true;
        }

      

        void objSrvClr_GetColorStyleCompleted(object sender, webprintDesigner.ProductServiceReference.GetColorStyleCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    List<ProductServiceReference.TemplateColorStyles> objColorStyleList = e.Result.ToList();
                    if (objColorStyleList.Count() > 0)
                    {
                        webprintDesigner.ColorDataList clrList = new ColorDataList();
                        
                        foreach (ProductServiceReference.TemplateColorStyles objColorStyle in objColorStyleList)
                        {
                            objColorStyle.ColorHex = clrList.getColorHex(Convert.ToInt32(Math.Floor(objColorStyle.ColorC.Value)), Convert.ToInt32(Math.Floor(objColorStyle.ColorM.Value)), Convert.ToInt32(Math.Floor(objColorStyle.ColorY.Value)), Convert.ToInt32(Math.Floor(objColorStyle.ColorK.Value)));
                        }
                        lstColors.ItemsSource = objColorStyleList;
                        if (SelObjCnt > 0)
                            ddpColor.IsEnabled = true;
                        ClrDwn = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objSrvClr_GetColorStyleCompleted::" + ex.ToString());
            }
        }

        private void lstImagesList_ImageOpened(object sender, RoutedEventArgs e)
        {
            UserImagesLoadedCount++;

            if (UserImagesCount == UserImagesLoadedCount)
            {
                brdLstImagesLoader.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        void objSrv_GetProductBackgroundImagesCompleted(object sender, webprintDesigner.ProductServiceReference.GetProductBackgroundImagesCompletedEventArgs e)
        {
            try
            {
                //lstBkImagesList.ItemsSource = e.Result.Where(g => g.ImageType == 1).ToList();


                lstImagesList.ItemsSource = e.Result.Where(g => g.ImageType == 2).ToList();
                UserImagesCount = lstImagesList.Items.Count;
                UserImagesLoadedCount = 0;
                if (UserImagesCount > 0 )
                    brdLstImagesLoader.Visibility = System.Windows.Visibility.Visible;

                //Testing code by MZ to be deleted

                //////List<ProductServiceReference.ProductBackgroundImages> mp = new List<ProductServiceReference.ProductBackgroundImages>();
                //////ProductServiceReference.ProductBackgroundImages x = new ProductServiceReference.ProductBackgroundImages();
                //////x.BackgroundImageRelativePath = "Designer/Products/1077/01.jpg";

                //////mp.Add(x);

                //////x = new ProductServiceReference.ProductBackgroundImages();
                //////x.BackgroundImageRelativePath = "Designer/Products/1077/mmm_Thumb.jpg";

                //////mp.Add(x);

                //////lstBkImagesList.ItemsSource = mp;


                ProductServiceReference.ProductServiceClient objSrvClr = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                objSrvClr.GetColorStyleCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetColorStyleCompletedEventArgs>(objSrvClr_GetColorStyleCompleted);
                objSrvClr.GetColorStyleAsync(ProductId);
                if (ProductId != 0)
                {
                    ProductServiceReference.ProductServiceClient objProSrv = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                    objProSrv.GetProductByIdCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetProductByIdCompletedEventArgs>(objProSrv_GetProductByIdCompleted);
                    objProSrv.GetProductByIdAsync(ProductId);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objSrv_GetProductBackgroundImagesCompleted::" + ex.ToString());
            }
        }
        void objSrv_UserLoginCompleted(object sender, webprintDesigner.UserServiceReference.UserLoginCompletedEventArgs e)
        {
            try
            {
                if ((bool)e.Result == true)
                {
                    lstImagesList.Visibility = Visibility.Visible;
                   
                    //GetUserImages();
                    IsUserLogin = true;
                    IsResUsrLogin = true;
                }
                else
                {
                    lstImagesList.Visibility = Visibility.Collapsed;
                   
                    IsUserLogin = false;
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objSrv_UserLoginCompleted::" + ex.ToString());
            }
        }
        

        void objSrv_IsUserLoginedCompleted2(object sender, webprintDesigner.UserServiceReference.IsUserLoginedCompletedEventArgs e)
        {
            try
            {
                if ((bool)e.Result == true)
                {
                    lstImagesList.Visibility = Visibility.Visible;
                  
                    IsUserLogin = true;
                    IsResUsrLogin = true;
                }
                else
                {
                    lstImagesList.Visibility = Visibility.Collapsed;
                    
                    IsResUsrLogin = true;
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objSrv_IsUserLoginedCompleted2::" + ex.ToString());
            }
        }


        //void objSrv_GetUserImagesCompleted(object sender, webprintDesigner.UserServiceReference.GetUserImagesCompletedEventArgs e)
        //{
        //    try
        //    {
        //        lstImagesList.ItemsSource = e.Result.ToList();
        //        //MessageBox.Show(e.Result.ToList().Count.ToString());
        //        //webprintDesigner.UserServiceReference.UserImages uimg = (webprintDesigner.UserServiceReference.UserImages)lstImagesList.Items[0];
        //        //MessageBox.Show(uimg.ImageAbsolutePath);
        //        //MessageBox.Show(uimg.ImageRelativePath);
        //        //MessageBox.Show(uimg.ImageName);

        //        if (progressSource == ProgressSource.UploadImage)
        //        {

        //            ProgressBar1.IsIndeterminate = false;
        //            ProgressPanel.Visibility = Visibility.Collapsed;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::objSrv_GetUserImagesCompleted::" + ex.ToString());
        //    }
        //}


        void objSrvClient_GetProductBackgroundImgCompleted(object sender, webprintDesigner.ProductServiceReference.GetProductBackgroundImgCompletedEventArgs e)
        {
            try
            {
                if (e.Result != "")
                {
                    System.Windows.Media.Imaging.BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + e.Result));
                    ImageBrush imgBk = new ImageBrush();
                    imgBk.Stretch = Stretch.Fill;
                    imgBk.ImageSource = bimg;
                    SelectedPage.brdDesign.Background = imgBk;
                }
                DesignerDisabl2.Visibility = Visibility.Collapsed;
                ProgressBar1.IsIndeterminate = false;
                ProgressPanel.Visibility = Visibility.Collapsed;
                //lstBkImagesList.SelectedItem = null;
                //ddpBackgroundImages.IsOpened = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objSrvObjects_GetProductObjectsProductCompleted::" + ex.ToString());
            }
        }
        #endregion

        #region "Add Controls"
        // add text addtext addtxt
        private void AddTxtObject_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtObjectName.Text != "" && SelectedPage != null) //&& SelectedTxtCtls != null
                    {
                        int OType = 1;
                        if (rdoSText.IsChecked == true) OType = 1;
                        else if (rdoMText.IsChecked == true) OType = 2;
                        else if (rdoLable.IsChecked == true) OType = 4;
                        if (SelectedPage != null) //&& SelectedTxtCtls != null
                        {
                            webprintDesigner.ProductServiceReference.TemplateFonts cmbFontItm = new webprintDesigner.ProductServiceReference.TemplateFonts();
                            foreach (webprintDesigner.ProductServiceReference.TemplateFonts item in cmbFontList.Items)
                            {
                                if (item.FontDisplayName == "Trebuchet MS")
                                {
                                    cmbFontItm = item;
                                    break;
                                }
                            }
                            if (txtFontSize.Value == 0)
                            {
                                txtFontSize.Value = 12;
                            }

                            ObjectContainer SelOc = null;
                            if (App.DesignerMode == DesignerModes.CreatorMode)
                            {
                                if (!SelObjPro && SelectedPage != null)
                                {

                                    foreach (UIElement el in SelectedPage.DesignArea.Children)
                                    {
                                        if (el.GetType().Name == "ObjectContainer")
                                        {
                                            ObjectContainer oc = (ObjectContainer)el;
                                            if (oc.Selected && !oc.MouseDown)
                                            {
                                                SelOc = oc;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            #region Object
                            ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
                            objObject.ObjectType = OType;
                            objObject.ColorC = 0;
                            objObject.ColorM = 100;
                            objObject.ColorY =100;
                            objObject.ColorK = 0;
                            objObject.Allignment = 1;
                            objObject.ColorStyleID = 0;
                            objObject.ColorType = 3;
                            objObject.PositionX = SelectedPage.brdDesign.ActualWidth / 2;
                            objObject.PositionY = SelectedPage.brdDesign.ActualHeight / 4;
                            objObject.MaxWidth = 100;
                            objObject.MaxHeight = 40;
                            objObject.RotationAngle = 0;
                            if (txtObjectName.Text.Length > 199)
                                objObject.Name = txtObjectName.Text.Substring(0, 199); //limiting the name to 200 chars due to db limit
                            else
                                objObject.Name = txtObjectName.Text;

                            
                            objObject.ContentString = txtObjectName.Text;
                            if (cmbFontItm != null)
                            {
                                objObject.FontName = cmbFontItm.FontDisplayName;
                                objObject.IsFontNamePrivate = cmbFontItm.IsPrivateFont;
                            }
                            else
                            {
                                objObject.FontName = "";
                                objObject.IsFontNamePrivate = false;
                            }
                            objObject.FontSize = txtFontSize.Value;
                            objObject.IsBold = false;
                            objObject.IsItalic = false;
                            objObject.IsUnderlinedText = false;
                            
                            
                            objObject.MaxCharacters = 0;
                            objObject.IsFontCustom = true;
                            objObject.PageNo = 1;
                            //objObject.SpaceAfter = 0;
                            objObject.DisplayOrderPdf = 0;
                            objObject.DisplayOrderTxtControl = 0;
                            objObject.OffsetX = 0;
                            objObject.OffsetY = 0;
                            objObject.ColorName = string.Empty;
                            objObject.ExField1 = string.Empty;
                            objObject.ExField2 = string.Empty;
                            objObject.SpotColorName = string.Empty;


                            
                            objObject.IsEditable = true;
                            objObject.IsPositionLocked = false;
                            objObject.IsHidden = false;

                            objObject.IsNewLine = false;
                            #endregion
                            if (App.DesignerMode == DesignerModes.CreatorMode && SelOc != null)
                            {
                                if (SelOc.SelContainerPanel != null)
                                {
                                    objObject.ParentId = 0;
                                    objObject.IsNewLine = (bool)chkControlNewLine.IsChecked;
                                    objObject.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                                    bool bl = SelOc.AddChildCtrol(objObject, cmbFontItm);

                                    //SelectedTxtCtls.AddControls(SelOc.ContainerName, "CtlTxtContent_" + CtlIdx.ToString(), txtObjectName.Text, txtObjectName.Text, OType);
                                    //SelectedTxtCtls.UpdateCtrlsList();
                                    CtlIdx++;
                                    // SelOc.UnSelectContainer();
                                    SelOc.SelectContainer(objObject.TCtlName,new Point(0,0),new Point(0,0));

                                    
                                }
                            }
                            else
                            {
                                objObject.ParentId = 0;
                                objObject.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                                SelectedPage.AddObject("CtlContainerTxt_" + CtlIdx.ToString(), "CtlTxtContent_" + CtlIdx.ToString(), txtObjectName.Text, txtObjectName.Text, OType, cmbFontItm, webprintDesigner.Common.PointToPixel(txtFontSize.Value), objObject);

                                //SelOc.SelectContainer("CtlTxtContent_" + CtlIdx.ToString(), new Point(0, 0), new Point(0, 0));
                                //SelectedTxtCtls.AddControls("CtlContainerTxt_" + CtlIdx.ToString(), "CtlTxtContent_" + CtlIdx.ToString(), txtObjectName.Text, txtObjectName.Text, OType);
                                //SelectedTxtCtls.UpdateCtrlsList();
                                CtlIdx++;
                            }
                        }
                        ddpAddTxtObject.IsOpened = false;
                        ZoomSlider.Value = 1;
                    }
                    else
                    {
                        MessageBox.Show("Please enter name");
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddTxtObject_Click::" + ex.ToString());
            }
        }
        private void CancelTxtObject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ddpAddTxtObject.IsOpened = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CancelTxtObject_Click::" + ex.ToString());
            }
        }
        #endregion
        #region " "




        #endregion
        #region "Object Event"



        private void DesignArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    UnSelObject();
            //    DesignFocus.Focus();
            //    SelEnbProButton();
            //}
            //catch (Exception ex)
            //{
            //    if (ShowException)
            //        MessageBox.Show("::DesignArea_MouseLeftButtonDown::" + ex.ToString());
            //}
        }
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    bool isHitObject = false;
            //    bool IsUpdate = false;
            //    //foreach (UIElement el in DesignArea.Children)
            //    //{
            //    for (int idx = DesignArea.Children.Count - 1; idx >= 0; idx--)
            //    {

            //        if (DesignArea.Children[idx].GetType().Name == "ObjectContainer")
            //        {
            //            ObjectContainer oc = (ObjectContainer)DesignArea.Children[idx];
            //            if (oc.Selected && !oc.MouseDown)
            //            {

            //                if (e.Key != Key.Unknown)
            //                {
            //                    if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            //                    {
            //                        //hdrtxt.Text = (el as Grid).Width.ToString();
            //                        Size NewSize = new Size(oc.Width, oc.Height);
            //                        if (e.Key == Key.Left)
            //                        {
            //                            if (oc.OriginalSize.Width > 32)
            //                                oc.OriginalSize.Width = oc.OriginalSize.Width - 5;
            //                            oc.UpdateContainerSize(oc.OriginalSize);
            //                            isHitObject = true;
            //                        }
            //                        else if (e.Key == Key.Up)
            //                        {
            //                            if (oc.OriginalSize.Height > 16)
            //                                oc.OriginalSize.Height = oc.OriginalSize.Height - 5;
            //                            oc.UpdateContainerSize(oc.OriginalSize);
            //                            isHitObject = true;
            //                        }
            //                        else if (e.Key == Key.Right)
            //                        {
            //                            oc.OriginalSize.Width = oc.OriginalSize.Width + 5;
            //                            oc.UpdateContainerSize(oc.OriginalSize);
            //                            isHitObject = true;
            //                        }
            //                        else if (e.Key == Key.Down)
            //                        {
            //                            oc.OriginalSize.Height = oc.OriginalSize.Height + 5;
            //                            oc.UpdateContainerSize(oc.OriginalSize);
            //                            isHitObject = true;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        double Lf = (double)oc.GetValue(Canvas.LeftProperty);
            //                        double Tp = (double)oc.GetValue(Canvas.TopProperty);
            //                        if (e.Key == Key.Left)
            //                        {
            //                            oc.SetValue(Canvas.LeftProperty, Lf - 5);
            //                            HorRuler.PointerVal = (double)oc.GetValue(Canvas.LeftProperty);
            //                            HorRuler.ShowPointer();
            //                            isHitObject = true;
            //                        }
            //                        else if (e.Key == Key.Up)
            //                        {
            //                            oc.SetValue(Canvas.TopProperty, Tp - 5);
            //                            VerRuler.PointerVal = (double)oc.GetValue(Canvas.TopProperty);
            //                            VerRuler.ShowPointer();
            //                            isHitObject = true;
            //                        }
            //                        else if (e.Key == Key.Right)
            //                        {
            //                            oc.SetValue(Canvas.LeftProperty, Lf + 5);
            //                            HorRuler.PointerVal = (double)oc.GetValue(Canvas.LeftProperty);
            //                            HorRuler.ShowPointer();
            //                            isHitObject = true;
            //                        }
            //                        else if (e.Key == Key.Down)
            //                        {
            //                            oc.SetValue(Canvas.TopProperty, Tp + 5);
            //                            VerRuler.PointerVal = (double)oc.GetValue(Canvas.TopProperty);
            //                            VerRuler.ShowPointer();
            //                            isHitObject = true;
            //                        }
            //                        else if (e.Key == Key.Delete)
            //                        {

            //                            RemoveZIdx((int)oc.GetValue(Canvas.ZIndexProperty));
            //                            DesignArea.Children.RemoveAt(idx);
            //                            if (oc.TxtCtlName != "" && CtrlsList.FindName(oc.TxtCtlName) != null)
            //                            {
            //                                if (((TextBox)CtrlsList.FindName(oc.TxtCtlName)).Parent.GetType().Name == "Grid")
            //                                {
            //                                    CtrlsList.Children.Remove(((TextBox)CtrlsList.FindName(oc.TxtCtlName)).Parent as UIElement);
            //                                    IsUpdate = true;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }

            //                //oc.UnSelectContainer();
            //            }
            //        }
            //    }
            //    if (IsUpdate)
            //        UpdateCtrlsList();
            //    // e.Handled = !isHitObject;
            //}
            //catch (Exception ex)
            //{
            //    if (ShowException)
            //        MessageBox.Show("::UserControl_KeyDown::" + ex.ToString());
            //}
        }
        public void SelEnbProButton(UIElementCollection objObjects)
        {
            try
            {
                

                SelObjPro = true;
                SelObjCnt = 0;
                double OptTmp = 0.3;
                cmbFontList.IsEnabled = false;
                //cmbFontSize.IsEnabled = false;
                txtFontSize.IsEnabled = false;
                txtLineHeight.IsEnabled = false;
                txtFontSize.Value = 12;
                txtLineHeight.Value = 0;
                cmbRotate.IsEnabled = false;
                ddpColor.IsEnabled = false;
                btnBold.IsEnabled = false;
                btnItalic.IsEnabled = false;
                btnUnderline.IsEnabled = false;
                btnLeftAlign.IsEnabled = false;
                btnCenterAlign.IsEnabled = false;
                btnRightAlign.IsEnabled = false;
                //btnJustifyAlign.IsEnabled = false;
                btnObjLAlign.IsEnabled = false;
                btnObjVCAlign.IsEnabled = false;
                btnObjRAlign.IsEnabled = false;
                btnObjTAlign.IsEnabled = false;
                btnObjCAlign.IsEnabled = false;
                btnObjBAlign.IsEnabled = false;
                btnBringToFront.IsEnabled = false;
                btnBringForward.IsEnabled = false;
                btnSendBackward.IsEnabled = false;
                btnSendToBack.IsEnabled = false;

                btnBold.Opacity = OptTmp;
                btnItalic.Opacity = OptTmp;
                btnUnderline.Opacity = OptTmp;
                btnLeftAlign.Opacity = OptTmp;
                btnCenterAlign.Opacity = OptTmp;
                btnRightAlign.Opacity = OptTmp;
                //btnJustifyAlign.Opacity = OptTmp;
                btnObjLAlign.Opacity = OptTmp;
                btnObjVCAlign.Opacity = OptTmp;
                btnObjRAlign.Opacity = OptTmp;
                btnObjTAlign.Opacity = OptTmp;
                btnObjCAlign.Opacity = OptTmp;
                btnObjBAlign.Opacity = OptTmp;

                btnBringToFront.Opacity = OptTmp;
                btnBringForward.Opacity = OptTmp;
                btnSendBackward.Opacity = OptTmp;
                btnSendToBack.Opacity = OptTmp;

                btnBold.BorderThickness = new Thickness(0);
                btnItalic.BorderThickness = new Thickness(0);
                btnUnderline.BorderThickness = new Thickness(0);
                btnLeftAlign.BorderThickness = new Thickness(0);
                btnCenterAlign.BorderThickness = new Thickness(0);
                btnRightAlign.BorderThickness = new Thickness(0);
                //btnJustifyAlign.BorderThickness = new Thickness(0);
                if (FntDwn)
                {
                    cmbFontList.SelectedIndex = 0;
                }
                cmbRotate.SelectedIndex = -1;
                bool txtFnd = false;
                bool objFnd = false;
                if (objObjects != null)
                {
                    foreach (UIElement el in objObjects)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer oc = (ObjectContainer)el;
                            if (oc.Selected)
                            {
                                if (!objFnd)
                                {
                                    lblObjPos.Text = "( " + Convert.ToDouble(oc.GetValue(Canvas.LeftProperty)).ToString("F0") + " , " + Convert.ToDouble(oc.GetValue(Canvas.TopProperty)).ToString("F0") + " )";
                                    

                                    cmbRotate.IsEnabled = true;
                                    double an = oc.getAngle();
                                    if (an == 0)
                                        cmbRotate.SelectedIndex = 0;
                                    else if (an == 90)
                                        cmbRotate.SelectedIndex = 1;
                                    else if (an == 180)
                                        cmbRotate.SelectedIndex = 2;
                                    else if (an == 270)
                                        cmbRotate.SelectedIndex = 3;
                                    else
                                        cmbRotate.SelectedIndex = -1;
                                    objFnd = true;


                                    chkLockEditing.IsEnabled = false;

                                    chkLockPosition.IsChecked = oc.IsLockedPosition;
                                    chkLockEditing.IsChecked = false;
                                    chkShowHide.IsChecked = oc.IsPrintable;

                                    pnlFontArea.Visibility = System.Windows.Visibility.Collapsed;
                                    pnlFontDecorationsArea.Visibility = System.Windows.Visibility.Collapsed;
                                    pnlImageArea.Visibility = System.Windows.Visibility.Visible;

                                    if (oc.ContainerType == ObjectContainer.ContainerTypes.Image )
                                    {//image object
                                        imgSelectedImagePreview.Source = oc.ContainerImage.Source;
                                        btnCropImage.Visibility = System.Windows.Visibility.Visible;
                                    }
                                    else if (oc.ContainerType == ObjectContainer.ContainerTypes.LogoImage)
                                    {
                                        imgSelectedImagePreview.Source = oc.ContainerImage.Source;

                                        if (App.DesignerMode != DesignerModes.CreatorMode)
                                            btnCropImage.Visibility = System.Windows.Visibility.Visible;
                                        else
                                            btnCropImage.Visibility = System.Windows.Visibility.Collapsed;
                                        
                                    }
                                    else if (oc.ContainerType == ObjectContainer.ContainerTypes.LineVector || oc.ContainerType == ObjectContainer.ContainerTypes.RectangleVector || oc.ContainerType == ObjectContainer.ContainerTypes.EllipseVector)
                                    {
                                        pnlImageArea.Visibility = System.Windows.Visibility.Collapsed;
                                        pnlFontDecorationsArea.Visibility = System.Windows.Visibility.Visible;
                                        pnlFontAlignment.Visibility = System.Windows.Visibility.Collapsed;
                                        pnlFontStyle.Visibility = System.Windows.Visibility.Collapsed;
                                        ddpColor.IsEnabled = true;
                                    }
                                    else
                                    {
                                        pnlFontAlignment.Visibility = System.Windows.Visibility.Visible;
                                        pnlFontStyle.Visibility = System.Windows.Visibility.Visible;
                                        ddpColor.IsEnabled = true;
                                    }
                                }

                                if (oc.ContainerType != ObjectContainer.ContainerTypes.Image && oc.ContainerType != ObjectContainer.ContainerTypes.LogoImage && oc.ContainerType != ObjectContainer.ContainerTypes.LineVector && oc.ContainerType != ObjectContainer.ContainerTypes.RectangleVector && oc.ContainerType != ObjectContainer.ContainerTypes.EllipseVector && !txtFnd)
                                {
                                    pnlFontArea.Visibility = System.Windows.Visibility.Visible;
                                    pnlFontDecorationsArea.Visibility = System.Windows.Visibility.Visible;
                                    pnlImageArea.Visibility = System.Windows.Visibility.Collapsed;
                                    if (FntDwn)
                                    {
                                        cmbFontList.IsEnabled = true;
                                        cmbFontList.SelectedIndex = 0;
                                    }
                                    //cmbFontSize.IsEnabled = true;
                                    txtFontSize.IsEnabled = true;
                                    txtLineHeight.IsEnabled = true;
                                    if (ClrDwn)
                                        ddpColor.IsEnabled = true;
                                    btnBold.IsEnabled = true;
                                    btnItalic.IsEnabled = true;
                                    btnUnderline.IsEnabled = true;
                                    btnLeftAlign.IsEnabled = true;
                                    btnCenterAlign.IsEnabled = true;
                                    btnRightAlign.IsEnabled = true;
                                    //btnJustifyAlign.IsEnabled = true;

                                    btnBold.Opacity = 1;
                                    btnItalic.Opacity = 1;
                                    btnUnderline.Opacity = 1;
                                    btnLeftAlign.Opacity = 1;
                                    btnCenterAlign.Opacity = 1;
                                    btnRightAlign.Opacity = 1;
                                    //btnJustifyAlign.Opacity = 1;


                                    chkLockEditing.IsEnabled = true;

                                    chkLockPosition.IsChecked = oc.IsLockedPosition;
                                    chkLockEditing.IsChecked = !oc.IsLockedEditing;
                                    chkShowHide.IsChecked = oc.IsPrintable;

                                    TextBlock tb = oc.getContainerContent;
                                    if (oc.SelContainerPanel != null)
                                    {
                                        if (oc.SelContainerPanel.HAlign == TextAlignment.Left)
                                            btnLeftAlign.BorderThickness = new Thickness(1);
                                        else if (oc.SelContainerPanel.HAlign == TextAlignment.Center)
                                            btnCenterAlign.BorderThickness = new Thickness(1);
                                        else if (oc.SelContainerPanel.HAlign == TextAlignment.Right)
                                            btnRightAlign.BorderThickness = new Thickness(1);
                                        //else if ( oc.SelContainerPanel.HAlign == TextAlignment.Justify)
                                        //    btnJustifyAlign.BorderThickness = new Thickness(1);
                                    }
                                    if (tb != null)
                                    {
                                        int fntInd = 0;
                                        bool IsLstFnd = false;
                                        foreach (webprintDesigner.ProductServiceReference.TemplateFonts fnt in cmbFontList.Items)
                                        {
                                            if (fnt.FontDisplayName == tb.FontFamily.Source)
                                            {
                                                IsLstFnd = true;
                                                break;
                                            }
                                            fntInd++;
                                        }
                                        if (IsLstFnd)
                                            cmbFontList.SelectedIndex = fntInd;
                                        fntInd = 0;
                                        IsLstFnd = false;

                                        //foreach (ComboBoxItem fntsz in cmbFontSize.Items)
                                        //{
                                        //    if (fntsz.Content.ToString() == tb.FontSize.ToString())
                                        //    {
                                        //        IsLstFnd = true;
                                        //        break;
                                        //    }
                                        //    fntInd++;
                                        //}
                                        //if (IsLstFnd)
                                        //cmbFontSize.SelectedIndex = fntInd;
                                        txtFontSize.Value = Math.Round(webprintDesigner.Common.PixelToPoint(tb.FontSize), 2);
                                        if (tb.LineHeight != double.NaN)
                                            txtLineHeight.Value = tb.LineHeight;
                                        else
                                            txtLineHeight.Value = 0;
                                        if (tb.FontWeight == FontWeights.Bold)
                                            btnBold.BorderThickness = new Thickness(1);
                                        else
                                            btnBold.BorderThickness = new Thickness(0);
                                        if (tb.FontStyle == FontStyles.Italic)
                                            btnItalic.BorderThickness = new Thickness(1);
                                        if (tb.TextDecorations == TextDecorations.Underline)
                                            btnUnderline.BorderThickness = new Thickness(1);

                                    }
                                    txtFnd = true;
                                }
                                SelObjCnt++;
                            }
                        }
                    }
                }

                if (SelObjCnt > 1)
                {
                    btnObjLAlign.IsEnabled = true;
                    btnObjVCAlign.IsEnabled = true;
                    btnObjRAlign.IsEnabled = true;
                    btnObjTAlign.IsEnabled = true;
                    btnObjCAlign.IsEnabled = true;
                    btnObjBAlign.IsEnabled = true;

                    btnObjLAlign.Opacity = 1;
                    btnObjVCAlign.Opacity = 1;
                    btnObjRAlign.Opacity = 1;
                    btnObjTAlign.Opacity = 1;
                    btnObjCAlign.Opacity = 1;
                    btnObjBAlign.Opacity = 1;
                }
                if (SelObjCnt == 1)
                {
                    btnBringToFront.IsEnabled = true;
                    btnBringForward.IsEnabled = true;
                    btnSendBackward.IsEnabled = true;
                    btnSendToBack.IsEnabled = true;
                    btnBringToFront.Opacity = 1;
                    btnBringForward.Opacity = 1;
                    btnSendBackward.Opacity = 1;
                    btnSendToBack.Opacity = 1;
                }
                SelObjPro = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::SelEnbProButton::" + ex.ToString());
            }
        }

        public void HideEditorWindow()
        {
            if (winEditorNew.Visibility  ==  System.Windows.Visibility.Visible)
                winEditorNew.Visibility = System.Windows.Visibility.Collapsed ;

            lblObjPos.Text = "";
        }

        #endregion
        #region "Layout main"
        private void LayoutMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {

                tbDesignPages.Width = LayoutMain.ActualWidth;
                tbDesignPages.Height = LayoutMain.ActualHeight;
                //LayoutDesigner.Width = LayoutMain.ActualWidth;
                //LayoutDesigner.Height = LayoutMain.ActualHeight;
                DesignerDisabl.Width = LayoutMain.ActualWidth;
                DesignerDisabl.Height = LayoutMain.ActualHeight;
                DesignerDisabl2.Width = LayoutMain.ActualWidth;
                DesignerDisabl2.Height = LayoutMain.ActualHeight;
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
                if (ShowException)
                    MessageBox.Show("::LayoutMain_SizeChanged::" + ex.ToString());
            }
        }



        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBlock curTextBlock = sender as TextBlock;
                webprintDesigner.ProductServiceReference.TemplateFonts objFonts = curTextBlock.DataContext as webprintDesigner.ProductServiceReference.TemplateFonts;
                if (objFonts.IsPrivateFont == true)
                {
                    System.IO.Stream ms = new System.IO.MemoryStream(objFonts.FontBytes);
                    curTextBlock.FontSource = new FontSource(ms);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::TextBlock_Loaded::" + ex.ToString());
            }
        }
        private void ExpanderLeft_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                //adnControls.Height = ExpanderLeft.ActualHeight - 2;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ExpanderLeft_SizeChanged::" + ex.ToString());
            }
        }


        private void ExpanderProp_Collapsed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ddpColor.IsOpened == true)
                    ddpColor.IsOpened = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ExpanderProp_Collapsed::" + ex.ToString());
            }
        }
        #endregion
        #region "Object Properties"
        public webprintDesigner.ProductServiceReference.TemplateFonts FindFont(string FntName)
        {
            webprintDesigner.ProductServiceReference.TemplateFonts ObjectFont = null;


            if (cmbFontList.SelectedIndex != -1)
            {
                bool FontFnd = false;
                ObjectFont = new webprintDesigner.ProductServiceReference.TemplateFonts();
                foreach (webprintDesigner.ProductServiceReference.TemplateFonts fnt in cmbFontList.Items)
                {
                    if (fnt.FontDisplayName.ToLower().Trim() == FntName.ToLower().Trim())
                    {
                        ObjectFont = fnt;
                        ////if (FontSource == FontSources.IsoLatedStorage)
                        ////{
                        ////    if (ObjectFont.IsPrivateFont)
                        ////    {
                        ////        ObjectFont.FontBytes = getFontFromLocalStorage(FntName);
                        ////        if (ObjectFont.FontBytes != null)
                        ////        {
                        ////            FontFnd = true;
                        ////        }
                        ////        break;
                        ////    }
                        ////}
                        FontFnd = true;
                        break;
                    }
                }
                if (!FontFnd)
                    ObjectFont = (webprintDesigner.ProductServiceReference.TemplateFonts)cmbFontList.Items[0];
            }
            
           
            return ObjectFont;
        }

        /// <summary>
        /// Rotate button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRotate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                    if (cmbRotate.SelectedIndex != -1 && !SelObjPro && SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateRotation2(Convert.ToDouble(((ComboBoxItem)cmbRotate.SelectedItem).Content));
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::cmbRotate_SelectionChanged::" + ex.ToString());
            }
        }

        private void cmbFontList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
               
                
                    if (cmbFontList.SelectedIndex != -1 && !SelObjPro && SelectedPage != null)
                    {
                        webprintDesigner.ProductServiceReference.TemplateFonts cmbItm = (webprintDesigner.ProductServiceReference.TemplateFonts)cmbFontList.SelectedItem;
                        cmbFontList.FontFamily = new FontFamily(cmbItm.FontName);
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerFont(cmbItm);
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::cmbFontList_SelectionChanged::" + ex.ToString());
            }

        }

        //private void CheckDouble(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (sender != null)
        //        {
        //            if (sender.GetType().Name == "TextBox")
        //            {
        //                TextBox txtbox = sender as TextBox;
        //                if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
        //                {
        //                    e.Handled = true;
        //                }
        //                else if (e.Key == Key.Decimal || (e.Key == Key.Unknown && e.PlatformKeyCode == 190))
        //                {
        //                    if (txtFontSize.Text != "")
        //                        e.Handled = txtbox.Text.Contains(".");
        //                    else
        //                        e.Handled = false;
        //                }
        //                else if (Key.D0 <= e.Key && e.Key <= Key.D9)
        //                {
        //                    e.Handled = false;
        //                }
        //                else if (Key.NumPad0 <= e.Key && e.Key <= Key.NumPad9)
        //                {
        //                    e.Handled = false;
        //                }
        //                else
        //                    e.Handled = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::txtFontSize_KeyDown::" + ex.ToString());
        //    }

        //}

        
        private void txtFontSize_GotFocus(object sender, RoutedEventArgs e)
        {
            IsFontChange = true;
        }
        private void txtFontSize_LostFocus(object sender, RoutedEventArgs e)
        {
            IsFontChange = false;
            if (txtFontSize.Value == 0)
            {
                txtFontSize.Value = 12;
            }
        }




        private void txtLineHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (txtLineHeight.Value != 0 && !SelObjPro && SelectedPage != null)
            {
                foreach (UIElement el in SelectedPage.DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer oc = (ObjectContainer)el;
                        if (oc.Selected && !oc.MouseDown)
                        {
                            oc.UpdateLineHeight(txtLineHeight.Value);
                        }
                            //oc.UpdateLineHeight(webprintDesigner.Common.PointToPixel(Convert.ToDouble(txtLineHeight.Value)));

                    }
                }
            }

        }

        private void txtLineHeight_GotFocus(object sender, RoutedEventArgs e)
        {
            IsLineHChange = true;
        }

        private void txtLineHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            IsLineHChange = false;
            //if (txtLineHeight.Value == "")
            //    txtLineHeight.Text = "0";
        }
        //private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbFontSize.SelectedIndex != -1 && !SelObjPro)
        //        {
        //            foreach (UIElement el in DesignArea.Children)
        //            {
        //                if (el.GetType().Name == "ObjectContainer")
        //                {
        //                    ObjectContainer oc = (ObjectContainer)el;
        //                    if (oc.Selected && !oc.MouseDown)
        //                        oc.UpdateContainerFontSize(Convert.ToDouble(((ComboBoxItem)cmbFontSize.SelectedItem).Content));

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::cmbFontSize_SelectionChanged::" + ex.ToString());
        //    }
        //}
        private void btnAlien_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        int Align = Convert.ToInt32((e.OriginalSource as Button).Tag);
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerAlign(Align);
                            }
                        }
                        if (Align == 1)
                        {
                            btnLeftAlign.BorderThickness = new Thickness(1);
                            btnCenterAlign.BorderThickness = new Thickness(0);
                            btnRightAlign.BorderThickness = new Thickness(0);
                            //btnJustifyAlign.BorderThickness = new Thickness(0);
                        }
                        else if (Align == 2)
                        {
                            btnLeftAlign.BorderThickness = new Thickness(0);
                            btnCenterAlign.BorderThickness = new Thickness(1);
                            btnRightAlign.BorderThickness = new Thickness(0);
                            //btnJustifyAlign.BorderThickness = new Thickness(0);
                        }
                        else if (Align == 3)
                        {
                            btnLeftAlign.BorderThickness = new Thickness(0);
                            btnCenterAlign.BorderThickness = new Thickness(0);
                            btnRightAlign.BorderThickness = new Thickness(1);
                            //btnJustifyAlign.BorderThickness = new Thickness(0);
                        }
                        else if (Align == 3)
                        {
                            btnLeftAlign.BorderThickness = new Thickness(0);
                            btnCenterAlign.BorderThickness = new Thickness(0);
                            btnRightAlign.BorderThickness = new Thickness(0);
                            //btnJustifyAlign.BorderThickness = new Thickness(1);
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::btnAlien_Click::" + ex.ToString());
            }
        }
        private void ObjectBold_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerBold();
                            }
                        }
                        if (btnBold.BorderThickness.Left == 0)
                            btnBold.BorderThickness = new Thickness(1);
                        else
                            btnBold.BorderThickness = new Thickness(0);
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ObjectBold_Click::" + ex.ToString());
            }
        }
        private void ObjectItalic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerItalic();
                            }
                        }
                        if (btnItalic.BorderThickness.Left == 0)
                            btnItalic.BorderThickness = new Thickness(1);
                        else
                            btnItalic.BorderThickness = new Thickness(0);
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ObjectItalic_Click::" + ex.ToString());
            }
        }
        private void ObjectUnderline_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerUnderline();
                            }
                        }
                        if (btnUnderline.BorderThickness.Left == 0)
                            btnUnderline.BorderThickness = new Thickness(1);
                        else
                            btnUnderline.BorderThickness = new Thickness(0);
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ObjectUnderline_Click::" + ex.ToString());
            }
        }
        private void btnObjectLeftAlign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        double PosX = 0;
                        bool FstPos = true;
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    if (FstPos)
                                    {
                                        PosX = (double)oc.GetValue(Canvas.LeftProperty);
                                        FstPos = false;
                                    }
                                    else
                                    {
                                        double Lf = (double)oc.GetValue(Canvas.LeftProperty);
                                        if (Lf < PosX)
                                            PosX = Lf;
                                    }
                                }
                            }
                        }
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    oc.SetValue(Canvas.LeftProperty, PosX);
                                }
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::btnObjectLeftAlign_Click::" + ex.ToString());
            }
        }
        private void btnObjectRightAlign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        double PosX = 0;
                        bool FstPos = true;
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    if (FstPos)
                                    {
                                        PosX = (double)oc.GetValue(Canvas.LeftProperty) + oc.OriginalSize.Width;
                                        FstPos = false;
                                    }
                                    else
                                    {
                                        double Rt = (double)oc.GetValue(Canvas.LeftProperty) + oc.OriginalSize.Width;
                                        if (Rt > PosX)
                                            PosX = Rt;
                                    }
                                }
                            }
                        }
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    oc.SetValue(Canvas.LeftProperty, PosX - oc.OriginalSize.Width);
                                }
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::btnObjectRightAlign_Click::" + ex.ToString());
            }
        }
        private void btnObjectCenterAlign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        double PosX = 0;
                        double Wd = 0;
                        bool FstPos = true;
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    if (FstPos)
                                    {
                                        PosX = (double)oc.GetValue(Canvas.LeftProperty);
                                        Wd = oc.OriginalSize.Width;
                                        FstPos = false;
                                    }
                                    else
                                    {
                                        if (oc.OriginalSize.Width > Wd)
                                        {
                                            PosX = (double)oc.GetValue(Canvas.LeftProperty);
                                            Wd = oc.OriginalSize.Width;
                                        }
                                    }
                                }
                            }
                        }
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    double Lf = PosX + Wd / 2 - (oc.OriginalSize.Width / 2);
                                    oc.SetValue(Canvas.LeftProperty, Lf);
                                }
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::btnObjectCenterAlign_Click::" + ex.ToString());
            }
        }
        private void btnObjectTopAlign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                    if (SelectedPage != null)
                    {
                        double PosY = 0;
                        bool FstPos = true;
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    if (FstPos)
                                    {
                                        PosY = (double)oc.GetValue(Canvas.TopProperty);
                                        FstPos = false;
                                    }
                                    else
                                    {
                                        double Tp = (double)oc.GetValue(Canvas.TopProperty);
                                        if (Tp < PosY)
                                            PosY = Tp;
                                    }
                                }
                            }
                        }
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    oc.SetValue(Canvas.TopProperty, PosY);
                                }
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::btnObjectTopAlign_Click::" + ex.ToString());
            }
        }
        private void btnObjectBottomAlign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        double PosY = 0;
                        bool FstPos = true;
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    if (FstPos)
                                    {
                                        PosY = (double)oc.GetValue(Canvas.TopProperty) + oc.OriginalSize.Height;
                                        FstPos = false;
                                    }
                                    else
                                    {
                                        double Bt = (double)oc.GetValue(Canvas.TopProperty) + oc.OriginalSize.Height;
                                        if (Bt > PosY)
                                            PosY = Bt;
                                    }
                                }
                            }
                        }
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    oc.SetValue(Canvas.TopProperty, PosY - oc.OriginalSize.Height);
                                }
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::btnObjectBottomAlign_Click::" + ex.ToString());
            }
        }
        private void btnObjectVCenterAlign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        double PosY = 0;
                        double Ht = 0;
                        bool FstPos = true;
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    if (FstPos)
                                    {
                                        PosY = (double)oc.GetValue(Canvas.TopProperty);
                                        Ht = oc.OriginalSize.Height;
                                        FstPos = false;
                                    }
                                    else
                                    {
                                        if (oc.OriginalSize.Height > Ht)
                                        {
                                            PosY = (double)oc.GetValue(Canvas.TopProperty);
                                            Ht = oc.OriginalSize.Height;
                                        }
                                    }
                                }
                            }
                        }
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                {
                                    double Tp = PosY + Ht / 2 - (oc.OriginalSize.Height / 2);
                                    oc.SetValue(Canvas.TopProperty, Tp);
                                }
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::btnObjectVCenterAlign_Click::" + ex.ToString());
            }
        }
        private void ShowGrid_Click(object sender, RoutedEventArgs e)
        {
            //if (!IsShowGrid)
            //{
            //    double GridPosY = -1;
            //    while (GridPosY <= brdDesign.ActualHeight)
            //    {
            //        double GridPosX = -1;
            //        while (GridPosX <= brdDesign.ActualWidth)
            //        {
            //            //Border BrdGridLine = new Border();
            //            //BrdGridLine.SetValue(Canvas.TopProperty, GridPosY);
            //            //BrdGridLine.SetValue(Canvas.LeftProperty, GridPosX);
            //            //BrdGridLine.Width = GridSize;
            //            //BrdGridLine.Height = GridSize;
            //            //BrdGridLine.BorderThickness = new Thickness(0, 0, 1, 1);
            //            //BrdGridLine.BorderBrush = new SolidColorBrush(Colors.Black);
            //            //BrdGridLine.
            //            //cnvDesign.Children.Add(BrdGridLine);
            //            Rectangle rec = new Rectangle();
            //            rec.Stroke = new SolidColorBrush(Colors.Black);
            //            rec.StrokeThickness = 1;
            //            rec.SetValue(Canvas.TopProperty, GridPosY);
            //            rec.SetValue(Canvas.LeftProperty, GridPosX);
            //            rec.Width = GridSize;
            //            rec.Height = GridSize;
            //            rec.StrokeDashArray = new DoubleCollection { 1.0, 10.0 };
            //            cnvDesign.Children.Add(rec);
            //            GridPosX += GridSize - 1;
            //        }
            //        GridPosY += GridSize - 1;
            //    }
            //    //double GridPos = GridSize;
            //    //while (GridPos <= brdDesign.ActualHeight - 5)
            //    //{
            //    //    Line GridLine = new Line();
            //    //    DoubleCollection DeshArray = new DoubleCollection();
            //    //    DeshArray.Add(1);
            //    //    DeshArray.Add(10);
            //    //    GridLine.X1 = 0;
            //    //    GridLine.X2 = brdDesign.ActualWidth;
            //    //    GridLine.Y1 =GridPos ;
            //    //    GridLine.Y2 =GridPos ;
            //    //    GridLine.Stroke = new SolidColorBrush(Colors.Black);
            //    //    GridLine.StrokeThickness = 1;

            //    //    //GridLine.StrokeDashArray = DeshArray;
            //    //    cnvDesign.Children.Add(GridLine);
            //    //    GridPos += GridSize;
            //    //}
            //    //GridPos = GridSize;
            //    //while (GridPos <= brdDesign.ActualWidth - 5)
            //    //{
            //    //    Line GridLine = new Line();
            //    //    DoubleCollection DeshArray = new DoubleCollection();
            //    //    DeshArray.Add(1);
            //    //    DeshArray.Add(10);
            //    //    GridLine.X1 = GridPos;
            //    //    GridLine.X2 = GridPos;
            //    //    GridLine.Y1 = 0;
            //    //    GridLine.Y2 = brdDesign.ActualHeight;
            //    //    GridLine.Stroke = new SolidColorBrush(Colors.Black);
            //    //    GridLine.StrokeThickness = 1;
            //    //    GridLine.StrokeDashArray = DeshArray;
            //    //    cnvDesign.Children.Add(GridLine);
            //    //    GridPos += GridSize;
            //    //}
            //    IsShowGrid = true;
            //}
            //else
            //{
            //    IsShowGrid = false;
            //}
        }
        #endregion
        #region "User"
        private void LoginPopwinOpen()
        {
            lblLoginMsg.Text = "";
            if (LayoutMain.ActualHeight > 0 && LayoutMain.ActualWidth > 0)
            {
                double wd = (LayoutMain.ActualWidth / 2) - 125;
                double ht = (LayoutMain.ActualHeight / 2) - 63;
                pwnLogin.SetValue(Canvas.LeftProperty, wd);
                pwnLogin.SetValue(Canvas.TopProperty, ht);
            }
            pwnLogin.IsOpened = true;

        }
        //private void btnLogin_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (txtUsername.Text != "" && txtPassword.Password != "")
        //        {
        //            UserServiceReference.UserServiceClient objSrv = new webprintDesigner.UserServiceReference.UserServiceClient();
        //            objSrv.UserLoginCompleted += new EventHandler<webprintDesigner.UserServiceReference.UserLoginCompletedEventArgs>(objSrv_UserLoginCompleted);
        //            objSrv.UserLoginAsync(txtUsername.Text, txtPassword.Password, App.DesignMode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::btnLogin_Click::" + ex.ToString());
        //    }
        //}

        //private void btnPopLogin_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (txtUsername2.Text != "" && txtPassword2.Password != "")
        //        {
        //            UserServiceReference.UserServiceClient objSrv = new webprintDesigner.UserServiceReference.UserServiceClient();
        //            objSrv.UserLoginCompleted += new EventHandler<webprintDesigner.UserServiceReference.UserLoginCompletedEventArgs>(objSrv_PopWinUserLoginCompleted);
        //            objSrv.UserLoginAsync(txtUsername2.Text, txtPassword2.Password, App.DesignerMode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::btnPopLogin_Click::" + ex.ToString());
        //    }
        //}

        private void btnPopCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pwnLogin.IsOpened = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::btnLogin_Click::" + ex.ToString());
            }
        }
        //private void AccordionImageItem_Selected(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        UserServiceReference.UserServiceClient objLognSrv = new webprintDesigner.UserServiceReference.UserServiceClient();
        //        objLognSrv.IsUserLoginedCompleted += new EventHandler<webprintDesigner.UserServiceReference.IsUserLoginedCompletedEventArgs>(objSrv_IsUserLoginedCompleted);
        //        objLognSrv.IsUserLoginedAsync(App.DesignerMode);

        //        if (IsUserLogin == false)
        //        {
        //            lstImagesList.Visibility = Visibility.Collapsed;
                    
        //            IsUserLogin = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::AccordionImageItem_Selected::" + ex.ToString());
        //    }
        //}


        #endregion
        #region "Images"

        //private void GetUserImages()
        //{
        //    try
        //    {
        //        UserServiceReference.UserServiceClient objSrv = new webprintDesigner.UserServiceReference.UserServiceClient();
        //        objSrv.GetUserImagesCompleted += new EventHandler<webprintDesigner.UserServiceReference.GetUserImagesCompletedEventArgs>(objSrv_GetUserImagesCompleted);
        //        objSrv.GetUserImagesAsync(App.DesignMode);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::GetUserImages::" + ex.ToString());
        //    }
        //}

        //private void lstBkImagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (App.DesignMode == 1 || App.DesignMode == 2)
        //        {
        //            if (lstBkImagesList.SelectedIndex != -1 && lstBkImagesList.SelectedItem != null)
        //            {
        //                if (objProduct.IsUsePDFFile)
        //                {
        //                    SelectedPage.BackgroundImageName = ((ProductServiceReference.TemplateBackgroundImages)lstBkImagesList.SelectedItem).BackgroundImageRelativePath;
        //                    ProductServiceReference.ProductServiceClient objSrvClient = new webprintDesigner.ProductServiceReference.ProductServiceClient();
        //                    objSrvClient.GetProductBackgroundImgCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetProductBackgroundImgCompletedEventArgs>(objSrvClient_GetProductBackgroundImgCompleted);
        //                    objSrvClient.GetProductBackgroundImgAsync(objProduct.ProductID, SelectedPage.BackgroundImageName, SelectedPage.IsBackSidePage, SelectedPage.PageNo);
        //                    DesignerDisabl2.Visibility = Visibility.Visible;
        //                    ProgressBar1.IsIndeterminate = true;
        //                    ProgessTxt.Text = "Generate Background";
        //                    ProgressPanel.Visibility = Visibility.Visible;
        //                }
        //                else
        //                {
        //                    System.Windows.Media.Imaging.BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(App.Current.Host.Source, ((ProductServiceReference.TemplateBackgroundImages)lstBkImagesList.SelectedItem).BackgroundImageRelativePath));
        //                    ImageBrush imgBk = new ImageBrush();
        //                    imgBk.Stretch = Stretch.Fill;
        //                    imgBk.ImageSource = bimg;
        //                    SelectedPage.brdDesign.Background = imgBk;
        //                    //SelectedPage.BackgroundImageName = ((ProductServiceReference.TemplateBackgroundImages)lstBkImagesList.SelectedItem).BackgroundImageRelativePath;
        //                    SelectedPage.BackgroundImageName = ((ProductServiceReference.TemplateBackgroundImages)lstBkImagesList.SelectedItem).ImageName;

        //                    //lstBkImagesList.SelectedItem = null;
        //                    //ddpBackgroundImages.IsOpened = false;
        //                }

                        
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::lstBkImagesList_SelectionChanged::" + ex.ToString());
        //    }
        //}


        ////private void UploadImages_Click(object sender, RoutedEventArgs e)
        ////{
        ////    try
        ////    {
        ////        IsResUsrLogin = false;
        ////        if (IsUserLogin == true)
        ////        {
        ////            UserServiceReference.UserServiceClient objLognSrv = new webprintDesigner.UserServiceReference.UserServiceClient();
        ////            objLognSrv.IsUserLoginedCompleted += new EventHandler<webprintDesigner.UserServiceReference.IsUserLoginedCompletedEventArgs>(objSrv_IsUserLoginedCompleted2);
        ////            objLognSrv.IsUserLoginedAsync(App.DesignMode);
        ////        }
        ////        OpenDialog();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        if (ShowException)
        ////            MessageBox.Show("::UploadImages_Click::" + ex.ToString());
        ////    }
        ////}

        ////void OpenDialog()
        ////{
        ////    try
        ////    {
        ////        if (App.DesignMode == 1 || App.DesignMode == 2)
        ////        {
        ////            if (IsUserLogin == true)
        ////            {
        ////                OpenFileDialog Ofd = new OpenFileDialog();
        ////                Ofd.Multiselect = false;
        ////                Ofd.Filter = "Image Files (*.jpg)|*.jpg|Png Image|*.png";
        ////                if ((bool)Ofd.ShowDialog())
        ////                {
        ////                    ProgressBar1.IsIndeterminate = true;
        ////                    ProgessTxt.Text = "Uploading Image...";
        ////                    progressSource = ProgressSource.UploadImage;
        ////                    ProgressPanel.Visibility = Visibility.Visible;
        ////                    Uri ur = new Uri(HtmlPage.Document.DocumentUri, UriPrefix + "Services/UserImageUploadHandler.ashx?ImgFileName=" + Ofd.File.Name + "&Mode=" + App.DesignMode);
        ////                    //MessageBox.Show(ur.ToString());
        ////                    WebClient objWeb = new WebClient();
        ////                    objWeb.OpenWriteCompleted += OnOpenWrite;
        ////                    //objWeb.UploadProgressChanged += objWeb_UploadProgressChanged;
        ////                    objWeb.OpenWriteAsync(ur, "POST", Ofd.File.OpenRead());
        ////                }
        ////            }
        ////            else
        ////                MessageBox.Show("Please login for upload images");
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        if (ShowException)
        ////            MessageBox.Show("::OpenDialog::" + ex.ToString());
        ////    }

        ////}
        ////void objWeb_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        ////{
        ////    try
        ////    {
        ////        // hdrtxt.Text = e.ProgressPercentage.ToString();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        if (ShowException)
        ////            MessageBox.Show("::objWeb_UploadProgressChanged::" + ex.ToString());
        ////    }

        ////}
        void OnOpenWrite(object sender, OpenWriteCompletedEventArgs e)
        {
            try
            {
                Stream inputStream = e.UserState as Stream;
                Stream outputStream = e.Result;
                byte[] buffer = new byte[4096];
                int byteRead = 0;
                while ((byteRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, byteRead);
                }
                outputStream.Close();
                inputStream.Close();
                //GetUserImages();
                //MessageBox.Show("Upload Completed");
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::OnOpenWrite::" + ex.ToString());
            }
        }
        private void ResetBkImages_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (objProduct.IsUsePDFFile)
                    {
                        SelectedPage.BackgroundImageName = "";
                        ProductServiceReference.ProductServiceClient objSrvClient = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                        objSrvClient.GetProductBackgroundImgCompleted += new EventHandler<webprintDesigner.ProductServiceReference.GetProductBackgroundImgCompletedEventArgs>(objSrvClient_GetProductBackgroundImgCompleted);
                        objSrvClient.GetProductBackgroundImgAsync(objProduct.ProductID, SelectedPage.BackgroundImageName, SelectedPage.IsBackSidePage, SelectedPage.PageNo);
                        DesignerDisabl2.Visibility = Visibility.Visible;
                        ProgressBar1.IsIndeterminate = true;
                        ProgessTxt.Text = "Generate Background";
                        ProgressPanel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        SelectedPage.brdDesign.Background = new SolidColorBrush(Colors.White);
                        SelectedPage.BackgroundImageName = "";
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ResetBkImages_Click::" + ex.ToString());
            }
        }
        #endregion



        #region "Color Picker"
        private void MoreColours_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ddpColor.IsOpened = false;
                pwnColor.IsOpened = true;

                ctlColorPicker.OnOkClick += new UserControls.ColorPicker.ColorPickerOkClick_EventHandler(ColorPicker_OnOkClick);

                foreach (UIElement el in SelectedPage.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer oc = (ObjectContainer)el;
                            if (oc.Selected && !oc.MouseDown){

                                ctlColorPicker.SetColor(oc.SelectedObect.ColorC, oc.SelectedObect.ColorM, oc.SelectedObect.ColorY, oc.SelectedObect.ColorK);
                                break;
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::MoreColours_Click::" + ex.ToString());
            }
        }
        private void CancelColour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ddpColor.IsOpened = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CancelColour_Click::" + ex.ToString());
            }
        }
        private void lstColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstColors.SelectedIndex != -1 && lstColors.SelectedItem != null && SelectedPage != null)
                {
                    ProductServiceReference.TemplateColorStyles objColorStyle = (ProductServiceReference.TemplateColorStyles)lstColors.SelectedItem;
                    if (objColorStyle.ColorHex != "")
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerColor(objColorStyle.ColorHex, objColorStyle.ColorC.Value, objColorStyle.ColorM.Value, objColorStyle.ColorY.Value, objColorStyle.ColorK.Value);
                            }
                        }
                        ddpColor.IsOpened = false;
                        lstColors.SelectedItem = null;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::lstColors_SelectionChanged::" + ex.ToString());
            }
        }

        protected void ColorPicker_OnOkClick(object sender, string ColorHex, int ColorC, int ColorM, int ColorY, int ColorK)
        {
            try
            {
                if (ColorHex != "" && SelectedPage != null)
                {
                    foreach (UIElement el in SelectedPage.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer oc = (ObjectContainer)el;
                            if (oc.Selected && !oc.MouseDown)
                                oc.UpdateContainerColor(ColorHex, ColorC, ColorM, ColorY, ColorK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ColorPicker_OnOkClick::" + ex.ToString());
            }
        }

        private void PopupWinMove(object sender, MouseEventArgs e)
        {
            try
            {
                PrintFlow.SilverlightControls.PopupWin c = (PrintFlow.SilverlightControls.PopupWin)sender;
                double X, Y;
                X = (e.GetPosition(LayoutMain).X - c.WinMousePoiner.X);
                Y = (e.GetPosition(LayoutMain).Y - c.WinMousePoiner.Y);
                c.SetValue(Canvas.LeftProperty, X);
                c.SetValue(Canvas.TopProperty, Y);
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::PopupWinMove::" + ex.ToString());
            }
        }
        private void PopupWinOpenClose(object sender, bool IsOpen)
        {
            try
            {
                if (IsOpen)
                    DesignerDisabl.Visibility = Visibility.Visible;
                else
                    DesignerDisabl.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::PopupWinOpenClose::" + ex.ToString());
            }
        }
        #endregion

        #region "Drag and Drop Images"
        private bool Dragabl = false;
        private Point ImgPoint;
        private int tmpidz = 1;
        private Storyboard DragImgeAnimSb = new Storyboard();
        private Storyboard DragLogoAnimSb = new Storyboard();
        private void lstImagesList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.OriginalSource.GetType().Name == "Image")
                {
                    Point DragPoint = e.GetPosition(LayoutMain);
                    Image img = e.OriginalSource as Image;
                    ImgPoint = e.GetPosition(img);
                    Image img2 = new Image();
                    img2.Source = img.Source;
                    img2.Height = img.ActualHeight;
                    img2.Width = img.ActualWidth;
                    img2.Name = "DADImg";
                    img2.MouseMove += DragImg_MouseMove;
                    LayoutMain.Children.Add(img2);
                    Image img3 = LayoutMain.FindName("DADImg") as Image;
                    //MessageBox.Show(ImgPoint.X.ToString());
                    //MessageBox.Show(DragPoint.X.ToString());

                    img3.SetValue(Canvas.LeftProperty, DragPoint.X - ImgPoint.X);
                    img3.SetValue(Canvas.TopProperty, DragPoint.Y - ImgPoint.Y);
                    img3.CaptureMouse();
                    Dragabl = true;

                    DragImgeAnimSb = new Storyboard();
                    DoubleAnimation DragImgeAnimX = new DoubleAnimation();
                    DoubleAnimation DragImgeAnimY = new DoubleAnimation();
                    DragImgeAnimX.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                    DragImgeAnimX.To = (DragPoint.X - ImgPoint.X);
                    DragImgeAnimY.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                    DragImgeAnimY.To = (DragPoint.Y - ImgPoint.Y);
                    Storyboard.SetTarget(DragImgeAnimX, img3);
                    Storyboard.SetTargetProperty(DragImgeAnimX, new PropertyPath(Canvas.LeftProperty));
                    Storyboard.SetTarget(DragImgeAnimY, img3);
                    Storyboard.SetTargetProperty(DragImgeAnimY, new PropertyPath(Canvas.TopProperty));
                    DragImgeAnimSb.Children.Add(DragImgeAnimX);
                    DragImgeAnimSb.Children.Add(DragImgeAnimY);
                    DragImgeAnimSb.Completed += new EventHandler(DragImgeAnimSb_Completed);

                    ddpImages.IsOpened = false;
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::lstImagesList_MouseLeftButtonDown::" + ex.ToString());
            }
        }


        //logo drag start capture
        void imgLogo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                
                    Point DragPoint = e.GetPosition(LayoutMain);
                    Image img = e.OriginalSource as Image;
                    ImgPoint = e.GetPosition(img);
                    Image img2 = new Image();
                    img2.Source = img.Source;
                    img2.Height = img.ActualHeight;
                    img2.Width = img.ActualWidth;
                    img2.Name = "DADLogo";
                    img2.MouseMove += DragLogo_MouseMove;
                    LayoutMain.Children.Add(img2);
                    Image img3 = LayoutMain.FindName("DADLogo") as Image;
                    //MessageBox.Show(ImgPoint.X.ToString());
                    //MessageBox.Show(DragPoint.X.ToString());

                    img3.SetValue(Canvas.LeftProperty, DragPoint.X - ImgPoint.X);
                    img3.SetValue(Canvas.TopProperty, DragPoint.Y - ImgPoint.Y);
                    img3.CaptureMouse();
                    DragablLogo = true;

                    DragLogoAnimSb = new Storyboard();
                    DoubleAnimation DragImgeAnimX = new DoubleAnimation();
                    DoubleAnimation DragImgeAnimY = new DoubleAnimation();
                    DragImgeAnimX.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                    DragImgeAnimX.To = (DragPoint.X - ImgPoint.X);
                    DragImgeAnimY.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                    DragImgeAnimY.To = (DragPoint.Y - ImgPoint.Y);
                    Storyboard.SetTarget(DragImgeAnimX, img3);
                    Storyboard.SetTargetProperty(DragImgeAnimX, new PropertyPath(Canvas.LeftProperty));
                    Storyboard.SetTarget(DragImgeAnimY, img3);
                    Storyboard.SetTargetProperty(DragImgeAnimY, new PropertyPath(Canvas.TopProperty));
                    DragLogoAnimSb.Children.Add(DragImgeAnimX);
                    DragLogoAnimSb.Children.Add(DragImgeAnimY);
                    DragLogoAnimSb.Completed += new EventHandler(DragLogoAnimSb_Completed);

                    ddpLogo.IsOpened = false;
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::lstImagesList_MouseLeftButtonDown::" + ex.ToString());
            }
        }


        
        //image
        void DragImgeAnimSb_Completed(object sender, EventArgs e)
        {
            try
            {
                Image img = LayoutMain.FindName("DADImg") as Image;
                if (img != null)
                {
                    LayoutMain.Children.Remove(img);
                }
                Dragabl = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::DragImgeAnimSb_Completed::" + ex.ToString());
            }
        }

        //logo
        void DragLogoAnimSb_Completed(object sender, EventArgs e)
        {
            try
            {
                Image img = LayoutMain.FindName("DADLogo") as Image;
                if (img != null)
                {
                    LayoutMain.Children.Remove(img);
                }
                DragablLogo = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::DragLogoAnimSb_Completed::" + ex.ToString());
            }
        }

        //image
        void DragImg_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (Dragabl && sender.GetType().Name == "Image")
                {
                    Image img = sender as Image;
                    img.SetValue(Canvas.LeftProperty, e.GetPosition(LayoutMain).X - ImgPoint.X);
                    img.SetValue(Canvas.TopProperty, e.GetPosition(LayoutMain).Y - ImgPoint.Y);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::DragImg_MouseMove::" + ex.ToString());
            }
        }


        //logo
        void DragLogo_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (DragablLogo && sender.GetType().Name == "Image")
                {
                    Image img = sender as Image;
                    img.SetValue(Canvas.LeftProperty, e.GetPosition(LayoutMain).X - ImgPoint.X);
                    img.SetValue(Canvas.TopProperty, e.GetPosition(LayoutMain).Y - ImgPoint.Y);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::DragImg_MouseMove::" + ex.ToString());
            }
        }

        private void LayoutMain_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image img = LayoutMain.FindName("DADImg") as Image;
                if (img != null && Dragabl && SelectedPage != null)
                {
                    bool ImgAdd = false;
                    double DsnX = e.GetPosition(SelectedPage.brdDesign).X - SelectedPage.scvDesign.HorizontalOffset;
                    double DsnY = e.GetPosition(SelectedPage.brdDesign).Y - SelectedPage.scvDesign.VerticalOffset;
                    if (DsnX >= 0 && DsnY >= 0)
                    {
                        if (DsnX < (SelectedPage.brdDesign.ActualWidth - SelectedPage.scvDesign.ScrollableWidth) && DsnY < (SelectedPage.brdDesign.ActualHeight - SelectedPage.scvDesign.ScrollableHeight))
                        {
                            if (lstImagesList.SelectedIndex != -1)
                            {
                                webprintDesigner.ProductServiceReference.TemplateBackgroundImages uimg = (webprintDesigner.ProductServiceReference.TemplateBackgroundImages)lstImagesList.SelectedItem;

                                //if (Convert.ToDouble(uimg.ImageWidth) >= SelectedPage.DesignArea.Width || Convert.ToDouble(uimg.ImageHeight) >= SelectedPage.DesignArea.Height)
                                //{
                                //    //if (uimg.ImageAbsolutePath.IndexOf("UserData") != -1)
                                //    //{
                                //    ImgPoint.X = e.GetPosition(SelectedPage.DesignArea).X - ImgPoint.X;
                                //    ImgPoint.Y = e.GetPosition(SelectedPage.DesignArea).Y - ImgPoint.Y;
                                //    //MessageBox.Show(uimg.ImageSource.Substring(uimg.ImageSource.IndexOf("UserData"), uimg.ImageSource.Length - uimg.ImageSource.IndexOf("UserData")));
                                //    ctlCropImage.ImgName = uimg.BackgroundImageRelativePath;// uimg.ImageAbsolutePath.Substring(uimg.ImageAbsolutePath.IndexOf("UserData"), uimg.ImageAbsolutePath.Length - uimg.ImageAbsolutePath.IndexOf("UserData"));
                                //    ctlCropImage.ImgSize = new Size(Convert.ToDouble(uimg.ImageWidth), Convert.ToDouble(uimg.ImageHeight));
                                //    ctlCropImage.MaxSize = new Size(SelectedPage.DesignArea.ActualWidth, SelectedPage.DesignArea.ActualHeight);
                                //    ctlCropImage.ImgSource = img.Source;

                                //    pwnCrop.IsOpened = true;
                                //    ctlCropImage.ShowException = ShowException;
                                //    ctlCropImage.OnCropClick += new UserControls.CropImage.CropImageCropClick_EventHandler(ctlCropImage_OnCropClick);

                                //    //ctlCropImage.BindUsrSrv = BindUsrSrv;
                                //    //ctlCropImage.EPUsrSrv = EPUsrSrv;
                                //    //}
                                //    LayoutMain.Children.Remove(img);
                                //    ImgAdd = true;
                                //    //MessageBox.Show("This image can't allow to add.(crop func under construction)");
                                //}
                                //else
                                //{
                                Point ImgPos = new Point(e.GetPosition(SelectedPage.DesignArea).X - ImgPoint.X, e.GetPosition(SelectedPage.DesignArea).Y - ImgPoint.Y);

                                
                                Size ImgSize = new Size( Common.PixelToPoint(Convert.ToDouble(uimg.ImageWidth)), Common.PixelToPoint(Convert.ToDouble(uimg.ImageHeight)));

                                //resize to height with aspect
                                double ratio = ImgSize.Width / ImgSize.Height;
                                if (ImgSize.Height > SelectedPage.DesignArea.ActualHeight && ImgSize.Width > SelectedPage.DesignArea.ActualWidth)
                                {
                                    ImgSize.Height = SelectedPage.DesignArea.ActualHeight;
                                    ImgSize.Width = ImgSize.Height * ratio;
                                }
                                else if (ImgSize.Height > SelectedPage.DesignArea.ActualHeight)
                                {
                                    ImgSize.Height = SelectedPage.DesignArea.ActualHeight;
                                    ImgSize.Width = ImgSize.Height * ratio;
                                }
                                else if (ImgSize.Width > SelectedPage.DesignArea.ActualWidth )
                                {
                                    ImgSize.Width = SelectedPage.DesignArea.ActualWidth;
                                    ImgSize.Height = ImgSize.Width / ratio;
                                }
                                

                                webprintDesigner.ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
                                objObject.ObjectID = 0;
                                objObject.ParentId = 0;
                                objObject.ObjectType = 3;
                                objObject.PositionX = ImgPos.X;
                                objObject.PositionY = ImgPos.Y;
                                objObject.MaxWidth = ImgSize.Width;
                                objObject.MaxHeight = ImgSize.Height;
                               
                                objObject.RotationAngle = 0;
                                objObject.Name = "";


                                objObject.IsEditable = true;
                                objObject.IsPositionLocked = false;
                                objObject.IsHidden = false;

                                objObject.ContentString = uimg.BackgroundImageRelativePath;
                                objObject.DisplayOrderTxtControl = 0;
                                objObject.IsMandatory = false;
                                objObject.IsRequireNumericValue = false;
                              
                                    objObject.DisplayOrderPdf = 0;

                                objObject.isSide2Object = false;
                                objObject.Tint = 0;
                                objObject.IsNewLine = false;
                                objObject.OffsetX = 0;
                                objObject.OffsetY = 0;
                                objObject.VAllignment = 0;
                                objObject.Allignment = 0;

                                objObject.ColorName = string.Empty;
                                objObject.ExField1 = string.Empty;
                                objObject.ExField2 = string.Empty;
                                objObject.SpotColorName = string.Empty;
                                objObject.FontName = string.Empty;
                                objObject.FontSize = 0;
                                objObject.FontStyleID = 0;
                                objObject.IsFontCustom = false;
                                objObject.IsFontNamePrivate = false;
                                objObject.TCtlName = string.Empty;
                                objObject.PageNo = 1;





                                SelectedPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), -1, img.Source, uimg.BackgroundImageRelativePath, ImgPos, ImgSize, objObject,false);
                                    CtlIdx++;
                                    LayoutMain.Children.Remove(img);
                                    ImgAdd = true;
                                //}
                            }
                        }
                    }
                    if (!ImgAdd)
                    {
                        DragImgeAnimSb.Begin();
                    }
                    //MessageBox.Show(e.OriginalSource.GetType().Name);
                }

                    //logo drag end here
                else if (DragablLogo)
                {
                    Image logo = LayoutMain.FindName("DADLogo") as Image;
                    bool LogoAdd = false;
                    double DsnX = e.GetPosition(SelectedPage.brdDesign).X - SelectedPage.scvDesign.HorizontalOffset;
                    double DsnY = e.GetPosition(SelectedPage.brdDesign).Y - SelectedPage.scvDesign.VerticalOffset;
                    if (DsnX >= 0 && DsnY >= 0)
                    {
                        if (DsnX < (SelectedPage.brdDesign.ActualWidth - SelectedPage.scvDesign.ScrollableWidth) && DsnY < (SelectedPage.brdDesign.ActualHeight - SelectedPage.scvDesign.ScrollableHeight))
                        {
                            
                                Point ImgPos = new Point(e.GetPosition(SelectedPage.DesignArea).X - ImgPoint.X, e.GetPosition(SelectedPage.DesignArea).Y - ImgPoint.Y);


                                Size ImgSize = new Size(Common.PixelToPoint(Convert.ToDouble(logo.Width)), Common.PixelToPoint(Convert.ToDouble(logo.Height)));

                                webprintDesigner.ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
                                objObject.ObjectID = 0;
                                objObject.ParentId = 0;
                                objObject.ObjectType = 3;
                                objObject.PositionX = ImgPos.X;
                                objObject.PositionY = ImgPos.Y;
                                objObject.MaxWidth = ImgSize.Width;
                                objObject.MaxHeight = ImgSize.Height;

                                objObject.RotationAngle = 0;
                                objObject.Name = "";


                                objObject.IsEditable = true;
                                objObject.IsPositionLocked = false;
                                objObject.IsHidden = false;

                                objObject.ContentString =  (logo.Source as BitmapImage).UriSource.LocalPath;
                                objObject.DisplayOrderTxtControl = 0;
                                objObject.IsMandatory = false;
                                objObject.IsRequireNumericValue = false;

                                objObject.DisplayOrderPdf = 0;

                                objObject.isSide2Object = false;
                                objObject.Tint = 0;
                                objObject.IsNewLine = false;
                                objObject.OffsetX = 0;
                                objObject.OffsetY = 0;
                                objObject.VAllignment = 0;
                                objObject.Allignment = 0;

                                objObject.ColorName = string.Empty;
                                objObject.ExField1 = string.Empty;
                                objObject.ExField2 = string.Empty;
                                objObject.SpotColorName = string.Empty;
                                objObject.FontName = string.Empty;
                                objObject.FontSize = 0;
                                objObject.FontStyleID = 0;
                                objObject.IsFontCustom = false;
                                objObject.IsFontNamePrivate = false;
                                objObject.TCtlName = string.Empty;
                                objObject.PageNo = 1;





                                SelectedPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), -1, logo.Source, objObject.ContentString, ImgPos, ImgSize, objObject,true);
                                CtlIdx++;
                                LayoutMain.Children.Remove(logo);
                                LogoAdd = true;
                                //}
                            }
                        }

                    if (!LogoAdd)
                    {
                        DragLogoAnimSb.Begin();
                    }

                }

                else if (DragablText)
                {
                    TextBlock oTB = LayoutMain.FindName("DADText") as TextBlock;
                    if (oTB != null && SelectedPage != null)
                    {
                        bool TextAdd = false;
                        double DsnX = e.GetPosition(SelectedPage.brdDesign).X - SelectedPage.scvDesign.HorizontalOffset;
                        double DsnY = e.GetPosition(SelectedPage.brdDesign).Y - SelectedPage.scvDesign.VerticalOffset;
                        if (DsnX >= 0 && DsnY >= 0)
                        {
                            if (DsnX < (SelectedPage.brdDesign.ActualWidth - SelectedPage.scvDesign.ScrollableWidth) && DsnY < (SelectedPage.brdDesign.ActualHeight - SelectedPage.scvDesign.ScrollableHeight))
                            {
                                QuickText oQuickText = null;
                                if (DictionaryManager.AppObjects.ContainsKey("QuickText") && DictionaryManager.AppObjects["QuickText"] != null)
                                    oQuickText = (QuickText)DictionaryManager.AppObjects["QuickText"];
                                else
                                {
                                    oQuickText = new QuickText();

                                    oQuickText.Company = "Your Company Name";
                                    oQuickText.CompanyMessage = "Your Company Message";
                                    oQuickText.Name = "Your Name";
                                    oQuickText.Title = "Your Title";
                                    oQuickText.Address1 = "Address Line 1";
                                    oQuickText.Address2 = "Address Line 2";
                                    oQuickText.Address3 = "Address Line 3";
                                    oQuickText.Telephone = "Telephone / Other";
                                    oQuickText.Fax = "Fax / Other";
                                    oQuickText.Email = "Email address / Other";
                                    oQuickText.Website = "Website address";

                                    DictionaryManager.AppObjects["QuickText"] = oQuickText;
                                }

                                Point TextPos = new Point(e.GetPosition(SelectedPage.DesignArea).X - TBPoint.X, e.GetPosition(SelectedPage.DesignArea).Y - TBPoint.Y);

                                if (oTB.Tag.ToString() != "All")
                                {


                                    switch (oTB.Tag.ToString())
                                    {
                                        case "CompanyName":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Company, TextPos);
                                                break;
                                            }
                                        case "CompanyMessage":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.CompanyMessage, TextPos);
                                                break;
                                            }
                                        case "Name":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Name, TextPos);
                                                break;
                                            }
                                        case "Title":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Title, TextPos);
                                                break;
                                            }
                                        case "AddressLine1":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Address1, TextPos);
                                                break;
                                            }
                                        case "AddressLine2":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Address2, TextPos);
                                                break;
                                            }
                                        case "AddressLine3":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Address3, TextPos);
                                                break;
                                            }
                                        case "Phone":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Telephone, TextPos);
                                                break;
                                            }
                                        case "Fax":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Fax, TextPos);
                                                break;
                                            }
                                        case "Email":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Email, TextPos);
                                                break;
                                            }
                                        case "Website":
                                            {
                                                AddTextObject(oTB.Tag.ToString(), oQuickText.Website, TextPos);
                                                break;
                                            }
                                    }


                                }
                                else
                                {


                                    AddTextObject("CompanyName", oQuickText.Company, new Point(TextPos.X, TextPos.Y));
                                    AddTextObject("CompanyMessage", oQuickText.CompanyMessage, new Point(TextPos.X, TextPos.Y + 15));
                                    AddTextObject("Name", oQuickText.Name, new Point(TextPos.X, TextPos.Y + 30));
                                    AddTextObject("Title", oQuickText.Title, new Point(TextPos.X, TextPos.Y + 45));
                                    AddTextObject("AddressLine1", oQuickText.Address1, new Point(TextPos.X, TextPos.Y + 60));
                                    //AddTextObject("AddressLine2", oQuickText.Address2, new Point(TextPos.X, TextPos.Y + 75));
                                    //AddTextObject("AddressLine3", oQuickText.Address3, new Point(TextPos.X, TextPos.Y + 90));
                                    AddTextObject("Phone", oQuickText.Telephone, new Point(TextPos.X, TextPos.Y + 105));
                                    AddTextObject("Fax", oQuickText.Fax, new Point(TextPos.X, TextPos.Y + 120));
                                    AddTextObject("Email", oQuickText.Email, new Point(TextPos.X, TextPos.Y + 135));
                                    AddTextObject("Website", oQuickText.Website, new Point(TextPos.X, TextPos.Y + 150));

                                }
                                LayoutMain.Children.Remove(oTB);
                                CtlIdx++;
                                TextAdd = true;
                            }
                        }
                        if (!TextAdd)
                        {
                            DragTextAnimSb.Begin();
                        }
                    }

                }

                DragablLogo = false;
                DragablText = false;
                Dragabl = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::LayoutMain_MouseLeftButtonUp::" + ex.ToString());
            }
        }

        private Point TBPoint;
        bool DragablText = false;
        private Storyboard DragTextAnimSb = new Storyboard();
        private void lblField_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.OriginalSource.GetType().Name == "TextBlock")
                {
                    if (SelectedPage != null)
                    {
                        SelectedPage.UnSelAllObject();
                    }

                    Point DragPoint = e.GetPosition(LayoutMain);
                    TextBlock oTB = e.OriginalSource as TextBlock;
                    TBPoint = e.GetPosition(oTB);

                    TextBlock oTB2 = new TextBlock();
                    oTB2.Tag = oTB.Tag;
                    oTB2.Text = oTB.Text;
                    oTB2.Height = oTB.ActualHeight;
                    oTB2.Width = oTB.ActualWidth;
                    oTB2.Name = "DADText";
                    oTB2.MouseMove += DragText_MouseMove;
                    LayoutMain.Children.Add(oTB2);

                    oTB2.SetValue(Canvas.LeftProperty, DragPoint.X - TBPoint.X);
                    oTB2.SetValue(Canvas.TopProperty, DragPoint.Y - TBPoint.Y);
                    oTB2.CaptureMouse();
                    DragablText = true;

                    DragTextAnimSb = new Storyboard();
                    DoubleAnimation DragImgeAnimX = new DoubleAnimation();
                    DoubleAnimation DragImgeAnimY = new DoubleAnimation();
                    DragImgeAnimX.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                    DragImgeAnimX.To = (DragPoint.X - TBPoint.X);
                    DragImgeAnimY.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                    DragImgeAnimY.To = (DragPoint.Y - TBPoint.Y);
                    Storyboard.SetTarget(DragImgeAnimX, oTB2);
                    Storyboard.SetTargetProperty(DragImgeAnimX, new PropertyPath(Canvas.LeftProperty));
                    Storyboard.SetTarget(DragImgeAnimY, oTB2);
                    Storyboard.SetTargetProperty(DragImgeAnimY, new PropertyPath(Canvas.TopProperty));
                    DragTextAnimSb.Children.Add(DragImgeAnimX);
                    DragTextAnimSb.Children.Add(DragImgeAnimY);
                    DragTextAnimSb.Completed += new EventHandler(DragTextAnimSb_Completed);


                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::lblField_MouseLeftButtonDown::" + ex.ToString());
            }
        }

        void DragText_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (DragablText && sender.GetType().Name == "TextBlock")
                {
                    TextBlock oTB = sender as TextBlock;
                    oTB.SetValue(Canvas.LeftProperty, e.GetPosition(LayoutMain).X - TBPoint.X);
                    oTB.SetValue(Canvas.TopProperty, e.GetPosition(LayoutMain).Y - TBPoint.Y);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::DragImg_MouseMove::" + ex.ToString());
            }
        }

        void DragTextAnimSb_Completed(object sender, EventArgs e)
        {
            try
            {
                TextBlock oTB = LayoutMain.FindName("DADText") as TextBlock;
                if (oTB != null)
                {
                    LayoutMain.Children.Remove(oTB);
                }
                DragablText = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::DragImgeAnimSb_Completed::" + ex.ToString());
            }
        }


        private void AddTextObject(string Name, string Content, Point oTextPosition)
        {
            try
            {

                if (SelectedPage != null) //&& SelectedTxtCtls != null
                {
                    int OType = 1;
                    if (rdoSText.IsChecked == true) OType = 1;
                    else if (rdoMText.IsChecked == true) OType = 2;
                    else if (rdoLable.IsChecked == true) OType = 4;
                    if (SelectedPage != null) //&& SelectedTxtCtls != null
                    {
                        webprintDesigner.ProductServiceReference.TemplateFonts cmbFontItm = new webprintDesigner.ProductServiceReference.TemplateFonts();
                        //if (cmbFontList.SelectedIndex != -1)
                        //{
                            foreach (webprintDesigner.ProductServiceReference.TemplateFonts item in cmbFontList.Items)
                            {
                                if (item.FontDisplayName == "Trebuchet MS")
                                {
                                    cmbFontItm = item;
                                    break;
                                }
                            }

                            //cmbFontItm = ((webprintDesigner.ProductServiceReference.TemplateFonts)cmbFontList.Items). .SelectedItem;
                        //}
                        if (txtFontSize.Value == 0)
                        {
                            txtFontSize.Value = 12;
                        }

                        ObjectContainer SelOc = null;
                        if (App.DesignerMode == DesignerModes.CreatorMode)
                        {
                            if (!SelObjPro && SelectedPage != null)
                            {

                                foreach (UIElement el in SelectedPage.DesignArea.Children)
                                {
                                    if (el.GetType().Name == "ObjectContainer")
                                    {
                                        ObjectContainer oc = (ObjectContainer)el;
                                        if (oc.Selected && !oc.MouseDown)
                                        {
                                            SelOc = oc;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        #region Object
                        ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
                        objObject.ObjectType = OType;
                        objObject.ColorC = 0;
                        objObject.ColorM = 100;
                        objObject.ColorY = 100;
                        objObject.ColorK = 0;
                        objObject.Allignment = 1;
                        objObject.ColorStyleID = 0;
                        objObject.ColorType = 3;
                        objObject.PositionX = oTextPosition.X;
                        objObject.PositionY = oTextPosition.Y;
                        objObject.MaxWidth = 100;
                        objObject.MaxHeight = 15;
                        objObject.RotationAngle = 0;

                        objObject.Name = Name;


                        objObject.ContentString = Content;
                        if (cmbFontItm != null)
                        {
                            objObject.FontName = cmbFontItm.FontDisplayName;
                            objObject.IsFontNamePrivate = cmbFontItm.IsPrivateFont;
                        }
                        else
                        {
                            objObject.FontName = "";
                            objObject.IsFontNamePrivate = false;
                        }
                        objObject.FontSize = txtFontSize.Value;
                        objObject.IsBold = false;
                        objObject.IsItalic = false;
                        objObject.IsUnderlinedText = false;


                        objObject.MaxCharacters = 0;
                        objObject.IsFontCustom = true;
                        objObject.PageNo = 1;
                        //objObject.SpaceAfter = 0;
                        objObject.DisplayOrderPdf = 0;
                        objObject.DisplayOrderTxtControl = 0;
                        objObject.OffsetX = 0;
                        objObject.OffsetY = 0;
                        objObject.ColorName = string.Empty;
                        objObject.ExField1 = string.Empty;
                        objObject.ExField2 = string.Empty;
                        objObject.SpotColorName = string.Empty;



                        objObject.IsEditable = true;
                        objObject.IsPositionLocked = false;
                        objObject.IsHidden = false;

                        objObject.IsNewLine = false;
                        #endregion
                        if (App.DesignerMode == DesignerModes.CreatorMode && SelOc != null)
                        {
                            if (SelOc.SelContainerPanel != null)
                            {
                                objObject.ParentId = 0;
                                objObject.IsNewLine = (bool)chkControlNewLine.IsChecked;
                                objObject.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                                bool bl = SelOc.AddChildCtrol(objObject, cmbFontItm);

                                //SelectedTxtCtls.AddControls(SelOc.ContainerName, "CtlTxtContent_" + CtlIdx.ToString(), txtObjectName.Text, txtObjectName.Text, OType);
                                //SelectedTxtCtls.UpdateCtrlsList();
                                CtlIdx++;
                                // SelOc.UnSelectContainer();
                                SelOc.SelectContainer(objObject.TCtlName, new Point(0, 0), new Point(0, 0));


                            }
                        }
                        else
                        {
                            objObject.ParentId = 0;
                            objObject.TCtlName = "CtlTxtContent_" + CtlIdx.ToString();
                            SelectedPage.AddObject("CtlContainerTxt_" + CtlIdx.ToString(), "CtlTxtContent_" + CtlIdx.ToString(), txtObjectName.Text, txtObjectName.Text, OType, cmbFontItm, webprintDesigner.Common.PointToPixel(txtFontSize.Value), objObject, oTextPosition);
                            //SelectedTxtCtls.AddControls("CtlContainerTxt_" + CtlIdx.ToString(), "CtlTxtContent_" + CtlIdx.ToString(), txtObjectName.Text, txtObjectName.Text, OType);
                            //SelectedTxtCtls.UpdateCtrlsList();
                            CtlIdx++;
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("Please enter name");
                }

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddTxtObject_Click::" + ex.ToString());
            }
        }

        //original function for user images..
        //private void LayoutMain_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        Image img = LayoutMain.FindName("DADImg") as Image;
        //        if (img != null && Dragabl && SelectedPage != null)
        //        {
        //            bool ImgAdd = false;
        //            double DsnX = e.GetPosition(SelectedPage.brdDesign).X - SelectedPage.scvDesign.HorizontalOffset;
        //            double DsnY = e.GetPosition(SelectedPage.brdDesign).Y - SelectedPage.scvDesign.VerticalOffset;
        //            if (DsnX >= 0 && DsnY >= 0)
        //            {
        //                if (DsnX < (SelectedPage.brdDesign.ActualWidth - SelectedPage.scvDesign.ScrollableWidth) && DsnY < (SelectedPage.brdDesign.ActualHeight - SelectedPage.scvDesign.ScrollableHeight))
        //                {
        //                    if (lstImagesList.SelectedIndex != -1)
        //                    {
        //                        webprintDesigner.UserServiceReference.UserImages uimg = (webprintDesigner.UserServiceReference.UserImages)lstImagesList.SelectedItem;
        //                        if (Convert.ToDouble(uimg.ImageWidth) >= SelectedPage.DesignArea.Width || Convert.ToDouble(uimg.ImageHeight) >= SelectedPage.DesignArea.Height)
        //                        {
        //                            if (uimg.ImageAbsolutePath.IndexOf("UserData") != -1)
        //                            {
        //                                ImgPoint.X = e.GetPosition(SelectedPage.DesignArea).X - ImgPoint.X;
        //                                ImgPoint.Y = e.GetPosition(SelectedPage.DesignArea).Y - ImgPoint.Y;
        //                                //MessageBox.Show(uimg.ImageSource.Substring(uimg.ImageSource.IndexOf("UserData"), uimg.ImageSource.Length - uimg.ImageSource.IndexOf("UserData")));
        //                                ctlCropImage.ImgName = uimg.ImageRelativePath;// uimg.ImageAbsolutePath.Substring(uimg.ImageAbsolutePath.IndexOf("UserData"), uimg.ImageAbsolutePath.Length - uimg.ImageAbsolutePath.IndexOf("UserData"));
        //                                ctlCropImage.ImgSize = new Size(Convert.ToDouble(uimg.ImageWidth), Convert.ToDouble(uimg.ImageHeight));
        //                                ctlCropImage.MaxSize = new Size(SelectedPage.DesignArea.ActualWidth, SelectedPage.DesignArea.ActualHeight);
        //                                ctlCropImage.ImgSource = img.Source;
                                        
        //                                pwnCrop.IsOpened = true;
        //                                ctlCropImage.ShowException = ShowException;
        //                                ctlCropImage.OnCropClick +=new UserControls.CropImage.CropImageCropClick_EventHandler(ctlCropImage_OnCropClick);

        //                                //ctlCropImage.BindUsrSrv = BindUsrSrv;
        //                                //ctlCropImage.EPUsrSrv = EPUsrSrv;
        //                            }
        //                            LayoutMain.Children.Remove(img);
        //                            ImgAdd = true;
        //                            //MessageBox.Show("This image can't allow to add.(crop func under construction)");
        //                        }
        //                        else
        //                        {
        //                            Point ImgPos = new Point(e.GetPosition(SelectedPage.DesignArea).X - ImgPoint.X, e.GetPosition(SelectedPage.DesignArea).Y - ImgPoint.Y);
        //                            Size ImgSize = new Size(Convert.ToDouble(uimg.ImageWidth), Convert.ToDouble(uimg.ImageHeight));
        //                            SelectedPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), -1, img.Source, uimg.ImageRelativePath.Substring(1), ImgPos, ImgSize);
        //                            CtlIdx++;
        //                            LayoutMain.Children.Remove(img);
        //                            ImgAdd = true;
        //                        }
        //                    }
        //                }
        //            }
        //            if (!ImgAdd)
        //            {
        //                DragImgeAnimSb.Begin();
        //            }
        //            //MessageBox.Show(e.OriginalSource.GetType().Name);
        //        }
        //        Dragabl = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::LayoutMain_MouseLeftButtonUp::" + ex.ToString());
        //    }
        //}

       
        #endregion
        #region "Zooming"

      

        private void RadSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (SelectedPage != null)
                {

                   
                    SelectedPage.ZoomAnimationX.To = e.NewValue;
                    SelectedPage.ZoomAnimationY.To = e.NewValue;
                    SelectedPage.ZoomStoryboard.Begin();

                    SelectedPage.flexCanvas.Width = (SelectedPage.DesignArea.ActualWidth + 90) * e.NewValue;
                    SelectedPage.flexCanvas.Height = (SelectedPage.DesignArea.ActualHeight + 50) * e.NewValue;


                    

                    zoomState = ZoomState.Free;
                }

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ZoomReset_Click::" + ex.ToString());
            }
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            ZoomSlider.Value += 0.25;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            ZoomSlider.Value -= 0.25;
        }


        private void btnScroll_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPage != null)
            {

                Button oBtn = (Button)sender;
                switch (oBtn.Tag.ToString())
                {
                    case "Left": SelectedPage.scvDesign.ScrollToHorizontalOffset(SelectedPage.scvDesign.HorizontalOffset - 30); break;
                    case "Up": SelectedPage.scvDesign.ScrollToVerticalOffset(SelectedPage.scvDesign.VerticalOffset - 30); break;
                    case "Right": SelectedPage.scvDesign.ScrollToHorizontalOffset(SelectedPage.scvDesign.HorizontalOffset + 30); break;
                    case "Down": SelectedPage.scvDesign.ScrollToVerticalOffset(SelectedPage.scvDesign.VerticalOffset + 30); break;
                    default:
                        break;
                }
            }
        }
      

     

        
        #endregion
        #region "Object Index"


        //bring object to the top most layer
        private void BringToFront_Click(object sender, RoutedEventArgs e)
        {
            ObjectContainer SelOc = null;
            try
            {
                if (SelectedPage != null)
                {
                    foreach (UIElement el in SelectedPage.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer oc = (ObjectContainer)el;
                            if (oc.Selected && !oc.MouseDown)
                            {
                                SelOc = oc;
                                break;
                            }
                        }
                    }

                    if (SelOc != null)
                    {
                        int SelZIdx = (int)SelOc.GetValue(Canvas.ZIndexProperty);

                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (!oc.Selected)
                                {
                                    int tZIdx = (int)oc.GetValue(Canvas.ZIndexProperty);
                                    if (tZIdx > SelZIdx)
                                    {
                                        oc.SetValue(Canvas.ZIndexProperty, tZIdx - 1);
                                    }
                                }
                            }
                        }
                        SelOc.SetValue(Canvas.ZIndexProperty, SelectedPage.GetMaxZidx());
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::BringToFront_Click::" + ex.ToString());
            }
        }
        private void BringForward_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedPage != null)
                {
                    ObjectContainer SelOc = null;
                    foreach (UIElement el in SelectedPage.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer oc = (ObjectContainer)el;
                            if (oc.Selected && !oc.MouseDown)
                            {
                                SelOc = oc;
                                break;
                            }
                        }
                    }
                    if (SelOc != null)
                    {
                        int SelZIdx = (int)SelOc.GetValue(Canvas.ZIndexProperty);
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (!oc.Selected)
                                {
                                    int tZIdx = (int)oc.GetValue(Canvas.ZIndexProperty);
                                    if (tZIdx == (SelZIdx + 1))
                                    {
                                        oc.SetValue(Canvas.ZIndexProperty, SelZIdx);
                                        SelOc.SetValue(Canvas.ZIndexProperty, SelZIdx + 1);
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::BringForward_Click::" + ex.ToString());
            }
        }
        private void SendBackward_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedPage != null)
                {
                    ObjectContainer SelOc = null;
                    foreach (UIElement el in SelectedPage.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer oc = (ObjectContainer)el;
                            if (oc.Selected && !oc.MouseDown)
                            {
                                SelOc = oc;
                                break;
                            }
                        }
                    }
                    if (SelOc != null)
                    {
                        int SelZIdx = (int)SelOc.GetValue(Canvas.ZIndexProperty);
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (!oc.Selected)
                                {
                                    int tZIdx = (int)oc.GetValue(Canvas.ZIndexProperty);
                                    if (tZIdx == (SelZIdx - 1))
                                    {
                                        oc.SetValue(Canvas.ZIndexProperty, SelZIdx);
                                        SelOc.SetValue(Canvas.ZIndexProperty, SelZIdx - 1);
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::SendBackward_Click::" + ex.ToString());
            }
        }
        private void SendToBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedPage != null)
                {
                    ObjectContainer SelOc = null;
                    foreach (UIElement el in SelectedPage.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer oc = (ObjectContainer)el;
                            if (oc.Selected && !oc.MouseDown)
                            {
                                SelOc = oc;
                                break;
                            }
                        }
                    }
                    if (SelOc != null)
                    {
                        int SelZIdx = (int)SelOc.GetValue(Canvas.ZIndexProperty);
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (!oc.Selected)
                                {
                                    int tZIdx = (int)oc.GetValue(Canvas.ZIndexProperty);
                                    if (tZIdx < SelZIdx)
                                        oc.SetValue(Canvas.ZIndexProperty, tZIdx + 1);
                                }
                            }
                        }
                        SelOc.SetValue(Canvas.ZIndexProperty, 100);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::SendToBack_Click::" + ex.ToString());
            }
        }
        #endregion


        //private void scvDesign_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        ScrollBar hBar = ((FrameworkElement)VisualTreeHelper.GetChild(scvDesign, 0)).FindName("VerticalScrollBar") as System.Windows.Controls.Primitives.ScrollBar;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::scvDesign_GotFocus::" + ex.ToString());
        //    }
        //}


        
        private System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateObjects> GetPageObjects(PageContainer objPage, bool ObjectSide, System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateObjects> ObjectsList, ref int ObjIndx)
        {
            PageTxtControl objPTxtCtls = objPage.objPageTxtControl;
            try
            {
                foreach (UIElement el in objPage.DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer oc = (ObjectContainer)el;

                        if (oc.ContainerType ==  ObjectContainer.ContainerTypes.SingleLineText || oc.ContainerType ==  ObjectContainer.ContainerTypes.MultiLineText || oc.ContainerType ==  ObjectContainer.ContainerTypes.Label)
                        {
                            //objObject.MaxHeight += 2;
                            PrintFlow.SilverlightControls.AdjustablePanel objPanel = null;
                            if (oc.SelContainerPanel != null)
                                objPanel = (PrintFlow.SilverlightControls.AdjustablePanel)oc.SelContainerPanel;
                            else if (oc.ContainerName != null)
                            {
                                objPanel = (PrintFlow.SilverlightControls.AdjustablePanel)oc.ContainerRoot.FindName("AP" + oc.ContainerName);
                            }
                            if (objPanel != null)
                            {
                                bool isPrnt = true;
                                int PrntId = 0;
                                int PDisIdx = 0;
                                foreach (FrameworkElement ChildCtl in objPanel.Children)
                                {
                                    if (ChildCtl.GetType().Name == "TextBlock" )
                                    {

                                        TextBlock ocContent = (TextBlock)ChildCtl;
                                        if (ocContent != null)
                                        {
                                            ProductServiceReference.TemplateObjects TmpObect = oc.FindObject(ocContent.Name);
                                            string TxtCtlName = TmpObect.TCtlName;
                                            webprintDesigner.ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
                                            objObject = TmpObect;
                                            objObject.ObjectID = ObjIndx;
                                            if (isPrnt)
                                            {

                                                objObject.ParentId = 0;
                                                objObject.ObjectType = Convert.ToInt32( oc.ContainerType);


                                                ////if (objPanel.HAlign == TextAlignment.Left)
                                                ////    objObject.PositionX = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.LeftProperty));
                                                ////else if (objPanel.HAlign == TextAlignment.Center)
                                                ////    objObject.PositionX = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.LeftProperty)) + (webprintDesigner.Common.PixelToPoint(oc.ActualWidth) / 2);
                                                ////else if (objPanel.HAlign == TextAlignment.Right)
                                                ////    objObject.PositionX = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.LeftProperty)) + webprintDesigner.Common.PixelToPoint(oc.ActualWidth);
                                                ////else
                                                    objObject.PositionX = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.LeftProperty));

                                                if (objPanel.VAlign == VerticalAlignment.Top)
                                                    objObject.PositionY = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.TopProperty));
                                                else if (objPanel.VAlign == VerticalAlignment.Center)
                                                    objObject.PositionY = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.TopProperty));
                                                else if (objPanel.VAlign == VerticalAlignment.Bottom)
                                                    objObject.PositionY = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.TopProperty)) + webprintDesigner.Common.PixelToPoint(oc.ActualHeight);
                                                else
                                                    objObject.PositionY = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.TopProperty));


                                               


                                                double ang = oc.getAngle();
                                                if (ang < 0)
                                                    ang = 360 + ang;
                                                if (ang != 0)
                                                    objObject.RotationAngle = 360 - ang;
                                                else
                                                    objObject.RotationAngle = 0;
                                                isPrnt = false;

                                                if (oc.GetValue(Canvas.ZIndexProperty) != null)
                                                {
                                                    PDisIdx = (int)oc.GetValue(Canvas.ZIndexProperty);
                                                    //PDisIdx = PDisIdx - 100;
                                                    //if (PDisIdx < 0)
                                                    //    PDisIdx = 0;
                                                    objObject.DisplayOrderPdf = PDisIdx;
                                                }
                                                else
                                                    objObject.DisplayOrderPdf = 0;

                                                PrntId = objObject.ObjectID;

                                            }
                                            else
                                            {
                                                objObject.ParentId = PrntId;
                                                if (ChildCtl.GetValue(PrintFlow.SilverlightControls.AdjustablePanel.DisplayIndexProperty) != null)
                                                {
                                                    objObject.DisplayOrderPdf = PDisIdx + (int)ChildCtl.GetValue(PrintFlow.SilverlightControls.AdjustablePanel.DisplayIndexProperty);
                                                }
                                                else
                                                    objObject.DisplayOrderPdf = PDisIdx;
                                                objObject.PositionX = 0;
                                                objObject.PositionY = 0;
                                            }


                                            //get the dimensions of object and store

                                           
                                            objObject.MaxWidth = webprintDesigner.Common.PixelToPoint(objPanel.Width);
                                            objObject.MaxHeight = webprintDesigner.Common.PixelToPoint(objPanel.Height);
                                            //objObject.Name = oc.ObjectName;
                                            



                                            objObject.ContentString = ocContent.Text;
                                           
                                            //objObject.FontName = ocContent.FontFamily.ToString();
                                            //objObject.IsFontNamePrivate = oc.IsCustomFont;
                                            //objObject.FontSize = webprintDesigner.Common.PixelToPoint(ocContent.FontSize);
                                            //if (ocContent.FontWeight == FontWeights.Bold)
                                            //    objObject.IsBold = true;
                                            //else
                                            //    objObject.IsBold = false;
                                            //if (ocContent.FontStyle == FontStyles.Italic)
                                            //    objObject.IsItalic = true;
                                            //else
                                            //    objObject.IsItalic = false;
                                            //if (ocContent.TextDecorations == TextDecorations.Underline)
                                            //    objObject.IsUnderlinedText = true;
                                            //else
                                            //    objObject.IsUnderlinedText = false;
                                            objObject.IsEditable = oc.IsLockedEditing;
                                            objObject.IsPositionLocked = true;// oc.IsLockedPosition;
                                            objObject.IsHidden = oc.IsPrintable;

                                            //objObject.

                                            objObject.MaxCharacters = 0;
                                            objObject.IsFontCustom = true;
                                            //objObject.PageNo = 1;
                                            //objObject.SpaceAfter = 0;
                                            objObject.ColorType = 3;
                                            //objObject.ColorC = oc.ColorC;
                                            //objObject.ColorM = oc.ColorM;
                                            //objObject.ColorY = oc.ColorY;
                                            //objObject.ColorK = oc.ColorK;
                                            objObject.ColorStyleID = 0;
                                            objObject.IsSpotColor = false;
                                            if (objPanel.HAlign == TextAlignment.Left)
                                                objObject.Allignment = 1;
                                            else if (objPanel.HAlign == TextAlignment.Center)
                                                objObject.Allignment = 2;
                                            else if (objPanel.HAlign == TextAlignment.Right)
                                                objObject.Allignment = 3;

                                            if (objPanel.VAlign == VerticalAlignment.Top)
                                                objObject.VAllignment = 1;
                                            else if (objPanel.VAlign == VerticalAlignment.Center)
                                                objObject.VAllignment = 1;
                                            else if (objPanel.VAlign == VerticalAlignment.Bottom)
                                                objObject.VAllignment = 3;

                                            if (ChildCtl.GetValue(PrintFlow.SilverlightControls.AdjustablePanel.IsStartNewLineProperty) != null)
                                            {
                                                objObject.IsNewLine = (bool)ChildCtl.GetValue(PrintFlow.SilverlightControls.AdjustablePanel.IsStartNewLineProperty);
                                            }
                                            else
                                                objObject.IsNewLine = false;

                                         
                                            objObject.DisplayOrderTxtControl = 0;
                                            if (TxtCtlName != "" && objPTxtCtls != null && objPTxtCtls.CtrlsList.FindName(TxtCtlName) != null)
                                            {

                                                TextBox tmpTb = (TextBox)objPTxtCtls.CtrlsList.FindName(TxtCtlName);
                                                if (tmpTb.Parent.GetType().Name == "Grid")
                                                {
                                                    Grid tmpGrd = (Grid)tmpTb.Parent;
                                                    if (tmpGrd.Tag != null && tmpGrd.Tag.ToString() != "")
                                                    {
                                                        int txtIdx = 0;
                                                        int.TryParse(tmpGrd.Tag.ToString(), out txtIdx);
                                                        objObject.DisplayOrderTxtControl = txtIdx;
                                                    }
                                                }
                                            }


                                            objObject.IsMandatory = false;
                                            objObject.IsRequireNumericValue = false;

                                            objObject.isSide2Object = ObjectSide;
                                            objObject.Tint = 0;

                                            ObjectsList.Add(objObject);
                                            ObjIndx++;
                                        }
                                    }
                                }
                            }

                        }
                        else if (oc.ContainerType ==  ObjectContainer.ContainerTypes.Image ||  oc.ContainerType ==  ObjectContainer.ContainerTypes.LogoImage)
                        {
                            webprintDesigner.ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
                            objObject.ObjectID = ObjIndx;
                            objObject.ParentId = 0;
                            objObject.ObjectType = Convert.ToInt32( oc.ContainerType);
                            objObject.PositionX = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.LeftProperty));
                            objObject.PositionY = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.TopProperty)) + webprintDesigner.Common.PixelToPoint(oc.ContainerRoot.ActualHeight);
                            objObject.MaxWidth = webprintDesigner.Common.PixelToPoint(oc.ContainerRoot.ActualWidth);
                            objObject.MaxHeight = webprintDesigner.Common.PixelToPoint(oc.ContainerRoot.ActualHeight);
                            double ang = oc.getAngle();
                            if (ang < 0)
                                ang = 360 + ang;
                            if (ang != 0)
                                objObject.RotationAngle = 360 - ang;
                            else
                                objObject.RotationAngle = 0;
                            objObject.Name = oc.ObjectName;


                            objObject.IsEditable = oc.IsLockedEditing;
                            objObject.IsPositionLocked = true;// oc.IsLockedPosition;
                            objObject.IsHidden = oc.IsPrintable;

                            objObject.ContentString = oc.ImagePath;
                            objObject.DisplayOrderTxtControl = 0;
                            objObject.IsMandatory = false;
                            objObject.IsRequireNumericValue = false;
                            if (oc.GetValue(Canvas.ZIndexProperty) != null)
                                objObject.DisplayOrderPdf = (int)oc.GetValue(Canvas.ZIndexProperty);
                            else
                                objObject.DisplayOrderPdf = 0;

                            objObject.isSide2Object = ObjectSide;
                            objObject.Tint = 0;
                            objObject.IsNewLine = false;
                            objObject.OffsetX = 0;
                            objObject.OffsetY = 0;
                            objObject.VAllignment = 0;
                            objObject.Allignment = 0;

                            objObject.ColorName = string.Empty;
                            objObject.ExField1 = string.Empty;
                            objObject.ExField2 = string.Empty;
                            objObject.SpotColorName = string.Empty;
                            objObject.FontName = string.Empty;
                            objObject.FontSize = 0;
                            objObject.FontStyleID = 0;
                            objObject.IsFontCustom = false;
                            objObject.IsFontNamePrivate = false;
                            objObject.TCtlName = string.Empty;
                            objObject.PageNo = 1;

                            ObjectsList.Add(objObject);
                            ObjIndx++;
                        }
                        else if (oc.ContainerType == ObjectContainer.ContainerTypes.LineVector || oc.ContainerType == ObjectContainer.ContainerTypes.RectangleVector || oc.ContainerType == ObjectContainer.ContainerTypes.EllipseVector)
                        {

                            ProductServiceReference.TemplateObjects TmpObect = oc.SelectedObect;
                            webprintDesigner.ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
                            objObject = TmpObect;



                            
                            objObject.ObjectID = ObjIndx;
                            objObject.ParentId = 0;
                            objObject.ObjectType = Convert.ToInt32(oc.ContainerType);
                            objObject.PositionX = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.LeftProperty));
                            objObject.PositionY = webprintDesigner.Common.PixelToPoint((double)oc.GetValue(Canvas.TopProperty)) ;
                            objObject.MaxWidth = webprintDesigner.Common.PixelToPoint(oc.ContainerRoot.ActualWidth);
                            objObject.MaxHeight = webprintDesigner.Common.PixelToPoint(oc.ContainerRoot.ActualHeight);
                            double ang = oc.getAngle();
                            if (ang < 0)
                                ang = 360 + ang;
                            if (ang != 0)
                                objObject.RotationAngle = 360 - ang;
                            else
                                objObject.RotationAngle = 0;
                            objObject.Name = oc.ObjectName;


                            objObject.IsEditable = oc.IsLockedEditing;
                            objObject.IsPositionLocked = true;// oc.IsLockedPosition;
                            objObject.IsHidden = oc.IsPrintable;

                            if (oc.ContainerType == ObjectContainer.ContainerTypes.LineVector )
                                objObject.ContentString = "line";
                            else if (oc.ContainerType == ObjectContainer.ContainerTypes.RectangleVector)
                                objObject.ContentString = "rectangle";
                            else if (oc.ContainerType == ObjectContainer.ContainerTypes.EllipseVector)
                                objObject.ContentString = "Ellipse";
                            objObject.DisplayOrderTxtControl = 0;
                            objObject.IsMandatory = false;
                            objObject.IsRequireNumericValue = false;
                            if (oc.GetValue(Canvas.ZIndexProperty) != null)
                                objObject.DisplayOrderPdf = (int)oc.GetValue(Canvas.ZIndexProperty);
                            else
                                objObject.DisplayOrderPdf = 0;

                            objObject.isSide2Object = ObjectSide;
                            objObject.Tint = 0;
                            objObject.IsNewLine = false;
                            objObject.OffsetX = 0;
                            objObject.OffsetY = 0;
                            objObject.VAllignment = 0;
                            objObject.Allignment = 0;

                            objObject.ColorType = 3;
                            objObject.ColorName = string.Empty;
                            objObject.ExField1 = string.Empty;
                            objObject.ExField2 = string.Empty;
                            objObject.SpotColorName = string.Empty;
                            objObject.FontName = string.Empty;
                            objObject.FontSize = 0;
                            objObject.FontStyleID = 0;
                            objObject.IsFontCustom = false;
                            objObject.IsFontNamePrivate = false;
                            objObject.TCtlName = string.Empty;
                            objObject.PageNo = 1;

                            ObjectsList.Add(objObject);
                            ObjIndx++;
                        }
                            

                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::GetPageObjects::" + ex.ToString());
            }
            return ObjectsList;
        }


        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ChildWindow oc = new ChildWindow();
                //oc.
                //oc.Show();

                //cwind.Visibility = System.Windows.Visibility.Visible;
                //cwind.con
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::GenratePDF_Click::" + ex.ToString());
            }

        }

        private void SavePDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CloseDropdowns();
                ProgressBar1.IsIndeterminate = true;
                ProgessTxt.Text = "Saving..";
                ProgressPanel.Visibility = Visibility.Visible;


                SaveOperationType = SaveOperationTypes.SaveReturnToDetails; 
                SaveOperation();
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::SavePDF_Click::" + ex.ToString());
            }
        }



        private void GenratePDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                CloseDropdowns();

                    ProgressBar1.IsIndeterminate = true;
                    ProgessTxt.Text = "Saving and Generating PDF Preview..";
                    ProgressPanel.Visibility = Visibility.Visible;

                    if (App.DesignerMode == DesignerModes.CreatorMode || App.DesignerMode == DesignerModes.CorporateMode)
                    {
                        SaveOperationType = SaveOperationTypes.SaveGenerateOpenPDFPreview;  //we are gonna view the preview after saving... 
                    }
                    else if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.SimpleEndUser )
                    {
                        SaveOperationType = SaveOperationTypes.SaveGeneratePDFAttachMode;   //we are not gonna view the preview instead we will attach the PDF to order embedded mode.
                    }
                    //UserServiceReference.UserServiceClient objLognSrv = new webprintDesigner.UserServiceReference.UserServiceClient();
                    //objLognSrv.IsUserLoginedCompleted += new EventHandler<webprintDesigner.UserServiceReference.IsUserLoginedCompletedEventArgs>(objSrv_PopwinIsUserLoginedCompleted);
                    //objLognSrv.IsUserLoginedAsync(App.DesignMode);
                    SaveOperation();
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::GenratePDF_Click::" + ex.ToString());
            }
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                CloseDropdowns();

                ProgressBar1.IsIndeterminate = true;
                ProgessTxt.Text = "Saving and Generating Preview";
                ProgressPanel.Visibility = Visibility.Visible;

                SaveOperationType = SaveOperationTypes.SaveGenerateOpenPDFPreview;

                //UserServiceReference.UserServiceClient objLognSrv = new webprintDesigner.UserServiceReference.UserServiceClient();
                //objLognSrv.IsUserLoginedCompleted += new EventHandler<webprintDesigner.UserServiceReference.IsUserLoginedCompletedEventArgs>(objSrv_PopwinIsUserLoginedCompleted);
                //objLognSrv.IsUserLoginedAsync(App.DesignMode);
                SaveOperation();

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::GenratePDF_Click::" + ex.ToString());
            }
        }

        
        private void SaveOperation()
        {
            if (ProductId != 0 && tbDesignPages.Items.Count > 0)
            {
                btnToggleSides.IsChecked = false;
                //tbDesignPages.SelectedIndex = 1;
                tbDesignPages.UpdateLayout();
                //System.Threading.Thread.Sleep(500);

                string BkImg1 = "";
                string BkImg2 = "";
                int ObjIndx = 1;
                DesignerDisabl.Visibility = Visibility.Visible;
                System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateObjects> ObjectsList = new System.Collections.ObjectModel.ObservableCollection<webprintDesigner.ProductServiceReference.TemplateObjects>();
                if (objProduct.IsDoubleSide)
                {
                    TabItem tbItem = new TabItem();
                    tbItem.FontSize = 18;
                    tbItem = (TabItem)tbDesignPages.Items[0];
                    PageContainer objPage = (PageContainer)tbItem.Content;
                    objPage.UnSelAllObject();
                    ObjectsList = GetPageObjects(objPage, false, ObjectsList, ref ObjIndx);
                    BkImg1 = objPage.BackgroundImageName;
                    if (tbDesignPages.Items.Count > 1)
                    {
                        
                        TabItem tbItem2 = new TabItem();
                        tbItem2.FontSize = 18;
                        tbItem2 = (TabItem)tbDesignPages.Items[1];
                        PageContainer objPage2 = (PageContainer)tbItem2.Content;
                        objPage2.UnSelAllObject();
                        BkImg2 = objPage2.BackgroundImageName;
                        ObjIndx++;
                        ObjectsList = GetPageObjects(objPage2, true, ObjectsList, ref ObjIndx);
                    }
                }
                else
                {
                    TabItem tbItem = new TabItem();
                    tbItem.FontSize = 18;
                    tbItem = (TabItem)tbDesignPages.Items[0];
                    PageContainer objPage = (PageContainer)tbItem.Content;
                    objPage.UnSelAllObject();
                    BkImg1 = objPage.BackgroundImageName;
                    ObjectsList = GetPageObjects(objPage, false, ObjectsList, ref ObjIndx);
                }
                ProductServiceReference.ProductServiceClient objSrvObectSave = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                objSrvObectSave.SaveObjectsAndGenratePDFCompleted += new EventHandler<webprintDesigner.ProductServiceReference.SaveObjectsAndGenratePDFCompletedEventArgs>(objSrvObectSave_SaveObjectsCompleted);

                objSrvObectSave.SaveObjectsAndGenratePDFAsync(ProductId, BkImg1, BkImg2, ObjectsList, App.DesignerMode, SaveOperationType);
               
            }

       
        }
        void objSrvObectSave_SaveObjectsCompleted(object sender, webprintDesigner.ProductServiceReference.SaveObjectsAndGenratePDFCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    //if (ShowException)
                    //MessageBox.Show(e.Result);


                    if (e.Result != "")
                    {

                        ProgressBar1.IsIndeterminate = false;
                        ProgressPanel.Visibility = Visibility.Collapsed;
                        tbDesignPages.SelectedIndex = 0;

                        if ((App.DesignerMode == DesignerModes.CreatorMode || App.DesignerMode == DesignerModes.CorporateMode))
                        {



                            if (SaveOperationType == SaveOperationTypes.SaveGenerateOpenPDFPreview) // If btn generate PDF clicked then show PDF
                            {

                                PreviewPane oPane = new PreviewPane(ProductId);
                                oPane.Show();
                                //if (tbDesignPages.Items.Count > 1 && objProduct.IsDoubleSide)
                                //{
                                //    if (tbDesignPages.SelectedIndex == 1)
                                //        HtmlPage.Window.Eval("switchToPreview(2);");
                                //    else
                                //        HtmlPage.Window.Eval("switchToPreview(1);");
                                //}
                                //else
                                //    HtmlPage.Window.Eval("switchToPreview(1);");
                            }
                            else if (SaveOperationType == SaveOperationTypes.SaveReturnToDetails)
                            {
                                //HtmlPage.Window.Eval("BackToTemplateDetails(" + HtmlPage.Document.QueryString["templateid"] + ");");
                                DictionaryManager.AppObjects["productid"] = objProduct.ProductID;
                                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);
                            }

                        }
                        else if ((App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.SimpleEndUser))
                        {
                            //PreviewPane oPreviewPane = new PreviewPane(ProductId);
                            //if ( SaveOperationType == SaveOperationTypes.SaveGeneratePDFAttachMode)
                            //    oPreviewPane.Closed += new EventHandler(oPreviewPane_Closed);
                            //oPreviewPane.Show();
                            //oPreviewPane.Tag = e.Result;

                            if (SaveOperationType == SaveOperationTypes.SaveGenerateOpenPDFPreview) // If btn generate PDF clicked then show PDF
                            {
                                PreviewPane oPreviewPane = new PreviewPane(ProductId);
                                //if (SaveOperationType == SaveOperationTypes.SaveGeneratePDFAttachMode)
                                //    oPreviewPane.Closed += new EventHandler(oPreviewPane_Closed);
                                oPreviewPane.Show();

                            }
                            else 
                            {
                                HtmlPage.Window.Eval("gotoLandingPage('" + e.Result.ToString() + "');");
                            }

                           

                            //HtmlPage.Window.Navigate(new Uri("default.aspx"));

                        }
                    }
                    DesignerDisabl.Visibility = Visibility.Collapsed;
                }
                else if (e.Error is Exception)
                {
                    MessageBox.Show((e.Error as Exception).ToString());
                }
            }
            catch (Exception ex)
            {
                DesignerDisabl.Visibility = Visibility.Collapsed;
                if (ShowException)
                    MessageBox.Show("::objSrvObectSave_SaveObjectsCompleted::" + ex.ToString());
            }
        }

        void oPreviewPane_Closed(object sender, EventArgs e)
        {
            PreviewPane oPane = (PreviewPane)sender;
            if (oPane.DialogResult.Value)
            {
                HtmlPage.Window.Eval("gotoLandingPage('" + oPane.Tag.ToString() + "');");
            }
        }

        private void tbDesignPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                TabItem UnSelItem = (TabItem)e.RemovedItems[0];
                PageContainer objPage = (PageContainer)UnSelItem.Content;
                objPage.objPageTxtControl.Visibility = Visibility.Collapsed;
                objPage.UnSelAllObject();
            }
            if (e.AddedItems.Count > 0)
            {
                TabItem SelItem = (TabItem)e.AddedItems[0];
                PageContainer objPage = (PageContainer)SelItem.Content;
                objPage.objPageTxtControl.Visibility = Visibility.Visible;
                SelectedPage = objPage;
                //SelectedTxtCtls = objPage.objPageTxtControl;
            }
        }


        private void ddpAddTxtObject_DropdownBeforeOpenClose(object sender, bool IsOpen)
        {
            try
            {
                
                    if (IsOpen)
                    {

                        if (SelectedPage != null)
                        {
                            SelectedPage.UnSelAllObject();
                        }

                       

                        txtObjectName.Text = string.Empty;
                        txtObjectName.Focus();
                        Dispatcher.BeginInvoke(() => { txtObjectName.Focus(); }); 

                        //ddpBackgroundImages.IsOpened = false;
                        CloseDropdowns();
                        ddpAddTxtObject.IsOpened = true;

                        if (SelectedPage != null)
                        {
                            //cmbControlList.Visibility = Visibility.Visible;
                            //chkControlNewLine.Visibility = Visibility.Visible;



                        }
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

         private void ddpBackgroundImages_DropdownBeforeOpenClose(object sender, bool IsOpen)
        {
            try
            {
                if (IsOpen)
                {
                    CloseDropdowns();
                    
                }
                
            }
              catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
         }

         private void ddpImages_DropdownBeforeOpenClose(object sender, bool IsOpen)
        {
            try
            {
                    if (IsOpen)
                    {
                        CloseDropdowns();
                        ddpImages.IsOpened = true;
                    }
                
            }
              catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
         }

         private void ddpLogo_DropdownBeforeOpenClose(object sender, bool IsOpen)
         {
             try
             {
                 if (IsOpen)
                 {
                     CloseDropdowns();
                     ddpLogo.IsOpened = true;
                 }

             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
         }
            


        private void lstImagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (e.AddedItems[0].GetType().Name == "Image")
                {
                    Image img = e.AddedItems[0] as Image;
                    Image img2 = new Image();
                    img2.Source = img.Source;
                    img2.Height = img.ActualHeight;
                    img2.Width = img.ActualWidth;
                    img2.Name = "DADImg";
                    img2.MouseMove += DragImg_MouseMove;
                    LayoutMain.Children.Add(img2);
                    Image img3 = LayoutMain.FindName("DADImg") as Image;

                    img3.SetValue(Canvas.LeftProperty, 50);
                    img3.SetValue(Canvas.TopProperty, 50);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::lstImagesList_SelectionChanged::" + ex.ToString());
            }

        }

        //private void btnToggleAdvancedControls_Click(object sender, RoutedEventArgs e)
        //{
        //    if (AdvancedToolsPanel.Visibility == System.Windows.Visibility.Collapsed)
        //        AdvancedToolsPanel.Visibility = System.Windows.Visibility.Visible;
        //    else
        //        AdvancedToolsPanel.Visibility = System.Windows.Visibility.Collapsed;

        //}

        private void txtFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            try
            {
                
                
                    if (txtFontSize.Value != 0 && !SelObjPro && SelectedPage != null) //IsFontChange && 
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerFontSize(txtFontSize.Value);

                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::txtFontSize_TextChanged::" + ex.ToString());
            }

        }


       

        string prvCtrl = string.Empty;

        void ProductFPage_ObjectSelect_Event(object sender, bool SelectState,string ControlName, bool unselectMode, Point e,Size ContainerSize)
        {

            //CloseDropdowns();

          
                winEditorNew.Visibility = System.Windows.Visibility.Visible;

                

                //winEditorNew.SetValue(Canvas.LeftProperty, 10d);  //e.X + ContainerSize.Width
               // winEditorNew.SetValue(Canvas.TopProperty, 10d);   //e.Y - 120// minus the height of top margin and area outside the page container
           
           

            //winEditorNew.SetValue(Canvas.LeftProperty, (Convert.ToDouble(SelectedPage.ActualWidth - 300)));
            //winEditorNew.SetValue(Canvas.TopProperty, Convert.ToDouble(145));
           

            //if (SelectState == true)
            //{

            //    winEditor.SetValue(Canvas.LeftProperty, (Convert.ToDouble( SelectedPage.ActualWidth - 300)));
            //    winEditor.SetValue(Canvas.TopProperty,Convert.ToDouble( 145));

            //    winEditor.IsOpened = SelectState;
            //}
            //else if (SelectState == false && unselectMode == true)
            //{
            //    winEditor.SetValue(Canvas.LeftProperty, e.X);
            //    winEditor.SetValue(Canvas.TopProperty, e.Y);
            //    winEditor.IsOpened = SelectState;

            //}


            //try
            //{
            //    FrameworkElement focusedElement = SelectedPage.objPageTxtControl.CtrlsList.FindName(ControlName) as FrameworkElement;
            //    GeneralTransform focusedVisualTransform = focusedElement.TransformToVisual(grTxtCtrlsScroll);
            //    Rect rectangle = focusedVisualTransform.TransformBounds(new Rect(new Point(focusedElement.Margin.Left, focusedElement.Margin.Top), focusedElement.RenderSize));
            //    double newOffset = grTxtCtrlsScroll.VerticalOffset + (rectangle.Bottom - grTxtCtrlsScroll.ViewportHeight);
            //    grTxtCtrlsScroll.ScrollToVerticalOffset(newOffset);
            //}
            //catch (Exception ex)
            //{ }


            //if (prvCtrl == ControlName)
            //{
            //    ChangeCtlExpanderState(SelectState);
            //}
            //    prvCtrl = ControlName;
        }

        private void btnEditorClose_Click(object sender, RoutedEventArgs e)
        {
            winEditor.IsOpened = false;

        }

        private void CloseDropdowns()
        {
            if (ddpAddTxtObject.IsOpened)
                ddpAddTxtObject.IsOpened = false;

            if (ddpImages.IsOpened)
                ddpImages.IsOpened = false;

            if ( ddpPDFCanvas.IsOpened)
                ddpPDFCanvas.IsOpened = false;

            if (ddpLogo.IsOpened)
                ddpLogo.IsOpened = false;



            winEditorNew.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnGotoProfile_Click(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            CloseDialog ocloseDialog = new CloseDialog();
            ocloseDialog.Show();
            ocloseDialog.Closed += new EventHandler(ocloseDialog_Closed);
            
            
            
        }

        void ocloseDialog_Closed(object sender, EventArgs e)
        {
            CloseDialog ocloseDialog = (CloseDialog)sender;

            if (ocloseDialog.DialogResult.Value == true)
            {
                ProgressBar1.IsIndeterminate = true;
                ProgessTxt.Text = "Saving..";
                ProgressPanel.Visibility = Visibility.Visible;


                SaveOperationType = SaveOperationTypes.SaveReturnToDetails;

                SaveOperation();
            }
            else
            {

                //goto details without saving
                DictionaryManager.AppObjects["productid"] = objProduct.ProductID;
                ((webprintDesigner.UserControlContainer)Application.Current.RootVisual).SwitchControl(DictionaryManager.EditTemplate, DictionaryManager.Pages.EditTemplate);
            }
        }

        private void btnSnapToggle_Checked(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            IsSnapToGrid = true;
            SelectedPage.IsSnapToGrid = IsSnapToGrid;

        }

        private void btnSnapToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            IsSnapToGrid = false;
            SelectedPage.IsSnapToGrid = IsSnapToGrid;
            //btnSnapToggle.Content = "";
        }


       

        private void btnPageNav_Click(object sender, RoutedEventArgs e)
        {
            Button obtn = (Button)sender;

            if (obtn.Tag.ToString() == "left")
            {
                if (tbDesignPages.SelectedIndex != 0)
                {
                    
                    tbDesignPages.SelectedIndex--;

                    if (SelectedPage != null)
                    {
                        if (SelectedPage.ZoomAnimationX.To.HasValue)
                            ZoomSlider.Value = SelectedPage.ZoomAnimationX.To.Value;
                    }
                }
            }
            else if (obtn.Tag.ToString() == "right")
            {
                if (tbDesignPages.SelectedIndex != tbDesignPages.Items.Count)
                {
                    tbDesignPages.SelectedIndex++;

                    if (SelectedPage != null)
                    {
                        if (SelectedPage.ZoomAnimationX.To.HasValue)
                            ZoomSlider.Value = SelectedPage.ZoomAnimationX.To.Value;
                    }
                }
            }

        }

       

        private void btnToggleSides_Checked(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            tbDesignPages.SelectedIndex = 0;
        }

        private void btnToggleSides_Unchecked(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            tbDesignPages.SelectedIndex = 1;
        }

        private void btnShowHideGuides_Checked(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            SelectedPage.ToggleGuides(true);
        }

        private void btnShowHideGuides_Unchecked(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            SelectedPage.ToggleGuides(false);
        }

        private void chkLockPosition_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
               
                    if (SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerLockPosition(true,true);
                            }
                        }
                        
                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::chkLockPosition_Checked::" + ex.ToString());
            }
        }

        private void chkLockPosition_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerLockPosition(false,true);
                            }
                        }

                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::chkLockPosition_Unchecked::" + ex.ToString());
            }
        }

        private void chkLockEditing_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
               
                    if (SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerLockEditing(false,true);
                            }
                        }

                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::chkLockEditing_Checked::" + ex.ToString());
            }
        }

        private void chkLockEditing_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerLockEditing(true,true);
                            }
                        }

                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::chkLockEditing_Unchecked::" + ex.ToString());
            }
        }

        private void chkShowHide_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerShowHide(true, true);
                            }
                        }

                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::chkShowHide_Checked::" + ex.ToString());
            }
        }

        private void chkShowHide_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (SelectedPage != null)
                    {
                        foreach (UIElement el in SelectedPage.DesignArea.Children)
                        {
                            if (el.GetType().Name == "ObjectContainer")
                            {
                                ObjectContainer oc = (ObjectContainer)el;
                                if (oc.Selected && !oc.MouseDown)
                                    oc.UpdateContainerShowHide(false, true);
                            }
                        }

                    }
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::chkShowHide_Unchecked::" + ex.ToString());
            }
        }


        private void SelectPDF(int Side)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PDF Documents (*.pdf)|*.pdf";
            dlg.Multiselect = false;

            if ((bool)dlg.ShowDialog())
            {
                DesignerDisabl2.Visibility = Visibility.Visible;
                ProgressBar1.IsIndeterminate = true;
                ProgessTxt.Text = "Uploading Background PDF";
                ProgressPanel.Visibility = Visibility.Visible;

                FileUpload oFileUploader = new FileUpload(this.Dispatcher, new Uri(HtmlPage.Document.DocumentUri, UriPrefix + "FileUpload/FileUploadHandler.ashx"), dlg.File);
                oFileUploader.ChunkSize = 9194304;
                oFileUploader.StatusChanged += new EventHandler(oFileUploader_StatusChanged);
                if (Side == 1)
                    oFileUploader.Upload(objProduct.ProductID, 3,0);
                else if (Side == 2)
                    oFileUploader.Upload(objProduct.ProductID, 4,0);

                oFileUploader = null;     


            }

        }

        void oFileUploader_StatusChanged(object sender, EventArgs e)
        {
            FileUpload fu = sender as FileUpload;
            if (fu.Status == FileUploadStatus.Complete)
            {
                ProductServiceReference.ProductServiceClient objSrvProd = new webprintDesigner.ProductServiceReference.ProductServiceClient();
                objSrvProd.GetProductByIdCompleted += new EventHandler<ProductServiceReference.GetProductByIdCompletedEventArgs>(objSrvProd_GetProductByIdCompleted);
                objSrvProd.GetProductByIdAsync(objProduct.ProductID);

               
            }
        }

        void objSrvProd_GetProductByIdCompleted(object sender, ProductServiceReference.GetProductByIdCompletedEventArgs e)
        {
            try
            {


                objProduct = (webprintDesigner.ProductServiceReference.Templates)e.Result;

                if (objProduct.TempString == "success")
                {
                    if (PDFSideUploading == 1)
                    {
                        System.Windows.Media.Imaging.BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objProduct.BackgroundArtwork));
                        ImageBrush imgBk = new ImageBrush();
                        imgBk.Stretch = Stretch.Fill;
                        imgBk.ImageSource = bimg;

                        TabItem SelItem = (TabItem)tbDesignPages.Items[0];
                        PageContainer objPage = (PageContainer)SelItem.Content;
                        objPage.brdDesign.Background = imgBk;
                        Side1PDFImage.Source = bimg;
                    }
                    else if (PDFSideUploading == 2)
                    {
                        System.Windows.Media.Imaging.BitmapImage bimg2 = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + objProduct.Side2BackgroundArtwork));
                        ImageBrush imgBk = new ImageBrush();
                        imgBk.Stretch = Stretch.Fill;
                        imgBk.ImageSource = bimg2;


                        TabItem SelItem = (TabItem)tbDesignPages.Items[1];
                        PageContainer objPage = (PageContainer)SelItem.Content;
                        objPage.brdDesign.Background = imgBk;
                        Side2PDFImage.Source = bimg2;
                    }

                    DesignerDisabl2.Visibility = Visibility.Collapsed;
                    ProgressBar1.IsIndeterminate = false;
                    ProgessTxt.Text = "";
                    ProgressPanel.Visibility = Visibility.Collapsed;

                }
                else
                {
                    DesignerDisabl2.Visibility = Visibility.Collapsed;
                    ProgressBar1.IsIndeterminate = false;
                    ProgressPanel.Visibility = Visibility.Collapsed;

                    MessageBox.Show(objProduct.TempString);
                }

               

            }
            catch (Exception ex)
            {

                if (ShowException)
                    MessageBox.Show("::objSrvProd_GetProductByIdCompleted::" + ex.ToString());
            }

        }


        private void btnPDFSide1_Click(object sender, RoutedEventArgs e)
        {

            

            PDFSideUploading = 1;
            SelectPDF(1);
        }

        private void btnPDFSide2_Click(object sender, RoutedEventArgs e)
        {
           

            PDFSideUploading = 2;
            SelectPDF(2);
        }

        private void btnQuickText_Click(object sender, RoutedEventArgs e)
        {
            DesignerDisabl2.Visibility = Visibility.Visible;
            winQuickText.SetValue(Canvas.LeftProperty, LayoutMain.ActualWidth / 2 - (winQuickText.ActualWidth / 2));  //e.X + ContainerSize.Width
            winQuickText.SetValue(Canvas.TopProperty, (LayoutMain.ActualHeight / 2) - (winQuickText.ActualHeight / 3));
            winQuickText.IsOpened = true;

        }

        private void btnQSave_Click(object sender, RoutedEventArgs e)
        {

            QuickText oQuickText = null;


            if (DictionaryManager.AppObjects.ContainsKey("QuickText") &&  DictionaryManager.AppObjects["QuickText"] != null)
                oQuickText = (QuickText)DictionaryManager.AppObjects["QuickText"];
            else
            {
                oQuickText = new QuickText();

                oQuickText.Company = "Your Company Name";
                oQuickText.CompanyMessage = "Your Company Message";
                oQuickText.Name = "Your Name";
                oQuickText.Title = "Your Title";
                oQuickText.Address1 = "Address Line 1";
                oQuickText.Address2 = "Address Line 2";
                oQuickText.Address3 = "Address Line 3";
                oQuickText.Telephone = "Telephone / Other";
                oQuickText.Fax = "Fax / Other";
                oQuickText.Email = "Email address / Other";
                oQuickText.Website = "Website address";
            }


            oQuickText.Company = (txtQCompany.Text == string.Empty ? "Your Company Name" : txtQCompany.Text);
            oQuickText.CompanyMessage = txtQCompanyMessage.Text == string.Empty ? "Your Company Message" : txtQCompanyMessage.Text;
            oQuickText.Name = txtQName.Text == string.Empty ? "Your Name" : txtQName.Text;
            oQuickText.Title = txtQTitle.Text == string.Empty ? "Your Title" : txtQTitle.Text;
            oQuickText.Address1 = txtQAddress1.Text == string.Empty ? "Address Line 1" : txtQAddress1.Text;
            oQuickText.Address2 = txtQAddress2.Text == string.Empty ? "Address Line 2" : txtQAddress2.Text;
            oQuickText.Address3 = txtQAddress3.Text == string.Empty ? "Address Line 3" : txtQAddress3.Text;
            oQuickText.Telephone = txtQTelephone.Text == string.Empty ? "Telephone / Other" :txtQTelephone.Text ;
            oQuickText.Fax = txtQFax.Text == string.Empty ? "Fax / Other"  : txtQFax.Text;
            oQuickText.Email = txtQEmail.Text == string.Empty ? "Email address / Other" :txtQEmail.Text ;
            oQuickText.Website = txtQWebsite.Text == string.Empty ? "Website address" : txtQWebsite.Text;

            DictionaryManager.AppObjects["QuickText"] = oQuickText;

           

            //
             
            TabItem tbItem = (TabItem)tbDesignPages.Items[0];  //iterating side 1 /page 1
            PageContainer page1 = (PageContainer)tbItem.Content;

            foreach (UIElement el in page1.DesignArea.Children)
            {
                if (el.GetType().Name == "ObjectContainer")
                {
                    ObjectContainer oc = (ObjectContainer)el;

                    if (oc.ContainerType ==  ObjectContainer.ContainerTypes.SingleLineText || oc.ContainerType ==  ObjectContainer.ContainerTypes.MultiLineText || oc.ContainerType ==  ObjectContainer.ContainerTypes.Label)
                    {

                        if (oc.lstProductObects[0].Name != null)
                        {

                            switch (oc.lstProductObects[0].Name)
                            {
                                case "CompanyName":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Company;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Company;
                                        break;
                                    }
                                case "CompanyMessage":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.CompanyMessage;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.CompanyMessage;
                                        break;
                                    }
                                case "Name":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Name;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Name;
                                        break;
                                    }
                                case "Title":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Title;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Title;
                                        break;
                                    }
                                case "AddressLine1":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Address1;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Address1;
                                        break;
                                    }
                                case "AddressLine2":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Address2;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Address2;
                                        break;
                                    }
                                case "AddressLine3":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Address3;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Address3;
                                        break;
                                    }
                                case "Phone":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Telephone;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Telephone;
                                        break;
                                    }
                                case "Fax":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Fax;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Fax;
                                        break;
                                    }
                                case "Email":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Email;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Email;
                                        break;
                                    }
                                case "Website":
                                    {
                                        ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Website;
                                        ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Website;
                                        break;
                                    }
                            }


                        }
                    }
                }
            }

            if (tbDesignPages.Items[1] != null)
            {
                tbItem = (TabItem)tbDesignPages.Items[1];  //iterating side 1 /page 1
                page1 = (PageContainer)tbItem.Content;

                foreach (UIElement el in page1.DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer oc = (ObjectContainer)el;

                        if (oc.ContainerType == ObjectContainer.ContainerTypes.SingleLineText || oc.ContainerType == ObjectContainer.ContainerTypes.MultiLineText || oc.ContainerType == ObjectContainer.ContainerTypes.Label)
                        {

                            if (oc.lstProductObects[0].Name != null)
                            {

                                switch (oc.lstProductObects[0].Name)
                                {
                                    case "CompanyName":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Company;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Company;
                                            break;
                                        }
                                    case "CompanyMessage":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.CompanyMessage;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.CompanyMessage;
                                            break;
                                        }
                                    case "Name":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Name;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Name;
                                            break;
                                        }
                                    case "Title":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Title;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Title;
                                            break;
                                        }
                                    case "AddressLine1":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Address1;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Address1;
                                            break;
                                        }
                                    case "AddressLine2":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Address2;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Address2;
                                            break;
                                        }
                                    case "AddressLine3":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Address3;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Address3;
                                            break;
                                        }
                                    case "Phone":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Telephone;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Telephone;
                                            break;
                                        }
                                    case "Fax":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Fax;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Fax;
                                            break;
                                        }
                                    case "Email":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Email;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Email;
                                            break;
                                        }
                                    case "Website":
                                        {
                                            ((TextBlock)oc.SelContainerPanel.Children[0]).Text = oQuickText.Website;
                                            ((TextBox)oc.SelContainerPanel.Children[1]).Text = oQuickText.Website;
                                            break;
                                        }
                                }


                            }
                        }
                    }
                }
            }

            if (CustomerID != 0 && ContactID != 0)
            {
                DesignerDisabl2.Visibility = Visibility.Visible;
                ProgressBar1.IsIndeterminate = true;
                ProgessTxt.Text = "Saving Data";
                ProgressPanel.Visibility = Visibility.Visible;

                ProductServiceClient oSvc = new ProductServiceClient();
                oSvc.UpdateQuickTextCompleted += new EventHandler<UpdateQuickTextCompletedEventArgs>(oSvc_UpdateQuickTextCompleted);
                
                oSvc.UpdateQuickTextAsync(oQuickText, ContactID);
            }
            else   //without saving close
            {
                DesignerDisabl2.Visibility = Visibility.Collapsed;
                winQuickText.IsOpened = false;
            }

        }

        private void winQuickText_PopupWinOpenClose(object sender, bool IsOpen)
        {
            if (!IsOpen)
            {
                DesignerDisabl2.Visibility = Visibility.Collapsed;
            }
        }

        void oSvc_UpdateQuickTextCompleted(object sender, UpdateQuickTextCompletedEventArgs e)
        {
            if (!e.Result)
            {
                MessageBox.Show("Error saving data");
            }

                DesignerDisabl2.Visibility = Visibility.Collapsed;
                winQuickText.IsOpened = false;

                DesignerDisabl2.Visibility = Visibility.Collapsed;
                ProgressBar1.IsIndeterminate = false;
                ProgessTxt.Text = "Saving Data";
                ProgressPanel.Visibility = Visibility.Collapsed;
            
            
        }

        private void btnQCancel_Click(object sender, RoutedEventArgs e)
        {
            DesignerDisabl2.Visibility = Visibility.Collapsed;
            winQuickText.IsOpened = false;
        }

        private void ddpPDFCanvas_DropdownBeforeOpenClose(object sender, bool IsOpen)
        {
            if (IsOpen)
            {
                CloseDropdowns();

                ddpPDFCanvas.IsOpened = true;
            }
        }

        private void btnExpandDragDropText_Click(object sender, RoutedEventArgs e)
        {
            if (PanelDragDropFields.Visibility == System.Windows.Visibility.Collapsed)
            {
                PanelDragDropFields.Visibility = System.Windows.Visibility.Visible;
                btnExpandDragDropText.Content = "Hide Quick Text Fields";
            }
            else
            {
                PanelDragDropFields.Visibility = System.Windows.Visibility.Collapsed;
                btnExpandDragDropText.Content = "Insert Quick Text Fields";
            }

        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            ZoomSlider.Value = 1;
            #region Object
            ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
            objObject.ObjectType = 5;
            objObject.ColorC = 0;
            objObject.ColorM = 0;
            objObject.ColorY = 0;
            objObject.ColorK = 100;
            objObject.Allignment = 1;
            objObject.ColorStyleID = 0;
            objObject.ColorType = 3;
            objObject.PositionX = 0;
            objObject.PositionY = 0;
            objObject.MaxWidth = 100;
            objObject.MaxHeight = 5;
            objObject.RotationAngle = 0;

            objObject.Name = Name;


            objObject.ContentString = "Line";
           
            objObject.FontSize = txtFontSize.Value;
            objObject.IsBold = false;
            objObject.IsItalic = false;
            objObject.IsUnderlinedText = false;


            objObject.MaxCharacters = 0;
            objObject.IsFontCustom = true;
            objObject.PageNo = 1;
            //objObject.SpaceAfter = 0;
            objObject.DisplayOrderPdf = 0;
            objObject.DisplayOrderTxtControl = 0;
            objObject.OffsetX = 0;
            objObject.OffsetY = 0;
            objObject.ColorName = string.Empty;
            objObject.ExField1 = string.Empty;
            objObject.ExField2 = string.Empty;
            objObject.SpotColorName = string.Empty;



            objObject.IsEditable = true;
            objObject.IsPositionLocked = false;
            objObject.IsHidden = false;

            objObject.IsNewLine = false;
            #endregion
            //Point ImgPos = new Point(e.GetPosition(SelectedPage.DesignArea).X - ImgPoint.X, e.GetPosition(SelectedPage.DesignArea).Y - ImgPoint.Y);
            //Size ImgSize = new Size(Convert.ToDouble(uimg.ImageWidth), Convert.ToDouble(uimg.ImageHeight));
            SelectedPage.AddVectorLine("CtlContainerTxt_" + CtlIdx.ToString(), "Line_" + CtlIdx.ToString(), -1, SelectedPage.brdDesign.ActualWidth / 2, SelectedPage.brdDesign.ActualHeight / 2, 5, objObject);
            ZoomSlider.Value = 1;
                //AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), -1, img.Source, uimg.BackgroundImageRelativePath, ImgPos, ImgSize, null);
        }

        private void btnRect_Click(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            ZoomSlider.Value = 1;
            #region Object
            ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
            objObject.ObjectType = 6;
            objObject.ColorC = 0;
            objObject.ColorM = 0;
            objObject.ColorY = 0;
            objObject.ColorK = 100;
            objObject.Allignment = 1;
            objObject.ColorStyleID = 0;
            objObject.ColorType = 3;
            objObject.PositionX = 0;
            objObject.PositionY = 0;
            objObject.MaxWidth = 50;
            objObject.MaxHeight = 50;
            objObject.RotationAngle = 0;

            objObject.Name = Name;


            objObject.ContentString = "Line";

            objObject.FontSize = txtFontSize.Value;
            objObject.IsBold = false;
            objObject.IsItalic = false;
            objObject.IsUnderlinedText = false;


            objObject.MaxCharacters = 0;
            objObject.IsFontCustom = true;
            objObject.PageNo = 1;
            //objObject.SpaceAfter = 0;
            objObject.DisplayOrderPdf = 0;
            objObject.DisplayOrderTxtControl = 0;
            objObject.OffsetX = 0;
            objObject.OffsetY = 0;
            objObject.ColorName = string.Empty;
            objObject.ExField1 = string.Empty;
            objObject.ExField2 = string.Empty;
            objObject.SpotColorName = string.Empty;



            objObject.IsEditable = true;
            objObject.IsPositionLocked = false;
            objObject.IsHidden = false;

            objObject.IsNewLine = false;
            #endregion
            //Point ImgPos = new Point(e.GetPosition(SelectedPage.DesignArea).X - ImgPoint.X, e.GetPosition(SelectedPage.DesignArea).Y - ImgPoint.Y);
            //Size ImgSize = new Size(Convert.ToDouble(uimg.ImageWidth), Convert.ToDouble(uimg.ImageHeight));
            SelectedPage.AddVectorRectangle("CtlContainerTxt_" + CtlIdx.ToString(), "Rect_" + CtlIdx.ToString(), -1, SelectedPage.brdDesign.ActualWidth / 2, SelectedPage.brdDesign.ActualHeight / 2, objObject);
            ZoomSlider.Value = 1;
            //AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), -1, img.Source, uimg.BackgroundImageRelativePath, ImgPos, ImgSize, null);
        }

        private void btnCircle_Click(object sender, RoutedEventArgs e)
        {
            CloseDropdowns();
            ZoomSlider.Value = 1;
            #region Object
            ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
            objObject.ObjectType = 6;
            objObject.ColorC = 0;
            objObject.ColorM = 0;
            objObject.ColorY = 0;
            objObject.ColorK = 100;
            objObject.Allignment = 1;
            objObject.ColorStyleID = 0;
            objObject.ColorType = 3;
            objObject.PositionX = 0;
            objObject.PositionY = 0;
            objObject.MaxWidth = 50;
            objObject.MaxHeight = 50;
            objObject.RotationAngle = 0;

            objObject.Name = Name;


            objObject.ContentString = "Line";

            objObject.FontSize = txtFontSize.Value;
            objObject.IsBold = false;
            objObject.IsItalic = false;
            objObject.IsUnderlinedText = false;


            objObject.MaxCharacters = 0;
            objObject.IsFontCustom = true;
            objObject.PageNo = 1;
            //objObject.SpaceAfter = 0;
            objObject.DisplayOrderPdf = 0;
            objObject.DisplayOrderTxtControl = 0;
            objObject.OffsetX = 0;
            objObject.OffsetY = 0;
            objObject.ColorName = string.Empty;
            objObject.ExField1 = string.Empty;
            objObject.ExField2 = string.Empty;
            objObject.SpotColorName = string.Empty;



            objObject.IsEditable = true;
            objObject.IsPositionLocked = false;
            objObject.IsHidden = false;

            objObject.IsNewLine = false;
            #endregion
            //Point ImgPos = new Point(e.GetPosition(SelectedPage.DesignArea).X - ImgPoint.X, e.GetPosition(SelectedPage.DesignArea).Y - ImgPoint.Y);
            //Size ImgSize = new Size(Convert.ToDouble(uimg.ImageWidth), Convert.ToDouble(uimg.ImageHeight));
            SelectedPage.AddVectorEllipse("CtlContainerTxt_" + CtlIdx.ToString(), "Ellipse_" + CtlIdx.ToString(), -1, SelectedPage.brdDesign.ActualWidth / 2, SelectedPage.brdDesign.ActualHeight / 2, objObject);
            ZoomSlider.Value = 1;
            //AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), -1, img.Source, uimg.BackgroundImageRelativePath, ImgPos, ImgSize, null);
        }

        private void tbDesignPages_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedPage.UnSelAllObject();
            winEditorNew.Visibility = System.Windows.Visibility.Collapsed;
            CloseDropdowns();
        }

        private void btnCropImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!SelObjPro && SelectedPage != null)
                {
                    foreach (UIElement el in SelectedPage.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer oc = (ObjectContainer)el;
                            if (oc.Selected && !oc.MouseDown && oc.ContainerType == ObjectContainer.ContainerTypes.Image)
                            {
                                ImageCropPane oCropWindow = new ImageCropPane();
                                //UserControls.CropImage ctlCropImage = new UserControls.CropImage();
                                oCropWindow.oc = oc;
                                oCropWindow.ImgName = oc.ImagePath;// uimg.ImageAbsolutePath.Substring(uimg.ImageAbsolutePath.IndexOf("UserData"), uimg.ImageAbsolutePath.Length - uimg.ImageAbsolutePath.IndexOf("UserData"));
                                //ctlCropImage.ImgSize = new Size(oc.SelectedObect.MaxWidth, oc.SelectedObect.MaxHeight);
                                //oc.ContainerImage.Stretch = Stretch.None;
                                oCropWindow.ImgSize = new Size(oc.ContainerImage.ActualWidth, oc.ContainerImage.ActualHeight);
                                //oc.ContainerImage.Stretch = Stretch.Fill;
                                oCropWindow.MaxSize = new Size(SelectedPage.DesignArea.ActualWidth, SelectedPage.DesignArea.ActualHeight);
                                oCropWindow.ImgSource = oc.ContainerImage.Source;
                                oCropWindow.OnCropClick += new ImageCropPane.CropImageCropClick_EventHandler(ctlCropImage_OnCropClick);


                                winEditorNew.Visibility = System.Windows.Visibility.Collapsed;


                                //double cropPos = (LayoutMain.ActualWidth / 2) - (oCropWindow.Width / 2);
                                //if (cropPos > 0)
                                //    pwnCrop.SetValue(Canvas.LeftProperty, cropPos);

                                //cropPos = 0;
                                //cropPos = (LayoutMain.ActualHeight / 2) - (pwnCrop.Height / 2);
                                //if (cropPos > 0)
                                //    pwnCrop.SetValue(Canvas.TopProperty, cropPos);



                                oCropWindow.Show();
                                
                                //ctlCropImage.ResetImage();

                                //    ctlCropImage.ShowException = ShowException;
                                


                            }
                               

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::btnCropImage_Click::" + ex.ToString());
            }
        }


        private void ctlCropImage_OnCropClick(object sender, ObjectContainer oc, double ImageWidth, double ImageHeight)
        {
            try
            {

                System.Windows.Media.Imaging.BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, oc.ImagePath));
                bimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                oc.ContainerImage.Source = bimg;
                oc.ContainerRoot.Width = ImageWidth;
                oc.ContainerRoot.Height = ImageHeight;


                foreach (TemplateBackgroundImages item in lstImagesList.ItemsSource)
                {
                    if (item.BackgroundImageRelativePath == oc.ImagePath)
                    {
                        item.BackgroundImageRelativePath = "";
                        item.BackgroundImageRelativePath = oc.ImagePath;// +"?cacheoverride=" + imgCropCounter.ToString();
                        break;
                    }
                    
                }


                TabItem tbItem = null;
                tbItem = (TabItem)tbDesignPages.Items[0];
                PageContainer objPage = (PageContainer)tbItem.Content;


                //sync all existing instances of same image
                foreach (UIElement el in objPage.DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer toc = (ObjectContainer)el;
                        if (!toc.Selected && toc.ContainerType == ObjectContainer.ContainerTypes.Image)
                        {
                            string imgpath = ((BitmapImage)toc.ContainerImage.Source).UriSource.OriginalString;
                            if ((imgpath == "/" + oc.ImagePath || imgpath == oc.ImagePath || imgpath == HtmlPage.Document.DocumentUri.ToString() + oc.ImagePath))
                            {

                                BitmapImage btempimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, oc.ImagePath)); //+ "?cacheoverride=" + imgCropCounter.ToString())
                                btempimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;

                                toc.ContainerImage.Source = btempimg;
                                //&& toc.ContainerImage.Source
                            }
                        }
                    }
                }

                tbItem = null;
                tbItem = (TabItem)tbDesignPages.Items[1];
                objPage = (PageContainer)tbItem.Content;


                //sync all existing instances of same image
                foreach (UIElement el in objPage.DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer toc = (ObjectContainer)el;
                        if (!toc.Selected && toc.ContainerType == ObjectContainer.ContainerTypes.Image)
                        {
                            string imgpath = ((BitmapImage)toc.ContainerImage.Source).UriSource.OriginalString;
                            if ((imgpath == "/" + oc.ImagePath || imgpath == oc.ImagePath || imgpath == HtmlPage.Document.DocumentUri.ToString() + oc.ImagePath))
                            {

                                BitmapImage btempimg = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, oc.ImagePath)); //+ "?cacheoverride=" + imgCropCounter.ToString())
                                btempimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;

                                toc.ContainerImage.Source = btempimg;
                                //&& toc.ContainerImage.Source
                            }
                        }
                    }
                }

                imgCropCounter++;

                
                


                //if (ImageName != "" && SelectedPage != null)
                //{
                //    System.Windows.Media.Imaging.BitmapImage bimg = new System.Windows.Media.Imaging.BitmapImage(new Uri("/" + ImagePath, UriKind.Relative));
                //    if ((ImgPoint.X + ImageWidth) > SelectedPage.DesignArea.Width)
                //        ImgPoint.X = ImgPoint.X - ((ImgPoint.X + ImageWidth) - SelectedPage.DesignArea.Width);
                //    if ((ImgPoint.Y + ImageHeight) > SelectedPage.DesignArea.Height)
                //        ImgPoint.Y = ImgPoint.Y - ((ImgPoint.Y + ImageHeight) - SelectedPage.DesignArea.Height);
                //    Point ImgPos = new Point(ImgPoint.X, ImgPoint.Y);
                //    Size ImgSize = new Size(ImageWidth, ImageHeight);
                //    SelectedPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Image_" + CtlIdx.ToString(), -1, bimg, ImagePath, ImgPos, ImgSize, null);
                //    CtlIdx++;
                //}

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ctlCropImage_OnCropClick::" + ex.ToString());
            }
        }

        private void btnLogo_Click(object sender, RoutedEventArgs e)
        {
            
             BitmapImage bimg = new BitmapImage(new Uri(HtmlPage.Document.DocumentUri, UriPrefix + "designer/upload-Logo.png"));
            

            Point ImgPos = new Point(60, 60);
            Size ImgSize = new Size(262, 198);

            webprintDesigner.ProductServiceReference.TemplateObjects objObject = new webprintDesigner.ProductServiceReference.TemplateObjects();
            objObject.ObjectID = 0;
            objObject.ParentId = 0;
            objObject.ObjectType = 8;
            objObject.PositionX = ImgPos.X;
            objObject.PositionY = ImgPos.Y;
            objObject.MaxWidth = ImgSize.Width;
            objObject.MaxHeight = ImgSize.Height;

            objObject.RotationAngle = 0;
            objObject.Name = "";


            objObject.IsEditable = true;
            objObject.IsPositionLocked = false;
            objObject.IsHidden = false;

            objObject.ContentString = "designer/upload-Logo.png";
            objObject.DisplayOrderTxtControl = 0;
            objObject.IsMandatory = false;
            objObject.IsRequireNumericValue = false;

            objObject.DisplayOrderPdf = 0;

            objObject.isSide2Object = false;
            objObject.Tint = 0;
            objObject.IsNewLine = false;
            objObject.OffsetX = 0;
            objObject.OffsetY = 0;
            objObject.VAllignment = 0;
            objObject.Allignment = 0;

            objObject.ColorName = string.Empty;
            objObject.ExField1 = string.Empty;
            objObject.ExField2 = string.Empty;
            objObject.SpotColorName = string.Empty;
            objObject.FontName = string.Empty;
            objObject.FontSize = 0;
            objObject.FontStyleID = 0;
            objObject.IsFontCustom = false;
            objObject.IsFontNamePrivate = false;
            objObject.TCtlName = string.Empty;
            objObject.PageNo = 1;

            SelectedPage.AddImgObject("CtlContainerTxt_" + CtlIdx.ToString(), "Logo_" + CtlIdx.ToString(), -1, bimg, objObject.ContentString, ImgPos, ImgSize, objObject, true);
            CtlIdx++;
            ddpLogo.IsOpened = false;
        }




        private void btnLogoUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images (*.jpg;*.png)|*.jpg;*.png";
            dlg.Multiselect = false;

            if ((bool)dlg.ShowDialog())
            {

                FileUpload oLogoUploader = new FileUpload(this.Dispatcher, new Uri(HtmlPage.Document.DocumentUri, UriPrefix + "FileUpload/FileUploadHandler.ashx"), dlg.File);
                oLogoUploader.ChunkSize = 9194304;
                oLogoUploader.StatusChanged += new EventHandler(oLogoUploader_StatusChanged);

                brdLogoLoader.Visibility = System.Windows.Visibility.Visible;
                ProgressBarLogo.IsIndeterminate = true;
                lblLogoProgress.Text = "Uploading Logo";

                oLogoUploader.Upload(objProduct.ProductID, 5, CustomerID);

                oLogoUploader = null;


            }
        }


        void oLogoUploader_StatusChanged(object sender, EventArgs e)
        {
            FileUpload fu = sender as FileUpload;
            if (fu.Status == FileUploadStatus.Complete)
            {

                QuickText oQuickText = DictionaryManager.AppObjects["QuickText"] as QuickText;
                BitmapImage oLogoImage = new BitmapImage(new Uri(HtmlPage.Document.DocumentUri, oQuickText.LogoPath.Substring(1)));
                oLogoImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                imgLogo.Source = oLogoImage;
                imgLogo.ImageOpened += new EventHandler<RoutedEventArgs>(imgLogo_ImageOpened);
                imgLogo.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(imgLogo_ImageFailed);



            }
        }

        void imgLogo_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            brdLogoLoader.Visibility = System.Windows.Visibility.Collapsed;
            ProgressBarLogo.IsIndeterminate = false;
            lblLogoMessage.Text = "Uploading Failed";
        }

        void imgLogo_ImageOpened(object sender, RoutedEventArgs e)
        {
            brdLogoLoader.Visibility = System.Windows.Visibility.Collapsed;
            ProgressBarLogo.IsIndeterminate = false;


            
            
        }

        

   

        
        

       

       

       

       

       

        

      

       
       

       
       

     

       

        //private Control FindControl(string CtlName)
        //{
        //    Control oc = null;
        //    try
        //    {
        //        if (SelectedPage != null)
        //        {
        //            foreach (Control el in SelectedPage.objPageTxtControl.CtrlsList.FindName(CtlName);)
        //            {
        //                //if (el.GetType().Name == "ObjectContainer")
        //                //{
        //                //if (((ObjectContainer)el).ContainerName != "" && ((ObjectContainer)el).ContainerName == CtlName)
        //                if (el.Name == CtlName)
        //                {
        //                    oc = el;
        //                    break;
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::FindControl::" + ex.ToString());
        //    }
        //    return oc;

        //}
       


        //private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    LayoutMain.Width = e.NewSize.Width;
        //    LayoutMain.Height = e.NewSize.Height;
        //    if (LayoutMain.Children.Count > 1)
        //    {

        //    }
        //}
    }


    public enum ProgressSource
    {
        UploadImage = 1
    }

    public enum FontSources
    {
        IsoLatedStorage = 1,
        MemoryStream = 2
    }

    public enum ZoomState
    {
        OriginalSize = 1,
        FitWindow = 2,
        Free = 3
    }

    

       
}

 