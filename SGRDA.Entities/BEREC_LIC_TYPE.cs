using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_LIC_TYPE: Paginacion
    {
        public string OWNER { get; set; }
        public decimal LIC_TYPE { get; set; }
        public string LIC_TDESC { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}
