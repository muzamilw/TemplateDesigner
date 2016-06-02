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
using System.Windows.Browser;
using Telerik.WebUI;

namespace webprintDesigner
{
    public partial class PreviewPane : ChildWindow
    {
        private int TotalPages = 0;
        private int LoadedPages = 0;


        private int TemplateID = 0;
        private Point pt;
        Size _size = new Size();
        private bool isBookZoomed = false;
        double InitialZoomX = 0;
        double InitialzoomY = 0;


        public PreviewPane()
        {
            InitializeComponent();

         

           

        }

        public PreviewPane(int _TemplateID)
        {
            InitializeComponent();
            ProgressBarStatus(true);
            TemplateID = _TemplateID;

            this.SizeChanged += new SizeChangedEventHandler(PreviewPane_SizeChanged);
        }

        void PreviewPane_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ProductServiceClient oSvc = new ProductServiceClient();
            oSvc.GetProductByIdCompleted += new EventHandler<GetProductByIdCompletedEventArgs>(oSvc_GetProductByIdCompleted);
            oSvc.GetProductByIdAsync(TemplateID);

            PreviewBookCanvas.Width = LayoutRoot.ActualWidth - 10;
            PreviewBookCanvas.Height = LayoutRoot.ActualHeight - 10;

        }

        void oSvc_GetProductByIdCompleted(object sender, GetProductByIdCompletedEventArgs e)
        {
            if (e.Result != null)
            {


                Templates oTemplate = e.Result;


               

                //PreviewBookCanvas.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                //PreviewBookCanvas.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

                //PreviewBookCanvas.Width = (webprintDesigner.Common.PointToPixel(oTemplate.PDFTemplateWidth.Value) );
                //PreviewBookCanvas.Height = webprintDesigner.Common.PointToPixel(oTemplate.PDFTemplateHeight.Value);

                brd.Width = (webprintDesigner.Common.PointToPixel(oTemplate.PDFTemplateWidth.Value * 2));
                brd.Height =  webprintDesigner.Common.PointToPixel(oTemplate.PDFTemplateHeight.Value);


                double WidthToHeightRatio = brd.Width / brd.Height;

                if (brd.Width > PreviewBookCanvas.ActualWidth && brd.Height > PreviewBookCanvas.ActualHeight)
                {
                    brd.Width = PreviewBookCanvas.ActualWidth * 0.9;
                    brd.Height = (PreviewBookCanvas.ActualWidth * 0.9) / WidthToHeightRatio;
                }
                if (brd.Width >  PreviewBookCanvas.ActualWidth)
                {
                    brd.Width = PreviewBookCanvas.ActualWidth * 0.9;
                    brd.Height = (PreviewBookCanvas.ActualWidth * 0.9) / WidthToHeightRatio;


                }
                else if (brd.Height > PreviewBookCanvas.ActualHeight)
                {
                    brd.Height = PreviewBookCanvas.ActualHeight * 0.8;
                    brd.Width = (PreviewBookCanvas.ActualHeight * 0.8) * WidthToHeightRatio;

                }
                


                PreviewBook.Width = brd.Width;
                PreviewBook.Height = brd.Height;

                

                Shadow.Height = PreviewBook.Height;
                Shadow.Width = PreviewBook.Width;

                //Shadow.SetValue(Canvas.LeftProperty, (PreviewBookCanvas.ActualWidth / 2));
                

                //ShadowBase.X = (PreviewBookCanvas.ActualWidth / 2) - (PreviewBook.Width / 2);
                //Shadow.Width = PreviewBook.Width;

                

                InitialZoomX = 1;
                InitialzoomY = 1;

               
               

                //brd.SetValue(Canvas.LeftProperty, (PreviewBookCanvas.ActualWidth / 2) - (brd.ActualWidth / 2));
                //brd.SetValue(Canvas.TopProperty, (PreviewBookCanvas.ActualHeight / 2) - (brd.ActualHeight /2 ));
                //Shadow.SetValue(Canvas.TopProperty, PreviewBook.Height );





                Image oImg1 = new Image();

                oImg1.ImageOpened += new EventHandler<RoutedEventArgs>(oImg_ImageOpened);
                
                System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, "designer/products/" + oTemplate.ProductID.ToString() + "/p1.png"));
                bi.CreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions.IgnoreImageCache;
                oImg1.Source = bi;
                PreviewBook.Items.Add(oImg1);

