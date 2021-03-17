using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOLicenciaEstadoTab
    {
        public decimal IdTab { get; set; }
        public decimal antIdTab { get; set; }
        public decimal IdLicencia { get; set; }
        public decimal IdEstado { get; set; }
        public decimal IdWorkFlow { get; set; }
        public string Nombre { get; set; }
        public string NombreEst { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public decimal sequencia { get; set; }

        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}