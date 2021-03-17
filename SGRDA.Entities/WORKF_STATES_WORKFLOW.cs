using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_STATES_WORKFLOW
    {
        public string OWNER { get; set; }
        public decimal WRKF_ID { get; set; }
        public decimal WRKF_SID { get; set; }
        public decimal WRKF_SID_ORIGEN { get; set; }
        public string WRKF_SLABEL { get; set; }
        public Boolean WRKF_INI { get; set; }

        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
        

    }
}
