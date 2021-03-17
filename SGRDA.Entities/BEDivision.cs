using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEDivision : Paginacion
    {
        public string OWNER { get; set; }
        public decimal DADV_ID { get; set; }
        public decimal DAD_ID { get; set; } 
        public string DAD_NAME { get; set; }
        public string DAD_CODE { get; set; }
        public decimal DAD_STYPE { get; set; }
        public string DAD_SNAME { get; set; }
        public string DAD_VCODE { get; set; }
        public string DAD_VNAME { get; set; }
        public string DAD_BELONGS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
