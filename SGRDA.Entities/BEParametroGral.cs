using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEParametroGral : Paginacion
    {
        public string OWNER { get; set; }
        public decimal PAR_ID { get; set; }
        public decimal PAR_TYPE { get; set; }
        public decimal ENT_ID { get; set; }
        public string PAR_VALUE { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public decimal PAR_SUBTYPE { get; set; }

        public string PAR_SUBTYPE_DESC { get; set; }
    }
}
