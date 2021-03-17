using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEContable
    {
        public decimal ACCOUNTANT_ID { get; set; }
        public string ACCOUNTANT_DESC { get; set; }
        public decimal ACCOUNTANT_YEAR { get; set; }
        public decimal ACCOUNTANT_MONTH { get; set; }
        public DateTime DATE_START { get; set; }
        public DateTime DATE_END { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime ENDS { get; set; }
        public DateTime DATE_START_COLLECT { get; set; }
        public DateTime DATE_END_COLLECT { get; set; }

    }
}
