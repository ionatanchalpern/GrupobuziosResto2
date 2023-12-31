//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACHE.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pedidos
    {
        public Pedidos()
        {
            this.PedidosDetalle = new HashSet<PedidosDetalle>();
        }
    
        public int IDPedido { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public int IDUsuario { get; set; }
        public decimal Total { get; set; }
        public System.DateTime FechaEstadiaDesde { get; set; }
        public System.DateTime FechaEstadiaHasta { get; set; }
        public string Pasajero { get; set; }
        public string EmailEnvio { get; set; }
        public string NroDocumento { get; set; }
        public string PagoPor { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
        public virtual ICollection<PedidosDetalle> PedidosDetalle { get; set; }
    }
}
