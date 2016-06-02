﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Svg;


namespace TemplateDesignerV2.Services.Utilities
{
    public class SvgParser
    {
        /// <summary>
        /// The maximum image size supported.
        /// </summary>
        public static Size MaximumSize { get; set; }

        /// <summary>
        /// Converts an SVG file to a Bitmap image.
        /// </summary>
        /// <param name="filePath">The full path of the SVG image.</param>
        /// <returns>Returns the converted Bitmap image.</returns>
        public static Bitmap GetBitmapFromSVG(string filePath,string hexColor)
        {
            SvgDocument document = GetSvgDocument(filePath, hexColor);

            Bitmap bmp = document.Draw();
            return bmp;
        }
        public static byte[] ImageToByteArraybyImageConverter(Bitmap image)
        {
            ImageConverter imageConverter = new ImageConverter();
            byte[] imageByte = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
            return imageByte;
        }
     
        /// <summary>
        /// Gets a SvgDocument for manipulation using the path provided.
        /// </summary>
        /// <param name="filePath">The path of the Bitmap image.</param>
        /// <returns>Returns the SVG Document.</returns>
        public static SvgDocument GetSvgDocument(string filePath,string hexColor)
        {
            SvgDocument document = SvgDocument.Open(filePath);
            //return AdjustSize(document);
            return AdjustColour(document,hexColor);
        }

        /// <summary>
        /// Makes sure that the image does not exceed the maximum size, while preserving aspect ratio.
        /// </summary>
        /// <param name="document">The SVG document to resize.</param>
        /// <returns>Returns a resized or the original document depending on the document.</returns>
        private static SvgDocument AdjustSize(SvgDocument document,string hexColor)
        {
            if (document.Height > MaximumSize.Height)
            {
                document.Width = (int)((document.Width / (double)document.Height) * MaximumSize.Height);
                document.Height = MaximumSize.Height;
            }
            return AdjustColour(document, hexColor) ;
        }

        private static SvgDocument AdjustColour(SvgDocument document,string hexColor)
        {
            if (hexColor != "")
            {
                SvgPaintServer firstColor = null;
                bool canColour = true;
                for(int i = 0; i < document.Children.Count;i++)
                {
                    if (firstColor == null)
                    {
                        if (document.Children[i] is SvgPath)
                        {

                            firstColor = (document.Children[i] as SvgPath).Fill;
                        }
                    }
                    if (document.Children[i] is SvgPath)
                    {
                        if ((document.Children[i] as SvgPath).Fill != firstColor)
                        {
                            canColour = false;
                        }
                    }
                }
                if (canColour)
                {
                    foreach (Svg.SvgElement item in document.Children)
                    {
                        Color color = HexToColor(hexColor);

                        ChangeFill(item, color);
                    }
                }
            }
            return document;
        }
        public static Color HexToColor(string hexColor)
        {
            //Remove # if present
            if (hexColor.IndexOf('#') != -1)
                hexColor = hexColor.Replace("#", "");

            int red = 0;
            int green = 0;
            int blue = 0;

            if (hexColor.Length == 6)
            {
                //#RRGGBB
                red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (hexColor.Length == 3)
            {
                //#RGB
                red = int.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);
            }

            return Color.FromArgb(red, green, blue);
        }
        /// <summary>
        ///  Recursive fill function to change the color of a selected node and all of its children.
        /// </summary>
        /// <param name="element">The current element been resolved.</param>
        /// <param name="sourceColor">The source color to search for.</param>
        /// <param name="replaceColor">The color to be replaced the source color with.</param>
        private static void ChangeFill(SvgElement element, Color hexColor)
        {
            if (element is SvgPath)
            {

                (element as SvgPath).Fill = new SvgColourServer(hexColor);
            }

            if (element.Children.Count > 0)
            {
                foreach (var item in element.Children)
                {
                    ChangeFill(item, hexColor);
                }
            }

        }
    }
}