using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEObservationAgenteRecaudo
    {
        public string OWNER { get; set; }
        public decimal COLL_OBS_ID { get; set; }
        public decimal OBS_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}
