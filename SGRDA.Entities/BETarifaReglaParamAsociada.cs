using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaReglaParamAsociada
    {
        public string OWNER { get; set; }
        public decimal RATE_PARAM_ID { get; set; }
        public decimal RATE_CHAR_ID { get; set; }
        public decimal RATE_CALC_ID { get; set; }
        public decimal RATE_CALC_AR { get; set; } //Código Caracteristica.
        public decimal RATE_CALC { get; set; }    //Código Regla.
        public string RATE_PARAM_VAR { get; set; }

        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
    }
}
