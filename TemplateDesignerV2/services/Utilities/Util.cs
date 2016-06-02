using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;

namespace TemplateDesignerV2.Services.Utilities
{
    public class Util
    {

        public static double MMToPoint(double val)
        {
            return val * 2.834645669;
        }

        public static double PointToMM(double val)
        {
            return val / 2.834645669;
        }


        public static double PointToPixel(double Val)
        {
            return Val * 96 / 72;
        }
        public static double PixelToPoint(double Val)
        {
            return Val / 96 * 72;
        }


        public static void LogException(Exception ex)
        {
            LogEntry logEntry = new LogEntry();

            logEntry.EventId = 100;
            logEntry.Priority = 2;
            logEntry.Message = ex.ToString();
            //logEntry.Categories.Add("None");


            Logger.Write(logEntry);
        }

    }
}