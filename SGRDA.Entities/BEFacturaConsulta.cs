using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEFacturaConsulta
    {
        public string OWNER { get; set; }
        public decimal INV_ID { get; set; }
        public string CUR_ALPHA { get; set; }
        public decimal INV_NMR { get; set; }
        public decimal INV_NUMBER { get; set; }
        public Nullable<DateTime> INV_DATE { get; set; }
        public decimal INV_TYPE { get; set; }
        public string INV_DETAIL { get; set; }
        public string INV_PHASE { get; set; }
        public decimal INV_REPRINTS { get; set; }
        public decimal INV_COPIES { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal ADD_ID { get; set; }
        public decimal INV_PRE { get; set; }
        public decimal INV_PROCES { get; set; }
        public Nullable<DateTime> INV_PRINT_DATE { get; set; }
        public Nullable<DateTime> INV_REPRINT_DATE { get; set; }
        public Nullable<DateTime> INV_COPY_DATE { get; set; }
        public Nullable<DateTime> INV_NULL { get; set; }
        public string INV_NULLREASON { get; set; }
        public Nullable<DateTime> INV_ACCOUNTED { get; set; }
        public decimal INV_ACC_PROCESS { get; set; }
        public Nullable<DateTime> INV_LIQ_COM_TOT { get; set; }
        public string INV_OBSERV { get; set; }
        public string INV_CN_IND { get; set; }
        public decimal INV_CN_REF { get; set; }
        public decimal INV_BASE { get; set; }
        public decimal INV_TAXES { get; set; }
        public decimal INV_NET { get; set; }
        public decimal INV_TBASE { get; set; }
        public decimal INV_TTAXES { get; set; }
        public decimal INV_TDEDUCTIONS { get; set; }
        public decimal INV_TNET { get; set; }
        public decimal INV_COLLECTB { get; set; }
        public decimal INV_COLLECTT { get; set; }
        public decimal INV_COLLECTD { get; set; }
        public decimal INV_COLLECTN { get; set; }
        public decimal INV_BALANCE { get; set; }
        public Nullable<DateTime> INV_DISTRIBUTED { get; set; }
        public decimal INV_DIST_PROCESS { get; set; }
        public string INV_KEY { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public decimal LIC_PL_ID { get; set; }
        public decimal INV_EXPID { get; set; }
        public string estados_Pago { get; set; }
        public string NMR_SERIAL { get; set; }
        public string estadosPago { get; set; }
        public string estadoFactura { get; set; }

        /// <summary>
        /// Facturacion Masiva
        /// </summary>
        public decimal Nro { get; set; }
        public string SOCIO { get; set; }
        public string MONEDA { get; set; }
        public string TIPO_PAGO { get; set; }
        public string GRUPO_FACT { get; set; }
        public decimal TOTAL { get; set; }

        public List<BEFactura> ListarFactura { get; set; }
        public List<BELicencias> ListarLicencia { get; set; }
        public List<BELicenciaPlaneamiento> ListarLicenciaPlaneamiento { get; set; }
        public List<BEFacturaDetalle> ListarDetalleFactura { get; set; }


        //Recibos - Detalle Métodos de pago 
        public List<BERecibo> ListarRecibosPendientes { get; set; }
        public List<BEDetalleMetodoPago> ListarDetalleRecibosPedientes { get; set; }//(el detalle son los métodos de pago)

        public decimal LIC_ID { get; set; }
        public decimal MONTO_DET { get; set; }
        public decimal MOD_ID { get; set; }
        public decimal EST_ID { get; set; }
        public decimal? INVG_ID { get; set; }
        public decimal LIC_TYPE { get; set; }
        public Nullable<DateTime> LIC_PL_DATE { get; set; }
        public string PAY_ID { get; set; }
        //Facturación datos para Recaudación cobro
        public Nullable<DateTime> INV_EXPDATE { get; set; }
        public string INVT_DESC { get; set; }
        //Calculo de la Tarifa
        public List<BEREC_RATES_GRAL> TT_Tarifa { get; set; }
        public List<BETarifaRegla> TT_Regla { get; set; }
        public List<BETarifaCaracteristica> TT_Caracteristica { get; set; }
        public List<BETarifaTest> TT_Test { get; set; }
        public List<BEDescuentos> TT_Descuento { get; set; }
        public decimal TAXV_VALUEP { get; set; }
        public decimal SUBTOTAL { get; set; }
        public decimal DESCUENTO { get; set; }

        public decimal TAXT_ID { get; set; }
        public string TAX_ID { get; set; }
        public string ADDRESS { get; set; }
        public string RUM { get; set; }

        public IList<BEFacturaConsulta> ListaConsultaFactura { get; set; }
        public BEFacturaConsulta()
        {
            ListaConsultaFactura = new List<BEFacturaConsulta>();
        }

        public int TotalVirtual { get; set; }
    }
}