                Image oImg2 = new Image();
                oImg1.ImageOpened += new EventHandler<RoutedEventArgs>(oImg_ImageOpened);
                bi = new System.Windows.Media.Imaging.BitmapImage(new Uri(HtmlPage.Document.DocumentUri, "designer/products/" + oTemplate.ProductID.ToString() + "/p2.png"));
                bi.CreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions.IgnoreImageCache;
                oImg2.Source = bi;
                PreviewBook.Items.Add(oImg2);

                TotalPages = 2;

              

                //RightNav.SetValue(Canvas.LeftProperty, PreviewBookCanvas.ActualWidth - 50);
                //RightNav.SetValue(Canvas.TopProperty, PreviewBookCanvas.ActualHeight / 2);

                RightNav.Visibility = System.Windows.Visibility.Visible;

                //VisualStateManager.GoToState(RightNav, "open", true);

                //LeftNav.SetValue(Canvas.LeftProperty, 0d);
                //LeftNav.SetValue(Canvas.TopProperty, PreviewBookCanvas.ActualHeight / 2);

                LeftNav.Visibility = System.Windows.Visibility.Visible;
                
                BookZoomStoryboardX.To = InitialZoomX;
                BookZoomStoryboardY.To = InitialzoomY;

                BookZoomStoryboard.Begin();

            }
        }

        void oImg_ImageOpened(object sender, RoutedEventArgs e)
        {
            lock (LoadedPages.ToString())
            {
                LoadedPages++;

                if (LoadedPages == TotalPages)
                {
                    ProgressBarStatus(false);
                }
            }
            
        }

      

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(CompositionTarget_Rendering);
            this.DialogResult = true;
        }



        private void LayoutRoot_MouseMove(object sender, MouseEventArgs e)
        {


            //3d Projection Calculation
            if (btnToggle3D.IsChecked.Value)
            {
                pt = e.GetPosition(PreviewBookCanvas);
                _size = PreviewBookCanvas.RenderSize;

                double CenterX = PreviewBookCanvas.ActualWidth / 2;
                double CenterY = PreviewBookCanvas.ActualHeight / 2;

                double xOffSet = (CenterX - pt.X) / 100;
                double yOffSet = (CenterY - pt.Y) / 100;


                pos.Text = xOffSet.ToString() + "    " + yOffSet.ToString();
                PreviewBook.ChangeProjection(new Point(xOffSet, yOffSet));


                //shadow calculation in 3d Mode
                pt = e.GetPosition(PreviewBookCanvas);
                pt.X /= _size.Width;
                pt.Y /= _size.Height;
                //LightAngle.GradientOrigin = pt;


                pt.X -= (pt.X * 2);
                pt.X += 1;
                pt.X *= _size.Width;
                pt.X -= _size.Width / 2;
                ShadowAngle.LocalOffsetX = pt.X * 0.7;


                pt.Y -= (pt.Y * 2);
                pt.Y += 1;
                pt.Y *= _size.Height;
                pt.Y -= _size.Height / 3;
                ShadowAngle.LocalOffsetY = pt.Y;
                pos.Text += "  " + pt.X.ToString() + "    " + pt.Y.ToString();
            }
            pt = e.GetPosition(PreviewBookCanvas);




        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {


            
            if (isBookZoomed)
            {
                // a = Mouse X position
                double a = pt.X;
                // b = Width of Screen area
                double b = PreviewBookCanvas.ActualWidth;
                // c = Width of area to be moved
                double c = brd.ActualWidth * 2;

                double d = (b-c)/2;


                pos.Text = pt.X.ToString() + "   " + ZoomTransform.TranslateX.ToString() + "  b " + b.ToString() + " c " + c.ToString();
                // if movable area is bigger than screen area, use '(a/b)*(b-c)' equation to position movable area based on mouse co-ordinates
                if (c > b)
                {
                    
                    //PreviewBook.SetValue(Canvas.LeftProperty, (a / b) * (b - c));
                    ZoomTransform.TranslateX = (((a / b) * (b - c)) - d) * 1.5;
                    pos.Text = pt.X.ToString() + "   " + ZoomTransform.TranslateX.ToString() + "  b " + b.ToString() + " c " +  c.ToString();
                }
                // if movable area is smaller than screen area, use '(b-c)/2' equation to position movable area in center of screen
                //else
                //    ZoomTransform.TranslateX = (b - c) / 2;


                a = pt.Y;
                // b = Width of Screen area
                b = PreviewBookCanvas.ActualHeight;
                // c = Width of area to be moved
                c = brd.ActualHeight * 2;

                d = (b - c) / 2;
                // if movable area is bigger than screen area, use '(a/b)*(b-c)' equation to position movable area based on mouse co-ordinates
                if (c > b)
                    ZoomTransform.TranslateY = (((a / b) * (b - c)) - d)* 1.5;
                // if movable area is smaller than screen area, use '(b-c)/2' equation to position movable area in center of screen
                //else
                //    ZoomTransform.TranslateY = (b - c) / 2;


            }
        }

        private void LayoutRoot_LayoutUpdated(object sender, EventArgs e)
        {
            _size = this.RenderSize;
            //Shadow.Width = _size.Width;
            ////Shadow.Height = _size.Height;

            //ShadowBase.X = _size.Width / 2;
            //ShadowBase.Y = _size.Height / 2;
        }

        private void btnToggle3D_Checked(object sender, RoutedEventArgs e)
        {
            PreviewBook.Mode3DEnabled = true;
            Shadow.Visibility = System.Windows.Visibility.Visible;
            btnToggle3D.Content = "Switch back to 2D view";

        }

        private void btnToggle3D_Unchecked(object sender, RoutedEventArgs e)
        {
            PreviewBook.Mode3DEnabled = false;
            Shadow.Visibility = System.Windows.Visibility.Visible;
            btnToggle3D.Content = "Switch to 3D view";
        }

        private void btnZoom_Checked(object sender, RoutedEventArgs e)
        {
            zoom(true);
            btnZoom.Content = "Zoom to full";
        }

        private void zoom(bool state)
        {
            if (state)
            {
                CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);

                BookZoomStoryboardX.To = 2;
                BookZoomStoryboardY.To = 2;

                BookZoomStoryboard.Begin();


                //ZoomTransform.ScaleX = 2;
                //ZoomTransform.ScaleY = 2;
                LayoutRoot.Cursor = Cursors.Hand;
                isBookZoomed = true;
            }
            else
            {
                CompositionTarget.Rendering -= new EventHandler(CompositionTarget_Rendering);
                BookZoomStoryboardX.To = InitialZoomX;
                BookZoomStoryboardY.To = InitialzoomY;

                ZoomTransform.TranslateX = 0;
                ZoomTransform.TranslateY = 0;

                BookZoomStoryboard.Begin();
                LayoutRoot.Cursor = Cursors.Arrow;
                isBookZoomed = false;
            }
            btnZoom.Content = "Zoom In";
        }

        private void btnZoom_Unchecked(object sender, RoutedEventArgs e)
        {
            zoom(false);
        }

        private void LayoutRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnZoom.IsChecked = false;
        }

        private void btnPanright_Click(object sender, RoutedEventArgs e)
        {
            ZoomTransform.TranslateX = 500;
        }


        private void btnPageNav_Click(object sender, RoutedEventArgs e)
        {
            Button obtn = (Button)sender;

            if (obtn.Tag.ToString() == "left")
            {
                PreviewBook.TurnPage(false);
            }
            else if (obtn.Tag.ToString() == "right")
            {
                PreviewBook.TurnPage(true);
            }

        }



        void ProgressBarStatus(bool Progress)
        {
            if (Progress)
            {
                ProgressPanel.SetValue(Canvas.LeftProperty, PreviewBookCanvas.ActualWidth / 2 - 90);
                ProgressPanel.SetValue(Canvas.TopProperty, PreviewBookCanvas.ActualHeight / 2 - 35);
                ProgressBar1.IsIndeterminate = true;
                ProgressPanel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ProgressPanel.Visibility = System.Windows.Visibility.Collapsed;
                ProgressBar1.IsIndeterminate = false;
            }
        }

        private void btnPDFPreview_Click(object sender, RoutedEventArgs e)
        {

            HtmlPage.Window.Eval("switchToPreview(1);");
        }


    }
}

