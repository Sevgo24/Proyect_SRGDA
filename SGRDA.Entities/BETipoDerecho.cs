using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BETipoDerecho : Paginacion
    {
        public Int32 RowNumber { get; set; }
        public string OWNER { get; set; }
        public string RIGHT_COD { get; set; }
        public string RIGHT_DESC { get; set; }
        public string WORK_RIGHT_CODE { get; set; }
        public string WORK_RIGHT_DESC { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
