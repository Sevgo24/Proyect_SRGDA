using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDerecho
    {
        public decimal Id { get; set; }
        public string RIGHT_COD { get; set; }
        public string RIGHT_DESC { get; set; }
        public string WORK_RIGHT_CODE { get; set; }
        public string WORK_RIGHT_DESC { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

    }
}
