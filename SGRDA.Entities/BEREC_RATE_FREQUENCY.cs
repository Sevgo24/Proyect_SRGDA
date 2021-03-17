using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_RATE_FREQUENCY : Paginacion
    {
        public string OWNER { get; set; }
        public decimal RAT_FID { get; set; }
        public string RAT_FDESC { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? ENDS { get; set; }
        public string Activo { get; set; }
        public List<BEPeriodoFrecuencia> PeriodoFrecuencia { get; set; }
    }
}
