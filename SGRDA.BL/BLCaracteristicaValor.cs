using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLCaracteristicaValor
    {
        public int Insertar(BECaracteristicaValor ent)
        {
            return new DACaracteristicaValor().Insertar(ent);
        }

        public List<BECaracteristicaValor> Listar(string owner, decimal id, int pagina, int cantRegxPag)
        {
            return new DACaracteristicaValor().Listar(owner, id, pagina, cantRegxPag);
        }

        public int Actualizar(BECaracteristicaValor ent)
        {
            return new DACaracteristicaValor().Actualizar(ent);
        }

        public int Eliminar(BECaracteristicaValor ent)
        {
            return new DACaracteristicaValor().Eliminar(ent);
        }
    }
}
