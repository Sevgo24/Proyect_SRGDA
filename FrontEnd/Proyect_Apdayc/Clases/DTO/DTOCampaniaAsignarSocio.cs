using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOCampaniaAsignarSocio
    {
        public decimal idCampania { get; set; }
        public decimal idSocio { get; set; }
        public string NombreSocio { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal NetoFact { get; set; }
        public decimal CobradoFact { get; set; }
        public decimal PendienteFact { get; set; }
        public string Usuario { get; set; }
        public string Recaudador { get; set; }
        public string Asociado { get; set; }
        public string Empleado { get; set; }
        public string Proveedor { get; set; }
        public string Perfil { get; set; }
    }
}