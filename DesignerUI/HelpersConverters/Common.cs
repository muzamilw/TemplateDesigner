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

namespace webprintDesigner
{
    public class Common
    {
        public static double PointToPixel(double Val)
        {
                return Val * 96 / 72;
        }
        public static  double PixelToPoint(double Val)
        {
                return Val / 96 * 72;
        }

        public static double MMToPoint(double val)
        {
            return val * 2.834645669;
        }

        public static double PointToMM(double val)
        {
            return val / 2.834645669;
        }
    }

    

}
