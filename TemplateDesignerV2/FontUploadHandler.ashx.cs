using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TemplateDesignerV2
{
    /// <summary>
    /// Summary description for FontUploadHandler
    /// </summary>
    public class FontUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //TODO: Whatever you would like to do now that you have the ID's of the objects
            //Example, could be creating directories for that ID specifically, etc....        
            string _idOfObject1 = context.Request.Headers["IDofObject1"].ToString();
            string _idOfObject2 = context.Request.Headers["IDofObject2"].ToString();

            int chunk = context.Request["chunk"] != null ? int.Parse(context.Request["chunk"]) : 0;
            string fileName = context.Request["name"] != null ? context.Request["name"] : string.Empty;
            string CustomerName = context.Request["CustomerName"] != null ? context.Request["CustomerName"] : string.Empty;
            // getting product id and product type as parameters
            string pid = context.Request["pid"] != null ? context.Request["pid"] : string.Empty;
            string ptype = context.Request["ptype"] != null ? context.Request["ptype"] : string.Empty;
            string IsCalledFrom = context.Request["IsCalledFrom"] != null ? context.Request["IsCalledFrom"] : string.Empty;
            //TODO: change as needed for your application
            int CustomerID = Convert.ToInt32(CustomerName);
            //var Tc1 = CustomerID;
            if (IsCalledFrom == "1")
            {
                CustomerID = -1;
            } 
            string path = "";
            string CustomerType ="Loc";
            if (CustomerID < 0)
            {
                string role = HttpContext.Current.Request.Cookies["role"].Value;
                if (role != null)
                {
                    if (role == "Designer")
                    {
                        CustomerID = -999;
                    }
                    else if (role == "Customer")
                    {
                        string Customer = HttpContext.Current.Request.Cookies["customerid"].Value;
                        CustomerID = Convert.ToInt32(Customer);
                        CustomerType = "Glo";
                    }
                }
            }

            if (CustomerID == -999 ||CustomerID == null)
            {
                // mpc designers
                path = "Designer/PrivateFonts/FontFace/";
            }
            else
            {
                if (CustomerType == "Glo")
                {
                    path = "Designer/PrivateFonts/FontFace/Glo" + CustomerID.ToString() + "/";
                }
                else
                {
                    path = "Designer/PrivateFonts/FontFace/" + CustomerID.ToString() + "/" ;
                }
            }

            string uploadPath = context.Server.MapPath("~/" + path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            HttpPostedFile fileUpload = context.Request.Files[0];

            using (var fs = new FileStream(Path.Combine(uploadPath, fileName), chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);

                fs.Write(buffer, 0, buffer.Length);

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}