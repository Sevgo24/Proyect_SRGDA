using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BERepresentanteLegal
    {
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public string TAX_ID { get; set; }
        public string ADDRESS { get; set; }
        public decimal? TIS_N { get; set; }
        public decimal? GEO_ID { get; set; }
    }
}
