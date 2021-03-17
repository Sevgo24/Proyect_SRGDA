using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaDivisionAgente
    {
        public string OWNER { get; set; }
        public decimal ID { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal DAD_ID { get; set; }
        public decimal COLL_OFF_ID { get; set; }
        public decimal OFF_ID { get; set; }

        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }


        public string OFF_NAME { get; set; }
        public decimal ROL_ID { get; set; }
        public string ROL_DESC { get; set; }
        public string AGENTE { get; set; }

        public decimal BPS_ID { get; set; }
        public decimal INDICADOR { get; set; }
    }
}
