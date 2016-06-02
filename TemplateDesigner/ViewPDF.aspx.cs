using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TemplateDesigner
{

    public partial class Admin_Products_ViewPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (Request.QueryString["OrderDetailID"] != null)
                {
                    ////PrintFlow.BLL.Orders.OrderDetails orderDetail = new PrintFlow.BLL.Orders.OrderDetails();
                    ////orderDetail.LoadByPrimaryKey(Convert.ToInt32(Request.QueryString["OrderDetailID"]));
                    ////byte[] obytes = orderDetail.AttachmentFileClient;
                    ////Response.Clear();
                    //////Response.AddHeader("Content-Length", obytes.Length.ToString());
                    ////Response.ContentType = "application/pdf";
                    ////Response.BinaryWrite(obytes);
                }
                else
                {
                    if (Request.QueryString["ViewSide"] != null && Request.QueryString["ViewSide"].ToString() != "" && Request.QueryString["ViewSide"].ToString() == "2")
                    {
                        if (Session["PDFFileSide2Admin"] != null)
                        {
                            byte[] obytes = (byte[])(Session["PDFFileSide2Admin"]);
                            if (obytes != null)
                            {
                                Response.Clear();
                                //Response.AddHeader("Content-Length", obytes.Length.ToString());
                                Response.ContentType = "application/pdf";

                                Response.BinaryWrite(obytes);
                            }
                        }
                    }
                    else if (Session["PDFFileAdmin"] != null)
                    {
                        byte[] obytes = (byte[])(Session["PDFFileAdmin"]);
                        if (obytes != null)
                        {
                            Response.Clear();
                            //Response.AddHeader("Content-Length", obytes.Length.ToString());
                            Response.ContentType = "application/pdf";

                            Response.BinaryWrite(obytes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Common.ErrorHandler(this, ex);
            }
            Response.End();
        }
    }
}