using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoGasto
    {
        public List<BETipoGasto> Listar_Page_TipoGasto(string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoGasto().Listar_Page_TipoGasto(param, st, pagina, cantRegxPag);
        }

        public int Insertar(BETipoGasto ins)
        {
            return new DATipoGasto().Insertar(ins);
        }

        public int Actualizar(BETipoGasto upd)
        {
            return new DATipoGasto().Actualizar(upd);
        }

        public int Eliminar(BETipoGasto del)
        {
            return new DATipoGasto().Eliminar(del);
        }

        public List<BETipoGasto> ListarCombo(string owner)
        {
            return new DATipoGasto().ListarTipoGasto(owner);
        }

        public List<BETipoGasto> Obtener(string owner, string parametro)
        {
            return new DATipoGasto().Obtener(owner, parametro);
        }
    }
}
