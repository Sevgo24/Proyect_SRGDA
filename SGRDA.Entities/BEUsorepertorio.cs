using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEUsorepertorio : Paginacion
    {
        public string OWNER { get; set; }
        public string MOD_USAGE { get; set; }
        public string MOD_DUSAGE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public string ESTADO { get; set; }
        public int valgraba { get; set; }
        public IList<BEUsorepertorio> ListaUsoRepertorio { get; set; }
        public BEUsorepertorio()
        {
            ListaUsoRepertorio = new List<BEUsorepertorio>();
        }
    }
}
