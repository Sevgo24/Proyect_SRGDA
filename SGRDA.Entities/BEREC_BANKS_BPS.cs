using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_BANKS_BPS : Paginacion
    {
        public decimal Id { get; set; }
        public string OWNER { get; set; }
        public decimal BNK_ID { get; set; } 
        public string BRCH_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal ROL_ID { get; set; }
        public string ROL_DESC { get; set; }
        public string TAXN_NAME { get; set; }
        public string TAX_ID { get; set; }
        public string BPS_NAME { get; set; }
        public decimal TAXT_ID { get; set; }        
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
    }
}
