using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREF_DIV_TYPE : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        [Required(ErrorMessage = "Ingrese propietario")]
        public string OWNER { get; set; }

        [DisplayName("Código")]
        [MaxLength(3)]
        [Required(ErrorMessage = "Ingrese tipo de división")]
        public string DAD_TYPE { get; set; }


        [DisplayName("Tipo de división")]
        [Required(ErrorMessage = "Ingrese los datos para el registro.")]
        [MaxLength(40)]
        public string DAD_TNAME { get; set; }

        [DisplayName("Territorio")]
        public decimal TIS_N { get; set; }

        public string NAME_TER { get; set; }

        [DisplayName("Fecha Creación")]
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }

        [DisplayName("Fecha ModificaciónS")]
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        [DisplayName("Usuario Creación")]
        [MaxLength(30)]
        public string LOG_USER_CREAT { get; set; }

        [DisplayName("Usuario Modificación")]
        [MaxLength(30)]
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
        public string DIVT_OBSERV { get; set; }

        public int accion { get; set; }
    }
}
