using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ACHE.Model
{
    public class CartItem
    {
        public int IDProducto { get; set; }
        public string Tipo { get; set; }
        public int CantCenas { get; set; }
        public int CantPax { get; set; }
        public decimal PrecioResto { get; set; }
        public decimal PrecioOperador { get; set; }
    //    public decimal PrecioRestoProxTemp { get; set; }
    //    public decimal PrecioOperadorProxTemp { get; set; }
        public List<PreciosProductos> preciosProductos { get; set; }

        public decimal Total
        {
            get { return (CantCenas * CantPax) * PrecioOperador; }
        }
/*
        public decimal TotalProxTemp
        {
            get { return (CantCenas * CantPax) * PrecioOperadorProxTemp; }
        }
        */
        public CartItem(int productId)
        {
            this.IDProducto = productId;
        }
    }

    public class ShoppingCart
    {
        #region Properties

        public List<CartItem> Items { get; set; }
        public int IDPedido { get; set; }
        public string Nombre { get; set; }
        public decimal Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string NroDoc { get; set; }
        public string Pasajero { get; set; }

        public string Observaciones { get; set; }

        #endregion

        public static ShoppingCart Instance;

        //#region Singleton Implementation
        // Readonly properties can only be set in initialization or in a constructor  
        //public static ShoppingCart Instance;

        public static ShoppingCart RetrieveShoppingCart()
        {
            if (HttpContext.Current.Session["ASPNETShoppingCart"] == null)
            {
                Instance = new ShoppingCart();
                Instance.Items = new List<CartItem>();
                HttpContext.Current.Session["ASPNETShoppingCart"] = Instance;
            }
            else
            {
                Instance = (ShoppingCart)HttpContext.Current.Session["ASPNETShoppingCart"];
            }

            return Instance;
        }
        
        public static decimal SubTotalProducto(int IdProducto)
        {
           // if (ShoppingCart.RetrieveShoppingCart().FechaDesde.HasValue)
            //{
                                return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).Total;

                /*
                var dtDesde = ShoppingCart.RetrieveShoppingCart().FechaDesde.Value;
                var fechaCambioTemp = DateTime.Parse(ConfigurationManager.AppSettings["GBR.CambioTemporada"]);
                if (dtDesde >= fechaCambioTemp)
                    return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).TotalProxTemp;
                else
                    return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).Total;
            }
            else
                return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).Total;*/
            /*if (dtDesde.Year == DateTime.Now.Year)
            {
                if (dtDesde.Month == 12)
                {
                    if (dtDesde.Day > 19)
                        return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).TotalProxTemp;
                    else
                        return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).Total;
                }
                else
                    return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).Total;
            }
            else if (dtDesde.Year > DateTime.Now.Year)
                return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).TotalProxTemp;
            else
                return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).Total;
        }
        else
            return ShoppingCart.RetrieveShoppingCart().Items.First(x => x.IDProducto == IdProducto).Total;*/
        }

        public static decimal GetTotal()
        {
            decimal total = 0;
            if (ShoppingCart.RetrieveShoppingCart().FechaDesde.HasValue)
            {
                var dtDesde = ShoppingCart.RetrieveShoppingCart().FechaDesde.Value;
                foreach (CartItem item in ShoppingCart.RetrieveShoppingCart().Items)
                {
               //     var fechaCambioTemp = DateTime.Parse(ConfigurationManager.AppSettings["GBR.CambioTemporada"]);
                  //  if (dtDesde >= fechaCambioTemp)
                   //     total += item.TotalProxTemp;
                 //   else
                        total += item.Total;
                    
                    /*if (dtDesde.Year == DateTime.Now.Year)
                    {
                        if (dtDesde.Month == 12)
                        {
                            if (dtDesde.Day > 19)
                                total += item.TotalProxTemp;
                            else
                                total += item.Total;
                        }
                        else
                            total += item.Total;
                    }
                    else if (dtDesde.Year > DateTime.Now.Year)
                        total += item.TotalProxTemp;
                    else
                        total += item.Total;*/
                }
            }

            return total;
        }

        // A protected constructor ensures that an object can't be created from outside  
        protected ShoppingCart() { }

        //#endregion
        public static decimal GetPrecioOperador(List<PreciosProductos> preciosProductos, DateTime dtDesde, DateTime dtHasta, decimal precioOp)
        {
            decimal precio = 0;

            if (preciosProductos != null)
            {

                if (preciosProductos != null)
                {
                    precio = preciosProductos.Where(x => (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).OrderByDescending(x => x.FechaDesde).Select(x => x.PrecioOperador).FirstOrDefault();
                    if (precio == 0)
                    {
                        precio = precioOp;

                    }
                }
                else
                {
                    precio = precioOp;
                }


                /*   if (precio == 0)
                {
                    precio = preciosProductos.Where(x => x.FechaDesde >= dtHasta).OrderByDescending(x => x.FechaDesde).Select(x => x.PrecioOperador).FirstOrDefault();

                }*/

            }
            return precio;
        }
        public static decimal GetPrecioResto(List<PreciosProductos> preciosProductos, DateTime dtDesde, DateTime dtHasta, decimal precioResto)
        {
            decimal precio = 0;
             
            if (preciosProductos != null)
            {
                precio = preciosProductos.Where(x => (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).OrderByDescending(x => x.FechaDesde).Select(x => x.PrecioRestaurante).FirstOrDefault();
                if (precio == 0)
                {
                    precio = precioResto;

                }
            }
            else
                precio = precioResto;
            return precio;
        }

        #region Item Modification Methods

        public void AddItem(int productId, int cantCenas, int cantPax)
        {
            // If this item already exists in our list of items, increase the quantity  
            // Otherwise, add the new item to the list  
            if (Items.Exists(x => x.IDProducto == productId))
            {
                foreach (CartItem item in ShoppingCart.RetrieveShoppingCart().Items)
                {
                    if (item.IDProducto == productId)
                    {
                        item.CantCenas = cantCenas;
                        item.CantPax = cantPax;
                        return;
                    }
                }
            }
            else
            {
                // Create a new item to add to the cart  
                CartItem newItem = new CartItem(productId);
                newItem.CantCenas = cantCenas;
                newItem.CantPax = cantPax;
                var user = (WebUser)HttpContext.Current.Session["CurrentUser"];
                using (var dbContext = new ACHEEntities())
                {
                    var prod = dbContext.Productos.Where(x => x.IDProducto == newItem.IDProducto).First();
                    //newItem.Nombre = prod.Nombre;
                    //newItem.Codigo = prod.Codigo;
                    //newItem.Imagen = prod.Foto1;
                    newItem.Tipo = prod.Tipo == "T" ? "Turista" : prod.Tipo == "P" ? "Premium" : prod.Tipo == "C" ? "Clasico" : (prod.Tipo == "M") ? "Menores" : prod.Tipo == "L" ? "Buffet Libre" : "Playa";
                    if (prod.PreciosProductos.Any())
                    {
                        newItem.preciosProductos = prod.PreciosProductos.ToList();
                    }
                  
                    newItem.PrecioOperador = prod.PrecioOperador;
                    newItem.PrecioResto = prod.Precio;
                  //  newItem.PrecioOperadorProxTemp = prod.PrecioOperadorProxTemp;
                  //  newItem.PrecioRestoProxTemp = prod.PrecioProxTemp;
                }

                ShoppingCart.RetrieveShoppingCart().Items.Add(newItem);
            }
        }

        /** 
         * SetItemQuantity() - Changes the quantity of an item in the cart 
         */
        public void SetItemQuantity(int productId, int cantCenas, int cantPax)
        {
            // Find the item and update the quantity  
            foreach (CartItem item in ShoppingCart.RetrieveShoppingCart().Items)
            {
                if (item.IDProducto == productId)
                {
                    item.CantCenas = cantCenas;
                    item.CantPax = cantPax;
                    return;
                }
            }
        }

        /** 
         * RemoveItem() - Removes an item from the shopping cart 
         */
        public void RemoveItem(int productId)
        {
            var removedItem = ShoppingCart.RetrieveShoppingCart().Items
                    .Where(x => x.IDProducto == productId)
                    .First();
            ShoppingCart.RetrieveShoppingCart().Items.Remove(removedItem);
        }

        public int GetTotalProductos()
        {
            return ShoppingCart.RetrieveShoppingCart().Items.Sum(x => x.CantCenas * x.CantPax);
        }

        #endregion
    }

    public class Pasajeros
    {
        public string Nombre { get; set; }
        public string DNI { get; set; }
     
    }

    public class TrasladosCart
    {
        public int IDUsuario { get; set; }
        public int IDServicio { get; set; }
        public int CantAdultos { get; set; }
        public int Cantmenores { get; set; }
        public int Cantmenores2 { get; set; }
        public DateTime? FechaIda { get; set; }
        public DateTime? FechaVuelta { get; set; }
        public string NroFile { get; set; }
        public string PagoPor { get; set; }
        public string TipoServicio { get; set; }
        public decimal Total { get; set; }
        public string AerolineaArribo { get; set; }
        public string VueloArribo { get; set; }
        public string HoraArribo { get; set; }
        public string AerolineaPartida { get; set; }
        public string VueloPartida { get; set; }
        public string HoraPartida { get; set; }
        public string Observaciones { get; set; }
        public int? IDAeropuertoOrigen { get; set; }
        public int? IDAeropuertoDestino { get; set; }
        public decimal Precio { get; set; }
        public string DireccionHotel1 { get; set; }
        public string DireccionHotel2 { get; set; }
        public string DireccionHotel3 { get; set; }
        public string DireccionHotel4 { get; set; }
        public string Hotel1 { get; set; }
        public string Hotel2 { get; set; }
        public string Hotel3 { get; set; }
        public string Hotel4 { get; set; }
        public int? IDPedido { get; set; }



        public List<Pasajeros> Items { get; set; }

        public static TrasladosCart Instance;
        public static TrasladosCart RetrieveTrasladosCart()
        {
            if (HttpContext.Current.Session["ASPNETTraslados"] == null)
            {
                Instance = new TrasladosCart();
                Instance.Items = new List<Pasajeros>();
                HttpContext.Current.Session["ASPNETTraslados"] = Instance;
            }
            else
            {
                Instance = (TrasladosCart)HttpContext.Current.Session["ASPNETTraslados"];
            }

            return Instance;
        }

        public decimal getPrecioPrivado(int idServicio,  DateTime? dtDesde, DateTime? dtHasta,ACHEEntities dbContext)
        {
            decimal precio = 0;
            if (dtDesde.HasValue && dtHasta.HasValue)//USO MISMO CRITERIO QUE GRUPO RESTO
            {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)))
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).Select(x => x.PrecioPrivado).FirstOrDefault();
                else
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).Select(x => x.PrecioPrivado).FirstOrDefault();
            }
            else if (dtDesde.HasValue) {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)))
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)).Select(x => x.PrecioPrivado).FirstOrDefault();
                else
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)).Select(x => x.PrecioPrivado).FirstOrDefault();
            }
            else {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)))
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)).Select(x => x.PrecioPrivado).FirstOrDefault();
                else
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)).Select(x => x.PrecioPrivado).FirstOrDefault();
            }
            return precio;
        }

        public decimal getPrecioRegular(int idServicio, DateTime? dtDesde, DateTime? dtHasta, ACHEEntities dbContext)
        {
            WebUser logueado = (WebUser)HttpContext.Current.Session["CurrentUser"];
            decimal precio = 0;
            if (dtDesde.HasValue && dtHasta.HasValue)//USO MISMO CRITERIO QUE GRUPO RESTO
            {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && x.IDUsuario == logueado.ID && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)))
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).Select(x => x.PrecioRegular).FirstOrDefault();
                else
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).Select(x => x.PrecioRegular).FirstOrDefault();
            }
            else if (dtDesde.HasValue) 
            {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && x.IDUsuario == logueado.ID && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)))
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)).Select(x => x.PrecioRegular).FirstOrDefault();
                else
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)).Select(x => x.PrecioRegular).FirstOrDefault();
            }
            else {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && x.IDUsuario == logueado.ID && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)))
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)).Select(x => x.PrecioRegular).FirstOrDefault();
                else
                    precio = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)).Select(x => x.PrecioRegular).FirstOrDefault();
            }
            return precio;
        }



        public string getObservacionesRegular(int idServicio, DateTime? dtDesde, DateTime? dtHasta, ACHEEntities dbContext)
        {
            string obs ="";
            if (dtDesde.HasValue && dtHasta.HasValue)//USO MISMO CRITERIO QUE GRUPO RESTO
            {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)))
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).Select(x => x.ObservacionesRegular).FirstOrDefault();
                else
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).Select(x => x.ObservacionesRegular).FirstOrDefault();
            }
            else if (dtDesde.HasValue) {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)))
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)).Select(x => x.ObservacionesRegular).FirstOrDefault();
                else
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)).Select(x => x.ObservacionesRegular).FirstOrDefault();
            }
            else {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)))
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)).Select(x => x.ObservacionesRegular).FirstOrDefault();
                else
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)).Select(x => x.ObservacionesRegular).FirstOrDefault();
            }
            return obs;
        }
        public string getObservacionesPrivado(int idServicio, DateTime? dtDesde, DateTime? dtHasta, ACHEEntities dbContext)
        {
            string obs = "";
            if (dtDesde.HasValue && dtHasta.HasValue)//USO MISMO CRITERIO QUE GRUPO RESTO
            {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)))
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).Select(x => x.ObservacionesPrivado).FirstOrDefault();
                else
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaHasta >= dtDesde) && (x.FechaDesde <= dtDesde || x.FechaDesde <= dtHasta)).Select(x => x.ObservacionesPrivado).FirstOrDefault();

            }
            else if (dtDesde.HasValue) {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)))
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)).Select(x => x.ObservacionesPrivado).FirstOrDefault();
                else
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaDesde <= dtDesde) && (x.FechaHasta >= dtDesde)).Select(x => x.ObservacionesPrivado).FirstOrDefault();
            }
            else {
                if (dbContext.PreciosServicios.Any(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)))
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)).Select(x => x.ObservacionesPrivado).FirstOrDefault();
                else
                    obs = dbContext.PreciosServicios.Where(x => x.IDServicio == idServicio && !x.IDUsuario.HasValue && (x.FechaDesde <= dtHasta) && (x.FechaHasta >= dtHasta)).Select(x => x.ObservacionesPrivado).FirstOrDefault();
            }
            return obs;
        }

        public void add(string Nombre,string DNI)
        {
            Pasajeros newItem = new Pasajeros();
            newItem.Nombre = Nombre;
            newItem.DNI = DNI;

            TrasladosCart.RetrieveTrasladosCart().Items.Add(newItem);

        }

      
    }

}