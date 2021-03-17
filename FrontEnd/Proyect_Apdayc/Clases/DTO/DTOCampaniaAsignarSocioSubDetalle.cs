using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOCampaniaAsignarSocioSubDetalle
    {
        public decimal idPeriodo { get; set; }
        public decimal idLicencia { get; set; }
        public string Periodo { get; set; }
        public decimal NetoFact { get; set; }
        public decimal CobradoFact { get; set; }
        public decimal PendienteFact { get; set; }

    }
}