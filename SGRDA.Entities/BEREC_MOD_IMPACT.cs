using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_MOD_IMPACT : Paginacion
    {
        [DisplayName("PROPIETARIO")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Id")]
        [Required(ErrorMessage = "Ingrese id")]
        [MaxLength(3)]
        public string MOD_INCID { get; set; }

        [DisplayName("DESCRIPCIÓN")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string MOD_IDESC { get; set; }

        [DisplayName("DETALLE")]
        [Required(ErrorMessage = "Ingrese detalle de la incidencia")]
        [MaxLength(40)]
        public string MOD_IDET { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
