using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGRDA.Entities
{
    public partial class BETipoDescuento : Paginacion
    {
        public string OWNER { get; set; }
        public decimal DISC_TYPE { get; set; }
        public string DISC_TYPE_NAME { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
