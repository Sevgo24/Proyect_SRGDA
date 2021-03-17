using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoNumerador
    {

        public List<BETipoNumerador> ListarTipoNum(string owner)
        {
            return new DATipoNumerador().ListarTipoNum(owner);
        }

        public List<BETipoNumerador> TipoDocumento(string owner)
        {
            return new DATipoNumerador().TipoDocumento(owner);
        }
    }
}
