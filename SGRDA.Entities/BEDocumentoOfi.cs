using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDocumentoOfi : Paginacion
    {
        public IList<BEDocumentoOfi> ListaDocumentoOfi { get; set; }

        public BEDocumentoOfi()
        {
            ListaDocumentoOfi = new List<BEDocumentoOfi>();
        }

        public string OWNER { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal DOC_ID { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public string DOC_DESC { get; set; }
        public string DOC_PATH { get; set; }
        public Nullable<DateTime> DOC_DATE { get; set; }

    }
}
