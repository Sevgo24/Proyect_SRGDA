using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoParametro
    {
        public List<BEREC_TIPO_PARAMETRO> usp_ListarTipoParametro(string owner)
        {
            return new DATipoParametro().Get_TipoParametro(owner);
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public BEREC_TIPO_PARAMETRO Obtener(string owner, decimal id)
        {
            return new DATipoParametro().Obtener(owner, id);
        }

        public List<BETipoParametro> Listar_Page_TipoParametro(string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoParametro().Listar_Page_TipoParametro(param, st, pagina, cantRegxPag);
        }

        public List<BETipoParametro> Obtener_Parametro(string owner, decimal id)
        {
            return new DATipoParametro().Obtener_Parametro(owner, id);
        }

        public int Insertar(BETipoParametro ins)
        {
            return new DATipoParametro().Insertar(ins);
        }

        public int Actualizar(BETipoParametro upd)
        {
            return new DATipoParametro().Actualizar(upd);
        }

        public int Eliminar(BETipoParametro del)
        {
            return new DATipoParametro().Eliminar(del);
        }

        public List<BETipoParametro> ListarTipoParametro(string owner)
        {
            return new DATipoParametro().ListarTipoParametro(owner);
        }

        public bool existeTipoParametro(string Owner, string nombre)
        {
            return new DATipoParametro().existeTipoParametro(Owner, nombre);
        }

        public bool existeTipoParametro(string Owner, decimal id, string nombre)
        {
            return new DATipoParametro().existeTipoParametro(Owner, id, nombre);
        }

        public List<BEParametroSubTipo> ObtenerListaSubTipoParametro(string Owner, decimal idTIpoParametro)
        {
            return new DATipoParametro().ObtenerListaSubTipoParametro(Owner, idTIpoParametro);
        }

    }
}
