using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pechkin;

public partial class pdftest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGetPdt_Click(object sender, EventArgs e)
    {
        litError.Text = string.Empty;
        try
        {
            string html;

            #region Read the HTML content

            if (string.IsNullOrEmpty(tbUrl.Text.Trim()))
            {
                throw new ApplicationException("The URL is empty.");
            }

            using (var client = new WebClient())
            {
                html = client.DownloadString(tbUrl.Text);
            }

            #endregion

            #region Transform the HTML into PDF

            var pechkin = Factory.Create(new GlobalConfig());
            var pdf = pechkin.Convert(new ObjectConfig()
                                    .SetLoadImages(true).SetZoomFactor(1.5)
                                    .SetPrintBackground(true)
                                    .SetScreenMediaType(true)
                                    .SetCreateExternalLinks(true), html);

            #endregion

            #region Return the pdf file

            Response.Clear();

            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=test.pdf; size={0}", pdf.Length));
            Response.BinaryWrite(pdf);

            Response.Flush();
            Response.End();

            #endregion
        }
        catch (Exception ex)
        {
            litError.Text = string.Format("<div style='padding-top: 20px; color: #ff0000; width:100%;'><strong>Error message:</strong> {0}<hr /><strong>Stack trace:</strong> {1}", ex.Message, ex.StackTrace);
        }
    }
}