using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDetalleMetodoPago : Paginacion
    {
        public string OWNER { get; set; }
        public decimal REC_PID { get; set; }
        public decimal REC_ID { get; set; }
        public decimal MREC_ID { get; set; }
        public decimal REC_DID { get; set; }
        public string REC_PWID { get; set; }
        public decimal REC_PVALUE { get; set; }
        public string REC_CONFIRMED { get; set; }
        public DateTime? REC_DATEDEPOSITE { get; set; }
        public DateTime? REC_DATECONFIRMED { get; set; }
        public string REC_USERCONFIRMED { get; set; }
        public string REC_CODECONFIRMED { get; set; }
        public DateTime? REC_DEVOLUTION { get; set; }
        public DateTime? REC_ACCOUNTED { get; set; }
        public DateTime? REC_CONCILIATION { get; set; }

        public string BNK_NAME { get; set; }
        public string BRCH_NAME { get; set; }
        public string BNK_ID { get; set; }
        public string BRCH_ID { get; set; }
        public decimal BPS_ACC_ID { get; set; }
        public string BACC_NUMBER { get; set; }
        public string REC_REFERENCE { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public decimal REC_BEC_ESPECIAL { get; set; }
        public decimal REC_BEC_ESPECIAL_APPROVED { get; set; }

        public string REC_BEC_ESPECIAL_OBSERVATION { get; set; }
        public string REC_BEC_ESPECIAL_USER_APPROVED { get; set; }

        //Formas de pago
        public string REC_PWDESC { get; set; }
        public decimal INV_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public string ESTADO_DEPOSITO { get; set; }
        public string CUR_ALPHA { get; set; }
        public string MONEDA { get; set; }
        public string CTA_CLIENTE { get; set; }
        public string FECHA_DEP { get; set; }
        public string REC_OBSERVATION { get; set; }
        public List<BEDetalleMetodoPago> ListarDetalleMetodoPago { get; set; }

        //COBROS
        public decimal CUR_VALUE { get; set; }
        public decimal CONVERSION_SOLES { get; set; }
        public decimal REC_BALANCE { get; set; }
        public decimal CONVERSION_SOLES_BALANCE { get; set; }

        //BANDEJA DE DEPOSITOS
        public string BANCO_DESTINO { get; set; }
        public string CUENTA_DESTINO { get; set; }
        public string OFICINA_RECAUDO { get; set; }
        public decimal OFF_ID { get; set; }
        public string REC_OBSERVATION_USER { get; set; }

        public string FECHA_INGRESO { get; set; }
        public string MONTO_DEPOSITADO { get; set; }
        public string FECHA_CONFIRMACION { get; set; }
        public string SALDO_MONTO_DEPOSITADO { get; set; }
        public decimal DOC_ID { get; set; }
        public decimal DOC_VERSION { get; set; }
        public string PERMISO { get; set; }
    }
}
