using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLBloqueos
    {
        public List<BEBloqueos> Listar_Page_Bloqueos(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DABloqueos().Listar_Page_Bloqueos(parametro, st, pagina, cantRegxPag);
        }
        public List<BEBloqueos> Listar(string owner)
        {
            return new DABloqueos().Listar(owner);
        }
        public List<BEBloqueos> Obtener(string owner, decimal id)
        {
            return new DABloqueos().Obtener(owner, id);
        }

        public int Insertar(BEBloqueos ins)
        {
            return new DABloqueos().Insertar(ins);
        }

        public int Actualizar(BEBloqueos upd)
        {
            return new DABloqueos().Actualizar(upd);
        }

        public int Eliminar(BEBloqueos del)
        {
            return new DABloqueos().Eliminar(del);
        }

        public bool existeTipoBloqueo(string Owner, string nombre)
        {
            return new DABloqueos().existeTipoBloqueo(Owner, nombre);
        }
    }
}
