using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGRDA.Entities
{
    public partial class BETerritorio : Paginacion
    {
        public decimal TIS_N { get; set; }
        public string COD_TIS_ALPHA { get; set; }
        public string ISO_LANG { get; set; }
        public string NAME_TER { get; set; }
        public string ABBREV_NAME_TER { get; set; }
        public string OFFI_NAME_TER { get; set; }
        public string UNOFFI_NAME_TER { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
