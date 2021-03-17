using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLParametrosEst
    {
        public int Insertar(BEParametrosEst en)
        {
            return new DAParametrosEst().Insertar(en);
        }
    }
}
