using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEModalidadIncidencia
    {
        public string OWNER { get; set; }
        public string MOD_INCID { get; set; }
        public string MOD_IDESC { get; set; }
        public string MOD_IDET { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public decimal MOD_PRC { get; set; }
    }
}
