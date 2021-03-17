using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEAutorizacion
    {
        public string OWNER { get; set; }
        public decimal LIC_AUT_ID { get; set; }
        public decimal LIC_ID { get; set; }
        public DateTime LIC_AUT_START { get; set; }
        public DateTime LIC_AUT_END { get; set; }
        public string LIC_AUT_OBS { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }
    }
}
