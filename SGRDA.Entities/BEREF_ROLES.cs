using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREF_ROLES : Paginacion
    {
        [DisplayName("Ident. Corta de División")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Id. Rol")]
        [Required(ErrorMessage = "Ingrese tipo de cargo(rol)")]
        public decimal ROL_ID { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string ROL_DESC { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }
    }
}
