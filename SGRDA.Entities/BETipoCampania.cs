using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETipoCampania
    {
        public string OWNER { get; set; }
        public decimal CONC_CTID { get; set; }
        public string CONC_CTNAME { get; set; }
        public string OBS_DESC { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string ESTADO { get; set; }
        public int TotalVirtual { get; set; }
        public decimal OBS_TYPE { get; set; }
        public int valgraba { get; set; }

        public IList<BETipoCampania> ListaCampTipo { get; set; }
        public BETipoCampania()
        {
            ListaCampTipo = new List<BETipoCampania>();
        }
    }
}
