using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREF_ADDRESS_TYPE : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Id")]
        [Required(ErrorMessage = "Ingrese Territorio de impuesto")]
        public decimal ADDT_ID { get; set; }

        [DisplayName("Descripción")]
        [MaxLength(30)]
        public string DESCRIPTION { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }

        public int ESTADO { get; set; }
        public string ACTIVO { get; set; }
        public string ADDT_OBSERV { get; set; }
    }
}
