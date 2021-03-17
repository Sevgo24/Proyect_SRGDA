using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLEntidadLic
    {
        public int Insertar(BEEntidadLic en)
        {
            return new DAEntidadLic().Insertar(en);
        }

        public List<BEEntidadLic> Listar(string owner, decimal idLicenciamiento)
        {
            return new DAEntidadLic().Listar(owner, idLicenciamiento);
        }

        public BEEntidadLic ObtenerEntidad(string owner, decimal id, decimal idLicencia)
        {
            return new DAEntidadLic().ObtenerEntidad(owner, id, idLicencia);
        }

        public int Actualizar(BEEntidadLic en)
        {
            return new DAEntidadLic().Actualizar(en);
        }

        public int Eliminar(string owner, decimal id, string user)
        {
            return new DAEntidadLic().Eliminar(owner, id, user);
        }

        public int Activar(string owner, decimal id, string user)
        {
            return new DAEntidadLic().Activar(owner, id, user);
        }
    }
}
