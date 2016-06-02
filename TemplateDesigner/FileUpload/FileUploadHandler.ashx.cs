using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading;
using System.Text;
//using TemplateDesigner.Models;
using TemplateDesigner.Services;
using TemplateDesignerModelTypes;


namespace TemplateDesigner
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FileUploadHandler : IHttpHandler
    {
        private HttpContext ctx;
        public void ProcessRequest(HttpContext context)
        {
            try
            {


                ctx = context;
                //string uploadPath = context.Server.MapPath("~/Upload");
                FileUploadProcess fileUpload = new FileUploadProcess();
                fileUpload.FileUploadCompleted += new FileUploadCompletedEvent(fileUpload_FileUploadCompleted);
                fileUpload.ProcessRequest(context);
            }
            catch (Exception ex)
            {
                AppCommon.LogException(ex);
                throw ex;
            }
        }

        void fileUpload_FileUploadCompleted(object sender, FileUploadCompletedEventArgs args)
        {
            try
            {


                using (TemplateDesignerEntities db = new TemplateDesignerEntities())
                { 
                    if ((args.FileType == 1 || args.FileType == 2) &&  !args.IsOverwriting)
                    {
                        var bgImg = new TemplateBackgroundImages();
                        bgImg.ImageName = args.ProductID.ToString() + "/" + args.FileName;
                        bgImg.Name = args.ProductID.ToString() + "/" + args.FileName;
                        bgImg.ProductID = args.ProductID;

                        bgImg.ImageWidth = args.ImageWidth;
                        bgImg.ImageHeight = args.ImageHeight;

                        bgImg.ImageType = args.FileType;

                        db.TemplateBackgroundImages.AddObject(bgImg);
                        db.SaveChanges();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                AppCommon.LogException(ex);
                throw;
            }
            
            //FileInfo fi = new FileInfo(args.FilePath);
            //string targetFile = Path.Combine(fi.Directory.FullName, args.FileName);
            //if (File.Exists(targetFile))
            //    File.Delete(targetFile);
            //fi.MoveTo(targetFile);
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
