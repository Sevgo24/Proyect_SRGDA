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
    public class BLSociedad
    {
        public List<BESociedad> Listar(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DASociedad().Listar(owner, param, st, pagina, cantRegxPag);
        }

        public int Eliminar(BESociedad origen)
        {
            return new DASociedad().Eliminar(origen);
        }

        public int Insertar(BESociedad origen)
        {
            return new DASociedad().Insertar(origen);
        }

        public int Actualizar(BESociedad origen)
        {
            return new DASociedad().Actualizar(origen);
        }

        public BESociedad Obtener(string owner, string id)
        {
            return new DASociedad().Obtener(owner, id);
        }

        public int ObtenerXDescripcion(BESociedad sociedad)
        {
            return new DASociedad().ObtenerXDescripcion(sociedad);
        }

        public int ObtenerXCodigo(BESociedad sociedad)
        {
            return new DASociedad().ObtenerXCodigo(sociedad);
        }

        public List<BESociedad> ListarTipo(string owner)
        {
            return new DASociedad().ListarTipo(owner);
        }
    }
}
