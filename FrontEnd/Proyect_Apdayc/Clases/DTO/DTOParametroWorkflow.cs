using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOParametroWorkflow
    {
        public decimal Id { get; set; }
        public decimal codigo { get; set; }
        public string nombre { get; set; }
        public string valor { get; set; }
        public decimal? orden { get; set; }
        public decimal? accionMappingId { get; set; }
        public decimal objetoId { get; set; }
        public decimal? wrkfdtid { get; set; }
        public string wrkfptid { get; set; }
        public decimal procmod { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}