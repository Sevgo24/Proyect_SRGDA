using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLPeriodoFrecuencia
    {
        public List<BEPeriodoFrecuencia> Listar(decimal periodoId, string owner)
        {
            return new DAPeriodoFrecuencia().Listar(periodoId, owner);
        }

        public int Insertar(BEPeriodoFrecuencia en)
        {
            return new DAPeriodoFrecuencia().Insertar(en);
        }

        public int Actualizar(BEPeriodoFrecuencia en)
        {
            return new DAPeriodoFrecuencia().Actualizar(en);
        }

        public int Activar(string owner, decimal periodoId, string user, decimal nroorden)
        {
            return new DAPeriodoFrecuencia().Activar(owner, periodoId, user, nroorden);
        }

        public int Eliminar(string owner, decimal periodoId, string user, decimal nroorden)
        {
            return new DAPeriodoFrecuencia().Eliminar(owner, periodoId, user, nroorden);
        }
    }
}
