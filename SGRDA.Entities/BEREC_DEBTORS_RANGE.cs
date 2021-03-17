using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_DEBTORS_RANGE : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Código")]
        public decimal RANGE_COD { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string DESCRIPTION { get; set; }

        [DisplayName("Inicio Rango")]
        public decimal RANGE_FROM { get; set; }

        [DisplayName("Fin Rango")]
        public decimal RANGE_TO { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
