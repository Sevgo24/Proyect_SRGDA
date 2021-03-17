using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaPlaneamiento:Paginacion
    {
        public string OWNER { get; set; }
        public decimal LIC_PL_ID { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal LIC_MASTER { get; set;}
        public decimal LIC_YEAR { get; set; }
        public decimal LIC_MONTH { get; set; }
        public decimal LIC_ORDER { get; set; }
        public DateTime LIC_DATE { get; set; }
        public string LIC_INVOICE { get; set; }
        public decimal BLOCK_ID { get; set; }
        public string PAY_ID { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public string LIC_MONTH_DESC { get; set; }
        public string FRQ_DESC { get; set; }

        public decimal SUB_MONTO { get; set; }
        public decimal DESCUENTO { get; set; }
        public decimal MONTO { get; set; }
        public decimal Nro { get; set; }
        public string CUR_ALPHA { get; set; }
        public decimal EST_ID { get; set; }
        public decimal MOD_ID { get; set; }
        public decimal TAXV_VALUEP { get; set; }
        public decimal RATE_ID { get; set; }
        public string LIC_CREQ { get; set; }
        public string OBSERVACION { get; set; }
        public bool STATE_CALC { get; set; }

        public bool STATE_CALC_FACT { get; set; }


        /// <summary>
        /// NroFactura 
        /// </summary>
        public string NroFactura { get; set; }
        /// <summary>
        /// NroSerie de la factura
        /// </summary>
        public string NroSerie{ get; set; }
        /// <summary>
        /// Importe asociado a la factura
        /// </summary>
        public string ImporteFactura { get; set; }

        /// <summary>
        /// Monto Total  a Facturar
        /// </summary>
        public decimal? LIC_PL_AMOUNT { get; set; }
        /// <summary>
        /// Estado del periodo: A (ABIERTO) P (PARCIAL) T ( TOTAL)
        /// </summary>
        public string LIC_PL_STATUS { get; set; }

        public string LIC_PL_STATUS_FACT { get; set; }

    }
}
