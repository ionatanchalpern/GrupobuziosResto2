using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class admin_modulos_seguridad_usuariosadmin : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.TipoUsuario != "A")
            Response.Redirect("~/admin/home.aspx");
    }

    [System.Web.Services.WebMethod(true)]
    public static void Delete(int id)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {

                var entity = dbContext.UsuariosAdmin.Where(x => x.IDUsuario == id).FirstOrDefault();
                if (entity != null)
                {
                    dbContext.UsuariosAdmin.Remove(entity);
                    dbContext.SaveChanges();
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
                return dbContext.UsuariosAdmin
                    .OrderBy(x => x.Nombre)
                    .Select(x => new UsuariosAdminViewModel()
                    {
                        ID = x.IDUsuario,
                        Nombre = x.Nombre,
                        Email = x.Email,
                        Tipo = x.Tipo,
                        Activo = x.Activo ? "Si" : "No"
                    }).ToDataSourceResult(take, skip, sort, filter);//.ToList();
            }
        }
        else
            return null;
    }
}