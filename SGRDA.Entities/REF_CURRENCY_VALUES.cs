using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class REF_CURRENCY_VALUES : Paginacion
    {
        public string CUR_ALPHA { get; set; }
        public Nullable<DateTime> CUR_DATE { get; set; }

        [Required(ErrorMessage = "Ingrese valor")]
        [DisplayName("VALOR")]
        public decimal CUR_VALUE { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
