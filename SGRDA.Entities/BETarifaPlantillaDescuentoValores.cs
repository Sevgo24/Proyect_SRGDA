using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaPlantillaDescuentoValores
    {
        public string OWNER { get; set; }
        public decimal TEMP_ID_DSC_MAT { get; set; }
        public decimal TEMP_ID_DSC { get; set; }

        
        public decimal TEMP_ID_DSC_VAL_1 { get; set; }
        public string TEMP_SEC_DESC_1 { get; set; }
        //public decimal TEMP_CHAR_ID_1 { get; set; }

        public decimal TEMP_ID_DSC_VAL_2 { get; set; }
        public string TEMP_SEC_DESC_2 { get; set; }
        //public decimal TEMP_CHAR_ID_2 { get; set; }

        public decimal TEMP_ID_DSC_VAL_3 { get; set; }
        public string TEMP_SEC_DESC_3 { get; set; }
        //public decimal TEMP_CHAR_ID_3 { get; set; }

        public decimal SECC_CHARSEQ { get; set; }   
        public decimal VAL_FORMULA { get; set; }

        public DateTime ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
    }
}
