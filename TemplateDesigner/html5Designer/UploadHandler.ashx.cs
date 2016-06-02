using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TemplateDesigner.html5Designer
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
            string pID = context.Request["productid"] != null ? context.Request["productid"] : string.Empty;
            string ptype = context.Request["filetype"] != null ? context.Request["filetype"] : string.Empty;
            
            //TODO: change as needed for your application
            string uploadPath = context.Server.MapPath("~/Uploads/");

            HttpPostedFile fileUpload = context.Request.Files[0];
         //  fileName = pID;
          fileName = fileName.Insert(0, string.Format("{0:MM}{0:dd}{0:yyyy}", DateTime.Now));
          fileName += pID;
            // for getting height and width of image by saqib 

            string imgpath = Path.Combine(uploadPath, fileName);
            storeImg(imgpath);
            
            
            
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