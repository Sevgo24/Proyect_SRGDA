using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOComisionEscala
    {
        public decimal Id { get; set; }
        public decimal IdComisionEscala { get; set; }
        public decimal Orden { get; set; }
        public decimal Desde { get; set; }
        public decimal Hasta { get; set; }
        public string TipoId { get; set; }
        public string  TipoDescripcion { get; set; }
        public decimal Valor { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

    }
}