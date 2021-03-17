using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEReporteDistribucion
    {

        public int ID { get; set; }
        public string GRUPO_ID { get; set; }
        public string GRUPO { get; set; }
        public string SAP_CODIGO { get; set; }
        public decimal MOD_ID { get; set; }

        public string MODALIDAD { get; set; }
        public string DERECHO { get; set; }
        public string CLIENTE { get; set; }
        public decimal LIC_ID { get; set; }
        public string LICENCIA { get; set; }

        public decimal EST_ID { get; set; }
        public string ESTABLECIMIENTO { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string SERIE { get; set; }
        public decimal NUMERO { get; set; }

        public string PERIODO { get; set; }
        public string MONEDA_FACTURA { get; set; }
        public decimal MONTO_FACTURADO { get; set; }
        public decimal IMPORTE_RECAUDADO_SOLES { get; set; }
        public int ANIO_CONTABLE { get; set; }


        public int MES_CONTABLE { get; set; }
        public DateTime FECHA_CONTABLE { get; set; }
        public string OFICINA { get; set; }
        public string RAZON_SOCIAL { get; set; }


        public decimal RECAUDO { get; set; }
        public decimal NC { get; set; }
        public decimal NETO { get; set; }

        public string TIPO_REPORTE { get; set; }

        // DESCUENTOS ADMINISTRATIVOS
        public decimal MOD_AD { get; set; }
        public decimal MOD_AD_MONTO { get; set; }
        public decimal MOD_SC { get; set; }
        public decimal MOD_SC_MONTO { get; set; }
        public decimal MOD_AC { get; set; }
        public decimal MOD_AC_MONTO { get; set; }
        public decimal MOD_LY { get; set; }
        public decimal MOD_LY_MONTO { get; set; }
        public decimal MOD_FC { get; set; }
        public decimal MOD_FC_MONTO { get; set; }
        public decimal MOD_AR { get; set; }
        public decimal MOD_AR_MONTO { get; set; }

        public Nullable<DateTime> FECHA_DEPOSITO { get; set; }
        public int BEC_ESPECIAL { get; set; }

        public string BANCO_DESTINO { get; set; }
        public string CTA { get; set; }
        public string CTA_SAP { get; set; }
        public string NRO_CONFIRMACION { get; set; }
        public decimal MONTO_DEPOSITADO { get; set; }

        public Nullable<DateTime> FEC_EMI_FACTURA { get; set; }
        public string ID_CLIENTE_SAP { get; set; }
        public decimal ID_CLIENTE { get; set; }
        public decimal ID_COBRO { get; set; }
        public decimal REC_PID { get; set; }
        public decimal ID_FACTURA { get; set; }
        public decimal ID_FACTURADET { get; set; }

    }
}
