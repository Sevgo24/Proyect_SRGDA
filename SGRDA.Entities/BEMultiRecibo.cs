using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEMultiRecibo : Paginacion
    {
        public List<BEMultiRecibo> ListarMultiRecibo { get; set; }

        public string OWNER { get; set; }
        public decimal MREC_ID { get; set; }
        public decimal MREC_NMR { get; set; }
        public decimal NMR_ID { get; set; }
        public string SERIAL { get; set; }
        public string MREC_NUMBER { get; set; }
        public DateTime MREC_DATE { get; set; }
        public decimal MREC_TBASE { get; set; }
        public decimal MREC_TTAXES { get; set; }
        public decimal MREC_TTDEDUCTION { get; set; }
        public decimal MREC_TTOTAL { get; set; }//TOTAL DEPOSITOS
        public decimal MREC_TFACTURAS { get; set; }
        public decimal MREC_TDEPOSITOS { get; set; }
        public decimal MREC_TTDISCOUNT { get; set; }
        public decimal BPS_ID { get; set; }
        public string MREC_OBSERVATION { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public decimal MREC_NMR_RECIBO { get; set; }

        public string FECHA { get; set; }
        public string RECIBO_BEC { get; set; }
        public string SERIAL_BEC { get; set; }
        public decimal MONTO { get; set; }
        public decimal NMR_ID_REC { get; set; }
        public string VOUCHER { get; set; }
        public string BANCO { get; set; }
        public string SUCURSAL { get; set; }
        public string CUENTA { get; set; }
        public string TIPO_MONEDA { get; set; }
        public decimal ESTADO_MULTIRECIBO { get; set; }
        public string ESTADO_MULTIRECIBO_DES { get; set; }
        public string STRMREC_TBASE { get; set; }
        public string STRMREC_TTAXES { get; set; }
        public string STRMREC_TTDEDUCTION { get; set; }
        public string STRMREC_TTOTAL { get; set; }
        public string STRMREC_TTDISCOUNT { get; set; }

        public string VERSION { get; set; }
        public string SOCIO { get; set; }
        //TIPO: SIMPLE , COMPUESTO
        public string TIPO { get; set; }
        public decimal REC_ID { get; set; }
        public decimal BNK_ID { get; set; }
        public decimal BACC_NUMBER { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal CUR_VALUE { get; set; }
        public decimal ESTADO_CONFIRMACION { get; set; }
        public string ESTADO_CONFIRMACION_DES { get; set; }
        public string ESTADO_COBRO { get; set; }
        public string OFICINA { get; set; }
        public DateTime FECH_DEPO { get; set; }
        public DateTime FECHA_CONFIR { get; set; }

        public List<BEMultiReciboDetalle> ListaMultiReciboDet { get; set; }
        public List<BERecibo> ListaRecibo { get; set; }
        public List<BEDetalleMetodoPago> ListaReciboDetalleVoucher { get; set; }
        public List<BEReciboDetalle> ListaReciboDetalleFactura { get; set; }
    }
}
