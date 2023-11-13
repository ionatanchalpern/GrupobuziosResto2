using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model
{
    public class DetalleViewModel
    {
        public int IDDetalle { get; set; }
        public int IDPedido { get; set; }
        public decimal Precio { get; set; }
        public string Validado { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public string Tipo { get; set; }
    }
}
