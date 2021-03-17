using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEContactoAsignarCampania
    {
        public string OWNER { get; set; }
        public decimal CONC_CID { get; set; }
        public decimal BPS_ID { get; set; }
        public DateTime? ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
    }
}
