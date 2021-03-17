using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLDocumentoEst
    {
        public int Insertar(BEDocumentoEst en)
        {
            return new DADocumentoEst().Insertar(en);
        }
    }
}
