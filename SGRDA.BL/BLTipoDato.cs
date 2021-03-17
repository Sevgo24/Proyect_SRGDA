using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoDato
    {
        public List<BETipoDato> Listar_Page_TipoDato(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DATipoDato().Listar_Page_TipoDato(parametro, st, pagina, cantRegxPag);
        }

        public List<BETipoDato> Obtener(string owner, decimal id)
        {
            return new DATipoDato().Obtener(owner, id);
        }

        public int Insertar(BETipoDato ins)
        {
            return new DATipoDato().Insertar(ins);
        }

        public int Actualizar(BETipoDato upd)
        {
            return new DATipoDato().Actualizar(upd);
        }

        public int Eliminar(BETipoDato del)
        {
            return new DATipoDato().Eliminar(del);
        }
    }
}
