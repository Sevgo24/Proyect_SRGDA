using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEFacturaVencimiento
    {
        public string OWNER { get; set; }
        public decimal INV_EXPID { get; set; }
        public decimal INV_ID { get; set; }
        public DateTime INV_EXPDATE { get; set; }
        public decimal INV_EXPBASE { get; set; }
        public decimal INV_EXPTAX { get; set; }
        public decimal INV_EXPTOTAL { get; set; }
        public decimal INV_EXPLBASE { get; set; }
        public decimal INV_EXPLTAX { get; set; }
        public decimal INV_EXPLTOTAL { get; set; }
        public decimal INV_EXPLRET { get; set; }
        public string INV_EXPFRAGMENT { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
