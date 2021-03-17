using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLRangoMorosidad
    {
        public List<BERangoMorosidad> Listar_Page_Rango_Morosidad(string param, int st, int pagina, int cantRegxPag)
        {
            return new DARangoMorosidad().Listar_Page_Rango_Morosidad(param, st, pagina, cantRegxPag);
        }

        public int Insertar(BERangoMorosidad ins)
        {
            return new DARangoMorosidad().Insertar(ins);
        }

        public int Actualizar(BERangoMorosidad upd)
        {
            return new DARangoMorosidad().Actualizar(upd);
        }

        public int Eliminar(BERangoMorosidad del)
        {
            return new DARangoMorosidad().Eliminar(del);
        }

        public List<BERangoMorosidad> Obtener(decimal id)
        {
            return new DARangoMorosidad().Obtener(id);
        }

        public List<BERangoMorosidad> Listar(string owner)
        {
            return new DARangoMorosidad().Listar(owner);
        }
    }
}
