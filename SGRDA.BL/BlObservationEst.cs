using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLObservationEst
    {
        public int Insertar(BEObservationEst en)
        {
            return new DAObservationEst().Insertar(en);
        }
    }
}
