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
using System.Windows.Controls.Primitives;
namespace webprintDesigner.EnhancedControls
{
    public partial class PageContainer : UserControl
    {
        public Point pt;
        public Page ParentPage;
        public bool ShowException = false;
        private int CtlDisplayIdxSt = 0;
        public string PageContainerName="";
        public string PageContrlListName = "";
        public PageTxtControl objPageTxtControl;
        public string BackgroundImageName;
        public bool IsBackSidePage;
        public int PageNo;
        public double TrimThickness;
        public bool IsSnapToGrid = false;

        public string SelectedContainerName = string.Empty;
        

        List<int> SnapXPoints = new List<int>();
        List<int> SnapYPoints = new List<int>();

        public delegate void ObjectSelected_EventHandler(object sender,bool SelectState, string ControlName, bool unSelectMode, Point e,Size ContainerSize);
        public event ObjectSelected_EventHandler ObjectSelect_Event;

        #region GuideElements
        
        TextBlock oSafeGuideText1 = new TextBlock();
        TextBlock oSafeGuideText2 = new TextBlock();
        TextBlock oSafeGuideText3 = new TextBlock();
        TextBlock oSafeGuideText4 = new TextBlock();

        TextBlock oSafeGuideText5 = new TextBlock();
        TextBlock oSafeGuideText6 = new TextBlock();
        TextBlock oSafeGuideText7 = new TextBlock();
        TextBlock oSafeGuideText8 = new TextBlock();
        #endregion

        public String TemplateHeight
        {
            
            set { lblHeight.Text = value; }
        }

        public String TemplateWidth
        {

            set {
                lblWidth.Text = value; 
            }
        }



        public bool Enable3D
        {
            
            set
            {
                if (value)
                {
                    gridShadow.Width = brdDesign.Width;
                    gridShadow.Height = brdDesign.Height / 3;

                    gridShadow.Visibility = System.Windows.Visibility.Visible;
                    PageRoot.MouseMove += new MouseEventHandler(scvDesign_MouseMove);
                }
                else
                {
                    
                    gridShadow.Visibility = System.Windows.Visibility.Collapsed;
                    PageRoot.MouseMove -= new MouseEventHandler(scvDesign_MouseMove);
                }
            }
        }
        
        

