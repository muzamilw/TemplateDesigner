using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSupergoo.ABCpdf6;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;

namespace TemplateDesigner.Services
{
    public class AppCommon
    {
       
       

        //public static string ReplaceObjectContentString(string content, System.Web.HttpContext objSession)
        //{
        //    try
        //    {
        //        if (objSession.Session["GlobalData"] != null)
        //        {

        //            AppGlobalData globalData = (AppGlobalData)objSession.Session["GlobalData"];
        //            if (globalData.user != null)
        //            {

        //                if (globalData.user != null)
        //                {
        //                    if (Regex.Match(content, "#CustomerName#").Success == true)
        //                    {
        //                        if (globalData.user.IsColumnNull("OfficeID") && globalData.user.OfficeID != 0)
        //                        {
        //                            printdesignBLL.Customers.Offices objOffice = new printdesignBLL.Customers.Offices();
        //                            objOffice.LoadByPrimaryKey(globalData.user.OfficeID);
        //                            if (objOffice.RowCount > 0)
        //                            {
        //                                if (!objOffice.IsColumnNull("Name"))
        //                                    content = content.Replace("#CustomerName#", objOffice.Name);
        //                            }
        //                        }
        //                        content = content.Replace("#CustomerName#", ((!globalData.user.IsColumnNull("Billing_Company")) ? globalData.user.Billing_Company : ""));

        //                    }

        //                    //if (Regex.Match(content, "#OfficeName#").Success == true)
        //                    //{
        //                    //    //if (Request.QueryString["OfficeName"] != null)
        //                    //    //{
        //                    //    content = content.Replace("#OfficeName#", Request.QueryString["OfficeName"]);
        //                    //    //}
        //                    //    //else
        //                    //    //{
        //                    //    //    content = content.Replace("#OfficeName#", "");
        //                    //    //}
        //                    //}

        //                    if (Regex.Match(content, "#Name#").Success == true)
        //                    {
        //                        content = content.Replace("#Name#", ((!globalData.user.IsColumnNull("Name")) ? globalData.user.Name : ""));
        //                    }

        //                    if (Regex.Match(content, "#EmailAddress#").Success == true)
        //                    {
        //                        content = content.Replace("#EmailAddress#", ((!globalData.user.IsColumnNull("EmailAddress")) ? globalData.user.EmailAddress : ""));
        //                    }

        //                    if (Regex.Match(content, "#Delivery_Company#").Success == true)
        //                    {
        //                        content = content.Replace("#Delivery_Company#", ((!globalData.user.IsColumnNull("Delivery_Company")) ? globalData.user.Delivery_Company : ""));
        //                    }

        //                    if (Regex.Match(content, "#Delivery_Address1#").Success == true)
        //                    {
        //                        content = content.Replace("#Delivery_Address1#", ((!globalData.user.IsColumnNull("Delivery_Address1")) ? globalData.user.Delivery_Address1 : ""));
        //                    }

        //                    if (Regex.Match(content, "#Delivery_Address2#").Success == true)
        //                    {
        //                        content = content.Replace("#Delivery_Address2#", ((!globalData.user.IsColumnNull("Delivery_Address2")) ? globalData.user.Delivery_Address2 : ""));
        //                    }

        //                    if (Regex.Match(content, "#Delivery_Address3#").Success == true)
        //                    {
        //                        content = content.Replace("#Delivery_Address3#", ((!globalData.user.IsColumnNull("Delivery_Address3")) ? globalData.user.Delivery_Address3 : ""));
        //                    }

        //                    if (Regex.Match(content, "#Delivery_City#").Success == true)
        //                    {
        //                        content = content.Replace("#Delivery_City#", ((!globalData.user.IsColumnNull("Delivery_City")) ? globalData.user.Delivery_City : ""));
        //                    }

        //                    if (Regex.Match(content, "#Delivery_ZipCode#").Success == true)
        //                    {
        //                        content = content.Replace("#Delivery_ZipCode#", ((!globalData.user.IsColumnNull("Delivery_ZipCode")) ? globalData.user.Delivery_ZipCode : ""));
        //                    }

        //                    if (Regex.Match(content, "#Billing_Company#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_Company#", ((!globalData.user.IsColumnNull("Billing_Company")) ? globalData.user.Billing_Company : ""));
        //                    }

        //                    if (Regex.Match(content, "#Billing_NameLine1#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_NameLine1#", ((!globalData.user.IsColumnNull("Billing_NameLine1")) ? globalData.user.Billing_NameLine1 : ""));
        //                    }

        //                    if (Regex.Match(content, "#Billing_NameLine2#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_NameLine2#", ((!globalData.user.IsColumnNull("Billing_NameLine2")) ? globalData.user.Billing_NameLine2 : ""));
        //                    }

        //                    if (Regex.Match(content, "#Billing_Address2#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_Address2#", ((!globalData.user.IsColumnNull("Billing_Address2")) ? globalData.user.Billing_Address2 : ""));
        //                    }

        //                    if (Regex.Match(content, "#Billing_Address3#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_Address3#", ((!globalData.user.IsColumnNull("Billing_Address3")) ? globalData.user.Billing_Address3 : ""));
        //                    }

        //                    if (Regex.Match(content, "#Billing_Address1#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_Address1#", ((!globalData.user.IsColumnNull("Billing_Address1")) ? globalData.user.Billing_Address1 : ""));
        //                    }

        //                    if (Regex.Match(content, "#Billing_City#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_City#", ((!globalData.user.IsColumnNull("Billing_City")) ? globalData.user.Billing_City : ""));
        //                    }

        //                    if (Regex.Match(content, "#Billing_ZipCode#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_ZipCode#", ((!globalData.user.IsColumnNull("Billing_ZipCode")) ? globalData.user.Billing_ZipCode : ""));
        //                    }

        //                    if (Regex.Match(content, "#Delivery_ContactNumber#").Success == true)
        //                    {
        //                        content = content.Replace("#Delivery_ContactNumber#", ((!globalData.user.IsColumnNull("Delivery_ContactNumber")) ? globalData.user.Delivery_ContactNumber : ""));
        //                    }
        //                    if (Regex.Match(content, "#Billing_ContactNumber#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_ContactNumber#", ((!globalData.user.IsColumnNull("Billing_ContactNumber")) ? globalData.user.Billing_ContactNumber : ""));
        //                    }



        //                    if (Regex.Match(content, "#Delivery_JobTitle#").Success == true)
        //                    {
        //                        content = content.Replace("#Delivery_JobTitle#", ((!globalData.user.IsColumnNull("DeliveryJobTitle")) ? globalData.user.DeliveryJobTitle : ""));
        //                    }
        //                    if (Regex.Match(content, "#Billing_JobTitle#").Success == true)
        //                    {
        //                        content = content.Replace("#Billing_JobTitle#", ((!globalData.user.IsColumnNull("BillingJobTitle")) ? globalData.user.BillingJobTitle : ""));
        //                    }
        //                }
        //            }
        //        }
        //        return content;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ReplaceString", ex);
        //    }
        //}

        public static double MMToPoint(double val)
        {
            return val * 2.834645669;
        }

        public static double PointToMM(double val)
        {
            return val / 2.834645669;
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