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
using System.Linq;
using System.Collections.Generic;
namespace PrintFlow.SilverlightControls
{
    public class AdjustablePanel : Panel
    {
        #region Dependency Properties
        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.RegisterAttached("OffsetX", typeof(double), typeof(AdjustablePanel), null);
        public static double GetOffsetX(DependencyObject obj)
        {
            return (double)obj.GetValue(OffsetXProperty);
        }
        public static void SetOffsetX(DependencyObject obj, double value)
        {
            obj.SetValue(OffsetXProperty, value);
        }

        public static readonly DependencyProperty OffsetYProperty = DependencyProperty.RegisterAttached("OffsetY", typeof(double), typeof(AdjustablePanel), null);
        public static double GetOffsetY(DependencyObject obj)
        {
            return (double)obj.GetValue(OffsetYProperty);
        }
        public static void SetOffsetY(DependencyObject obj, double value)
        {
            obj.SetValue(OffsetYProperty, value);
        }

        public static readonly DependencyProperty IsStartNewLineProperty = DependencyProperty.RegisterAttached("IsStartNewLine", typeof(bool), typeof(AdjustablePanel), null);
        public static bool GetIsStartNewLine(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsStartNewLineProperty);
        }
        public static void SetIsStartNewLine(DependencyObject obj, bool value)
        {
            obj.SetValue(IsStartNewLineProperty, value);
        }

        public static readonly DependencyProperty HAlignProperty = DependencyProperty.Register("HAlign", typeof(TextAlignment), typeof(AdjustablePanel), new PropertyMetadata(TextAlignment.Left));
        public TextAlignment HAlign
        {
            get { return (TextAlignment)GetValue(HAlignProperty); }
            set
            {
                SetValue(HAlignProperty, value);
                //this.HorizontalAlignment = value;
                if (this.Children.Count > 0)
                {
                    ((TextBlock)this.Children[0]).TextAlignment = value;
                }
            }
        }

        //private TextAlignment _textAlign;

        //public TextAlignment HAlign
        //{
        //    get { return _textAlign; }
        //    set { _textAlign = value; ((TextBlock)this.Children[0]).TextAlignment = value; }
        //}
        



