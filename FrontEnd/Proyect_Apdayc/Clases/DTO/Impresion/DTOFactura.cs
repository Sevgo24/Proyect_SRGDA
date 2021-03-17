using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO.Impresion
{
    public class DTOFactura
    {
        public string RUM { get; set; }
        public string Usuario { get; set; }
        public string RUC { get; set; }
        public string Local { get; set; }
        public string Direccion { get; set; }
        public string Fecha { get; set; }
        public string Nro { get; set; }
        public List<DTOFacturaDeta> Detalle { get; set; }

    }
}