using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEEntidadLic
    {
        public string OWNER { get; set; }
        public decimal LIC_BPS_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal LIC_BPS { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? ENDS { get; set; }

        /// <summary>
        /// Datos entidad del socio de negocio
        /// </summary>
        public string BPS_NAME { get; set; }
        public decimal TAXT_ID { get; set; }
        public string TAX_ID { get; set; }
    }
}
