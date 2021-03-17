using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaPlantillaVariable
    {
        public string OWNER { get; set; }
        public decimal TEMP_ID { get; set; }
        public DateTime STARTS { get; set; }
        public string TEMP_DESC { get; set; }
        public decimal TEMP_NVAR { get; set; }

        public decimal? TEMP_VID1 { get; set; }
        public string TEMP_VAR_TRA1 { get; set; }
        public decimal? TEMP_VID2 { get; set; }
        public string TEMP_VAR_TRA2 { get; set; }
        public decimal? TEMP_VID3 { get; set; }
        public string TEMP_VAR_TRA3 { get; set; }
        public decimal? TEMP_VID4 { get; set; }
        public string TEMP_VAR_TRA4 { get; set; }
        public decimal? TEMP_VID5 { get; set; }
        public string TEMP_VAR_TRA5 { get; set; }

        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public List<BETarifaVariable> Variables { get; set; }
        
    }
}
