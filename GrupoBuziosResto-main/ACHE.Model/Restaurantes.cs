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
    
    public partial class Restaurantes
    {
        public Restaurantes()
        {
            this.Menues = new HashSet<Menues>();
            this.PedidosDetalle = new HashSet<PedidosDetalle>();
        }
    
        public int IDRestaurant { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string HorarioAtencion { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public string Observaciones { get; set; }
        public string Logo { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string ImagenMapa { get; set; }
    
        public virtual ICollection<Menues> Menues { get; set; }
        public virtual ICollection<PedidosDetalle> PedidosDetalle { get; set; }
    }
}