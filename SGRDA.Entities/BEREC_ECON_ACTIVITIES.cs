using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_ECON_ACTIVITIES : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Actividad Económica")]
        [Required(ErrorMessage = "Ingrese actividad economica")]
        [MaxLength(10)]
        public string ECON_ID { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string ECON_DESC { get; set; }

        [DisplayName("Actividad Dependencia")]
        [MaxLength(10)]
        public string ECON_BELONGS { get; set; }

        public string ECON_ID_Bellong { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }
    }
}
