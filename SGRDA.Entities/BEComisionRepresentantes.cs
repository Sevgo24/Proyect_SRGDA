using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEComisionRepresentantes
    {
        public string OWNER { get; set; }
        public decimal PRG_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public DateTime STARTS { get; set; }
        public DateTime ENDS { get; set; }
        public DateTime? LOG_DATE_CREATE { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public decimal SEQUENCE { get; set; }
        public Nullable<DateTime> Inactivo { get; set; }
    }
}
