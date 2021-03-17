using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_UNLICENSE_REASONS
    {
        public List<BEREC_UNLICENSE_REASONS> Get_REC_UNLICENSE_REASONS()
        {
            return new DAREC_UNLICENSE_REASONS().Get_REC_UNLICENSE_REASONS();
        }

        public List<BEREC_UNLICENSE_REASONS> REC_UNLICENSE_REASONS_GET_by_UNL_ID(string OWNER, string UNL_ID)
        {
            return new DAREC_UNLICENSE_REASONS().REC_UNLICENSE_REASONS_GET_by_UNL_ID(OWNER, UNL_ID);
        }

        public List<BEREC_UNLICENSE_REASONS> REC_UNLICENSE_REASONS_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_UNLICENSE_REASONS().REC_UNLICENSE_REASONS_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_UNLICENSE_REASONS_Ins(BEREC_UNLICENSE_REASONS en)
        {
            return new DAREC_UNLICENSE_REASONS().REC_UNLICENSE_REASONS_Ins(en);
        }

        public bool REC_UNLICENSE_REASONS_Upd(BEREC_UNLICENSE_REASONS en)
        {
            return new DAREC_UNLICENSE_REASONS().REC_UNLICENSE_REASONS_Upd(en);
        }

        public bool REC_UNLICENSE_REASONS_Del(string OWNER, string UNL_ID)
        {
            return new DAREC_UNLICENSE_REASONS().REC_UNLICENSE_REASONS_Del(OWNER, UNL_ID);
        }
    }
}
