using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model {
    public class LugaresTrasladosViewModel {
        public int IDLugarTraslado { get; set; }
        public int IDProveedor { get; set; }
        public string Proveedor { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Activo { get; set; }
    }
}
