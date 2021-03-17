using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEParametroOff : Paginacion
    {

        public List<BEParametroOff> ListaParametroOff { get; set; }
        public BEParametroOff()
        {
            ListaParametroOff = new List<BEParametroOff>();
        }
        public string OWNER { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal PAR_ID { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public string PAR_DESC { get; set; }
        public string PAR_VALUE { get; set; }

    }
}
