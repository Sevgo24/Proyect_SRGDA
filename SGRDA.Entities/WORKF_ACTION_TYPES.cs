using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_ACTION_TYPES
    {
        public string OWNER { get; set; }
        public decimal WRKF_ATID { get; set; }
        public string WRKF_ATNAME { get; set; }
        public string WRKF_ATLABEL { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
    }
}
