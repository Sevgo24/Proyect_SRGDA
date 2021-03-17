using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_USES_TYPE : Paginacion
    {
        [DisplayName("PROPIETARIO")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("ID TIPO DE USO")]
        [Required(ErrorMessage = "Ingrese id")]
        [MaxLength(3)]
        public string USET_ID { get; set; }

        [DisplayName("DESCRIPCIÓN")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string USET_DESC { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
