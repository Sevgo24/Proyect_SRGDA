using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLSocioNegocioBanco
    {
        public int InsertarSocioNegocioBanco(BEREC_BANKS_BPS en)
        {
            return new DASocioNegocioBanco().InsertarSocioNegocioBanco(en);
        }

        public int ActualizarSocioNegocioBanco(BEREC_BANKS_BPS en)
        {
            return new DASocioNegocioBanco().ActualizarSocioNegocioBanco(en);
        }

        public List<BEREC_BANKS_BPS> SocioNegocioBancoXSucursales(string idSucursal, string owner, decimal idBank)
        {
            return new DASocioNegocioBanco().SocioNegocioBancoXSucursalesListar(idSucursal, owner, idBank);
        }

        public List<BEREC_BANKS_BPS> SocioNegocioBancoXSucursalesListar(string idSucursal, string owner, decimal idBank)
        {
            return new DASocioNegocioBanco().SocioNegocioBancoXSucursalesListar(idSucursal, owner, idBank);
        }
    }
}
