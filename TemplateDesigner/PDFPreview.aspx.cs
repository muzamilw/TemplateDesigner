using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TemplateDesigner
{
    public partial class PDFPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSide.Visible = false;
            if (Session["PDFFileSide2Admin"] != null)
            {
                btnSide.Visible = true;
                if (!IsPostBack)
                {
                    if (Request.QueryString["ViewSide"] != null && Request.QueryString["ViewSide"].ToString() != "" && Request.QueryString["ViewSide"].ToString() == "2")
                    {
                        btnSide.Text = "Preview Side 1";
                        lblView.Text = "Showing Side 2";
                    }
                    else
                    {
                        btnSide.Text = "Preview Side 2";
                        lblView.Text = "Showing Side 1";
                    }
                }
            }
            else
            {
                lblView.Visible = false;

            }
        }

        protected void btnSide_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ViewSide"] != null && Request.QueryString["ViewSide"].ToString() != "" && Request.QueryString["ViewSide"].ToString() == "2")
                Response.Redirect("PdfPreview.aspx", false);
            else
                Response.Redirect("PdfPreview.aspx?ViewSide=2", false);
        }

        protected void btnSide_Click1(object sender, EventArgs e)
        {

        }
    }
}