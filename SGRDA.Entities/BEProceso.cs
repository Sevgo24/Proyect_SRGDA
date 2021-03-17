using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public class BEProceso : Paginacion
    {
        public string OWNER { get; set; }

        public decimal MOD_ID { get; set; }
        public string MOD_DESC { get; set; }
        public string PROC_NAME { get; set; }
        public decimal PROC_ID { get; set; }
        public decimal PROC_TYPE { get; set; }
        public decimal PROC_ORDER { get; set; }
        public decimal PROC_MOD { get; set; }
        public string PROC_DESC { get; set; }
        public string PROC_TDESC { get; set; }
        public string MOG_ID { get; set; }
        public string MOG_DESC { get; set; }
        public decimal PROC_JOBS { get; set; }
        public string PROC_FUCTION { get; set; }
        public DateTime? PROC_LAUNCH { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string ESTADO { get; set; }

        public string WRKF_ANAME { get; set; }
        public string WRKF_ALABEL { get; set; }
        public string WRKF_ODESC { get; set; }
        public decimal WRKF_AID { get; set; }
        public decimal WRKF_OID { get; set; }
        public decimal WRKF_AMID { get; set; }

        public decimal ORDER { get; set; }
        public string WRKF_NAME { get; set; }
        public List<BEProceso> ListaProceso { get; set; }
        public decimal WRKF_ID { get; set; }
        public decimal WRKF_CID { get; set; }
        public string PROC_FREQ_TYPE { get; set; }
        public string PROC_SHOW { get; set; }

        public BEProceso()
        {
            ListaProceso = new List<BEProceso>();
        }
    }
}
