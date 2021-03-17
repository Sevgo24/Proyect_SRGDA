using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_BANKS_GRAL
    {
        public List<BEREC_BANKS_GRAL> Get_REC_BANKS_GRAL()
        {
            return new DAREC_BANKS_GRAL().Get_REC_BANKS_GRAL();
        }

        public List<BEREC_BANKS_GRAL> REC_BANKS_GRAL_GET_by_BNK_ID(string OWNER, decimal BNK_ID)
        {
            return new DAREC_BANKS_GRAL().REC_BANKS_GRAL_GET_by_BNK_ID(OWNER, BNK_ID);
        }

        public List<BEREC_BANKS_GRAL> USP_GET_DAREC_BANKS_GRAL_PAGE(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_BANKS_GRAL().USP_GET_DAREC_BANKS_GRAL_PAGE(param, st, pagina, cantRegxPag);
        }

        public List<BEREC_BANKS_BRANCH> LISTAR_SUCURSAL_X_BANCO_PAGE(string owner, decimal id, int pagina, int cantRegxPag)
        {
            return new DAREC_BANKS_GRAL().LISTAR_SUCURSAL_X_BANCO_PAGE(owner, id, pagina, cantRegxPag);
        }

        public List<BEREC_BANKS_BPS> LISTAR_CONTACTOS_X_BANCO_PAGE(string owner, decimal id, int pagina, int cantRegxPag)
        {
            return new DAREC_BANKS_GRAL().LISTAR_CONTACTOS_X_BANCO_PAGE(owner, id, pagina, cantRegxPag);
        }

        public bool REC_BANKS_GRAL_Ins(BEREC_BANKS_GRAL en)
        {
            var lista = new DAREC_BANKS_GRAL().REC_BANKS_GRAL_GET_by_BNK_ID(GlobalVars.Global.OWNER, en.BNK_ID);
            if (lista.Count == 0)
                return new DAREC_BANKS_GRAL().REC_BANKS_GRAL_Ins(en);
            else
                return false;
        }

        public bool REC_BANKS_GRAL_Upd(BEREC_BANKS_GRAL en)
        {
            return new DAREC_BANKS_GRAL().REC_BANKS_GRAL_Upd(en);
        }

        public bool REC_BANKS_GRAL_Del(decimal BNK_ID)
        {
            return new DAREC_BANKS_GRAL().REC_BANKS_GRAL_Del(BNK_ID);
        }

        public string ObtenerLongitudCodigoSucursal(string owner, decimal id)
        {
            return new DAREC_BANKS_GRAL().ObtenerLongitudCodigoSucursal(owner, id);
        }
    }
}
