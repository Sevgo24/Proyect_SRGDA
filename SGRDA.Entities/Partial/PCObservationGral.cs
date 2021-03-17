using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEObservationGral
    {
        public IList<BEObservationGral> ListaObservationGral  {get;set ;}

        public BEObservationGral()
        {
            ListaObservationGral = new List<BEObservationGral>();
        }

        public BEObservationGral(IDataReader Reader, int flag)
        {
            OBS_ID = Convert.ToInt32(Reader["OBS_ID"]);
            DES_TYPE = Convert.ToString(Reader["DES_TYPE"]);
            OBS_VALUE = Convert.ToString(Reader["OBS_VALUE"]);
            TotalVirtual = flag;
        }
    }
}
