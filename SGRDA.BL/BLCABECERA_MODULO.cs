using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLCABECERA_MODULO
    {
        public List<CABECERA_MODULO> usp_listar_Cabecera_Modulo()
        {
            return new DACABECERA_MODULO().usp_listar_Cabecera_Modulo();
        }
    }
}
