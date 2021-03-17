using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEClaseCreacion : Paginacion
    {
        public string OWNER { get; set; }
        public string RIGHT_COD { get; set; }
        public string auxRIGHT_COD { get; set; }
        public string RIGHT_DESC { get; set; }
        public string CLASS_COD { get; set; }
        public string CLASS_DESC { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }

        public List<BEDerecho> Derecho { get; set; }

        public decimal SEQUENCE { get; set; }
    }
}
