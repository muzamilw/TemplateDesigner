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
using System.Windows.Controls.Primitives;

namespace PrintFlow.SilverlightControls
{
    [TemplateVisualState(Name = "HideContent", GroupName = "ViewStates")]
    [TemplateVisualState(Name = "ShowContent", GroupName = "ViewStates")]
    [TemplatePart(Name = "ContentPanel", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "ShowHideButton", Type = typeof(ToggleButton))]
    public class DropdownPanel : ContentControl
    {
        #region Fields
        private ToggleButton btnShowHide;
        private FrameworkElement contentElement;
        private VisualState HideState;
        private Popup DdPopup;
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register("HeaderContent", typeof(object), typeof(DropdownPanel), null);
        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }
        public static readonly DependencyProperty HeaderButtonContentProperty = DependencyProperty.Register("HeaderButtonContent", typeof(object), typeof(DropdownPanel), null);
        public object HeaderButtonContent
        {
            get { return (object)GetValue(HeaderButtonContentProperty); }
            set { SetValue(HeaderButtonContentProperty, value); }
        }
        public static readonly DependencyProperty IsOpenedProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(DropdownPanel), new PropertyMetadata(false, new PropertyChangedCallback(IsOpenedPropertyChanged)));
        public bool IsOpened
        {
            get { return (bool)GetValue(IsOpenedProperty); }
            set
            {
                SetValue(IsOpenedProperty, value);
            }
        }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(DropdownPanel), null);
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty HeaderHeightProperty = DependencyProperty.Register("HeaderHeight", typeof(double), typeof(DropdownPanel), new PropertyMetadata(20d));
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }
        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(DropdownPanel), null);
        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }
        public static readonly DependencyProperty HeaderMarginProperty = DependencyProperty.Register("HeaderMargin", typeof(Thickness), typeof(DropdownPanel), null);
        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }
        public static readonly DependencyProperty AnimHeightProperty = DependencyProperty.Register("AnimHeight", typeof(double), typeof(DropdownPanel), new PropertyMetadata(20d));
        public double AnimHeight
        {
            get { return (double)GetValue(AnimHeightProperty); }
            set { SetValue(AnimHeightProperty, value); }
        }
        public static readonly DependencyProperty AnimDurationProperty = DependencyProperty.Register("AnimDuration", typeof(Duration), typeof(DropdownPanel), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5))));
        public Duration AnimDuration
        {
            get { return (Duration)GetValue(AnimDurationProperty); }
            set { SetValue(AnimDurationProperty, value); }
        }
        #endregion
        #region "event declaration"
        public delegate void DropdownBeforeOpenClose_EventHandler(object sender, bool IsOpen);
        public event DropdownBeforeOpenClose_EventHandler DropdownBeforeOpenClose;

        #endregion

        public DropdownPanel()
        {
            DefaultStyleKey = typeof(DropdownPanel);
            IsOpened = false;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            btnShowHide = GetTemplateChild("ShowHideButton") as ToggleButton;
            if (btnShowHide != null)
            {
                btnShowHide.Click += btnShowHide_Click;
            }
            contentElement = GetTemplateChild("ContentPanel") as FrameworkElement;
            if (contentElement != null)
            {
                HideState = GetTemplateChild("HideContent") as VisualState;
                if (HideState != null && HideState.Storyboard != null)
                {
                    HideState.Storyboard.Completed += HideStoryboard_Completed;
                    if ((HideState.Storyboard.Children[0] as DoubleAnimation) != null)
                    {
                        (HideState.Storyboard.Children[0] as DoubleAnimation).To = -AnimHeight;
                        (HideState.Storyboard.Children[0] as DoubleAnimation).Duration = AnimDuration;
                    }
                }
                VisualState ShowState = GetTemplateChild("ShowContent") as VisualState;
                if (ShowState != null && ShowState.Storyboard != null)
                {
                    if ((ShowState.Storyboard.Children[0] as DoubleAnimation) != null)
                    {
                        (ShowState.Storyboard.Children[0] as DoubleAnimation).Duration = AnimDuration;
                    }
                }
            }
            DdPopup = GetTemplateChild("ContentPopup") as Popup;
            if (DdPopup != null)
                DdPopup.IsOpen = IsOpened;
            ChangeVisualState(false);
        }
        private void btnShowHide_Click(object sender, RoutedEventArgs e)
        {
            IsOpened = !IsOpened;
            btnShowHide.IsChecked = IsOpened;
            if (DropdownBeforeOpenClose != null)
                DropdownBeforeOpenClose(this, IsOpened);
            ChangeVisualState(true);
        }
        private void HideStoryboard_Completed(object sender, EventArgs e)
        {
            contentElement.Visibility = Visibility.Collapsed;
            if (DdPopup != null)
                DdPopup.IsOpen = IsOpened;
        }
        private void ChangeVisualState(bool useTransitions)
        {
            if (IsOpened)
            {
                if (contentElement != null)
                {
                    contentElement.Visibility = Visibility.Visible;
                }
                if (DdPopup != null)
                    DdPopup.IsOpen = IsOpened;
                VisualStateManager.GoToState(this, "ShowContent", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "HideContent", useTransitions);
                if (HideState == null)
                {
                    if (contentElement != null) contentElement.Visibility = Visibility.Collapsed;
                }
            }
        }
        static void IsOpenedPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DropdownPanel ddp = sender as DropdownPanel;
            ddp.ChangeVisualState(true);
        }
    }
}
