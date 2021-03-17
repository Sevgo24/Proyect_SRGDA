using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaPlantillaSeccion
    {
        public string OWNER { get; set; }
        public decimal TEMP_ID { get; set; }
        public DateTime STARTS { get; set; }
        public decimal SECC_CHARSEQ { get; set; }
        public decimal SECC_FROM { get; set; }
        public decimal SECC_TO { get; set; }
        public string SECC_DESC { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
    }
}
