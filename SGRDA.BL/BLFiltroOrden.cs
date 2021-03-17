using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using SGRDA.Entities.FacturaElectronica;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;

namespace SGRDA.BL
{
    public class BLFiltroOrden
    {

        public List<BeFiltroOrden> listar()
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAFiltroOrden().Listar(owner);
        }
    }
}
