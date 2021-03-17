using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEModuloCliente :Paginacion
    {
        public List<BEModuloCliente> ListarModuloCliente { get; set; }

        public string OWNER { get; set; }
        public decimal PROC_MOD { get; set; }
        public string MOD_DESC { get; set; }
        public string MOD_CLABEL { get; set; }
        public string MOD_CAPIKEY { get; set; }
        public string MOD_CSECRETKEY { get; set; }


        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string ESTADO { get; set; }
    }
}
