using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLDireccionEst
    {
        public int Insertar(BEDireccionEst en)
        {
            return new DADireccionEst().Insertar(en);
        }
    }
}
