using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BECodigoPostal : Paginacion
    {
        public decimal CPO_ID { get; set; }
        public decimal TIS_N { get; set; }
        public string DescripcionUbigeo { get; set; }
        public string DAD_VNAME { get; set; }
        public decimal POSITIONS { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
