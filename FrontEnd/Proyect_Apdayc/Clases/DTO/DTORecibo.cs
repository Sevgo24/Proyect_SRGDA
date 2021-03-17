using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTORecibo
    {
        public decimal Id { get; set; }
        public decimal IdUsuDerecho { get; set; }
        public string Serie { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Observacion { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public DateTime? FechaPago { get; set; }

        public decimal Base { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}