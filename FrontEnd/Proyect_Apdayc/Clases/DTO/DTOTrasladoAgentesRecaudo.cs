using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOTrasladoAgentesRecaudo
    {
        public decimal Codigo { get; set; }
        public decimal CodigoSocio { get; set; }
        public string Oficina { get; set; }
        public decimal Level { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}