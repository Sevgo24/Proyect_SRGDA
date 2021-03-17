using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BESocioNegocioOficina
    {
        public string OWNER  { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal ROL_ID { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }

        public string NOMBRE { get; set; }
        public string ROL { get; set; }
        public decimal TAXT_ID { get; set; }
        public string TAX_ID { get; set; }
    }
}
