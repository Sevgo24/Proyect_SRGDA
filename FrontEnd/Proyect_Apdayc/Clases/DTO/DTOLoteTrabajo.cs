using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOLoteTrabajo
    {
        public decimal Id { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        public decimal IdAgente { get; set; }
        public string NombreAgente { get; set; }
        public decimal IdCampania { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}