using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_CLIENTS
    {
        public string OWNER { get; set; }
        public decimal WRKF_CID { get; set; }
        public string WRKF_CNAME { get; set; }
        public string WRKF_CLABEL { get; set; }
        public string WRKF_CAPIKEY { get; set; }
        public string WRKF_CSECRETKEY { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
    }
}
