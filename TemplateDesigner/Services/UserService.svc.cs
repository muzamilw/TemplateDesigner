using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using TemplateDesigner.Models;

namespace TemplateDesigner.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    
    [AspNetCompatibilityRequirements(RequirementsMode =AspNetCompatibilityRequirementsMode.Required)]
    public class UserService : IUserService
    {
        public bool IsUserLogined(int Mode)
        {
            bool RetVal = false;
            try
            {
                if (Mode == 2)
                {
                    if (System.Web.HttpContext.Current.Session["UserId"] != null)
                    {
                        if ((int)System.Web.HttpContext.Current.Session["UserId"] != 0)
                        {
                            RetVal = true;
                        }
                    }
                    //else
                    //    System.Web.HttpContext.Current.Session["UserId"] = -1;

                }
                else
                {
                    //if (System.Web.HttpContext.Current.Session["AppGlobalData"] != null)
                    //{
                    //    AppGlobalData AppGlobalData = (AppGlobalData)System.Web.HttpContext.Current.Session["AppGlobalData"];
                    //    if (AppGlobalData != null && AppGlobalData.user != null)
                    //    {
                    //        if (AppGlobalData.user.RowCount > 0)
                    //            RetVal = true;
                    //    }
                    //}

                    RetVal = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return RetVal;
        }
        public bool UserLogin(string UserName, string UserPassword, int Mode)
        {
            bool RetVal = false;
            try
            {

                
                //addmin mode  
                if (Mode == 2)
                {
                    //printdesignBLL.AdminUser.Users objAdminUser = new printdesignBLL.AdminUser.Users();
                    if (UserName == "admin" && UserPassword == "admin")
                    {
                        //printdesignBLL.AdminUser.RoleRights objRR = new printdesignBLL.AdminUser.RoleRights();
                        //printdesignBLL.AdminUser.Rights objRits = new printdesignBLL.AdminUser.Rights();
                        //objRits.LoadAll();
                        //while (!objRits.EOF)
                        //{
                        //    objRR.AddNew();
                        //    objRR.RightID = objRits.RightID;
                        //    objRR.RoleID = -1;
                        //    objRits.MoveNext();
                        //}
                        //System.Web.HttpContext.Current.Session["UserRights"] = objRR;
                        System.Web.HttpContext.Current.Session["MenuItem"] = null;
                        System.Web.HttpContext.Current.Session["CMSAdmin"] = null;
                        System.Web.HttpContext.Current.Session["AdminUsername"] = "Admin";
                        //System.Web.HttpContext.Current.Session["UserId"] = -1;
                        RetVal = true;
                    }
                    else
                    {
                        int UserId = 9;// objAdminUser.getUserIdByUsernamePass(UserName, UserPassword);
                        if (UserId != 0)
                        {
                            //objAdminUser = new printdesignBLL.AdminUser.Users();
                            //objAdminUser.LoadByPrimaryKey(UserId);
                            //printdesignBLL.AdminUser.RoleRights objRR = new printdesignBLL.AdminUser.RoleRights();
                            //objRR.LoadByRoleId(objAdminUser.RoleID);
                            //System.Web.HttpContext.Current.Session["UserRights"] = objRR;
                            System.Web.HttpContext.Current.Session["MenuItem"] = null;
                            System.Web.HttpContext.Current.Session["CMSAdmin"] = null;
                            System.Web.HttpContext.Current.Session["AdminUsername"] = UserName;
                            //System.Web.HttpContext.Current.Session["UserId"] = UserId;
                            RetVal = true;
                        }
                    }
                }
                    //user mode
                else
                {
                    //if (System.Web.HttpContext.Current.Session["AppGlobalData"] != null)
                    //{
                    //    AppGlobalData AppGlobalData = (AppGlobalData)System.Web.HttpContext.Current.Session["AppGlobalData"];
                    //    if (AppGlobalData != null)
                    //    {
                    //        printdesignBLL.Customers.Users objUser = new printdesignBLL.Customers.Users();
                    //        objUser.LoadByUsernameAndPass(UserName, UserPassword);
                    //        if (objUser.RowCount > 0)
                    //        {
                    //            AppGlobalData.user = objUser;
                    //            if (!objUser.IsColumnNull("CountryID") && objUser.CountryID != 0)
                    //            {
                    //                AppGlobalData.CountryID = objUser.CountryID;
                    //            }
                    //            printdesignBLL.CMS.WebSites.WebSiteUrls oWebUrls = new printdesignBLL.CMS.WebSites.WebSiteUrls();
                    //            oWebUrls.LoadByCustomerId(objUser.OfficeID);
                    //            if (oWebUrls.RowCount > 0 && !oWebUrls.IsColumnNull("WebSiteId"))
                    //            {
                    //                System.Web.HttpContext.Current.Session["WebSiteId"] = oWebUrls.WebSiteId;
                    //            }

                    //            AppGlobalData.WebUrlCustomerId = (!objUser.IsColumnNull("OfficeID")) ? objUser.OfficeID : 0;

                    //            if (AppGlobalData.WebUrlCustomerId == 0)
                    //                AppGlobalData.ISCooperateCustomer = false;
                    //            else
                    //                AppGlobalData.ISCooperateCustomer = true;
                    //            printdesignBLL.Countries country = new printdesignBLL.Countries();
                    //            country.LoadByPrimaryKey(AppGlobalData.CountryID);

                    //            printdesignBLL.Currencies cur = new printdesignBLL.Currencies();
                    //            cur.LoadByPrimaryKey(country.CurrencyID);

                    //            AppGlobalData.Currencies = cur;
                    //            System.Web.HttpContext.Current.Session["AppGlobalData"] = AppGlobalData;
                    //            RetVal = true;
                    //        }
                    //    }
                    //}
                    RetVal = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public List<UserImages> GetUserImages(int Mode)
        {
            List<UserImages> lstUserImages = new List<UserImages>();
            try
            {
                bool IsValidUsr = false;
                string imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/UserData/");
                //string imgPath = System.Web.HttpContext.Current.Server.MapPath("~/Designer/UserData/");
                string UserNm = "";
                Uri objUri = new Uri(imgPath);
                //Uri objUri = new Uri(System.Web.HttpContext.Current.Request.Url, "~/Designer/UserData/");
                if (Mode == 2)
                {
                    if (System.IO.Directory.Exists(imgPath))
                    {
                        imgPath += "Admin\\";
                        if (!System.IO.Directory.Exists(imgPath))
                        {
                            System.IO.Directory.CreateDirectory(imgPath);
                        }
                        imgPath += "Images" + "\\";
                        if (!System.IO.Directory.Exists(imgPath))
                        {
                            System.IO.Directory.CreateDirectory(imgPath);
                        }
                        
                        objUri = new Uri(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/UserData/Admin/Images/"));
                        IsValidUsr = true;
                        UserNm = "Admin";
                    }
                }
                else
                {
                    if (System.Web.HttpContext.Current.Session["AppGlobalData"] != null)
                    {
                        AppGlobalData AppGlobalData = (AppGlobalData)System.Web.HttpContext.Current.Session["AppGlobalData"];
                        if (AppGlobalData != null && AppGlobalData.user != null)
                        {
                            if (AppGlobalData.user != null && AppGlobalData.user.UserID != null && AppGlobalData.user.Username != null)
                            {

                                if (System.IO.Directory.Exists(imgPath))
                                {
                                    imgPath += AppGlobalData.user.Username + "\\";
                                    if (!System.IO.Directory.Exists(imgPath))
                                    {
                                        System.IO.Directory.CreateDirectory(imgPath);
                                    }
                                    imgPath += "Images" + "\\";
                                    if (!System.IO.Directory.Exists(imgPath))
                                    {
                                        System.IO.Directory.CreateDirectory(imgPath);
                                    }
                                    objUri = new Uri(System.Web.HttpContext.Current.Request.Url, "../AppData/UserData/" + AppGlobalData.user.Username + "/Images/");
                                    IsValidUsr = true;
                                    //string imgPath2 = System.Configuration.ConfigurationSettings.AppSettings["SiteUrl"] + "AppData/UserData/" + AppGlobalData.user.Username + "/Images/";
                                    UserNm = AppGlobalData.user.Username;
                                }
                            }
                        }
                    }
                }
                if (IsValidUsr && UserNm != "")
                {
                    foreach (string ImgFilePath in System.IO.Directory.GetFiles(imgPath, "*.jpg"))
                    {
                        System.IO.FileAttributes attributes = System.IO.File.GetAttributes(ImgFilePath);
                        if ((attributes & System.IO.FileAttributes.Hidden) != System.IO.FileAttributes.Hidden)
                        {
                            System.Drawing.Image objImage = System.Drawing.Image.FromFile(ImgFilePath);
                            string ImgName = System.IO.Path.GetFileName(ImgFilePath);
                            lstUserImages.Add(new UserImages { ImageName = ImgName, ImageAbsolutePath = objUri.OriginalString + ImgName, ImageRelativePath = "/Designer/UserData/" + UserNm + "/Images/" + ImgName, ImageWidth = objImage.Width, ImageHeight = objImage.Height });
                            objImage.Dispose();
                        }
                    }
                    foreach (string ImgFilePath in System.IO.Directory.GetFiles(imgPath, "*.png"))
                    {
                        System.IO.FileAttributes attributes = System.IO.File.GetAttributes(ImgFilePath);
                        if ((attributes & System.IO.FileAttributes.Hidden) != System.IO.FileAttributes.Hidden)
                        {
                            System.Drawing.Image objImage = System.Drawing.Image.FromFile(ImgFilePath);
                            string ImgName = System.IO.Path.GetFileName(ImgFilePath);
                            lstUserImages.Add(new UserImages { ImageName = ImgName, ImageAbsolutePath = objUri.OriginalString + ImgName, ImageRelativePath = "/Designer/UserData/" + UserNm + "/Images/" + ImgName, ImageWidth = objImage.Width, ImageHeight = objImage.Height });
                            objImage.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstUserImages;
        }
       
    }
}
