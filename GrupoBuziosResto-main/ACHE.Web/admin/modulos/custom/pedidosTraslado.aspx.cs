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


public partial class admin_modulos_custom_pedidos_traslado : AdminBasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter, string fechaIdaDesde, string fechaIdaHasta
        , string fechaVueltaDesde, string fechaVueltaHasta, string altaDesde, string altaHasta)
    {
        using (var dbContext = new ACHEEntities())
        {
            var result = dbContext.PedidosTraslado
               .Include("Usuarios").Include("PasajerosPorPedidoTraslado").Include("Servicios").Include("Servicios.Proveedores")
               .Where(x => x.Estado.ToLower() != "borrador")
               .OrderByDescending(x => x.FechaAlta)
               .Select(x => new PedidosTrasladoViewModel()
               {
                   IDPedido = x.IDPedidoTraslado,
                   FechaAlta = x.FechaAlta,
                   Estado = x.Estado,
                   Tipo = x.TipoServicio == "R" ? "Round Trip" : "One Way",
                   Servicio = x.Servicios.Nombre,
                   Proovedor = x.Servicios.Proveedores.Nombre,
                   Pasajero = x.PasajerosPorPedidoTraslado.FirstOrDefault().Nombre,
                   Operador = x.Usuarios.Empresa.ToUpper(),
                   NombreContacto = x.Usuarios.Contacto.ToUpper(),
                   FechaIda = x.FechaIda,
                   FechaVuelta = x.FechaVuelta,
                   Total = x.Total,
                   NroFile = x.NroFile,
                   PagoPor = x.PagoPor,
                   CantPasajeros = x.CantAdultos + x.CantMenoresAsiento + x.CantMenoresGratis

               });

            if (fechaIdaDesde != string.Empty)
            {
                string[] aux1 = fechaIdaDesde.Split("/");
                var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                result = result.Where(x => x.FechaIda != null && x.FechaIda >= dtDesde);
            }

            if (fechaIdaHasta != string.Empty)
            {
                string[] aux1 = fechaIdaHasta.Split("/");
                var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                result = result.Where(x => x.FechaIda != null && x.FechaIda <= dtHasta);
            }

            if (fechaVueltaDesde != string.Empty)
            {
                string[] aux1 = fechaVueltaDesde.Split("/");
                var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                result = result.Where(x => x.FechaVuelta != null && x.FechaVuelta >= dtDesde);
            }

            if (fechaVueltaHasta != string.Empty)
            {
                string[] aux1 = fechaVueltaHasta.Split("/");
                var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                result = result.Where(x => x.FechaVuelta != null && x.FechaVuelta <= dtHasta);
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

            return result.ToDataSourceResult(take, skip, sort, filter);
        }

    }

    [System.Web.Services.WebMethod(true)]
    public static void Delete(int id)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                var entity = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == id).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.Estado.ToLower() != "cancelado")
                        entity.Estado = "Cancelado";
                    else
                        dbContext.PedidosTraslado.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static string Exportar(string nombreContacto, string operador, string fechaIdaDesde, string fechaIdaHasta, string fechaVueltaDesde
        , string fechaVueltaHasta, string altaDesde, string altaHasta, string pasajero)
    {

        string fileName = "Traslados_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        string path = "/tmp/";
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var dbContext = new ACHEEntities())
                {
                    var result = dbContext.PedidosTraslado
                        .Include("Usuarios").Include("PasajerosPorPedidoTraslado").Include("Servicios").Include("Servicios.Proveedores")
                        .OrderByDescending(x => x.FechaAlta).ToList().AsQueryable();
                    if (nombreContacto != "")
                        result = result.Where(x => x.Usuarios.Contacto.Contains(nombreContacto));
                    if (operador != "")
                        result = result.Where(x => x.Usuarios.Empresa.ToLower().Contains(operador.ToLower()));
                    if (pasajero != "")
                         result = result.Where(x => x.PasajerosPorPedidoTraslado.FirstOrDefault().Nombre.ToLower().Contains(pasajero.ToLower()));

                    if (fechaIdaDesde != string.Empty)
                    {
                        string[] aux1 = fechaIdaDesde.Split("/");
                        var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                        result = result.Where(x => x.FechaIda != null && x.FechaIda >= dtDesde);
                    }

                    if (fechaIdaHasta != string.Empty)
                    {
                        string[] aux1 = fechaIdaHasta.Split("/");
                        var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                        result = result.Where(x => x.FechaIda != null && x.FechaIda <= dtHasta);
                    }

                    if (fechaVueltaDesde != string.Empty)
                    {
                        string[] aux1 = fechaVueltaDesde.Split("/");
                        var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                        result = result.Where(x => x.FechaVuelta != null && x.FechaVuelta >= dtDesde);
                    }

                    if (fechaVueltaHasta != string.Empty)
                    {
                        string[] aux1 = fechaVueltaHasta.Split("/");
                        var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                        result = result.Where(x => x.FechaVuelta != null && x.FechaVuelta <= dtHasta);
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

                    dt = result.ToList().Select(x => new
                    {
                        IDPedido = x.IDPedidoTraslado,
						FechaAlta = x.FechaAlta.ToString("dd/MM/yyyy"),
                        Estado = x.Estado,
                        Tipo = x.TipoServicio == "R" ? "Round Trip" : "One Way",
                        Servicio = x.Servicios.Nombre,
                        Proovedor = x.Servicios.Proveedores.Nombre,
                        Pasajero = x.PasajerosPorPedidoTraslado.FirstOrDefault().Nombre,
                        Operador = x.Usuarios.Empresa.ToUpper(),
                        NombreContacto = x.Usuarios.Contacto.ToUpper(),
                        FechaIda = x.FechaIda.HasValue ? x.FechaIda.Value.ToString("dd/MM/yyyy") : "",
                        FechaVuelta = x.FechaVuelta.HasValue ? x.FechaVuelta.Value.ToString("dd/MM/yyyy") : "",
                        CantPasajeros = x.CantAdultos + x.CantMenoresAsiento + x.CantMenoresGratis,
                        NroFile = x.NroFile,
                        Total = x.Total
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
                return path + fileName + ".xlsx";
            }
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                BasicLog.AppendToFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
                throw e;
            }
        }
        return "";
    }

    public static void generarArchivo(DataTable dt, string path, string fileName)
    {
        var wb = new XLWorkbook();
        wb.Worksheets.Add(dt, "Traslados");
        wb.SaveAs(path + ".xlsx");
    }


    [System.Web.Services.WebMethod(true)]
    public static List<PasajerosViewModel> GetPasajeros(int id)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                var list = new List<PasajerosViewModel>();
                var aux = dbContext.PasajerosPorPedidoTraslado.Where(x => x.IDPedidoTraslado == id)
                    .OrderBy(x => x.Nombre).Select(x => new PasajerosViewModel()
                    {
                        Nombre = x.Nombre,
                        DNI = x.DNI
                    });
                list = aux.ToList();
                return list;
            }
        }
        else
            return null;

    }



    //[System.Web.Services.WebMethod(true)]
    //public static PedidosTrasladoViewModel GetInfo(int idPedido) {
    //    PedidosTrasladoViewModel result = null;
    //    if (HttpContext.Current.Session["AdminUser"] != null) {
    //        var usu = (WebUser)HttpContext.Current.Session["AdminUser"];
    //        using (var dbContext = new ACHEEntities()) {
    //            var traslado = dbContext.PedidosTraslado
    //                .Where(x => x.IDPedidoTraslado == idPedido)
    //                .Select(x => new PedidosTrasladoViewModel() {
    //                    IDPedido = x.IDPedidoTraslado,
    //                    IDUsuario = x.IDUsuario,
    //                    FechaAlta = x.FechaAlta,
    //                    Origen = x.LugaresTrasladosOrigen.Nombre,
    //                    Destino = x.LugaresTrasladosDestino.Nombre,
    //                    FechaIda = x.FechaIda,
    //                    FechaVuelta = x.FechaVuelta,
    //                    Pasajero = x.Pasajero.ToUpper(),
    //                    NombreContacto = x.Usuarios.Contacto.ToUpper(),
    //                    Empresa = x.Usuarios.Empresa.ToUpper(),
    //                    Total = x.Total,
    //                    PagoPor = x.PagoPor != null ? x.PagoPor : "",
    //                    CantidadPasajeros = x.CantAdultos + x.CantMenoresAsiento + x.CantMenoresGratis,
    //                    Estado = x.Estado,
    //                }).FirstOrDefault();
    //            if (traslado != null)
    //                result = traslado;
    //        }
    //    }
    //    return result;
    //}
}