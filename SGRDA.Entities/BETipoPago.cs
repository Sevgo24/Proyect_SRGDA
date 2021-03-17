using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETipoPago
    {
        public string OWNER { get; set; }
        public string PAY_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public int PAY_BANK { get; set; }
        public int PAY_BANK_RECEIPT { get; set; }
        public int PAY_AGE_RECEIPT { get; set; }
        public int PAT_TRANSFER { get; set; }
        public int PAY_DATE_FIX { get; set; }
        public int PAY_DATE_FIX_DAY { get; set; }
        public decimal VTO1 { get; set; }
        public decimal VTO2 { get; set; }
        public decimal VTO3 { get; set; }
        public decimal VTO4 { get; set; }
        public decimal VTO5 { get; set; }
        public decimal VTO6 { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime ENDS { get; set; }
    }
}
