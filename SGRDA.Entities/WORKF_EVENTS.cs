using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_EVENTS : Paginacion
    {
        
        public string OWNER { get; set; }
        public decimal WRKF_EID { get; set; }
        public string WRKF_ENAME { get; set; }
        public string WRKF_ELABEL { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
        public int TotalVirtual { get; set; }
        public string ESTADO { get; set; }

        public IList<WORKF_EVENTS> listaEv { get; set; }
        public WORKF_EVENTS()
        {
            listaEv = new List<WORKF_EVENTS>();
        }

        public decimal WRKF_TID { get; set; }
        public string WRKF_LABEL { get; set; }
    }
}
