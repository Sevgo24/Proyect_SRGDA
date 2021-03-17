using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDivisionRecaudador
    {
        public string OWNER { get; set; }
        public decimal ID_COLL_DIV { get; set; }
        public decimal DAD_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal OFF_ID { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public decimal DAD_ID_ANT { get; set; }    
        public string DAD_NAME { get; set; }
        public string DIV_DESCRIPTION { get; set; }
        public List<SocioNegocio> AgenteRecaudo { get; set; }
        
    }
}
