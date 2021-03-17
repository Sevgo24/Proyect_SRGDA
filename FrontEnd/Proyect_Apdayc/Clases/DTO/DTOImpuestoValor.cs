using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOImpuestoValor
    {
        public decimal Id { get; set; }
        public decimal IdDivision { get; set; }
        public decimal IdImpuesto { get; set; }
        public string Division { get; set; }
        public decimal ValorPorcentaje { get; set; }
        public decimal ValorMonto { get; set; }
        public string FechaVigencia { get; set; }
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