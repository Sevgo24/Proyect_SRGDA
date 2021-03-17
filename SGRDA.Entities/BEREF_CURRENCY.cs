using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREF_CURRENCY : Paginacion
    {
        [DisplayName("Código ISO")]
        [Required(ErrorMessage = "Ingrese código iso")]
        [MaxLength(3)]
        public string CUR_ALPHA { get; set; }

        [DisplayName("Moneda")]
        [Required(ErrorMessage = "Ingrese nombre de la moneda")]
        [MaxLength(40)]
        public string CUR_DESC { get; set; }

        [DisplayName("Código Numerico")]
        [Required(ErrorMessage = "Ingrese código numérico de la moneda")]
        public decimal CUR_NUM { get; set; }

        [DisplayName("Unidad Mayor Medida")]
        [MaxLength(30)]
        public string UNIT_MAJOR { get; set; }

        [DisplayName("Unidad Menor Medida")]
        [MaxLength(30)]
        public string UNIT_MINOR { get; set; }

        [DisplayName("Numero Decimales")]
        public decimal DECIMALS { get; set; }

        [DisplayName("Formato Moneda")]
        [MaxLength(15)]
        public string FORMAT { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }
    }
}
