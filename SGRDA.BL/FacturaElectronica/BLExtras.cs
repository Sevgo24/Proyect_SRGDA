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
    public class BLExtras
    {
        public List<BEExtras> ListarExtras(string owner, decimal idfactura)
        {
            return new DAExtras().ListarExtras(owner, idfactura);
        }
    }
}
