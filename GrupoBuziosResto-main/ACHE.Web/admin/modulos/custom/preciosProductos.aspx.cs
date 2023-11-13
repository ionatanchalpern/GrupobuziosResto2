using ACHE.Extensions;
using ACHE.Model;
using ACHE.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_modulos_custom_preciosProductos : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

 
    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter, string fechaDesde, string fechaHasta)
    {
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                var result = dbContext.PreciosProductos.Include("Productos")
                    .OrderBy(x => x.FechaDesde)
                    .Select(x => new PreciosProductoViewModel()
                    {
                        IDPrecioProducto = x.IDPrecioProducto,
                        Tipo = x.Productos.Tipo == "T" ? "Turista" : (x.Productos.Tipo == "P") ? "Premium" : (x.Productos.Tipo == "C") ? "Clasico" : (x.Productos.Tipo == "M") ? "Menores" : (x.Productos.Tipo == "L") ? "Buffet Libre" : "Playa",

                        Precio = x.PrecioRestaurante,
                        PrecioOperador = x.PrecioOperador,
                        FechaDesde = x.FechaDesde,
                        FechaHasta = x.FechaHasta

                    });//.ToList()
                if (fechaDesde != string.Empty)
                {
                    string[] aux1 = fechaDesde.Split("/");
                    var dtDesde = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);
                    result = result.Where(x => x.FechaDesde >= dtDesde);
                }

                if (fechaHasta != string.Empty)
                {
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

}