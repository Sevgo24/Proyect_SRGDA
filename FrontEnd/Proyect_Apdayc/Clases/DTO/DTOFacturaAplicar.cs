using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOFacturaAplicar
    {
        public decimal Id { get; set; }
        public DateTime? FechaExp { get; set; }
        public decimal Base { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal idFecha { get; set; }
        public decimal idRecibo { get; set; }
    }
}