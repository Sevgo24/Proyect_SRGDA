using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEValoresConfig
    {
        public decimal ID_VALUE { get; set; }
        public string VTYPE { get; set; }
        public string VSUB_TYPE { get; set; }
        public string VDESC { get; set; }
        public string VALUE { get; set; }
        public DateTime ENDS { get; set; }
    }
}
