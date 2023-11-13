using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using System.Collections.Specialized;
using ACHE.Extensions;
using System.Configuration;
using System.Globalization;

public partial class carrito : WebBasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser != null && CurrentUser.TipoUsuario == "O")
            {
                hdnIDCurrentUser.Value = CurrentUser.ID.ToString();
                pnlFinalizar.Visible = true;
                cargarCarrito();

                trPagoPor.Visible = CurrentUser.PagoPorHabilitado;
            }
            else
            {
                pnlFinalizar.Visible = false;
                pnlLogin.Visible = true;
                pnlCarrito.Visible = false;
            }
        }
    }

    private void cargarCarrito()
    {
        using (var dbContext = new ACHEEntities())
        {
            var productos = ShoppingCart.RetrieveShoppingCart().Items.ToList();

            txtPasajero.Text = ShoppingCart.RetrieveShoppingCart().Pasajero;
            txtNroDocumento.Text = ShoppingCart.RetrieveShoppingCart().NroDoc;
            if (ShoppingCart.RetrieveShoppingCart().FechaDesde.HasValue)
                spnDesde.InnerText = ShoppingCart.RetrieveShoppingCart().FechaDesde.Value.ToString("dd/MM/yyyy");
            if (ShoppingCart.RetrieveShoppingCart().FechaHasta.HasValue)
                spnHasta.InnerText = ShoppingCart.RetrieveShoppingCart().FechaHasta.Value.ToString("dd/MM/yyyy");

            if (productos.Any())
            {
                List<ProductosFrontViewModel> productosLista = new List<ProductosFrontViewModel>();
                var dtDesde = ShoppingCart.RetrieveShoppingCart().FechaDesde.Value;
                var anioActual = DateTime.Now.Year;

                foreach (CartItem item in productos)
                {
                    var productoCarrito = new ProductosFrontViewModel();

                    productoCarrito.ID = item.IDProducto;

                    var fechaCambioTemp = DateTime.Parse(ConfigurationManager.AppSettings["GBR.CambioTemporada"]);
                  /*  if (dtDesde >= fechaCambioTemp)
                    {
                        productoCarrito.PrecioResto = item.PrecioRestoProxTemp.ToString("N2");
                        productoCarrito.PrecioOperador = item.PrecioOperadorProxTemp.ToString("N2");
                    }
                    else
                    {
                        productoCarrito.PrecioResto = item.PrecioResto.ToString("N2");
                        productoCarrito.PrecioOperador = item.PrecioOperador.ToString("N2");
                    }
                    */
                    productoCarrito.PrecioResto = item.PrecioResto.ToString("N2");
                    productoCarrito.PrecioOperador = item.PrecioOperador.ToString("N2");
                    /*if (dtDesde.Year == anioActual) {
                        if (dtDesde.Month == 12) {
                            if (dtDesde.Day > 19) {
                                productoCarrito.PrecioResto = item.PrecioRestoProxTemp.ToString("N2");
                                productoCarrito.PrecioOperador = item.PrecioOperadorProxTemp.ToString("N2");
                            }
                            else {
                                productoCarrito.PrecioResto = item.PrecioResto.ToString("N2");
                                productoCarrito.PrecioOperador = item.PrecioOperador.ToString("N2");
                            }
                        }
                        else {
                            productoCarrito.PrecioResto = item.PrecioResto.ToString("N2");
                            productoCarrito.PrecioOperador = item.PrecioOperador.ToString("N2");
                        }
                    }
                    else if (dtDesde.Year > anioActual) {
                        productoCarrito.PrecioResto = item.PrecioRestoProxTemp.ToString("N2");
                        productoCarrito.PrecioOperador = item.PrecioOperadorProxTemp.ToString("N2");
                    }*/

                    //productoCarrito.PrecioRestoProxTemp = item.PrecioRestoProxTemp.ToString("N2");
                    //productoCarrito.PrecioOperadorProxTemp = item.PrecioOperadorProxTemp.ToString("N2");
                    productoCarrito.Tipo = item.Tipo;
                    productoCarrito.CantCenas = item.CantCenas;
                    productoCarrito.CantPax = item.CantPax;
                    productoCarrito.SubTotal = (ShoppingCart.SubTotalProducto(item.IDProducto)).ToString("N2");
                    productosLista.Add(productoCarrito);
                }

                rptProductos.DataSource = productosLista;
                rptProductos.DataBind();
            }
            else
                Response.Redirect("/productos.aspx");
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static decimal ObtenerTotal()
    {
        decimal total = ShoppingCart.GetTotal();
        return total;
    }

    [System.Web.Services.WebMethod(true)]
    public static int CrearPedido(int id, string pasajero, string dni, string estadiaDesde, string estadiaHasta, string pagoPor)
    {
        if (HttpContext.Current.Session["CurrentUser"] != null)
        {
            //WebUser CurrentUser = (WebUser) HttpContext.Current.Session["CurrentUser"];
            try
            {
                bool pedidoGenerado = false;
                int idPedido = 0;

                //string[] aux1 = estadiaDesde.Split("/");
                //string[] aux2 = estadiaHasta.Split("/");

                //if (aux1.Length < 3 || aux2.Length < 3)
                //    throw new Exception("Las fechas de estadía son inválidas.");

                //var dtDesde = DateTime.Parse(aux1[1] + "-" + aux1[0] + "-" + aux1[2]);
                //var dtHasta = DateTime.Parse(aux2[1] + "-" + aux2[0] + "-" + aux2[2]);



                DateTime dtDesde = Convert.ToDateTime(DateTime.ParseExact(estadiaDesde, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString());
                DateTime dtHasta = Convert.ToDateTime(DateTime.ParseExact(estadiaHasta, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString());

                int cantDiasEstadia = dtHasta.GetDaysDiff(dtDesde);

                //var dtHoy = DateTime.Now.AddDays(-1);
                var anioActual = DateTime.Now.Year;

                //if (dtDesde.Date < dtHoy)
                //    throw new Exception("La fecha de estadía desde no puede ser menor a HOY.");

                var lstItems = ShoppingCart.RetrieveShoppingCart().Items;
                int cantTotalCenas = ShoppingCart.RetrieveShoppingCart().Items.Sum(x => x.CantCenas * x.CantPax);
                int cantPax = ShoppingCart.RetrieveShoppingCart().Items.Sum(x => x.CantPax);

                if (!lstItems.Any())
                {
                    throw new Exception("Debe agregar por lo menos 1 producto al carrito para poder realizar el pedido.");
                }
                else if (cantTotalCenas > (cantDiasEstadia * cantPax))
                {
                    throw new Exception("La cant de cenas es mayor a los dias de estadia.");
                }

                using (var dbContext = new ACHEEntities())
                {
                    var usuario = dbContext.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault();

                    Pedidos pedido = new Pedidos();
                    int idPedidoMod = ShoppingCart.RetrieveShoppingCart().IDPedido;
                    if (idPedidoMod > 0)
                        pedido = dbContext.Pedidos.Where(x => x.IDPedido == idPedidoMod).FirstOrDefault();

                    if (usuario != null)
                    {
                        //Creamos la cabecera del pedido
                        pedido.IDUsuario = usuario.IDUsuario;

                        pedido.FechaEstadiaDesde = dtDesde;
                        pedido.FechaEstadiaHasta = dtHasta;
                        pedido.Pasajero = pasajero;
                        pedido.NroDocumento = dni;
                        pedido.FechaAlta = DateTime.Now;
                        if (pagoPor != "undefined")
                            pedido.PagoPor = pagoPor;
                        if (idPedidoMod > 0)
                            dbContext.Database.ExecuteSqlCommand("delete PedidosDetalle where IDPedido=" + idPedidoMod, new object[] { });

                        pedido.PedidosDetalle = new List<PedidosDetalle>();
                        decimal total = 0;
                        //Creamos cada uno de los detalles del pedido recorriendo todos los productos

                        Random r = new Random();
                        foreach (CartItem item in lstItems)
                        {
                            var cantTotal = item.CantCenas * item.CantPax;
                            int auxFechaCambioTarifa = cantTotal / cantDiasEstadia;
                            var auxDate = dtDesde;

                            for (int i = 1; i <= cantTotal; i++)
                            {
                                var detalle = new PedidosDetalle();
                                detalle.IDProducto = item.IDProducto;
                                //detalle.IDPedido = pedido.IDPedido;


                                /*if (dtDesde.Year >= anioActual && dtDesde.Month >= 12 && dtDesde.Day > 19)
                                {
                                    detalle.PrecioOperador = item.PrecioOperadorProxTemp;
                                    detalle.Precio = item.PrecioRestoProxTemp;
                                }
                                else {
                                    detalle.PrecioOperador = item.PrecioOperador;
                                    detalle.Precio = item.PrecioResto;
                                }*/
                               
                                /*if (dtDesde.Year == anioActual)
                                {
                                    if (dtDesde.Month == 12)
                                    {
                                        if (dtDesde.Day > 19)
                                        {
                                            detalle.Precio = item.PrecioRestoProxTemp;
                                            detalle.PrecioOperador = item.PrecioOperadorProxTemp;
                                        }
                                        else
                                        {
                                            detalle.Precio = item.PrecioResto;
                                            detalle.PrecioOperador = item.PrecioOperador;
                                        }
                                    }
                                    else
                                    {
                                        detalle.Precio = item.PrecioResto;
                                        detalle.PrecioOperador = item.PrecioOperador;
                                    }
                                }
                                else if (dtDesde.Year > anioActual)
                                {
                                    detalle.Precio = item.PrecioRestoProxTemp;
                                    detalle.PrecioOperador = item.PrecioOperadorProxTemp;
                                }*/

                             //   var fechaCambioTemp = DateTime.Parse(ConfigurationManager.AppSettings["GBR.CambioTemporada"]);
                         /*       if (dtDesde >= fechaCambioTemp)
                                {
                                    detalle.Precio = item.PrecioRestoProxTemp;
                                    detalle.PrecioOperador = item.PrecioOperadorProxTemp;
                                }
                                else
                                {
                                    detalle.Precio = item.PrecioResto;
                                    detalle.PrecioOperador = item.PrecioOperador;
                                }
                                */
                                detalle.Precio = item.PrecioResto;
                                detalle.PrecioOperador = item.PrecioOperador;
                                detalle.Validado = false;
                                detalle.FechaValidacion = null;
                                detalle.IDRestauranteValidacion = null;
                                detalle.CantCenas = item.CantCenas;//Guardo la cant original para poder modificar el pedido
                                detalle.CantPasajeros = item.CantPax;//Guardo la cant original para poder modificar el pedido
                                detalle.DigitoVerficador = r.Next(0, 9);

                                pedido.PedidosDetalle.Add(detalle);
                                total += detalle.PrecioOperador;

                            }
                        }
                        pedido.Total = total;
                        if (idPedidoMod == 0)
                            dbContext.Pedidos.Add(pedido);
                        dbContext.SaveChanges();
                        idPedido = pedido.IDPedido;
                        pedidoGenerado = true;
                    }
                    else
                    {
                        throw new Exception("El usuario no existe");
                    }

                    if (pedidoGenerado)
                    {
                        HttpContext.Current.Session["ASPNETShoppingCart"] = null;
                        return idPedido;
                    }
                    else
                        throw new Exception("Hubo un error, intente nuevamente");

                }
            }
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                BasicLog.AppendToFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
                throw e;
            }
        }
        else
            throw new Exception("Debe iniciar sesión");
    }
}