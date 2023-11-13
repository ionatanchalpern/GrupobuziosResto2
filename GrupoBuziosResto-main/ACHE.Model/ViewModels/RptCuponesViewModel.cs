using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model
{
    public class RptCuponesViewModel
    {
        public int IDPedido { get; set; }
        public DateTime FechaValidacion { get; set; }
        public DateTime EstadiaDesde { get; set; }
        public DateTime EstadiaHasta { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public string Pasajero { get; set; }
        public string Restaurant { get; set; }
        public string Operador { get; set; }
        public decimal Precio { get; set; }
        public string Validado { get; set; }
        public string PagoPor { get; set; }
    }
}
