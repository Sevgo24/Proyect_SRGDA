using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEParametroSubTipo
    {
        public string OWNER { get; set; }
        public decimal PAR_SUBTYPE { get; set; }
        public decimal PAR_TYPE { get; set; }
        public string PAR_SUBTYPE_DESC { get; set; }
        public string PAR_SUBTYPE_OBSERV { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
    }
}
