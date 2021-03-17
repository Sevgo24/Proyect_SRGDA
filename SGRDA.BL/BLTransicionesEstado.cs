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
    public class BLTransicionesEstado
    {
        public List<BETransicionesEstado> ListaTrancionEstadoPaginada(string owner, decimal estadoOri, decimal estadoDes, int st, int pagina, int cantRegxPag)
        {
            return new DATransicionesEstado().ListaTrancionEstadoPaginada(owner, estadoOri, estadoDes, st, pagina, cantRegxPag);
        }

        public int Eliminar(BETransicionesEstado en)
        {
            return new DATransicionesEstado().Eliminar(en);
        }

        public BETransicionesEstado Obtener(string owner, decimal idori, decimal iddes)
        {
            return new DATransicionesEstado().Obtener(owner, idori, iddes);
        }

        public List<BETransicionesEstado> ObtenerDatosValidad(string owner, decimal idori, decimal iddes)
        {
            return new DATransicionesEstado().ObtenerDatosValidad(owner, idori, iddes);
        }

        public int Insertar(BETransicionesEstado en)
        {
            return new DATransicionesEstado().Insertar(en);
        }

        public int actualizar(BETransicionesEstado en)
        {
            return new DATransicionesEstado().actualizar(en);
        }
    }
}
