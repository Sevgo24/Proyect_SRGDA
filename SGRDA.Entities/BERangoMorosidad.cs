using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BERangoMorosidad : Paginacion
    {
        public Int32 RowNumber { get; set; }
        public string OWNER { get; set; }
        public decimal RANGE_COD { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal RANGE_FROM { get; set; }
        public decimal RANGE_TO { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }
    }
}
