using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLDefinicionGasto
    {
        public List<BEDefinicionGasto> Listar_Page_DefinicionGasto(string owner, string tipo, string grupo, string param, int st, int pagina, int cantRegxPag)
        {
            return new DADefinicionGasto().Listar_Page_DefinicionGasto(owner, tipo, grupo, param, st, pagina, cantRegxPag);
        }

        public int ValidacionInsertarObtener(string owner, string id, string descripcion)
        {
            return new DADefinicionGasto().ValidacionInsertarObtener(owner, id, descripcion);
        }

        public int Insertar(BEDefinicionGasto ins)
        {
            return new DADefinicionGasto().Insertar(ins);
        }

        public int Actualizar(BEDefinicionGasto upd)
        {
            return new DADefinicionGasto().Actualizar(upd);
        }

        public int Eliminar(BEDefinicionGasto del)
        {
            return new DADefinicionGasto().Eliminar(del);
        }

        public List<BEDefinicionGasto> Obtener(string owner, string parametro)
        {
            return new DADefinicionGasto().Obtener(owner, parametro);
        }

        public List<BEDefinicionGasto> ListarCombo(string owner, string tipo)
        {
            return new DADefinicionGasto().ListarDefinicionGasto(owner, tipo);
        }
    }
}
