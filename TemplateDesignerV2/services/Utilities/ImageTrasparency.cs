﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace TemplateDesignerV2.Services.Utilities
{
    public class ImageTrasparency
    {
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
                Graphics graphics = Graphics.FromImage(bmp);
                ColorMatrix colormatrix = new ColorMatrix();
                colormatrix.Matrix33 = opacityvalue;
                ImageAttributes imgAttribute = new ImageAttributes();
                imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
                graphics.Dispose();
                return bmp;
        }
    }
}