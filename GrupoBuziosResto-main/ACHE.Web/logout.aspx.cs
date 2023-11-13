using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class logout : WebBasePage {

    protected void Page_Load(object sender, EventArgs e) {
        HttpContext.Current.Session["CurrentUser"] = null;
        HttpContext.Current.Session["ASPNETTraslados"] = null;
        WebBasePage.UsuarioCerro = true;
        Response.Redirect("default.aspx");
    }
}