using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACHE.Model.ViewModels
{
    public class PreciosProductoViewModel
    {
        public int IDPrecioProducto { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioOperador { get; set; }
        public string Tipo { get; set; }

    }                                               
}
