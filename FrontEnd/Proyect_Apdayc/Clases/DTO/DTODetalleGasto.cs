using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODetalleGasto
    {        
        public decimal Id { get; set; }
        public decimal ReqGasto_Id { get; set; }

        public string Tipo_Id { get; set; }
        public string Grupo_Id { get; set; }
        public string Gasto_Id { get; set; }
        public decimal Leg_id { get; set; }

        public string Gasto_Des { get; set; }

        public decimal Monto_Solicitado { get; set; }
        public decimal Monto_Aprobado { get; set; }
        public decimal Monto_Gastado { get; set; }

        public string Estado { get; set; }
        public DateTime? ENDS { get; set; }

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