using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BETrasladoAgentesRecaudo : Paginacion
    {
        public string OWNER { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal OFF_IDAux { get; set; }
        public string OFF_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public decimal LEVEL_ID { get; set; }
        public Nullable<DateTime> START { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}
