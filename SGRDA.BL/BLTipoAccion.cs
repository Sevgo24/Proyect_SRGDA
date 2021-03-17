using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoAccion
    {
        public List<BETipoAccion> Listar_Page_TipoAccion(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DATipoAccion().Listar_Page_TipoAccion(parametro, st, pagina, cantRegxPag);
        }

        public List<BETipoAccion> Obtener(string owner, decimal id)
        {
            return new DATipoAccion().Obtener(owner, id);
        }

        public int Insertar(BETipoAccion ins)
        {
            return new DATipoAccion().Insertar(ins);
        }

        public int Actualizar(BETipoAccion upd)
        {
            return new DATipoAccion().Actualizar(upd);
        }

        public int Eliminar(BETipoAccion del)
        {
            return new DATipoAccion().Eliminar(del);
        }
    }
}
