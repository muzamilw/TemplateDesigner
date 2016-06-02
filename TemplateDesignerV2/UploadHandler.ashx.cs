using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TemplateDesignerV2
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

       public void ProcessRequest(HttpContext context)
        {
            //TODO: Whatever you would like to do now that you have the ID's of the objects
            //Example, could be creating directories for that ID specifically, etc....        
            string _idOfObject1 = context.Request.Headers["IDofObject1"].ToString();
            string _idOfObject2 = context.Request.Headers["IDofObject2"].ToString();

            int chunk = context.Request["chunk"] != null ? int.Parse(context.Request["chunk"]) : 0;
            string fileName = context.Request["name"] != null ? context.Request["name"] : string.Empty;
           
           // getting product id and product type as parameters
           string pid = context.Request["pid"] != null ? context.Request["pid"] : string.Empty;
           string isCalledFrom = context.Request["IsCalledFrom"] != null ? context.Request["IsCalledFrom"] : string.Empty;
           string CustomerID = context.Request["CustomerName"] != null ? context.Request["CustomerName"] : string.Empty;
           string uploadPath = context.Server.MapPath("~/Designer/Products/");
           if (isCalledFrom == "1" || isCalledFrom == "2")
           {
               uploadPath = context.Server.MapPath("~/Designer/Products/UserImgs/" + CustomerID.ToString());
           }
           else if (isCalledFrom == "3" || isCalledFrom == "4")
           {
               uploadPath = context.Server.MapPath("~/Designer/Products/UserImgs/Retail/" + CustomerID.ToString());
           }
           else
           {
               uploadPath = Path.Combine(uploadPath, pid);
           }
           
           
           //uploadPath += pid;
           if (!Directory.Exists(uploadPath))
           {
               Directory.CreateDirectory(uploadPath);
           }
           
            HttpPostedFile fileUpload = context.Request.Files[0];
          //  fileName = fileName.Insert(0,pID);
           //fileName = fileName.Insert(0, string.Format("{0:MM}{0:dd}{0:yyyy}", DateTime.Now));
            // for getting height and width of image by saqib 

            string imgpath = Path.Combine(uploadPath, fileName);
         //   storeImg(imgpath);
            
            
            
            using (var fs = new FileStream(Path.Combine(uploadPath, fileName), chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);

                fs.Write(buffer, 0, buffer.Length);
                
            }

        }

        // for calculating image size 
        public int ImageWidth;
        public int ImageHeight;
        private void ImgSize(string imgpath)
        {
            
            
        }
        private void storeImg(string imgpath)
        {
            
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
