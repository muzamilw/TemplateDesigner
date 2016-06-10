using PDFlib_dotnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateDesignerModelV2;

namespace TemplateDesignerV2.services
{
    public class ClippingPathService
    {
        public void generateClippingPaths(string path, List<TemplateObjects> lstObjs,string outputPath,double width, double height)
        {
            PDFlib p;
            int image;
            //float width, height;

            // This is where font/image/PDF input files live. Adjust as necessary.
            // string searchpath = "/";

            p = new PDFlib();

            try
            {
                // This means we must check return values of load_font() etc.
                p.set_option("errorpolicy=return");

                // Set the search path for fonts and PDF files 
                //   p.set_option("SearchPath={{" + searchpath + "}}");

                if (p.begin_document(outputPath, "") == -1)
                {
                    Console.WriteLine("Error: {0}\n", p.get_errmsg());
                    return;
                }
                var oldDoc = p.open_pdi_document(path,"");
                var oldPage = p.open_pdi_page(oldDoc,1,"");

                p.begin_page_ext(width, height, "");    
                p.fit_pdi_page(oldPage,0,0,"");
                p.close_pdi_page(oldPage);

                    // p.set_info("Creator", "image.cs");
                //  p.set_info("Author", "Rainer Schaaf");
                //  p.set_info("Title", "image sample (.NET/C#)");
                foreach (var obj in lstObjs)
                {
                    string imgName  = obj.ContentString.Replace("__clip_mpc.png",".jpg");
                   // imgName = imgName.Replace("./", "");
                    string imagefile = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/" + imgName;
                    image = p.load_image("auto", imagefile, "");

                    if (image == -1)
                    {
                        Console.WriteLine("Error: {0}\n", p.get_errmsg());
                        return;
                    }
                    var posY =height- obj.PositionY - obj.MaxHeight  ;
                    p.fit_image(image, (float)obj.PositionX, (float)posY, "boxsize={" + obj.MaxWidth + " " + obj.MaxHeight + "} " +
            "fitmethod=entire");

                    p.close_image(image);
                }
                // dummy page size, will be adjusted by p.fit_image() 
                //   p.begin_page_ext(595, 841, "");
                //   p.setcolor("fill", "rgb", 1, 0, 0, 0);
                //   p.rect(0, 0, 595, 842);
                //  p.fill();


                p.end_page_ext("");
               
                p.end_document("");
                p.close_pdi_document(oldDoc);
            }

            catch (PDFlibException e)
            {
                // caught exception thrown by PDFlib
                Console.WriteLine("PDFlib exception occurred in image sample:");
                Console.WriteLine("[{0}] {1}: {2}\n", e.get_errnum(),
                        e.get_apiname(), e.get_errmsg());
            }
            finally
            {
                if (p != null)
                {
                    p.Dispose();
                }
            }
        }
    }
}