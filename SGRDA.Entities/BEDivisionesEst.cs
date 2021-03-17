using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDivisionesEst
    {
        public string OWNER { get; set; }
        public decimal Id { get; set; }
        public decimal EST_ID { get; set; }
        public decimal auxDADV_ID { get; set; }
        public string idTIPODIVISION { get; set; }
        public decimal idDIVISION { get; set; } 
        public decimal idSUBTIPODIVISION { get; set; }
        public decimal idDIVISIONVAL { get; set; }

        public string TIPODIVISION { get; set; }
        public string DIVISION { get; set; }
        public string SUBTIPODIVISION { get; set; }
        public string DIVISIONVAL { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
