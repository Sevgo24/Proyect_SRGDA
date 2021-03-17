using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEPeriodoFrecuencia
    {
        public List<BEPeriodoFrecuencia> ListaPeriodoFrecuencia { get; set; }
        public BEPeriodoFrecuencia()
        {
            ListaPeriodoFrecuencia = new List<BEPeriodoFrecuencia>();
        }

        public string OWNER { get; set; }
        public decimal RAT_FID { get; set; }
        public decimal FRQ_NPER { get; set; }
        public decimal FRQ_NPER_ANT { get; set; }
        public string FRQ_DESC { get; set; }
        public decimal FRQ_DAYS { get; set; }
        public Nullable<DateTime> FRQ_DATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
