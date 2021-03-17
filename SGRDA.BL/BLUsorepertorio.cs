using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLUsorepertorio
    {
        public List<BEUsorepertorio> usp_Get_UsoRepertorioPage(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAUsorepertorio().usp_Get_UsoRepertorioPage(param, st, pagina, cantRegxPag);
        }

        public int Eliminar(BEUsorepertorio Usorepertorio)
        {
            return new DAUsorepertorio().Eliminar(Usorepertorio);
        }

        public int Insertar(BEUsorepertorio Usorepertorio)
        {
            return new DAUsorepertorio().Insertar(Usorepertorio);
        }

        public BEUsorepertorio Obtener(string owner, string id)
        {
            return new DAUsorepertorio().Obtener(owner, id);
        }

        public int Actualizar(BEUsorepertorio Usorepertorio)
        {
            return new DAUsorepertorio().Actualizar(Usorepertorio);
        }

        public int ObtenerXDescripcion(BEUsorepertorio Usorepertorio)
        {
            return new DAUsorepertorio().ObtenerXDescripcion(Usorepertorio);
        }

        public int ObtenerXCodigo(BEUsorepertorio Usorepertorio)
        {
            return new DAUsorepertorio().ObtenerXCodigo(Usorepertorio);
        }

        public List<BEUsorepertorio> ListarTipo(string owner)
        {
            return new DAUsorepertorio().ListarTipo(owner);
        }
    }
}
