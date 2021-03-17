using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_ESTABLISHMENT_GRAL: Paginacion
    {
        public string OWNER { get; set; }
        public decimal EST_ID { get; set; }
        public string EST_NAME { get; set; }
        public decimal ESTT_ID { get; set; }
        public decimal DAD_ID { get; set; }
        public decimal SUBE_ID { get; set; }
        public decimal DIF_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
    }
}
