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
    
    public partial class PedidosTraslado
    {
        public PedidosTraslado()
        {
            this.PasajerosPorPedidoTraslado = new HashSet<PasajerosPorPedidoTraslado>();
        }
    
        public int IDPedidoTraslado { get; set; }
        public string Estado { get; set; }
        public int CantAdultos { get; set; }
        public int CantMenoresAsiento { get; set; }
        public int CantMenoresGratis { get; set; }
        public Nullable<System.DateTime> FechaIda { get; set; }
        public Nullable<System.DateTime> FechaVuelta { get; set; }
        public string TipoServicio { get; set; }
        public decimal Total { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public string AerolineaArribo { get; set; }
        public string VueloArribo { get; set; }
        public string HoraArribo { get; set; }
        public string AerolineaPartida { get; set; }
        public string VueloPartida { get; set; }
        public string HoraPartida { get; set; }
        public string EmailEnvio { get; set; }
        public string PagoPor { get; set; }
        public string Observaciones { get; set; }
        public int IDUsuario { get; set; }
        public int IDServicio { get; set; }
        public Nullable<int> IDLugarOrigen { get; set; }
        public Nullable<int> IDLugarDestino { get; set; }
        public decimal Precio { get; set; }
        public string DireccionHotel1 { get; set; }
        public string DireccionHotel2 { get; set; }
        public string DireccionHotel3 { get; set; }
        public string Hotel1 { get; set; }
        public string Hotel2 { get; set; }
        public string Hotel3 { get; set; }
        public string Hotel4 { get; set; }
        public string DireccionHotel4 { get; set; }
        public string NroFile { get; set; }
    
        public virtual LugaresTraslados LugaresTrasladosOrigen { get; set; }
        public virtual LugaresTraslados LugaresTrasladosDestino { get; set; }
        public virtual ICollection<PasajerosPorPedidoTraslado> PasajerosPorPedidoTraslado { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        public virtual Servicios Servicios { get; set; }
    }
}
