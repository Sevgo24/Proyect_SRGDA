using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BERegistroVentaNC
    {
        public string FECHA { get; set; }
        public string TD { get; set; }
        public decimal NUMERO { get; set; }
        public string SERIE { get; set; }
        public string RUC { get; set; }
        public string NOMBRE { get; set; }
        public decimal AFECTO { get; set; }
        public decimal INAFECTO { get; set; }
        public decimal IGV { get; set; }
        public decimal TOTAL { get; set; }
        public string DOC_REFERENCIAS { get; set; }
        public string Fecha1 { get; set; }
        public string Fecha2 { get; set; }
        public string OFICINA { get; set; }
        public string ESTADO { get; set; }

    }
}
