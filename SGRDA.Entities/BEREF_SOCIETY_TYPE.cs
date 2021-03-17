using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREF_SOCIETY_TYPE : Paginacion
    {

        [DisplayName("ID")]
        [MaxLength(3)]
        [Required(ErrorMessage = "Ingrese tipo de derecho")]
        public string SOC_TYPE { get; set; }

        [DisplayName("Descripción")]
        [MaxLength(40)]
        [Required(ErrorMessage = "Ingrese descripcion de tipo de derecho")]
        public string SOC_DESC { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}
