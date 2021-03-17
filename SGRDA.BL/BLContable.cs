using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using SGRDA.Entities.Reporte;

namespace SGRDA.BL
{
    public class BLContable
    {
        public List<BEContable> ListaContableDesplegable()
        {
            return new DAContable().ListaContableDesplegable();
        }

    }
}
