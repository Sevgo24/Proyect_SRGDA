using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BERedes_Sociales : Paginacion
    {
        public string OWNER { get; set; }
        public decimal CONT_ID { get; set; }
        public decimal CONT_TYPE { get; set; }
        public string CONT_DESC { get; set; }
        public string CONT_TDESC { get; set; }
        public string CONT_OBS { get; set; }
        public decimal ENT_ID { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
