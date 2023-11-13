using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class admin_modulos_custom_lugaresTraslados : AdminBasePage {
    
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack)
            LoadInfo();
    }

    private void LoadInfo() {
        using (var dbContext = new ACHEEntities()) {
            var proveedores = dbContext.Proveedores.Where(x => x.Activo).ToList();
            if (proveedores != null && proveedores.Count() > 0) {
                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DataValueField = "IDProveedor";
                cmbProveedor.DataTextField = "Nombre";
                cmbProveedor.DataBind();

                cmbProveedor.Items.Insert(0, new ListItem("", "0"));
            }
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter) {
        if (HttpContext.Current.Session["AdminUser"] != null) {
            using (var dbContext = new ACHEEntities()) {
                return dbContext.LugaresTraslados
                    .Include("Proveedores")
                    .OrderBy(x => x.Nombre)
                    .Select(x => new LugaresTrasladosViewModel() {
                        IDLugarTraslado = x.IDLugarTraslado,
                        IDProveedor = x.IDProveedor,
                        Proveedor = x.Proveedores.Nombre,
                        Nombre = x.Nombre,
                        Tipo = x.Tipo == "O" ? "Origen" : "Destino",
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
                var entity = dbContext.LugaresTraslados.Where(x => x.IDLugarTraslado == id).FirstOrDefault();
                if (entity != null) {
                    if (entity.PedidosTraslado.Any() || entity.PedidosTraslado1.Any())
                        throw new Exception("El lugar de traslado está asociado a uno o más pedidos");
                    dbContext.LugaresTraslados.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}