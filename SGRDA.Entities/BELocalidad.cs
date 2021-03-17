using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELocalidad
    {

        public decimal SEC_ID { get; set; }
        public string SECID { get; set; }
        public string SEC_DESC { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }
    }
}
