using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BERecibo
    {
        public string OWNER { get; set; }
        public decimal REC_ID_TMP { get; set; }
        public decimal REC_ID { get; set; }
        public decimal NMR_ID { get; set; }
        public decimal REC_NUMBER { get; set; }
        public DateTime REC_DATE { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal REC_TBASE { get; set; }
        public decimal REC_TTAXES { get; set; }
        public decimal REC_TDEDUCTIONS { get; set; }
        public decimal REC_TTOTAL { get; set; }
        public string REC_OBSERVATION { get; set; }
        public DateTime REC_DISTRIBUTION { get; set; }
        public decimal REC_DISTRIBUTION_PRC { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string SERIE { get; set; }
        public List<BEDetalleMetodoPago> MetodoPago { get; set; }

        
        public string TIPO_PERSONA { get; set; }
        public string TIPO_DOC { get; set; }
        public string NUM_DOC { get; set; }
        public string SOCIO { get; set; }
        public string CUR_ALPHA { get; set; }
    }
}
