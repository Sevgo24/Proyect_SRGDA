using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaLocalidad
    {
        public string OWNER { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal SEC_ID { get; set; }
        public string SEC_DESC { get; set; }

        public string SEC_COLOR { get; set; }

        public decimal SEC_TICKETS { get; set; }
        public decimal SEC_VALUE { get; set; }
        public decimal SEC_GROSS { get; set; }
        public decimal SEC_TAXES { get; set; }
        public decimal SEC_NET { get; set; }

        public DateTime? ENDS { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
