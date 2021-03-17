using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaAforo
    {
        public string OWNER { get; set; }
        public string CAP_ID { get; set; }
        public decimal ACOUNT_ID { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal PER_ID { get; set; }

        public decimal SEC_ID { get; set; }
        public string SECID { get; set; }
        public string SEC_DESC { get; set; }

        public decimal LIC_SEC_ID { get; set; }
        public decimal TICKET_PRE { get; set; }
        public decimal NETO_PRE { get; set; }
        public decimal TICKET_LIQ { get; set; }
        public decimal NETO_LIQ { get; set; }

        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public List<BELicenciaLocalidad> listaTIPODEAFOROS {get;set;}
    }
}
