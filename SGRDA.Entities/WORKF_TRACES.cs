using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_TRACES
    {
        public string OWNER { get; set; }
        public decimal WRKF_TID { get; set; }
        public Nullable<decimal> WRKF_AID { get; set; }
        public Nullable<decimal> WRKF_ID { get; set; }
        public decimal WRKF_SID { get; set; }
        public Nullable<decimal> WRKF_REF1 { get; set; }
        public Nullable<decimal> PROC_MOD { get; set; }
        public Nullable<System.DateTime> WRKF_ADATE { get; set; }
        public Nullable<decimal> PROC_ID { get; set; }
        public Nullable<decimal> WRKF_REF2 { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
        public decimal? WRKF_AMID { get; set; }
    }
}
