using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEReciboDetalle
    {
        public string OWNER { get; set; }
        public decimal MREC_ID { get; set; }
        public decimal REC_DID { get; set; }
        public decimal REC_ID { get; set; }
        public decimal INV_ID { get; set; }
        public decimal INV_EXPID { get; set; }
        public decimal REC_BASE { get; set; }
        public decimal REC_TAXES { get; set; }
        public decimal REC_DISCOUNTS { get; set; }
        public decimal REC_DEDUCTIONS { get; set; }
        public decimal REC_TOTAL { get; set; }
        public decimal REC_TOTAL_PAGAR { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public string TIPO_DOC { get; set; }
        public string FACTURA { get; set; }
        public decimal NMR_ID { get; set; }
        public string NMR_SERIAL { get; set; }
        public decimal NMR_NOW { get; set; }

        public string RECIBO { get; set; }
        public decimal NMR_ID_REC { get; set; }
        public string NMR_SERIAL_REC { get; set; }
        public decimal NMR_NOW_REC { get; set; }
        
        public decimal BPS_ID { get; set; }
        public string NUM_DOC { get; set; }
        public decimal SALDO_PENDIENTE { get; set; }
        public string MONEDA { get; set; }
        public string CUR_ALPHA { get; set; }
        public decimal CUR_VALUE { get; set; }

        //COBROS
        public decimal APLICAR_COBRO { get; set; }
        public decimal APLICAR_COBRO_SOLES { get; set; }
        public decimal APLICAR_COBRO_DOLARES { get; set; }
        public decimal CONVERSION_TOTAL_SOLES { get; set; }
        public decimal REC_BALANCE { get; set; }
        public decimal CONVERSION_BALANCE_SOLES { get; set; }
        public decimal REC_PID { get; set; }
        
    }
}
