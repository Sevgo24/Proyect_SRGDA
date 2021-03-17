using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaReglaAsociada
    {
        public string OWNER { get; set; }
        public decimal RATE_CALC_ID { get; set; }
        public decimal RATE_CALC { get; set; }
        public decimal RATE_ID { get; set; }
        public string RATE_CALCT { get; set; }
        public string RATE_CALC_VAR { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public string ELEMENTO { get; set; }
    }
}
