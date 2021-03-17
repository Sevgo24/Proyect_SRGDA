using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoEstado
    {
        public List<BETipoEstado> Listar_Page_TipoEstado(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DATipoEstado().Listar_Page_TipoEstado(parametro, st, pagina, cantRegxPag);
        }

        public List<BETipoEstado> Obtener(string owner, decimal id)
        {
            return new DATipoEstado().Obtener(owner, id);
        }

        public int Insertar(BETipoEstado ins)
        {
            return new DATipoEstado().Insertar(ins);
        }

        public int Actualizar(BETipoEstado upd)
        {
            return new DATipoEstado().Actualizar(upd);
        }

        public int Eliminar(BETipoEstado del)
        {
            return new DATipoEstado().Eliminar(del);
        }

    }
}
