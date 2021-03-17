using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoCorreo
    {
        public List<BECorreoType> Listar_Page_TipoCorreo(string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoCorreo().Listar_Page_TipoCorreo(param, st, pagina, cantRegxPag);
        }

        public List<BECorreoType> Obtener_Correo(string owner, decimal id)
        {
            return new DATipoCorreo().Obtener_Correo(owner, id);
        }

        public int Insertar(BECorreoType ins)
        {
            return new DATipoCorreo().Insertar(ins);
        }

        public int Actualizar(BECorreoType upd)
        {
            return new DATipoCorreo().Actualizar(upd);
        }

        public int Eliminar(BECorreoType del)
        {
            return new DATipoCorreo().Eliminar(del);
        }

        public BECorreoType Obtener(string Owner, decimal idTipo)
        {
            return new DATipoCorreo().Obtener(Owner, idTipo);
        }

        public List<BECorreoType> ListarCombo(string owner)
        {
            return new DATipoCorreo().Get_TipoCorreo(owner);
        }

        public List<BECorreoType> ListarTipoCorreos(string owner)
        {
            return new DATipoCorreo().ListarTipoCorreos(owner);
        }

        public bool existeTipoCorreo(string Owner, string nombre)
        {
            return new DATipoCorreo().existeTipoCorreo(Owner, nombre);
        }

        public bool existeTipoCorreo(string Owner, decimal id, string nombre)
        {
            return new DATipoCorreo().existeTipoCorreo(Owner, id, nombre);
        }
    }
}
