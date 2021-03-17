using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    //REC_EST_CHAR_VAL
    public class BECaracteristicaEst
    {
        public string OWNER { get; set; }
        public decimal CHAR_ID { get; set; }
        public string CHAR_SHORT { get; set; }
        public string CHAR_LONG { get; set; }
        public decimal EST_ID { get; set; }
        public decimal SUBE_ID { get; set; }
        public decimal ESTT_ID { get; set; }
        public decimal VALUE { get; set; }
        public decimal INSP_ID { get; set; }
        public decimal LIC_ID { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}
