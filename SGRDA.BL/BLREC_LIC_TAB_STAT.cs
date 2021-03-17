using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL
{
    public class BLREC_LIC_TAB_STAT
    {
        //public BEREC_LIC_TAB_STAT ObtenerEstadoLicenciaTab(string owner, decimal tabId, decimal staId)
        //{
        //    return new DAREC_LIC_TAB_STAT().ObtenerDetalle(owner, tabId, staId);
        //}

        public int Insertar(BEREC_LIC_TAB_STAT en)
        {
            return new DAREC_LIC_TAB_STAT().InsertarDetalle(en);
        }

        public int Actualizar(BEREC_LIC_TAB_STAT en)
        {
            return new DAREC_LIC_TAB_STAT().ActualizarDetalle(en);
        }

        public int Activar(string owner, decimal secuencia, string user)
        {
            return new DAREC_LIC_TAB_STAT().Activar(owner, secuencia, user);
        }

        public int Eliminar(string owner, decimal secuencia, string user)
        {
            return new DAREC_LIC_TAB_STAT().Eliminar(owner, secuencia, user);
        }

        public List<BEREC_LIC_TAB_STAT> TabxEstado(string owner, decimal staId,decimal wfId) {
            return new DAREC_LIC_TAB_STAT().TabxEstado(owner, staId, wfId);
        }
    }
}
