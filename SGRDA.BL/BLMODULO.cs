using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLMODULO
    {
        public List<MODULO> MODULO_spBuscarMenu(int usua_icodigo_usuario, int CABE_ICODIGO_MODULO)
        {
            return new DAMODULO().MODULO_spBuscarMenu(usua_icodigo_usuario, CABE_ICODIGO_MODULO);
        }
    }
}