using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_LIC_STAT:Paginacion
    {
        public string OWNER { get; set; }
        public decimal LICS_ID { get; set; }
        public string LICS_NAME { get; set; }
        public string LICS_INI { get; set; }
        public string LICS_END { get; set; }
        public decimal LIC_TYPE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
    }
}
