using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class admin_modulos_custom_proveedores : AdminBasePage {
    
    protected void Page_Load(object sender, EventArgs e) {
    }

    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter) {
        if (HttpContext.Current.Session["AdminUser"] != null) {
            using (var dbContext = new ACHEEntities()) {
                return dbContext.Proveedores
                    .OrderBy(x => x.Nombre)
                    .Select(x => new ProveedoresViewModel() {
                        IDProveedor = x.IDProveedor,
                        Nombre = x.Nombre,
                        Email = x.Email,
                        Activo = x.Activo ? "Si" : "No",
                    }).ToDataSourceResult(take, skip, sort, filter);//.ToList()
            }
        }
        else
            return null;
    }

    [System.Web.Services.WebMethod(true)]
    public static void Delete(int id) {
        if (HttpContext.Current.Session["AdminUser"] != null) {
            using (var dbContext = new ACHEEntities()) {
                var entity = dbContext.Proveedores.Where(x => x.IDProveedor == id).FirstOrDefault();
                if (entity != null) {
                    if (dbContext.PedidosTraslado.Any(x => x.Servicios.IDProveedor == id || x.LugaresTrasladosOrigen.IDProveedor == id|| x.LugaresTrasladosDestino.IDProveedor == id))
                        throw new Exception("El proveedor está asociado a uno o más pedidos");
                    if (entity.Servicios.Any())
                        throw new Exception("El proveedor está asociado a uno o más servicios");
                    if (entity.LugaresTraslados.Any())
                        throw new Exception("El proveedor está asociado a uno o más lugares de traslado");
                    dbContext.Proveedores.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}