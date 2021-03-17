using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEModulo : Paginacion
    {
        public string OWNER { get; set; }
        public decimal PROC_MOD { get; set; }
        public string MOD_DESC { get; set; }
        public string MOD_CLABEL { get; set; }
        public string MOD_CAPIKEY { get; set; }
        public string MOD_CSECRETKEY { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
