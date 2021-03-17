using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BECampaniaContactollamada
    {
        public string OWNER { get; set; }
        public decimal CONC_MID { get; set; }
        public decimal CONC_SID { get; set; }
        public decimal? BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public string OBS_DESC { get; set; }
        public string OBS_VALUE { get; set; }
        public decimal? CONC_MEXPEC { get; set; }
        public decimal? CONC_MREAL { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public decimal? CONC_CID { get; set; }
        public decimal? OBS_ID { get; set; }
        public decimal DOC_ID { get; set; }
        public string DOC_PATH { get; set; }
        public decimal OBS_TYPE { get; set; }
        public decimal ENT_ID { get; set; }
        public string LOG_DATE_CREATE { get; set; }

        public List<BEDocumentoGral> Documentos { get; set; }
        public BEObservationGral Observacion { get; set; }
        public List<BEObservationGral> Observaciones { get; set; }
        public List<BECampaniaContactollamada> LoteCliente { get; set; }
    }
}
