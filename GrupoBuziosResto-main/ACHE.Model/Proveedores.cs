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
    
    public partial class Proveedores
    {
        public Proveedores()
        {
            this.LugaresTraslados = new HashSet<LugaresTraslados>();
            this.Servicios = new HashSet<Servicios>();
        }
    
        public int IDProveedor { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Logo { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
    
        public virtual ICollection<LugaresTraslados> LugaresTraslados { get; set; }
        public virtual ICollection<Servicios> Servicios { get; set; }
    }
}