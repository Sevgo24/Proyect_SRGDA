using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaConteo
    {
        public string OWNER { get; set; }
        public decimal LIC_ID { get; set; }
        public string CAP_ID { get; set; }
        public string SEC_ID { get; set; }
        public decimal SEC_TICKETS { get; set; }
        public decimal SEC_VALUE { get; set; }
        public decimal SEC_GROSS { get; set; }
        public decimal SEC_TAXES { get; set; }
        public decimal SEC_NET { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
    }
}
