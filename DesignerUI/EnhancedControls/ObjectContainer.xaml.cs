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
using PrintFlow.SilverlightControls;
using System.Windows.Media.Imaging;
using webprintDesigner.ProductServiceReference;
using System.Text;


namespace webprintDesigner.EnhancedControls
{


    public enum AspectResize
    {
        LockedAspect = 1,
        Horizontal = 2,
        Vertical = 3
        
    }


   

    public partial class ObjectContainer : UserControl
    {
        private bool IsLoaded = false;
        public bool MouseDown;
        private bool MouseDownResizing;
        private bool MouseDownRotating;
        
        public Image ContainerImage;
        public bool Selected = false;
        public bool IsLockedPosition = false;  ///disa
        public bool IsLockedEditing = false;  ///disa
        public bool IsPrintable = false;
        ///
                                               ///
        public Size OriginalSize = new Size(0.0, 0.0);
        //public Point OriginalPostion = new Point(0.0, 0.0);
        public double ObjectAngle = 0.0;
        public double AngleOffset = 0.0;
        public Point MousePoiner = new Point(0.0, 0.0);
        public Size ScalingFactor = new Size(0.0, 0.0);
        
        private DoubleClickDetection _DoubleClickDetector = new DoubleClickDetection();

        #region "event declaration"
        public delegate void MouseObjectMove_EventHandler(object sender, MouseEventArgs e);
        public event MouseObjectMove_EventHandler MouseObjectMove_Event;



        public delegate void MouseObjectResizeRM_EventHandler(object sender, MouseEventArgs e, AspectResize aspect);
        public event MouseObjectResizeRM_EventHandler MouseObjectResizeRM_Event;

        public delegate void MouseObjectResizeLM_EventHandler(object sender, MouseEventArgs e);
        public event MouseObjectResizeLM_EventHandler MouseObjectResizeLM_Event;
        public delegate void MouseObjectRotate_EventHandler(object sender, MouseEventArgs e);
        public event MouseObjectRotate_EventHandler MouseObjectRotate_Event;

        public delegate void ObjectPosChange_EventHandler(object sender, Point e);
        public event ObjectPosChange_EventHandler ObjectPosChange_Event;

        public delegate void ContainerSelect_EventHandler(object sender, string ContainerName,  ContainerTypes ContainerType, Point MousePosition, Size ContainerSize);
        public event ContainerSelect_EventHandler ContainerSelect_Event;

        public delegate void ContainerUnSelect_EventHandler(object sender, string ContainerName, ContainerTypes ContainerType, bool mode);
        public event ContainerUnSelect_EventHandler ContainerUnSelect_Event;


        public delegate void ContainerLoadedChangeSize_EventHandler(object sender, Point PosDiff);
        public event ContainerLoadedChangeSize_EventHandler ContainerLoadedChangeSize_Event;
        #endregion
        #region "Properties"
        private string _ContainerName;
        public string ContainerName { get { return _ContainerName; } }

        private ContainerTypes _ContainerType;
        public ContainerTypes ContainerType { get { return _ContainerType; } }
        private string _ObjectName;
        public string ObjectName { get { return _ObjectName; } }
        private TextBlock ContainerContent;
        public TextBlock getContainerContent { get { return ContainerContent; } }
        private AdjustablePanel ContainerPanel;
        public AdjustablePanel SelContainerPanel { get { return ContainerPanel; } }
        
        public string ImagePath { get; set; }

        public List<ProductServiceReference.TemplateObjects> lstProductObects;
        public ProductServiceReference.TemplateObjects SelectedObect;
        #endregion
        //public ObjectContainer()
        //{
        //    InitializeComponent();
        //    _ContainerName = "";
        //}
        public ObjectContainer(string Name, ProductServiceReference.TemplateObjects objObects, webprintDesigner.ProductServiceReference.TemplateFonts objFont)
        {
            IsLoaded = false;
            try
            {
            InitializeComponent();
            lstProductObects = new List<webprintDesigner.ProductServiceReference.TemplateObjects>();
            _ContainerType =  (ContainerTypes)objObects.ObjectType;
            _ContainerName = Name;
           
            //this.Loaded += OnContentLoad;
            //ScaleTransform st = new ScaleTransform();
            //st.ScaleX = 1.0;
            //st.ScaleY = 1.0;
            //ScalingFactor.Width = st.ScaleX;
            //ScalingFactor.Height = st.ScaleY;
            //st.SetValue(Canvas.NameProperty, "ContentScale");
            //ContainerRoot.RenderTransform = st;

           //t-- ContainerRoot.Width = ObjWidth;
            
            SelectedObect = objObects;
            
            AdjustablePanel objAPanel = new AdjustablePanel();
            ContainerPanel = objAPanel; //ContainerPanel = (AdjustablePanel)ContainerBrd.FindName("AP" + _ContainerName);

            AddChildCtrol(objObects, objFont);
            if (objObects.Allignment == 1)
                objAPanel.HAlign = TextAlignment.Left;
            else if (objObects.Allignment == 2)
                objAPanel.HAlign = TextAlignment.Center;
            else if (objObects.Allignment == 3)
                objAPanel.HAlign = TextAlignment.Right;
            else
                objAPanel.HAlign = TextAlignment.Left;


            if (objObects.VAllignment == 1)
                objAPanel.VAlign = VerticalAlignment.Top;
            else if (objObects.VAllignment == 2)
                objAPanel.VAlign = VerticalAlignment.Center;
            else if (objObects.VAllignment == 3)
                objAPanel.VAlign = VerticalAlignment.Bottom;
            else
                objAPanel.VAlign = VerticalAlignment.Top;
           // objAPanel.Loaded += new RoutedEventHandler(objAPanel_Loaded);
            //objAPanel.SizeChanged += new SizeChangedEventHandler(objAPanel_SizeChanged);
           objAPanel.AdjustablePanelArranged += new AdjustablePanel.AdjustablePanelArranged_EventHandler(objAPanel_AdjustablePanelArranged);
            objAPanel.Name = "AP" + _ContainerName;

            

            //ContainerBrd.Child = objAPanel;
               
            //ContainerContent = (TextBlock)ContainerBrd.FindName("ContainerContent");
           
            if (ContainerPanel.Children.Count > 0)
            {
                if (((TextBlock)ContainerPanel.Children[0]) != null)
                    ContainerContent = (TextBlock)(ContainerPanel.Children[0]);
            }
            //ContainerContent.Text = CContent;
            RotateTransform rt = new RotateTransform();
            rt.SetValue(Canvas.NameProperty, "ContentRotate");
            rt.Angle = 360 - objObects.RotationAngle;
            //rt.CenterX = (ContainerRoot.Width) / 2;
            //rt.CenterY = (ContainerRoot.Height) / 2;
            ContainerRoot.RenderTransform = rt;

            //ContainerPanel.Width = ContainerContent.ActualWidth;
            //ContainerPanel.Height = ContainerContent.ActualHeight;
            ContainerRoot.Children.Add(ContainerPanel);
            //ContainerRoot.Width =ObjWidth;
            ContainerPanel.UpdateLayout();
            ContainerRoot.UpdateLayout();
            //UpdateContainerHeight(true);

            //ContainerContent.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            //SelectedObect.ColorC = 0;
            //SelectedObect.ColorM = 0;
            //SelectedObect.ColorY = 0;
            //SelectedObect.ColorK = 100;

                //removed by mz to enable the resize of text boxes
            //ContainerRectTR.Cursor = null;
            //ContainerRectRB.Cursor = null;
            //ContainerRectRM.Cursor = null;


            UnSelectContainer(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("::ObjectContainer::" + ex.ToString());
                MessageBox.Show("::ObjectContainer::stack=" + ex.StackTrace.ToString());
            }
            
        }

        void objAPanel_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("loded::" + ContainerPanel.PanelWidth+",A:"+ContainerPanel.ActualWidth.ToString());
            //UpdateSz();
        }

