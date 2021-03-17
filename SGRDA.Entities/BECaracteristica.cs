using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BECaracteristica
    {
        public string OWNER { get; set; }
        public decimal CHAR_ID { get; set; }
        public string CHAR_SHORT { get; set; }
        public string CHAR_LONG { get; set; }
        public DateTime ENDS { get; set; }
    }
}
