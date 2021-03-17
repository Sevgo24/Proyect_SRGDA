using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BECampaniaCallCenter
    {
        public string OWNER { get; set; }
        public decimal CONC_CID { get; set; }
        public string CONC_CNAME { get; set; }
        public decimal CONC_ID { get; set; }
        public string CONC_CDESC { get; set; }
        public decimal CONC_CTID { get; set; }
        public decimal ENT_ID { get; set; }
        public string CONC_CSTATUS { get; set; }
        public Nullable<DateTime> CONC_CDINI { get; set; }
        public Nullable<DateTime> CONC_CDEND { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string CONC_CSTATUS_DESC { get; set; }
        public int TotalVirtual { get; set; }
        public string CONC_CTNAME { get; set; }
        public string ENT_DESC { get; set; }
        public string CONC_NAME { get; set; }
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public IList<BECampaniaCallCenter> ListaCampaniaCall { get; set; }

        public BECampaniaCallCenter()
        {
            ListaCampaniaCall = new List<BECampaniaCallCenter>();
        }

        public string ESTADO { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }


        public List<BEDocumentoGral> Documentos { get; set; }
        public List<BEAgenteCampania> Asociados { get; set; }
        public List<BELoteTrabajo> LoteTrabajo { get; set; }
    }
}
