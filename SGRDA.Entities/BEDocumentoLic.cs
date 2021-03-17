using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEDocumentoLic:Paginacion
    {
        public string OWNER { get; set; }
        public decimal DOC_ID { get; set; }
        public decimal LIC_ID { get; set; }
        public DateTime? ENDS { get; set; }
        public string DOC_ORG { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
