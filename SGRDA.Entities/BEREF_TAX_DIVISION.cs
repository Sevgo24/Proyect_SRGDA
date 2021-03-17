using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREF_TAX_DIVISION : Paginacion
    {
        [DisplayName("Ident. Corta de División")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Tipo de Direcciones")]
        [Required(ErrorMessage = "Ingrese Territorio de impuesto")]
        public decimal TAXD_ID { get; set; }

        [DisplayName("Territorio División Fiscal")]
        public decimal TIS_N { get; set; }

        [DisplayName("Divisiób Fiscal")]
        [Required(ErrorMessage = "Ingrese Descripción.")]
        public string DESCRIPTION { get; set; }       

        [DisplayName("Fecha de Baja Logica")]
        public DateTime? ENDS { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public string NAME_TER { get; set; }

        public string Activo { get; set; }
    }
}
