using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLROLES_PERMISOS
    {
        public List<ROLES_PERMISOS> usp_Get_RolesPermisosPage(int rol, int mod, int pagina, int cantRegxPag)
        {
            return new DAROLES_PERMISOS().usp_Get_RolesPermisosPage(rol, mod, pagina, cantRegxPag);
        }

        public List<ROLES_PERMISOS> usp_Get_RolesPermisos(int ROL_ICODIGO_ROL, int CABE_ICODIGO_MODULO)
        {
            return new DAROLES_PERMISOS().usp_Get_RolesPermisos(ROL_ICODIGO_ROL, CABE_ICODIGO_MODULO);
        }

        public int usp_Ins_Roles_Permisos(ROLES_PERMISOS permisos)
        {
            return new DAROLES_PERMISOS().usp_Ins_Roles_Permisos(permisos);
        }

        public int usp_Upd_Roles_Permisos(ROLES_PERMISOS upd)
        {
            return new DAROLES_PERMISOS().usp_Upd_RolesPermisos(upd);
        }

        public List<ROLES_PERMISOS> usp_listar_RolesPermisos_by_codigo(int id)
        {
            return new DAROLES_PERMISOS().usp_listar_RolesPermisos_by_codigo(id);
        }

        public List<ROLES_PERMISOS> usp_listarNivelModulo(int nivel)
        {
            return new DAROLES_PERMISOS().usp_listarNivelModulo(nivel);
        }
    }
}
