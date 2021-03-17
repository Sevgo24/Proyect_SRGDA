using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_MODALITY : Paginacion
    {
        public string OWNER { get; set; }
        public decimal MOD_ID { get; set; }
        public string MOD_DEC { get; set; }
        public string MOD_ORIG { get; set; }
        public string MOD_SOC { get; set; }
        public string CLASS_COD { get; set; }
        public string MOG_ID { get; set; }
        public string RIGHT_COD { get; set; }
        public string MOD_INCID { get; set; }
        public string MOD_USAGE { get; set; }
        public string MOD_REPER { get; set; }
        public string RATE_ID { get; set; }
        public decimal MOD_COM { get; set; }
        public decimal MOD_DISCA { get; set; }
        public decimal MOD_DISCS { get; set; }
        public decimal MOD_DISCC { get; set; }
        public DateTime? ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
    }
}
