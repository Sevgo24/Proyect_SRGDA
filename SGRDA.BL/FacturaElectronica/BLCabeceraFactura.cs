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
    public class BLCabeceraFactura
    {
        public List<BECabeceraFactura> ListarCabeceraFactura(string owner, decimal idfactura)
        {
            var ListarCabecera = new DACabeceraFactura().ListarCabeceraFactura(owner, idfactura);
            return ListarCabecera;
        }

        public List<BECabeceraFactura> ListarCabeceraFacturaEmision(decimal idfactura, decimal serie, decimal dir)
        {
            var ListarCabecera = new DACabeceraFactura().ListarCabeceraFacturaEmision(idfactura, serie, dir);
            return ListarCabecera;
        }

        public List<BECabeceraFactura> ListarCabeceraPreview(string owner, decimal idfactura)
        {
            var ListarCabecera = new DACabeceraFactura().ListarCabeceraPreview(owner, idfactura);
            return ListarCabecera;
        }

        public List<BECabeceraFactura> ListarCabeceraFacturaNc(string owner, decimal idfactura)
        {
            var ListarCabecera = new DACabeceraFactura().ListarCabeceraFacturaNc(owner, idfactura);
            return ListarCabecera;
        }

        public List<BECabeceraFactura> ObtenerCorrelativo(string owner, string serie)
        {
            var Correlativo = new DACabeceraFactura().ObtenerCorrelativo(owner, serie);
            return Correlativo;
        }
    }
}
