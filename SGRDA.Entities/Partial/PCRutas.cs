using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BERutas
    {
        public List<BERutas> Rutas { get; set; }
        public BERutas() {}

        public BERutas(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ROU_ID = Convert.ToDecimal(Reader["ROU_ID"]);
            ROU_COD = Convert.ToString(Reader["ROU_COD"]);
            ROU_TSEL = Convert.ToString(Reader["ROU_TSEL"]);
        }

        public BERutas(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ROU_ID = Convert.ToDecimal(Reader["ROU_ID"]);
            ROU_COD = Convert.ToString(Reader["ROU_COD"]);
            ROU_TSEL = Convert.ToString(Reader["ROU_TSEL"]);

            TotalVirtual = flag;
        }
    }
}
