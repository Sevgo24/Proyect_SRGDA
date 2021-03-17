using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOShow
    {

        public decimal Codigo { get; set; }
        public decimal CodigoAutorizacion { get; set; }
        public string Nombre { get; set; }
        public string Observacion { get; set; }
        public string FechaInicio  { get; set; }
        public string FechaFin { get; set; }
        public decimal Orden { get; set; }
        public bool Activo { get; set; }
    }
}