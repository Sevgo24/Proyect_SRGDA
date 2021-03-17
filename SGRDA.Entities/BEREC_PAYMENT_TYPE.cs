using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_PAYMENT_TYPE : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }
        public int RowNumber { get; set; }

        [DisplayName("Forma de Pago")]
        [MaxLength(2)]
        [Required(ErrorMessage = "Ingrese forma de pago")]
        public string PAY_ID { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string DESCRIPTION { get; set; }

        [DisplayName("Pago Bancos")]
        public bool PAY_BANK { get; set; }
        [DisplayName("Recibo Bancario")]
        public bool PAY_BANK_RECEIPT { get; set; }
        [DisplayName("Recibo Agente")]
        public bool PAY_AGE_RECEIPT { get; set; }
        [DisplayName("Transferencia")]
        public bool PAY_TRANSFER { get; set; }

        [DisplayName("Fecha")]
        public bool PAY_DATE_FIX { get; set; }

        public Decimal PAY_DATE_FIX_DAY { get; set; }

        [DisplayName("Vencimiento 1")]
        public decimal VTO1 { get; set; }
        [DisplayName("Vencimiento 2")]
        public decimal VTO2 { get; set; }
        [DisplayName("Vencimiento 3")]
        public decimal VTO3 { get; set; }
        [DisplayName("Vencimiento 4")]
        public decimal VTO4 { get; set; }
        [DisplayName("Vencimiento 5")]
        public decimal VTO5 { get; set; }
        [DisplayName("Vencimiento 6")]
        public decimal VTO6 { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }
    }
}
