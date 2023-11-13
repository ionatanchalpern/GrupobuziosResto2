using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model
{
    public class UsuariosPendientesViewModel
    {
        public int IDUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public string NombreContacto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public string FechaAltaString { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
    }
}
