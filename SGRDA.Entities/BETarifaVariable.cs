using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaVariable
    {
        public int SECC_CHARSEQ { get; set; }
        public decimal TEMP_VID { get; set; }
        public string CHAR_LONG { get; set; }
        public string TEMP_VAR_TRA { get; set; }

        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

    }
}
