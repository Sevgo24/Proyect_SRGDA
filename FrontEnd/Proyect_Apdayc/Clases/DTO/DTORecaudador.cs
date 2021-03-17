using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTORecaudador 
    {
        public decimal Codigo { get; set; }
        public decimal? OficinaComercial { get; set;}
        public string MonedaRecaudacion { get; set;}
        public decimal? Nivel { get; set; }
        public DateTime? InicioContrato { get; set; }
        public DateTime? LiquidacionContrato { get; set; }
        public DateTime? LiquidacionGastos { get; set; }
        public DateTime? LiquidacionComision { get; set; }
        public string CuentaCorriente { get; set; }
        public bool Activo { get; set; }

    }
}
