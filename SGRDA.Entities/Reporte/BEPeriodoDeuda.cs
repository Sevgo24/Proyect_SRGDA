using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEPeriodoDeuda
    {
        public decimal INV_ID { get; set; }
        public decimal LIC_YEAR { get; set; }
        public decimal LIC_MONTH { get; set; }
        public decimal INV_NET { get; set; }
        public string LIC_MONTH_NAME { get; set; }
    }
}
