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
using webprintDesigner.EnhancedControls;

namespace webprintDesigner
{
    public partial class ImageCropPane : ChildWindow
    {
        public ImageCropPane()
        {
            InitializeComponent();
            //this.Loaded += new RoutedEventHandler(CropImage_Loaded);
        }




      

        public System.ServiceModel.BasicHttpBinding BindUsrSrv;
        public System.ServiceModel.EndpointAddress EPUsrSrv;
        public bool ShowException = false;
        private Size MaxCropSize;

        public delegate void CropImageCropClick_EventHandler(object sender, ObjectContainer oc, double ImageWidth, double ImageHeight);
        public event CropImageCropClick_EventHandler OnCropClick;

        #region Dependency Properties
        public readonly DependencyProperty ImgSourceProperty = DependencyProperty.Register("ImgSource", typeof(ImageSource), typeof(ImageCropPane), new PropertyMetadata(ImageChanged));
        public ImageSource ImgSource
        {
            get { return (ImageSource)GetValue(ImgSourceProperty); }
            set
            {
                SetValue(ImgSourceProperty, value); 
            
               
            }
        }
        public readonly DependencyProperty ImgSizeProperty = DependencyProperty.Register("ImgSize", typeof(Size), typeof(ImageCropPane), new PropertyMetadata(new Size(0d, 0d),null));
        public Size ImgSize
        {
            get { return (Size)GetValue(ImgSizeProperty); }
            set { SetValue(ImgSizeProperty, value); }
        }
        public readonly DependencyProperty MaxSizeProperty = DependencyProperty.Register("MaxSize", typeof(Size), typeof(ImageCropPane), new PropertyMetadata(new Size(0d, 0d), null));
        public Size MaxSize
        {
            get { return (Size)GetValue(MaxSizeProperty); }
            set { SetValue(MaxSizeProperty, value); }
        }
        public string ImgName { internal get; set; }
        private int CX, CY, CW, CH;
        public ObjectContainer oc { internal get; set; }


        #endregion

        private void LayoutRoot_LayoutUpdated(object sender, EventArgs e)
        {
            //ResetImage();
        }



