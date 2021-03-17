using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOCampaniaAsignarSocioDetalle
    {
        public decimal idLicencia { get; set; }
        public decimal idSocio { get; set; }
        public string Licencia { get; set; }
        public string Establecimiento { get; set; }
        public decimal NetoFact { get; set; }
        public decimal CobradoFact { get; set; }
        public decimal PendienteFact { get; set; }
        public string Moneda { get; set; }
    }
}