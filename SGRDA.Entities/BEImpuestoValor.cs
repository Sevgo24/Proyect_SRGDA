using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEImpuestoValor
    {
        public string OWNER { get; set; }
        public decimal DIV_ID { get; set; }
        public decimal TAXV_ID { get; set; }
        public decimal TAXD_ID { get; set; }
        public decimal TAX_ID { get; set; }
        public decimal TAXV_VALUEP { get; set; }
        public decimal TAXV_VALUEM { get; set; }
        public Nullable<DateTime> START { get; set; }
        public string FechaVigencia { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        /// <summary>
        /// DESCRIPCIOND DE DIVISION
        /// </summary>
        public string DIVISION { get; set; }

        /// <summary>
        /// DESCRIPCIOND DEL IMPUESTO
        /// </summary>
        public string IMPUESTO { get; set; }
        
        
    }
}
