using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoCalificador
    {
        public List<BETipoCalificador> Listar_Page_Tipo_Calificador(string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoCalificador().Listar_Page_Tipo_Calificador(param, st, pagina, cantRegxPag);
        }

        public int Insertar(BETipoCalificador ins)
        {
            return new DATipoCalificador().Insertar(ins);
        }

        public int Actualizar(BETipoCalificador upd)
        {
            return new DATipoCalificador().Actualizar(upd);
        }

        public int Eliminar(BETipoCalificador del)
        {
            return new DATipoCalificador().Eliminar(del);
        }

        public List<BETipoCalificador> Obtener(decimal id)
        {
            return new DATipoCalificador().Obtener(id);
        }

        public List<BETipoCalificador> ListarCombo(string owner)
        {
            return new DATipoCalificador().ListarTipoCalificador(owner);
        }
    }
}
