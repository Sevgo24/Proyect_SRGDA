using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOAsociacion
    {
        public decimal Codigo { get; set; }
        public string Tarifa { get; set; }
        public DateTime? InicioBonificacion { get; set; }
        public DateTime? FinBonificacion { get; set; }
        public decimal? PCuotaFederativa { get; set; }
        public decimal? VCuataFederativa { get; set; }
        public decimal? PBonificacionAsociado { get; set; }
        public decimal? VBonificacionAsociado { get; set; }
        public string CuentaCorriente { get; set; }
        public bool Activo { get; set; }
    }
}
