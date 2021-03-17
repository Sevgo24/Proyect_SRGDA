using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BECorreo : Paginacion
    {
  
        public string OWNER { get; set; }

        public decimal MAIL_ID { get; set; }
        public decimal ENT_ID { get; set; }
        public decimal MAIL_TYPE { get; set; }
        
        public string MAIL_DESC { get; set; }
        public string MAIL_OBS { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
 
    }
}
