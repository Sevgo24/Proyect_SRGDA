using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDifusion
    {
        public string OWNER { get; set; }
        public decimal BROAD_ID { get; set; }
        public string BROAD_DESC { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}
