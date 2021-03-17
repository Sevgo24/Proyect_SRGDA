using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.DA.FacturacionElectronica;
using SGRDA.Entities;
using SGRDA.Entities.FacturaElectronica;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;

namespace SGRDA.BL.FacturaElectronica
{
    public class BLSunat
    {
        public int ActualizarObs(BESunat factura)
        {
            return new DASunat().ActualizarObs(factura);
        }

        public int ActualizarEstadoSunat(BESunat factura)
        {
            return new DASunat().ActualizarEstadoSunat(factura);
        }
    }
}
