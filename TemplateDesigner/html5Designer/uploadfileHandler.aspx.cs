using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using TemplateDesignerModelTypes;

namespace TemplateDesigner.html5Designer
{
    public partial class uploadfileHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region "Get IDs"

        [WebMethod]
        public static string GetIDs()
        {
            //This is strictly here just so you can see how you can grab ID's and make plupload aware of them
            //This is useful for data driven application. Example, need the ID of the user who is uploading a file
            //so you can pull that data out later when the user logs in.

            //default is pipe delimited for js file in function OnGetIDSucceeded
            //IDOfObject|IDOfObject2
            string IDs = "0|0";

            //These IDs could be pulled from anywhere (i.e, Session, DB, XML file)

            IDs = string.Format("{0}|{1}", "10", "11"); //"10" and "11" and filler so you can have data to mess around with

            return IDs;
        }

        #endregion

        #region "Insert Attachment"

        [WebMethod]
        public static void InsertFileRecord(string idOfObject1, string idOfObject2, string fileID, string fileName)
        {
            _InsertAttachment(idOfObject1, idOfObject2, fileID, fileName);
        }

        #endregion

        #region "Private _InsertAttachment"

        private static void _InsertAttachment(string idOfObject1, string idOfObject2, string fileID, string fileName)
        {
            //TODO: Insert into Database as needed, or whatever data store you are using.
            string doSomething = ""; //left here for debugging purposes...delete as needed
            //try
            //{

            //    string imgpath = "";
            //    System.Drawing.Image objImage = System.Drawing.Image.FromFile(imgpath);

            //    int ImageWidth = objImage.Width;
            //    int ImageHeight = objImage.Height;
            //    objImage.Dispose();
            //    using (TemplateDesignerEntities db = new TemplateDesignerEntities())
            //    {
                    //if ((args.FileType == 1 || args.FileType == 2) && !args.IsOverwriting)
                    //{
                        //var bgImg = new TemplateBackgroundImages();
                        //bgImg.ImageName = args.ProductID.ToString() + "/" + fileName;
                        //bgImg.Name = args.ProductID.ToString() + "/" + fileName;
                        //bgImg.ProductID = args.ProductID;

                        //bgImg.ImageWidth = args.ImageWidth;
                        //bgImg.ImageHeight = args.ImageHeight;

                        //bgImg.ImageType = '2';

                        //db.TemplateBackgroundImages.AddObject(bgImg);
                        //db.SaveChanges();
                    //}

            //    }
            //}
            //catch (Exception ex)
            //{
            //    AppCommon.LogException(ex);
            //    throw;
            //}
        }

        #endregion
    }
}