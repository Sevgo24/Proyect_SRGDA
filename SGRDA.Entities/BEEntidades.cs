using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEEntidades
    {
        public decimal ENT_ID { get; set; }
        public string ENT_DESC { get; set; }
        public string ENT_TYPE { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAR { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}
