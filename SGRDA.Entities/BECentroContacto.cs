using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BECentroContacto
    {
        public string OWNER { get; set; }
        public decimal CONC_ID { get; set; }
        public string CONC_NAME { get; set; }
        public decimal OFF_ID { get; set; }
        public string CONC_DESC { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string ESTADO { get; set; }
        public int TotalVirtual { get; set; }
        public int valgraba { get; set; }
        public string OFF_NAME { get; set; }

        public IList<BECentroContacto> ListaCentroCont { get; set; }
        public BECentroContacto()
        {
            ListaCentroCont = new List<BECentroContacto>();
        }
    }
}



