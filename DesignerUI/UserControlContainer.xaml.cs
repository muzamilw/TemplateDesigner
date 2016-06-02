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

namespace webprintDesigner
{
    public partial class UserControlContainer : UserControl
    {
        public UserControlContainer()
        {
            InitializeComponent();
        }

        public void SwitchControl(UserControl newControl,DictionaryManager.Pages P)
        {
            LayoutRoot.Children.Clear();
            if (newControl != null)
            {
                Margin = new Thickness(0);
                Height = newControl.Height;
                Width = newControl.Width;
                LayoutRoot.Children.Add(newControl);
            }
            DictionaryManager.ClearOthers(P);
        }
    }
}
