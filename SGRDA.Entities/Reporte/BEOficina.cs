using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEOficina
    {
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; } // nombre del agente de recaudo de la oficina
        public string OFF_NAME { get; set; } // nombre de la oficina
        public string ADDRESS { get; set; } // direccion de la oficina
        public decimal TIS_N { get; set; }
        public decimal GEO_ID { get; set; }
    }
}
