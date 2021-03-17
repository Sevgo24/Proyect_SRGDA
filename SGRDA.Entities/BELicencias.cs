using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicencias : Paginacion
    {
        public List<BELicencias> ListaLicencias { get; set; }
        public BELicencias()
        {
            ListaLicencias = new List<BELicencias>();
        }

        public string OWNER { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal LIC_TYPE { get; set; }
        /// <summary>
        /// Estado de la Licencia
        /// </summary>
        public decimal LICS_ID { get; set; }
        public string CUR_ALPHA { get; set; }
        public string LIC_TEMP { get; set; }
        public string LIC_NAME { get; set; }
        public string LIC_DESC { get; set; }

        /// <summary>
        /// Tipo de Envio de Factura
        /// </summary>
        public decimal? LIC_SEND { get; set; }
        /// <summary>
        /// Indicador de requiere documentos
        /// </summary>
        public string LIC_DREQ { get; set; }
        public string LIC_CREQ { get; set; }
        public string LIC_PREQ { get; set; }

        /// <summary>
        /// indicador de factura detalla
        /// </summary>
        public string LIC_INVD { get; set; }
        /// <summary>
        /// Formas de Facturacion
        /// </summary>
        public decimal INVF_ID { get; set; }
        /// <summary>
        /// indicador de descuentos visibles
        /// </summary>
        public string LIC_DISC { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public decimal MOD_ID { get; set; }
        public decimal RATE_ID { get; set; }
        public decimal RAT_FID { get; set; }
        public decimal EST_ID { get; set; }
        public decimal BPS_ID { get; set; }
        /// <summary>
        /// Grupo de Facturacion
        /// </summary>
        public decimal INVG_ID { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public decimal? LIC_MASTER { get; set; }

        /// <summary>
        /// Para Obtener la descripción del establesimiento
        /// </summary>
        public string Establecimiento { get; set; }
        /// <summary>
        /// Para Obtener la descripción del tipo
        /// </summary>
        public string Tipo { get; set; }
        /// <summary>
        /// Para Obtener la descripción del estado
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// Para Obtener la descripción de la modalidad
        /// </summary>
        public string Modalidad { get; set; }
        /// <summary>
        /// Para Obtener la descripción del usuario
        /// </summary>
        public string UsuDerecho { get; set; }
        /// <summary>
        /// Para Obtener el monto de la tarifa
        /// </summary>
        public decimal MontoTarifa { get; set; }
        /// <summary>
        /// Para Obtener fecha de la l
        /// </summary>
        public DateTime LIC_DATE { get; set; }


        public string PAY_ID { get; set; }

        /// <summary>
        /// Para Obtener fecha de la licencia y acepte nulo
        /// </summary>
        public decimal? INVG_ID_FACT { get; set; }
        /// <summary>
        /// Para Obtener id de las fechas de facturacion
        /// </summary>
        public decimal LIC_PL_ID { get; set; }
        /// <summary>
        /// Para Obtener keys invoices
        /// </summary>
        public string KEY_INVOICE { get; set; }
        public decimal Nro { get; set; }
        public decimal SUBTOTAL { get; set; }

        public decimal INV_ID { get; set; }
        public decimal INVL_GROSS { get; set; }
        public decimal INVL_DISC { get; set; }
        public decimal INVL_BASE { get; set; }
        public decimal INVL_TAXES { get; set; }
        public decimal INVL_NET { get; set; }
        public decimal ADD_ID_EST { get; set; }
        ///TestTarifa
        public bool STATE_CALC { get; set; }

        public bool STATE_CALC_FACT_L { get; set; }
        public string OBSERVACION { get; set; }
        /// <summary>
        /// Licencia
        /// </summary>
        public decimal SUB_MONTO_TARIFA_LIC { get; set; }
        public decimal DESCUENTO { get; set; }
        public decimal IMPUESTO { get; set; }
        public decimal MONTO_TARIFA_LIC { get; set; }
        public decimal TOTAL { get; set; }
        public decimal INVL_BALANCE { get; set; }

        ///TIPO DE PAGO
        public string TIPOPAGO { get; set; }

        public string MONEDA { get; set; }
        public string INVG_DESC { get; set; }
        public string WRKF_SNAME { get; set; }

        public string LIC_AUT_START { get; set; }

        public string LIC_AUT_END { get; set; }

        public string LIC_TDESC { get; set; }

        public string MOD_DEC { get; set; }
        public string BPS_NAME { get; set; }

        public string EST_NAME { get; set; }

        public string WRKF_SLABEL { get; set; }

        //FACTURACION MASIVA
        public decimal MONTO_LIRICS_BRUTO { get; set; }
        public decimal MONTO_LIRICS_DCTO { get; set; }

        public decimal MONTO_LIRICS_NETO { get; set; }

        public string RAT_FDESC { get; set; }

        public string DIVISION { get; set; }

        public string OFICINA { get; set; }

        public string LIC_EMI_MENSUAL { get; set; }

        public string PERIODO_DESCRIPCION { get; set; }

        public string FECHA_DESCRIPCION { get; set; }

        public decimal DESCUENTO_REDONDEO { get; set; }
    }
}
