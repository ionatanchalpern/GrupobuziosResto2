using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class admin_modulos_custom_restaurantes : AdminBasePage
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
                return dbContext.Restaurantes
                    .OrderBy(x => x.Nombre)
                    .Select(x => new RestaurantesViewModel()
                    {
                        IDRestaurant = x.IDRestaurant,
                        Nombre = x.Nombre,
                        HorarioAtencion = x.HorarioAtencion,
                        Direccion = x.Direccion,
                        Email = x.Email,
                        Telefono = x.Telefono,
                        FechaAlta = x.FechaAlta,
                        Activo = x.Activo ? "Si" : "No",
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
                if (dbContext.PedidosDetalle.Any(x => x.IDRestauranteValidacion == id))
                    throw new Exception("El restaurant está asociado a uno o más pedidos");
                else if (dbContext.Menues.Any(x => x.IDRestaurant == id))
                    throw new Exception("El restaurant está asociado a uno o más menues");
                else
                {
                    var entity = dbContext.Restaurantes.Where(x => x.IDRestaurant == id).FirstOrDefault();
                    if (entity != null)
                    {
                        dbContext.Restaurantes.Remove(entity);
                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}