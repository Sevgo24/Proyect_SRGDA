using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEParametrosEst
    {
        public string OWNER { get; set; }
        public decimal EST_ID { get; set; }
        public decimal PAR_ID { get; set; }
        public string PAR_DESC { get; set; }
        public string PAR_VALUE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}
