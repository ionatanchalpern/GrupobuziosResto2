using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;

public partial class cart : WebBasePage {

    protected void Page_Load(object sender, EventArgs e) {

    }

    [System.Web.Services.WebMethod(true, CacheDuration = 0)]
    public static void AddItem(int id, int cantCenas, int cantPax) {
        ShoppingCart.RetrieveShoppingCart().AddItem(id, cantCenas, cantPax);
        //HttpContext.Current.Session["CurrentUser"] = null;
    }
    /*
    [System.Web.Services.WebMethod(true, CacheDuration = 0)]
    public static void AddTraslado(string Nombre,string DNI)
    {
        TrasladosCart.RetrieveTrasladosCart().add(Nombre, DNI);
       
    }
   */
    [System.Web.Services.WebMethod(true, CacheDuration = 0)]
    public static void UpdateItem(int id, int cantCenas, int cantPax) {
        ShoppingCart.RetrieveShoppingCart().SetItemQuantity(id, cantCenas, cantPax);
    }

    [System.Web.Services.WebMethod(true, CacheDuration = 0)]
    public static void RemoveItem(int id) {
        ShoppingCart.RetrieveShoppingCart().RemoveItem(id);
    }

    [System.Web.Services.WebMethod(true)]
    public static int ObtenerTotalProductos() {
        return ShoppingCart.RetrieveShoppingCart().GetTotalProductos();
    }

    [System.Web.Services.WebMethod(true)]
    public static string ActualizarSubtotal(int id) {
        return ShoppingCart.SubTotalProducto(id).ToString("N2");
    }

    [System.Web.Services.WebMethod(true, CacheDuration = 0)]
    public static void SetFechas(string desde, string hasta) {
        ShoppingCart.RetrieveShoppingCart().FechaDesde = DateTime.Parse(desde);
        ShoppingCart.RetrieveShoppingCart().FechaHasta = DateTime.Parse(hasta);
        foreach (var item in ShoppingCart.RetrieveShoppingCart().Items)
        {
            var precioOperador = ShoppingCart.GetPrecioOperador(item.preciosProductos, DateTime.Parse(desde), DateTime.Parse(hasta), item.PrecioOperador);
            var precioRes = ShoppingCart.GetPrecioResto(item.preciosProductos, DateTime.Parse(desde), DateTime.Parse(hasta), item.PrecioResto);
            item.PrecioOperador = precioOperador;
            item.PrecioResto = precioRes;
          
        }
    }

    [System.Web.Services.WebMethod(true, CacheDuration = 0)]
    public static bool ValidarFechas() {
        if (ShoppingCart.RetrieveShoppingCart().FechaDesde != null && ShoppingCart.RetrieveShoppingCart().FechaHasta != null)
            return true;
        else
            return false;
    }

    [System.Web.Services.WebMethod(true, CacheDuration = 0)]
    public static bool ValidarItems() {
        if (ShoppingCart.RetrieveShoppingCart().Items.Count() > 0)
            return true;
        else
            return false;
    }
}