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
    public class BLDetalleFactura
    {
        public List<BEDetalleFactura> ListarDetalleFactura(string owner, decimal idfactura)
        {
            var listar = new DADetalleFactura().ListarDetalleFactura(owner, idfactura);
            return listar;
        }

        public List<BEDetalleFactura> ListarDetalleFacturaMasiva(string owner, decimal idfactura)
        {
            var listar = new DADetalleFactura().ListarDetalleFacturaMasiva(owner, idfactura);
            return listar;
        }

        public List<BEDetalleFactura> ListarDetalleFacturaNC(string owner, decimal idfactura)
        {
            var listar = new DADetalleFactura().ListarDetalleFacturaNC(owner, idfactura);
            return listar;
        }
    }
}
