using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREF_DIVISIONES_VALUES
    {
        public List<BEREF_DIVISIONES_VALUES> REFDIVISIONES_VALUES { get; set; }
        public BEREF_DIVISIONES_VALUES() { }

        public BEREF_DIVISIONES_VALUES(IDataReader Reader)
        {
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            DAD_VNAME = Convert.ToString(Reader["DAD_VNAME"]);
        }

        public BEREF_DIVISIONES_VALUES(IDataReader Reader, int flag)
        {
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            DAD_VNAME = Convert.ToString(Reader["DAD_VNAME"]);

            TotalVirtual = flag;
        }
    }
}
