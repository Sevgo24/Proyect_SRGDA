using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDocumentoEst
    {
        public string OWNER { get; set; }
        public decimal EST_ID { get; set; }
        public decimal DOC_ID { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }

        public string DOC_DESC { get; set; }
        public string DOC_PATH { get; set; }
    }
}
