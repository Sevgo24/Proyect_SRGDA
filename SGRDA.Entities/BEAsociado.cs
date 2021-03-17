using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEAsociado
    {
        public string OWNER { get; set; }

        public string ROL_ID { get; set; }
        public string ROL_DESC { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal BPSA_ID { get; set; }
        public decimal SEQUENCE { get; set; }
        public decimal ENT_ID { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }
    }
}
