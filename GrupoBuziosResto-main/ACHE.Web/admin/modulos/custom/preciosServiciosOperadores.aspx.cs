using ACHE.Extensions;
using ACHE.Model;
using ACHE.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_modulos_custom_preciosServiciosOperadores : AdminBasePage {
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack)
            LoadInfo();
    }
    private void LoadInfo() {
        using (var dbContext = new ACHEEntities()) {
            var proveedores = dbContext.Proveedores.OrderBy(x => x.Nombre).ToList();
            if (proveedores != null && proveedores.Count() > 0) {
                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DataValueField = "IDProveedor";
                cmbProveedor.DataTextField = "Nombre";
                cmbProveedor.DataBind();

                cmbProveedor.Items.Insert(0, new ListItem("", "0"));
            }

            var operadores = dbContext.Usuarios.Where(x => x.Activo).ToList();
            if (operadores != null) {
                cmbOperador.DataSource = operadores;
                cmbOperador.DataValueField = "IDUsuario";
                cmbOperador.DataTextField = "Empresa";
                cmbOperador.DataBind();
                cmbOperador.Items.Insert(0, new ListItem("", ""));
            }
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter, string fechaDesde, string fechaHasta) {
        if (HttpContext.Current.Session["AdminUser"] != null) {
            using (var dbContext = new ACHEEntities()) {
                var result = dbContext.PreciosServicios
                    .Include("Proveedores").Where(x=> x.IDUsuario != null)
                    .OrderBy(x => x.FechaDesde)
                    .Select(x => new PreciosServiciosViewModel() {
                        IDPrecioServicio = x.IDPrecioServicio,
                        IDUsuario = x.IDUsuario ??0,
                        IDProveedor = x.Servicios.IDProveedor,
                        Operador = x.Usuarios.Empresa,
                        Proveedor = x.Servicios.Proveedores.Nombre,
                        Servicio = x.Servicios.Nombre,
                        PrecioRegular = x.PrecioRegular,
                        PrecioPrivado = x.PrecioPrivado,
                        //ImporteNeto = x.ImporteNeto??0,
                        FechaDesde = x.FechaDesde,
                        FechaHasta = x.FechaHasta
                    });//.ToList()
                if (fechaDesde != string.Empty) {
                    string[] aux1 = fechaDesde.Split("/");
                    var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                    result = result.Where(x => x.FechaDesde >= dtDesde);
                }

                if (fechaHasta != string.Empty) {
                    string[] aux1 = fechaHasta.Split("/");
                    var dtHasta = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0] + " 23:59:59 pm");
                    result = result.Where(x => x.FechaHasta <= dtHasta);
                }
                return result.AsQueryable().ToDataSourceResult(take, skip, sort, filter);//.ToList();

            }
        }
        else
            return null;
    }

    [System.Web.Services.WebMethod(true)]
    public static void Delete(int id) {
        if (HttpContext.Current.Session["AdminUser"] != null) {
            using (var dbContext = new ACHEEntities()) {
                var entity = dbContext.PreciosServicios.Where(x => x.IDPrecioServicio == id).FirstOrDefault();
                if (entity != null) {

                    dbContext.PreciosServicios.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}