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

namespace webprintDesigner.UserControls
{
    public partial class ColorPicker : UserControl
    {
        webprintDesigner.ColorDataList clrList;
        #region "event declaration"
        public delegate void ColorPickerOkClick_EventHandler(object sender, string ColorHex,int ColorC,int ColorM,int ColorY,int ColorK);
        public event ColorPickerOkClick_EventHandler OnOkClick;
        public delegate void ColorPickerUpdateColor_EventHandler(object sender, string ColorHex, int ColorC, int ColorM, int ColorY, int ColorK);
        public event ColorPickerUpdateColor_EventHandler OnUpdateColor;
        #endregion
        public ColorPicker()
        {
            InitializeComponent();
            clrList = new ColorDataList();
            
        }
        private void UpdateColor()
        {
            int c=0;
            int m=0;
            int y=0;
            int k=0;
            Int32.TryParse(txtCyan.Text, out c);
            Int32.TryParse(txtMagenta.Text, out m);
            Int32.TryParse(txtYellow.Text, out y);
            Int32.TryParse(txtBlack.Text, out k);

           string ClrHex= clrList.getColorHex(c, m, y, k);
            byte a = (byte)(Convert.ToInt32(255));
            byte r = (byte)(Convert.ToUInt32(ClrHex.Substring(1, 2), 16));
            byte g = (byte)(Convert.ToUInt32(ClrHex.Substring(3, 2), 16));
            byte b = (byte)(Convert.ToUInt32(ClrHex.Substring(5, 2), 16));
            recColorView.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            if (OnUpdateColor != null)
                OnUpdateColor(this, ClrHex, c, m, y, k);
        }

        public void SetColor(int c, int m, int y, int k)
        {
            sdrCyan.Value = c;
            sdrMagenta.Value = m;
            sdrYellow.Value = y;
            sdrBlack.Value = k;
        }

        private void sdrCyan_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //lblCyan.Text = e.NewValue.ToString() + " %";
            txtCyan.Text = Math.Round(e.NewValue, 0).ToString();
            //sdrCyan.Value = Math.Round(e.NewValue, 0);
            UpdateColor();
        }

        private void sdrMagenta_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtMagenta.Text = Math.Round(e.NewValue, 0).ToString();
            UpdateColor();
        }

        private void sdrYellow_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtYellow.Text = Math.Round(e.NewValue, 0).ToString();
            UpdateColor();
        }

        private void sdrBlack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtBlack.Text = Math.Round(e.NewValue, 0).ToString();
            UpdateColor();
        }

        private void ColorTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender.GetType().Name == "TextBox")
            {
                TextBox tb = sender as TextBox;
                if (tb.Text == "")
                    tb.Text = "0";
                int val = 0;
                if (int.TryParse(tb.Text, out val))
                {
                    if (val > 100)
                        tb.Text = "100";
                    else if (val < 0)
                        tb.Text = "0";
                }
                else
                    tb.Text = "0";
                if (tb.Name == "txtCyan")
                    sdrCyan.Value = Convert.ToDouble(tb.Text);
                else if (tb.Name == "txtMagenta")
                    sdrMagenta.Value = Convert.ToDouble(tb.Text);
                else if (tb.Name == "txtYellow")
                    sdrYellow.Value = Convert.ToDouble(tb.Text);
                else if (tb.Name == "txtBlack")
                    sdrBlack.Value = Convert.ToDouble(tb.Text);

            }
        }

        private void ColorTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender.GetType().Name == "TextBox")
            {
                //MessageBox.Show(e.Handled.ToString());
                if (e.Key >= Key.D0 && e.Key <= Key.D9)
                {
                    e.Handled = false;
                }
                else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                {
                    e.Handled = false;
                }
                else if (e.Key == Key.Back || e.Key == Key.Enter || e.Key == Key.Escape || e.Key == Key.Delete || e.Key == Key.End || e.Key == Key.Home || e.Key == Key.Insert || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.PageDown || e.Key == Key.PageUp || e.Key == Key.Tab)
                {
                    e.Handled = false;
                }
                else
                    e.Handled = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent.GetType().Name == "PopupWin")
            {
                PrintFlow.SilverlightControls.PopupWin pw = this.Parent as PrintFlow.SilverlightControls.PopupWin;
                pw.IsOpened = false;
            }
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent.GetType().Name == "PopupWin")
            {
                PrintFlow.SilverlightControls.PopupWin pw = this.Parent as PrintFlow.SilverlightControls.PopupWin;
                pw.IsOpened = false;
            }
            if (OnOkClick != null)
            {
                int c = 0;
                int m = 0;
                int y = 0;
                int k = 0;
                Int32.TryParse(txtCyan.Text, out c);
                Int32.TryParse(txtMagenta.Text, out m);
                Int32.TryParse(txtYellow.Text, out y);
                Int32.TryParse(txtBlack.Text, out k);
                string ClrHex = clrList.getColorHex(c, m, y, k);
                OnOkClick(this, ClrHex, c, m, y, k);
            }
        }
    }
}
