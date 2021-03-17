using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREF_DIVISIONES_VALUES : Paginacion
    {
        public string OWNER { get; set; }
        public decimal DADV_ID { get; set; }
        public decimal DAD_ID { get; set; }
        public decimal DAD_STYPE { get; set; }
        public string DAD_VCODE { get; set; }
        public string DAD_VNAME { get; set; }
        public decimal DAD_BELONGS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }

        public decimal TIS_N { get; set; }
        public string NOMBRE { get; set; }
        public string SUBDIVISION { get; set; }
        public string DEPENDENCIA { get; set; }
    }
}
