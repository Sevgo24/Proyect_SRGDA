using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;


namespace SGRDA.BL
{
    public class BLTipoDescuento
    {
        public List<BETipoDescuento> Listar_Page_TipoDescuento(string owner, string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DATipoDescuento().Listar_Page_TipoDescuento(owner, parametro, st, pagina, cantRegxPag);
        }

        public List<BETipoDescuento> Obtener(string owner, decimal id)
        {
            return new DATipoDescuento().Obtener(owner, id);
        }

        public int ValidacionTipoDescuento(BETipoDescuento en)
        {
            return new DATipoDescuento().ValidacionTipoDescuento(en);
        }

        public int Insertar(BETipoDescuento ins)
        {
            return new DATipoDescuento().Insertar(ins);
        }

        public int Actualizar(BETipoDescuento upd)
        {
            return new DATipoDescuento().Actualizar(upd);
        }

        public int Eliminar(BETipoDescuento del)
        {
            return new DATipoDescuento().Eliminar(del);
        }

        public List<BETipoDescuento> ListarCombo(string owner)
        {
            return new DATipoDescuento().ListarTipoDescuento(owner);
        }
    }
}
