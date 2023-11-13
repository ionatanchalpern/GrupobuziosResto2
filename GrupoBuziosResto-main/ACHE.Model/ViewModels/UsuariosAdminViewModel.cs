using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACHE.Model
{

    /// <summary>
    /// Summary description for UsuariosAdminViewModel
    /// </summary>
    public class UsuariosAdminViewModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Activo { get; set; }
        public string Tipo { get; set; }
    }
}