using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaReglaValor
    {
        public string OWNER { get; set; }
        public decimal CALRV_ID { get; set; }
        public decimal CALR_ID { get; set; }
        public DateTime CALRV_STARTS { get; set; }
        
        public decimal CRI1_FROM { get; set; }
        public decimal CRI2_FROM { get; set; }
        public decimal CRI3_FROM { get; set; }
        public decimal CRI4_FROM { get; set; }
        public decimal CRI5_FROM { get; set; }

        public decimal CRI1_TO { get; set; }
        public decimal CRI2_TO { get; set; }
        public decimal CRI3_TO { get; set; }
        public decimal CRI4_TO { get; set; }
        public decimal CRI5_TO { get; set; }

        public decimal? VAL_FORMULA { get; set; }
        public decimal? VAL_MINIMUM { get; set; }

        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }

        public decimal TEMPS1_ID { get; set; }
        public decimal TEMPS2_ID { get; set; }
        public decimal TEMPS3_ID { get; set; }
        public decimal TEMPS4_ID { get; set; }
        public decimal TEMPS5_ID { get; set; }

        public string SECC1_DESC { get; set; }
        public string SECC2_DESC { get; set; }
        public string SECC3_DESC { get; set; }
        public string SECC4_DESC { get; set; }
        public string SECC5_DESC { get; set; }
    }
}
