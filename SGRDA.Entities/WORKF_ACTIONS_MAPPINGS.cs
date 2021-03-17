using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_ACTIONS_MAPPINGS
    {
        public string OWNER { get; set; }
        public decimal? WRKF_AMID { get; set; }
        public decimal WRKF_ID { get; set; }
        public decimal WRKF_SID { get; set; }
        
        public Nullable<decimal> WRKF_AMPR { get; set; }
        //public Nullable<decimal> WRKF_AID { get; set; }
        public Nullable<decimal> WRKF_AID { get; set; }
        public decimal? WRKF_AIDAUX { get; set; }
        public decimal? WRKF_AIDAUXId { get; set; }
        public decimal? WRKF_TID { get; set; }
        public string WRKF_GUARD_DESC { get; set; }
        public Nullable<decimal> WRKF_GUARD_ID { get; set; }
        public Nullable<decimal> WRKF_OID { get; set; }
        public string WRKF_AMAND { get; set; }
        public string WRKF_AMVISIBLE { get; set; }
        public Nullable<decimal> WRKF_AIDPRE { get; set; }
        public string WRKF_AORDER { get; set; }
        public string WRKF_AORDERNew { get; set; }
        public string WRKF_ATIMEMAX { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }

        public Nullable<decimal> WRKF_ETRIGGER { get; set; }
        public Nullable<decimal> WRKF_AMTRIGGER { get; set; }
        public string WRKF_ATIMEMAXP { get; set; }


        public string WRKF_ANAME { get; set; }
        public string WRKF_ODESC { get; set; }


        //public int valgraba { get; set; }
        //public List<WORKF_ACTIONS_MAPPINGS> Mappings { get; set; }

        public List<WORKF_PARAMETERS> ParametrosTransicion { get; set; }
        public List<WORKF_ACTIONS_MAPPINGS> Mapping { get; set; }
    }
}
