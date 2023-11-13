using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class changesession : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUser.TipoLogin = Request.QueryString["Tipo"];
        if (CurrentUser.TipoLogin.ToLower() == "traslados")
            Response.Redirect("/traslados-paso1.aspx");
        else
            Response.Redirect("/productos.aspx");
    }
}