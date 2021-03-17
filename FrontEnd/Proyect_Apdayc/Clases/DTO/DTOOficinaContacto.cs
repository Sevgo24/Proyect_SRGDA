using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOOficinaContacto
    {
        public string owner { get; set; }
        public decimal Id { get; set; }  //bps_id
        public decimal off_id { get; set; }
        public decimal rol_id { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public Nullable<DateTime> ENDS { get; set; }

        public string nombre { get; set; }
        public string rol_descripcion { get; set; }
        public decimal tipo_documento { get; set; }
        public string numero_documento { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}