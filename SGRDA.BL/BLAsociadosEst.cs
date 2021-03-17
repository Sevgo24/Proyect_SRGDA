using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLAsociadosEst
    {
        public BEAsociadosEst ObtenerDivisionAdministrativa(string Owner, decimal Id)
        {
            return new DAAsociadosEst().ObtenerDivisionAdministrativa(Owner, Id);
        }
        public List<BEAsociado> ObtenerAsociadoBPS(decimal bps_id)    
        {
            string OWNER = GlobalVars.Global.OWNER;
            return new DAAsociado().AsociadoXSocio(bps_id, OWNER);
        }
    }
}
