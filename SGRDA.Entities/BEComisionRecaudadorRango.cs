using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEComisionRecaudadorRango
    {
        public string OWNER { get; set; }
        public decimal SEQUENCE { get; set; }
        public decimal PRG_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal PRG_ORDER { get; set; }
        public decimal PRG_VALUEI { get; set; }
        public decimal PRG_VALUEF { get; set; }
        public decimal? PRG_PERC { get; set; }
        public decimal? PRG_VALUEC { get; set; }
        public DateTime? LOG_DATE_CREATE { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
        public string Formato { get; set; }
    }
}
