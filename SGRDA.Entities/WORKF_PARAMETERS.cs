using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_PARAMETERS
    {
        public string OWNER { get; set; }
        public decimal WRKF_PID { get; set; }
        public string WRKF_PNAME { get; set; }
        public string WRKF_PVALUE { get; set; }
        public Nullable<decimal> WRKF_PORDER { get; set; }
        public Nullable<decimal> WRKF_AID { get; set; }
        public Nullable<decimal> WRKF_DTID { get; set; }
        public Nullable<decimal> WRKF_IS_ARRAY { get; set; }
        public string WRKF_PTID { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
        public Nullable<decimal> WRKF_AMID { get; set; }
        public decimal PROC_MOD { get; set; }
        public decimal WRKF_OID { get; set; }

        public List<WORKF_PARAMETERS> Parametros { get; set; }
        public List<WORKF_PARAMETERS> ParametrosAnterior { get; set; }
    }
}
