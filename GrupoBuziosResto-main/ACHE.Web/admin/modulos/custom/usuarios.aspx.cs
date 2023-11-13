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

public partial class admin_modulos_custom_usuarios : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod(true)]
    public static void Delete(int id)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                var entity = dbContext.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.Pedidos.Any())
                    {
                        entity.Activo = false;
                        dbContext.SaveChanges();
                        throw new Exception("El operador tiene pedidos realizados. Se procedió a desactivarlo.");
                    }
                    else
                    {
                        dbContext.Usuarios.Remove(entity);
                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                return dbContext.Usuarios
                    .Where(x => x.Activo && x.Estado == "A")
                    .OrderByDescending(x => x.FechaAlta)
                    .Select(x => new UsuariosViewModel()
                    {
                        IDOperador = x.IDUsuario,
                        Empresa = x.Empresa.ToUpper(),
                        NombreContacto = x.Contacto.ToUpper(),
                        Email = x.Email.ToLower(),
                        FechaAlta = x.FechaAlta,
                        Activo = x.Activo ? "Si" : "No",
                    }).ToDataSourceResult(take, skip, sort, filter);//.ToList();
            }
        }
        else
            return null;
    }

    [System.Web.Services.WebMethod(true)]
    public static UsuariosViewModel GetInfo(int id)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            UsuariosViewModel usuario;
            using (var dbContext = new ACHEEntities())
            {
                usuario = dbContext.Usuarios.Where(x => x.IDUsuario == id)
                    .Select(x => new UsuariosViewModel()
                    {
                        IDOperador = x.IDUsuario,
                        Empresa = x.Empresa.ToUpper(),
                        NombreContacto = x.Contacto.ToUpper(),
                        Email = x.Email.ToLower(),
                        FechaAlta = x.FechaAlta,
                        Direccion =  x.Direccion,
                        Telefono = x.Telefono,
                        Pwd = x.Pwd,
                        Observaciones = x.Observaciones,
                        Activo = x.Activo ? "Si" : "No",
                        ServiciosEspeciales = x.ServiciosEspeciales                        
                    }).First();

                var a = DateTime.Parse(usuario.FechaAlta.ToString()).ToShortDateString();
                if (a != null)
                {
                    usuario.FechaAltaString = a;
                }
            }

            return usuario;
        }
        else
            return null;
    }

    [System.Web.Services.WebMethod(true)]
    public static void Save(int id,bool chkServiciosEsp) {
        try{
            if (HttpContext.Current.Session["AdminUser"] != null) {
                Usuarios usuario;
                using (var dbContext = new ACHEEntities()) {
                    usuario = dbContext.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault();
                    if (usuario != null) {
                        usuario.ServiciosEspeciales = chkServiciosEsp;
                        dbContext.SaveChanges();
                    }
                }

            }
        }
        catch (Exception e)
        {
            var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
            BasicLog.AppendToFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
            throw e;
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static string Exportar(string empresa, string nombreContacto, string email)
    {
        string fileName = "Operadores";
        string path = "/tmp/";
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var dbContext = new ACHEEntities())
                {
                    var info = dbContext.Usuarios.OrderByDescending(x => x.FechaAlta).Where(x => x.Activo && x.Estado == "A").AsEnumerable();
                    if (empresa != "")
                        info = info.Where(x => x.Empresa.ToLower().Contains(empresa.ToLower()));
                    if (nombreContacto != "")
                        info = info.Where(x => x.Contacto.ToLower().Contains(nombreContacto.ToLower()));
                    if (email != "")
                        info = info.Where(x => x.Email.ToLower().Contains(email.ToLower()));

                    dt = info.Select(x => new
                    {
                        Empresa = x.Empresa.ToUpper(),
                        NombreContacto = x.Contacto.ToUpper(),
                        Email = x.Email.ToLower(),
                        FechaAlta = x.FechaAlta.ToShortDateString(),
                        Direccion = x.Direccion,
                        Telefono = x.Telefono,
                        Pwd = x.Pwd,
                        Observaciones = x.Observaciones,
                        Activo = x.Activo ? "Si" : "No",
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
                return path + fileName + "_" + DateTime.Now.ToString("yyymmdd") + ".xlsx";
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
        wb.Worksheets.Add(dt, fileName);
        wb.SaveAs(path + "_" + DateTime.Now.ToString("yyymmdd") + ".xlsx");
    }
}