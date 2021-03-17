using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_LAWSUITE_ACTIVITIES_TYPE : Paginacion
    {
        [DisplayName("propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Id")]
        [Required(ErrorMessage = "Ingrese id")]
        [MaxLength(3)]
        public string LAWS_ATY { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string LAWS_ATDESC { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
