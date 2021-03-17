using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoObjeto
    {
        public List<BETipoObjeto> Listar_Page_TipoObjeto(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DATipoObjeto().Listar_Page_TipoObjeto(parametro, st, pagina, cantRegxPag);
        }

        public List<BETipoObjeto> Obtener(string owner, decimal? id)
        {
            return new DATipoObjeto().Obtener(owner, id);
        }

        public int Insertar(BETipoObjeto ins)
        {
            return new DATipoObjeto().Insertar(ins);
        }

        public int Actualizar(BETipoObjeto upd)
        {
            return new DATipoObjeto().Actualizar(upd);
        }

        public int Eliminar(BETipoObjeto del)
        {
            return new DATipoObjeto().Eliminar(del);
        }

        public bool existeTipoObjeto(string Owner, string nombre)
        {
            return new DATipoObjeto().existeTipoObjeto(Owner, nombre);
        }

        public bool existePrefijo(string Owner, string prefijo)
        {
            return new DATipoObjeto().existePrefijo(Owner, prefijo);
        }

        public bool existeTipoObjeto(string Owner, decimal id, string nombre)
        {
            return new DATipoObjeto().existeTipoObjeto(Owner, id, nombre);
        }

        public bool existePrefijo(string Owner, decimal id, string prefijo)
        {
            return new DATipoObjeto().existeTipoObjeto(Owner, id, prefijo);
        }
    }
}
