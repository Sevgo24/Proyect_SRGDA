using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{

    public class BLObservationGral
    {

        public int Insertar(BEObservationGral obs)
        {
            return new DAObservationGral().InsertarObsGrl(obs);
        }

        public int EliminarObsGral(BEObservationGral obs)
        {
            return new DAObservationGral().EliminarObsGral(obs);
        }
        public List<BEObservationGral> ObternerObsXLic(decimal codLic, string owner, decimal tipoEntidad)
        {
            return new DAObservationGral().ObservacionXLicencia(codLic, owner, tipoEntidad);
        }
        public BEObservationGral ObtenerObsXCodLic(string owner, decimal idObs, decimal idLic, decimal idEntidad)
        {
            return new DAObservationGral().ObtenerObsLic(owner, idObs, idLic, idEntidad);
        }
        public int Update(BEObservationGral obs)
        {
            return new DAObservationGral().Update(obs);
        }

        public int Activar(string owner, decimal idObs, string user)
        {
            return new DAObservationGral().Activar(owner, idObs, user);
        }
        public int Eliminar(string owner, decimal idObs, string user)
        {
            return new DAObservationGral().Eliminar(owner, idObs, user);
        }
    }

}
