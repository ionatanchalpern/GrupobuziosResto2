using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class historialCupones : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser == null || CurrentUser.TipoUsuario == "O")
                Response.Redirect("login.aspx");
            else
                cargarHistorial();
        }
    }

    private void cargarHistorial()
    {
        //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        //var random = new Random();
        //var resul = new string(
        //    Enumerable.Repeat(chars, 4)
        //              .Select(s => s[random.Next(s.Length)])
        //            .ToArray());

        using (var dbContext = new ACHEEntities())
        {
            var result = dbContext.PedidosDetalle.Include("Pedidos").Where(x => x.IDRestauranteValidacion == CurrentUser.ID && x.FechaValidacion.HasValue)
               .OrderByDescending(x => x.FechaValidacion).ToList()
               .Select(x => new
               {
                   IDPedido = x.IDPedido,
                   Codigo = Cryptography.Encrypt(x.IDDetalle.ToString()),
                   Tipo = x.Productos.Tipo == "T" ? "Turista" : (x.Productos.Tipo == "P" ? "Premium" : (x.Productos.Tipo=="C")?"Clasico":(x.Productos.Tipo=="M"?"Menores":"Playa")),
                   FechaValidacion = x.FechaValidacion.Value,
                   //Estadia = x.Pedidos.FechaEstadiaDesde.ToString("dd/MM/yyyy") + " - " + x.Pedidos.FechaEstadiaHasta.ToString("dd/MM/yyyy"),
                   Operador = x.Pedidos.Usuarios.Empresa,
                   //NombreRestaurant = x.Restaurantes.Nombre,
                   Total = x.Precio.ToString("N2"),
                   Pasajero = x.Pedidos.Pasajero
               }).ToList();

            rptProductos.DataSource = result;
            rptProductos.DataBind();
        }
    }
}