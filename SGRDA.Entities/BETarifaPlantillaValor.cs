using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaPlantillaValor
    {
        public string OWNER { get; set; }
        public decimal TEMPS_ID { get; set; }
        public decimal TEMPL_ID { get; set; }
        public decimal SECC_FROM { get; set; }
        public decimal SECC_TO { get; set; }
        public string SECC_DESC { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public string IND_TR { get; set; }
        public string CARACTERISTICA { get; set; }
        public decimal CHAR_ID { get; set; }
    }
}
