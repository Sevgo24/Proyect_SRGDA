using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
   public  class BEShow
    {
       public string OWNER { get; set; }  
       public decimal SHOW_ID { get; set; }
       public decimal LIC_AUT_ID { get; set; }  
       public string SHOW_NAME { get; set; }
       public DateTime SHOW_START { get; set; }
       public DateTime SHOW_ENDS { get; set; }  
       public decimal SHOW_ORDER { get; set; }  
       public string SHOW_OBSERV { get; set; }
       public DateTime LOG_DATE_CREAT { get; set; }
       public DateTime? LOG_DATE_UPDATE { get; set; }  
       public string LOG_USER_CREAT { get; set; }  
       public string LOG_USER_UPDATE { get; set; }  
       public DateTime? ENDS { get; set; }  
    }
}
