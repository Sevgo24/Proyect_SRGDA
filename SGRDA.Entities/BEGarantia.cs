using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEGarantia
    {
		public decimal LIC_ID {get;set;}
		public decimal GUAR_ID {get;set;}
		public decimal GUAR_VAL {get;set;}
		public string CUR_ALPHA {get;set;}
		public string GUAR_TYPE {get;set;}
		public string GUAR_NRO {get;set;}
		public string GUAR_ENTITY {get;set;}
		public DateTime GUAR_RDATE {get;set;}
		public DateTime? GUAR_DDATE {get;set;}
		public decimal? GUAR_AVAL {get;set;}
		public decimal? GUAR_DVAL {get;set;}
        public DateTime? GUAR_TDATE { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }

    }
}
