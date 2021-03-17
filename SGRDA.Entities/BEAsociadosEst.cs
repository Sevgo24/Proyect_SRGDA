using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAsociadosEst
    {
        public string OWNER { get; set; }
        public decimal Id { get; set; }
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public decimal EST_ID { get; set; }
        public string ROL_ID { get; set; }
        public string ROL_DESC { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
        public decimal DAD_ID { get; set; }
        public Boolean BPS_MAIN { get; set; }
        public decimal SEQUENCE { get; set; }
    }
}
