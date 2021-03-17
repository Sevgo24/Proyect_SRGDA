using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoCorrelativo
    {
        public List<BETipoCorrelativo> Listar_Page_TipoCorrelativo(string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoCorrelativo().Listar_Page_TipoCorrelativo(param, st, pagina, cantRegxPag);
        }

        public int Insertar(BETipoCorrelativo ins)
        {
            return new DATipoCorrelativo().Insertar(ins);
        }

        public int Actualizar(BETipoCorrelativo upd)
        {
            return new DATipoCorrelativo().Actualizar(upd);
        }

        public int Eliminar(BETipoCorrelativo del)
        {
            return new DATipoCorrelativo().Eliminar(del);
        }

        public List<BETipoCorrelativo> Obtener(string owner, string parametro)
        {
            return new DATipoCorrelativo().Obtener(owner, parametro);
        }
    }
}
