using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEFacturaDetalle
    {
        public string OWNER { get; set; }
        public decimal INVL_ID { get; set; }
        public decimal INV_ID { get; set; }
        public decimal INVL_ORDER { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal LIC_PL_ID { get; set; }
        public decimal MOD_ID { get; set; }
        public decimal EST_ID { get; set; }
        public decimal ADD_ID { get; set; }
        public decimal RATE_ID { get; set; }
        public decimal INVL_VAR1 { get; set; }
        public decimal INVL_VAR2 { get; set; }
        public decimal INVL_VAR3 { get; set; }
        public decimal INVL_VAR4 { get; set; }
        public decimal INVL_VAR5 { get; set; }
        public decimal INVL_VAR6 { get; set; }
        public decimal INVL_VAR7 { get; set; }
        public decimal INVL_GROSS { get; set; }
        public decimal INVL_DISC { get; set; }
        public decimal INVL_SURC { get; set; }
        public decimal INVL_BASE { get; set; }
        public decimal INVL_TAXES { get; set; }
        public decimal INVL_NET { get; set; }
        public decimal INV_TBASE { get; set; }
        public decimal INV_TTAXES { get; set; }
        public decimal INV_TDEDUCTIONS { get; set; }
        public decimal INV_TNET { get; set; }
        public decimal INVL_COLLECTB { get; set; }
        public decimal INVL_COLLECTT { get; set; }
        public decimal INVL_COLLECTD { get; set; }
        public decimal INVL_COLLECTN { get; set; }
        public decimal INVL_BALANCE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public string PERIODO { get; set; }
        public decimal PENDIENTE { get; set; }
        public string REC_DATE { get; set; }
        public decimal VAL_NOTACREDITO { get; set; }

        /// Planificacipon de la Licencia
        public DateTime LIC_DATE { get; set; }
        public string EST_NAME { get; set; }
        public decimal TOTAL { get; set; }
        public string LIC_NAME { get; set; }
        public decimal LIC_YEAR { get; set; }
        public decimal LIC_MONTH { get; set; }

        //Recibos factura detalle

        public List<BEFacturaDetalle> FacturaDetalle { get; set; }
        public List<BERecibo> Recibos { get; set; }

        //Reporte
        public DateTime INV_DATE { get; set; }
        public string INVT_DESC { get; set; }
        public string TAXN_NAME { get; set; }
        public string TAX_ID { get; set; }
        public string SOCIO { get; set; }
        public string ESTADO { get; set; }
        public string NMR_SERIAL { get; set; }
        public decimal INV_NUMBER { get; set; }
        public Nullable<DateTime> FECHA_CANCELACION { get; set; }
        public string TOTAL_LETRA { get; set; }
        public decimal INV_NET { get; set; }

        /// <summary>
        /// Monto Total  a Facturar
        /// </summary>
        public decimal? LIC_PL_AMOUNT { get; set; }
        /// <summary>
        /// Estado del periodo: A (ABIERTO) P (PARCIAL) T ( TOTAL)
        /// </summary>
        public string LIC_PL_STATUS { get; set; }
        /// <summary>
        /// Tipo de reporte
        /// </summary>
        public int ReporteAgrupado { get; set; }

        //Capturar el Total de Nota de Credito
        public decimal INVL_CN_TOTAL { get; set; }

        //COBROS
        public decimal MONTO_ORIGINAL { get; set; }
        public decimal MONTO_POR_COBRAR { get; set; }
        public decimal CUR_VALUE { get; set; }
        public string CUR_ALPHA { get; set; }
        public decimal REC_ID { get; set; }
        public decimal INVL_GROSS_SOLES { get; set; }
        public decimal INVL_TAXES_SOLES { get; set; }
        public decimal INVL_DISC_SOLES { get; set; }
        public decimal INVL_NET_SOLES { get; set; }
        public decimal INVL_BALANCE_SOLES { get; set; }
        public decimal REC_PID { get; set; }
        public decimal INVL_COLLECTN_SOLES { get; set; }
        public decimal MREC_ID { get; set; }

        //DIVISION - LICENCIA
        public decimal OFF_ID { get; set; }
        public decimal DAD_ID { get; set; }
    }
}
