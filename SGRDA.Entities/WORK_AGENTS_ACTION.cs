using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORK_AGENTS_ACTION
    {
        public string OWNER { get; set; }
        public decimal WRKF_AGAC_ID { get; set; }
        public Nullable<decimal> WRKF_AGID { get; set; }
        public string Nombre { get; set; }
        public Nullable<decimal> WRKF_AID { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
