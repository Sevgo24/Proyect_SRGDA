using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaReglaData
    {
        public string OWNER { get; set; }
        public decimal CALRD_ID { get; set; }
        public decimal CALR_ID { get; set; }
        public decimal CHAR_ID { get; set; }
        public string CHAR_OTYPE { get; set; }
        public string CALRD_VAR { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }


        //public string OWNER { get; set; }
        public decimal TEMPL_ID { get; set; }
        public decimal TEMP_ID { get; set; }
        //public decimal CHAR_ID { get; set; }
        public string IND_TR { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        //public string LOG_USER_CREAT { get; set; }
        //public string LOG_USER_UPDATE { get; set; }
        //public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        //public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        /// 
        public string CHAR_LONG { get; set; }
    }
}
