using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDifusionEst
    {
        public string OWNER { get; set; }
        public decimal SEQUENCE { get; set; }
        public decimal EST_ID { get; set; }
        public decimal BROAD_ID { get; set; }
        public decimal BROADE_NUM { get; set; }
        public string BROAD_DESC { get; set; }
        public string BROADE_STORAGE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
    }
}
