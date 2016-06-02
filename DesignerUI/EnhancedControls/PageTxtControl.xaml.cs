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

namespace webprintDesigner.EnhancedControls
{
    public partial class PageTxtControl : UserControl
    {
        public bool ShowException = false;
        public string PageConName="";
        public string PageTxtCtlName = "";
        public PageContainer objPageContainer;
        private int CtlIdx=1;
        public PageTxtControl(string PCnName,string PCtlName,PageContainer PgContainer)
        {
            InitializeComponent();
            PageConName = PCnName;
            PageTxtCtlName = PCtlName;
            objPageContainer = PgContainer;
        }

        //programatically adding objects ( loading from db )
        public void AddObjects(string ObjectName,string TxtObjName, ProductServiceReference.TemplateObjects objObects)
        {
            try
            {
                if (objObects.ObjectType == 1 || objObects.ObjectType == 2 || (objObects.ObjectType == 4 && App.DesignerMode== ProductServiceReference.DesignerModes.CreatorMode))
                {

                    CtlIdx++;
                    Grid grdCrlsList = new Grid();
                    grdCrlsList.Margin = new Thickness(1);
                    //grdCrlsList.ShowGridLines = true;
                    grdCrlsList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    grdCrlsList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                    grdCrlsList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
                    //grdCrlsList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });


                    grdCrlsList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(19) });
                    grdCrlsList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });
                    grdCrlsList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(19) });
                    TextBlock ctlLblName = new TextBlock();
                    //TextBlock ctlLblShow = new TextBlock();
                    //TextBlock ctlLblLock = new TextBlock();
                    CheckBox ctlchkShow = new CheckBox();
                    CheckBox ctlChkLock = new CheckBox();
                    TextBox ctlTextContent = new TextBox();
                    TextBlock ctlLblContent = new TextBlock();

                    
                    ctlLblName.Margin = new Thickness(1);
                    ctlchkShow.Margin = new Thickness(1);
                    ctlChkLock.Margin = new Thickness(1);
                    ctlTextContent.Margin = new Thickness(1);
                    ctlLblContent.Margin = new Thickness(1);

                    ctlTextContent.MaxLength = 5000;
                    
                    //Button ctlUpButton = new Button();
                    //Button ctlDownButton = new Button();
                    //Image ctlUpImage = new Image();
                    //Image ctlDownImage = new Image();

                    ctlLblName.Name = "CtlLblName_" + CtlIdx.ToString();
                    //ctlLblShow.Name = "CtlLblShow_" + CtlIdx.ToString();
                    //ctlLblLock.Name = "CtlLblLook_" + CtlIdx.ToString();
                    ctlchkShow.Name = "CtlChkShow_" + CtlIdx.ToString();
                    ctlChkLock.Name = "CtlChkLock_" + CtlIdx.ToString();
                    ctlLblContent.Name = "ctlLblContent_" + CtlIdx.ToString();
                    ctlTextContent.Name = TxtObjName;

                    ctlchkShow.Tag = ObjectName;
                    ctlChkLock.Tag = ObjectName;
                    ctlTextContent.Tag = ObjectName;
                    ctlLblName.Tag = ObjectName;

                    ctlLblName.Text = objObects.Name;
                    //ctlLblShow.Text = "Visible";
                    //ctlLblLock.Text = "Editable";
                    ctlchkShow.IsChecked = true;
                    ctlChkLock.IsChecked = false;
                    ctlTextContent.Text = objObects.ContentString;
                    ctlLblContent.Text = "Label/Text";
                    ctlChkLock.Content = "Locked for Editing";
                    ctlchkShow.Content = "Visible";

                    if (objObects.ObjectType == 2)
                    {
                        ctlTextContent.AcceptsReturn = true;
                        ctlTextContent.Height = 65;
                        ctlTextContent.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    }
                    else
                        ctlTextContent.AcceptsReturn = false;

                    ////ctlUpImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Images/upArrow.png", UriKind.Relative));
                    ////ctlDownImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Images/downArrow.png", UriKind.Relative));
                    //ctlUpButton.Width = 16;
                    //ctlUpButton.Height = 16;
                    //ctlDownButton.Width = 16;
                    //ctlDownButton.Height = 16;
                    //ctlUpButton.VerticalAlignment = VerticalAlignment.Top;
                    //ctlDownButton.VerticalAlignment = VerticalAlignment.Bottom;
                    ctlLblName.Style = this.Resources["stlCtrlListTextBlock"] as Style;
                    //ctlLblShow.Style = this.Resources["stlCtrlListLable2"] as Style;
                    //ctlLblLock.Style = this.Resources["stlCtrlListLable2"] as Style;
                    ctlchkShow.Style = this.Resources["stlCtrlListChkBox"] as Style;
                    ctlChkLock.Style = this.Resources["stlCtrlListChkBox"] as Style;
                    ctlTextContent.Style = this.Resources["stlCtrlListTxtBox"] as Style;
                    ctlLblContent.Style = this.Resources["stlCtrlListLable2"] as Style;

                    //ctlUpButton.Style = this.Resources["RoundUpButtonStyle"] as Style;
                    //ctlDownButton.Style = this.Resources["RoundDownButtonStyle"] as Style;

                    ctlTextContent.GotFocus += ctlTextBox1_GotFocus;
                    ctlTextContent.TextChanged += ctlTextBox1_TextChanged;
                    ctlchkShow.Checked += ctlShow_Checked;
                    ctlchkShow.Unchecked += ctlShow_Checked;
                    ctlChkLock.Checked += ctlLock_Checked;
                    ctlChkLock.Unchecked += ctlLock_Checked;
                    //ctlUpButton.Click += new RoutedEventHandler(ctlUpButton_Click);
                    //ctlDownButton.Click += new RoutedEventHandler(ctlDownButton_Click);
                    //if (App.DesignMode == 1 || App.DesignMode == 2)
                    //{
                    //    ctlUpButton.Visibility = Visibility.Visible;
                    //    ctlDownButton.Visibility = Visibility.Visible;
                    //}
                    //else
                    //{
                    //    ctlUpButton.Visibility = Visibility.Collapsed;
                    //    ctlDownButton.Visibility = Visibility.Collapsed;
                    //}
                    if (App.DesignerMode == DesignerModes.CreatorMode)
                    {
                        ctlchkShow.Visibility = Visibility.Visible;
                        ctlChkLock.Visibility = Visibility.Visible;
                        //ctlLblShow.Visibility = Visibility.Visible;
                        //ctlLblLock.Visibility = Visibility.Visible;
                        ctlChkLock.IsEnabled = true;
                    }
                    else if (App.DesignerMode == DesignerModes.SimpleEndUser || App.DesignerMode ==  DesignerModes.AdvancedEndUser)
                    {
                        ctlchkShow.Visibility = Visibility.Visible;
                        ctlChkLock.Visibility = Visibility.Visible;
                        //ctlLblShow.Visibility = Visibility.Visible;
                        //ctlLblLock.Visibility = Visibility.Visible;
                        ctlChkLock.IsEnabled = false;
                    }
                    else
                    {
                        ctlchkShow.Visibility = Visibility.Collapsed;
                        ctlChkLock.Visibility = Visibility.Collapsed;
                        //ctlLblShow.Visibility = Visibility.Collapsed;
                        //ctlLblLock.Visibility = Visibility.Collapsed;
                    }
                    ////ctlTextBlock1.Style.SetValue(, "2,5,0,0");
                    //ctlUpButton.SetValue(Grid.ColumnProperty, 0);
                    //ctlDownButton.SetValue(Grid.ColumnProperty, 0);
                    ctlLblName.SetValue(Grid.ColumnProperty, 1);
                    //ctlLblShow.SetValue(Grid.ColumnProperty, 1);
                    //ctlLblLock.SetValue(Grid.ColumnProperty, 2);
                    ctlchkShow.SetValue(Grid.ColumnProperty, 1);
                    ctlChkLock.SetValue(Grid.ColumnProperty, 2);
                    ctlTextContent.SetValue(Grid.ColumnProperty, 1);
                    ctlTextContent.SetValue(Grid.ColumnSpanProperty, 2);
                    ctlLblContent.SetValue(Grid.ColumnProperty, 0);

                    //ctlUpButton.SetValue(Grid.RowProperty, 0);
                    //ctlDownButton.SetValue(Grid.RowProperty, 1);
                    ctlLblName.SetValue(Grid.RowProperty, 0);
                    //ctlLblShow.SetValue(Grid.RowProperty, 2);
                    //ctlLblLock.SetValue(Grid.RowProperty, 2);
                    ctlchkShow.SetValue(Grid.RowProperty, 2);
                    ctlChkLock.SetValue(Grid.RowProperty, 2);
                    ctlTextContent.SetValue(Grid.RowProperty, 1);
                    ctlLblContent.SetValue(Grid.RowProperty, 1);

                    
                    //grdCrlsList.Children.Add(ctlUpButton);
                    //grdCrlsList.Children.Add(ctlDownButton);
                    grdCrlsList.Children.Add(ctlLblName);
                    //grdCrlsList.Children.Add(ctlLblShow);
                    //grdCrlsList.Children.Add(ctlLblLock);
                    grdCrlsList.Children.Add(ctlchkShow);
                    grdCrlsList.Children.Add(ctlChkLock);
                    grdCrlsList.Children.Add(ctlTextContent);
                    //grdCrlsList.Children.Add(ctlLblContent);
                    
                    
                    CtrlsList.Children.Add(grdCrlsList);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddObjects::" + ex.ToString());
            }
        }

        //new objects being added from UI
        public void AddControls(string ObjectName, string TxtObjName, string CName, string CText, int Ctype)
        {
            try
            {
                if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
                {
                    if (Ctype == 1 || Ctype == 2 || (Ctype == 4 && App.DesignerMode == DesignerModes.CreatorMode))
                    {
                        CtlIdx++;
                        Grid grdCrlsList = new Grid();
                        grdCrlsList.Margin = new Thickness(1);

                        grdCrlsList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                        grdCrlsList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                        grdCrlsList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });

                        grdCrlsList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(19) });
                        grdCrlsList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(53) });
                        grdCrlsList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(19) });


                        TextBlock ctlLblName = new TextBlock();
                        //TextBlock ctlTextBlock2 = new TextBlock();
                        //TextBlock ctlTextBlock3 = new TextBlock();
                        CheckBox ctlchkShow = new CheckBox();
                        CheckBox ctlChkLock = new CheckBox();
                        TextBox ctlTextContent = new TextBox();
                        //Button ctlUpButton = new Button();
                        //Button ctlDownButton = new Button();
                        //Image ctlUpImage = new Image();
                        //Image ctlDownImage = new Image();

                        ctlLblName.Margin = new Thickness(1);
                        ctlchkShow.Margin = new Thickness(1);
                        ctlChkLock.Margin = new Thickness(1);
                        ctlTextContent.Margin = new Thickness(1);

                        ctlTextContent.MaxLength = 5000;
                        

                        ctlLblName.Name = "CtlLblName_" + CtlIdx.ToString();
                        //ctlTextBlock2.Name = "CtlLblShow_" + CtlIdx.ToString();
                        //ctlTextBlock3.Name = "CtlLblLook_" + CtlIdx.ToString();
                        ctlchkShow.Name = "CtlChkShow_" + CtlIdx.ToString();
                        ctlChkLock.Name = "CtlChkLock_" + CtlIdx.ToString();
                        if (Ctype == 2)
                        {
                            ctlTextContent.AcceptsReturn = true;
                            ctlTextContent.Height = 50;
                            ctlTextContent.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                        }
                        else
                            ctlTextContent.AcceptsReturn = false;
                        ctlTextContent.Name = TxtObjName;

                        ctlChkLock.Content = "Locked for Editing";
                        ctlchkShow.Content = "Visible";

                        ctlchkShow.Tag = ObjectName;
                        ctlChkLock.Tag = ObjectName;
                        ctlTextContent.Tag = ObjectName;

                        ctlLblName.Text = CName;
                        //ctlTextBlock2.Text = "Show on Canvas";
                        //ctlTextBlock3.Text = "Lock";
                        ctlchkShow.IsChecked = true;
                        ctlChkLock.IsChecked = false;
                        ctlTextContent.Text = CText;
                        ////ctlUpImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Images/upArrow.png", UriKind.Relative));
                        ////ctlDownImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Images/downArrow.png", UriKind.Relative));
                        //ctlUpButton.Width = 16;
                        //ctlUpButton.Height = 16;
                        //ctlDownButton.Width = 16;
                        //ctlDownButton.Height = 16;
                        //ctlUpButton.VerticalAlignment = VerticalAlignment.Top;
                        //ctlDownButton.VerticalAlignment = VerticalAlignment.Bottom;
                        ctlLblName.Style = this.Resources["stlCtrlListTextBlock"] as Style;
                        //ctlTextBlock2.Style = this.Resources["stlCtrlListLable2"] as Style;
                        //ctlTextBlock3.Style = this.Resources["stlCtrlListLable2"] as Style;
                        ctlchkShow.Style = this.Resources["stlCtrlListChkBox"] as Style;
                        ctlChkLock.Style = this.Resources["stlCtrlListChkBox"] as Style;
                        ctlTextContent.Style = this.Resources["stlCtrlListTxtBox"] as Style;

                        //ctlUpButton.Style = this.Resources["RoundUpButtonStyle"] as Style;
                        //ctlDownButton.Style = this.Resources["RoundDownButtonStyle"] as Style;

                        ctlTextContent.GotFocus += ctlTextBox1_GotFocus;
                        ctlTextContent.TextChanged += ctlTextBox1_TextChanged;
                        ctlchkShow.Checked += ctlShow_Checked;
                        ctlchkShow.Unchecked += ctlShow_Checked;
                        ctlChkLock.Checked += ctlLock_Checked;
                        ctlChkLock.Unchecked += ctlLock_Checked;
                        //ctlUpButton.Click += new RoutedEventHandler(ctlUpButton_Click);
                        //ctlDownButton.Click += new RoutedEventHandler(ctlDownButton_Click);
                        if (App.DesignerMode == DesignerModes.CreatorMode )
                        {
                            ctlchkShow.Visibility = Visibility.Visible;
                            ctlChkLock.Visibility = Visibility.Visible;
                            //ctlTextBlock2.Visibility = Visibility.Visible;
                            //ctlTextBlock3.Visibility = Visibility.Visible;
                            ctlChkLock.IsEnabled = true;
                        }
                        else if (App.DesignerMode == DesignerModes.AdvancedEndUser)
                        {
                            ctlchkShow.Visibility = Visibility.Visible;
                            ctlChkLock.Visibility = Visibility.Visible;
                            //ctlTextBlock2.Visibility = Visibility.Visible;
                            //ctlTextBlock3.Visibility = Visibility.Visible;
                            ctlChkLock.IsEnabled = false;
                        }
                        //ctlTextBlock1.Style.SetValue(, "2,5,0,0");
                        //ctlUpButton.SetValue(Grid.ColumnProperty, 0);
                        //ctlDownButton.SetValue(Grid.ColumnProperty, 0);
                        ctlLblName.SetValue(Grid.ColumnProperty, 1);
                        //ctlTextBlock2.SetValue(Grid.ColumnProperty, 2);
                        //ctlTextBlock3.SetValue(Grid.ColumnProperty, 4);
                        ctlchkShow.SetValue(Grid.ColumnProperty, 1);
                        ctlChkLock.SetValue(Grid.ColumnProperty, 2);
                        ctlTextContent.SetValue(Grid.ColumnProperty, 1);
                        ctlTextContent.SetValue(Grid.ColumnSpanProperty, 2);
                        //ctlUpButton.SetValue(Grid.RowProperty, 0);
                        //ctlDownButton.SetValue(Grid.RowProperty, 1);
                        ctlLblName.SetValue(Grid.RowProperty, 0);
                        //ctlTextBlock2.SetValue(Grid.RowProperty, 0);
                        //ctlTextBlock3.SetValue(Grid.RowProperty, 0);
                        ctlchkShow.SetValue(Grid.RowProperty, 2);
                        ctlChkLock.SetValue(Grid.RowProperty, 2);
                        ctlTextContent.SetValue(Grid.RowProperty, 1);

                        //grdCrlsList.Children.Add(ctlUpButton);
                        //grdCrlsList.Children.Add(ctlDownButton);
                        grdCrlsList.Children.Add(ctlLblName);
                        //grdCrlsList.Children.Add(ctlTextBlock2);
                        //grdCrlsList.Children.Add(ctlTextBlock3);
                        grdCrlsList.Children.Add(ctlchkShow);
                        grdCrlsList.Children.Add(ctlChkLock);
                        grdCrlsList.Children.Add(ctlTextContent);
                        CtrlsList.Children.Add(grdCrlsList);
                        //CtrlsList.Children.Insert(0, grdCrlsList);

                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::AddControls::" + ex.ToString());
            }
        }
        public bool RemoveObject(string TxtObjName)
        {
            bool IsRemoved = false;
            if (TxtObjName != "" && CtrlsList.FindName(TxtObjName) != null)
            {
                if (((TextBox)CtrlsList.FindName(TxtObjName)).Parent.GetType().Name == "Grid")
                {
                    CtrlsList.Children.Remove(((TextBox)CtrlsList.FindName(TxtObjName)).Parent as UIElement);
                    IsRemoved = true;
                }
            }
            if (IsRemoved)
                UpdateCtrlsList();
            return IsRemoved;
        }
       public void UpdateCtrlsList()
        {
            try
            {
                int ctlIdx = 0;
                foreach (UIElement ctl in CtrlsList.Children)
                {
                    if (ctl.GetType().Name == "Grid")
                    {
                        Grid grdCtrls1 = ctl as Grid;
                        grdCtrls1.Tag = ctlIdx;
                        if (App.DesignerMode == DesignerModes.AdvancedEndUser || App.DesignerMode == DesignerModes.CreatorMode)
                        {
                            //if (grdCtrls1.ColumnDefinitions.Count > 0)
                            //    grdCtrls1.ColumnDefinitions[0].Width = new GridLength(18);


                            //if (grdCtrls1.Children[0].GetType().Name == "Button")
                            //    (grdCtrls1.Children[0] as Button).Visibility = Visibility.Visible;
                            //if (grdCtrls1.Children[1].GetType().Name == "Button")
                            //    (grdCtrls1.Children[1] as Button).Visibility = Visibility.Visible;

                            if (grdCtrls1.Children[0].GetType().Name == "TextBlock")
                                (grdCtrls1.Children[0] as TextBlock).Visibility = Visibility.Visible;
                            if (grdCtrls1.Children[1].GetType().Name == "TextBlock")
                                (grdCtrls1.Children[1] as TextBlock).Visibility = Visibility.Visible;
                            if (grdCtrls1.Children[2].GetType().Name == "CheckBox")
                                (grdCtrls1.Children[2] as CheckBox).Visibility = Visibility.Visible;
                            if (grdCtrls1.Children[3].GetType().Name == "CheckBox")
                            {
                                (grdCtrls1.Children[3] as CheckBox).Visibility = Visibility.Visible;
                                if (App.DesignerMode == DesignerModes.CreatorMode)
                                    (grdCtrls1.Children[4] as CheckBox).IsEnabled = true;
                                else
                                    (grdCtrls1.Children[4] as CheckBox).IsEnabled = false;
                            }
                        }
                        else
                        {
                            if (grdCtrls1.ColumnDefinitions.Count > 0)
                                grdCtrls1.ColumnDefinitions[0].Width = new GridLength(2);

                            //if (grdCtrls1.Children[0].GetType().Name == "Button")
                            //    (grdCtrls1.Children[0] as Button).Visibility = Visibility.Collapsed;
                            //if (grdCtrls1.Children[1].GetType().Name == "Button")
                            //    (grdCtrls1.Children[1] as Button).Visibility = Visibility.Collapsed;


                            if (grdCtrls1.Children[0].GetType().Name == "TextBlock")
                                (grdCtrls1.Children[0] as TextBlock).Visibility = Visibility.Collapsed;
                            if (grdCtrls1.Children[1].GetType().Name == "TextBlock")
                                (grdCtrls1.Children[1] as TextBlock).Visibility = Visibility.Collapsed;
                            if (grdCtrls1.Children[2].GetType().Name == "CheckBox")
                                (grdCtrls1.Children[2] as CheckBox).Visibility = Visibility.Collapsed;
                            if (grdCtrls1.Children[3].GetType().Name == "CheckBox")
                                (grdCtrls1.Children[3] as CheckBox).Visibility = Visibility.Collapsed;
                        }
                        ctlIdx++;
                    }
                }
                if (CtrlsList.Children.Count > 0)
                {
                    Grid grdCtrls2 = CtrlsList.Children[0] as Grid;
                    if (grdCtrls2.Children[0].GetType().Name == "Button")
                        (grdCtrls2.Children[0] as Button).Visibility = Visibility.Collapsed;

                    grdCtrls2 = CtrlsList.Children[CtrlsList.Children.Count - 1] as Grid;
                    if (grdCtrls2.Children[1].GetType().Name == "Button")
                        (grdCtrls2.Children[1] as Button).Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::UpdateCtrlsList::" + ex.ToString());
            }
        }
        void ctlUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid grdCtrls1 = (sender as Button).Parent as Grid;
                if ((int)grdCtrls1.Tag != 0)
                {
                    int idx = (int)grdCtrls1.Tag;
                    CtrlsList.Children.RemoveAt(idx);
                    CtrlsList.Children.Insert(idx - 1, grdCtrls1);
                    UpdateCtrlsList();
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ctlUpButton_Click::" + ex.ToString());
            }
        }

        void ctlDownButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid grdCtrls1 = (sender as Button).Parent as Grid;
                if ((int)grdCtrls1.Tag != CtrlsList.Children.Count - 1)
                {
                    int idx = (int)grdCtrls1.Tag;
                    CtrlsList.Children.RemoveAt(idx);
                    CtrlsList.Children.Insert(idx + 1, grdCtrls1);
                    UpdateCtrlsList();
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ctlDownButton_Click::" + ex.ToString());
            }
        }
        void ctlShow_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (objPageContainer != null)
                {
                    CheckBox chk = sender as CheckBox;
                    ObjectContainer oc = FindControl((string)chk.Tag);
                    objPageContainer.UnSelAllObject();
                    if (oc != null)
                        oc.UpdateContainerShowHide((bool)chk.IsChecked, true);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ctlShow_Checked::" + ex.ToString());
            }
        }
        void ctlLock_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (objPageContainer != null)
                {
                    CheckBox chk = sender as CheckBox;
                    ObjectContainer oc = FindControl((string)chk.Tag);
                    objPageContainer.UnSelAllObject();
                    if (oc != null)
                        oc.UpdateContainerLockPosition((bool)chk.IsChecked,true);
                }
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ctlLock_Checked::" + ex.ToString());
            }
        }

        void ctlTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                ObjectContainer oc = FindControl((string)tb.Tag);
                if (oc != null)
                    oc.UpdateContainerContent(tb.Name, tb.Text);
            }
            catch (Exception ex)
            {
                if (ShowException)
                    MessageBox.Show("::ctlTextBox1_TextChanged::" + ex.ToString());
            }
        }
        private ObjectContainer FindControl(string CtlName)
        {
            ObjectContainer oc = null;
            try
            {
                if (objPageContainer != null)
                {
                    foreach (UIElement el in objPageContainer.DesignArea.Children)
                    {
                        if (el.GetType().Name == "ObjectContainer")
                        {
                            if (((ObjectContainer)el).ContainerName != "" && ((ObjectContainer)el).ContainerName == CtlName)
                                oc = (ObjectContainer)el;
                        }
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
        void ctlTextBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (App.DesignMode == 1 || App.DesignMode == 2)
            //    {
            //        if (objPageContainer != null)
            //        {
            //            TextBox tb = sender as TextBox;
            //            ObjectContainer oc = FindControl((string)tb.Tag);
            //            objPageContainer.UnSelAllObject();
            //            if (oc != null)
            //            {

            //                GeneralTransform objGeneralTransform = oc.TransformToVisual(Application.Current.RootVisual as UIElement); 
            //                Point point = objGeneralTransform.Transform(new Point(0, 0));


            //                oc.SelectContainer(tb.Name, point);

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (ShowException)
            //        MessageBox.Show("::ctlTextBox1_GotFocus::" + ex.ToString());
            //}
        }
    }
}
