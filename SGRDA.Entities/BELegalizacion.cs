using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELegalizacion
    {
        public string OWNER { get; set; }
        public decimal LEG_ID { get; set; }
        public decimal MNR_ID { get; set; }
        public Nullable<DateTime> LEG_DATE { get; set; }
        public string LEG_ADJ { get; set; }
        public decimal MNR_VALUE_ADJ { get; set; }
        public string MNR_DOC_ADJ { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}
