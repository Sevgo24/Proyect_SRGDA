using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGRDA.DA.FacturacionElectronica;
using SGRDA.Entities.FacturaElectronica;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;

namespace SGRDA.BL.FacturaElectronica
{
    public class BLTipoNotaCredito
    {
        public List<BETipoNotaCredito> ListarTipoNotaCredito(string owner)
        {
            return new DATipoNotaCredito().ListarTipoNotaCredito(owner);
        }
    }
}
