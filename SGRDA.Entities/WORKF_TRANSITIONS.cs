using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_TRANSITIONS :Paginacion
    {
        public List<WORKF_TRANSITIONS> ListarTransiciones { get; set; }
        public string OWNER { get; set; }
        public decimal WRKF_TID { get; set; }
        public Nullable<decimal> WRKF_ID { get; set; }
        public Nullable<decimal> WRKF_CSTATE { get; set; }
        public Nullable<decimal> WRKF_NSTATE { get; set; }
        public Nullable<decimal> WRKF_EID { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }

        public string WRKF_NAME { get; set; }  // Ciclo
        public string WRKF_ENAME { get; set; } //Evento
        public string ESTADO_INI { get; set; }
        public string ESTADO_FIN { get; set; }

        public string ESTADO { get; set; }
        
    }
}
