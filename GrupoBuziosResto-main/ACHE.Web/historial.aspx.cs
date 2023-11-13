using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class historial : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser == null || CurrentUser.TipoUsuario == "R")
                Response.Redirect("login.aspx");
            else
            {
                using (var dbContext = new ACHEEntities())
                {
                    var result = dbContext.Pedidos.Include("PedidosDetalle").Where(x => x.IDUsuario == CurrentUser.ID)
                       .OrderByDescending(x => x.FechaAlta).ToList()
                       .Select(x => new
                       {
                           IDPedido = x.IDPedido,
                           FechaAlta = x.FechaAlta.ToString("dd/MM/yyyy"),
                           Estadia = x.FechaEstadiaDesde.ToString("dd/MM/yyyy") + " - " + x.FechaEstadiaHasta.ToString("dd/MM/yyyy"),
                           Pasajero = x.Pasajero,
                           Total = x.Total.ToString("N2"),
                           Cantidad = x.PedidosDetalle.Count(),
                           Links = x.PedidosDetalle.Any(y => y.Validado.HasValue && y.Validado.Value) ? "" : "<a href='javascript:modificarPedido(" + x.IDPedido + ");'>modificar</a>&nbsp;<a href='javascript:eliminarPedido(" + x.IDPedido + ");'>eliminar</a>"
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
                var pedido = dbContext.Pedidos.Include("PedidosDetalle").Where(x => x.IDPedido == id).FirstOrDefault();
                if (pedido != null)
                {
                    if (pedido.PedidosDetalle.Any(y => y.Validado.HasValue && y.Validado.Value))
                        throw new Exception("El pedido no se puede eliminar ya que tiene cupones validados");
                    else
                    {
                        dbContext.Pedidos.Remove(pedido);
                        dbContext.SaveChanges();
                    }
                }
            }
        }
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
                    if (pedido.PedidosDetalle.Any(y => y.Validado.HasValue && y.Validado.Value))
                        throw new Exception("El pedido no se puede eliminar ya que tiene cupones validados");
                    else
                    {
                        HttpContext.Current.Session["ASPNETShoppingCart"] = null;

                        ShoppingCart.RetrieveShoppingCart().IDPedido = id;
                        ShoppingCart.RetrieveShoppingCart().Pasajero = pedido.Pasajero;
                        ShoppingCart.RetrieveShoppingCart().NroDoc = pedido.NroDocumento;
                        ShoppingCart.RetrieveShoppingCart().FechaDesde = pedido.FechaEstadiaDesde;
                        ShoppingCart.RetrieveShoppingCart().FechaHasta = pedido.FechaEstadiaHasta;

                        //Obtengo los primeros de cada uno ya que guardo la cantidad original
                        var turista = pedido.PedidosDetalle.Where(x => x.Productos.Tipo == "T").FirstOrDefault();
                        var playa = pedido.PedidosDetalle.Where(x => x.Productos.Tipo == "B").FirstOrDefault();
                        var kids = pedido.PedidosDetalle.Where(x => x.Productos.Tipo == "M").FirstOrDefault();
                        var clasico = pedido.PedidosDetalle.Where(x => x.Productos.Tipo == "C").FirstOrDefault();

                        //Si no fueron pedidos, los agrego en 0 para que pueda modificar la cantidad al realizar la modificacion de un pedido
                        if (turista != null)
                            ShoppingCart.RetrieveShoppingCart().AddItem(turista.Productos.IDProducto, turista.CantCenas, turista.CantPasajeros);
                        else
                            ShoppingCart.RetrieveShoppingCart().AddItem(4, 0, 0);

                        if (playa != null)
                            ShoppingCart.RetrieveShoppingCart().AddItem(playa.Productos.IDProducto, playa.CantCenas, playa.CantPasajeros);
                        else
                            ShoppingCart.RetrieveShoppingCart().AddItem(5, 0, 0);
                        
                        if (kids != null)
                            ShoppingCart.RetrieveShoppingCart().AddItem(kids.Productos.IDProducto, kids.CantCenas, kids.CantPasajeros);
                        else
                            ShoppingCart.RetrieveShoppingCart().AddItem(6, 0, 0);

                        if (clasico != null)
                            ShoppingCart.RetrieveShoppingCart().AddItem(clasico.Productos.IDProducto, clasico.CantCenas, clasico.CantPasajeros);
                        else
                            ShoppingCart.RetrieveShoppingCart().AddItem(7, 0, 0);
                    }
                }
            }
        }
    }

}