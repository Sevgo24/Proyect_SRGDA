using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOTarifaManReglaAsoc
    {
        public decimal Id { get; set; }
        public decimal IdTarifa { get; set; }
        public decimal IdRegla { get; set; }
        public string Tipo { get; set; }
        public string Elemento { get; set; }
        public string TipoCalculo { get; set; }
        public string Letra { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        /// <summary>
        /// Propiedades para Tarifa Test
        /// </summary>
        public decimal IdPlantilla { get; set; }
        public string AjustarUnidades { get; set; }
        public string AcumularTramos { get; set; }

        public string Formula { get; set; }
        public string FormulaTipo { get; set; }
        public decimal FormulaDec{ get; set; }

        public string Minimo { get; set; }
        public string MinimoTipo { get; set; }
        public decimal MinimoDec { get; set; }

        public decimal Variables { get; set; }

        public decimal? ValorFormula { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorR { get; set; }
    }
}