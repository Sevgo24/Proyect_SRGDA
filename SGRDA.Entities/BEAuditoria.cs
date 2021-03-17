using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAuditoria
    {
        public decimal AUDIT_ID { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public DateTime? AUDIT_DATE { get; set; }
        public string AUDIT_OBSR { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        /// <summary>
        /// Nombre del auditor
        /// /// </summary>
        public string AUDITOR { get; set; }

        public List<BEAuditoria> Auditoria { get; set; }
    }
}
