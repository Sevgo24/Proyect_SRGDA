using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEFacturaDescuento
    {
        public string OWNER { get; set; }
        public decimal INVL_DID { get; set; }
        public decimal INV_ID { get; set; }
        public decimal INVL_ID { get; set; }
        public decimal DISC_ID { get; set; }
        public string DISC_SIGN { get; set; }
        public decimal DISC_PERC { get; set; }
        public decimal DISC_VALUE { get; set; }
        public decimal DISC_ACC { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
