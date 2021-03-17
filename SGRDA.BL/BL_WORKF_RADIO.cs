using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL
{
    public class BL_WORKF_RADIO
    {
        public List<WORKF_RADIO> ListaActualizarEstadoLicRadioDif(string owner)
        {
            return new DA_WORKF_RADIO().ListaActualizarEstadoLicRadioDif(owner);
        }
    }
}
