using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACHE.Model
{
    public class UsuariosViewModel
    {
        public int IDOperador { get; set; }
        public string NombreContacto { get; set; }
        public string Empresa { get; set; }
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
        public string FechaAltaString { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Pwd { get; set; }
        public string Observaciones { get; set; }
        public string Activo { get; set; }
        public bool ServiciosEspeciales { get; set; }
    }
}
