using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model {

    public class PedidosTrasladoViewModel {
        public int IDPedido { get; set; }
        public string Estado { get; set; }
        public int IDUsuario { get; set; }
        public DateTime FechaAlta { get; set; }
      //  public string Origen { get; set; }
       // public string Destino{ get; set; }
        public DateTime? FechaIda { get; set; }
        public DateTime? FechaVuelta { get; set; }
        public decimal Total { get; set; }
        public string Tipo { get; set; }
        public string Pasajero { get; set; }
       // public string Empresa { get; set; }
        //public string PagoPor { get; set; }
        public string Operador { get; set; }
        public string Proovedor { get; set; }
        public string Servicio { get; set; }
        public string NombreContacto { get; set; }
       // public int CantMenores { get; set; }
       // public int CantAdultos { get; set; }
        public int CantPasajeros { get; set; }
        public string NroFile { get; set; }
        public string PagoPor { get; set; }
    }
}