        public static readonly DependencyProperty VAlignProperty = DependencyProperty.Register("VAlign", typeof(VerticalAlignment), typeof(AdjustablePanel), new PropertyMetadata(VerticalAlignment.Top));
        public VerticalAlignment VAlign
        {
            get { return (VerticalAlignment)GetValue(VAlignProperty); }
            set { SetValue(VAlignProperty, value);
                this.VerticalAlignment = value; }
        }
        public static readonly DependencyProperty DisplayIndexProperty = DependencyProperty.RegisterAttached("DisplayIndex", typeof(int), typeof(AdjustablePanel), new PropertyMetadata(0, DisplayIndexChanged));
        public static int GetDisplayIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(DisplayIndexProperty);
        }
        public static void SetDisplayIndex(DependencyObject obj, int value)
        {
            obj.SetValue(DisplayIndexProperty, value);
        }
        #endregion
        #region "event declaration"
        public delegate void AdjustablePanelArranged_EventHandler(object sender, Size PanelSize,int ChildCount);
        public event AdjustablePanelArranged_EventHandler AdjustablePanelArranged;

        #endregion
        public static void DisplayIndexChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                if (element.Parent != null && element.Parent.GetType().Name == "AdjustablePanel")
                {
                    AdjustablePanel objPanel = element.Parent as AdjustablePanel;
                    if (objPanel != null)
                    {
                        objPanel.InvalidateArrange();
                    }
                }
            }
        }  
        public AdjustablePanel()
        {
            // default orientation
            HAlign = TextAlignment.Left;
            VAlign = VerticalAlignment.Top;
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in Children)
            {
                child.Measure(new Size(availableSize.Width, availableSize.Height));
            }
            return new Size(availableSize.Width, availableSize.Height);
            //Size MxSize = GetMaxPanelSize();
            //return new Size(MxSize.Width, MxSize.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {

            //The location where the child will be displayed
            //double currentX = 0, currentY = 0, PnlW = 0, PnlH = 0, MxLineWd = 0; ;
            //double CtlOffsetX = 0, CtlOffsetY = 0;
            //double largestHeight = 0;



            foreach (UIElement child in Children)
            {
                child.Arrange(new Rect(new Point(0,0), child.DesiredSize));

            }
            
            //Return the total size used
            //if (AdjustablePanelArranged != null)
                //AdjustablePanelArranged(this, finalSize, Children.Count);
            return  finalSize;
        }

        //protected override Size ArrangeOverride(Size finalSize)
        //{

        //    //The location where the child will be displayed
        //    double currentX = 0, currentY = 0, PnlW = 0, PnlH = 0, MxLineWd = 0; ;
        //    double CtlOffsetX = 0, CtlOffsetY = 0;
        //    double largestHeight = 0;

        //    IList<UIElement> orderedChildren =
        //Children
        //.Select(element => new
        //{
        //    Element = element,
        //    SortOrder = element.GetValue(DisplayIndexProperty),

        //})
        //.OrderBy(ordered => ordered.SortOrder)
        //.Select(ordered => ordered.Element)
        //.ToList();
        //    Size MxSize = GetMaxPanelSize(orderedChildren);
        //    if (HAlign == HorizontalAlignment.Right)
        //    {
        //        currentX = MxSize.Width;
        //    }
        //    if (VAlign == VerticalAlignment.Bottom)
        //    {
        //        currentY = MxSize.Height;
        //    }
        //    for (int CrtIdx = 0; CrtIdx < orderedChildren.Count; CrtIdx++)
        //    {
        //        FrameworkElement child = (FrameworkElement)orderedChildren[CrtIdx];
        //        if (child.GetType().Name == "Border")
        //        {
        //            Border objBrd = child as Border;
        //            if (objBrd.Child.GetType().Name == "TextBlock")
        //            {
        //                TextBlock ObjChild = objBrd.Child as TextBlock;
        //                //objBrd.BorderThickness = new Thickness(1.0);
        //                //objBrd.BorderBrush = new SolidColorBrush(Colors.Green);
        //                if (ObjChild.Text == "" || objBrd.Visibility == Visibility.Collapsed)
        //                {

        //                    child.Arrange(new Rect(0, 0, 0, 0));
        //                }
        //                else
        //                {
        //                    CtlOffsetX = 0;
        //                    CtlOffsetY = 0;
        //                    if (child.GetValue(OffsetXProperty) != null)
        //                        CtlOffsetX = (double)child.GetValue(OffsetXProperty);
        //                    if (child.GetValue(OffsetYProperty) != null)
        //                        CtlOffsetY = (double)child.GetValue(OffsetYProperty);
        //                    #region left , top panel

        //                    if (HAlign == HorizontalAlignment.Left && VAlign == VerticalAlignment.Top)
        //                    {
        //                        currentX += CtlOffsetX;
        //                        currentY += CtlOffsetY;
        //                        if (child.GetValue(IsStartNewLineProperty) != null)
        //                        {
        //                            if ((bool)child.GetValue(IsStartNewLineProperty))
        //                            {
        //                                currentX = CtlOffsetX;
        //                                currentY += CtlOffsetY + largestHeight;
        //                            }
        //                        }

        //                        double t = child.ActualHeight;
        //                        double t2 = child.DesiredSize.Height;

        //                        Point location = new Point(currentX, currentY);
        //                        child.Arrange(new Rect(location, child.DesiredSize));
        //                        //Calculate the new location


        //                        //currentY += child.DesiredSize.Height;



        //                        currentX += child.DesiredSize.Width;
        //                        if (currentX > PnlW)
        //                            PnlW = currentX;
        //                        if (currentY > PnlH)
        //                            PnlH = currentY;
        //                        if (child.DesiredSize.Height > largestHeight)
        //                            largestHeight = child.DesiredSize.Height;
        //                    }
        //                    #endregion
        //                    #region Right , top panel
        //                    else if (HAlign == HorizontalAlignment.Right && VAlign == VerticalAlignment.Top)
        //                    {
        //                        currentX -= CtlOffsetX;
        //                        currentY += CtlOffsetY;
        //                        if (child.GetValue(IsStartNewLineProperty) != null)
        //                        {
        //                            if ((bool)child.GetValue(IsStartNewLineProperty))
        //                            {
        //                                currentX = MxSize.Width - CtlOffsetX;
        //                                currentY += CtlOffsetY + largestHeight;
        //                            }
        //                        }

        //                        currentX -= child.DesiredSize.Width;

        //                        Point location = new Point(currentX, currentY);
        //                        child.Arrange(new Rect(location, child.DesiredSize));
        //                        //Calculate the new location


        //                        //currentY += child.DesiredSize.Height;




        //                        if (currentX > PnlW)
        //                            PnlW = currentX;
        //                        if (currentY > PnlH)
        //                            PnlH = currentY;
        //                        if (child.DesiredSize.Height > largestHeight)
        //                            largestHeight = child.DesiredSize.Height;
        //                    }
        //                    #endregion
        //                    #region left , Bottom panel
        //                    else if (HAlign == HorizontalAlignment.Left && VAlign == VerticalAlignment.Bottom)
        //                    {
        //                        currentX += CtlOffsetX;
        //                        currentY -= CtlOffsetY;
        //                        if (child.GetValue(IsStartNewLineProperty) != null)
        //                        {
        //                            if ((bool)child.GetValue(IsStartNewLineProperty))
        //                            {
        //                                currentX = CtlOffsetX;
        //                                currentY = currentY - CtlOffsetY - largestHeight;
        //                            }
        //                        }

        //                        Point location = new Point(currentX, currentY - child.DesiredSize.Height);
        //                        child.Arrange(new Rect(location, child.DesiredSize));
        //                        //Calculate the new location


        //                        //currentY += child.DesiredSize.Height;



        //                        currentX += child.DesiredSize.Width;
        //                        if (currentX > PnlW)
        //                            PnlW = currentX;
        //                        if (currentY > PnlH)
        //                            PnlH = currentY;
        //                        if (child.DesiredSize.Height > largestHeight)
        //                            largestHeight = child.DesiredSize.Height;
        //                    }
        //                    #endregion
        //                    #region Right , bottom panel
        //                    else if (HAlign == HorizontalAlignment.Right && VAlign == VerticalAlignment.Bottom)
        //                    {
        //                        currentX -= CtlOffsetX;
        //                        currentY += CtlOffsetY;
        //                        if (child.GetValue(IsStartNewLineProperty) != null)
        //                        {
        //                            if ((bool)child.GetValue(IsStartNewLineProperty))
        //                            {
        //                                currentX = MxSize.Width - CtlOffsetX;
        //                                currentY = currentY - CtlOffsetY - largestHeight;
        //                            }
        //                        }

        //                        currentX -= child.DesiredSize.Width;

        //                        Point location = new Point(currentX, currentY - child.DesiredSize.Height);
        //                        child.Arrange(new Rect(location, child.DesiredSize));
        //                        //Calculate the new location


        //                        //currentY += child.DesiredSize.Height;




        //                        if (currentX > PnlW)
        //                            PnlW = currentX;
        //                        if (currentY > PnlH)
        //                            PnlH = currentY;
        //                        if (child.DesiredSize.Height > largestHeight)
        //                            largestHeight = child.DesiredSize.Height;
        //                    }
        //                    #endregion

        //                    #region Center , top panel

        //                    else if (HAlign == HorizontalAlignment.Center && VAlign == VerticalAlignment.Top)
        //                    {
        //                        currentX += CtlOffsetX;
        //                        currentY += CtlOffsetY;
        //                        if (CrtIdx == 0)
        //                        {
        //                            MxLineWd = GetLineWidth(orderedChildren, CrtIdx);
        //                            if (MxSize.Width > MxLineWd)
        //                                currentX = CtlOffsetX + ((MxSize.Width - MxLineWd) / 2);
        //                            else
        //                                currentX = CtlOffsetX;
        //                        }
        //                        if (CrtIdx != 0 && child.GetValue(IsStartNewLineProperty) != null)
        //                        {
        //                            if ((bool)child.GetValue(IsStartNewLineProperty))
        //                            {
        //                                MxLineWd = GetLineWidth(orderedChildren, CrtIdx);
        //                                if (MxSize.Width > MxLineWd)
        //                                    currentX = CtlOffsetX + ((MxSize.Width - MxLineWd) / 2);
        //                                else
        //                                    currentX = CtlOffsetX;
        //                                currentY += CtlOffsetY + largestHeight;
        //                            }
        //                        }

        //                        double t = child.ActualHeight;
        //                        double t2 = child.DesiredSize.Height;

        //                        Point location = new Point(currentX, currentY);
        //                        child.Arrange(new Rect(location, child.DesiredSize));
        //                        //Calculate the new location


        //                        //currentY += child.DesiredSize.Height;



        //                        currentX += child.DesiredSize.Width;
        //                        if (currentX > PnlW)
        //                            PnlW = currentX;
        //                        if (currentY > PnlH)
        //                            PnlH = currentY;
        //                        if (child.DesiredSize.Height > largestHeight)
        //                            largestHeight = child.DesiredSize.Height;
        //                    }
        //                    #endregion
        //                    #region Center , Bottom panel
        //                    else if (HAlign == HorizontalAlignment.Center && VAlign == VerticalAlignment.Bottom)
        //                    {
        //                        currentX += CtlOffsetX;
        //                        currentY -= CtlOffsetY;
        //                        if (CrtIdx == 0)
        //                        {
        //                            MxLineWd = GetLineWidth(orderedChildren, CrtIdx);
        //                            if (MxSize.Width > MxLineWd)
        //                                currentX = CtlOffsetX + ((MxSize.Width - MxLineWd) / 2);
        //                            else
        //                                currentX = CtlOffsetX;
        //                        }
        //                        if (CrtIdx != 0 && child.GetValue(IsStartNewLineProperty) != null)
        //                        {
        //                            if ((bool)child.GetValue(IsStartNewLineProperty))
        //                            {
        //                                MxLineWd = GetLineWidth(orderedChildren, CrtIdx);
        //                                if (MxSize.Width > MxLineWd)
        //                                    currentX = CtlOffsetX + ((MxSize.Width - MxLineWd) / 2);
        //                                else
        //                                    currentX = CtlOffsetX;
        //                                currentY = currentY - CtlOffsetY - largestHeight;
        //                            }
        //                        }

        //                        Point location = new Point(currentX, currentY - child.DesiredSize.Height);
        //                        child.Arrange(new Rect(location, child.DesiredSize));
        //                        //Calculate the new location


        //                        //currentY += child.DesiredSize.Height;



        //                        currentX += child.DesiredSize.Width;
        //                        if (currentX > PnlW)
        //                            PnlW = currentX;
        //                        if (currentY > PnlH)
        //                            PnlH = currentY;
        //                        if (child.DesiredSize.Height > largestHeight)
        //                            largestHeight = child.DesiredSize.Height;
        //                    }
        //                    #endregion
        //                }
        //            }
        //        }

        //    }
        //    PnlH += largestHeight;
        //    //Return the total size used
        //    if (AdjustablePanelArranged != null)
        //        AdjustablePanelArranged(this, MxSize, Children.Count);
        //    return new Size(MxSize.Width, MxSize.Height);
        //}

        //private Size GetMaxPanelSize(IList<UIElement> orderedChildren)
        //{
        //    //The location where the child will be displayed
        //    double currentX = 0, currentY = 0, PnlW = 0, PnlH = 0;
        //    double CtlOffsetX = 0, CtlOffsetY = 0;
        //    double largestHeight = 0;
        //    foreach (FrameworkElement child in orderedChildren)
        //    {
        //        if (child.GetType().Name == "Border")
        //        {
        //            Border objBrd = child as Border;
        //            if (objBrd.Child.GetType().Name == "TextBlock")
        //            {
        //                TextBlock ObjChild = objBrd.Child as TextBlock;
        //                if (ObjChild.Text == "" || objBrd.Visibility == Visibility.Collapsed)
        //                {

        //                }
        //                else
        //                {

        //                    CtlOffsetX = 0;
        //                    CtlOffsetY = 0;
        //                    if (child.GetValue(OffsetXProperty) != null)
        //                        CtlOffsetX = (double)child.GetValue(OffsetXProperty);
        //                    if (child.GetValue(OffsetYProperty) != null)
        //                        CtlOffsetY = (double)child.GetValue(OffsetYProperty);

        //                    currentX += CtlOffsetX;
        //                    currentY += CtlOffsetY;
        //                    if (child.GetValue(IsStartNewLineProperty) != null)
        //                    {
        //                        if ((bool)child.GetValue(IsStartNewLineProperty))
        //                        {
        //                            currentX = CtlOffsetX;
        //                            currentY += CtlOffsetY + largestHeight;
        //                        }
        //                    }
        //                    currentX += child.DesiredSize.Width;
        //                    if (currentX > PnlW)
        //                        PnlW = currentX;
        //                    if (currentY > PnlH)
        //                        PnlH = currentY;
        //                    if (child.DesiredSize.Height > largestHeight)
        //                        largestHeight = child.DesiredSize.Height;

        //                }
        //            }
        //        }
        //    }
        //    PnlH += largestHeight;
        //    //Return the total size used
        //    return new Size(PnlW, PnlH);
        //}
        //private double GetLineWidth(IList<UIElement> orderedChildren, int Idx)
        //{
        //    double LWd = 0;
        //    for (int CrtIdx = Idx; CrtIdx < orderedChildren.Count; CrtIdx++)
        //    {
        //        FrameworkElement child = (FrameworkElement)orderedChildren[CrtIdx];
        //        if (child.GetType().Name == "Border")
        //        {
        //            Border objBrd = child as Border;
        //            if (objBrd.Child.GetType().Name == "TextBlock")
        //            {
        //                TextBlock ObjChild = objBrd.Child as TextBlock;
        //                if (ObjChild.Text != "" && objBrd.Visibility != Visibility.Collapsed)
        //                {
        //                    LWd += child.DesiredSize.Width;
        //                    if (CrtIdx + 1 >= orderedChildren.Count)
        //                        break;
        //                    else
        //                    {
        //                        FrameworkElement child2 = (FrameworkElement)orderedChildren[CrtIdx + 1];
        //                        if (child2.GetValue(IsStartNewLineProperty) != null)
        //                        {
        //                            if ((bool)child2.GetValue(IsStartNewLineProperty))
        //                                break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return LWd;
        //}
    }
}
