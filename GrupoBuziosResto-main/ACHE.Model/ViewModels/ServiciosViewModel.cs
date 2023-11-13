using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model {
    public class ServiciosViewModel {
        public int IDServicio { get; set; }
        public int IDProveedor { get; set; }
        public string Proveedor { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioRegular { get; set; }
        public decimal PrecioPrivado { get; set; }
        public decimal PrecioRegularNR { get; set; }
        public decimal PrecioPrivadoNR { get; set; }
        public string Activo { get; set; }
        public string Tipo { get; set; }
        public string SubTipo { get; set; }
    }
}
