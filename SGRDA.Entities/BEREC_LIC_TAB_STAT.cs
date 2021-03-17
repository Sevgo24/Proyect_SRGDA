using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEREC_LIC_TAB_STAT : Paginacion
    {
        public string OWNER { get; set; }
        public decimal SECUENCIA { get; set; }
        public decimal TAB_ID { get; set; }
        public string TAB_NAME { get; set; }
        public decimal antTAB_ID { get; set; }
        public decimal WORKF_SID { get; set; }
        public decimal WORKF_ID { get; set; }
        public string WRKF_SNAME { get; set; }
        public string WRKF_SLABEL { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }


    }
}
