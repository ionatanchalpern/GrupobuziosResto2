using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model
{
    public class MenuesViewModel
    {
        public int IDMenu { get; set; }
        public int IDRestaurant { get; set; }
        public string Restaurant { get; set; }
        public string TipoMenu { get; set; }
        public string Entrada { get; set; }
        public string PlatoPrincipal { get; set; }
        public string Postre { get; set; }
        public string IncluyeBebida { get; set; }
        public string Imagen { get; set; }
    }
}
