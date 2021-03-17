using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_ACTIONS :Paginacion
    {
        public string OWNER { get; set; }
        public decimal WRKF_AID { get; set; }
        public string WRKF_ANAME { get; set; }
        public string WRKF_ALABEL { get; set; }
        public Nullable<decimal> WRKF_ATID { get; set; }
        public string WRKF_AAPLIC { get; set; }
        public string WRKF_ADESC { get; set; }
        public Nullable<decimal> WRKF_DTID { get; set; }
        public Nullable<decimal> PROC_ID { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }

        public string WRKF_ATNAME { get; set; }
        public string WRKF_DTNAME { get; set; }
        public string PROC_NAME { get; set; }
        public string ESTADO { get; set; }
        public string TIPO_ACCION { get; set; }

        public List<WORKF_ACTIONS> ListarAcciones { get; set; }
        public WORKF_ACTIONS Acciones { get; set; }
        public List<BEAgenteAccion> AgenteAccion { get; set; }
    }
}
