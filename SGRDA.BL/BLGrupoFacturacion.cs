using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLGrupoFacturacion
    {
        public List<BEGrupoFacturacion> Listar_Page_Grupo_Facturacion(decimal UserDer, string GrupoMod, string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DAGrupoFacturacion().Listar_Page_Grupo_Facturacion(UserDer, GrupoMod, parametro, st, pagina, cantRegxPag);
        }

        public List<BEGrupoFacturacion> Listar(decimal idSocio, decimal idGrupoFac, string owner)
        {
            return new DAGrupoFacturacion().GET_REC_LIC_FACT_GROUP_X_USU_MOD(idSocio, idGrupoFac, owner);
        }
        public int InsertarXModId(string OWNER, string INVG_DESC, decimal BPS_ID, decimal MOD_ID, string USER)
        {
            return new DAGrupoFacturacion().InsertarXModId(OWNER, INVG_DESC, BPS_ID, MOD_ID, USER);
        }

        public List<BEGrupoFacturacion> Obtener(string owner, decimal id)
        {
            return new DAGrupoFacturacion().Obtener(owner, id);
        }

        public int Insertar(BEGrupoFacturacion ins)
        {
            return new DAGrupoFacturacion().Insertar(ins);
        }

        public int Actualizar(BEGrupoFacturacion upd)
        {
            return new DAGrupoFacturacion().Actualizar(upd);
        }

        public int Eliminar(BEGrupoFacturacion del)
        {
            return new DAGrupoFacturacion().Eliminar(del);
        }
    }
}
