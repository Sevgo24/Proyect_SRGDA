using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;


namespace SGRDA.BL
{
    public class BLTipoDerecho
    {
        public List<BETipoDerecho> Listar_Page_Clase_Creacion(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoDerecho().Listar_Page_Tipo_Derecho(owner, param, st, pagina, cantRegxPag);
        }

        public int ValidacionTipoDerecho(BETipoDerecho en)
        {
            return new DATipoDerecho().ValidacionTipoDerecho(en);
        }

        public int Insertar(BETipoDerecho ins)
        {
            return new DATipoDerecho().Insertar(ins);
        }

        public int Actualizar(BETipoDerecho upd)
        {
            return new DATipoDerecho().Actualizar(upd);
        }

        public int Eliminar(BETipoDerecho del)
        {
            return new DATipoDerecho().Eliminar(del);
        }

        public List<BETipoDerecho> Obtener(string owner, string parametro)
        {
            return new DATipoDerecho().Obtener(owner, parametro);
        }

        public List<BETipoDerecho> ListarCombo(string owner)
        {
            return new DATipoDerecho().ListarCombo(owner);
        }
    }
}
