using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaPlantillaDescuentoCaracteristica
    {
        public string OWNER { get; set; }
        public decimal TEMP_ID_DSC_CHAR { get; set; }
        public decimal TEMP_ID_DSC { get; set; }
        public decimal CHAR_ID { get; set; }
        public decimal SECC_CHARSEQ { get; set; }
        public decimal IND_TR { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string CHAR_SHORT { get; set; }
   
    }
}

