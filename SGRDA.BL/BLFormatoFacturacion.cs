using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLFormatoFacturacion
    {
        public List<BEFormatoFacturacion> Listar(string owner)
        {
            return new DAFormatoFacturacion().GET_REC_INV_FORMAT(owner);
        }
    }
}
