using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
namespace SGRDA.BL
{
    public class BLGrupoGasto
    {
        public List<BEGrupoGasto> Listar_Page_GrupoGasto(string tipo, string param, int st, int pagina, int cantRegxPag)
        {
            return new DAGrupoGasto().Listar_Page_GrupoGasto(tipo, param, st, pagina, cantRegxPag);
        }

        public int Insertar(BEGrupoGasto ins)
        {
            return new DAGrupoGasto().Insertar(ins);
        }

        public int Actualizar(BEGrupoGasto upd)
        {
            return new DAGrupoGasto().Actualizar(upd);
        }

        public int Eliminar(BEGrupoGasto del)
        {
            return new DAGrupoGasto().Eliminar(del);
        }

        public List<BEGrupoGasto> Obtener(string owner, string parametro)
        {
            return new DAGrupoGasto().Obtener(owner, parametro);
        }

        public List<BEGrupoGasto> ListarCombo(string owner, string tipo)
        {
            return new DAGrupoGasto().ListarGrupoGasto(owner, tipo);
        }
    }
}
