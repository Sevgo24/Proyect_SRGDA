using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaEstadoTab
    {
        public string OWNER { get; set; }
        public decimal SECUENCIA { get; set; }
        public decimal TAB_ID { get; set; }
        public decimal antTAB_ID { get; set; }
        public decimal LICS_ID { get; set; }
        public decimal WORKF_SID { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
    }
}