        public PageContainer(Size PageSize, string PConName, string PCtName,bool IsBSidePage,int PNo, double trimThickness, bool ApplyFoldLines, List<webprintDesigner.ProductServiceReference.tbl_ProductCategoryFoldLines> FoldLines, Page parentPage)
        {
            InitializeComponent();
            
            scvDesign.MouseMove += new MouseEventHandler(scvDesign_MouseMove);
            ParentPage = parentPage;

            TrimThickness = trimThickness;
            brdDesign.Width = PageSize.Width;
            brdDesign.Height = PageSize.Height;
            cnvDesign.Width = PageSize.Width + TrimThickness;
            cnvDesign.Height = PageSize.Height + TrimThickness;

            TrimArea.Height = PageSize.Height - trimThickness*2;
            TrimArea.Width = PageSize.Width - trimThickness*2;
            TrimArea.BorderThickness = new Thickness(1);
            TrimArea.Margin = new Thickness(trimThickness, trimThickness, 0, 0);

            double SafeAreaMargin = TrimThickness + 8.49;
            SafeArea.Margin = new Thickness(SafeAreaMargin, SafeAreaMargin, 0, 0);
            SafeArea.Height = PageSize.Height - (TrimThickness * 2) - (8.49 *2);
            SafeArea.Width = PageSize.Width - (TrimThickness *2) - (8.49 *2);

            if (cnvDesign.Height / 2 - 82 > 0)
            {
                leftTopArrow.Height = cnvDesign.Height / 2 - 82;
                leftBottomArrow.Height = cnvDesign.Height / 2 - 82;
            }
            else
            {
                leftTopArrow.Height = cnvDesign.Height / 2;
                leftBottomArrow.Height = cnvDesign.Height / 2;
            }


            TopLeftArrow.Width = cnvDesign.Width / 2- 87;
            TopRightArrow.Width = cnvDesign.Width / 2 - 87;


            DesignArea.Width = PageSize.Width;
            DesignArea.Height = PageSize.Height;
            PageContainerName = PConName;
            PageContrlListName = PCtName;
            BackgroundImageName = "";
            IsBackSidePage = IsBSidePage;
            PageNo = PNo;

            GuideElements.Height = PageSize.Height + 50;

            int iCounter = 1;
            
            while (iCounter < DesignArea.ActualWidth)
            {
                //Line oln = new Line();
                //oln.X1 = iCounter;
                //oln.Y1 = 1;
                //oln.Stroke = new SolidColorBrush(Colors.Green);
                //oln.StrokeThickness = 1;
                //oln.X2 = iCounter;
                //oln.Y2 = DesignArea.ActualWidth -1;

                SnapXPoints.Add(iCounter);

                //DesignArea.Children.Add(oln);

                iCounter += 5;
            }

            iCounter = 1;

            while (iCounter < DesignArea.ActualHeight)
            {
                //Line oln = new Line();
                //oln.X1 = 1;
                //oln.Y1 = iCounter;
                //oln.Stroke = new SolidColorBrush(Colors.Green);
                //oln.StrokeThickness = 1;
                //oln.X2 = DesignArea.ActualWidth - 1;
                //oln.Y2 = iCounter;

                SnapYPoints.Add(iCounter);

                //DesignArea.Children.Add(oln);

                iCounter += 5;
            }

            //safe zone guides

            double safezonetexthalf = 40;


            //top left
            
            oSafeGuideText1.Text = "SAFE ZONE GUIDE";
            oSafeGuideText1.SetValue(Canvas.LeftProperty, (DesignArea.ActualWidth / 4) - safezonetexthalf);
            oSafeGuideText1.SetValue(Canvas.TopProperty, 13d);
            oSafeGuideText1.SetValue(Canvas.ZIndexProperty, 1000);
            oSafeGuideText1.FontSize = 9;
            oSafeGuideText1.Foreground =  new SolidColorBrush( Colors.Gray);
            
            DesignArea.Children.Add(oSafeGuideText1);

            //top right

            oSafeGuideText2.Text = "SAFE ZONE GUIDE";
            oSafeGuideText2.SetValue(Canvas.LeftProperty, DesignArea.ActualWidth - (DesignArea.ActualWidth / 4) - safezonetexthalf);
            oSafeGuideText2.SetValue(Canvas.TopProperty, 13d);
            oSafeGuideText2.SetValue(Canvas.ZIndexProperty, 1000);
            oSafeGuideText2.FontSize = 9;
            oSafeGuideText2.Foreground = new SolidColorBrush(Colors.Gray);
            DesignArea.Children.Add(oSafeGuideText2);

            //bottom left

            oSafeGuideText3.Text = "SAFE ZONE GUIDE";
            oSafeGuideText3.SetValue(Canvas.LeftProperty, (DesignArea.ActualWidth / 4) - safezonetexthalf);
            oSafeGuideText3.SetValue(Canvas.TopProperty, DesignArea.ActualHeight - 25d);
            oSafeGuideText3.SetValue(Canvas.ZIndexProperty, 1000);
            oSafeGuideText3.FontSize = 9;
            oSafeGuideText3.Foreground = new SolidColorBrush(Colors.Gray);
            DesignArea.Children.Add(oSafeGuideText3);

            //top right
            
            oSafeGuideText4.Text = "SAFE ZONE GUIDE";
            oSafeGuideText4.SetValue(Canvas.LeftProperty, DesignArea.ActualWidth - (DesignArea.ActualWidth / 4) - safezonetexthalf);
            oSafeGuideText4.SetValue(Canvas.TopProperty, DesignArea.ActualHeight - 25d);
            oSafeGuideText4.SetValue(Canvas.ZIndexProperty, 1000);
            oSafeGuideText4.FontSize = 9;
            oSafeGuideText4.Foreground = new SolidColorBrush(Colors.Gray);
            DesignArea.Children.Add(oSafeGuideText4);

            

            if (ApplyFoldLines)
            {

                //horizontal lines
                var Lines = FoldLines.Where(g => g.FoldLineOrientation == false).ToList();


                foreach (var lineItem in Lines)
                {
                    Line oln = new Line();
                    oln.X1 = 1;
                    oln.Y1 = lineItem.FoldLineOffsetFromOrigin.Value;
                    oln.Stroke = new SolidColorBrush(Colors.Green);
                    oln.StrokeThickness = 1;
                    oln.X2 = DesignArea.ActualWidth - 1;
                    oln.Y2 = lineItem.FoldLineOffsetFromOrigin.Value;
                    oln.SetValue(Canvas.ZIndexProperty, 1000);


                    Line oln1 = new Line();
                    oln1.X1 = 1;
                    oln1.Y1 = lineItem.FoldLineOffsetFromOrigin.Value - 8;
                    oln1.Stroke = new SolidColorBrush(Colors.Cyan);
                    oln1.StrokeThickness = 1;
                    oln1.X2 = DesignArea.ActualWidth - 1;
                    oln1.Y2 = lineItem.FoldLineOffsetFromOrigin.Value - 8;
                    oln1.SetValue(Canvas.ZIndexProperty, 1000);

                    Line oln2 = new Line();
                    oln2.X1 = 1;
                    oln2.Y1 = lineItem.FoldLineOffsetFromOrigin.Value + 8;
                    oln2.Stroke = new SolidColorBrush(Colors.Cyan);
                    oln2.StrokeThickness = 1;
                    oln2.X2 = DesignArea.ActualWidth - 1;
                    oln2.Y2 = lineItem.FoldLineOffsetFromOrigin.Value + 8;
                    oln2.SetValue(Canvas.ZIndexProperty, 1000);


                    TextBlock oText = new TextBlock();
                    oText.Text = "FOLDING AREA";
                    oText.FontSize = 9;
                    oText.Foreground = new SolidColorBrush(Colors.Gray);
                    oText.SetValue(Canvas.LeftProperty, DesignArea.ActualWidth / 2);
                    oText.SetValue(Canvas.TopProperty, lineItem.FoldLineOffsetFromOrigin.Value - 6);
                    oText.SetValue(Canvas.ZIndexProperty, 1000);
                    
                    DesignArea.Children.Add(oln);
                    DesignArea.Children.Add(oln1);
                    DesignArea.Children.Add(oln2);
                    DesignArea.Children.Add(oText);
                }


                var LinesVert = FoldLines.Where(g => g.FoldLineOrientation == true).ToList();


                foreach (var lineItem in LinesVert)
                {
                    Line oln = new Line();
                    oln.X1 = lineItem.FoldLineOffsetFromOrigin.Value;
                    oln.Y1 = 1;
                    oln.Stroke = new SolidColorBrush(Colors.Green);
                    oln.StrokeThickness = 1;
                    oln.X2 = lineItem.FoldLineOffsetFromOrigin.Value;
                    oln.Y2 = DesignArea.ActualHeight - 1;
                    oln.SetValue(Canvas.ZIndexProperty, 1000);


                    Line oln1 = new Line();
                    oln1.X1 = lineItem.FoldLineOffsetFromOrigin.Value + 8;
                    oln1.Y1 = 1;
                    oln1.Stroke = new SolidColorBrush(Colors.Cyan);
                    oln1.StrokeThickness = 1;
                    oln1.X2 = lineItem.FoldLineOffsetFromOrigin.Value + 8;
                    oln1.Y2 = DesignArea.ActualHeight - 1;
                    oln1.SetValue(Canvas.ZIndexProperty, 1000);

                    Line oln2 = new Line();
                    oln2.X1 = lineItem.FoldLineOffsetFromOrigin.Value - 8;
                    oln2.Y1 = 1;
                    oln2.Stroke = new SolidColorBrush(Colors.Cyan);
                    oln2.StrokeThickness = 1;
                    oln2.X2 = lineItem.FoldLineOffsetFromOrigin.Value - 8;
                    oln2.Y2 = DesignArea.ActualHeight - 1;
                    oln2.SetValue(Canvas.ZIndexProperty, 1000);

                    DesignArea.Children.Add(oln);
                    DesignArea.Children.Add(oln1);
                    DesignArea.Children.Add(oln2);

                    TextBlock oText = new TextBlock();
                    oText.Text = "FOLDING AREA";
                    oText.FontSize = 9;
                    oText.Foreground = new SolidColorBrush(Colors.Gray);
                    oText.SetValue(Canvas.LeftProperty, lineItem.FoldLineOffsetFromOrigin.Value + 6);
                    oText.SetValue(Canvas.TopProperty, DesignArea.ActualHeight / 2);
                    oText.SetValue(Canvas.ZIndexProperty, 1000);

                    RotateTransform rt = new RotateTransform();
                    //rt.SetValue(Canvas.NameProperty, "ContentRotate");
                    rt.Angle = 90;
                    rt.CenterX = 0d;
                    rt.CenterY = 0d;
                    oText.RenderTransform = rt;

                    DesignArea.Children.Add(oText);


                }
                

            }

                //left top

                oSafeGuideText5.Text = "SAFE ZONE GUIDE";
                oSafeGuideText5.FontSize = 9;
                oSafeGuideText5.Foreground = new SolidColorBrush(Colors.Gray);

                RotateTransform rotateTransform2 = new RotateTransform();
                rotateTransform2.Angle = 270;
                rotateTransform2.CenterX = 0d;
                rotateTransform2.CenterY = 0d;
                oSafeGuideText5.RenderTransform = rotateTransform2;
                oSafeGuideText5.SetValue(Canvas.LeftProperty, 13d);
                oSafeGuideText5.SetValue(Canvas.TopProperty, (DesignArea.ActualHeight / 4) + safezonetexthalf);
                oSafeGuideText5.SetValue(Canvas.ZIndexProperty, 1000);
                DesignArea.Children.Add(oSafeGuideText5);

                //left bottom
                
                oSafeGuideText6.Text = "SAFE ZONE GUIDE";
                oSafeGuideText6.FontSize = 9;
                oSafeGuideText6.Foreground = new SolidColorBrush(Colors.Gray);

                rotateTransform2 = new RotateTransform();
                rotateTransform2.Angle = 270;
                rotateTransform2.CenterX = 0d;
                rotateTransform2.CenterY = 0d;
                oSafeGuideText6.RenderTransform = rotateTransform2;
                oSafeGuideText6.SetValue(Canvas.LeftProperty, 13d);
                oSafeGuideText6.SetValue(Canvas.TopProperty, DesignArea.ActualHeight - (DesignArea.ActualHeight / 4) + safezonetexthalf);
                oSafeGuideText6.SetValue(Canvas.ZIndexProperty, 1000);
                DesignArea.Children.Add(oSafeGuideText6);

                //right top
                
                oSafeGuideText7.Text = "SAFE ZONE GUIDE";
                oSafeGuideText7.FontSize = 9;
                oSafeGuideText7.Foreground = new SolidColorBrush(Colors.Gray);

                rotateTransform2 = new RotateTransform();
                rotateTransform2.Angle = 90;
                rotateTransform2.CenterX = 0d;
                rotateTransform2.CenterY = 0d;
                oSafeGuideText7.RenderTransform = rotateTransform2;
                oSafeGuideText7.SetValue(Canvas.LeftProperty, DesignArea.ActualWidth - 13d);
                oSafeGuideText7.SetValue(Canvas.TopProperty, (DesignArea.ActualHeight / 4) + safezonetexthalf - 80d);
                oSafeGuideText7.SetValue(Canvas.ZIndexProperty, 1000);
                DesignArea.Children.Add(oSafeGuideText7);


                //right bottom
                
                oSafeGuideText8.Text = "SAFE ZONE GUIDE";
                oSafeGuideText8.FontSize = 9;
                oSafeGuideText8.Foreground = new SolidColorBrush(Colors.Gray);

                rotateTransform2 = new RotateTransform();
                rotateTransform2.Angle = 90;
                rotateTransform2.CenterX = 0d;
                rotateTransform2.CenterY = 0d;
                oSafeGuideText8.RenderTransform = rotateTransform2;
                oSafeGuideText8.SetValue(Canvas.LeftProperty, DesignArea.ActualWidth - 13d);
                oSafeGuideText8.SetValue(Canvas.TopProperty, DesignArea.ActualHeight - ((DesignArea.ActualHeight / 4)) + safezonetexthalf - 80d);
                oSafeGuideText8.SetValue(Canvas.ZIndexProperty, 1000);
                DesignArea.Children.Add(oSafeGuideText8);

                    
                
            

        }
        #region "Add Controls"
        public ObjectContainer AddObjects(string ObjectContainerName, string TxtCtlName, ProductServiceReference.TemplateObjects objObects, webprintDesigner.ProductServiceReference.TemplateFonts ObjectFont)
        {
            ObjectContainer objCon = null;
            try
            {
                if (objObects.ObjectType == 1 || objObects.ObjectType == 2 || objObects.ObjectType == 4)
                {

                    objCon = new ObjectContainer(ObjectContainerName,  objObects, ObjectFont);
                    objCon.MouseObjectMove_Event += new ObjectContainer.MouseObjectMove_EventHandler(ObjectMove);
                    objCon.MouseObjectResizeRM_Event += new ObjectContainer.MouseObjectResizeRM_EventHandler(ObjectResize);
                    objCon.MouseObjectResizeLM_Event += new ObjectContainer.MouseObjectResizeLM_EventHandler(ObjectResizeMove);
                    objCon.MouseObjectRotate_Event += new ObjectContainer.MouseObjectRotate_EventHandler(ObjectRotate);
                    objCon.ContainerSelect_Event += new ObjectContainer.ContainerSelect_EventHandler(objCon_ContainerSelect_Event);
                    //objCon.ContainerUnSelect_Event += new ObjectContainer.ContainerUnSelect_EventHandler(objCon_ContainerUnSelect_Event);
                    objCon.ObjectPosChange_Event += new ObjectContainer.ObjectPosChange_EventHandler(objCon_ObjectPosChange_Event);
                    objCon.ContainerLoadedChangeSize_Event += new ObjectContainer.ContainerLoadedChangeSize_EventHandler(objCon_ContainerLoadedChangeSize_Event);
                    DesignArea.Children.Add(objCon);
                    objCon.SetValue(Canvas.LeftProperty, webprintDesigner.Common.PointToPixel(objObects.PositionX));
                    objCon.SetValue(Canvas.TopProperty, webprintDesigner.Common.PointToPixel(objObects.PositionY));
                    //objCon.SetValue(Canvas.ZIndexProperty, objObects.DisplayOrderPdf);
                    objCon.SetValue(Canvas.ZIndexProperty, CtlDisplayIdxSt + objObects.DisplayOrderPdf);
                    
                    //if (cmbFontList.SelectedIndex != -1)
                    //{
                    //    foreach (webprintDesigner.ProductServiceReference.ProductFonts fnt in cmbFontList.Items)
                    //    {
                    //        if (fnt.FontDisplayName.ToLower() == objObects.FontName.ToLower())
                    //        {
                    //            objCon.UpdateContainerFont(fnt);
                    //        }
                    //    }
                    //}
                    //if (ObjectFont != null)
                      //  objCon.UpdateContainerFont(ObjectFont);
                    //objCon.UpdateContainerFontSize(webprintDesigner.Common.PointToPixel(objObects.FontSize));
                    //objCon.UpdateContainerAlign(objObects.Allignment);
                    //objCon.AddChildCtrol(objObects, ObjectFont);

                    objCon.UpdateContainerLockEditing(objObects.IsEditable, false);

                    if (objObects.IsPositionLocked != null)
                    {
                        objCon.UpdateContainerLockPosition(objObects.IsPositionLocked.Value,false);
                    }

                    objCon.UpdateContainerShowHide(objObects.IsHidden, false);
                    //objCon.UpdateContainerBold(objObects.IsBold);
                    //objCon.UpdateContainerItalic(objObects.IsItalic);
                    //objCon.UpdateContainerUnderline(objObects.IsUnderlinedText);
                   // objCon.UpdateContainerColor(objObects.ColorC, objObects.ColorM, objObects.ColorY, objObects.ColorK);
                    objCon.UpdateContainer();

                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                {
                    MessageBox.Show("::AddObjects::" + ex.ToString());
                    MessageBox.Show("::AddObjects::stack=" + ex.StackTrace.ToString());
                }
            }
            return objCon;
        }
              
        public void AddObject(string ObjectContainerName, string TxtCtlName, string CName, string CText, int Ctype, webprintDesigner.ProductServiceReference.TemplateFonts cmbItm, double FontSize, ProductServiceReference.TemplateObjects objObects)
        {
            try
            {
                // HtmlPage.Window.Alert(this.Resources.Count.ToString());
                if (Ctype == 1 || Ctype == 2 || Ctype == 4)
                {
                    int DisplayIndx = GetMaxZidx();
                    ObjectContainer objCon = new ObjectContainer(ObjectContainerName, objObects, cmbItm);
                    objCon.MouseObjectMove_Event += new ObjectContainer.MouseObjectMove_EventHandler(ObjectMove);
                    objCon.MouseObjectResizeRM_Event += new ObjectContainer.MouseObjectResizeRM_EventHandler(ObjectResize);
                    objCon.MouseObjectResizeLM_Event += new ObjectContainer.MouseObjectResizeLM_EventHandler(ObjectResizeMove);
                    objCon.MouseObjectRotate_Event += new ObjectContainer.MouseObjectRotate_EventHandler(ObjectRotate);
                    objCon.ContainerSelect_Event += new ObjectContainer.ContainerSelect_EventHandler(objCon_ContainerSelect_Event);
                    //objCon.ContainerUnSelect_Event += new ObjectContainer.ContainerUnSelect_EventHandler(objCon_ContainerUnSelect_Event);
                    objCon.ObjectPosChange_Event += new ObjectContainer.ObjectPosChange_EventHandler(objCon_ObjectPosChange_Event);
                    DesignArea.Children.Add(objCon);
                    objCon.SetValue(Canvas.LeftProperty, objObects.PositionX);
                    objCon.SetValue(Canvas.TopProperty, objObects.PositionY);
                    objCon.SetValue(Canvas.ZIndexProperty, DisplayIndx);

                    objCon.UpdateContainerAlign(objObects.Allignment);

                    objCon.UpdateContainerLockEditing(objObects.IsEditable,false);

                    objCon.UpdateContainerLockPosition(objObects.IsPositionLocked.Value,false);

                    objCon.UpdateContainerShowHide(objObects.IsHidden, false);
                    //if (cmbItm != null)
                    //{
                    //    objCon.UpdateContainerFont(cmbItm);
                    //}
                    
                    //objCon.UpdateContainerFontSize(FontSize);
                    objCon.UpdateContainer();

                    objCon.SelectContainer(CName, new Point(0, 0), new Point(0, 0));
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddObject::" + ex.ToString());
            }
        }

        public void AddObject(string ObjectContainerName, string TxtCtlName, string CName, string CText, int Ctype, webprintDesigner.ProductServiceReference.TemplateFonts cmbItm, double FontSize, ProductServiceReference.TemplateObjects objObects, Point XYPosition)
        {
            try
            {
                // HtmlPage.Window.Alert(this.Resources.Count.ToString());
                if (Ctype == 1 || Ctype == 2 || Ctype == 4)
                {
                    int DisplayIndx = GetMaxZidx();
                    ObjectContainer objCon = new ObjectContainer(ObjectContainerName, objObects, cmbItm);
                    objCon.MouseObjectMove_Event += new ObjectContainer.MouseObjectMove_EventHandler(ObjectMove);
                    objCon.MouseObjectResizeRM_Event += new ObjectContainer.MouseObjectResizeRM_EventHandler(ObjectResize);
                    objCon.MouseObjectResizeLM_Event += new ObjectContainer.MouseObjectResizeLM_EventHandler(ObjectResizeMove);
                    objCon.MouseObjectRotate_Event += new ObjectContainer.MouseObjectRotate_EventHandler(ObjectRotate);
                    objCon.ContainerSelect_Event += new ObjectContainer.ContainerSelect_EventHandler(objCon_ContainerSelect_Event);
                    //objCon.ContainerUnSelect_Event += new ObjectContainer.ContainerUnSelect_EventHandler(objCon_ContainerUnSelect_Event);
                    objCon.ObjectPosChange_Event += new ObjectContainer.ObjectPosChange_EventHandler(objCon_ObjectPosChange_Event);
                    DesignArea.Children.Add(objCon);
                    objCon.SetValue(Canvas.LeftProperty, XYPosition.X);
                    objCon.SetValue(Canvas.TopProperty, XYPosition.Y);
                    objCon.SetValue(Canvas.ZIndexProperty, DisplayIndx);

                    objCon.UpdateContainerAlign(objObects.Allignment);

                    objCon.UpdateContainerLockEditing(objObects.IsEditable,false);

                    objCon.UpdateContainerLockPosition(objObects.IsPositionLocked.Value,false);

                    objCon.UpdateContainerShowHide(objObects.IsHidden, false);
                    //if (cmbItm != null)
                    //{
                    //    objCon.UpdateContainerFont(cmbItm);
                    //}

                    //objCon.UpdateContainerFontSize(FontSize);
                    objCon.UpdateContainer();
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddObject::" + ex.ToString());
            }
        }

        public void AddImgObject(string ObjectContainerName, string CName, int DisplayIndx, ImageSource imgSrc, string imgPath, Point ObjectPos, Size ObjectSize, ProductServiceReference.TemplateObjects objObects, bool IsLogo)
        {
            try
            {
                if (DisplayIndx < 0)
                {
                    DisplayIndx = GetMaxZidx();
                }
                else
                    DisplayIndx = CtlDisplayIdxSt + DisplayIndx;
                ObjectContainer objCon = new ObjectContainer(ObjectContainerName, imgSrc, imgPath, ObjectSize.Width, ObjectSize.Height, CName, objObects, IsLogo);
                objCon.MouseObjectMove_Event += new ObjectContainer.MouseObjectMove_EventHandler(ObjectMove);
                objCon.MouseObjectResizeRM_Event += new ObjectContainer.MouseObjectResizeRM_EventHandler(ObjectResize);
                objCon.MouseObjectResizeLM_Event += new ObjectContainer.MouseObjectResizeLM_EventHandler(ObjectResizeMove);
                objCon.MouseObjectRotate_Event += new ObjectContainer.MouseObjectRotate_EventHandler(ObjectRotate);
                objCon.ContainerSelect_Event += new ObjectContainer.ContainerSelect_EventHandler(objCon_ContainerSelect_Event);
                //objCon.ContainerUnSelect_Event += new ObjectContainer.ContainerUnSelect_EventHandler(objCon_ContainerUnSelect_Event);
                DesignArea.Children.Add(objCon);
                objCon.SetValue(Canvas.LeftProperty, ObjectPos.X);
                objCon.SetValue(Canvas.TopProperty, ObjectPos.Y);
                objCon.SetValue(Canvas.ZIndexProperty, DisplayIndx);

                if (objObects != null)
                    objCon.UpdateContainerLockEditing(objObects.IsEditable,false);
                else
                    objCon.UpdateContainerLockEditing(false,false);


                if ( objObects != null && objObects.IsPositionLocked != null)
                    objCon.UpdateContainerLockPosition(objObects.IsPositionLocked.Value,false);
                else
                    objCon.UpdateContainerLockPosition(false,false);

                if (objObects != null)
                    objCon.UpdateContainerShowHide(objObects.IsHidden, false);
                else
                    objCon.UpdateContainerShowHide(false, false);

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddImgControls::" + ex.ToString());
            }
        }

        

        public void AddVectorLine(string ObjectContainerName, string CName, int DisplayIndx, double X1, double Y1,double LineStroke, ProductServiceReference.TemplateObjects oObject)
        {
            try
            {
                if (DisplayIndx < 0)
                {
                    DisplayIndx = GetMaxZidx();
                }
                else
                    DisplayIndx = CtlDisplayIdxSt + DisplayIndx;

                ObjectContainer objCon = new ObjectContainer(ObjectContainerName,X1,Y1,LineStroke, CName, oObject);
                objCon.MouseObjectMove_Event += new ObjectContainer.MouseObjectMove_EventHandler(ObjectMove);
                objCon.MouseObjectResizeRM_Event += new ObjectContainer.MouseObjectResizeRM_EventHandler(ObjectResize);
                objCon.MouseObjectResizeLM_Event += new ObjectContainer.MouseObjectResizeLM_EventHandler(ObjectResizeMove);
                objCon.MouseObjectRotate_Event += new ObjectContainer.MouseObjectRotate_EventHandler(ObjectRotate);
                objCon.ContainerSelect_Event += new ObjectContainer.ContainerSelect_EventHandler(objCon_ContainerSelect_Event);
                //objCon.ContainerUnSelect_Event += new ObjectContainer.ContainerUnSelect_EventHandler(objCon_ContainerUnSelect_Event);
                DesignArea.Children.Add(objCon);

                objCon.SetValue(Canvas.LeftProperty, X1);
                objCon.SetValue(Canvas.TopProperty, Y1);
                objCon.SetValue(Canvas.ZIndexProperty, DisplayIndx);

              
                if (oObject != null && oObject.IsPositionLocked != null)
                    objCon.UpdateContainerLockPosition(oObject.IsPositionLocked.Value,false);
                else
                    objCon.UpdateContainerLockPosition(false,false);

                if (oObject != null)
                    objCon.UpdateContainerShowHide(oObject.IsHidden, false);
                else
                    objCon.UpdateContainerShowHide(false, false);

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddVectorLine::" + ex.ToString());
            }
        }


        public void AddVectorRectangle(string ObjectContainerName, string CName, int DisplayIndx, double X1, double Y1, ProductServiceReference.TemplateObjects oObject)
        {
            try
            {
                if (DisplayIndx < 0)
                {
                    DisplayIndx = GetMaxZidx();
                }
                else
                    DisplayIndx = CtlDisplayIdxSt + DisplayIndx;

                ObjectContainer objCon = new ObjectContainer(ObjectContainerName, X1, Y1, CName, oObject, false);
                objCon.MouseObjectMove_Event += new ObjectContainer.MouseObjectMove_EventHandler(ObjectMove);
                objCon.MouseObjectResizeRM_Event += new ObjectContainer.MouseObjectResizeRM_EventHandler(ObjectResize);
                objCon.MouseObjectResizeLM_Event += new ObjectContainer.MouseObjectResizeLM_EventHandler(ObjectResizeMove);
                objCon.MouseObjectRotate_Event += new ObjectContainer.MouseObjectRotate_EventHandler(ObjectRotate);
                objCon.ContainerSelect_Event += new ObjectContainer.ContainerSelect_EventHandler(objCon_ContainerSelect_Event);
                //objCon.ContainerUnSelect_Event += new ObjectContainer.ContainerUnSelect_EventHandler(objCon_ContainerUnSelect_Event);
                DesignArea.Children.Add(objCon);

                objCon.SetValue(Canvas.LeftProperty, X1);
                objCon.SetValue(Canvas.TopProperty, Y1);
                objCon.SetValue(Canvas.ZIndexProperty, DisplayIndx);


                if (oObject != null && oObject.IsPositionLocked != null)
                    objCon.UpdateContainerLockPosition(oObject.IsPositionLocked.Value,false);
                else
                    objCon.UpdateContainerLockPosition(false,false);

                if (oObject != null)
                    objCon.UpdateContainerShowHide(oObject.IsHidden, false);
                else
                    objCon.UpdateContainerShowHide(false, false);

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddVectorLine::" + ex.ToString());
            }
        }

        public void AddVectorEllipse(string ObjectContainerName, string CName, int DisplayIndx, double X1, double Y1, ProductServiceReference.TemplateObjects oObject)
        {
            try
            {
                if (DisplayIndx < 0)
                {
                    DisplayIndx = GetMaxZidx();
                }
                else
                    DisplayIndx = CtlDisplayIdxSt + DisplayIndx;

                ObjectContainer objCon = new ObjectContainer(ObjectContainerName, X1, Y1, CName, oObject, true);
                objCon.MouseObjectMove_Event += new ObjectContainer.MouseObjectMove_EventHandler(ObjectMove);
                objCon.MouseObjectResizeRM_Event += new ObjectContainer.MouseObjectResizeRM_EventHandler(ObjectResize);
                objCon.MouseObjectResizeLM_Event += new ObjectContainer.MouseObjectResizeLM_EventHandler(ObjectResizeMove);
                objCon.MouseObjectRotate_Event += new ObjectContainer.MouseObjectRotate_EventHandler(ObjectRotate);
                objCon.ContainerSelect_Event += new ObjectContainer.ContainerSelect_EventHandler(objCon_ContainerSelect_Event);
                //objCon.ContainerUnSelect_Event += new ObjectContainer.ContainerUnSelect_EventHandler(objCon_ContainerUnSelect_Event);
                DesignArea.Children.Add(objCon);

                objCon.SetValue(Canvas.LeftProperty, X1);
                objCon.SetValue(Canvas.TopProperty, Y1);
                objCon.SetValue(Canvas.ZIndexProperty, DisplayIndx);


                if (oObject != null && oObject.IsPositionLocked != null)
                    objCon.UpdateContainerLockPosition(oObject.IsPositionLocked.Value,false);
                else
                    objCon.UpdateContainerLockPosition(false,false);

                if (oObject != null)
                    objCon.UpdateContainerShowHide(oObject.IsHidden, false);
                else
                    objCon.UpdateContainerShowHide(false, false);

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddVectorLine::" + ex.ToString());
            }
        }

        #endregion
        #region "Object Event"

        void objCon_ContainerLoadedChangeSize_Event(object sender, Point PosDiff)
        {
            ObjectContainer ObjCt = (ObjectContainer)sender;
            double CurtX, CurtY;
            CurtX = (double)ObjCt.GetValue(Canvas.LeftProperty) - PosDiff.X; //(e.X - (e.X * radians));
            CurtY = (double)ObjCt.GetValue(Canvas.TopProperty) - PosDiff.Y; //(e.Y - (e.Y * radians));
            ObjCt.SetValue(Canvas.LeftProperty, CurtX);
            ObjCt.SetValue(Canvas.TopProperty, CurtY);
        }

        void objCon_ObjectPosChange_Event(object sender, Point e)
        {

            ObjectContainer ObjCt = (ObjectContainer)sender;

            //Point CenterLoc = new Point(
            //       (Double)ObjCt.GetValue(Canvas.LeftProperty),
            //       (Double)ObjCt.GetValue(Canvas.TopProperty)
            //       );
            //CenterLoc.X += (ObjCt.ActualWidth/2);
            //CenterLoc.Y += (ObjCt.ActualHeight/2);

            //double radians = ObjCt.getAngle() * (Math.PI / 180);
            //double x = CenterLoc.X + ((ObjCt.ActualWidth / 2) * Math.Cos(radians));
            //double y = CenterLoc.Y + ((ObjCt.ActualHeight / 2) * Math.Sin(radians));

            if (ObjCt.getAngle() == 0)
            {
                double CurtX, CurtY;
                CurtX = (double)ObjCt.GetValue(Canvas.LeftProperty) - e.X; //(e.X - (e.X * radians));
                CurtY = (double)ObjCt.GetValue(Canvas.TopProperty) - e.Y; //(e.Y - (e.Y * radians));
                ObjCt.SetValue(Canvas.LeftProperty, CurtX);
                ObjCt.SetValue(Canvas.TopProperty, CurtY);
                ObjCt.UpdateLayout();
                //HorRuler.PointerVal = CurtX;
                //VerRuler.PointerVal = CurtY;
                //HorRuler.ShowPointer();
                //VerRuler.ShowPointer();
            }



            //double tn = Math.Atan2(e.Y, e.X);
            //App CurrentApp = (App)Application.Current;
            //Page ObjPage = (Page)CurrentApp.RootVisual;
            //ObjPage.hdrtxt.Text = ObjCt.GetValue(Canvas.LeftProperty).ToString() + "X" + ObjCt.GetValue(Canvas.TopProperty).ToString() + " s: " + (e.X*radians).ToString() + "X" + (e.Y*radians).ToString() + " x:" + x.ToString() + " y:" + y.ToString()+" tn:"+Math.Atan2(x,y).ToString() ;
        }
        public void TextObjBkChange(string ctlNm, Brush bg)
        {
            try
            {
                //MessageBox.Show(ctlNm);
                if (ctlNm!=null && ctlNm != "")
                {
                    object obj = objPageTxtControl.CtrlsList.FindName(ctlNm);
                    if (obj != null && obj.GetType().Name == "TextBox")
                    {
                        TextBox tb = (TextBox)obj;
                        tb.BorderBrush = bg;
                        tb.BorderThickness = new Thickness(3);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::TextObjBkChange::" + ex.ToString());
            }
        }

        //void objCon_ContainerUnSelect_Event(object sender, string ContainerName, int ContainerType, bool mode)
        //{
        //    try
        //    {
        //        if (ContainerType == 1 || ContainerType == 2 || ContainerType == 4)
        //        {
        //            ObjectContainer oc = (ObjectContainer)sender;
        //            //if (oc.SelectedObect != null && oc.SelectedObect.TCtlName != "")
        //            //    TextObjBkChange(oc.SelectedObect.TCtlName, new SolidColorBrush(Colors.White));

        //            this.UnSelectedContainerName = "x";
                 
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ShowException)
        //            MessageBox.Show("::objCon_ContainerUnSelect_Event::" + ex.ToString());
        //    }
        //}

        //selection actually happening.
        void objCon_ContainerSelect_Event(object sender, string ContainerName, ObjectContainer.ContainerTypes ContainerType, Point e, Size ContainerSize)
        {
            //SelObjCnt += 1;
            //EnableProButton(sender);
            try
            {

                ObjectContainer oc = (ObjectContainer)sender;
                //if (oc != null)
                //{
                //    //HorRuler.PointerVal = (double)oc.GetValue(Canvas.LeftProperty);
                //    //HorRuler.ShowPointer();
                //    //VerRuler.PointerVal = (double)oc.GetValue(Canvas.TopProperty);
                //    //VerRuler.ShowPointer();
                //}

               

                    if (oc.SelectedObect != null && oc.SelectedObect.TCtlName != "")
                    {
                        //TextObjBkChange(oc.SelectedObect.TCtlName, new SolidColorBrush(Colors.Green));

                        this.SelectedContainerName = ContainerName;

                        if (ObjectSelect_Event != null)
                            ObjectSelect_Event(this, true, oc.SelectedObect.TCtlName, true, e, ContainerSize);
                        //open the window


                    }
                    else //newly added image which does not have the base object created
                    {
                        this.SelectedContainerName = ContainerName;

                        if (ObjectSelect_Event != null)
                            ObjectSelect_Event(this, true, "", true, e, ContainerSize);
                    }
                

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::objCon_ContainerSelect_Event::" + ex.ToString());
            }
        }
        void ObjectMove(object sender, MouseEventArgs e)
        {
            try
            {
                ObjectContainer c = (ObjectContainer)sender;
                double X, Y;


                X = (e.GetPosition(DesignArea).X - c.MousePoiner.X);
                Y = (e.GetPosition(DesignArea).Y - c.MousePoiner.Y);

                if (IsSnapToGrid)
                {
                    //new code
                    int line1 = 0;
                    int line2 = 0;

                    line1 = SnapXPoints[0];
                    line2 = SnapXPoints[1];

                    int iCounter = 1;

                    while (iCounter < SnapXPoints.Count() - 1)
                    {

                        if (X > line1 && X < line2)
                        {
                            X = line1;
                            break;
                        }

                        iCounter++;
                        line1 = SnapXPoints[iCounter - 1];
                        line2 = SnapXPoints[iCounter];
                    }

                    line1 = 0;
                    line2 = 0;

                    line1 = SnapYPoints[0];
                    line2 = SnapYPoints[1];

                    iCounter = 1;

                    while (iCounter < SnapYPoints.Count() - 1)
                    {

                        if (Y > line1 && Y < line2)
                        {
                            Y = line1;
                            break;
                        }

                        iCounter++;
                        line1 = SnapYPoints[iCounter - 1];
                        line2 = SnapYPoints[iCounter];
                    }

                }



                c.SetValue(Canvas.LeftProperty, X);
                c.SetValue(Canvas.TopProperty, Y);
                //HorRuler.PointerVal = X;
                //VerRuler.PointerVal = Y;
                //HorRuler.ShowPointer();
                //VerRuler.ShowPointer();
                //hdrtxt.Text = " X :" + c.GetValue(Canvas.LeftProperty).ToString() + " Y :" + c.GetValue(Canvas.TopProperty).ToString();
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ObjectMove::" + ex.ToString());
            }
        }
        void ObjectResize(object sender, MouseEventArgs e, AspectResize ResizeAspect)
        {
            try
            {
                ObjectContainer c = (ObjectContainer)sender;
                double X, Y;
                X = (e.GetPosition(c).X - c.MousePoiner.X);
                Y = (e.GetPosition(c).Y - c.MousePoiner.Y);


                //snap to grid locking
                if (IsSnapToGrid)
                {
                    //new code
                    int line1 = 0;
                    int line2 = 0;

                    line1 = SnapXPoints[0];
                    line2 = SnapXPoints[1];

                    int iCounter = 1;

                    while (iCounter < SnapXPoints.Count() - 1)
                    {

                        if (X > line1 && X < line2)
                        {
                            X = line1;
                            break;
                        }

                        iCounter++;
                        line1 = SnapXPoints[iCounter - 1];
                        line2 = SnapXPoints[iCounter];
                    }

                    line1 = 0;
                    line2 = 0;

                    line1 = SnapYPoints[0];
                    line2 = SnapYPoints[1];

                    iCounter = 1;

                    while (iCounter < SnapYPoints.Count() - 1)
                    {

                        if (Y > line1 && Y < line2)
                        {
                            Y = line1;
                            break;
                        }

                        iCounter++;
                        line1 = SnapYPoints[iCounter - 1];
                        line2 = SnapYPoints[iCounter];
                    }

                }


                if (c.ContainerType == ObjectContainer.ContainerTypes.Image || c.ContainerType == ObjectContainer.ContainerTypes.LogoImage)
                {
                    c.UpdateContainerImgSize(X, Y, ResizeAspect);
                }
                else if (c.ContainerType == ObjectContainer.ContainerTypes.LineVector)
                {
                    if (Y + c.OriginalSize.Height > 2)
                    {

                        Size NewSize = new Size(X + c.OriginalSize.Width, Y + c.OriginalSize.Height);
                        c.UpdateContainerVectorLineSize(NewSize);
                    }
                }
                else if (c.ContainerType == ObjectContainer.ContainerTypes.RectangleVector)
                {
                    if (Y + c.OriginalSize.Height > 5 && X + c.OriginalSize.Width > 5)
                    {

                        //Size NewSize = new Size(X + c.OriginalSize.Width, Y + c.OriginalSize.Height);
                        c.UpdateContainerVectorRectSize(X, Y, ResizeAspect);
                    }
                }
                else if (c.ContainerType == ObjectContainer.ContainerTypes.EllipseVector)
                {
                    if (Y + c.OriginalSize.Height > 5 && X + c.OriginalSize.Width > 5)
                    {

                        //Size NewSize = new Size(X + c.OriginalSize.Width, Y + c.OriginalSize.Height);
                        c.UpdateContainerVectorEllipseSize(X, Y, ResizeAspect);
                    }
                }
                else
                {
                    if (X + c.OriginalSize.Width > 32 && webprintDesigner.Common.PixelToPoint(Y + c.OriginalSize.Height) >= c.SelectedObect.FontSize)
                    {
                        Size NewSize = new Size();
                        if (ResizeAspect == AspectResize.Horizontal)
                        {
                            NewSize = new Size(X + c.OriginalSize.Width, c.OriginalSize.Height);
                        }
                        else if (ResizeAspect == AspectResize.Vertical)
                        {
                            NewSize = new Size(c.OriginalSize.Width, Y + c.OriginalSize.Height);
                        }
                        else if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift || (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                        {
                            double Xfac = 0;
                            double Yfac = 0;

                            if (c.OriginalSize.Width > c.OriginalSize.Height)
                            {
                                Xfac = X;
                                Yfac = X / ((c.OriginalSize.Width) / c.OriginalSize.Height);

                            }
                            else
                            {
                                Xfac = Y / ((c.OriginalSize.Height) / c.OriginalSize.Width);
                                Yfac = Y;
                            }

                            NewSize = new Size(Xfac + c.OriginalSize.Width, Yfac + c.OriginalSize.Height);
                        }
                        else
                        {
                            NewSize = new Size(X + c.OriginalSize.Width, Y + c.OriginalSize.Height);
                        }



                        //NewSize = new Size(X + c.OriginalSize.Width, Y + c.OriginalSize.Height);
                        c.UpdateContainerSize(NewSize);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ObjectResize::" + ex.ToString());
            }
        }

        void ObjectResizeMove(object sender, MouseEventArgs e)
        {
            try
            {
                ObjectContainer c = (ObjectContainer)sender;
                double X, X2;
                X = (c.MousePoiner.X - e.GetPosition(DesignArea).X);
                X2 = (e.GetPosition(DesignArea).X - c.MousePoiner.X);
                if (c.OriginalSize.Width - X2 > 32)
                {


                    Size NewSize = new Size(c.OriginalSize.Width - X2, c.OriginalSize.Height);
                    c.UpdateContainerSize(NewSize);
                    c.SetValue(Canvas.LeftProperty, X2);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ObjectResizeMove::" + ex.ToString());
            }
        }
        void ObjectRotate(object sender, MouseEventArgs e)
        {
            try
            {
                ObjectContainer c = (ObjectContainer)sender;
                Point MouseLoc = e.GetPosition(DesignArea);
                Size Dimension = c.OriginalSize;
                Dimension.Width /= 2;
                Dimension.Height /= 2;
                // Dimension.Width += 16 + 1; // adjust size for left hand control, make sense
                // Dimension.Height += 16 + 1;  // no idea why this is needed but it works </shrug>
                Point CenterLoc = new Point(
                    (Double)c.GetValue(Canvas.LeftProperty),
                    (Double)c.GetValue(Canvas.TopProperty)
                    );
                CenterLoc.X += Dimension.Width;
                CenterLoc.Y += Dimension.Height;
                Double Radians = Math.Atan2(MouseLoc.X - CenterLoc.X, MouseLoc.Y - CenterLoc.Y);
                Double NewAngle = Radians * (180 / Math.PI);
                c.UpdateRotation(NewAngle);
                //cmbRotate.SelectedIndex = -1;

                //App CurrentApp = (App)Application.Current;
                //Page ObjPage = (Page)CurrentApp.RootVisual;
                //ObjPage.hdrtxt.Text += " " + Radians.ToString() + " " + NewAngle.ToString();
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ObjectRotate::" + ex.ToString());
            }
        }
        void HorBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        void VerBar_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                ScrollBar ScrlBar = sender as ScrollBar;
                //VerRuler.RulerScrollOffset = ScrlBar.Value;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::VerBar_Scroll::" + ex.ToString());
            }
        }
        void HorBar_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                ScrollBar ScrlBar = sender as ScrollBar;
                //HorRuler.RulerScrollOffset = ScrlBar.Value;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::HorBar_Scroll::" + ex.ToString());
            }
        }
        #endregion
        #region "Object Function"
        private ObjectContainer FindControl(string CtlName)
        {
            ObjectContainer oc = null;
            try
            {

                foreach (UIElement el in DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        if (((ObjectContainer)el).ContainerName != "" && ((ObjectContainer)el).ContainerName == CtlName)
                            oc = (ObjectContainer)el;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::FindControl::" + ex.ToString());
            }
            return oc;

        }
        public int GetMaxZidx()
        {
            int RetVal = 100;
            try
            {

                foreach (UIElement el in DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer oc = (ObjectContainer)el;
                        if (oc.GetValue(Canvas.ZIndexProperty) != null)
                        {
                            int ZIdx = (int)oc.GetValue(Canvas.ZIndexProperty);
                            if (ZIdx > RetVal)
                                RetVal = ZIdx;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::GetMaxZidx::" + ex.ToString());
            }
            return RetVal + 1;
        }
        public void UnSelAllObject()
        {
            try
            {
                foreach (UIElement el in DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer oc = (ObjectContainer)el;
                        if (oc.Selected && !oc.MouseDown)
                        {
                            oc.UnSelectContainer(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::UnSelAllObject::" + ex.ToString());
            }
        }
        private void UnSelObject()
        {
            try
            {
                string unsel = string.Empty;
                if ((Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.Shift)
                {
                    bool isUnSel = false;
                    
                    foreach (UIElement el in DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            ObjectContainer oc = (ObjectContainer)el;
                            if (oc.Selected && !oc.MouseDown)
                            {
                                oc.UnSelectContainer(true);
                                unsel = oc.ContainerName;
                                isUnSel = true;

                            }
                          
                        }
                    }

                    
                    
                    //ObjPage.SelEnbProButton(DesignArea.Children);
                    if (isUnSel)
                    {
                        if (this.SelectedContainerName == unsel)
                        {

                            ParentPage.HideEditorWindow();
                            //SelObjCnt = 0;
                            //DisablProButton();
                            this.SelectedContainerName = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::UnSelObject::" + ex.ToString());
            }
        }

        public void RemoveZIdx(int oZIdx)
        {
            try
            {
                foreach (UIElement el in DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer oc = (ObjectContainer)el;
                        if (!oc.Selected)
                        {
                            int tZIdx = (int)oc.GetValue(Canvas.ZIndexProperty);
                            if (tZIdx > oZIdx)
                            {
                                oc.SetValue(Canvas.ZIndexProperty, tZIdx - 1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::RemoveZIdx::" + ex.ToString());
            }

        }
        #endregion

        /// <summary>
        /// Toggles visibility of all Guide elements
        /// </summary>
        /// <param name="Value"></param>
        public void ToggleGuides(bool Value)
        {
            Visibility vValue = (Value == true) ? Visibility.Visible : Visibility.Collapsed;


            //TrimArea.Visibility = vValue;
            SafeArea.Visibility = vValue;

            leftTopArrow.Visibility = vValue;
            leftBottomArrow.Visibility = vValue;
            TopLeftArrow.Visibility = vValue;
            TopRightArrow.Visibility = vValue;
            lblHeight.Visibility = vValue;
            lblWidth.Visibility = vValue;


            oSafeGuideText1.Visibility = vValue;
            oSafeGuideText2.Visibility = vValue;
            oSafeGuideText3.Visibility = vValue;
            oSafeGuideText4.Visibility = vValue;
            oSafeGuideText5.Visibility = vValue;
            oSafeGuideText6.Visibility = vValue;
            oSafeGuideText7.Visibility = vValue;
            oSafeGuideText8.Visibility = vValue;


            foreach (var item in DesignArea.Children)
            {
                if (item.GetType().Name == "Line")
                {
                    item.Visibility = vValue;
                }
                else if (item.GetType().Name == "TextBlock" && ((TextBlock)item).Text == "FOLDING AREA")
                {
                    item.Visibility = vValue;
                }
            }


        }

        private void PageRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UnSelObject();
               
                ParentPage.SelEnbProButton(DesignArea.Children);
                DesignFocus.Focus();

            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::DesignArea_MouseLeftButtonDown::" + ex.ToString());
            }

            e.Handled = true;
        }
        private void PageRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool isSel = false;
                foreach (UIElement el in DesignArea.Children)
                {
                    if (el.GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer oc = (ObjectContainer)el;
                        if (oc.Selected && !oc.MouseDown)
                        {
                            isSel = true;
                        }
                    }
                }
                if (!isSel)
                {
                    //HorRuler.HidePointer();
                    //VerRuler.HidePointer();
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::stpDesign_MouseLeftButtonUp::" + ex.ToString());
            }
        }
        private void PageRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                //HtmlPage.Window.Alert("sizechanged");
                //hdrtxt.Text = "sizechanged : " + stpDesign.ColumnDefinitions[1].ActualWidth.ToString() + " : " + stpDesign.RowDefinitions[1].ActualHeight.ToString();
                scvDesign.Width = PageRoot.ActualWidth;
                scvDesign.Height = PageRoot.ActualHeight;
                //HorRuler.Width = scvDesign.Width - 18;
                //VerRuler.Height = scvDesign.Height - 18;
                //scvDesign.Height = stpDesign.ActualHeight;
                //scvDesign.Width = stpDesign.ActualWidth;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::Grid_SizeChanged::" + ex.ToString());
            }
        }
        private void PageRoot_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                bool isHitObject = false;
                //foreach (UIElement el in DesignArea.Children)
                //{
                for (int idx = DesignArea.Children.Count - 1; idx >= 0; idx--)
                {

                    if (DesignArea.Children[idx].GetType().Name == "ObjectContainer")
                    {
                        ObjectContainer oc = (ObjectContainer)DesignArea.Children[idx];
                        if (oc.Selected && !oc.MouseDown)
                        {

                            if (e.Key != Key.Unknown)
                            {
                                //if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                                //{
                                //    //hdrtxt.Text = (el as Grid).Width.ToString();
                                //    Size NewSize = new Size(oc.Width, oc.Height);
                                //    if (e.Key == Key.Left)
                                //    {
                                //        if (oc.OriginalSize.Width > 32)
                                //            oc.OriginalSize.Width = oc.OriginalSize.Width - 5;
                                //        oc.UpdateContainerSize(oc.OriginalSize);
                                //        isHitObject = true;
                                //    }
                                //    else if (e.Key == Key.Up)
                                //    {
                                //        if (oc.OriginalSize.Height > 16)
                                //            oc.OriginalSize.Height = oc.OriginalSize.Height - 5;
                                //        oc.UpdateContainerSize(oc.OriginalSize);
                                //        isHitObject = true;
                                //    }
                                //    else if (e.Key == Key.Right)
                                //    {
                                //        oc.OriginalSize.Width = oc.OriginalSize.Width + 5;
                                //        oc.UpdateContainerSize(oc.OriginalSize);
                                //        isHitObject = true;
                                //    }
                                //    else if (e.Key == Key.Down)
                                //    {
                                //        oc.OriginalSize.Height = oc.OriginalSize.Height + 5;
                                //        oc.UpdateContainerSize(oc.OriginalSize);
                                //        isHitObject = true;
                                //    }
                                //}
                                //else
                                //{
                                    double Lf = (double)oc.GetValue(Canvas.LeftProperty);
                                    double Tp = (double)oc.GetValue(Canvas.TopProperty);
                                    if (e.Key == Key.Left)
                                    {
                                        if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                                            oc.SetValue(Canvas.LeftProperty, Lf - 1);
                                        else
                                            oc.SetValue(Canvas.LeftProperty, Lf - 5);
                                        //HorRuler.PointerVal = (double)oc.GetValue(Canvas.LeftProperty);
                                        //HorRuler.ShowPointer();
                                        isHitObject = true;
                                    }
                                    else if (e.Key == Key.Up)
                                    {
                                        if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                                            oc.SetValue(Canvas.TopProperty, Tp - 1);
                                        else
                                            oc.SetValue(Canvas.TopProperty, Tp - 5);
                                        //VerRuler.PointerVal = (double)oc.GetValue(Canvas.TopProperty);
                                        //VerRuler.ShowPointer();
                                        isHitObject = true;
                                    }
                                    else if (e.Key == Key.Right)
                                    {
                                        if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                                            oc.SetValue(Canvas.LeftProperty, Lf + 1);
                                        else
                                            oc.SetValue(Canvas.LeftProperty, Lf + 5);
                                        //HorRuler.PointerVal = (double)oc.GetValue(Canvas.LeftProperty);
                                        //HorRuler.ShowPointer();
                                        isHitObject = true;
                                    }
                                    else if (e.Key == Key.Down)
                                    {
                                        if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                                            oc.SetValue(Canvas.TopProperty, Tp + 1);
                                        else
                                            oc.SetValue(Canvas.TopProperty, Tp + 5);
                                        //VerRuler.PointerVal = (double)oc.GetValue(Canvas.TopProperty);
                                        //VerRuler.ShowPointer();
                                        isHitObject = true;
                                    }
                                    else if (e.Key == Key.Delete || e.Key == Key.Back)
                                    {
                                        if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                                        {
                                            RemoveZIdx((int)oc.GetValue(Canvas.ZIndexProperty));
                                            foreach (ProductServiceReference.TemplateObjects objObject in oc.lstProductObects)
                                            {
                                                objPageTxtControl.RemoveObject(objObject.TCtlName);
                                            }
                                            DesignArea.Children.RemoveAt(idx);

                                            ParentPage.HideEditorWindow();
                                        }
                                        else
                                        {
                                            if (oc.lstProductObects == null)
                                            {
                                                RemoveZIdx((int)oc.GetValue(Canvas.ZIndexProperty));
                                                DesignArea.Children.RemoveAt(idx);
                                                ParentPage.HideEditorWindow();
                                            }
                                            else if (oc.lstProductObects.Count == 1)
                                            {
                                                RemoveZIdx((int)oc.GetValue(Canvas.ZIndexProperty));
                                                objPageTxtControl.RemoveObject(oc.lstProductObects[0].TCtlName);
                                                DesignArea.Children.RemoveAt(idx);
                                                ParentPage.HideEditorWindow();
                                            }
                                            else
                                            {
                                                if (oc.SelContainerPanel!=null && oc.getContainerContent != null && oc.SelectedObect != null)
                                                {
                                                    if (oc.getContainerContent.Name == "Tb" + oc.SelectedObect.TCtlName)
                                                    {
                                                        objPageTxtControl.RemoveObject(oc.SelectedObect.TCtlName);
                                                        oc.SelContainerPanel.Children.Remove((UIElement)oc.getContainerContent.Parent);
                                                        oc.lstProductObects.Remove(oc.SelectedObect);
                                                        oc.UnSelectContainer(true);
                                                        ParentPage.HideEditorWindow();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                //}
                            }

                            //oc.UnSelectContainer();
                        }
                    }
                }
                
                // e.Handled = !isHitObject;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::UserControl_KeyDown::" + ex.ToString());
            }
        }
        private void brdDesign_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                //HorRuler.RulerLength = DesignArea.ActualWidth;
                //VerRuler.RulerLength = DesignArea.ActualHeight;
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::brdDesign_SizeChanged::" + ex.ToString());
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ScrollBar VerBar = ((FrameworkElement)VisualTreeHelper.GetChild(scvDesign, 0)).FindName("VerticalScrollBar") as System.Windows.Controls.Primitives.ScrollBar;
            //VerBar.Scroll += new ScrollEventHandler(VerBar_Scroll);
            //ScrollBar HorBar = ((FrameworkElement)VisualTreeHelper.GetChild(scvDesign, 0)).FindName("HorizontalScrollBar") as System.Windows.Controls.Primitives.ScrollBar;
            //HorBar.Scroll += new ScrollEventHandler(HorBar_Scroll);
            //HorBar.MouseLeftButtonDown += new MouseButtonEventHandler(HorBar_MouseLeftButtonDown);

            
        }
        

        void scvDesign_MouseMove(object sender, MouseEventArgs e)
        {
            pt = e.GetPosition(brdDesign);
            //pt.X /= 490;
            //pt.Y /= 490;
            ////LightAngle.GradientOrigin = pt;
            //pt.X -= (pt.X * 2);
            //pt.X += 1;
            //pt.X *= 1000;
            //pt.X -= 500;
            //ShadowAngle.LocalOffsetX = pt.X;
            //pt.Y -= (pt.Y * 2);
            //pt.Y += 1;
            //pt.Y *= 400;
            //pt.Y -= 450;
            //ShadowAngle.LocalOffsetY = pt.Y;
        }

    }
}
