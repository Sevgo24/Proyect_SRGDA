using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_NUMBERING : Paginacion
    {

        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Consecutivo Numerador")]
        public decimal NMR_ID { get; set; }

        [DisplayName("territorio Numerador")]
        [Required(ErrorMessage = "Ingrese territorio numerador")]
        public decimal TIS_N { get; set; }

        [DisplayName("Tipo Numerador")]
        [MaxLength(2)]
        public string NMR_TYPE { get; set; }

        //[DisplayName("Serial numerador")]
        //[MaxLength(4)]
        public string NMR_SERIAL { get; set; }

        [DisplayName("Nombre Documento")]
        [MaxLength(40)]
        [Required(ErrorMessage = "Ingrese Nombre")]
        public string NMR_NAME { get; set; }

        [DisplayName("Indicador Serial")]
        [MaxLength(1)]
        public string W_SERIAL { get; set; }

        [DisplayName("indicador Año")]
        [MaxLength(1)]
        public string W_YEAR { get; set; }

        [DisplayName("Inicio Rango")]
        [Required(ErrorMessage = "Ingrese Rango Desde")]
        public decimal NMR_FORM { get; set; }

        [DisplayName("Fin Rango")]
        [Required(ErrorMessage = "Ingrese Rango Hasta")]
        public decimal NMR_TO { get; set; }

        [DisplayName("Estado Actual")]
        [Required(ErrorMessage = "Ingrese Rango Actual")]
        public decimal NMR_NOW { get; set; }

        [DisplayName("Ajustes")]
        [Required(ErrorMessage = "Ingrese Ajuste")]
        [MaxLength(1)]
        public string AJUST { get; set; }

        [DisplayName("Posicion de la Serie")]
        public decimal POS_SERIAL { get; set; }

        [DisplayName("Longitud del Año")]
        
        public decimal LON_YEAR { get; set; }

        [DisplayName("Posicion del Año")]
        public decimal POS_YEAR { get; set; }

        [DisplayName("Divisor 1")]
        [MaxLength(3)]
        public string DIVIDER1 { get; set; }

        [DisplayName("Divisor 2")]
        [MaxLength(3)]
        public string DIVIDER2 { get; set; }

        [DisplayName("Nnumerador Manual")]
        [MaxLength(1)]
        public string NMR_MANUAL { get; set; }
        
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        [DisplayName("Tipo Numerador")]
        public string NMR_TDESC { get; set; }

        public string DIV_ID { get; set; }

        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }
    }
}
