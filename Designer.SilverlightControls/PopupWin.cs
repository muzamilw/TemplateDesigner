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
    [TemplateVisualState(Name = "HideWin", GroupName = "ViewStates")]
    [TemplateVisualState(Name = "ShowWin", GroupName = "ViewStates")]
    [TemplatePart(Name = "PopupWinRoot", Type = typeof(Canvas))]
    [TemplatePart(Name = "HeaderPanel", Type = typeof(Border))]
    public class PopupWin : ContentControl
    {
        #region Fields
        private Border brdHeaderPanel;
        private Canvas cnvRootElement;
        //private FrameworkElement contentElement;
        private VisualState HideState;
        private bool MouseDown;
        private Point MousePoiner = new Point(0.0, 0.0);
        #endregion
        #region Properties
        public Point WinMousePoiner { get { return MousePoiner; } }
        #endregion
        #region Dependency Properties
        public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register("HeaderContent", typeof(object), typeof(PopupWin), null);
        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }
        
        public static readonly DependencyProperty HeaderMarginProperty = DependencyProperty.Register("HeaderMargin", typeof(Thickness), typeof(PopupWin), null);
        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }
        public static readonly DependencyProperty HeaderPaddingProperty = DependencyProperty.Register("HeaderPadding", typeof(Thickness), typeof(PopupWin), null);
        public Thickness HeaderPadding
        {
            get { return (Thickness)GetValue(HeaderPaddingProperty); }
            set { SetValue(HeaderPaddingProperty, value); }
        }
        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(PopupWin), null);
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }
        public static readonly DependencyProperty HeaderCornerRadiusProperty = DependencyProperty.Register("HeaderCornerRadius", typeof(CornerRadius), typeof(PopupWin), null);
        public CornerRadius HeaderCornerRadius
        {
            get { return (CornerRadius)GetValue(HeaderCornerRadiusProperty); }
            set { SetValue(HeaderCornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(PopupWin), null);
        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(PopupWin), null);
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty IsOpenedProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(PopupWin), new PropertyMetadata(false, new PropertyChangedCallback(IsOpenedPropertyChanged)));
        public bool IsOpened
        {
            get { return (bool)GetValue(IsOpenedProperty); }
            set
            {
                SetValue(IsOpenedProperty, value);
            }
        }





        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowCloseButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(PopupWin), new PropertyMetadata(true));

        
       
        #endregion
        #region "event declaration"
        public delegate void PopupWinMove_EventHandler(object sender, MouseEventArgs e);
        public event PopupWinMove_EventHandler PopupWinMove;
        public delegate void PopupWinOpenClose_EventHandler(object sender, bool IsOpen);
        public event PopupWinOpenClose_EventHandler PopupWinOpenClose;
        
        #endregion

        public PopupWin()
        {
            DefaultStyleKey = typeof(PopupWin);
            //IsOpened = false;
            MouseDown = false;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            brdHeaderPanel = GetTemplateChild("HeaderPanel") as Border;
            if (brdHeaderPanel != null)
            {

                 Button btnCloseButton = GetTemplateChild("btnClose") as Button;
                if (ShowCloseButton)
                {
                   
                    if (btnCloseButton != null)
                        btnCloseButton.Click += btnClose_Click;
                }
                else
                {
                    btnCloseButton.Visibility = System.Windows.Visibility.Collapsed;
                }
                brdHeaderPanel.MouseLeftButtonDown += grdHeaderPanel_MouseLeftButtonDown;
                brdHeaderPanel.MouseLeftButtonUp += grdHeaderPanel_MouseLeftButtonUp;
                brdHeaderPanel.MouseMove += grdHeaderPanel_MouseMove;
            }
            cnvRootElement = GetTemplateChild("PopupWinRoot") as Canvas;
            if (cnvRootElement != null)
            {
                if (IsOpened)
                    cnvRootElement.Visibility = Visibility.Visible;
                else
                    cnvRootElement.Visibility = Visibility.Collapsed;

                HideState = GetTemplateChild("HideWin") as VisualState;
                if (HideState != null && HideState.Storyboard != null)
                {
                    HideState.Storyboard.Completed += HideStoryboard_Completed;
                }

            }
            ChangeVisualState(false);
        }

        void grdHeaderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDown)
            {
                //double X, Y;
                //X = (e.GetPosition(cnvRootElement).X - MousePoiner.X);
                //Y = (e.GetPosition(cnvRootElement).Y - MousePoiner.Y);
                //cnvRootElement.SetValue(Canvas.LeftProperty, X);
                //cnvRootElement.SetValue(Canvas.TopProperty, Y);
                if (PopupWinMove != null)
                    PopupWinMove(this, e);
            }
        }

        void grdHeaderPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseDown = false;
        }

        void grdHeaderPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UIElement el = (UIElement)sender;
            el.CaptureMouse();
            MouseDown = true;
            MousePoiner = e.GetPosition(this);
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            IsOpened = !IsOpened;
            ChangeVisualState(false);
        }
        private void HideStoryboard_Completed(object sender, EventArgs e)
        {
            cnvRootElement.Visibility = Visibility.Collapsed;
            if (PopupWinOpenClose != null)
                PopupWinOpenClose(this, false);
            
        }
        private void ChangeVisualState(bool useTransitions)
        {
            if (IsOpened)
            {
                if (cnvRootElement != null)
                {
                    cnvRootElement.Visibility = Visibility.Visible;
                    if (PopupWinOpenClose != null)
                        PopupWinOpenClose(this, true);
                }
                VisualStateManager.GoToState(this, "ShowWin", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "HideWin", useTransitions);
                if (HideState == null)
                {
                    if (cnvRootElement != null)
                    {
                        cnvRootElement.Visibility = Visibility.Collapsed;
                        if (PopupWinOpenClose != null)
                            PopupWinOpenClose(this, false);
                    }
                }
            }
        }
        static void IsOpenedPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PopupWin ddp = sender as PopupWin;
            ddp.ChangeVisualState(false);
        }
    }
}
