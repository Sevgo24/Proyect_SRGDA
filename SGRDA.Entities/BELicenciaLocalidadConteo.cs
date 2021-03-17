using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaLocalidadConteo
    {
        public string OWNER { get; set; }
        public decimal LIC_ID { get; set; }
        public string CAP_ID { get; set; }
        public string CAP_IPRELQ { get; set; }
        public decimal TOTAL_IPRELQ { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        //**no se utilizan v
        public decimal Nro { get; set; }
        //public decimal LIC_ID { get; set; }
        public decimal CAP_LIC_ID { get; set; }        
        //public string CAP_ID { get; set; }
        public decimal LIC_SEC_ID { get; set; }
        public decimal SEC_ID { get; set; }
        public decimal? SEC_TICKETS { get; set; }
        public decimal SEC_VALUE { get; set; }
        public decimal SEC_GROSS { get; set; }
        public decimal SEC_TAXES { get; set; }
        public decimal SEC_NET { get; set; }
        //public Nullable<DateTime> ENDS { get; set; }
        //public string LOG_USER_UPDATE { get; set; }
        //public DateTime LOG_DATE_CREAT { get; set; }
        //public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }      
        public string CAP_DESC { get; set; }
        public string SEC_DESC { get; set; }
        public decimal LOC_SEC_GROSS { get; set; }
        public decimal LOC_SEC_TAXES { get; set; }
        public decimal LOC_SEC_NET { get; set; }
    }
}

