using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class productos : WebBasePage {

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (CurrentUser != null && CurrentUser.TipoUsuario == "O") {
                HttpContext.Current.Session["ASPNETShoppingCart"] = null;

                cargarProductos();

                for (int i = 1; i <= 20; i++) {
                    ddlCantCenasTurista.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    //ddlCantCenasPremium.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    ddlCantCenasKids.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    ddlCantCenasClasico.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    //ddlCantCenasPlaya.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    ddlCantCenasBuffetLibre.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    
                    ddlCantPaxTurista.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                   // ddlCantPaxPremium.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    ddlCantPaxKids.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    ddlCantPaxClasico.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    //ddlCantPaxPlaya.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));
                    ddlCantPaxBuffetLibre.Items.Add(new ListItem(i.ToString("#00"), i.ToString()));


                }
            }
            else
                Response.Redirect("login.aspx");
        }
    }

    private void cargarProductos() {
        using (var dbContext = new ACHEEntities()) {
            var HTML_BEBIDA = "<h3>Bebida</h3><p style='margin-bottom: 0px;'>Incluye 1 (una) bebida por persona</p>";
            var HTML_NO_BEBIDA = " <span id='MainContent_spnBebidaNoTurista' class='no-drink'>No Incluye Bebida</span>";
            #region turista
            var htmlTurista = "";
            
            var turista = dbContext.Productos.Where(x => x.Tipo == "T" && x.Activo).FirstOrDefault();
            if (turista != null) {
                pnlTurista.Visible = true;
                if (turista.Entrada != null && turista.Entrada != "" && turista.Entrada != "-")
                    htmlTurista += "<h3>Entrada</h3><p>" + turista.Entrada + "</p>   <hr  />";
                if (turista.PlatoPrincipal != null && turista.PlatoPrincipal != "" && turista.PlatoPrincipal != "-")
                    htmlTurista += "<h3>Plato Principal</h3><p>" + turista.PlatoPrincipal + "</p>   <hr  />";
                if (turista.Postre != null && turista.Postre != "" && turista.Postre != "-")
                    htmlTurista += "<h3>Postre</h3><p>" + turista.Postre + "</p>   <hr  />";
                litAgregarTurista.Text = "<a class='button' onclick=\"AddItem("+turista.IDProducto+", $('#ddlCantCenasTurista').val(), $('#ddlCantPaxTurista').val());\" style='cursor:pointer'>Agregar <img src='/imgs/buy-proc-compra.png'></a>";
                if (turista.IncluyeBebida)
                {
                    htmlTurista += HTML_BEBIDA;
                }
                else
                {
                    htmlTurista += HTML_NO_BEBIDA;
                }
                divInfoTurista.InnerHtml = htmlTurista;
            }
            #endregion
/*
            #region premium
            var htmlPremium = "";
            var premium = dbContext.Productos.Where(x => x.Tipo == "P" && x.Activo).FirstOrDefault();
            if (premium != null) {
                pnlPremium.Visible = true;
                if (premium.Entrada != null && premium.Entrada != "" && premium.Entrada != "-")
                    htmlPremium += "<h3>Entrada</h3><p>" + premium.Entrada + "</p>   <hr/>";
                if (premium.PlatoPrincipal != null && premium.PlatoPrincipal != "" && premium.PlatoPrincipal != "-")
                    htmlPremium += "<h3>Plato Principal</h3><p>" + premium.PlatoPrincipal + "</p>   <hr/>";
                if (premium.Postre != null && premium.Postre != "" && premium.Postre != "-")
                    htmlPremium += "<h3>Postre</h3><p>" + premium.Postre + "</p>   <hr/>";
                litAgregarPremium.Text = "<a class='button' onclick=\"AddItem("+premium.IDProducto+", $('#ddlCantCenasPremium').val(), $('#ddlCantPaxPremium').val());\" style='cursor:pointer'>Agregar <img src='/imgs/buy-proc-compra.png'></a>";
                if (premium.IncluyeBebida)
                {
                    htmlPremium += HTML_BEBIDA;
                }
                else
                {
                    htmlPremium += HTML_NO_BEBIDA;
                }
                divInfoPremium.InnerHtml = htmlPremium;
            }
            #endregion
            */
            /*
            #region playa
            var htmlPlaya = "";
            var playa = dbContext.Productos.Where(x => x.Tipo == "B" && x.Activo).FirstOrDefault();
            if (playa != null)
            {
                pnlPlaya.Visible = true;
                if (playa.Entrada != null && playa.Entrada != "" && playa.Entrada != "-")
                    htmlPlaya += "<h3>Entrada</h3><p>" + playa.Entrada + "</p>   <hr/>";
                if (playa.PlatoPrincipal != null && playa.PlatoPrincipal != "" && playa.PlatoPrincipal != "-")
                    htmlPlaya += "<h3>Plato Principal</h3><p>" + playa.PlatoPrincipal + "</p>   <hr/>";
                if (playa.Postre != null && playa.Postre != "" && playa.Postre != "-")
                    htmlPlaya += "<h3>Postre</h3><p>" + playa.Postre + "</p>   <hr/>";
                litAgregarPlaya.Text = "<a class='button' onclick=\"AddItem(" + playa.IDProducto + ", $('#ddlCantCenasPlaya').val(), $('#ddlCantPaxPlaya').val());\" style='cursor:pointer'>Agregar <img src='/imgs/buy-proc-compra.png'></a>";
                if (playa.IncluyeBebida)
                {
                    htmlPlaya += HTML_BEBIDA;
                }
                else
                {
                    htmlPlaya += HTML_NO_BEBIDA;
                }
                divInfoPlaya.InnerHtml = htmlPlaya;
            }
            #endregion
            */
            #region menores
            var  htmlKids= "";
            var  kids = dbContext.Productos.Where(x => x.Tipo == "M" && x.Activo).FirstOrDefault();
            if (kids != null) {
                pnlMenores.Visible = true;
                if (kids.Entrada != null && kids.Entrada != "" && kids.Entrada != "-")
                    htmlKids += "<h3>Entrada</h3><p>" + kids.Entrada + "</p>   <hr  />";
                if (kids.PlatoPrincipal != null && kids.PlatoPrincipal != "" && kids.PlatoPrincipal != "-")
                    htmlKids += "<h3>Plato Principal</h3><p>" + kids.PlatoPrincipal + "</p>   <hr  />";
                if (kids.Postre != null && kids.Postre != "" && kids.Postre != "-")
                    htmlKids += "<h3>Postre</h3><p>" + kids.Postre + "</p>   <hr  />";
                litAgregarMenores.Text = "<a class='button' onclick=\"AddItem("+kids.IDProducto+", $('#ddlCantCenasKids').val(), $('#ddlCantPaxKids').val());\" style='cursor:pointer'>Agregar <img src='/imgs/buy-proc-compra.png'></a>";
                if (kids.IncluyeBebida)
                {
                    htmlKids += HTML_BEBIDA;
                }
                else
                {
                    htmlKids += HTML_NO_BEBIDA;
                }
                divInfoMenores.InnerHtml = htmlKids;
            }
            #endregion
           
            #region clasico    
            var htmlClasico = "";

            var clasico = dbContext.Productos.Where(x => x.Tipo == "C" && x.Activo).FirstOrDefault();
            if (clasico != null)
            {
                pnlClasico.Visible = true;
                if (clasico.Entrada != null && clasico.Entrada != "" && clasico.Entrada != "-")
                    htmlClasico += "<h3>Entrada</h3><p>" + clasico.Entrada + "</p>   <hr  />";
                if (clasico.PlatoPrincipal != null && clasico.PlatoPrincipal != "" && clasico.PlatoPrincipal != "-")
                    htmlClasico += "<h3>Plato Principal</h3><p>" + clasico.PlatoPrincipal + "</p>   <hr  />";
                if (clasico.Postre != null && clasico.Postre != "" && clasico.Postre != "-")
                    htmlClasico += "<h3>Postre</h3><p>" + clasico.Postre + "</p>   <hr  />";
                litAgregarClasico.Text = "<a class='button' onclick=\"AddItem("+clasico.IDProducto+", $('#ddlCantCenasClasico').val(), $('#ddlCantPaxClasico').val());\" style='cursor:pointer'>Agregar <img src='/imgs/buy-proc-compra.png'></a>";
                if (clasico.IncluyeBebida)
                {
                    htmlClasico += HTML_BEBIDA;
                }
                else
                {
                    htmlClasico += HTML_NO_BEBIDA;
                }
                divinfoClasico.InnerHtml = htmlClasico;
            }
            #endregion

            #region BuffetLibre
            var htmlBuffetLibre = "";

            var buffetLibre = dbContext.Productos.Where(x => x.Tipo == "L" && x.Activo).FirstOrDefault();
            if (buffetLibre != null)
            {
                pnlBuffetLibre.Visible = true;
                if (buffetLibre.Entrada != null && buffetLibre.Entrada != "" && buffetLibre.Entrada != "-")
                    htmlBuffetLibre += "<h3>Entrada</h3><p>" + buffetLibre.Entrada + "</p>   <hr  />";
                if (buffetLibre.PlatoPrincipal != null && buffetLibre.PlatoPrincipal != "" && buffetLibre.PlatoPrincipal != "-")
                    htmlBuffetLibre += "<h3>Plato Principal</h3><p>" + buffetLibre.PlatoPrincipal + "</p>   <hr  />";
                if (buffetLibre.Postre != null && buffetLibre.Postre != "" && buffetLibre.Postre != "-")
                    htmlBuffetLibre += "<h3>Postre</h3><p>" + buffetLibre.Postre + "</p>   <hr  />";
                litAgregarBuffetLibre.Text = "<a class='button' onclick=\"AddItem(" + buffetLibre.IDProducto + ", $('#ddlCantCenasBuffetLibre').val(), $('#ddlCantPaxBuffetLibre').val());\" style='cursor:pointer'>Agregar <img src='/imgs/buy-proc-compra.png'></a>";
                if (buffetLibre.IncluyeBebida)
                {
                    htmlBuffetLibre += HTML_BEBIDA;
                }
                else
                {
                    htmlBuffetLibre += HTML_NO_BEBIDA;
                }
                divInfoBuffetLibre.InnerHtml = htmlBuffetLibre;
            }
            #endregion
        }
    }
}