using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoTelefono
    {
        public BETelefonoType Obtener(string Owner, decimal idTipo)
        {
            return new DATipoTelefono().Obtener(Owner, idTipo);
        }

        public List<BETelefonoType> ListarCombo(string owner)
        {
            return new DATipoTelefono().Get_TipoTelefono(owner);
        }

        public List<BETelefonoType> Listar_Page_TipoTelefono(string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoTelefono().Listar_Page_TipoTelefono(param, st, pagina, cantRegxPag);
        }

        public List<BETelefonoType> Obtener_Telefono(string owner, decimal id)
        {
            return new DATipoTelefono().Obtener_Telefono(owner, id);
        }

        public int Insertar(BETelefonoType ins)
        {
            return new DATipoTelefono().Insertar(ins);
        }

        public int Actualizar(BETelefonoType upd)
        {
            return new DATipoTelefono().Actualizar(upd);
        }

        public int Eliminar(BETelefonoType del)
        {
            return new DATipoTelefono().Eliminar(del);
        }

        public bool existeTipoTelefono(string Owner, string nombre)
        {
            return new DATipoTelefono().existeTipoTelefono(Owner, nombre);
        }

        public bool existeTipoTelefono(string Owner, decimal id, string nombre)
        {
            return new DATipoTelefono().existeTipoTelefono(Owner, id, nombre);
        }
    }
}
