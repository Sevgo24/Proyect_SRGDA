using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLLocalidad
    {
        public List<BELocalidad> Listar(string owner)
        {
            return new DALocalidad().Listar(owner);
        }

        //public List<BELocalidad> ListarxLic(string owner, decimal IdLic)
        //{
        //    return new DALocalidad().ListarxLic(owner,IdLic);
        //}

        public BELocalidad ObtenerLocalidadXCod(string owner, decimal idLocalidad)
        {
            return new DALocalidad().ObtenerLocalidadXCod(owner, idLocalidad);
        }
    }
}
