using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model
{
    public class PedidosViewModel
    {
        public int IDPedido { get; set; }
        public int IDUsuario { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime EstadiaDesde { get; set; }
        public DateTime EstadiaHasta { get; set; }
        public decimal Total { get; set; }
        public string Pasajero { get; set; }
        public string Empresa { get; set; }
        public string PagoPor { get; set; }
        public string NombreContacto { get; set; }
        public int Cantidad { get; set; }
        public int Validados { get; set; }
    }
}
