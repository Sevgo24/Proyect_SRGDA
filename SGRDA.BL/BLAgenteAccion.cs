using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using System.Transactions;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;
using SGRDA.DA.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BLAgenteAccion
    {
        public List<BEAgenteAccion> AgenteXAccion(string owner, decimal id, string prefijo)
        {
            return new DAAgenteAccion().AgenteXAccion(owner, id, prefijo);
        }
    }
}
