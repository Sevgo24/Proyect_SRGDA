using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.FacturaElectronica
{
    public class BETipoNotaCredito
    {
        public string owner { get; set; }
        public decimal Id { get; set; }
        public string Code_Description { get; set; }
        public string Description { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
