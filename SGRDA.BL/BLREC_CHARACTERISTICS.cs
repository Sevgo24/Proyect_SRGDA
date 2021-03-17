using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREC_CHARACTERISTICS
    {
        public List<BEREC_CHARACTERISTICS> Get_REC_CHARACTERISTICS()
        {
            return new DAREC_CHARACTERISTICS().Get_REC_CHARACTERISTICS();
        }

        public List<BEREC_CHARACTERISTICS> REC_CHARACTERISTICS_GET_by_CHAR_ID(decimal CHAR_ID)
        {
            return new DAREC_CHARACTERISTICS().REC_CHARACTERISTICS_GET_by_CHAR_ID(CHAR_ID);
        }

        public List<BEREC_CHARACTERISTICS> REC_RATE_FREQUENCY_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_CHARACTERISTICS().REC_RATE_FREQUENCY_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_CHARACTERISTICS_Ins(BEREC_CHARACTERISTICS en)
        {
            var lista = new DAREC_CHARACTERISTICS().REC_CHARACTERISTICS_GET_by_CHAR_ID(en.CHAR_ID);
            if (lista.Count == 0)
                return new DAREC_CHARACTERISTICS().REC_CHARACTERISTICS_Ins(en);
            else
                return false;
        }

        public bool REC_CHARACTERISTICS_Upd(BEREC_CHARACTERISTICS en)
        {
            return new DAREC_CHARACTERISTICS().REC_CHARACTERISTICS_Upd(en);
        }

        public bool REC_CHARACTERISTICS_Del(decimal CHAR_ID)
        {
            return new DAREC_CHARACTERISTICS().REC_CHARACTERISTICS_Del(CHAR_ID);
        }
    }
}
