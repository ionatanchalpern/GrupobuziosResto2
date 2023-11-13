using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class _default : WebBasePage {

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            cargarRestaurantes();
            if (CurrentUser != null) {
                if (CurrentUser.TipoLogin.ToLower() == "traslados")
                    Response.Redirect("/traslados-paso1.aspx");
                Session["CurrentUser"] = CurrentUser;
            }
        }
    }

    private void cargarRestaurantes() {
        using (var dbContext = new ACHEEntities()) {
            var restaurantes = dbContext.Restaurantes.Where(x => x.Activo)
                .Select(x => new {
                    IDRestaurant = x.IDRestaurant,
                    Nombre = x.Nombre,
                    Logo = "files/logos/" + x.Logo,
                    Observaciones = x.Observaciones,
                    Direccion = x.Ciudad,
                }).ToList();
            if (restaurantes != null) {
                rptRestaurantes.DataSource = restaurantes;
                rptRestaurantes.DataBind();
            }
        }
    }
}