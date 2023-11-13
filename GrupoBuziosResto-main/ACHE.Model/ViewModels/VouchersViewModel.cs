using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACHE.Model
{
    public class VouchersViewModel
    {
        public int Codigo { get; set; }
        public int? DigitoVerificador { get; set; }
        public string CodigoString { get; set; }
        public string Nombre { get; set; }
        public string NroDocumento { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string FechaDesdeString { get; set; }
        public string FechaHastaString { get; set; }
        public string Entrada { get; set; }
        public string Principal { get; set; }
        public string Postre { get; set; }
        public string Bebida { get; set; }
        public string Asociados { get; set; }
        public string TipoMenu { get; set; }
        public string Excepto { get; set; }
        public string Texto { get; set; }
        public string Excepciones { get; set; }
        public string HorarioAtencion { get; set; }
        public string HorarioAtencionDatosUtiles { get; set; }
        public string Ubicacion { get; set; }

    }
}
