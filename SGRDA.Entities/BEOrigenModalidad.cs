using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEOrigenModalidad : Paginacion
    {
        public IList<BEOrigenModalidad> ListaOrigenModalidad { get; set; }
        public BEOrigenModalidad()
        {
            ListaOrigenModalidad = new List<BEOrigenModalidad>();
        }
        public string OWNER { get; set; }
        public string MOD_ORIG { get; set; }
        public string MOD_ODESC { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }


        public string ESTADO { get; set; }

    }
}
