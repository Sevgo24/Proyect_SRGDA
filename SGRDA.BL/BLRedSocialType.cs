using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLRedSocialType
    {
        public List<BERedSocialType> Listar_Page_TipRedSocial(string param, int st, int pagina, int cantRegxPag)
        {
            return new DARedSocialType().Listar_Page_TipRedSocial(param, st, pagina, cantRegxPag);
        }

        public int Insertar(BERedSocialType ins)
        {
            return new DARedSocialType().Insertar(ins);
        }

        public int Actualizar(BERedSocialType upd)
        {
            return new DARedSocialType().Actualizar(upd);
        }

        public int Eliminar(BERedSocialType del)
        {
            return new DARedSocialType().Eliminar(del);
        }

        public List<BERedSocialType> Obtener(string owner, decimal id)
        {
            return new DARedSocialType().Obtener(owner, id);
        }

        public List<BERedSocialType> ListarCombo(string owner)
        {
            return new DARedSocialType().ListarTipoRedes(owner);
        }
    }
}
