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
//using ClosedXML.Excel;
using System.Configuration;
using System.Collections.Specialized;

public partial class admin_modulos_custom_usuariosPendientes : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                return dbContext.Usuarios.Where(x => x.TipoUsuario.ToUpper() == "O"
                    && x.Estado.ToUpper() == "P")
                    .OrderByDescending(x => x.FechaAlta).ThenBy(x => x.Empresa)
                    .Select(x => new UsuariosPendientesViewModel()
                    {
                        IDUsuario = x.IDUsuario,
                        TipoUsuario = "Operador",
                        Email = x.Email.ToLower(),
                        Empresa = x.Empresa,
                        NombreContacto = x.Contacto,
                        Activo = x.Activo ? "Si" : "No",
                        FechaAlta = x.FechaAlta,
                        Estado = "Pendiente",

                    }).ToDataSourceResult(take, skip, sort, filter);//.ToList();
            }
        }
        else
            return null;
    }

    [System.Web.Services.WebMethod(true)]
    public static UsuariosPendientesViewModel GetInfo(int id)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            //UsuariosPendientesViewModel usuario;
            using (var dbContext = new ACHEEntities())
            {
                var usuario = dbContext.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault();

                var info = new UsuariosPendientesViewModel();
                if (usuario != null)
                {
                    info.IDUsuario = usuario.IDUsuario;
                    info.TipoUsuario = "Operador";
                    info.Email = usuario.Email.ToLower();
                    info.Empresa = usuario.Empresa;
                    info.NombreContacto = usuario.Contacto;
                    info.Direccion = usuario.Direccion;
                    info.Telefono = usuario.Telefono;
                    info.Activo = usuario.Activo ? "Si" : "No";
                    info.FechaAltaString = usuario.FechaAlta.ToShortDateString();
                    info.Estado = "Pendiente";
                    info.Observaciones = usuario.Observaciones;
                }

                return info;
            }
        }
        else
            return null;
    }

    [System.Web.Services.WebMethod(true)]
    public static void Rechazar(int id, string motivo, string obs)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {

                var entity = dbContext.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault();
                if (entity != null)
                {
                    entity.Estado = "R";
                    dbContext.SaveChanges();

                    ListDictionary replacements = new ListDictionary();
                    replacements.Add("<USUARIO>", entity.Contacto);
                    replacements.Add("<MENSAJE>", "Su solicitud de registro ha sido denegada por el siguiente motivo: " + motivo);

                    bool send = EmailHelper.SendMessage(EmailTemplate.SolicitudRegistro, replacements, entity.Email, "GrupoBuziosResto - Solicitud denegada");
                }

            }
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static void Aprobar(int id, string obs)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {

                var entity = dbContext.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault();
                if (entity != null)
                {
                    entity.Estado = "A";
                    entity.Activo = true;
                    dbContext.SaveChanges();

                    ListDictionary replacements = new ListDictionary();
                    replacements.Add("<USUARIO>", entity.Contacto);
                    replacements.Add("<MENSAJE>", "Su solicitud de registro ha sido aprobada. Ya puede comenzar a comprar por el sitio");

                    bool send = EmailHelper.SendMessage(EmailTemplate.SolicitudRegistro, replacements, entity.Email, "GrupoBuziosResto - Solicitud aprobada");
                }

            }
        }
    }

    //[System.Web.Services.WebMethod(true)]
    //public static string Exportar(string nombre, string email)
    //{
    //    string fileName = "Usuarios";
    //    string path = "/tmp/";
    //    if (HttpContext.Current.Session["AdminUser"] != null)
    //    {
    //        try
    //        {
    //            DataTable dt = new DataTable();
    //            using (var dbContext = new ACHEEntities())
    //            {

    //                return dbContext.Usuarios.Where(x => x.TipoUsuario.ToUpper() == "O"
    //                && x.Estado.ToUpper() == "P")
    //                .OrderByDescending(x => x.FechaAlta).ThenBy(x => x.Empresa)
    //                .Select(x => new UsuariosPendientesViewModel()
    //                {
    //                    IDUsuario = x.IDUsuario,
    //                    TipoUsuario = "Operador",
    //                    Email = x.Email.ToLower(),
    //                    Empresa = x.Empresa,
    //                    NombreContacto = x.Contacto,
    //                    Activo = x.Activo ? "Si" : "No",
    //                    FechaAlta = x.FechaAlta,
    //                    Estado = "Pendiente",

    //                }).ToDataSourceResult(take, skip, sort, filter);//.ToList();

    //                var info = dbContext.Usuarios.Where(x => x.TipoUsuario.ToUpper() == "O" && x.Estado.ToUpper() == "P")
    //                    .OrderByDescending(x => x.Apellido).AsEnumerable();
    //                if (nombre != "")
    //                    info = info.Where(x => x.Nombre.ToLower().Contains(nombre.ToLower()) || x.Apellido.ToLower().Contains(nombre.ToLower()));
    //                if (email != "")
    //                    info = info.Where(x => x.Email.ToLower().Contains(email.ToLower()));


    //                dt = info.Select(x => new
    //                {
    //                    FechaAlta = x.FechaAlta,
    //                    Estado = x.Estado,
    //                    Apellido = x.Apellido,
    //                    Nombre = x.Nombre,
    //                    DNI = x.DNI,
    //                    Sexo = x.Sexo,
    //                    Email = x.Email,
    //                    Telefono = x.Telefono,
    //                    Celular = x.Celular,
    //                    Pais = x.Pais,
    //                    Provincia = x.Provincia,
    //                    Ciudad = x.Ciudad,
    //                    Calle = x.Calle,
    //                    Altura = x.Altura,
    //                    Depto = x.Depto,
    //                    CP = x.CP,
    //                    CUIT = x.CUIT,
    //                    CuitFc = x.CUITFactura,
    //                    HorarioDeContacto = x.HorarioDeContacto,
    //                    TieneLocal = x.TieneLocal,
    //                    Ubicacion = x.UbicacionLocal,
    //                    OtrasMarcasQueTrabaja = x.OtrasMarcasQueTrabaja,
    //                    ComoNosConociste = x.ComoNosConociste,
    //                    TipoEmprendimiento = x.TipoEmprendimiento,
    //                    Observaciones = x.Observaciones
    //                }).ToList().ToDataTable();
    //            }

    //            if (dt.Rows.Count > 0)
    //            {
    //                //generarArchivo(dt, HttpContext.Current.Server.MapPath(path) + Path.GetFileName(fileName), fileName);
    //            }
    //            else
    //            {
    //                throw new Exception("No se encuentran datos para los filtros seleccionados");
    //            }
    //            return path + fileName + "_" + DateTime.Now.ToString("yyymmdd") + ".xlsx";
    //        }
    //        catch (Exception e)
    //        {
    //            var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
    //            BasicLog.AppendToFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
    //            throw e;
    //        }
    //    }
    //    return "";
    //}

    //public static void generarArchivo(DataTable dt, string path, string fileName)
    //{
    //    var wb = new XLWorkbook();
    //    wb.Worksheets.Add(dt, fileName);
    //    wb.SaveAs(path + "_" + DateTime.Now.ToString("yyymmdd") + ".xlsx");
    //}
}