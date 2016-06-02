using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PrintFlow.SilverlightControls
{
    [TemplateVisualState(Name = "HorizontalState", GroupName = "RulerOrientationStates")]
    [TemplateVisualState(Name = "VerticalState", GroupName = "RulerOrientationStates")]
    [TemplatePart(Name = "RulerPanel", Type = typeof(Canvas))]
    public class Ruler : ContentControl
    {
        #region Fields
        private Canvas spRulerPanel;
        private Border brRulerPanel;
        private ScrollViewer svRulerPanel;
        private Rectangle Pointer;
        private double DPIVal = 96;
        public double PointerVal = 0;
        #endregion
        #region Dependency Properties
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Ruler), null);
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty RulerPaddingProperty = DependencyProperty.Register("RulerPadding", typeof(Thickness), typeof(Ruler), new PropertyMetadata(new Thickness(0)));
        public Thickness RulerPadding
        {
            get { return (Thickness)GetValue(RulerPaddingProperty); }
            set { SetValue(RulerPaddingProperty, value); }
        }
        public static readonly DependencyProperty RulerOrientationProperty = DependencyProperty.Register("RulerOrientation", typeof(Orientation), typeof(Ruler), null);
        public Orientation RulerOrientation
        {
            get { return (Orientation)GetValue(RulerOrientationProperty); }
            set { SetValue(RulerOrientationProperty, value); }
        }
        public static readonly DependencyProperty RulerLengthProperty = DependencyProperty.Register("RulerLength", typeof(double), typeof(Ruler), new PropertyMetadata(100d, RulerPropertyChanged));
        public double RulerLength
        {
            get { return (double)GetValue(RulerLengthProperty); }
            set { SetValue(RulerLengthProperty, value); }
        }
        public static readonly DependencyProperty RulerDPIProperty = DependencyProperty.Register("RulerDPI", typeof(double), typeof(Ruler), null);
        public double RulerDPI
        {
            get { return (double)GetValue(RulerDPIProperty); }
            set { SetValue(RulerDPIProperty, value); }
        }

        public static readonly DependencyProperty RulerHorizontalAlignmentProperty = DependencyProperty.Register("RulerHorizontalAlignment", typeof(HorizontalAlignment), typeof(Ruler), null);
        public HorizontalAlignment RulerHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(RulerHorizontalAlignmentProperty); }
            set { SetValue(RulerHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty RulerVerticalAlignmentProperty = DependencyProperty.Register("RulerVerticalAlignment", typeof(VerticalAlignment), typeof(Ruler), null);
        public VerticalAlignment RulerVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(RulerVerticalAlignmentProperty); }
            set { SetValue(RulerVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty RulerColorProperty = DependencyProperty.Register("RulerColor", typeof(Brush), typeof(Ruler), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        public Brush RulerColor
        {
            get { return (Brush)GetValue(RulerColorProperty); }
            set { SetValue(RulerColorProperty, value); }
        }
        public static readonly DependencyProperty RulerBackgroundProperty = DependencyProperty.Register("RulerBackground", typeof(Brush), typeof(Ruler), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        public Brush RulerBackground
        {
            get { return (Brush)GetValue(RulerBackgroundProperty); }
            set { SetValue(RulerBackgroundProperty, value); }
        }
        /// <summary>
        /// 0 : Pixel
        /// 1 :Inch
        /// 2 : Cm
        /// </summary>
        public static readonly DependencyProperty ScaleModeProperty = DependencyProperty.Register("ScaleMode", typeof(int), typeof(Ruler), new PropertyMetadata(0));
        public int ScaleMode
        {
            get { return (int)GetValue(ScaleModeProperty); }
            set { SetValue(ScaleModeProperty, value); }
        }
        public static readonly DependencyProperty DivisionProperty = DependencyProperty.Register("Division", typeof(int), typeof(Ruler), new PropertyMetadata(6));
        public int Division
        {
            get { return (int)GetValue(DivisionProperty); }
            set { SetValue(DivisionProperty, value); }
        }
        public static readonly DependencyProperty MajorIntervalProperty = DependencyProperty.Register("MajorInterval", typeof(double), typeof(Ruler), new PropertyMetadata(40d));
        public double MajorInterval
        {
            get { return (double)GetValue(MajorIntervalProperty); }
            set { SetValue(MajorIntervalProperty, value); }
        }

        public static readonly DependencyProperty DivisionMarkLengthProperty = DependencyProperty.Register("DivisionMarkLength", typeof(double), typeof(Ruler), new PropertyMetadata(4d));
        public double DivisionMarkLength
        {
            get { return (double)GetValue(DivisionMarkLengthProperty); }
            set { SetValue(DivisionMarkLengthProperty, value); }
        }
        public static readonly DependencyProperty MiddleMarkLengthProperty = DependencyProperty.Register("MiddleMarkLength", typeof(double), typeof(Ruler), new PropertyMetadata(6d));
        public double MiddleMarkLength
        {
            get { return (double)GetValue(MiddleMarkLengthProperty); }
            set { SetValue(MiddleMarkLengthProperty, value); }
        }
        public static readonly DependencyProperty MajorMarkLengthProperty = DependencyProperty.Register("MajorMarkLength", typeof(double), typeof(Ruler), new PropertyMetadata(10d));
        public double MajorMarkLength
        {
            get { return (double)GetValue(MajorMarkLengthProperty); }
            set { SetValue(MajorMarkLengthProperty, value); }
        }
        public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register("ZoomFactor", typeof(double), typeof(Ruler), new PropertyMetadata(1d, RulerPropertyChanged));
        public double ZoomFactor
        {
            get { return (double)GetValue(ZoomFactorProperty); }
            set { SetValue(ZoomFactorProperty, value); }
        }
        public static readonly DependencyProperty RulerScrollOffsetProperty = DependencyProperty.Register("RulerScrollOffset", typeof(double), typeof(Ruler), new PropertyMetadata(0d, RulerScrollPropertyChanged));
        public double RulerScrollOffset
        {
            get { return (double)GetValue(RulerScrollOffsetProperty); }
            set { SetValue(RulerScrollOffsetProperty, value); }
        }
        #endregion
        public Ruler()
        {
            DefaultStyleKey = typeof(Ruler);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            spRulerPanel = GetTemplateChild("RulerPanel") as Canvas;
            brRulerPanel = GetTemplateChild("brdRulerPanel") as Border;
            Pointer = GetTemplateChild("recPointer") as Rectangle;
            if (spRulerPanel != null && RulerLength != 0)
            {
                ChangeVisualState(false);
            }
            svRulerPanel = GetTemplateChild("scvRulerPanel") as ScrollViewer;
            RulerScroll();
            
        }
        public void ShowPointer()
        {
            if (Pointer != null)
            {
                if (Pointer.Visibility == Visibility.Collapsed)
                    Pointer.Visibility = Visibility.Visible;
                if (RulerOrientation == Orientation.Vertical)
                {
                    Pointer.SetValue(Canvas.TopProperty, PointerVal * ZoomFactor);
                    Pointer.SetValue(Canvas.LeftProperty, 0d);
                }
                else
                {
                    Pointer.SetValue(Canvas.TopProperty, 0d);
                    Pointer.SetValue(Canvas.LeftProperty, PointerVal * ZoomFactor);
                }
            }
        }
        public void HidePointer()
        {
            if (Pointer != null)
                Pointer.Visibility = Visibility.Collapsed;
        }
        private void RulerScroll()
        {
            if (svRulerPanel != null)
            {
                if (RulerOrientation == Orientation.Vertical)
                {
                    svRulerPanel.ScrollToVerticalOffset(RulerScrollOffset);
                }
                else
                {
                    svRulerPanel.ScrollToHorizontalOffset(RulerScrollOffset);
                }
            }
        }
        static void RulerScrollPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Ruler ddp = sender as Ruler;
            ddp.RulerScroll();
        }
        static void RulerPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Ruler ddp = sender as Ruler;
            ddp.ChangeVisualState(true);
        }
        private void ChangeVisualState(bool useTransitions)
        {
            getDPI();
            if (RulerOrientation == Orientation.Vertical)
            {
                VisualStateManager.GoToState(this, "VerticalState", useTransitions);
                spRulerPanel.VerticalAlignment = RulerVerticalAlignment;
                if (brRulerPanel != null)
                {
                    brRulerPanel.VerticalAlignment = RulerVerticalAlignment;
                    if (RulerHorizontalAlignment == HorizontalAlignment.Left)
                    {
                        brRulerPanel.BorderThickness = new Thickness(1d, 0d, 0d, 0d);
                    }
                    else if (RulerHorizontalAlignment == HorizontalAlignment.Right)
                    {
                        brRulerPanel.BorderThickness = new Thickness(0d, 0d, 1d, 0d);
                    }
                    else
                        brRulerPanel.BorderThickness = new Thickness(0d, 0d, 0d, 0d);

                }
                if (Pointer != null)
                {
                    Pointer.Height = 1d;
                    Pointer.Width = this.Width;
                }
                GenrateVScale();
            }
            else
            {
                VisualStateManager.GoToState(this, "HorizontalState", useTransitions);
                spRulerPanel.HorizontalAlignment = RulerHorizontalAlignment;
                if (brRulerPanel != null)
                {
                    brRulerPanel.HorizontalAlignment = RulerHorizontalAlignment;
                    if (RulerVerticalAlignment == VerticalAlignment.Top)
                    {
                        brRulerPanel.BorderThickness = new Thickness(0d, 1d, 0d, 0d);
                    }
                    else if (RulerVerticalAlignment == VerticalAlignment.Bottom)
                    {
                        brRulerPanel.BorderThickness = new Thickness(0d, 0d, 0d, 1d);
                    }
                    else
                        brRulerPanel.BorderThickness = new Thickness(0d, 0d, 0d, 0d);

                }
                if (Pointer != null)
                {
                    Pointer.Height = this.Height;
                    Pointer.Width = 1d;
                }
                GenrateHScale();
            }
        }
        private void getDPI()
        {
            if (RulerDPI != 0)
            {
                DPIVal = RulerDPI;
            }
            else
                DPIVal = 96;
        }
        private void ClearScale()
        {
            if (spRulerPanel != null)
            {
                for (int i = spRulerPanel.Children.Count - 1; i >= 0; i--)
                {
                    if (spRulerPanel.Children[i].GetType().Name == "Rectangle")
                    {
                        if (((Rectangle)spRulerPanel.Children[i]).Name != "recPointer")
                        {
                            spRulerPanel.Children.RemoveAt(i);
                        }
                    }
                    else if (spRulerPanel.Children[i].GetType().Name == "TextBlock")
                    {
                        spRulerPanel.Children.RemoveAt(i);
                    }
                }
            }
        }
        private void GenrateHScale()
        {
            if (spRulerPanel != null && MajorInterval>0)
            {
                ClearScale();
                spRulerPanel.Height =this.Height-1;
                spRulerPanel.Width = RulerLength * ZoomFactor;
                double Fct = Division;
                int SDivs = 0;
                int MDivs = 0;
                if (Division > 1)
                {
                    SDivs = Division;
                    if ((Division % 2) == 0)
                    {
                        MDivs = Convert.ToInt16(Division / 2);
                    }
                }
                double MInterval=  MajorInterval*ZoomFactor;
                for (double CPoint = 0; CPoint <= RulerLength; CPoint += MajorInterval)
                {
                    Rectangle SclLine = new Rectangle();
                    SclLine.Fill = RulerColor;
                    SclLine.StrokeThickness = 0;
                    SclLine.Width = 1;
                    SclLine.Height = MajorMarkLength;
                    TextBlock lblNo = new TextBlock();
                    lblNo.Text = CPoint.ToString();
                    lblNo.FontFamily = new FontFamily("Arail");
                    lblNo.FontSize = 7;

                    spRulerPanel.Children.Add(SclLine);
                    spRulerPanel.Children.Add(lblNo);
                    SclLine.SetValue(Canvas.LeftProperty, CPoint * ZoomFactor);
                    double lblMrg = 0d;
                    if (lblNo.Text.Length == 2)
                        lblMrg = 3;
                    else if (lblNo.Text.Length == 3)
                        lblMrg = 6;
                    else if (lblNo.Text.Length == 4)
                        lblMrg = 9;
                    else if (lblNo.Text.Length > 4)
                        lblMrg = 12;
                    lblNo.SetValue(Canvas.LeftProperty, CPoint * ZoomFactor - lblMrg);
                    if (RulerVerticalAlignment == VerticalAlignment.Top)
                    {
                        SclLine.SetValue(Canvas.TopProperty, 0d);
                        lblNo.SetValue(Canvas.TopProperty,Convert.ToDouble( MajorMarkLength));
                    }
                    else if (RulerVerticalAlignment == VerticalAlignment.Bottom)
                    {
                        if ((spRulerPanel.Height - MajorMarkLength) > 0)
                        {
                            SclLine.SetValue(Canvas.TopProperty, spRulerPanel.Height - MajorMarkLength);
                            lblNo.SetValue(Canvas.TopProperty, spRulerPanel.Height - MajorMarkLength-10);
                        }
                        else
                        {
                            SclLine.SetValue(Canvas.TopProperty, 0d);
                            lblNo.SetValue(Canvas.TopProperty, Convert.ToDouble(MajorMarkLength / 2));
                        }
                    }
                    else if (RulerVerticalAlignment == VerticalAlignment.Center)
                    {
                        if (((spRulerPanel.Height / 2) - (MajorMarkLength / 2)) > 0)
                        {
                            SclLine.SetValue(Canvas.TopProperty, (spRulerPanel.Height / 2) - (MajorMarkLength / 2));
                            lblNo.SetValue(Canvas.TopProperty, (spRulerPanel.Height / 2) - 5);
                        }
                        else
                        {
                            SclLine.SetValue(Canvas.TopProperty, 0d);
                            lblNo.SetValue(Canvas.TopProperty, MajorMarkLength);
                        }
                    }
                    if (SDivs > 1) //&& ((CPoint + MajorInterval) <= RulerLength)
                    {
                        double sd = MajorInterval / SDivs;
                        int cnt = 1;
                        for (double spoint = CPoint + sd; spoint < CPoint + MajorInterval ; spoint += sd)
                        {
                            if (spoint <= RulerLength)
                            {
                                Rectangle DSclLine = new Rectangle();
                                DSclLine.Fill = RulerColor;
                                DSclLine.StrokeThickness = 0;
                                DSclLine.Width = 1;
                                if (MDivs != 0 && MDivs == cnt)
                                    DSclLine.Height = MiddleMarkLength;
                                else
                                    DSclLine.Height = DivisionMarkLength;
                                spRulerPanel.Children.Add(DSclLine);
                                DSclLine.SetValue(Canvas.LeftProperty, spoint * ZoomFactor);
                                if (RulerVerticalAlignment == VerticalAlignment.Top)
                                {
                                    DSclLine.SetValue(Canvas.TopProperty, 0d);
                                }
                                else if (RulerVerticalAlignment == VerticalAlignment.Bottom)
                                {
                                    if (MDivs != 0 && MDivs == cnt)
                                        DSclLine.SetValue(Canvas.TopProperty, spRulerPanel.Height - MiddleMarkLength);
                                    else
                                        DSclLine.SetValue(Canvas.TopProperty, spRulerPanel.Height - DivisionMarkLength);
                                }
                                else if (RulerVerticalAlignment == VerticalAlignment.Center)
                                {
                                    if (MDivs != 0 && MDivs == cnt)
                                        DSclLine.SetValue(Canvas.TopProperty, (spRulerPanel.Height / 2) - (MiddleMarkLength / 2));
                                    else
                                        DSclLine.SetValue(Canvas.TopProperty, (spRulerPanel.Height / 2) - (DivisionMarkLength / 2));
                                }
                                cnt++;
                            }
                        }
                    }
                }
            }

        }
        private void GenrateVScale()
        {
            if (spRulerPanel != null && MajorInterval > 0)
            {
                ClearScale();
                spRulerPanel.Width = this.Width - 1; ;
                brRulerPanel.Width = spRulerPanel.Width;
                spRulerPanel.Height = RulerLength * ZoomFactor;
                double Fct = Division;
                int SDivs = 0;
                int MDivs = 0;
                if (Division > 1)
                {
                    SDivs = Division;
                    if ((Division % 2) == 0)
                    {
                        MDivs = Convert.ToInt16(Division / 2);
                    }
                }
                double MInterval = MajorInterval * ZoomFactor;
                for (double CPoint = 0; CPoint <= RulerLength; CPoint += MajorInterval)
                {
                    Rectangle SclLine = new Rectangle();
                    SclLine.Fill = RulerColor;
                    SclLine.StrokeThickness = 0;
                    SclLine.Width = MajorMarkLength;
                    SclLine.Height =1 ;
                    TextBlock lblNo = new TextBlock();
                    lblNo.Text = CPoint.ToString();
                    lblNo.FontFamily = new FontFamily("Arail");
                    lblNo.FontSize = 7;

                    spRulerPanel.Children.Add(SclLine);
                    spRulerPanel.Children.Add(lblNo);
                    SclLine.SetValue(Canvas.TopProperty, CPoint * ZoomFactor);
                    double lblMrg = 5d;
                    lblNo.SetValue(Canvas.TopProperty, CPoint * ZoomFactor - lblMrg);
                    if (RulerHorizontalAlignment == HorizontalAlignment.Left)
                    {
                        
                        SclLine.SetValue(Canvas.LeftProperty, 0d);
                        lblNo.SetValue(Canvas.LeftProperty, Convert.ToDouble(MajorMarkLength + 1));
                    }
                    else if (RulerHorizontalAlignment == HorizontalAlignment.Right)
                    {
                        lblMrg = 5d;
                        if (lblNo.Text.Length == 2)
                            lblMrg = 10;
                        else if (lblNo.Text.Length == 3)
                            lblMrg = 15;
                        else if (lblNo.Text.Length == 4)
                            lblMrg = 20;
                        else if (lblNo.Text.Length > 4)
                            lblMrg = 25;
                        SclLine.SetValue(Canvas.LeftProperty, spRulerPanel.Width - MajorMarkLength);
                        lblNo.SetValue(Canvas.LeftProperty, spRulerPanel.Width - MajorMarkLength-lblMrg);
                    }
                    else if (RulerHorizontalAlignment == HorizontalAlignment.Center)
                    {
                        lblMrg = 5d;
                        if (lblNo.Text.Length == 2)
                            lblMrg = 10;
                        else if (lblNo.Text.Length == 3)
                            lblMrg = 15;
                        else if (lblNo.Text.Length == 4)
                            lblMrg = 20;
                        else if (lblNo.Text.Length > 4)
                            lblMrg = 25;
                        SclLine.SetValue(Canvas.LeftProperty, (spRulerPanel.Width/2) - (MajorMarkLength/2));
                        lblNo.SetValue(Canvas.LeftProperty, (spRulerPanel.Width / 2) - (lblMrg / 2));
                    }
                    if (SDivs > 1) //&& ((CPoint + MajorInterval) <= RulerLength)
                    {
                        double sd = MajorInterval / SDivs;
                        int cnt = 1;
                        for (double spoint = CPoint + sd; spoint < CPoint + MajorInterval; spoint += sd)
                        {
                            if (spoint <= RulerLength)
                            {
                                Rectangle DSclLine = new Rectangle();
                                DSclLine.Fill = RulerColor;
                                DSclLine.StrokeThickness = 0;
                                DSclLine.Height = 1;
                                if (MDivs != 0 && MDivs == cnt)
                                    DSclLine.Width = MiddleMarkLength;
                                else
                                    DSclLine.Width = DivisionMarkLength;
                                spRulerPanel.Children.Add(DSclLine);
                                DSclLine.SetValue(Canvas.TopProperty, spoint * ZoomFactor);
                                if (RulerHorizontalAlignment == HorizontalAlignment.Left)
                                {
                                    DSclLine.SetValue(Canvas.LeftProperty, 0d);
                                }
                                else if (RulerHorizontalAlignment == HorizontalAlignment.Right)
                                {
                                    if (MDivs != 0 && MDivs == cnt)
                                        DSclLine.SetValue(Canvas.LeftProperty, spRulerPanel.Width - MiddleMarkLength);
                                    else
                                        DSclLine.SetValue(Canvas.LeftProperty, spRulerPanel.Width - DivisionMarkLength);
                                }
                                else if (RulerHorizontalAlignment == HorizontalAlignment.Center)
                                {
                                    if (MDivs != 0 && MDivs == cnt)
                                        DSclLine.SetValue(Canvas.LeftProperty, (spRulerPanel.Width / 2) - (MiddleMarkLength / 2));
                                    else
                                        DSclLine.SetValue(Canvas.LeftProperty, (spRulerPanel.Width / 2) - (DivisionMarkLength / 2));
                                }
                                cnt++;
                            }
                        }
                    }
                }
            }

        }

    }
}
