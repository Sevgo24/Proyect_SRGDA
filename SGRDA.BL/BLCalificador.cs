using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLCalificador
    {
        public List<BECalificador> Listar_Page_Calificador(decimal tipo, string param, int st, int pagina, int cantRegxPag)
        {
            return new DACalificador().Listar_Page_Calificador(tipo, param, st, pagina, cantRegxPag);
        }

        public int Insertar(BECalificador ins)
        {
            return new DACalificador().Insertar(ins);
        }

        public int Actualizar(BECalificador upd)
        {
            return new DACalificador().Actualizar(upd);
        }

        public int Eliminar(BECalificador del)
        {
            return new DACalificador().Eliminar(del);
        }

        public List<BECalificador> Obtener(string owner, decimal id)
        {
            return new DACalificador().Obtener(owner, id);
        }
    }
}
