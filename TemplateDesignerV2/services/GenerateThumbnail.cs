using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace TemplateDesignerV2.Services
{
    public class GenerateThumbnail
    {
        public static void GenerateThumbNails(string sourcefile)
        {
            int ThumbnailSizeWidth = 98;
            int ThumbnailSizeHeight = 98;
            //int width = 98;
            string ext = Path.GetExtension(sourcefile);
            string[] results = sourcefile.Split(new string[] { ext }, StringSplitOptions.None);
            string destinationfile = results[0] + "_thumb" + ext;
            System.Drawing.Image image = null;
            Bitmap bmp = null;
            try
            {
                if (File.Exists(sourcefile))
                {
                    using (image = System.Drawing.Image.FromFile(sourcefile))
                    {
                        int srcWidth = image.Width;
                        int srcHeight = image.Height;
                        int thumbWidth;//= width;
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
    }
}