        void CropImage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //MaxCropSize = new Size(0d, 0d);
                //ResetImage();
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropImage_Loaded::" + ex.ToString());
            }
        }

        static void  ImageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //try
            //{
          
            //}
            //catch (Exception ex)
            //{
            //    //if (ShowException)
            //        MessageBox.Show("::ImageChanged::" + ex.ToString());
            //}
        }
        public void ResetImage()
        {
            try
            {
                if (ImgSource != null)
                {
                    //LayoutRoot.UpdateLayout();
                    //imgCrop.Source = ImgSource;
                   
                    
                    //imgCrop.Width = ImgSize.Width;
                    //imgCrop.Height = ImgSize.Height;


                 



                    cnvCrop.Width = imgCrop.ActualWidth;
                    cnvCrop.Height = imgCrop.ActualHeight;


                    
                 


                    if (imgCrop.ActualWidth < MaxSize.Width)
                        MaxCropSize.Width = imgCrop.ActualWidth;
                    else
                        MaxCropSize.Width = MaxSize.Width;


                    if (imgCrop.ActualHeight < MaxSize.Height)
                        MaxCropSize.Height = imgCrop.ActualHeight;
                    else
                        MaxCropSize.Height = MaxSize.Height;


                    CropBoxRoot.Width = MaxCropSize.Width;// imgCrop.ActualWidth;
                    CropBoxRoot.Height = MaxCropSize.Height;// imgCrop.ActualHeight;

                    double ratio = imgCrop.ActualWidth / imgCrop.ActualHeight;



                    if (cnvCrop.Height > cnvCrop.Width)   ///height is greater
                    {
                        //if (cnvCrop.Height > LayoutRoot.ActualHeight && cnvCrop.Width > LayoutRoot.ActualWidth)
                        //{
                        //    CanvasScaleTransform.ScaleY = 1 - Math.Abs(cnvCrop.Height - LayoutRoot.ActualHeight + 60) / cnvCrop.Height;
                        //    CanvasScaleTransform.ScaleX = 1 - Math.Abs(cnvCrop.Width - LayoutRoot.ActualWidth + 3) / cnvCrop.Width;
                        //}
                        //else 
                            if (cnvCrop.Height > LayoutRoot.ActualHeight)   //height is more than container then zoom to max height and width as per ratio
                        {
                            CanvasScaleTransform.ScaleY = 1 - Math.Abs(cnvCrop.Height - LayoutRoot.ActualHeight + 60) / cnvCrop.Height;
                            CanvasScaleTransform.ScaleX = CanvasScaleTransform.ScaleY * ratio;
                            //CropBoxRoot.Height = CropBoxRoot.Height * CanvasScaleTransform.ScaleY;
                            //MaxCropSize.Height = MaxCropSize.Height * CanvasScaleTransform.ScaleY;
                        }
                        else if (cnvCrop.Width > LayoutRoot.ActualWidth)     //
                        {


                            CanvasScaleTransform.ScaleX = 1 - Math.Abs(cnvCrop.Width - LayoutRoot.ActualWidth + 3) / cnvCrop.Width;
                            CanvasScaleTransform.ScaleY = CanvasScaleTransform.ScaleX * ratio;
                            
                        }
                        else
                        {
                            CanvasScaleTransform.ScaleY = 1;
                        }

                       

                    }
                    else  //width is greater
                    {

                        if (cnvCrop.Height > LayoutRoot.ActualHeight && cnvCrop.Width > LayoutRoot.ActualWidth)
                        {
                            CanvasScaleTransform.ScaleY = 1 - Math.Abs(cnvCrop.Height - LayoutRoot.ActualHeight + 60) / cnvCrop.Height;
                            CanvasScaleTransform.ScaleX = 1 - Math.Abs(cnvCrop.Width - LayoutRoot.ActualWidth + 3) / cnvCrop.Width;
                        }
                        else if (cnvCrop.Width > LayoutRoot.ActualWidth)
                        {
                            CanvasScaleTransform.ScaleX = 1 - Math.Abs(cnvCrop.Width - LayoutRoot.ActualWidth + 3) / cnvCrop.Width;
                            CanvasScaleTransform.ScaleY = CanvasScaleTransform.ScaleX * ratio;
                            
                            //CropBoxRoot.Width = CropBoxRoot.Width * CanvasScaleTransform.ScaleX;
                            //MaxCropSize.Width = MaxCropSize.Width * CanvasScaleTransform.ScaleX;
                        }
                        else if (cnvCrop.Height > LayoutRoot.ActualHeight)
                        {

                            CanvasScaleTransform.ScaleY = 1 - Math.Abs(cnvCrop.Height - LayoutRoot.ActualHeight + 60) / cnvCrop.Height;

                            CanvasScaleTransform.ScaleX = CanvasScaleTransform.ScaleY * ratio;
                        }
                        else
                        {
                            CanvasScaleTransform.ScaleX = 1;
                            CanvasScaleTransform.ScaleY = 1;
                        }



                    }

                   

                    

                    
                   


                    if (CanvasScaleTransform.ScaleX < 0.5 || CanvasScaleTransform.ScaleY < 0.5)
                        CropBoxBrd.BorderThickness = new Thickness(4);
                    else
                        CropBoxBrd.BorderThickness = new Thickness(2);

                    CropBoxRoot.UpdateLayout();
                    LayoutRoot.UpdateLayout();
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ResetImage::" + ex.ToString());
            }
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                imgCrop.Source = ImgSource;
                //imgCrop.Stretch = Stretch.None;
                imgCrop.UpdateLayout();
                //double ratio = imgCrop.ActualWidth / imgCrop.ActualHeight;
                //imgCrop.Width = imgCrop.Height * ratio;
                //imgCrop.UpdateLayout();


                //double cropPos = (LayoutRoot.ActualWidth / 2) - (ProgressPanel.Width / 2);
                //if (cropPos > 0)
                //    ProgressPanel.SetValue(Canvas.LeftProperty, cropPos);

                //cropPos = 0;
                //cropPos = (LayoutMain.ActualHeight / 2) - (pwnCrop.Height / 2);
                //if (cropPos > 0)
                //    pwnCrop.SetValue(Canvas.TopProperty, cropPos);
               
                
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::LayoutRoot_SizeChanged::" + ex.ToString());
            }
        }
        #region Mouse Events
        private bool MouseDown = false;
        private Point MousePoiner;
        private bool MouseDownResizing = false;
        private bool MouseDownHResizing = false;
        private Size OriginalSize;
        private void CropBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UIElement el = (UIElement)sender;
                el.CaptureMouse();
                MouseDown = true;
                MousePoiner = e.GetPosition(CropBoxRoot);
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBox_MouseLeftButtonDown::" + ex.ToString());
            }
        }

        private void CropBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MouseDown = false;
                OriginalSize.Width = CropBoxRoot.ActualWidth;
                OriginalSize.Height = CropBoxRoot.ActualHeight;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBox_MouseLeftButtonUp::" + ex.ToString());
            }
        }
        private void CropBox_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (MouseDown && !MouseDownResizing && !MouseDownHResizing)
                {
                    Point pox = e.GetPosition(cnvCrop);
                    if ((pox.X - MousePoiner.X) >= 0 && (pox.X - MousePoiner.X + CropBoxRoot.ActualWidth) <= cnvCrop.Width)
                        CropBoxRoot.SetValue(Canvas.LeftProperty, pox.X - MousePoiner.X);
                    if ((pox.Y - MousePoiner.Y) >= 0 && (pox.Y - MousePoiner.Y + CropBoxRoot.ActualHeight) <= cnvCrop.Height)
                        CropBoxRoot.SetValue(Canvas.TopProperty, pox.Y - MousePoiner.Y);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBox_MouseMove::" + ex.ToString());
            }
        }
        private void CropBox_LostMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                MouseDown = false;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBox_LostMouseCapture::" + ex.ToString());
            }
        }

        private void CropBoxResizeRM_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                UIElement el = (UIElement)sender;
                el.CaptureMouse();
                MouseDownResizing = true;
                MousePoiner = e.GetPosition(CropBoxRoot);
                OriginalSize.Width = CropBoxRoot.ActualWidth;
                OriginalSize.Height = CropBoxRoot.ActualHeight;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBoxResizeRM_MouseLeftButtonDown::" + ex.ToString());
            }
        }
        private void CropBoxResizeRM_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MouseDownResizing = false;
                OriginalSize.Width = CropBoxRoot.ActualWidth;
                OriginalSize.Height = CropBoxRoot.ActualHeight;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBoxResizeRM_MouseLeftButtonUp::" + ex.ToString());
            }
        }
        private void CropBoxResizeRM_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (MouseDownResizing)
                {
                    Point pox = e.GetPosition(CropBoxRoot);
                    Point pox2 = e.GetPosition(cnvCrop);
                    if ((pox.X - MousePoiner.X + OriginalSize.Width) <= MaxCropSize.Width && (pox.X - MousePoiner.X + OriginalSize.Width) > 10 && (pox2.X - MousePoiner.X + OriginalSize.Width) <= cnvCrop.Width)
                        CropBoxRoot.Width = pox.X - MousePoiner.X + OriginalSize.Width;
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBoxResizeRM_MouseMove::" + ex.ToString());
            }
        }

        private void CropBoxResizeBM_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UIElement el = (UIElement)sender;
                el.CaptureMouse();
                MouseDownHResizing = true;
                MousePoiner = e.GetPosition(CropBoxRoot);
                OriginalSize.Width = CropBoxRoot.ActualWidth;
                OriginalSize.Height = CropBoxRoot.ActualHeight;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBoxResizeBM_MouseLeftButtonDown::" + ex.ToString());
            }
        }
        private void CropBoxResizeBM_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MouseDownHResizing = false;
                OriginalSize.Width = CropBoxRoot.ActualWidth;
                OriginalSize.Height = CropBoxRoot.ActualHeight;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBoxResizeBM_MouseLeftButtonUp::" + ex.ToString());
            }
        }
        private void CropBoxResizeBM_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (MouseDownHResizing)
                {
                    Point pox = e.GetPosition(CropBoxRoot);
                    Point pox2 = e.GetPosition(cnvCrop);
                    if ((pox.Y - MousePoiner.Y + OriginalSize.Height) <= MaxCropSize.Height && (pox.Y - MousePoiner.Y + OriginalSize.Height) > 10 && (pox2.Y - MousePoiner.Y + OriginalSize.Height) <= cnvCrop.Height)
                        CropBoxRoot.Height = pox.Y - MousePoiner.Y + OriginalSize.Height;
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::CropBoxResizeBM_MouseMove::" + ex.ToString());
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Parent.GetType().Name == "PopupWin")
                {
                    PrintFlow.SilverlightControls.PopupWin pw = this.Parent as PrintFlow.SilverlightControls.PopupWin;
                    pw.IsOpened = false;
                    
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::Cancel_Click::" + ex.ToString());
            }
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ImgName != "")
                {
                    double CrpX = (double)CropBoxRoot.GetValue(Canvas.LeftProperty);
                    double CrpY = (double)CropBoxRoot.GetValue(Canvas.TopProperty);
                    if (CrpX >= 0 && CrpY >= 0 && CropBoxRoot.Width <= MaxCropSize.Width && CropBoxRoot.Height <= MaxCropSize.Height)
                    {
                        ProductServiceReference.ProductServiceClient oSvc = new ProductServiceReference.ProductServiceClient();
                        oSvc.CropImageCompleted += new EventHandler<ProductServiceReference.CropImageCompletedEventArgs>(oSvc_CropImageCompleted);
                        
                        int.TryParse(Math.Round(CrpX, 0).ToString(), out CX);
                        int.TryParse(Math.Round(CrpY, 0).ToString(), out CY);
                        int.TryParse(Math.Round(CropBoxRoot.Width, 0).ToString(), out CW);
                        int.TryParse(Math.Round(CropBoxRoot.Height, 0).ToString(), out CH);
                        oSvc.CropImageAsync(ImgName, CX, CY, CW, CH);
                        ProgressPanel.Visibility = System.Windows.Visibility.Visible;
                        ProgessTxt.Text = "Processing...";
                        ProgressBar1.IsIndeterminate = true;
                        //objClnt.CropImageAsync(ImgName, CX, CY, CW, CH, App.DesignerMode);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::Ok_Click::" + ex.ToString());
            }
        }



        void oSvc_CropImageCompleted(object sender, ProductServiceReference.CropImageCompletedEventArgs e)
        {
            try
            {

                ProgressPanel.Visibility = System.Windows.Visibility.Collapsed;
                ProgessTxt.Text = "Processing...";
                ProgressBar1.IsIndeterminate = false;

                if (e.Result)
                {
                    OnCropClick(this, this.oc, this.CW, this.CH);
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }

                //if (usrImg != null && OnCropClick != null)
                //{
                //    if (usrImg.ImageAbsolutePath != "")
                //        OnCropClick(this, usrImg.ImageAbsolutePath, usrImg.ImageRelativePath, usrImg.ImageWidth, usrImg.ImageHeight);
                //}
                //if (this.Parent.GetType().Name == "PopupWin")
                //{
                //    PrintFlow.SilverlightControls.PopupWin pw = this.Parent as PrintFlow.SilverlightControls.PopupWin;
                //    pw.IsOpened = false;
                //}
                ////MessageBox.Show("Crop Completed :" + rs);
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::oSvc_CropImageCompleted::" + ex.ToString());
            }
        }

        #endregion

       

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void imgCrop_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void imgCrop_ImageOpened(object sender, RoutedEventArgs e)
        {
            this.ResetImage();
        }

      
    

       

       
    }
}

