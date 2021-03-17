using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEHistorico
    {
        public string OWNER { get; set; }
        public int OFF_ID { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal LEVEL_ID { get; set; }
        public DateTime START { get; set; }
        public DateTime? ENDS { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
    }
}