        void objAPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //MessageBox.Show("New:"+ e.NewSize.Width.ToString());
            //MessageBox.Show("pr:" + e.PreviousSize.Width.ToString());
            //MessageBox.Show("Ac:" + ContainerRoot.ActualWidth);
            //if (e.NewSize.Width > 11.50)
            //    ContainerRoot.Width = e.NewSize.Width + 6;
            //else
            //    ContainerRoot.Width = 11.50;
            //if (e.NewSize.Height > 11.50)
            //    ContainerRoot.Height = e.NewSize.Height + 6;
            //else
            //    ContainerRoot.Height = 11.50;
        }
        public bool AddChildCtrol(ProductServiceReference.TemplateObjects objObects, webprintDesigner.ProductServiceReference.TemplateFonts objFont)
        {
            bool RetVAl = false;
            try
            {
                Size cntSz= GetContainerSize();
                //MessageBox.Show("rootWidth:" + cntSz.Width.ToString());
                if (ContainerPanel != null)
                {
                    //Border objBorder = new Border();
                    //objBorder.Name = "Br" + objObects.TCtlName;
                    //objBorder.SetValue(AdjustablePanel.OffsetXProperty, webprintDesigner.Common.PointToPixel(objObects.OffsetX));
                    //objBorder.SetValue(AdjustablePanel.OffsetYProperty, webprintDesigner.Common.PointToPixel(objObects.OffsetY));
                    //objBorder.SetValue(AdjustablePanel.IsStartNewLineProperty, objObects.IsNewLine);
                    //objBorder.SetValue(AdjustablePanel.DisplayIndexProperty, objObects.DisplayOrderPdf);
                    //objBorder.MouseLeftButtonDown += new MouseButtonEventHandler(ContainerContent_MouseLeftButtonDown);
                    //objBorder.BorderBrush = new SolidColorBrush(Colors.Black);
                    TextBlock tb = new TextBlock();
                    TextBox tbEdit = new TextBox();
                    tb.SetValue(AdjustablePanel.OffsetXProperty, webprintDesigner.Common.PointToPixel(objObects.OffsetX));
                    tb.SetValue(AdjustablePanel.OffsetYProperty, webprintDesigner.Common.PointToPixel(objObects.OffsetY));

                    tbEdit.SetValue(AdjustablePanel.OffsetXProperty, webprintDesigner.Common.PointToPixel(objObects.OffsetX));
                    tbEdit.SetValue(AdjustablePanel.OffsetYProperty, webprintDesigner.Common.PointToPixel(objObects.OffsetY));

                    tb.SetValue(AdjustablePanel.IsStartNewLineProperty, objObects.IsNewLine);
                    tb.SetValue(AdjustablePanel.DisplayIndexProperty, objObects.DisplayOrderPdf);

                    tbEdit.SetValue(AdjustablePanel.DisplayIndexProperty, objObects.DisplayOrderPdf);


                    //tb.MouseLeftButtonDown += new MouseButtonEventHandler(ContainerContent_MouseLeftButtonDown);
                    //tb.BorderBrush = new SolidColorBrush(Colors.Black);
                    //tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    tb.TextWrapping = TextWrapping.Wrap;
                    tbEdit.TextWrapping = TextWrapping.Wrap;
                    tbEdit.AcceptsReturn = true;

                    #region "Set Property"
                    tb.Name = "Tb" + objObects.TCtlName;
                    tbEdit.Name = "TbEdit" + objObects.TCtlName;

                    if (objFont != null)
                    {
                        if (objFont.IsPrivateFont)
                        {
                            System.IO.Stream ms = new System.IO.MemoryStream(objFont.FontBytes);
                            tb.FontSource = new FontSource(ms);
                            tb.FontFamily = new FontFamily(objFont.FontDisplayName);

                            tbEdit.FontSource = new FontSource(ms);
                            tbEdit.FontFamily = new FontFamily(objFont.FontDisplayName);


                        }
                        else
                        {
                            tb.FontFamily = new FontFamily(objFont.FontDisplayName);
                            tb.FontSource = null;

                            tbEdit.FontFamily = new FontFamily(objFont.FontDisplayName);
                            tbEdit.FontSource = null;
                        }
                    }

                    tb.FontSize = webprintDesigner.Common.PointToPixel(objObects.FontSize);
                    tbEdit.FontSize = webprintDesigner.Common.PointToPixel(objObects.FontSize);


                    webprintDesigner.ColorDataList clrList = new ColorDataList();
                    string ClrHex = clrList.getColorHex(objObects.ColorC, objObects.ColorM, objObects.ColorY, objObects.ColorK);

                    byte a = (byte)(Convert.ToInt32(255));
                    byte r = (byte)(Convert.ToUInt32(ClrHex.Substring(1, 2), 16));
                    byte g = (byte)(Convert.ToUInt32(ClrHex.Substring(3, 2), 16));
                    byte b = (byte)(Convert.ToUInt32(ClrHex.Substring(5, 2), 16));
                    tb.Foreground = new SolidColorBrush(Color.FromArgb(a, r, g, b));

                    tbEdit.Foreground = new SolidColorBrush(Color.FromArgb(a, r, g, b));



                    if (objObects.IsBold)
                    {
                        tb.FontWeight = FontWeights.Bold;
                        tbEdit.FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        tb.FontWeight = FontWeights.Normal;
                        tbEdit.FontWeight = FontWeights.Normal;
                    }


                    if (objObects.IsItalic)
                    {
                        tb.FontStyle = FontStyles.Italic;
                        tbEdit.FontStyle = FontStyles.Italic;
                    }
                    else
                    {
                        tb.FontStyle = FontStyles.Normal;
                        tbEdit.FontStyle = FontStyles.Normal;
                    }

                    if (objObects.IsUnderlinedText)
                    {
                        tb.TextDecorations = TextDecorations.Underline;
                        
                    }
                    else
                        tb.TextDecorations = null;

                    #endregion

                    //tb.TextWrapping = TextWrapping.Wrap;


                    
                    tb.Text = System.Windows.Browser.HttpUtility.HtmlDecode(objObects.ContentString);
                    tb.LineHeight = objObects.LineSpacing;
                    tb.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                    //objBorder.Child = tb;

                    tbEdit.Text = System.Windows.Browser.HttpUtility.HtmlDecode(objObects.ContentString);

                    tb.Width = webprintDesigner.Common.PointToPixel(objObects.MaxWidth);
                    tb.Height = webprintDesigner.Common.PointToPixel(objObects.MaxHeight);

                    tbEdit.Width = webprintDesigner.Common.PointToPixel(objObects.MaxWidth);
                    tbEdit.Height = webprintDesigner.Common.PointToPixel(objObects.MaxHeight);


                    //ContainerPanel.Background = new SolidColorBrush(Colors.Red);
                    ContainerPanel.Width = webprintDesigner.Common.PointToPixel( objObects.MaxWidth);
                    ContainerPanel.Height = webprintDesigner.Common.PointToPixel(objObects.MaxHeight);


                    //ContainerRoot.Background = new SolidColorBrush(Colors.Red);
                    ContainerRoot.Width =webprintDesigner.Common.PointToPixel( objObects.MaxWidth) + 4;
                    ContainerRoot.Height = webprintDesigner.Common.PointToPixel(objObects.MaxHeight) + 4;


                    //tbEdit.Background = new  SolidColorBrush(Colors.Transparent);
                    //tbEdit.Background.Opacity = 1d;
                    tbEdit.Visibility = System.Windows.Visibility.Collapsed;

                    tbEdit.TextChanged += new TextChangedEventHandler(tbEdit_TextChanged);
                    tbEdit.Tag = tb.Name;

                    Style TransparentTBStyle = this.Resources["TransparentTextBox"] as Style;
                    tbEdit.Style = TransparentTBStyle;

                    //tbEdit.Opacity = 0.5d;
                    //tbEdit.BorderThickness = new Thickness(1);
                    //tbEdit.BorderBrush = new SolidColorBrush(Colors.Red);

                    
                    ContainerPanel.Children.Add(tb);
                    ContainerPanel.Children.Add(tbEdit);
                    lstProductObects.Add(objObects);
                    //ContainerBrd.Child = ContainerPanel;
                    //ContainerBrd.BorderThickness = new Thickness(3.0);
                    UnSelectContainer(true);
                    ContainerPanel.UpdateLayout();
                    //UpdateContainerHeight(true);
                    RetVAl= true;
                    //UpdateSz(cntSz);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("::AddChildCtrol::" + ex.ToString());
                MessageBox.Show("::AddChildCtrol::stack=" + ex.Message.ToString());
                RetVAl = false;
            }
            return RetVAl;
        }

        void tbEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            UpdateContainerContentInline(tb.Tag.ToString(), tb.Text);
        }

