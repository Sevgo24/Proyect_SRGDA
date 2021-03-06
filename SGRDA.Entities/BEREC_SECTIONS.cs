using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_SECTIONS : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Id. Localidad")]
        [MaxLength(3)]
        [Required(ErrorMessage = "Ingrese id")]
        public string SEC_ID { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string SEC_DESC { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
