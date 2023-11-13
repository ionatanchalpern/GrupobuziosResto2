using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ACHE.Model;

public partial class admin_logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        AdminBasePage.UsuarioCerro = true;
        Session["AdminUser"] = null;
        Response.Redirect("~/admin/default.aspx");
    }
}