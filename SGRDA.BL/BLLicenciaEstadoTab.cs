using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLLicenciaEstadoTab
    {
        public List<BELicenciaEstadoTab> ListarTabEstadoLicencia(string owner, decimal Id)
        {
            return new DALicenciaEstadoTab().ListarTabEstadoLicencia(owner, Id);
        }

        public BELicenciaEstadoTab ObtenerEstadoLicenciaTab(string owner, decimal tabId, decimal licsId)
        {
            return new DALicenciaEstadoTab().ObtenerEstadoLicenciaTab(owner, tabId, licsId);
        }

        public int Insertar(BELicenciaEstadoTab en)
        {
            return new DALicenciaEstadoTab().Insertar(en);
        }

        public int Actualizar(BELicenciaEstadoTab en)
        {
            return new DALicenciaEstadoTab().Actualizar(en);
        }

        public int Activar(string owner, decimal secuencia, string user)
        {
            return new DALicenciaEstadoTab().Activar(owner, secuencia, user);
        }

        public int Eliminar(string owner, decimal secuencia, string user)
        {
            return new DALicenciaEstadoTab().Eliminar(owner, secuencia, user);
        }
    }
}
