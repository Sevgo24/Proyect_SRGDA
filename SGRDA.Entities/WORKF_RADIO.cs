using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public class WORKF_RADIO
    {
        public string OWNER { get; set; }
        public decimal ID_BSP { get; set; }
        public decimal ID_LIC { get; set; }
        public int CANT_FACT_DEUDA { get; set; }

        public decimal LICS_ID { get; set; }
        public decimal WRFK_ID { get; set; }
    }
}
