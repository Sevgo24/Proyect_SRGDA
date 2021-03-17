using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLTipoUsorepertorio
    {
        public List<BETipoUsorepertorio> usp_Get_UsoRepertorioPage(string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoUsorepertorio().usp_Get_UsoRepertorioPage(param, st, pagina, cantRegxPag);
        }

        public int Eliminar(BETipoUsorepertorio TipoUsorepertorio)
        {
            return new DATipoUsorepertorio().Eliminar(TipoUsorepertorio);
        }

        public int Insertar(BETipoUsorepertorio TipoUsorepertorio)
        {
            return new DATipoUsorepertorio().Insertar(TipoUsorepertorio);
        }

        public BETipoUsorepertorio Obtener(string owner, string id)
        {
            return new DATipoUsorepertorio().Obtener(owner, id);
        }

        public int Actualizar(BETipoUsorepertorio TipoUsorepertorio)
        {
            return new DATipoUsorepertorio().Actualizar(TipoUsorepertorio);
        }

        public int ObtenerXDescripcion(BETipoUsorepertorio TipoUsorepertorio)
        {
            return new DATipoUsorepertorio().ObtenerXDescripcion(TipoUsorepertorio);
        }

        public int ObtenerXCodigo(BETipoUsorepertorio TipoUsorepertorio)
        {
            return new DATipoUsorepertorio().ObtenerXCodigo(TipoUsorepertorio);
        }

        public List<BETipoUsorepertorio> ListarTipo(string owner)
        {
            return new DATipoUsorepertorio().ListarTipo(owner);
        }
    }
}
