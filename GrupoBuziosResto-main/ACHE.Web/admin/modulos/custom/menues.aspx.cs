using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class admin_modulos_custom_menues : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cargarRestaurantes();
    }

    private void cargarRestaurantes()
    {
        using (var dbContext = new ACHEEntities())
        {
            var restaurantes = dbContext.Restaurantes.Where(x => x.Activo).ToList();
            if (restaurantes != null)
            {
                cmbRestos.DataSource = restaurantes;
                cmbRestos.DataValueField = "IDRestaurant";
                cmbRestos.DataTextField = "Nombre";
                cmbRestos.DataBind();

                cmbRestos.Items.Insert(0, new ListItem("", "0"));
            }
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter)
    {

        //AdminBasePage.ValidateUser();
        
        if (HttpContext.Current.Session["AdminUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                return dbContext.Menues
                    .Include("Restaurantes")
                    .OrderBy(x => x.IDMenu)
                    .Select(x => new MenuesViewModel()
                    {
                        IDMenu = x.IDMenu,
                        TipoMenu = x.TipoMenu == "T" ? "Turista" : (x.TipoMenu == "P") ? "Premium" : (x.TipoMenu == "C") ? "Clasico" : (x.TipoMenu == "M") ? "Menores" : (x.TipoMenu == "L") ? "Buffet Libre" : "Playa",
                        IDRestaurant = x.IDRestaurant,
                        Restaurant = x.Restaurantes.Nombre,
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
                var entity = dbContext.Menues.Where(x => x.IDMenu == id).FirstOrDefault();
                if (entity != null)
                {
                    dbContext.Menues.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}