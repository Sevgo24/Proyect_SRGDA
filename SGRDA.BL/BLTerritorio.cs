using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTerritorio
    {
        public List<BETerritorio> Listar_Territorio()
        {
            return new DATerritorio().Get_TERRITORIO();
        }
    }
}
