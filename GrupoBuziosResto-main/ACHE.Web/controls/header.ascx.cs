using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class controls_header : System.Web.UI.UserControl {

    protected void Page_Load(object sender, EventArgs e) {
        if (Session["CurrentUser"] != null) {
            WebUser user = (WebUser)Session["CurrentUser"];
            litUsuario.Text = user.Nombre;

            pnlLogin.Visible = true;
            liComprar.Visible = true;

            if (user.TipoLogin == "resto") {
                liHistorialTraslados.Visible = false;
                if (user.TipoUsuario == "R") {
                    //pnlResto.Visible = 
                    liComprar.Visible = false;
                    liMisdatos.Visible = false;
                    liHistorial.Visible = false;
                    liHistorialCupones.Visible = true;
                }
                else {
                    //pnlOperador.Visible = true;
                    liHistorialCupones.Visible = false;
                    liValidar.Visible = liValidar2.Visible = false;

                }
            }
            else {
                liComprar.Visible = liValidar.Visible = liValidar2.Visible = liMisdatos.Visible = liMapa.Visible = liHistorial.Visible = liHistorialCupones.Visible = false;
                liHistorialTraslados.Visible = true;

                lnkHome.NavigateUrl = lnkLogo.NavigateUrl = "~/traslados-paso1.aspx";
                imgLogo.ImageUrl = "~/imgs/logo-traslados.png";
            }
        }
        else {
            if (Request.Url.ToString().ToLower().Contains("traslados"))
            {
                lnkHome.NavigateUrl = lnkLogo.NavigateUrl = "~/traslados-paso1.aspx";
                imgLogo.ImageUrl = "~/imgs/logo-traslados.png";
                liMapa.Visible = false;
            }
            
            pnlNoLogin.Visible = true;
            liComprar.Visible = false;

            liComprar.Visible = false;
            liMisdatos.Visible = false;
            liValidar.Visible = liValidar2.Visible = false;


        }
    }
}