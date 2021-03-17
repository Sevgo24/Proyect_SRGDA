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
    public class BLOrigenModalidad
    {
        public List<BEOrigenModalidad> Listar(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAOrigenModalidad().Listar(param, st, pagina, cantRegxPag);
        }

        public int Eliminar(BEOrigenModalidad origen)
        {
            return new DAOrigenModalidad().Eliminar(origen);
        }

        public int Insertar(BEOrigenModalidad origen)
        {
            return new DAOrigenModalidad().Insertar(origen);
        }

        public int Actualizar(BEOrigenModalidad origen)
        {
            return new DAOrigenModalidad().Actualizar(origen);
        }

        public BEOrigenModalidad Obtener(string owner, string id)
        {
            return new DAOrigenModalidad().Obtener(owner, id);
        }

        public int ObtenerXDescripcion(BEOrigenModalidad sociedad)
        {
            return new DAOrigenModalidad().ObtenerXDescripcion(sociedad);
        }

        public int ObtenerXCodigo(BEOrigenModalidad sociedad)
        {
            return new DAOrigenModalidad().ObtenerXCodigo(sociedad);
        }

        public List<BEOrigenModalidad> ListarTipo(string owner)
        {
            return new DAOrigenModalidad().ListarTipo(owner);
        }
    }
}
