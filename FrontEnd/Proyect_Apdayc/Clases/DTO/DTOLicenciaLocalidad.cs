using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOLicenciaLocalidad
    {
        //
        // GET: /DTOLicenciaLocalidad/
        public decimal idLicenciaLocalidad { get; set; }
        public decimal CodigoLicencia { get; set; }
        public string CodigoTipoAforo { get; set; }
        public string TipoAforo { get; set; }
        public decimal CodigoTipoLocalidad { get; set; }
        public string TipoLocalidad { get; set; }
        public string Funcion { get; set; }
        public string Color { get; set; }
        public decimal Ticket { get; set; }
        public decimal PrecVenta { get; set; }
        public decimal ImporteBruto { get; set; }
        public decimal Impuesto { get; set; }
        public decimal ImporteNeto { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }

        public bool Activo { get; set; }
    }
}
