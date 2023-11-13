using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using ACHE.Model;
using System.Configuration;

public partial class historial_traslados : WebBasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser == null || CurrentUser.TipoUsuario == "R")
                Response.Redirect("login-traslados.aspx");
            else
            {
                var dtHoy = DateTime.Now.AddYears(-1);

                using (var dbContext = new ACHEEntities())
                {
                    var result = dbContext.PedidosTraslado
                        .Include("PasajerosPorPedidoTraslado")
                        .Where(x => x.IDUsuario == CurrentUser.ID
                            && x.FechaAlta >= dtHoy
                            && x.Estado.ToLower() != "cancelado" && x.Estado.ToLower() != "borrador")
                       .OrderByDescending(x => x.FechaAlta).ToList()
                       .Select(x => new
                       {
                           IDPedidoTraslado = x.IDPedidoTraslado,
                           FechaAlta = x.FechaAlta.ToString("dd/MM/yyyy"),
                           Estadia = (x.FechaIda.HasValue ? x.FechaIda.Value.ToString("dd/MM/yyyy") : "") + " - " + (x.FechaVuelta.HasValue ? x.FechaVuelta.Value.ToString("dd/MM/yyyy") : ""),
                           Pasajero = x.PasajerosPorPedidoTraslado.FirstOrDefault().Nombre,
                           File = x.NroFile,
                           Total = x.Total.ToString("N2"),
                           Links = "<a href='javascript:modificarPedido(" + x.IDPedidoTraslado + ");'>Modificar</a>&nbsp;<a href='javascript:eliminarPedido(" + x.IDPedidoTraslado + ");'>Cancelar</a>"
                       }).ToList();

                    rptProductos.DataSource = result;
                    rptProductos.DataBind();
                }
            }
        }
    }

    [System.Web.Services.WebMethod(true, CacheDuration = 0)]
    public static void Eliminar(int id)
    {
        if (HttpContext.Current.Session["CurrentUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                var pedido = dbContext.PedidosTraslado.Include("Servicios").Include("Servicios.Proveedores").Where(x => x.IDPedidoTraslado == id).FirstOrDefault();
                if (pedido != null)
                {
                    DateTime fechaIda = pedido.FechaIda.HasValue ? pedido.FechaIda.Value.Date : pedido.FechaVuelta.Value.Date;
                    DateTime fechaHoy = DateTime.Now.Date;
                    if (fechaIda > fechaHoy)
                    {
                        if ((fechaIda - fechaHoy).TotalDays < 3)
                            throw new Exception("Ya se cumplió el plazo de cancelación del transporte.");
                    }
                    else
                        throw new Exception("El transporte ya fue realizado");


                    ListDictionary datos = new ListDictionary();
                    datos.Add("<IDPEDIDO>", pedido.IDPedidoTraslado);
                    datos.Add("<USUARIO>", pedido.Usuarios.Contacto);
                    datos.Add("<EMPRESA>", pedido.Usuarios.Empresa);
                    if (pedido.FechaIda.HasValue)
                        datos.Add("<FECHAIDA>", pedido.FechaIda.Value.ToString("dd/MM/yyyy"));
                    if (pedido.FechaVuelta.HasValue)
                        datos.Add("<FECHAVUELTA>", pedido.FechaVuelta.Value.ToString("dd/MM/yyyy"));
                    datos.Add("<ORIGEN>", (pedido.LugaresTrasladosOrigen != null) ? pedido.LugaresTrasladosOrigen.Nombre : "");
                    datos.Add("<DESTINO>", (pedido.LugaresTrasladosDestino != null) ? pedido.LugaresTrasladosDestino.Nombre : "");

                    bool send = EmailHelper.SendMessage(EmailTemplate.ReservaCancelada, datos, pedido.Servicios.Proveedores.Email, "TRASLADOS RED: Reserva Cancelada #" + id);
                    if (send)
                    {
                        pedido.Estado = "Cancelado";
                        dbContext.SaveChanges();
                    }
                    else
                        throw new Exception("Hubo un error cancelando el pedido, por favor intente nuevamente");
                }
            }
        }
        else
            throw new Exception("Por favor, vuelva a iniciar sesión");
    }

    [System.Web.Services.WebMethod(true, CacheDuration = 0)]
    public static void Modificar(int id)
    {
        if (HttpContext.Current.Session["CurrentUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                var pedido = dbContext.Pedidos.Include("PedidosDetalle").Where(x => x.IDPedido == id).FirstOrDefault();
                if (pedido != null)
                {
                    HttpContext.Current.Session["ASPNETShoppingCart"] = null;

                    ShoppingCart.RetrieveShoppingCart().IDPedido = id;
                    ShoppingCart.RetrieveShoppingCart().Pasajero = pedido.Pasajero;
                    ShoppingCart.RetrieveShoppingCart().NroDoc = pedido.NroDocumento;
                    ShoppingCart.RetrieveShoppingCart().FechaDesde = pedido.FechaEstadiaDesde;
                    ShoppingCart.RetrieveShoppingCart().FechaHasta = pedido.FechaEstadiaHasta;

                    ////Obtengo los primeros de cada uno ya que guardo la cantidad original
                    //var turista = pedido.PedidosDetalle.Where(x => x.Productos.Tipo == "T").FirstOrDefault();
                    //var premium = pedido.PedidosDetalle.Where(x => x.Productos.Tipo == "P").FirstOrDefault();
                    //var kids = pedido.PedidosDetalle.Where(x => x.Productos.Tipo == "M").FirstOrDefault();

                    ////Si no fueron pedidos, los agrego en 0 para que pueda modificar la cantidad al realizar la modificacion de un pedido
                    //if (turista != null)
                    //    ShoppingCart.RetrieveShoppingCart().AddItem(turista.Productos.IDProducto, turista.CantCenas, turista.CantPasajeros);
                    //else
                    //    ShoppingCart.RetrieveShoppingCart().AddItem(4, 0, 0);

                    //if (premium != null)
                    //    ShoppingCart.RetrieveShoppingCart().AddItem(premium.Productos.IDProducto, premium.CantCenas, premium.CantPasajeros);
                    //else
                    //    ShoppingCart.RetrieveShoppingCart().AddItem(5, 0, 0);

                    //if (kids != null)
                    //    ShoppingCart.RetrieveShoppingCart().AddItem(kids.Productos.IDProducto, kids.CantCenas, kids.CantPasajeros);
                    //else
                    //    ShoppingCart.RetrieveShoppingCart().AddItem(6, 0, 0);
                }
            }
        }
    }
}