using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELoteTrabajo
    {
        public string OWNER { get; set; }
        public decimal CONC_SID { get; set; }
        public DateTime CONC_SDATEINI { get; set; }
        public DateTime CONC_SDATEND { get; set; }
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? ENDS { get; set; }
        public string ESTADO { get; set; }
        public int TotalVirtual { get; set; }
        public decimal CONC_CID { get; set; }
        public string LOTEAGENTE { get; set; }

         public IList<BELoteTrabajo> ListaLote { get; set; }

         public BELoteTrabajo()
        {
            ListaLote = new List<BELoteTrabajo>();
        }
    }
}
