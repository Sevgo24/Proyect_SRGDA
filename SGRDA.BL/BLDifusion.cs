using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLDifusion
    {
        public List<BEDifusion> ListarMedioDifusion(string owner)
        {
            return new DADifusion().ListarMedioDifusion(owner);
        }
    }
}
