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
    public class BLReferencia
    {
        public List<BEReferencia> ListarRefFactura(string owner, decimal idfactura)
        {
            var listar = new DAReferencia().ListarRefFactura(owner, idfactura);
            return listar;
        }
        public string ConsultaCorrelativoNC(string owner, decimal idfactura)
        {
            var result = new DAReferencia().ConsultaCorrelativoNC(owner, idfactura);
            return result;
        }
    }
}
