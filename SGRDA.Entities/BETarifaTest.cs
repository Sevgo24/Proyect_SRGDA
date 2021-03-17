using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaTest
    {
        public string OWNER { get; set; }
        public decimal CALRV_ID { get; set; }
        public decimal CALR_ID { get; set; }        //REGLA
        public decimal TEMP_ID { get; set; }        //PLANTILLA
        public decimal RATE_ID { get; set; }        //7TARIFA

        public decimal? CHAR1_ID { get; set; }
        public decimal? CHAR2_ID { get; set; }
        public decimal? CHAR3_ID { get; set; }
        public decimal? CHAR4_ID { get; set; }
        public decimal? CHAR5_ID { get; set; }

        public decimal? CRI1_FROM { get; set; }
        public decimal? CRI2_FROM { get; set; }
        public decimal? CRI3_FROM { get; set; }
        public decimal? CRI4_FROM { get; set; }
        public decimal? CRI5_FROM { get; set; }

        public decimal? CRI1_TO { get; set; }
        public decimal? CRI2_TO { get; set; }
        public decimal? CRI3_TO { get; set; }
        public decimal? CRI4_TO { get; set; }
        public decimal? CRI5_TO { get; set; }

        public decimal? TEMPS1_ID { get; set; }
        public decimal? TEMPS2_ID { get; set; }
        public decimal? TEMPS3_ID { get; set; }
        public decimal? TEMPS4_ID { get; set; }
        public decimal? TEMPS5_ID { get; set; }

        public string SECC1_DESC { get; set; }
        public string SECC2_DESC { get; set; }
        public string SECC3_DESC { get; set; }
        public string SECC4_DESC { get; set; }
        public string SECC5_DESC { get; set; }

        public string IND1_TR { get; set; }
        public string IND2_TR { get; set; }
        public string IND3_TR { get; set; }
        public string IND4_TR { get; set; }
        public string IND5_TR { get; set; }

        public decimal? VAL_FORMULA { get; set; }
        public decimal? VAL_MINIMUM { get; set; }

    }
}
