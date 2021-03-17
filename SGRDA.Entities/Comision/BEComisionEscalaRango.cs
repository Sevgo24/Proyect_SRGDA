using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Comision
{
    public class BEComisionEscalaRango
    {
        public string OWNER { get; set; }
        public decimal RANG_ID { get; set; }
        public decimal SET_ID { get; set; }
        public decimal PRG_ORDER { get; set; }
        public decimal PRG_VALUEI { get; set; }
        public decimal PRG_VALUEF { get; set; }
        public decimal PRG_PERC { get; set; }
        public decimal PRG_VALUEC { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
    }
}
