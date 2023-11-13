using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class admin_modulos_custom_productos : AdminBasePage
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
                return dbContext.Productos
                    .OrderBy(x => x.FechaAlta)
                    .Select(x => new ProductosViewModel()
                    {
                        IDProducto = x.IDProducto,
                        Tipo = x.Tipo == "T" ? "Turista" : (x.Tipo == "P") ? "Premium" : (x.Tipo=="C")?"Clasico":(x.Tipo=="M")?"Menores":(x.Tipo=="L")?"Buffet Libre":"Playa",
                        IncluyeBebida = x.IncluyeBebida ? "Si" : "No",
                        Activo = x.Activo ? "Si" : "No",
                        Precio = x.Precio,
                        PrecioOperador = x.PrecioOperador,
                        FechaAlta = x.FechaAlta,
                    }).ToDataSourceResult(take, skip, sort, filter);//.ToList()
            }
        }
        else
            return null;
    }
    //[System.Web.Services.WebMethod(true)]
    //public static void Delete(int id)
    //{
    //    if (HttpContext.Current.Session["AdminUser"] != null)
    //    {
    //        using (var dbContext = new ACHEEntities())
    //        {
    //            var entity = dbContext.Productos.Where(x => x.IDProducto == id).FirstOrDefault();
    //            if (entity != null)
    //            {
    //                dbContext.Productos.Remove(entity);
    //                dbContext.SaveChanges();
    //            }
    //        }
    //    }
    //}
}