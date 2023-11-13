using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using System.Configuration;


public partial class admin_modulos_custom_pedidos : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter, string fechaInDesde, string fechaInHasta, string fechaOutDesde, string fechaOutHasta, string altaDesde, string altaHasta)
    {
        using (var dbContext = new ACHEEntities())
        {
            var result = dbContext.Pedidos.Include("PedidosDetalle").Include("Usuarios")
               .OrderBy(x => x.FechaAlta)
               .Select(x => new PedidosViewModel()
               {
                   IDPedido = x.IDPedido,
                   IDUsuario = x.IDUsuario,
                   FechaAlta = x.FechaAlta,
                   EstadiaDesde = x.FechaEstadiaDesde,
                   EstadiaHasta = x.FechaEstadiaHasta,
                   Pasajero = x.Pasajero.ToUpper(),
                   NombreContacto = x.Usuarios.Contacto.ToUpper(),
                   Empresa = x.Usuarios.Empresa.ToUpper(),
                   Total = x.Total,
                   PagoPor = x.PagoPor != null ? x.PagoPor : "",
                   Cantidad = x.PedidosDetalle.Count(),
                   Validados = x.PedidosDetalle.Where(y => y.Validado == true).Count(),
               });

            if (fechaInDesde != string.Empty)
            {
                string[] aux1 = fechaInDesde.Split("/");
                var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                result = result.Where(x => x.EstadiaDesde >= dtDesde);
            }

            if (fechaInHasta != string.Empty)
            {
                string[] aux1 = fechaInHasta.Split("/");
                var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                result = result.Where(x => x.EstadiaDesde <= dtHasta);
            }

            if (fechaOutDesde != string.Empty)
            {
                string[] aux1 = fechaOutDesde.Split("/");
                var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                result = result.Where(x => x.EstadiaHasta >= dtDesde);
            }

            if (fechaOutHasta != string.Empty)
            {
                string[] aux1 = fechaOutHasta.Split("/");
                var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                result = result.Where(x => x.EstadiaHasta <= dtHasta);
            }


            if (altaDesde != string.Empty)
            {
                string[] aux1 = altaDesde.Split("/");
                var dtDesde2 = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                result = result.Where(x => x.FechaAlta >= dtDesde2);
            }

            if (altaHasta != string.Empty)
            {
                string[] aux1 = altaHasta.Split("/");
                var dtHasta2 = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                result = result.Where(x => x.FechaAlta <= dtHasta2);
            }

            return result.ToDataSourceResult(take, skip, sort, filter);//.ToList();
        }

    }

    [System.Web.Services.WebMethod(true)]
    public static void Delete(int id)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                var entity = dbContext.Pedidos.Include("PedidosDetalle").Where(x => x.IDPedido == id).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.PedidosDetalle.Any(x => x.Validado.HasValue && x.Validado.Value))
                        throw new Exception("El pedido tiene cupones validados. No se puede eliminar");
                    dbContext.Pedidos.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static void Desvalidar(int id)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                var entity = dbContext.PedidosDetalle.Where(x => x.IDDetalle == id).FirstOrDefault();
                if (entity != null)
                {
                    entity.Validado = null;
                    entity.IDRestauranteValidacion = null;
                    entity.FechaValidacion = null;

                    dbContext.SaveChanges();
                }
            }
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static string GetInfo(int idPedido)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            var usu = (WebUser)HttpContext.Current.Session["AdminUser"];

            using (var dbContext = new ACHEEntities())
            {
                string datosPedido = string.Empty;
                string htmlAdjunto = string.Empty;
                //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                //var random = new Random();

                #region armo htmlAdjunto
                htmlAdjunto += "<table style='border-collapse: collapse;text-align: center; border: 1px solid #833332; font-family: Arial,Trebuchet MS,Segoe UI,Helvetica,sans-serif; font-size: 14px; background-color: #FFF'>";
                htmlAdjunto += "<tbody id='formulario'>";
                htmlAdjunto += "<tr>";
                htmlAdjunto += "<td style='border: 1px solid #833332; font-weight: bold; width: 100px;' rowspan='1'>Codigo</td>";
                htmlAdjunto += "<td style='border: 1px solid #833332; font-weight: bold; width: 100px;' rowspan='1'>Tipo</td>";
                htmlAdjunto += "<td style='border: 1px solid #833332; font-weight: bold; width: 100px;' rowspan='1'>Neto Restó</td>";
                htmlAdjunto += "<td style='border: 1px solid #833332; font-weight: bold; width: 100px;' rowspan='1'>Neto Op</td>";
                htmlAdjunto += "<td style='border: 1px solid #833332; font-weight: bold; width: 100px;' rowspan='1'>Validado</td>";
                htmlAdjunto += "<td style='border: 1px solid #833332; font-weight: bold; width: 100px;' rowspan='1'></td>";
                htmlAdjunto += "</tr>";
                #endregion

                var detalles = dbContext.PedidosDetalle.Where(x => x.IDPedido == idPedido).OrderBy(x => x.IDProducto).ToList().Select(x => new
                {
                    IDDetalle = x.IDDetalle.ToString(),
                    DigitoVerficador = x.DigitoVerficador,
                    IDProducto = x.IDProducto,
                    IDPedido = x.IDPedido,
                    Precio = x.Precio,
                    PrecioOperador = x.PrecioOperador,
                    Validado = x.Validado == true ? "Si" : "No",
                    Eliminar = x.Validado == true ? (usu.TipoUsuario == "A" ? "<a href='javascript:desvalidar(" + x.IDDetalle + ");'>desvalidar</a>" : "") : "",
                    FechaValidacion = x.FechaValidacion,
                    Tipo = x.Productos.Tipo == "T" ? "Turista" : (x.Productos.Tipo == "P") ? "Premium" : (x.Productos.Tipo == "C") ? "Clasico" : (x.Productos.Tipo == "M" ? "Menores" : "Playa"),
                }).ToList();

                var info = new DetalleViewModel();
                if (detalles.Any())
                {
                    foreach (var detalle in detalles)
                    {
                        //string codigo = string.Empty;
                        //var result = new string(
                        //    Enumerable.Repeat(chars, 4)
                        //              .Select(s => s[random.Next(s.Length)])
                        //            .ToArray());

                        //codigo = result;

                        htmlAdjunto += "<tr style='border: 1px solid #833332; width: 100px;'>";
                        htmlAdjunto += "<td style='border: 1px solid black; width: 100px;'>" + Cryptography.Encrypt(detalle.IDDetalle) + (detalle.DigitoVerficador.HasValue ? "-" + detalle.DigitoVerficador.Value : "") + "</td>";
                        //codigo = string.Empty;
                        htmlAdjunto += "<td style='border: 1px solid black; width: 100px;'>" + detalle.Tipo + "</td>";
                        htmlAdjunto += "<td style='border: 1px solid black; width: 100px;'>" + detalle.Precio + "</td>";
                        htmlAdjunto += "<td style='border: 1px solid black; width: 100px;'>" + detalle.PrecioOperador + "</td>";
                        htmlAdjunto += "<td style='border: 1px solid black; width: 100px;' id='tdValidado_" + detalle.IDDetalle + "'>" + detalle.Validado + "</td>";
                        htmlAdjunto += "<td style='border: 1px solid black; width: 100px;' id='tdDesvalidar_" + detalle.IDDetalle + "'>" + detalle.Eliminar + "</td>";

                        htmlAdjunto += "</tr>";
                    }
                }

                var pedido = dbContext.Pedidos.Where(x => x.IDPedido == idPedido).FirstOrDefault();

                if (pedido != null)
                {
                    //datosPedido += "<style>body { font-family: calibri;}</style>";
                    if (!string.IsNullOrEmpty(pedido.PagoPor))
                        datosPedido += "Pago por: " + pedido.PagoPor + "<br/>";
                    datosPedido += "Pasajero: " + pedido.Pasajero + "<br/>";
                    datosPedido += "DNI: " + pedido.NroDocumento + "<br/>";
                    datosPedido += "Estadia desde: " + pedido.FechaEstadiaDesde.ToString("dd/MM/yyyy") + "</br>";
                    datosPedido += "Estadia hasta: " + pedido.FechaEstadiaHasta.ToString("dd/MM/yyyy") + "</br>";
                }

                htmlAdjunto += "</table>";

                htmlAdjunto = datosPedido + "<br /><br/>" + htmlAdjunto;

                return htmlAdjunto;
            }
        }
        else
            return null;
    }

    [System.Web.Services.WebMethod(true)]
    public static string Exportar(string nombreContacto, string operador, string fechaInDesde, string fechaInHasta, string fechaOutDesde, string fechaOutHasta, string altaDesde, string altaHasta, string pasajero)
    {
        string fileName = "Pedidos_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        string path = "/tmp/";
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            //try
            //{
                DataTable dt = new DataTable();
                using (var dbContext = new ACHEEntities())
                {
                    var result = dbContext.Pedidos.Include("PedidosDetalle").Include("Usuarios").OrderBy(x => x.FechaAlta).ToList().AsQueryable();
                    if (nombreContacto.Trim() != "")
                        result = result.Where(x => x.Usuarios.Contacto.ToLower().Contains(nombreContacto.ToLower()));
                    if (operador.Trim()  != "")
                        result = result.Where(x => x.Usuarios.Empresa.ToLower().Contains(operador.ToLower()));
                    if (pasajero.Trim()  != "")
                        result = result.Where(x => x.Pasajero.ToLower().Contains(pasajero.ToLower()));

                    if (fechaInDesde != string.Empty)
                    {
                        string[] aux1 = fechaInDesde.Split("/");
                        var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                        result = result.Where(x => x.FechaEstadiaDesde >= dtDesde);
                    }

                    if (fechaInHasta != string.Empty)
                    {
                        string[] aux1 = fechaInHasta.Split("/");
                        var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                        result = result.Where(x => x.FechaEstadiaDesde <= dtHasta);
                    }

                    if (fechaOutDesde != string.Empty)
                    {
                        string[] aux1 = fechaOutDesde.Split("/");
                        var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                        result = result.Where(x => x.FechaEstadiaHasta >= dtDesde);
                    }

                    if (fechaOutHasta != string.Empty)
                    {
                        string[] aux1 = fechaOutHasta.Split("/");
                        var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                        result = result.Where(x => x.FechaEstadiaHasta <= dtHasta);
                    }

                    if (altaDesde != string.Empty)
                    {
                        string[] aux3 = altaDesde.Split("/");
                        var dtDesde2 = DateTime.Parse(aux3[2] + "-" + aux3[1] + "-" + aux3[0]);
                        result = result.Where(x => x.FechaAlta >= dtDesde2);
                    }

                    if (altaHasta != string.Empty)
                    {
                        string[] aux4 = altaHasta.Split("/");
                        var dtHasta2 = DateTime.Parse(aux4[2] + "-" + aux4[1] + "-" + aux4[0] + " 23:59:59 pm");
                        result = result.Where(x => x.FechaAlta <= dtHasta2);
                    }

                    dt = result.ToList().Select(x => new
                    {
                        //IDPedido = x.IDPedido,
                        Operador = x.Usuarios.Empresa.ToUpper(),
                        NombreContacto = x.Usuarios.Contacto.ToUpper(),
                        FechaCompra = x.FechaAlta.ToString("dd/MM/yyyy"),
                        EstadiaDesde = x.FechaEstadiaDesde.ToString("dd/MM/yyyy"),
                        EstadiaHasta = x.FechaEstadiaHasta.ToString("dd/MM/yyyy"),
                        Pasajero = x.Pasajero.ToUpper(),
                        TotalNetoOperador = x.Total,
                        PagoPor = x.PagoPor != null ? x.PagoPor : "",
                        Cantidad = x.PedidosDetalle.Count(),
                        Validados = x.PedidosDetalle.Where(y => y.Validado == true).Count(),
                    }).ToList().ToDataTable();
                }

                if (dt.Rows.Count > 0)
                {
                    generarArchivo(dt, HttpContext.Current.Server.MapPath(path) + Path.GetFileName(fileName), fileName);
                }
                else
                {
                    throw new Exception("No se encuentran datos para los filtros seleccionados");
                }
                return path + fileName  + ".xlsx";
            /*}
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                BasicLog.AppendToFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
                throw e;
            }*/
        }
        return "";
    }

    public static void generarArchivo(DataTable dt, string path, string fileName)
    {
        var wb = new XLWorkbook();
        wb.Worksheets.Add(dt, "Pedidos");
        wb.SaveAs(path + ".xlsx");
    }
}