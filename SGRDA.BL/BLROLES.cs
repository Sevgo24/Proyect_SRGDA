using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
namespace SGRDA.BL
{
    public  class BLROLES
    {
        public List<ROLES> usp_listar_Roles()
        {
            return new DAROLES().usp_listar_Roles();
        }

        public List<ROLES> usp_listar_Roles_by_codigo(int cod)
        {
            return new DAROLES().usp_listar_Roles_by_codigo(cod);
        }

        public List<ROLES> usp_Get_RolesPage(string param,int pagina, int cantRegxPag)
        {
            return new DAROLES().usp_Get_RolesPage(param,pagina,cantRegxPag);
        }

        public int usp_Upd_Roles(ROLES rol)
        {
            return new DAROLES().usp_Upd_Roles(rol);
        }

        public int usp_Ins_Roles(ROLES rol)
        {
            return new DAROLES().usp_Ins_Roles(rol);
        }

        public int usp_Upd_estado_Roles(ROLES rol)
        {
            return new DAROLES().usp_Upd_estado_Roles(rol);
        }


    }
}
