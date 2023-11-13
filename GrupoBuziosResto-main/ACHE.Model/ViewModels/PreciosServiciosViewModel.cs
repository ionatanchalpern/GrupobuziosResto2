using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACHE.Model.ViewModels
{
    public class PreciosServiciosViewModel
    {
        public int IDPrecioServicio { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public decimal PrecioRegular { get; set; }
        public decimal PrecioPrivado { get; set; }
        public string Servicio { get; set; }
        public int IDProveedor { get; set; }
        public int IDUsuario { get; set; }
        public string Proveedor { get; set; }
        public string Operador { get; set; }
    }
}
