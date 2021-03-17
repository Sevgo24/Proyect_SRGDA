using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoProceso
    {
        public List<BETipoProceso> Listar_Page_TipoProceso(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DATipoProceso().Listar_Page_TipoProceso(parametro, st, pagina, cantRegxPag);
        }

        public List<BETipoProceso> Obtener(string owner, decimal id)
        {
            return new DATipoProceso().Obtener(owner, id);
        }

        public int Insertar(BETipoProceso ins)
        {
            return new DATipoProceso().Insertar(ins);
        }

        public int Actualizar(BETipoProceso upd)
        {
            return new DATipoProceso().Actualizar(upd);
        }

        public int Eliminar(BETipoProceso del)
        {
            return new DATipoProceso().Eliminar(del);
        }

        public List<BETipoProceso> Listar(string owner)
        {
            return new DATipoProceso().Listar(owner);
        }
    }
}
