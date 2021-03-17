using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_MOV_TYPE : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Tipo Movimiento")]
        [MaxLength(3)]
        [Required(ErrorMessage = "Ingrese Id")]
        public string MOV_TYPE { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string MOV_DESC { get; set; }

        [DisplayName("Signo")]
        [MaxLength(1)]
        [Required(ErrorMessage = "Ingrese signo tipo movimiento")]
        public string MOV_SIGN { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
