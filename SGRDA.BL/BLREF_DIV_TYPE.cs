using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREF_DIV_TYPE
    {
        public List<BEREF_DIV_TYPE> usp_Get_REF_DIV_TYPE()
        {
            return new DAREF_DIV_TYPE().usp_Get_REF_DIV_TYPE();
        }

        public List<BEREF_DIV_TYPE> ListarPage(string param, decimal terr, int st, int pagina, int cantRegxPag)
        {
            return new DAREF_DIV_TYPE().ListarPage(param, terr, st, pagina, cantRegxPag);
        }

        public int Insertar(BEREF_DIV_TYPE en)
        {
            return new DAREF_DIV_TYPE().Insertar(en);
        }

        public int Actualizar(BEREF_DIV_TYPE en)
        {
            return new DAREF_DIV_TYPE().Actualizar(en);
        }

        public BEREF_DIV_TYPE Obtiene(string owner, string DAD_TYPE)
        {
            return new DAREF_DIV_TYPE().Obtiene(owner, DAD_TYPE);
        }

        public int Eliminar(BEREF_DIV_TYPE en)
        {
            return new DAREF_DIV_TYPE().Eliminar(en);
        }

        public List<BEREF_DIV_TYPE> ListarTipoDivisiones(string owner)
        {
            return new DAREF_DIV_TYPE().ListarTipoDivisiones(owner);
        }
    }
}
