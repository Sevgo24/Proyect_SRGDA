using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREF_DIV_STYPE_CHAR : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Id.Caracteristica")]
        [MaxLength(3)]
        public string DAC_ID { get; set; }

        [DisplayName("Tipo División")]
        [MaxLength(3)]
        public string DAD_TYPE { get; set; }

        [DisplayName("SubTipo de la Caracteristica")]
        [MaxLength(3)]
        public string DAD_STYPE { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        [DisplayName("Tipo de Divisiones")]
        [MaxLength(3)]
        public string DAD_TNAME { get; set; }

        [DisplayName("Subtipos de Divisiones")]
        [MaxLength(3)]
        public string DAD_SNAME { get; set; }

        [DisplayName("Caracteristicas de Divisiones")]
        [MaxLength(3)]
        public string DESCRIPTION { get; set; }
    }
}
