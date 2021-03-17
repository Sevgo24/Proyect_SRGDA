using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAgenteCampania
    {
        public string OWNER { get; set; }
        public decimal SEQUENCE { get; set; } // Id
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public decimal CONC_CID { get; set; }
        public string ROL_ID { get; set; }
        public string ROL_DESC { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
    }
}
