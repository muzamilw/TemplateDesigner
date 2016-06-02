using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Browser;

namespace webprintDesigner.Converters
{
    public class ConvertToStream : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Stream st = new MemoryStream((byte[])value);
            return new System.Windows.Documents.FontSource(st);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("CnvrtBk");
        }
    }

    public class VisibilityConverter : IValueConverter
    {
        

        public object Convert(object value, Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value != null)
                if (value.GetType() == typeof(bool))
                    if ((bool)value) return Visibility.Visible;
                    else return Visibility.Collapsed;
                else if (value.GetType() == typeof(int))
                    if (((int)value) > 0) return Visibility.Visible;
                    else return Visibility.Collapsed;
                else if (!string.IsNullOrEmpty(value.ToString())) return Visibility.Visible;
                else return Visibility.Collapsed;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var obj = (Visibility)value;

                if (obj == Visibility.Visible)
                    return true;
                if (obj == Visibility.Collapsed)
                    return false;
            }
            return false;
        }
    }

    public class TemplateActionNameConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value != null)
                return "Edit Template";
            else
                return "Create Template";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {

            return "";
        }

       
    }

    public class TemplateStatusConverter : IValueConverter
    {

        public object Convert(object value, Type targetType,
            object parameter,
            CultureInfo culture)
        {

           
           
            if (value != null)
                return  "/webprintDesigner;component/Images/tick.png";
            else
                return "/webprintDesigner;component/Images/error2.png";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {

            return "";
        }


    }

    public class TemplateStatusConverterTooltip : IValueConverter
    {

        public object Convert(object value, Type targetType,
            object parameter,
            CultureInfo culture)
        {



            if (value != null)
                return "Template Available";
            else
                return "Template not Found";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {

            return "";
        }


    }


    public class ImageSourceNoCacheConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                       object parameter,
                       System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            Image img = new Image();
            //Uri uri = new Uri(HtmlPage.Document.DocumentUri, value.ToString());
            Uri uri = new Uri(value.ToString(), UriKind.Relative);
            BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage(uri);

            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            img.Source = bi;

            return img.Source;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ImageSourceNoCacheConverter");
        }
    }


    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string colorString = value as string;
            if (String.IsNullOrEmpty(colorString))
                return new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

            try
            {
                Color c = new Color();
                c.A = 255;
                c.R = byte.Parse(colorString.Substring(0, 2), NumberStyles.HexNumber);
                c.G = byte.Parse(colorString.Substring(2, 2), NumberStyles.HexNumber);
                c.B = byte.Parse(colorString.Substring(4, 2), NumberStyles.HexNumber);

                return new SolidColorBrush(c);
            }
            catch
            {
                return new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //NOTE "value as Color" - doesn't work

            if (value is Color)
            {
                Color c = (Color)value;
                return c.ToString();
            }

            return "000000";
        }
    }
}
