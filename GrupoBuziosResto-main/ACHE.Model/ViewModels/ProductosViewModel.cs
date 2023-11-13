using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model
{
    public class ProductosViewModel
    {
        public int IDProducto { get; set; }
        public string Tipo { get; set; }
        public string Entrada { get; set; }
        public string PlatoPrincipal { get; set; }
        public string Postre { get; set; }
        public string IncluyeBebida { get; set; }
        public string Activo { get; set; }
        public int Precio { get; set; }
        public int PrecioOperador { get; set; }
        public DateTime FechaAlta { get; set; }
    }

    public class ProductosFrontViewModel
    {
        public int ID { get; set; }
        public string Tipo { get; set; }
        public string PrecioResto { get; set; }
        public string PrecioOperador { get; set; }
        //public string PrecioRestoProxTemp { get; set; }
        //public string PrecioOperadorProxTemp { get; set; }

        public int CantCenas { get; set; }
        public int CantPax { get; set; }
        public string SubTotal { get; set; }
    }
}
