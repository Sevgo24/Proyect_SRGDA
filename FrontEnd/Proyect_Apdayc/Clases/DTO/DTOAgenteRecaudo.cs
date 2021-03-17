using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOAgenteRecaudo
    {
        public decimal Codigo { get; set; }
        public decimal IdAgenteRecaudo { get; set; } // BPS_ID
        public string NombreAgenteRecaudo { get; set; }
        public DateTime FechaVigencia { get; set; }
        public DateTime FechaBaja { get; set; }
        public string NombreDocumento { get; set; }
        public string NumDocumento { get; set; }
        
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