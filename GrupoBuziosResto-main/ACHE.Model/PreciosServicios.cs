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
    
    public partial class PreciosServicios
    {
        public int IDPrecioServicio { get; set; }
        public Nullable<int> IDServicio { get; set; }
        public System.DateTime FechaDesde { get; set; }
        public System.DateTime FechaHasta { get; set; }
        public decimal PrecioRegular { get; set; }
        public decimal PrecioPrivado { get; set; }
        public string ObservacionesRegular { get; set; }
        public string ObservacionesPrivado { get; set; }
        public Nullable<int> IDUsuario { get; set; }
    
        public virtual Servicios Servicios { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
