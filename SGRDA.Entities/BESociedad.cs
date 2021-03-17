using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BESociedad:Paginacion
    {
        public IList<BESociedad> ListaSociedad { get; set; }
        public BESociedad()
        {
            ListaSociedad = new List<BESociedad>();
        }
        public string OWNER { get; set; }
        public string MOG_SOC { get; set; }
        public string MOG_SDESC { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public string ESTADO { get; set; }
    }
}
