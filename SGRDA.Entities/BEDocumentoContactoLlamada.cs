using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDocumentoContactoLlamada
    {
        public string OWNER { get; set; }
        public decimal CONC_MID { get; set; }
        public decimal DOC_ID { get; set; }
        public DateTime? EDNS { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public string DOC_PATH { get; set; }
    }
}
