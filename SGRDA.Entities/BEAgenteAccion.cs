using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public partial class BEAgenteAccion
    {
        public string OWNER { get; set; }

        public decimal WRKF_AGAC_ID { get; set; }
        public decimal WRKF_AGID { get; set; }
        public decimal WRKF_AID { get; set; }
        public string Nombre { get; set; }
        public string Prefijo { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }
    }
}