        void ContainerContent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //SelectChildObject(sender);
        }
        void objAPanel_AdjustablePanelArranged(object sender, Size PanelSize,int Cnt)
        {
            if (ContainerPanel != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                
                ContainerRoot.Width = PanelSize.Width + 100;
                ContainerRoot.Height = PanelSize.Height + 100;
                //   ContainerRoot.UpdateLayout();




                //if (ContainerRoot.Width < 12)
                //{
                //    ContainerRoot.Width = 12;
                //}
                //if (ContainerRoot.Height < 12)
                //{
                //    ContainerRoot.Height = 12;
                //}


                //OriginalSize.Width = ContainerRoot.ActualWidth;
                //OriginalSize.Height = ContainerRoot.ActualHeight;
                
            }
            //Point ObjPos=new Point(0,0);
            //double CntMrg = 0;
            //if (Selected)
            //    CntMrg = 6;
            //if (ContainerPanel != null && _ContainerType != 3)
            //{
            //    if (ContainerPanel.HAlign == HorizontalAlignment.Left)
            //    {
            //        if (PanelSize.Width > 11.50)
            //            ContainerRoot.Width = PanelSize.Width + 6;
            //        else
            //            ContainerRoot.Width = 11.50;

            //    }
            //    else if (ContainerPanel.HAlign == HorizontalAlignment.Center)
            //    {
            //        if (PanelSize.Width > 11.50)
            //        {
            //            ObjPos.X =  (ContainerRoot.Width - (PanelSize.Width + CntMrg)) / 2;
            //            ContainerRoot.Width = PanelSize.Width + 6;
            //        }
            //        else
            //        {
            //            ObjPos.X =  (ContainerRoot.Width - 11.50) / 2;
            //            ContainerRoot.Width = 11.50;
            //        }

            //    }
            //    else if (ContainerPanel.HAlign == HorizontalAlignment.Right)
            //    {
            //        if (PanelSize.Width > 11.50)
            //        {
            //            ObjPos.X = ContainerRoot.Width - (PanelSize.Width + CntMrg);
            //            ContainerRoot.Width = PanelSize.Width + 6;
                        
            //        }
            //        else
            //        {
            //            ObjPos.X = ContainerRoot.Width - 11.50;
            //            ContainerRoot.Width = 11.50;
            //        }
            //    }

            //    if (ContainerPanel.VAlign== VerticalAlignment.Top )
            //    {
            //        if (PanelSize.Height > 11.50)
            //            ContainerRoot.Height = PanelSize.Height + 6;
            //        else
            //            ContainerRoot.Height = 11.50;

            //    }
            //    else if (ContainerPanel.VAlign == VerticalAlignment.Center)
            //    {
            //        if (PanelSize.Height > 11.50)
            //        {
            //            ObjPos.Y = (ContainerRoot.Height - (PanelSize.Height + CntMrg)) / 2;
            //            ContainerRoot.Height = PanelSize.Height + 6;
            //        }
            //        else
            //        {
            //            ObjPos.Y =(ContainerRoot.Height - 11.50) / 2;
            //            ContainerRoot.Height = 11.50;
            //        }
            //    }
            //    else if (ContainerPanel.VAlign == VerticalAlignment.Bottom)
            //    {
            //        if (PanelSize.Height > 11.50)
            //        {
            //            ObjPos.Y = ContainerRoot.Height - (PanelSize.Height + CntMrg);
            //            ContainerRoot.Height = PanelSize.Height + 6;
            //        }
            //        else
            //        {
            //            ObjPos.Y = ContainerRoot.Height - 11.50;
            //            ContainerRoot.Height = 11.50;
            //        }

            //    }
            //    //if (UpdateSize)
            //    //{
            //        OriginalSize.Width = ContainerRoot.ActualWidth;
            //        OriginalSize.Height = ContainerRoot.ActualHeight;
            //    //}
            //        if (ObjectPosChange_Event != null)
            //            ObjectPosChange_Event(this, ObjPos);
            //        ContainerRoot.UpdateLayout();
            //}
        }

        //constructor for image
        public ObjectContainer(string Name, ImageSource ImgSource, string imgPath, double ImgWidth, double ImgHeight, string ObjName, ProductServiceReference.TemplateObjects objObects, bool IsLogo)
        {
            SelectedObect = objObects;

            IsLoaded = false;
            InitializeComponent();
            Image img = new Image();

            if (IsLogo)
            {
                _ContainerType = ContainerTypes.LogoImage;
                img.Name = "ContainerLogoImg";
            }
            else
            {
                _ContainerType = ContainerTypes.Image;
                img.Name = "ContainerImg";
            }

            _ContainerName = Name;
            _ObjectName = ObjName;
            ImagePath = imgPath;
            //applying minimum image size
            if (ImgWidth < 12)
                ContainerRoot.Width = 12;
            else
                ContainerRoot.Width = ImgWidth;


            if (ImgHeight < 12)
                ContainerRoot.Height = 12;
            else
                ContainerRoot.Height = ImgHeight;


            
            
            img.Stretch = Stretch.Fill;
            img.Source=ImgSource;

            img.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(img_ImageFailed);
                        //ContainerBrd.Child = img;
            ContainerImage = img;// = (Image)ContainerBrd.FindName("ContainerImg");
            ContainerRoot.Children.Add(img);

            ContainerPanel = null;
            RotateTransform rt = new RotateTransform();
            rt.SetValue(Canvas.NameProperty, "ContentRotate");

            if (objObects != null)
                rt.Angle = 360 - objObects.RotationAngle;
            else
                rt.Angle = 0;

            //rt.CenterX = (ContainerRoot.Width) / 2;
            //rt.CenterY = (ContainerRoot.Width) / 2;

            ContainerRoot.RenderTransform = rt;
            ContainerRoot.UpdateLayout();
            UnSelectContainer(true);
        }


        //constructor for Line Vector
        public ObjectContainer(string Name, double X1, double Y1,double LineStroke, string ObjName, ProductServiceReference.TemplateObjects objObects)
        {

           
            SelectedObect = objObects;

            IsLoaded = false;
            InitializeComponent();
            _ContainerType =  ContainerTypes.LineVector;
            _ContainerName = Name;
            _ObjectName = ObjName;
       



            Line oLine = new Line();
            //oLine.Fill = new SolidColorBrush(Colors.Red);
            oLine.Margin = new Thickness(0);
            oLine.X1 = 0;
            oLine.X2 = webprintDesigner.Common.PointToPixel(objObects.MaxWidth);
            oLine.Y1 = webprintDesigner.Common.PointToPixel(objObects.MaxHeight)/2;
            oLine.Y2 = webprintDesigner.Common.PointToPixel(objObects.MaxHeight) / 2;
            oLine.StrokeThickness = LineStroke * 2;
            //oLine.Stretch = Stretch.Fill;
            //oLine.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            oLine.VerticalAlignment = System.Windows.VerticalAlignment.Center;


            webprintDesigner.ColorDataList clrList = new ColorDataList();
            string ClrHex = clrList.getColorHex(objObects.ColorC, objObects.ColorM, objObects.ColorY, objObects.ColorK);

            byte a = (byte)(Convert.ToInt32(255));
            byte r = (byte)(Convert.ToUInt32(ClrHex.Substring(1, 2), 16));
            byte g = (byte)(Convert.ToUInt32(ClrHex.Substring(3, 2), 16));
            byte b = (byte)(Convert.ToUInt32(ClrHex.Substring(5, 2), 16));
            oLine.Stroke = new SolidColorBrush(Color.FromArgb(a, r, g, b));

            ContainerRoot.Height = LineStroke;
            ContainerRoot.Width = webprintDesigner.Common.PointToPixel(objObects.MaxWidth);


            ContainerRoot.Children.Add(oLine);

            ContainerPanel = null;
            RotateTransform rt = new RotateTransform();
            rt.SetValue(Canvas.NameProperty, "ContentRotate");

            if (objObects != null)
                rt.Angle = 360 - objObects.RotationAngle;
            else
                rt.Angle = 0;
            //rt.CenterX = (ContainerRoot.Width) / 2;
            //rt.CenterY = (ContainerRoot.Width) / 2;

            ContainerRoot.RenderTransform = rt;
            ContainerRoot.UpdateLayout();
            UnSelectContainer(true);

            ContainerRectTL.Width = 1d;
            ContainerRectTL.Height = 1d;

            ContainerRectLB.Width = 1d;
            ContainerRectLB.Height = 1d;


            ContainerRectRB.Margin = new Thickness(6, 6, -10, -20d);


            ContainerRectRM.Width = 1d;
            ContainerRectRM.Height = 1d;

            //ContainerRectBM.Width = 3d;
            ContainerRectBM.Margin = new Thickness(0, 0, 0, -20d);


            ContainerRectTR.Margin = new Thickness(6, -10, -10, -10);

            ContainerRectLM.Width = 1d;
            ContainerRectLM.Height = 1d;

            ContainerRectTM.Width = 1d;
            ContainerRectTM.Height = 1d;

            ContainerRectLM.Visibility = System.Windows.Visibility.Collapsed;
            ContainerRectTM.Visibility = System.Windows.Visibility.Collapsed;

            //pnlStatus.Height = 1d;
        }

        //constructor for Rectangle Vector
        public ObjectContainer(string Name, double X1, double Y1, string ObjName, ProductServiceReference.TemplateObjects objObects, bool IsEllipse)
        {


            SelectedObect = objObects;

            IsLoaded = false;
            InitializeComponent();
          
            _ContainerName = Name;
            _ObjectName = ObjName;

            webprintDesigner.ColorDataList clrList = new ColorDataList();
            string ClrHex = clrList.getColorHex(objObects.ColorC, objObects.ColorM, objObects.ColorY, objObects.ColorK);

            byte a = (byte)(Convert.ToInt32(255));
            byte r = (byte)(Convert.ToUInt32(ClrHex.Substring(1, 2), 16));
            byte g = (byte)(Convert.ToUInt32(ClrHex.Substring(3, 2), 16));
            byte b = (byte)(Convert.ToUInt32(ClrHex.Substring(5, 2), 16));

            ContainerRoot.Height = webprintDesigner.Common.PointToPixel(objObects.MaxHeight);
            ContainerRoot.Width = webprintDesigner.Common.PointToPixel(objObects.MaxWidth);

           
            if (IsEllipse)
            {
                _ContainerType = ContainerTypes.EllipseVector;
                Ellipse oEllipse = new Ellipse();
                oEllipse.Margin = new Thickness(0);

                oEllipse.Width = webprintDesigner.Common.PointToPixel(objObects.MaxWidth);
                oEllipse.Height = webprintDesigner.Common.PointToPixel(objObects.MaxHeight);
                oEllipse.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                ContainerRoot.Children.Add(oEllipse);
            }
            else
            {
                _ContainerType = ContainerTypes.RectangleVector;
                Rectangle oRect = new Rectangle();
                oRect.Margin = new Thickness(0);

                oRect.Width = webprintDesigner.Common.PointToPixel(objObects.MaxWidth);
                oRect.Height = webprintDesigner.Common.PointToPixel(objObects.MaxHeight);
                oRect.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                ContainerRoot.Children.Add(oRect);
            }


            ContainerPanel = null;
            RotateTransform rt = new RotateTransform();
            rt.SetValue(Canvas.NameProperty, "ContentRotate");

            if (objObects != null)
                rt.Angle = 360 - objObects.RotationAngle;
            else
                rt.Angle = 0;
            //rt.CenterX = (ContainerRoot.Width) / 2;
            //rt.CenterY = (ContainerRoot.Width) / 2;

            ContainerRoot.RenderTransform = rt;
            ContainerRoot.UpdateLayout();
            UnSelectContainer(true);

            ContainerRectTL.Width = 1d;
            ContainerRectTL.Height = 1d;

            ContainerRectLB.Width = 1d;
            ContainerRectLB.Height = 1d;


            ContainerRectRB.Margin = new Thickness(6, 6, -10, -20d);


            ContainerRectRM.Width = 1d;
            ContainerRectRM.Height = 1d;

            //ContainerRectBM.Width = 3d;
            ContainerRectBM.Margin = new Thickness(0, 0, 0, -20d);


            ContainerRectTR.Margin = new Thickness(6, -10, -10, -10);

            ContainerRectLM.Width = 1d;
            ContainerRectLM.Height = 1d;

            ContainerRectTM.Width = 1d;
            ContainerRectTM.Height = 1d;

            ContainerRectLM.Visibility = System.Windows.Visibility.Collapsed;
            ContainerRectTM.Visibility = System.Windows.Visibility.Collapsed;

            //pnlStatus.Height = 1d;
        }

        void img_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            TextBlock oTB = new TextBlock();
            oTB.Name = "Error";
            oTB.Text = "Error: Missing or Invalid Image Object. Please delete..";
            oTB.Foreground = new SolidColorBrush(Colors.Red);
            oTB.FontSize = 15;

            ContainerRoot.Width = 400;
            ContainerRoot.Height = 25;


            oTB.Width = ContainerRoot.Width;
            oTB.Height = ContainerRoot.Height;
            ContainerRoot.Children.Add(oTB);
        }
        void ContainerContent_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ContainerContent.ActualHeight.ToString());
            UpdateContainerHeight(true);
        }
        private void OnContentLoad(object sender, RoutedEventArgs e)
        {
            //if (ContainerContent != null && _ContainerType != 3)
            //{
            //    ContainerContent.Width = 150d;
            //    if (ContainerContent.ActualHeight > 11.50)
            //        ContainerRoot.Height = ContainerContent.ActualHeight;
            //    else
            //        ContainerRoot.Height = 11.50;
            //    ContainerContent.Width = double.NaN;
            //}
            ////ContainerRoot.SetValue(Canvas.TopProperty, OriginalPostion.Y);
            ////ContainerRoot.SetValue(Canvas.LeftProperty, OriginalPostion.X);
        }
        public void UpdateContainer()
        {
            //ContainerRoot.SetValue(Canvas.TopProperty, OriginalPostion.Y);
            //ContainerRoot.SetValue(Canvas.LeftProperty, OriginalPostion.X);
        }
        public void UpdateContainerColor(string clr,int ClrC, int ClrM,int ClrY,int ClrK)
        {

            byte a = (byte)(Convert.ToInt32(255));
            byte r = (byte)(Convert.ToUInt32(clr.Substring(1, 2), 16));
            byte g = (byte)(Convert.ToUInt32(clr.Substring(3, 2), 16));
            byte b = (byte)(Convert.ToUInt32(clr.Substring(5, 2), 16));

            if (ContainerContent != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage && SelectedObect != null)
            {
                ContainerContent.Foreground = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }
            else if (_ContainerType == ContainerTypes.LineVector)
            {
                Line oLine = (Line)ContainerRoot.Children[1];
                oLine.Stroke = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }
            else if (_ContainerType == ContainerTypes.RectangleVector)
            {
                Rectangle oRect = (Rectangle)ContainerRoot.Children[1];
                oRect.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }
            else if (_ContainerType == ContainerTypes.EllipseVector)
            {
                Ellipse oEllipse = (Ellipse)ContainerRoot.Children[1];
                oEllipse.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }

            SelectedObect.ColorC = ClrC;
            SelectedObect.ColorM = ClrM;
            SelectedObect.ColorY = ClrY;
            SelectedObect.ColorK = ClrK;
        }
        //public void UpdateContainerColor(int ClrC, int ClrM, int ClrY, int ClrK)
        //{
        //    webprintDesigner.ColorDataList clrList = new ColorDataList();
        //    string ClrHex = clrList.getColorHex(ClrC, ClrM, ClrY, ClrK);
        //    if (ContainerContent != null && _ContainerType != 3)
        //    {
        //        byte a = (byte)(Convert.ToInt32(255));
        //        byte r = (byte)(Convert.ToUInt32(ClrHex.Substring(1, 2), 16));
        //        byte g = (byte)(Convert.ToUInt32(ClrHex.Substring(3, 2), 16));
        //        byte b = (byte)(Convert.ToUInt32(ClrHex.Substring(5, 2), 16));
        //        ContainerContent.Foreground = new SolidColorBrush(Color.FromArgb(a, r, g, b));
        //        ColorC = ClrC;
        //        ColorM = ClrM;
        //        ColorY = ClrY;
        //        ColorK = ClrK;
        //    }
        //}


        public void UpdateContainerLockPosition(bool IsLock, bool show)
        {
            
                //UnSelectContainer(true);
                IsLockedPosition = IsLock;
                if (show)
                {
                    if (IsLockedPosition)
                    {
                        SetcontainerMode(ContainerMode.LockedPosition);
                    }
                    else
                    {
                        SetcontainerMode(ContainerMode.clear);
                    }
                }
        }


        public void UpdateContainerLockEditing(bool IsLock, bool show)
        {
            if (ContainerContent != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage && _ContainerType != ContainerTypes.LineVector && _ContainerType != ContainerTypes.RectangleVector && _ContainerType != ContainerTypes.EllipseVector)
            {
                //UnSelectContainer(true);
                IsLockedEditing = IsLock;
                if (show)
                {

                    if (IsLockedEditing == false)
                    {
                        SetcontainerMode(ContainerMode.LockedEditing);
                    }
                    else
                    {
                        SetcontainerMode(ContainerMode.clear);
                    }
                }
            }
        }


        public void UpdateContainerShowHide(bool Printable, bool show)
        {
            IsPrintable = Printable;
            if (show)
            {

                if (Printable == false) //logic is inverse
                {
                    SetcontainerMode(ContainerMode.clear);
                }
                else
                {
                    SetcontainerMode(ContainerMode.NonPrintable);

                }
            }
                
           
        }

        private enum ContainerMode
        {
            LockedEditing =1,
            clear=2,
            NonPrintable=3,
            LockedPosition
        }

        public enum ContainerTypes
        {
            SingleLineText = 1,
            MultiLineText = 2,
            Image =3,
            Label = 4,
            LineVector = 5,
            RectangleVector = 6,
            EllipseVector = 7,
            LogoImage = 8
        }

        private void SetcontainerMode(ContainerMode Mode)
        {
            if ( Mode == ContainerMode.LockedEditing)
            {
                pnlStatus.Visibility = System.Windows.Visibility.Visible;
                imgLockEditing.Visibility = System.Windows.Visibility.Visible;

            }
            else if (Mode == ContainerMode.LockedPosition)
            {
                pnlStatus.Visibility = System.Windows.Visibility.Visible;
                imgLockPosition.Visibility = System.Windows.Visibility.Visible;

            }
            else if (Mode == ContainerMode.NonPrintable)
            {
                pnlStatus.Visibility = System.Windows.Visibility.Visible;
                imgNonPrint.Visibility = Visibility.Visible;
            }
            else if (Mode == ContainerMode.clear)
            {

                if (IsLockedPosition == false)
                {
                    imgLockPosition.Visibility = System.Windows.Visibility.Collapsed;
                }


                if (IsLockedEditing != false)
                {
                    imgLockEditing.Visibility = System.Windows.Visibility.Collapsed;
                }


                if (IsPrintable == false) //logic is inverse
                {
                    imgNonPrint.Visibility = System.Windows.Visibility.Collapsed;

                }

                  //if (IsLockedPosition == false && IsLockedEditing == false && )

                //pnlStatus.Visibility = System.Windows.Visibility.Collapsed;
                
                
               
                
            }
        }


        public Size GetContainerSize()
        {
            ContainerRoot.UpdateLayout();
            return new Size(ContainerRoot.ActualWidth, ContainerRoot.ActualHeight);
        }

        public void UpdateSz(Size ObjSz)
        {
            Point ObjPos = new Point(0, 0);
            double CntMrg = 0;
            if (Selected)
                CntMrg = 6;
            if (ContainerPanel != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                //ContainerRoot.UpdateLayout();
                //ContainerPanel.UpdateLayout();
                //MessageBox.Show("ActualWidth::" + ContainerRoot.ActualWidth.ToString());
                ObjPos.X = 0;
                //if (ContainerPanel.HAlign == TextAlignment.Left)
                //    ObjPos.X = 0;
                //else if (ContainerPanel.HAlign == TextAlignment.Center)
                //{
                //    ObjPos.X = (ContainerRoot.ActualWidth - ObjSz.Width) / 2;
                //}
                //else if (ContainerPanel.HAlign == TextAlignment.Right)
                //{
                //    ObjPos.X = ContainerRoot.ActualWidth - ObjSz.Width;
                //}

                if (ContainerPanel.VAlign == VerticalAlignment.Top)
                {
                    ObjPos.Y = 0;
                }
                else if (ContainerPanel.VAlign == VerticalAlignment.Center)
                {
                    ObjPos.Y = 0;
                }
                else if (ContainerPanel.VAlign == VerticalAlignment.Bottom)
                {
                    ObjPos.Y = ContainerRoot.ActualHeight - ObjSz.Height;

                }
                if (ObjectPosChange_Event != null)
                    ObjectPosChange_Event(this, ObjPos);
            }
        }

        public void UpdateContainerHeight(bool UpdateSize)
        {
//            MessageBox.Show(ContainerPanel.PanelWidth.ToString());
//Point ObjPos=new Point(0,0);
//            double CntMrg = 0;
//            if (Selected)
//                CntMrg = 6;
//            if (ContainerContent != null && _ContainerType != 3)
//            {
//                if (ContainerPanel.HAlign == HorizontalAlignment.Left)
//                {
//                    if (ContainerPanel.PanelWidth > 11.50)
//                        ContainerRoot.Width = ContainerPanel.PanelWidth + 6;
//                    else
//                        ContainerRoot.Width = 11.50;

//                }
//                else if (ContainerPanel.HAlign == HorizontalAlignment.Center)
//                {
//                    if (ContainerPanel.PanelWidth > 11.50)
//                    {
//                        ObjPos.X = (ContainerRoot.Width - (ContainerPanel.PanelWidth + 6)) / 2;
//                        ContainerRoot.Width = ContainerPanel.PanelWidth + 6;
//                    }
//                    else
//                    {
//                        ObjPos.X = (ContainerRoot.Width - 11.50) / 2;
//                        ContainerRoot.Width = 11.50;
//                    }

//                }
//                else if (ContainerPanel.HAlign == HorizontalAlignment.Right)
//                {
//                    if (ContainerPanel.PanelWidth > 11.50)
//                    {
//                        ObjPos.X = ContainerRoot.Width - (ContainerPanel.PanelWidth + 6);
//                        ContainerRoot.Width = ContainerPanel.PanelWidth + 6;

//                    }
//                    else
//                    {
//                        ObjPos.X = ContainerRoot.Width - 11.50;
//                        ContainerRoot.Width = 11.50;
//                    }
//                }

//                if (ContainerPanel.VAlign == VerticalAlignment.Top)
//                {
//                    if (ContainerPanel.PanelHeight > 11.50)
//                        ContainerRoot.Height = ContainerPanel.PanelHeight + 6;
//                    else
//                        ContainerRoot.Height = 11.50;

//                }
//                else if (ContainerPanel.VAlign == VerticalAlignment.Center)
//                {
//                    if (ContainerPanel.PanelHeight > 11.50)
//                    {
//                        ObjPos.Y = (ContainerRoot.Height - (ContainerPanel.PanelHeight + 6)) / 2;
//                        ContainerRoot.Height = ContainerPanel.PanelHeight + 6;
//                    }
//                    else
//                    {
//                        ObjPos.Y = (ContainerRoot.Height - 11.50) / 2;
//                        ContainerRoot.Height = 11.50;
//                    }
//                }
//                else if (ContainerPanel.VAlign == VerticalAlignment.Bottom)
//                {
//                    if (ContainerPanel.PanelHeight > 11.50)
//                    {
//                        ObjPos.Y = ContainerRoot.Height - (ContainerPanel.PanelHeight + 6);
//                        ContainerRoot.Height = ContainerPanel.PanelHeight + 6;
//                    }
//                    else
//                    {
//                        ObjPos.Y = ContainerRoot.Height - 11.50;
//                        ContainerRoot.Height = 11.50;
//                    }

//                }
//                //if (UpdateSize)
//                //{
//                OriginalSize.Width = ContainerRoot.ActualWidth;
//                OriginalSize.Height = ContainerRoot.ActualHeight;
//                //}
//                if (ObjectPosChange_Event != null)
//                    ObjectPosChange_Event(this, ObjPos);
//                ContainerRoot.UpdateLayout();
//            }
            //if (ContainerContent != null && _ContainerType != 3)
            //{
            //    MessageBox.Show(ContainerPanel.ActualWidth.ToString());
            //    if (ContainerPanel.ActualWidth > 11.50)
            //        ContainerRoot.Width = ContainerPanel.ActualWidth;
            //    else
            //        ContainerRoot.Width = 11.50;

            //    if (ContainerPanel.ActualHeight > 11.50)
            //        ContainerRoot.Height = ContainerPanel.ActualHeight;
            //    else
            //        ContainerRoot.Height = 11.50;

            //    //if (ContainerContent.ActualHeight > 11.50)
            //    //    ContainerRoot.Height = ContainerContent.ActualHeight;
            //    //else
            //    //    ContainerRoot.Height = 11.50;
            //    if (UpdateSize)
            //    {
            //        OriginalSize.Width = ContainerRoot.ActualWidth;
            //        OriginalSize.Height = ContainerRoot.ActualHeight;
            //    }
            //}
        }
        public void UpdateContainerContent(string CtrlName, string CContent)
        {
            Size TSz = GetContainerSize();
            if (ContainerPanel != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                
                object obj = ContainerPanel.FindName("Tb" + CtrlName);
                if (obj != null)
                {
                    TextBlock tb = (TextBlock)obj;
                    if (tb != null)
                    {
                       
                        tb.Text = CContent;
                        //ContainerPanel.UpdateLayout();
                        UpdateSz(TSz);
                    }
                }
            }
        }

        public void UpdateContainerContentInline(string CtrlName, string CContent)
        {
            Size TSz = GetContainerSize();
            if (ContainerPanel != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                object obj = ContainerPanel.Children[0];
                if (obj != null)
                {
                    TextBlock tb = (TextBlock)obj;
                    if (tb != null)
                    {

                        tb.Text = CContent;
                        //ContainerPanel.UpdateLayout();
                        UpdateSz(TSz);
                    }
                }
            }
        }
        public void UpdateContainerFont(webprintDesigner.ProductServiceReference.TemplateFonts objFont)
        {
            Size TSz = GetContainerSize();
            if (ContainerContent != null && SelectedObect != null && objFont != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                if ("Tb" + SelectedObect.TCtlName == ContainerContent.Name)
                {
                    if (objFont.IsPrivateFont)
                    {
                        System.IO.Stream ms = new System.IO.MemoryStream(objFont.FontBytes);
                        ContainerContent.FontSource = new FontSource(ms);
                        ContainerContent.FontFamily = new FontFamily(objFont.FontDisplayName);

                        

                        SelectedObect.FontName = objFont.FontDisplayName;
                        SelectedObect.IsFontNamePrivate = true;
                    }
                    else
                    {
                        ContainerContent.FontFamily = new FontFamily(objFont.FontDisplayName);
                        ContainerContent.FontSource = null;

                        SelectedObect.FontName = objFont.FontDisplayName;
                        SelectedObect.IsFontNamePrivate = true;
                    }
                    UpdateSz(TSz);
                }
            }
        }
        public void UpdateContainerFontSize(double FntSize)
        {
            Size TSz = GetContainerSize();
            if (ContainerContent != null && SelectedObect != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                if ("Tb" + SelectedObect.TCtlName == ContainerContent.Name)
                {
                    ContainerContent.FontSize = webprintDesigner.Common.PointToPixel(FntSize);
                    SelectedObect.FontSize = FntSize;
                    UpdateSz(TSz);
                }
                
            }
        }
        public void UpdateContainerAlign(int Alignment)
        {
            if (ContainerPanel != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                if (Alignment == 1)
                {
                    //ContainerContent.HorizontalAlignment = HorizontalAlignment.Left;
                    ContainerPanel.HAlign = TextAlignment.Left;
                    //ContainerContent.TextAlignment = TextAlignment.Left;
                    //ContainerPanel.InvalidateArrange();
                }
                else if (Alignment == 2)
                {
                    //ContainerContent.HorizontalAlignment = HorizontalAlignment.Center;
                   // ContainerContent.TextAlignment = TextAlignment.Center;
                    ContainerPanel.HAlign = TextAlignment.Center;
                    //ContainerPanel.UpdateLayout();
                }
                else if (Alignment == 3)
                {
                    //ContainerContent.HorizontalAlignment = HorizontalAlignment.Right;
                    //ContainerPanel.HAlign = HorizontalAlignment.Right;
                    //ContainerContent.TextAlignment = TextAlignment.Right;
                    ContainerPanel.HAlign = TextAlignment.Right;
                    //ContainerPanel.InvalidateArrange();
                }
                else if (Alignment == 4)
                {
                    //ContainerContent.HorizontalAlignment = HorizontalAlignment.Right;
                    //ContainerPanel.HAlign = HorizontalAlignment.Right;
                    //ContainerContent.TextAlignment = TextAlignment.Right;
                    ContainerPanel.HAlign = TextAlignment.Justify;
                    //ContainerPanel.InvalidateArrange();
                }
            }
        }
        public void UpdateContainerBold()
        {
            Size TSz = GetContainerSize();
            if (ContainerContent != null && SelectedObect != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                if ("Tb" + SelectedObect.TCtlName == ContainerContent.Name)
                {
                    if (ContainerContent.FontWeight != FontWeights.Bold)
                    {
                        ContainerContent.FontWeight = FontWeights.Bold;
                        SelectedObect.IsBold = true;
                    }
                    else
                    {
                        ContainerContent.FontWeight = FontWeights.Normal;
                        SelectedObect.IsBold = false;
                    }
                    UpdateSz(TSz);
                }
            }
        }
        public void UpdateContainerItalic()
        {
            Size TSz = GetContainerSize();
            if (ContainerContent != null && SelectedObect != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                if ("Tb" + SelectedObect.TCtlName == ContainerContent.Name)
                {
                    if (ContainerContent.FontStyle != FontStyles.Italic)
                    {
                        ContainerContent.FontStyle = FontStyles.Italic;
                        SelectedObect.IsItalic = true;
                    }
                    else
                    {
                        ContainerContent.FontStyle = FontStyles.Normal;
                        SelectedObect.IsItalic = false;
                    }
                    UpdateSz(TSz);
                }
            }
        }
        public void UpdateContainerUnderline()
        {
            Size TSz = GetContainerSize();
            if (ContainerContent != null && SelectedObect != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                if ("Tb" + SelectedObect.TCtlName == ContainerContent.Name)
                {
                    if (ContainerContent.TextDecorations != TextDecorations.Underline)
                    {
                        ContainerContent.TextDecorations = TextDecorations.Underline;
                        SelectedObect.IsUnderlinedText = true;
                    }
                    else
                    {
                        ContainerContent.TextDecorations = null;
                        SelectedObect.IsUnderlinedText = false;
                    }
                    UpdateSz(TSz);
                }
            }
        }
        //public void UpdateContainerUnderline(bool IsUnderline)
        //{
        //    if (ContainerContent != null && _ContainerType != 3)
        //    {
        //        if (IsUnderline)
        //            ContainerContent.TextDecorations = TextDecorations.Underline;
        //        else
        //            ContainerContent.TextDecorations = null;
        //        UpdateContainerHeight(true);
        //    }
        //}


        public void UpdateContainerImgSize(double ImgX, double ImgY, AspectResize ResizeAspect)
        {
            if (ContainerImage != null)
            {
                //double ImgS=0;
                //if (ImgY == 0 && ImgX != 0)
                //{
                //    ImgS=ImgX;
                   
                //}
                //else if (ImgY != 0 && ImgX == 0)
                //{
                //    ImgS= ImgY ;
                    
                //}
                //else if (ImgY != 0 && ImgX != 0)
                //{
                //    if(ImgY>ImgX)
                //        ImgS = ImgY;
                //    else
                //        ImgS = ImgX;
                //}
                //if ((ImgS + this.OriginalSize.Width) > 14 && (ImgS + this.OriginalSize.Height) > 14)
                //{
                //    ContainerRoot.Width = ImgS + this.OriginalSize.Width;
                //    ContainerRoot.Height = ImgS + this.OriginalSize.Height;
                //}

                if (ImgX + this.OriginalSize.Width > 12 && ImgY + this.OriginalSize.Height > 12)
                {

                    if (ResizeAspect == AspectResize.LockedAspect) // keep aspect ratio
                    {

                        if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift || (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                        {

                            double Xfac = 0;
                            double Yfac = 0;

                            if (this.OriginalSize.Width > this.OriginalSize.Height)
                            {
                                Xfac = ImgX;
                                Yfac = ImgX / ((this.OriginalSize.Width) / this.OriginalSize.Height);

                            }
                            else
                            {
                                Xfac = ImgY / ((this.OriginalSize.Height) / this.OriginalSize.Width);
                                Yfac = ImgY;
                            }


                            if ((Xfac + this.OriginalSize.Width) > 14 && (Yfac + this.OriginalSize.Height) > 14)
                            {
                                ContainerRoot.Width = Xfac + this.OriginalSize.Width;
                                ContainerRoot.Height = Yfac + this.OriginalSize.Height;
                            }
                        }
                        else
                        {
                            ContainerRoot.Width = ImgX + this.OriginalSize.Width;
                            ContainerRoot.Height = ImgY + this.OriginalSize.Height;
                        }
                    }
                    else if (ResizeAspect == AspectResize.Horizontal) // horizontal
                    {
                        ContainerRoot.Width = ImgX + this.OriginalSize.Width;
                    }

                    else if (ResizeAspect == AspectResize.Vertical) // vertical
                    {
                        ContainerRoot.Height = ImgY + this.OriginalSize.Height;
                    }
                }

            }
        }

        //resize vector line
        public void UpdateContainerVectorLineSize(Size NewSize)
        {

            Line oLine = (Line)ContainerRoot.Children[1];
            if (NewSize.Height> 10)
                oLine.StrokeThickness = NewSize.Height * 2;
            else
                oLine.StrokeThickness = NewSize.Height;

            oLine.Y1 = NewSize.Height / 2;
            oLine.Y2 = NewSize.Height / 2;
            oLine.X2 = NewSize.Width;
            //oLine.Width = NewSize.Width;

            ContainerRoot.Width = NewSize.Width;
            ContainerRoot.Height = NewSize.Height;

            ContainerRoot.UpdateLayout();


        }

        //resize rectanble vector
        public void UpdateContainerVectorRectSize(double ImgX, double ImgY, AspectResize ResizeAspect)
        {

            Rectangle oLine = (Rectangle)ContainerRoot.Children[1];

            if (ImgX + this.OriginalSize.Width > 12 && ImgY + this.OriginalSize.Height > 12)
            {

                if (ResizeAspect == AspectResize.LockedAspect) // keep aspect ratio
                {

                    if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift || (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    {

                        double Xfac = 0;
                        double Yfac = 0;

                        if (this.OriginalSize.Width > this.OriginalSize.Height)
                        {
                            Xfac = ImgX;
                            Yfac = ImgX / ((this.OriginalSize.Width) / this.OriginalSize.Height);

                        }
                        else
                        {
                            Xfac = ImgY / ((this.OriginalSize.Height) / this.OriginalSize.Width);
                            Yfac = ImgY;
                        }


                        if ((Xfac + this.OriginalSize.Width) > 14 && (Yfac + this.OriginalSize.Height) > 14)
                        {
                            ContainerRoot.Width = Xfac + this.OriginalSize.Width;
                            ContainerRoot.Height = Yfac + this.OriginalSize.Height;

                            oLine.Width = Xfac + this.OriginalSize.Width;
                            oLine.Height = Yfac + this.OriginalSize.Height;
                        }
                    }
                    else
                    {
                        ContainerRoot.Width = ImgX + this.OriginalSize.Width;
                        ContainerRoot.Height = ImgY + this.OriginalSize.Height;

                        oLine.Width = ImgX + this.OriginalSize.Width;
                        oLine.Height = ImgY + this.OriginalSize.Height;
                    }
                }
                else if (ResizeAspect == AspectResize.Horizontal) // horizontal
                {
                    ContainerRoot.Width = ImgX + this.OriginalSize.Width;
                    oLine.Width = ImgX + this.OriginalSize.Width;
                }

                else if (ResizeAspect == AspectResize.Vertical) // vertical
                {
                    ContainerRoot.Height = ImgY + this.OriginalSize.Height;
                    oLine.Height = ImgY + this.OriginalSize.Height;
                }
            }

         

            ContainerRoot.UpdateLayout();

        }

        //resize rectanble vector
        public void UpdateContainerVectorEllipseSize(double ImgX, double ImgY, AspectResize ResizeAspect)
        {

            Ellipse oLine = (Ellipse)ContainerRoot.Children[1];
            

            if (ImgX + this.OriginalSize.Width > 12 && ImgY + this.OriginalSize.Height > 12)
            {

                if (ResizeAspect == AspectResize.LockedAspect) // keep aspect ratio
                {

                    if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift || (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    {

                        double Xfac = 0;
                        double Yfac = 0;

                        if (this.OriginalSize.Width > this.OriginalSize.Height)
                        {
                            Xfac = ImgX;
                            Yfac = ImgX / ((this.OriginalSize.Width) / this.OriginalSize.Height);

                        }
                        else
                        {
                            Xfac = ImgY / ((this.OriginalSize.Height) / this.OriginalSize.Width);
                            Yfac = ImgY;
                        }


                        if ((Xfac + this.OriginalSize.Width) > 14 && (Yfac + this.OriginalSize.Height) > 14)
                        {
                            ContainerRoot.Width = Xfac + this.OriginalSize.Width;
                            ContainerRoot.Height = Yfac + this.OriginalSize.Height;

                            oLine.Width = Xfac + this.OriginalSize.Width;
                            oLine.Height = Yfac + this.OriginalSize.Height;
                        }
                    }
                    else
                    {
                        ContainerRoot.Width = ImgX + this.OriginalSize.Width;
                        ContainerRoot.Height = ImgY + this.OriginalSize.Height;

                        oLine.Width = ImgX + this.OriginalSize.Width;
                        oLine.Height = ImgY + this.OriginalSize.Height;
                    }
                }
                else if (ResizeAspect == AspectResize.Horizontal) // horizontal
                {
                    ContainerRoot.Width = ImgX + this.OriginalSize.Width;
                    oLine.Width = ImgX + this.OriginalSize.Width;
                }

                else if (ResizeAspect == AspectResize.Vertical) // vertical
                {
                    ContainerRoot.Height = ImgY + this.OriginalSize.Height;
                    oLine.Height = ImgY + this.OriginalSize.Height;
                }
            }



            ContainerRoot.UpdateLayout();

        }

        //resize text box size
        public void UpdateContainerSize(Size NewSize)
        {
            if (ContainerContent != null && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                //Size TSz = GetContainerSize();
                //NewSize.Width = 500;
                //UpdateSz(NewSize);
                if (NewSize.Height > 6 && NewSize.Width > 6)
                {

                    ((TextBlock)ContainerPanel.Children[0]).Width = NewSize.Width;
                    ((TextBlock)ContainerPanel.Children[0]).Height = NewSize.Height;

                    ((TextBox)ContainerPanel.Children[1]).Width = NewSize.Width;
                    ((TextBox)ContainerPanel.Children[1]).Height = NewSize.Height;

                    ContainerPanel.Width = NewSize.Width;
                    ContainerPanel.Height = NewSize.Height;

                    ContainerRoot.Width = NewSize.Width + 4;
                    ContainerRoot.Height = NewSize.Height + 4;

                    ContainerRoot.UpdateLayout();
                }
                // NewSize.Width;
                //UpdateContainerHeight(false);
                //OriginalSize = NewSize;
            }
            //else if (ContainerImage != null)
            //{//    double nPercent = 0;
            //    double nPercentW = 0;
            //    double nPercentH = 0;
            //    nPercentW = (ContainerRoot.Width / NewSize.Width);
            //    nPercentH = (ContainerRoot.Height / NewSize.Height);
            //    if (nPercentH < nPercentW)
            //        nPercent = nPercentH;
            //    else
            //        nPercent = nPercentW;
            //    ContainerRoot.Width = nPercent * NewSize.Width;
            //    ContainerRoot.Height = nPercent * NewSize.Height;
            //}

        }
        public void UpdateRotation(double NewAngle)
        {
            RotateTransform rt = FindContentRotate();
            rt.Angle = ObjectAngle + -NewAngle;
            //rt.CenterX = (ContainerRoot.ActualWidth) / 2;
            //rt.CenterY = (ContainerRoot.ActualHeight ) / 2;

            //App CurrentApp = (App)Application.Current;
            //Page ObjPage = (Page)CurrentApp.RootVisual;
            //ObjPage.hdrtxt.Text = "Ang:: " + rt.Angle + " ";
        }
        public void UpdateRotation2(double NewAngle)
        {
            RotateTransform rt = FindContentRotate();
            rt.Angle = NewAngle;
            //rt.CenterX = (ContainerRoot.ActualWidth) / 2;
            //rt.CenterY = (ContainerRoot.ActualHeight) / 2;
        }
        public void UpdateLineHeight(double LineHeight)
        {
            if (ContainerContent != null && _ContainerType !=  ContainerTypes.Image)
            {
                if (LineHeight != 0)
                {
                    //ContainerContent.LineHeight = ContainerContent.FontSize + LineHeight;

                    TextBlock tb = (TextBlock)ContainerPanel.Children[0];
                    TextBox tbEdit = (TextBox)ContainerPanel.Children[1];

                    tb.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                    tb.LineHeight = LineHeight;


                    SelectedObect.LineSpacing = LineHeight;
                    
                }
                //UpdateContainerHeight(true);
            }
        }
        public double getAngle()
        {
            RotateTransform rt = FindContentRotate();
            return rt.Angle;
        }
        private ScaleTransform FindContentScale()
        {
            return ((ScaleTransform) ContainerRoot.FindName("ContentScale"));
        }
        private RotateTransform FindContentRotate()
        {
            return ((RotateTransform)ContainerRoot.FindName("ContentRotate"));
        }
        public ProductServiceReference.TemplateObjects FindObject(string CtlName)
        {
            ProductServiceReference.TemplateObjects FndObect = null;
            foreach (ProductServiceReference.TemplateObjects objObect in lstProductObects)
            {
                if ("Tb" + objObect.TCtlName == CtlName)
                {
                    FndObect = objObect;
                    break;
                }
            }
            return FndObect;
        }
       
        public void SelectContainer(string ChildCtlName, Point MousePositionObjectContainer, Point MousePositionContainerRoot )
        {
            try
            {
                if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
                {
                    if (Selected == false) //!Disabled &&
                    {
                        if (ContainerPanel == null)
                        {
                            ContainerPanel = (AdjustablePanel)ContainerRoot.FindName("AP" + _ContainerName);
                        }
                        if (ContainerPanel != null)
                        {

                            if (ContainerSelect_Event != null)
                            {
                                MousePositionContainerRoot.X = MousePositionContainerRoot.X - MousePositionObjectContainer.X;
                                MousePositionContainerRoot.Y = MousePositionContainerRoot.Y - MousePositionObjectContainer.Y;

                                ContainerSelect_Event(this, this._ContainerName, this._ContainerType, MousePositionContainerRoot, new Size(ContainerPanel.Width, ContainerRoot.Height));
                            }

                        }
                        else //image object which is without containerpanel
                        {

                            if (ContainerSelect_Event != null)
                            {
                                MousePositionContainerRoot.X = MousePositionContainerRoot.X - MousePositionObjectContainer.X;
                                MousePositionContainerRoot.Y = MousePositionContainerRoot.Y - MousePositionObjectContainer.Y;

                                ContainerSelect_Event(this, this._ContainerName, this._ContainerType, MousePositionContainerRoot, new Size(ContainerRoot.Width, ContainerRoot.Height));
                            }
                        }
                        ContainerRectTL.Visibility = Visibility.Visible;
                        ContainerRectTR.Visibility = Visibility.Visible;
                        ContainerRectLB.Visibility = Visibility.Visible;
                        ContainerRectRB.Visibility = Visibility.Visible;
                        ContainerRectLM.Visibility = Visibility.Visible;
                        ContainerRectTM.Visibility = Visibility.Visible;
                        ContainerRectRM.Visibility = Visibility.Visible;
                        ContainerRectBM.Visibility = Visibility.Visible;

                        if (_ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage && _ContainerType != ContainerTypes.LineVector && _ContainerType != ContainerTypes.RectangleVector && _ContainerType != ContainerTypes.EllipseVector)
                        {
                            pnlStatus.Visibility = Visibility.Visible;
                            btnEditIcon.Visibility = Visibility.Visible;

                            if (IsLockedEditing == false)
                            {
                                SetcontainerMode(ContainerMode.LockedEditing);
                            }
                        }

                        ContainerRoot.Background = new SolidColorBrush(Colors.LightGray);// = new Thickness(1.0);
                        brdObject.BorderThickness = new Thickness(0.5);
                        //ContainerRotateCn.Visibility = Visibility.Visible;
                        ContainerRoot.Cursor = Cursors.Hand;

                        if (IsLockedPosition)
                        {
                            SetcontainerMode(ContainerMode.LockedPosition);
                        }
                  

                       
                       

                        if (IsPrintable != false) //logic is inverse
                        {
                            SetcontainerMode(ContainerMode.NonPrintable);

                        }

                        Selected = true;
                        
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       
        public void UnSelectContainer(bool mode)
        {
            //UnSelectChildObject(mode);
            ContainerRectTL.Visibility = Visibility.Collapsed;
            ContainerRectTR.Visibility = Visibility.Collapsed;
            ContainerRectLB.Visibility = Visibility.Collapsed;
            ContainerRectRB.Visibility = Visibility.Collapsed;
            ContainerRectLM.Visibility = Visibility.Collapsed;
            ContainerRectTM.Visibility = Visibility.Collapsed;
            ContainerRectRM.Visibility = Visibility.Collapsed;
            ContainerRectBM.Visibility = Visibility.Collapsed;
            pnlStatus.Visibility = Visibility.Collapsed;
            btnEditIcon.Visibility = Visibility.Collapsed;
            ContainerRoot.Background = new SolidColorBrush(Colors.Transparent); //.BorderThickness = new Thickness(0.0);
            brdObject.BorderThickness = new Thickness(0);
            //ContainerRotateCn.Visibility = Visibility.Collapsed;
            ContainerRoot.Cursor = Cursors.Arrow;
            Selected = false;
            
            if (ContainerUnSelect_Event != null)
                ContainerUnSelect_Event(this, this._ContainerName,this._ContainerType,mode);


            if (ContainerPanel != null && ContainerPanel.Children.Count > 0)
            {
                ContainerPanel.Children[0].Visibility = System.Windows.Visibility.Visible;
                if (ContainerPanel.Children.Count > 1)
                    ContainerPanel.Children[1].Visibility = System.Windows.Visibility.Collapsed;
            }

          

           
        }
        private void Container_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (_DoubleClickDetector.isDoubleClick(sender))
            //{
            //    SelectChildObject(sender, e.GetPosition(null));
            //}
            //else
            //{ //single click
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
                {
                    //if (!Disabled)
                    //{
                        UIElement el = (UIElement)sender;
                        SelectContainer("", e.GetPosition(el), e.GetPosition(null));

                       

                        el.CaptureMouse();
                        MouseDown = true;
                        MousePoiner = e.GetPosition(this);
                        //e.Handled = true;

                        
                    //}
                }
            //}
       
            
            //ContainerContent.Text = sender.GetType().Name;
            //if (sender.GetType().Name == "Grid")
            //{
            //    //tmp.Text = "d";
            //    //isUnSel = false;
            //    //UnSelObject();
            //    //SelObject(sender);
            //    IsObjMove = true;
            //    //Border DsnObject;
            //    //if (e.OriginalSource.GetType().Name == "TextBlock")
            //    //{
            //    //    DsnObject = (Border)((TextBlock)e.OriginalSource).Parent;
            //    //}
            //    //else
            //    //    DsnObject = (Border)e.OriginalSource;
            //    DsnSelObject = (Grid)sender;
            //    ObjOffSetX = e.GetPosition(DsnSelObject).X;
            //    ObjOffSetY = e.GetPosition(DsnSelObject).Y;
            //    ObjNowW = DsnSelObject.Width;
            //    ObjNowH = DsnSelObject.Height;
            //    ContainerContent.Text = ObjOffSetX.ToString();
            //    DsnSelObject.CaptureMouse();
            //}

        }


        //new double click handler for the object container
        ////private void Container_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        ////{

        private void btnEditIcon_Click(object sender, RoutedEventArgs e)
        {

        
            //MessageBox.Show("dbl click happened");
            //SelectChildObject(sender,e.GetPosition(null));


            //new code here

            if (IsLockedEditing && _ContainerType != ContainerTypes.Image && _ContainerType != ContainerTypes.LogoImage)
            {
                TextBlock tb = (TextBlock)ContainerPanel.Children[0];
                TextBox tbEdit = (TextBox)ContainerPanel.Children[1];

                tbEdit.FontFamily = tb.FontFamily;
                tbEdit.FontSource = tb.FontSource;

                tbEdit.FontSize = tb.FontSize;
                tbEdit.Foreground = tb.Foreground;

                tbEdit.FontWeight = tb.FontWeight;
                tbEdit.FontStyle = tb.FontStyle;

                tbEdit.Width = tb.Width;
                tbEdit.Height = tb.Height;

                //tbEdit.Style = TransparentTextBox
                tbEdit.Opacity = 1;


                ContainerPanel.Children[1].Visibility = System.Windows.Visibility.Visible;
                ContainerPanel.Children[0].Visibility = System.Windows.Visibility.Collapsed;
                tbEdit.Focus();


                ContainerRoot.Background = new SolidColorBrush(Colors.Transparent);
                brdObject.BorderThickness = new Thickness(0);
            }
            
        }

        private void Container_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseDown = false;
            OriginalSize.Width = ContainerRoot.ActualWidth;
            OriginalSize.Height = ContainerRoot.ActualHeight;
        }
        private void Container_MouseMove(object sender, MouseEventArgs e)
        {
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!IsLockedPosition && !MouseDownResizing && !MouseDownRotating && MouseDown && Selected)
                {
                    MouseObjectMove_Event(this, e);
                    //UIElement el = (UIElement)sender;
                }
            }
        }

        private void ContainerResizeRM_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ScaleTransform ContentScaler = FindContentScale();
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!IsLockedPosition)
                {

                    UIElement el = (UIElement)sender;
                    el.CaptureMouse();
                    MouseDownResizing = true;
                    MousePoiner = e.GetPosition(ContainerRoot);
                    OriginalSize.Width = ContainerRoot.ActualWidth;
                    OriginalSize.Height = ContainerRoot.ActualHeight;
                    //ScalingFactor.Width = ContentScaler.ScaleX;
                    //ScalingFactor.Height = ContentScaler.ScaleY;
                }
            }
        }
        private void ContainerResizeRM_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseDownResizing = false;
            OriginalSize.Width = ContainerRoot.ActualWidth;
            OriginalSize.Height = ContainerRoot.ActualHeight;
        }


        private void ContainerResizeRM_MouseMoveAspectHorizontal(object sender, MouseEventArgs e)
        {
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!IsLockedPosition && MouseDownResizing) //&& _ContainerType == 3 // 
                {
                    MouseObjectResizeRM_Event(this, e,  AspectResize.Horizontal);
                }
            }
        }

        private void ContainerResizeRM_MouseMoveAspectVertical(object sender, MouseEventArgs e)
        {
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!IsLockedPosition && MouseDownResizing) //&& _ContainerType == 3
                {
                    MouseObjectResizeRM_Event(this, e, AspectResize.Vertical);
                }
            }
        }

        private void ContainerResizeRM_MouseMoveAspectLocked(object sender, MouseEventArgs e)
        {
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!IsLockedPosition && MouseDownResizing) //&& _ContainerType == 3
                {
                    MouseObjectResizeRM_Event(this, e, AspectResize.LockedAspect);
                }
            }
        }

        private void ContainerResizeLM_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ////ScaleTransform ContentScaler = FindContentScale();
            //if (!Disabled)
            //{

            //    UIElement el = (UIElement)sender;
            //    el.CaptureMouse();
            //    MouseDownResizing = true;
            //    MousePoiner = e.GetPosition(ContainerRoot);
            //    OriginalSize.Width = ContainerRoot.ActualWidth;
            //    OriginalSize.Height = ContainerRoot.ActualHeight;
            //    //ScalingFactor.Width = ContentScaler.ScaleX;
            //    //ScalingFactor.Height = ContentScaler.ScaleY;
            //}
        }
        private void ContainerResizeLM_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //MouseDownResizing = false;
            //OriginalSize.Width = ContainerRoot.ActualWidth;
            //OriginalSize.Height = ContainerRoot.ActualHeight;
        }
        private void ContainerResizeLM_MouseMove(object sender, MouseEventArgs e)
        {
            //if (!Disabled && MouseDownResizing)
            //{
            //    MouseObjectResizeLM_Event(this, e);
            //    //UIElement el = (UIElement)sender;
            //    //MessageBox.Show("rs");
            //}
        }


        private void Container_LostMouseCapture(object sender, MouseEventArgs e)
        {
            MouseDown = false;
        }
        private void ContainerRotate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!IsLockedPosition)
                {
                    UIElement el = (UIElement)sender;
                    el.CaptureMouse();
                    MouseDownRotating = true;
                    MousePoiner = e.GetPosition(ContainerRoot);

                    //RotateTransform rt = FindContentRotate();
                    AngleOffset = FindAngle();
                    ObjectAngle = AngleOffset;
                    OriginalSize.Width = ContainerRoot.ActualWidth;
                    OriginalSize.Height = ContainerRoot.ActualHeight;
                }
            }
        }

        private void ContainerRotate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!IsLockedPosition)
                {
                    UIElement el = (UIElement)sender;
                    el.ReleaseMouseCapture();
                    MouseDownResizing = false;
                    OriginalSize.Width = ContainerRoot.ActualWidth;
                    OriginalSize.Height = ContainerRoot.ActualHeight;
                    ContainerRoot.UpdateLayout();
                }
            }
        }
        private void ContainerRotate_MouseMove(object sender, MouseEventArgs e)
        {
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!IsLockedPosition && MouseDownRotating && Selected)
                {
                    MouseObjectRotate_Event(this, e);
                    //UIElement el = (UIElement)sender;
                }
            }
        }
        private void ContainerRotate_LostMouseCapture(object sender, MouseEventArgs e)
        {
            MouseDownRotating = false;
        }
        private double FindAngle()
        {
            Point ControlLoc = new Point(ContainerRoot.Width, 0); // half a control+a control width+1px border
            Point CenterLoc = new Point();
            Size Dimension = new Size(ContainerRoot.ActualWidth, ContainerRoot.ActualHeight);
            Dimension.Width /= 2;
            Dimension.Height /= 2;
            CenterLoc.X = Dimension.Width;
            CenterLoc.Y = Dimension.Height;
            Double Radians = Math.Atan2(-ControlLoc.Y + CenterLoc.Y, ControlLoc.X - CenterLoc.X);
            Double NewAngle = Radians * (180 / Math.PI);
            NewAngle += 90;
            RotateTransform rt = FindContentRotate();
            return (NewAngle);
        }

        private void ContainerRoot_MouseEnter(object sender, MouseEventArgs e)
        {
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!Selected && !MouseDownResizing && !MouseDownRotating) //!IsLockedPosition && 
                {
                    //ContainerBrd.BorderThickness = new Thickness(0.2);
                    ContainerRoot.Background = new SolidColorBrush(Colors.LightGray);
                    brdObject.BorderThickness = new Thickness(1);

                    switch (this.SelectedObect.Name)
                    {
                        case "CompanyName":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "CompanyMessage":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "Name":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "Title":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "AddressLine1":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "AddressLine2":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "AddressLine3":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "Phone":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "Fax":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "Email":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                        case "Website":
                            {
                                brdObject.BorderBrush = new SolidColorBrush(Colors.Green);
                                break;
                            }
                    }
                }
            }
           
        }

        private void ContainerRoot_MouseLeave(object sender, MouseEventArgs e)
        {
            if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
            {
                if (!Selected && !MouseDownResizing && !MouseDownRotating) //!IsLockedPosition && 
                {
                    //ContainerBrd.BorderThickness = new Thickness(0.0);
                    ContainerRoot.Background = new SolidColorBrush(Colors.Transparent);
                    brdObject.BorderThickness = new Thickness(0);
                }
            }
        }

        private void ContainerRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ContainerRoot.Width < 12)
            {
                ContainerRoot.Width = 12;
            }

            if (ContainerRoot.Height < 1)
            {
                ContainerRoot.Height = 1;
            }

            
            
        }

        private void ContainerRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
            {
                if (ContainerLoadedChangeSize_Event != null)
                {
                    if (ContainerPanel == null)
                        ContainerPanel = (AdjustablePanel)ContainerRoot.FindName("AP" + _ContainerName);
                    if (ContainerPanel != null)
                    {
                        Point NPntDff = new Point(0, 0);
                        //if (ContainerPanel.HAlign == TextAlignment.Left)
                        //    NPntDff.X = 0;
                        //else if (ContainerPanel.HAlign == TextAlignment.Center)
                        //    NPntDff.X = ContainerRoot.ActualWidth / 2;
                        //else if (ContainerPanel.HAlign == TextAlignment.Right)
                        //    NPntDff.X = ContainerRoot.ActualWidth;

                        if (ContainerPanel.VAlign == VerticalAlignment.Top)
                            NPntDff.Y = 0;
                        else if (ContainerPanel.VAlign == VerticalAlignment.Center)
                            NPntDff.Y = 0;
                        else if (ContainerPanel.VAlign == VerticalAlignment.Bottom)
                            NPntDff.Y = ContainerRoot.ActualHeight;
                        ContainerLoadedChangeSize_Event(this, NPntDff);
                    }
                }
                IsLoaded=true;
            }
        }

        
    }


    public class DoubleClickDetection
    {
        private object _previousSender;
        private DateTime _previousClickTime;

        private TimeSpan _timeDifference;
        public DoubleClickDetection()
        {
            _previousClickTime = DateTime.Now;
            _timeDifference = System.TimeSpan.Zero;
        }

        public bool isDoubleClick(object sender)
        {
            System.DateTime _currentClickTime = DateTime.Now;
            if ((!object.ReferenceEquals(sender, _previousSender)))
            {
                _previousSender = sender;
                _previousClickTime = _currentClickTime;
                return false;
            }
            else
            {
                _timeDifference = _currentClickTime - _previousClickTime;
                _previousClickTime = _currentClickTime;
                if (_timeDifference.TotalMilliseconds <= 300)
                {

                    _previousSender = null;
                    return true;
                }
                else
                {
                    _previousSender = null;
                    return false;
                }
            }
        }

    }

}
