using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaPlantillaCaracteristica
    {
        public string OWNER { get; set; }
        public decimal TEMPL_ID { get; set; }
        public decimal TEMP_ID { get; set; }
        public decimal CHAR_ID { get; set; }
        public string IND_TR { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        /// 
        public string CHAR_LONG { get; set; }
        public string LETRA { get; set; }
        public decimal SECC_CHARSEQ { get; set; }
    }
}
