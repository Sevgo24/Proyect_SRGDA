using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_BLOCKS : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Código")]
        [Required(ErrorMessage = "Ingrese código de bloqueo")]
        public decimal BLOCK_ID { get; set; }

        [DisplayName("Descripción")]
        [MaxLength(40)]
        public string BLOCK_DESC { get; set; }

        [DisplayName("Arrastre Bloqueo")]
        [MaxLength(1)]
        public string BLOCK_PULL { get; set; }

        [DisplayName("Autorización")]
        [MaxLength(1)]
        public string BLOCK_AUT { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
