using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEOficinaDivisionModalidad
    {
        public string OWNER { get; set; }
        public decimal DIV_RiGHTS_ID { get; set; }
        public decimal ID_COLL_DIV { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal DAD_ID { get; set; }
        public string MOG_ID { get; set; }
        public string MOG_DESC { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
    }
}
