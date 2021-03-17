using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_TAX_ID : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Código")]
        [Required(ErrorMessage = "Ingrese identificador fiscal")]
        public decimal TAXT_ID { get; set; }

        [DisplayName("Territorio")]
        public decimal TIS_N { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Ingrese Nombre")]
        [MaxLength(40)]
        public string TAXN_NAME { get; set; }

        [DisplayName("Longitud")]
        [Required(ErrorMessage = "Ingrese longitud")]
        public decimal TAXN_POS { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(300)]
        public string TEXT_DESCRIPTION { get; set; }

        public string NAME_TER { get; set; }

        public DateTime? ENDS { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public string ACTIVO { get; set; }
    }
}
