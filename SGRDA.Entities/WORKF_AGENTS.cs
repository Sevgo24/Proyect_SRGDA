using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_AGENTS :Paginacion
    {
        public List<WORKF_AGENTS> ListarAgentes { get; set; }
        public string OWNER { get; set; }
        public decimal WRKF_AGID { get; set; }
        public string WRKF_AGNAME { get; set; }
        public string WRKF_AGLABEL { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public string ESTADO { get; set; }
        public int ID_ESTADO { get; set; }
    }
}
