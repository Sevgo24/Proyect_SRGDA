using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_CHARACTERISTICS : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }


        [DisplayName("Id")]
        public decimal CHAR_ID { get; set; }


        [DisplayName("Descripción Corta")]
        [Required(ErrorMessage = "Ingrese descripcion corta")]
        [MaxLength(20)]
        public string CHAR_SHORT { get; set; }

        [DisplayName("Descripción Larga")]
        [Required(ErrorMessage = "Ingrese descripcion larga")]
        [MaxLength(100)]
        public string CHAR_LONG { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}
