using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BERequerimientoDinero : Paginacion
    {
        public string OWNER { get; set; }
        public decimal MNR_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal TAXT_ID { get; set; }
        public string TAX_ID { get; set; }
        public string BPS_NAME { get; set; }
        public decimal STT_ID { get; set; }
        public string MNR_DESC { get; set; }
        public DateTime MNR_DATE { get; set; }
        public decimal MNR_VALUE_PRE { set; get; }
        public decimal MNR_VALUE_APR { set; get; }
        public decimal MNR_VALUE_CON { set; get; }
        public decimal EXP_APR { set; get; }
        public string MNR_APP { get; set; }
        public DateTime MNR_APP_DATE { get; set; }
        public decimal MNR_APP_USER { set; get; }
        public decimal MNR_APP_CODE { set; get; }
        public DateTime MNR_COUNT { get; set; }
        public string MNR_COUNT_N { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string ESTADO { get; set; }

        public string TIPO_DOC { get; set; }
        public string NUM_DOC { get; set; }
        /// <summary>
        /// LISTA DETALLE DE GASTO
        /// </summary>
        public List<BEDetalleGasto> DetalleGasto { get; set; }

        public string FECHA { get; set; }
        
    }
}
