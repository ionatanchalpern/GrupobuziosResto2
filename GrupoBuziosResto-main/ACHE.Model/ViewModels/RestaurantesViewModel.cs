using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHE.Model
{
    public class RestaurantesViewModel
    {
        public int IDRestaurant { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string HorarioAtencion { get; set; }
        public string Telefono { get; set; }
        public string Activo { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
