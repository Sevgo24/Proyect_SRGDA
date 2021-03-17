using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOGarantia
    {
        //
        // GET: /DTOGarantia/

        public decimal idGarantia { get; set; }
        public decimal idLic { get; set; }
        public decimal Valor { get; set; }
        public string moneda { get; set; }
        public string tipo { get; set; }
        public string numero { get; set; }
        public string entidad { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public decimal? ValorAplicado { get; set; }
        public decimal? ValorDevuelto { get; set; }
        public DateTime? FechaRetencion { get; set; }
        public string FechaRecepcionChar { get; set; }
        public string FechaDevolucionChar { get; set; }
        public bool Activo { get; set; }
    }
}
