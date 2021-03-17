using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLRoles_Usuarios
    {
        public List<Roles_Usuarios> usp_Get_RolesUsuariosPage(string param, int pagina, int cantRegxPag)
        {
            return new DARoles_Usuarios().usp_Get_RolesUsuariosPage(param, pagina, cantRegxPag);
        }
    }
}
