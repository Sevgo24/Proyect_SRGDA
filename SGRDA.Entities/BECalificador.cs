using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BECalificador : Paginacion
    {
        public Int32 RowNumber { get; set; }
        public string OWNER { get; set; }
        public decimal QUC_ID { get; set; }
        public decimal QUA_ID { get; set; }
        public string TIPO { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }
    }
}
