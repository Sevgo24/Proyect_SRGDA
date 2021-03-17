using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaDescuento
    {
        public string OWNER { get; set; }
        public decimal RATE_DISC_ID { get; set; }
        public decimal RATE_ID { get; set; }
        public decimal DISC_ID { get; set; }

        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }

        public decimal DISC_TYPE { get; set; }
        public string DISC_TYPE_NAME { get; set; }
        public string DISC_NAME { get; set; }
        public string DISC_SIGN { get; set; }
        public decimal DISC_PERC { get; set; }
        public decimal DISC_VALUE { get; set; }
        public string DISC_ACC { get; set; }
        public string DISC_AUT { get; set; }
        public decimal monto { get; set; }

    }
}
