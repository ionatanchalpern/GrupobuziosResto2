using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class admin_modulos_custom_servicios : AdminBasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadInfo();
    }

    private void LoadInfo()
    {
        using (var dbContext = new ACHEEntities())
        {
            var proveedores = dbContext.Proveedores.Where(x => x.Activo).ToList();
            if (proveedores != null && proveedores.Count() > 0)
            {
                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DataValueField = "IDProveedor";
                cmbProveedor.DataTextField = "Nombre";
                cmbProveedor.DataBind();

                cmbProveedor.Items.Insert(0, new ListItem("", "0"));
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
                return dbContext.Servicios
                    .Include("Proveedores")
                    .OrderBy(x => x.Nombre)
                    .Select(x => new ServiciosViewModel()
                    {
                        IDServicio = x.IDServicio,
                        IDProveedor = x.IDProveedor,
                        Proveedor = x.Proveedores.Nombre,
                        Nombre = x.Nombre,
                        PrecioRegular = x.PrecioRegular,
                        PrecioPrivado = x.PrecioPrivado,
                        PrecioRegularNR = x.PrecioRegularNoRepresentado,
                        PrecioPrivadoNR = x.PrecioPrivadoNoRepresentado,
                        Activo = x.Activo ? "Si" : "No",
                        Tipo = x.SubTipos.Tipo == "R" ? "Round Trip" : "One Way",
                        SubTipo = x.SubTipos.Tipo == "R" ? x.SubTipos.Lugar1 + " - " + x.SubTipos.Lugar2 + " - " + x.SubTipos.Lugar3 : x.SubTipos.Lugar1 + " - " + x.SubTipos.Lugar2
                    }).ToDataSourceResult(take, skip, sort, filter);//.ToList()
            }
        }
        else
            return null;
    }

    [System.Web.Services.WebMethod(true)]
    public static void Delete(int id)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                var entity = dbContext.Servicios.Where(x => x.IDServicio == id).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.PedidosTraslado.Any())
                        throw new Exception("El servicio está asociado a uno o más traslados");
                    dbContext.Servicios.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}