using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_WORKFLOWS :Paginacion
    {
        public List<WORKF_WORKFLOWS> ListaFlujo { get; set; }
        public string OWNER { get; set; }
        public decimal WRKF_ID { get; set; }
        public string WRKF_NAME { get; set; }
        public string WRKF_LABEL { get; set; }
        public Nullable<decimal> PROC_MOD { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public string MOD_DESC { get; set; }
        public string ESTADO { get; set; }
        public List<WORKF_STATES_WORKFLOW> WorkflowEstados { get; set; }
        public List<WORKF_TRANSITIONS> WorkflowTransiciones { get; set; }
        public List<BEREC_LIC_TAB_STAT> WorkflowTabs { get; set; }
    }
}
