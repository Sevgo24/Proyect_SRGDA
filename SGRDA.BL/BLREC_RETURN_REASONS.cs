using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_RETURN_REASONS
    {
        public List<BEREC_RETURN_REASONS> Get_REC_RETURN_REASONS()
        {
            return new DAREC_RETURN_REASONS().Get_REC_RETURN_REASONS();
        }

        public List<BEREC_RETURN_REASONS> REC_RETURN_REASONS_GET_by_RET_ID(string OWNER, string RET_ID)
        {
            return new DAREC_RETURN_REASONS().REC_RETURN_REASONS_GET_by_RET_ID(OWNER, RET_ID);
        }

        public List<BEREC_RETURN_REASONS> REC_RETURN_REASONS_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_RETURN_REASONS().REC_RETURN_REASONS_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_RETURN_REASONS_Ins(BEREC_RETURN_REASONS en)
        {
            var lista = new DAREC_RETURN_REASONS().REC_RETURN_REASONS_GET_by_RET_ID(GlobalVars.Global.OWNER, en.RET_ID);
            if (lista.Count() == 0)
                return new DAREC_RETURN_REASONS().REC_RETURN_REASONS_Ins(en);
            else
                return false;
        }

        public bool REC_RETURN_REASONS_Upd(BEREC_RETURN_REASONS en)
        {
            return new DAREC_RETURN_REASONS().REC_RETURN_REASONS_Upd(en);
        }

        public bool REC_RETURN_REASONS_Del(string OWNER, string RET_ID)
        {
            return new DAREC_RETURN_REASONS().REC_RETURN_REASONS_Del(OWNER, RET_ID);
        }
    }
}
