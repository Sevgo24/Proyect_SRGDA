using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEInspectionEst : Paginacion
    {
        public string OWNER { get; set; }
        public decimal INSP_ID { get; set; }
        public decimal EST_ID { get; set; }
        public string INSP_DOC { get; set; }
        public string INSP_OBS { get; set; }
        public decimal BPS_ID { get; set; }
        public string EST_NAME { get; set; }
        public Nullable<DateTime> INSP_DATE { get; set; }
        public Nullable<DateTime> INSP_HOUR { get; set; }

        public string HOUR { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; } 
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public int valgraba { get; set; }
        public string ESTADO { get; set; }

        public IList<BEInspectionEst> ListaInspection { get; set; }
        public BEInspectionEst()
        {
            ListaInspection = new List<BEInspectionEst>();
        }
    }
}
