using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOWorkflowEstado
    {
        public decimal Secuencia { get; set; }
        public decimal Id_WF { get; set; }
        public decimal Id_Estado { get; set; }
        public decimal Id_Estado_origen { get; set; }
        public string DesEstado { get; set; }

        public Boolean EsPrincipal { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}