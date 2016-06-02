using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using TemplateDesignerModelTypesV2;
using LinqKit;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using System.Drawing;
using System.Net;
using SD = System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace TemplateDesignerV2.Services
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file


    public class imageSvc
    {

        [WebGet(UriTemplate = "{productID}")]

        public Stream GetProductBackgroundImages(string productID)
        {

            int productId = Convert.ToInt32(productID);
            //return null;

            try
            {
                if (productId != 0)
                {

                    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                    {

                        db.ContextOptions.LazyLoadingEnabled = false;
                        //printdesignBLL.Products.ProductBackgroundImages objBackground = new printdesignBLL.Products.ProductBackgroundImages();
                        //objBackground.LoadByProductId(ProductId);

                        var backgrounds = db.TemplateBackgroundImages.Where(g => g.ProductID == productId).ToList();

                      
                        string imgPath = System.AppDomain.CurrentDomain.BaseDirectory + ("Designer\\Products\\");
                        string imgUrl = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                        
                        Uri objUri = new Uri(System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/"));

                        foreach (var objBackground in backgrounds)
                        {
                            string url1 = Path.Combine(imgUrl,objBackground.ImageName);
                            if (objBackground.ImageName != null && objBackground.ImageName != "")
                            {
                                //if (System.IO.File.Exists(url1))
                                //{
                                    //string ImgExt = System.IO.Path.GetExtension(objBackground.ImageName).ToLower();
                                    //if (ImgExt == ".jpg" || ImgExt == ".png")
                                    //{

                                    //lstProductBackground.Add(new PrintFlow.Model.Products.ProductBackgroundImages { BackgroundImageName = ((!objBackground.IsColumnNull("Name")) ? objBackground.Name : ""), 

                                    //objBackground.BackgroundImageAbsolutePath = objUri.OriginalString + objBackground.ImageName, 
                                    //ImgRelativePath = new Uri(@"Designer/Products/" + objBackground.ImageName, UriKind.Relative), 
                                    //BackgroundImageRelativePath = "Designer/Products/" + objBackground.ImageName });

                                    //objBackground.BackgroundImageAbsolutePath = objUri.OriginalString + objBackground.ImageName;
                                    //}
                                    objBackground.BackgroundImageRelativePath = "Designer/Products/" + objBackground.ImageName;
                               // }
                            }

                        }
                        JsonSerializerSettings oset = new JsonSerializerSettings();


                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                        return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(backgrounds.ToList(), Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
                      
                    }
                }
            }
            catch (Exception ex)
            {
              //  AppCommon.LogException(ex);
                throw ex;
                //throw new Exception(ex.ToString());
            }

            return null;
        }

        [WebGet(UriTemplate = "{productID},{ImageID}" , ResponseFormat = WebMessageFormat.Json)  ]


        //function to delete background image from Database and from physical location 

        public string DeleteProductBackgroundImage(string productID, string ImageID)
		{
			try
			{
                int BackgroundImageID = Convert.ToInt32(ImageID);
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    
                    var obj = db.TemplateBackgroundImages.Where(g => g.ID == BackgroundImageID).Single();
                    if (obj != null)
                    {
                        string sfilePath = System.Web.Hosting.HostingEnvironment.MapPath("/templatedesignerv2/designer/products/" + obj.ImageName);
                        string imURL =  "Designer/Products/" + obj.ImageName;

                        var objImgPer = db.ImagePermissions.Where(i=>i.ImageID == BackgroundImageID).ToList();
                        foreach (var oPerm in objImgPer)
                        {
                            db.DeleteObject(oPerm);
                        }

                        db.DeleteObject(obj);

                        //delete the actual image as well
                        if (System.IO.File.Exists(sfilePath))
                            System.IO.File.Delete(sfilePath);

                        db.SaveChanges();


                        return imURL;
                    }
                }
                return false.ToString();
				
			}
			catch (Exception ex)
			{
			//	AppCommon.LogException(ex);
				throw ex;
			}
		}
        //mode = 1 for replace exisitng images 
        //mode = 2 for adding new image
        public byte[] Crop(string Img, int Width, int Height, int X, int Y, int mode, string NfileName)
        {
            try
            {
                using (SD.Image OriginalImage = SD.Image.FromFile(Img))
                {
                    using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        [OperationContract, WebInvoke(UriTemplate = "CropImg/{ImgName},{ImgX1},{ImgY1},{ImWidth1},{ImHeight1},{ImProductName},{mode},{objectID}", Method = "GET", BodyStyle = WebMessageBodyStyle.Bare)]
        public string CropImage(string ImgName, string ImgX1, string ImgY1, string ImWidth1, string ImHeight1, string ImProductName, string mode, string objectID)
        {
            try
            {
                
                ImgName = ImgName.Replace("___", "/");
                ImgName = ImgName.Replace("%20", " ");
                string thumbName = "";
                string NewPath = "";
                string ContentString = ImgName;
                string newImgName = Path.GetFileNameWithoutExtension(ImgName);
                string NewImgPath;
                ImgName = System.Web.Hosting.HostingEnvironment.MapPath("~/" + ImgName);
                string imgNameWtihoutExt = Path.GetFileNameWithoutExtension(ImgName);
                thumbName = System.Web.Hosting.HostingEnvironment.MapPath("~/" + imgNameWtihoutExt +"_thumb"+ Path.GetExtension(ImgName));
               // byte[] CropImage = Crop(ImgName, Convert.ToInt32(ImWidth1), Convert.ToInt32(ImHeight1), Convert.ToInt32(ImgX1), Convert.ToInt32(ImgY1));

                using (SD.Image OriginalImage = SD.Image.FromFile(ImgName))
                {
                    using (SD.Bitmap bmp = new SD.Bitmap(Convert.ToInt32(ImWidth1), Convert.ToInt32(ImHeight1)))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Convert.ToInt32(ImWidth1), Convert.ToInt32(ImHeight1)), Convert.ToInt32(ImgX1), Convert.ToInt32(ImgY1), Convert.ToInt32(ImWidth1), Convert.ToInt32(ImHeight1), SD.GraphicsUnit.Pixel);
                            //MemoryStream ms = new MemoryStream();
                           // bmp.Save(ms, OriginalImage.RawFormat);
                          //  return ms.GetBuffer();


                            string fname = Path.GetFileNameWithoutExtension(ImgName);
                            string ext = Path.GetExtension(ImgName).ToLower();
                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int numIterations = 0;
                            numIterations = rand.Next(1, 100);
                            string bgImgName = ImProductName + "/" + newImgName + numIterations + ext;
                            NewImgPath = "./Designer/Products/" + ImProductName + "/" + newImgName + numIterations + ext;
                            string NewImgPrdoctPath = ImProductName + "/" + newImgName + numIterations + ext;
                             NewPath = System.Web.Hosting.HostingEnvironment.MapPath("~/" + NewImgPath);
                            //if (mode == "1")
                            //{
                            //    File.Delete(ImgName);
                            //}
                            if (ext == ".jpg")
                            {
                                bmp.Save(NewPath, ImageFormat.Jpeg);
                            }
                            else if (ext == ".png")
                            {
                                bmp.Save(NewPath, ImageFormat.Png);
                            }
                            else if (ext == ".gif")
                            {
                                bmp.Save(NewPath, ImageFormat.Gif);
                            }
                            int pID = Convert.ToInt32(ImProductName);
                            using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                            {
                                if (mode == "1")
                                {
                                    foreach (TemplateObjects c in db.TemplateObjects.Where(g => g.ContentString == ContentString))
                                    {
                                        c.ContentString = NewImgPath;
                                    }
                                    string[] paths = ContentString.Split(new string[] { "Designer/Products/" }, StringSplitOptions.None);
                                    string Comp = paths[paths.Length - 1];
                                    foreach (TemplateBackgroundImages c in db.TemplateBackgroundImages.Where(g => g.ImageName == Comp))
                                    {
                                        c.ImageName = NewImgPrdoctPath;
                                        c.Name = NewImgPrdoctPath;
                                    }
                                }
                                else
                                {
                                    int ToID = Convert.ToInt32(objectID);
                                    foreach (TemplateObjects c in db.TemplateObjects.Where(g => g.ObjectID == ToID))
                                    {
                                        c.ContentString = NewImgPath;
                                    }
                                    var bgImg = new TemplateBackgroundImages();
                                    bgImg.Name = bgImgName;
                                    bgImg.ImageName = bgImgName;
                                    bgImg.ProductID = Convert.ToInt32(ImProductName);

                                    bgImg.ImageWidth = Convert.ToInt32(ImWidth1);
                                    bgImg.ImageHeight = Convert.ToInt32(ImHeight1);

                                    bgImg.ImageType = 2;

                                    db.TemplateBackgroundImages.AddObject(bgImg);
                                }
                                db.SaveChanges();
                            }
                        }
                    }
                    string getGName = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/" + ImProductName + "/" + Path.GetFileNameWithoutExtension(NewPath) + "_thumb" + Path.GetExtension(NewPath));
                    GenerateThumbNail(NewPath, getGName, 98);
                }
                if (mode == "1")
                {
                    if (File.Exists(ImgName))
                        File.Delete(ImgName);
                    if (File.Exists(thumbName))
                        File.Delete(thumbName);
                }
    
                return NewImgPath;
            }
            catch (Exception e)
            {
                throw e;
            }
            //ImgName= ImgName.Replace("___", "/");
            //ImgName = ImgName.Replace("%20", " ");
            //string ContentString = ImgName;
            //string newImgName = Path.GetFileNameWithoutExtension(ImgName);
            //int ImgX = Convert.ToInt32(ImgX1);
            //int ImgY = Convert.ToInt32(ImgY1);
            //int ImgWidth = Convert.ToInt32(ImWidth1);
            //int ImgHeight = Convert.ToInt32(ImHeight1);
            ////bool result = false;
            //string NewImgPath;
            //System.Drawing.Image img = null ;
            //Bitmap bm = null;
            //MemoryStream mm = new MemoryStream();
            //try
            //{
            //    ImgName = System.Web.Hosting.HostingEnvironment.MapPath("~/" + ImgName);

            //    using (img = System.Drawing.Image.FromFile(ImgName))
            //    {

            //        bm = CropImg1(img, ImgX, ImgY, ImgWidth, ImgHeight);
            //    }

            //    string fname = Path.GetFileNameWithoutExtension(ImgName);
            //    string ext = Path.GetExtension(ImgName).ToLower();

            //    //string ImgPath= SavePath + arr[0];
            //    if (ext == ".jpg")
            //    {
            //        bm.Save(mm, ImageFormat.Jpeg);
            //    }
            //    else if (ext == ".png")
            //    {
            //        bm.Save(mm, ImageFormat.Png);
            //    }
            //    else if (ext == ".gif")
            //    {
            //        bm.Save(mm, ImageFormat.Gif);
            //    }


            //    Random rand = new Random((int)DateTime.Now.Ticks);
            //    int numIterations = 0;
            //    numIterations = rand.Next(1, 100);
            //    string bgImgName = ImProductName + "/" + newImgName + numIterations + ext;
            //    NewImgPath = "./Designer/Products/" + ImProductName + "/" + newImgName + numIterations + ext;
            //    string NewImgPrdoctPath = ImProductName + "/" + newImgName + numIterations + ext;
            //    string NewPath = System.Web.Hosting.HostingEnvironment.MapPath("~/" + NewImgPath);
            //    if (mode == "1")
            //    {
            //        File.Delete(ImgName);
            //    }
            //    File.WriteAllBytes(NewPath, mm.GetBuffer());
            //    ImgHeight = bm.Height;
            //    ImgWidth = bm.Width;
            //    //result = true;
            //    int pID = Convert.ToInt32(ImProductName);
            //    using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
            //    {
            //        if (mode == "1")
            //        {
            //            foreach (TemplateObjects c in db.TemplateObjects.Where(g => g.ContentString == ContentString))
            //            {
            //                c.ContentString = NewImgPath;
            //            }
            //            string[] paths = ContentString.Split(new string[] { "Designer/Products/" }, StringSplitOptions.None);
            //            string Comp = paths[paths.Length - 1];
            //            foreach (TemplateBackgroundImages c in db.TemplateBackgroundImages.Where(g => g.ImageName == Comp))
            //            {
            //                c.ImageName = NewImgPrdoctPath;
            //                c.Name = NewImgPrdoctPath;
            //            }
            //        }
            //        else
            //        {
            //            int ToID = Convert.ToInt32(objectID);
            //            foreach (TemplateObjects c in db.TemplateObjects.Where(g => g.ObjectID == ToID))
            //            {
            //                c.ContentString = NewImgPath;
            //            }
            //            var bgImg = new TemplateBackgroundImages();
            //            bgImg.Name = bgImgName;
            //            bgImg.ImageName = bgImgName;
            //            bgImg.ProductID = Convert.ToInt32(ImProductName);

            //            bgImg.ImageWidth = ImgWidth;
            //            bgImg.ImageHeight = ImgHeight;

            //            bgImg.ImageType = 2;

            //            db.TemplateBackgroundImages.AddObject(bgImg);
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //finally
            //{
            //    if (img != null)
            //    {
            //        img.Dispose();
            //    }
            //    if (bm != null)
            //    {
            //        bm.Dispose();
            //    }
            //    if (mm != null)
            //    {
            //        mm.Dispose();
            //    }
                
            //}
           // return NewImgPath;

        }
        //public Bitmap CropImg1(System.Drawing.Image img, int xSize, int ySize, int wd, int ht)
        //{
        //    Bitmap bImg  = null;
        //    Graphics grImg  = null;
        //    try
        //    {

        //        if (img.Width < wd)
        //            wd = img.Width;
        //        if (img.Height < ht)
        //            ht = img.Height;
        //        ImageFormat fmt = img.RawFormat;
        //        bImg = new Bitmap(wd, ht, img.PixelFormat);
        //        grImg = Graphics.FromImage(bImg);
        //        grImg.SmoothingMode = SmoothingMode.AntiAlias;
        //        grImg.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        grImg.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //        grImg.DrawImage(img, new Rectangle(0, 0, wd, ht), xSize, ySize, wd, ht, GraphicsUnit.Pixel);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        if (img != null)
        //        {
        //            img.Dispose();
        //        }
        //        if (grImg != null)
        //        {
        //            grImg.Dispose();
        //        }
                
        //    }
        //    return bImg;
        //}

       

        [OperationContract, WebInvoke(UriTemplate = "DownloadImg/{ImgName},{TemplateID},{imgType}", Method = "GET", BodyStyle = WebMessageBodyStyle.Bare)]
        public string DownloadImageLocally(string ImgName, string TemplateID,string imgType)
        {
            if (ImgName.Contains("__clip_mpc.png"))
            {
                string imgName  = ImgName.Replace("__clip_mpc.png",".jpg");
                DownloadImageLocally(imgName, TemplateID, imgType);
            }

            int imageType = Convert.ToInt32(imgType);
            System.Drawing.Image objImage = null;
            ImgName = ImgName.Replace("___", "/");
            ImgName = ImgName.Replace("%20", " ");
            ImgName = ImgName.Replace("./", "");
            ImgName = ImgName.Replace("@@",":");
            string NewImgPath = "";
            try
            {
                string designerPath = "http://designerv2.myprintcloud.com/";
                string ImgPath = "";
                if (ImgName.Contains("-999"))
                {

                    if (System.Configuration.ConfigurationManager.AppSettings["Designerv2Path"] != null)
                    {
                        designerPath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Designerv2Path"]);
                    }
                    if (!ImgName.Contains(designerPath))
                    {
                        ImgPath = designerPath + ImgName;
                    }
                    else
                    {
                        ImgPath = ImgName;
                    }

                }
                else
                {
                    if (!ImgName.Contains("DesignEngine"))
                    {
                        ImgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/" + ImgName);
                    }
                    else
                    {
                        string[] arr = ImgName.Split(new string[] { "DesignEngine" }, StringSplitOptions.None);
                        ImgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/" +arr[ arr.Length-1]);
                    }
                }
                string[] fileName = ImgName.Split(new string[] { "/" }, StringSplitOptions.None);
                string pathToDownload = HttpContext.Current.Server.MapPath("~/designer/") + "products\\" + TemplateID.ToString() + "/" + fileName[fileName.Length - 1];
                DownloadFile(ImgPath, pathToDownload);
                NewImgPath = "./Designer/Products/" + TemplateID + "/" + fileName[fileName.Length - 1];
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    db.ContextOptions.LazyLoadingEnabled = false;
                    string Imname = TemplateID + "/" + fileName[fileName.Length - 1];
                    int tID = Convert.ToInt32(TemplateID);
                    var backgrounds = db.TemplateBackgroundImages.Where(g => g.ImageName == Imname && g.ImageType == imageType && g.ProductID == tID).SingleOrDefault();

                    if (backgrounds != null)
                    {


                        //result = backgrounds.ID.ToString();

                    }
                    else
                    {
                        int ImageWidth =0;
                        int ImageHeight =0;
                        if (!Path.GetExtension(fileName[fileName.Length - 1]).Contains("svg"))
                        {
                            using (objImage = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/designer/") + "products\\" + TemplateID.ToString() + "/" + fileName[fileName.Length - 1]))
                            {
                                ImageWidth = objImage.Width;
                                ImageHeight = objImage.Height;
                                objImage.Dispose();
                            }
                        }
                        var bgImg = new TemplateBackgroundImages();
                        bgImg.Name = TemplateID + "/" + fileName[fileName.Length - 1];
                        bgImg.ImageName = TemplateID + "/" + fileName[fileName.Length - 1];
                        bgImg.ProductID = Convert.ToInt32(TemplateID);

                        bgImg.ImageWidth = ImageWidth;
                        bgImg.ImageHeight = ImageHeight;

                        bgImg.ImageType = imageType;

                        db.TemplateBackgroundImages.AddObject(bgImg);

                        db.SaveChanges();
                        // generate thumbnail 
                        string ext = Path.GetExtension(fileName[fileName.Length - 1]);
                        if (!ext.Contains("svg"))
                        {
                            Services.imageSvc objSvc = new Services.imageSvc();
                            string sourcePath = pathToDownload;
                            //string ext = Path.GetExtension(uploadPath);
                            string[] results = sourcePath.Split(new string[] { ext }, StringSplitOptions.None);
                            string destPath = results[0] + "_thumb" + ext;
                            objSvc.GenerateThumbNail(sourcePath, destPath, 98);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (objImage != null)
                    objImage.Dispose();
            }
            return NewImgPath;

        }
        private bool DownloadFile(string SourceURL, string DestinationBasePath)
        {
            if (!File.Exists(DestinationBasePath))
            {
                Stream stream = null;
                MemoryStream memStream = new MemoryStream();
                try
                {
                    WebRequest req = WebRequest.Create(SourceURL);
                    WebResponse response = req.GetResponse();
                    if (response != null)
                    {
                        stream = response.GetResponseStream();


                        byte[] downloadedData = new byte[0];

                        byte[] buffer = new byte[1024];

                        //Get Total Size
                        int dataLength = (int)response.ContentLength;


                        while (true)
                        {
                            //Try to read the data
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                                break;
                            else
                                memStream.Write(buffer, 0, bytesRead);
                        }

                        File.WriteAllBytes(DestinationBasePath, memStream.ToArray());
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    if (stream != null)
                        stream.Close();

                    if (memStream != null)
                        memStream.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void GenerateThumbNail(string sourcefile, string destinationfile, int width)
        {
            System.Drawing.Image image =  null;
            int ThumbnailSizeWidth = 98;
            int ThumbnailSizeHeight = 98;
            Bitmap bmp = null;
            try
            {

                using (image = System.Drawing.Image.FromFile(sourcefile))
                {
                    int srcWidth = image.Width;
                    int srcHeight = image.Height;
                    int thumbWidth = width;
                    int thumbHeight;
                    float WidthPer, HeightPer;


                    int NewWidth, NewHeight;

                    if (srcWidth > srcHeight)
                    {
                        NewWidth = ThumbnailSizeWidth;
                        WidthPer = (float)ThumbnailSizeWidth / srcWidth;
                        NewHeight = Convert.ToInt32(srcHeight * WidthPer);
                    }
                    else
                    {
                        NewHeight = ThumbnailSizeHeight;
                        HeightPer = (float)ThumbnailSizeHeight / srcHeight;
                        NewWidth = Convert.ToInt32(srcWidth * HeightPer);
                    }
                    //if (srcHeight > srcWidth)
                    //{
                    //    thumbHeight = (srcHeight / srcWidth) * thumbWidth;
                    //    bmp = new Bitmap(thumbWidth, thumbHeight);
                    //}
                    //else
                    //{
                    //    thumbHeight = thumbWidth;
                    //    thumbWidth = (srcWidth / srcHeight) * thumbHeight;
                    //    bmp = new Bitmap(thumbWidth, thumbHeight);
                    //}
                    thumbWidth = NewWidth;
                    thumbHeight = NewHeight;
                    bmp = new Bitmap(thumbWidth, thumbHeight);
                    System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                    gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    System.Drawing.Rectangle rectDestination =
                           new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                    gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                    bmp.Save(destinationfile);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }


        [OperationContract, WebInvoke(UriTemplate = "GenerateThumbnails", Method = "GET", BodyStyle = WebMessageBodyStyle.Bare)]
        public string GenerateThumbnails()
        {
            string status = "Done !";
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    var result = db.TemplateBackgroundImages.ToList();
                    foreach (var obj in result)
                    {
                        try
                        {
                            if (!obj.ImageName.Contains(".svg"))
                            {
                                string imgUrl = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                                db.sp_ResizeImages((imgUrl + "/" + obj.ImageName));
                            }
                        }
                        catch (Exception e)
                        {
                            status += " " + obj.ID.ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;

        }
        [OperationContract, WebInvoke(UriTemplate = "GenerateThumbnailsV2", Method = "GET", BodyStyle = WebMessageBodyStyle.Bare)]
        public string GenerateThumbnailsv2()
        {
            string status = "Done !";
            try
            {
                using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
                {
                    var result = db.TemplateBackgroundImages.ToList();
                    foreach (var obj in result)
                    {
                        try
                        {
                            if (!obj.ImageName.Contains(".svg"))
                            {
                                string imgUrl = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                                GenerateThumbnail.GenerateThumbNails(imgUrl + "/" + obj.ImageName);
                                // db.sp_ResizeImages((imgUrl + "/" + obj.ImageName));
                            }
                        }
                        catch (Exception e)
                        {
                            status += " " + obj.ID.ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;

        }
    }
}