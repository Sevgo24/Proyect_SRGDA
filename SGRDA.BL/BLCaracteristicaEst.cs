using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLCaracteristicaEst
    {
        public int Insertar(BECaracteristicaEst en)
        {
            return new DACaracteristicaEst().Insertar(en);
        }

        public List<BECaracteristicaEst> CaracteristicaXSubtipoEstablecimiento(decimal idTipoEstablecimiento, decimal idSubtipoEstablecimiento)
        {
            return new DACaracteristicaEst().CaracteristicaXSubtipoEstablecimiento(idTipoEstablecimiento, idSubtipoEstablecimiento);
        }
        public List<BECaracteristicaEst> CaractersiticaxEstablecimiento(string owner, decimal ESTID)
        {

            return new DACaracteristicaEst().CaracteristicaXEstablecimiento(ESTID, owner);
        }
    }
}
