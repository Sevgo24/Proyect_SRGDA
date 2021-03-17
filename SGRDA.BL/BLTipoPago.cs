using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoPago
    {
        public List<BETipoPago> ListarTipoPago(string owner)
        {
            return new DATipoPago().ListarTipoPago(owner);
        }
    }
}
