using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BECorreoType : Paginacion
    {
        public string OWNER { get; set; }
        public decimal MAIL_TYPE { get; set; }
        public string MAIL_TDESC { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public int ESTADO { get; set; }
        public string ACTIVO { get; set; }
        public string MAIL_OBSERV { get; set; }
    }
}
