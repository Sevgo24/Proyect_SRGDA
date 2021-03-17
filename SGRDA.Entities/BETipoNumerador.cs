using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETipoNumerador
    {
        public string OWNER { get; set; }
        public string NMR_TYPE { get; set; }
        public string NMR_TDESC { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public Nullable<DateTime> LOG_USER_CREAT { get; set; }
        public Nullable<DateTime> LOG_USER_UPDAT { get; set; }        

    }
}
