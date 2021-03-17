using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_STATES:Paginacion
    {
        public string OWNER { get; set; }
        public decimal WRKF_SID { get; set; }
        public string WRKF_SNAME { get; set; }
        public string WRKF_SLABEL { get; set; }
        public string WRKF_SDESC { get; set; }
        public Nullable<decimal> WRKF_STID { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
        
        public string WRKF_STNAME { get; set; }
        public string ESTADO { get; set; }
        public int ID_ESTADO { get; set; }

        public List<BEREC_LIC_TAB_STAT> ListaTab { get; set; }
        public List<WORKF_STATES> ListarEstados { get; set; }
        public decimal WRKF_ID { get; set; }
    }
}
