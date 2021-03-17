using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Validation
{
    public class REF_DIV_SUBTYPE
    {
        [DisplayName("PROPIETARIO")]
        [Required(ErrorMessage = "Ingrese el propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("DIVISION")]
        public decimal DAD_ID { get; set; }

        [DisplayName("SUBTIPO DIVISION")]
        [Required(ErrorMessage = "Ingrese el subtipo division")]
        public decimal DAD_STYPE { get; set; }

        [DisplayName("IDENTIFICACION CORTA")]
        [StringLength(40, ErrorMessage = "El identificador corto no debe ser mayor a 40 caracteres")]
        [Required(ErrorMessage = "Ingrese el subtipo division")]
        public string DAD_SNAME { get; set; }

        [DisplayName("IDENTIFICACION LARGA")]
        [StringLength(40, ErrorMessage = "El identificador largo no debe ser mayor a 40 caracteres")]
        public string DAD_NAME { get; set; }

        [DisplayName("JERARQUIA SUBDIVISION")]
        [MaxLength(3)]
        [Required(ErrorMessage = "Ingrese jerarquia subtipo division")]
        public string DAD_BELONGS { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
