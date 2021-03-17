using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOTarifa
    {
        public decimal idTarifa { get; set; }
        public string TarifaDesc { get; set; }
        public string idMoneda { get; set; }
        public decimal idTemporalidad { get; set; }

        public decimal CantVariable { get; set; }
        public decimal CantCaracteristica { get; set; }
        public string Formula { get; set; }
        public string Minima { get; set; }
        public string FormulaTipo { get; set; }
        public string MinimoTipo { get; set; }
        public decimal FormulaDec { get; set; }
        public decimal MinimoDec { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public decimal ValorFormula { get; set; }
        public decimal ValorMinimo { get; set; }
        public int Redondeo { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal VUM { get; set; }
    }
}
