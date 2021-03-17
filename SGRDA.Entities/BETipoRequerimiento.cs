using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETipoRequerimiento
    {
        public decimal ID_REQ_TYPE { get; set; }
        public string REQUERIMENTS_DESC { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public string LOG_DATE_CREAT { get; set; }
        public string LOG_DATE_UPDAT { get; set; }

        public string ENDS { get; set; }

    }
}
