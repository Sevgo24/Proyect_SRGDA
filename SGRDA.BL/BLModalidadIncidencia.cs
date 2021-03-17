using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLModalidadIncidencia
    {
        public List<BEModalidadIncidencia> ListarTipo(string owner)
        {
            return new DAModalidadIncidencia().ListarTipo(owner);
        }
    }
}
