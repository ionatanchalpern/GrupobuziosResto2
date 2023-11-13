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


public partial class admin_modulos_reportes_rpt_cupones : AdminBasePage {
    protected void Page_Load(object sender, EventArgs e) {

    }

    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter, string fechaDesde, string fechaHasta) {
        if (HttpContext.Current.Session["AdminUser"] != null) {
            using (var dbContext = new ACHEEntities()) {
                var result = dbContext.PedidosDetalle.Include("Pedidos").Include("Pedidos.Usuarios").Include("Restaurantes").Include("Productos")
                    .Where(x => x.FechaValidacion.HasValue)
                   .OrderByDescending(x => x.FechaValidacion.Value).ToList()
                   .Select(x => new RptCuponesViewModel() {
                       IDPedido = x.IDPedido,
                       Codigo = Cryptography.Encrypt(x.IDDetalle) + (x.DigitoVerficador.HasValue ? "-" + x.DigitoVerficador.Value : ""),
                       FechaValidacion = x.FechaValidacion.Value,
                       Tipo = x.Productos.Tipo == "T" ? "Turista" : (x.Productos.Tipo == "P") ? "Premium" : (x.Productos.Tipo == "C") ? "Clasico" : (x.Productos.Tipo=="M"?"Kids":"Playa"),

                       //EstadiaDesde = x.Pedidos.FechaEstadiaDesde,
                       //EstadiaHasta = x.Pedidos.FechaEstadiaHasta,
                       Pasajero = x.Pedidos.Pasajero.ToUpper(),
                       Restaurant = x.Restaurantes.Nombre.ToUpper(),
                       Operador = x.Pedidos.Usuarios.Empresa.ToUpper(),
                       PagoPor = x.Pedidos.PagoPor != null ? x.Pedidos.PagoPor : "",
                       Precio = x.Precio,
                       Validado = x.Validado.HasValue && x.Validado.Value ? "Si" : "No"
                   });

                if (fechaDesde != string.Empty) {
                    string[] aux1 = fechaDesde.Split("/");
                    var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                    result = result.Where(x => x.FechaValidacion >= dtDesde);
                }
                
                if (fechaHasta != string.Empty) {
                    string[] aux1 = fechaHasta.Split("/");
                    var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                    result = result.Where(x => x.FechaValidacion <= dtHasta);
                }


                return result.AsQueryable().ToDataSourceResult(take, skip, sort, filter);//.ToList();
            }
        }
        else
            return null;
    }

    [System.Web.Services.WebMethod(true)]
    public static string Exportar(string restaurant, string operador, string fechaDesde, string fechaHasta, string codigo) {
        string fileName = "RptPedidos_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        string path = "/tmp/";
        if (HttpContext.Current.Session["AdminUser"] != null) {
            try {
                DataTable dt = new DataTable();
                using (var dbContext = new ACHEEntities()) {
                    var info = dbContext.PedidosDetalle.Include("Pedidos").Include("Pedidos.Usuarios").Include("Restaurantes").Include("Productos")
                        .Where(x => x.FechaValidacion.HasValue).AsQueryable();
                    if (operador != "")
                        info = info.Where(x => x.Pedidos.Usuarios.Empresa.ToLower().Contains(operador.ToLower()));
                    if (restaurant != "")
                        info = info.Where(x => x.Restaurantes.Nombre.ToLower().Contains(restaurant.ToLower()));
                    //if (codigo != "")
                    //    info = info.Where(x => x.Pasajero.Contains(codigo));

                    if (fechaDesde != string.Empty) {
                        string[] aux1 = fechaDesde.Split("/");
                        var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                        info = info.Where(x => x.FechaValidacion >= dtDesde);
                    }

                    if (fechaHasta != string.Empty) {
                        string[] aux2 = fechaHasta.Split("/");
                        var dtHasta = DateTime.Parse(aux2[2] + "-" + aux2[1] + "-" + aux2[0] + " 23:59:59 pm");
                        info = info.Where(x => x.FechaValidacion <= dtHasta);
                    }

                    dt = info.ToList().Select(x => new {
                        Codigo = Cryptography.Encrypt(x.IDDetalle),
                        FechaValidacion = x.FechaValidacion.Value,
                        Menu = x.Productos.Tipo == "T" ? "Turista" : (x.Productos.Tipo == "P") ? "Premium" : (x.Productos.Tipo == "C") ? "Clasico" : (x.Productos.Tipo == "M" ? "Kids" : "Playa"),

                        //EstadiaDesde = x.Pedidos.FechaEstadiaDesde,
                        //EstadiaHasta = x.Pedidos.FechaEstadiaHasta,
                        //Pasajero = x.Pedidos.Pasajero.ToUpper(),
                        //DNI = x.Pedidos.NroDocumento,
                        Restaurant = x.Restaurantes.Nombre.ToUpper(),
                        Operador = x.Pedidos.Usuarios.Empresa.ToUpper(),
                        PagoPor = x.Pedidos.PagoPor != null ? x.Pedidos.PagoPor : "",
                        NetoResto = x.Precio,
                    }).ToList().ToDataTable();
                }

                if (dt.Rows.Count > 0) {
                    generarArchivo(dt, HttpContext.Current.Server.MapPath(path) + Path.GetFileName(fileName), fileName);
                }
                else {
                    throw new Exception("No se encuentran datos para los filtros seleccionados");
                }
                return path + fileName + ".xlsx";
            }
            catch (Exception e) {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                BasicLog.AppendToFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
                throw e;
            }
        }
        else
            return "";
    }

    public static void generarArchivo(DataTable dt, string path, string fileName) {
        var wb = new XLWorkbook();
        wb.Worksheets.Add(dt, fileName);
        wb.SaveAs(path + ".xlsx");
    }
}