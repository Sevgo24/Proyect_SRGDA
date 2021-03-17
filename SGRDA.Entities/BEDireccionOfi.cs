using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDireccionOfi : Paginacion
    {
        public IList<BEDireccionOfi> ListaDireccionOfi { get; set; }
        public BEDireccionOfi()
        {
            ListaDireccionOfi = new List<BEDireccionOfi>();
        }

        public string OWNER { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal ADD_ID { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}
