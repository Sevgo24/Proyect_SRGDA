using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_SECTIONS
    {
        public List<BEREC_SECTIONS> Get_REC_SECTIONS()
        {
            return new DAREC_SECTIONS().Get_REC_SECTIONS();
        }

        public List<BEREC_SECTIONS> REC_SECTIONS_GET_by_SEC_ID(string SEC_ID)
        {
            return new DAREC_SECTIONS().REC_SECTIONS_GET_by_SEC_ID(SEC_ID);
        }

        public List<BEREC_SECTIONS> REC_SECTIONS_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_SECTIONS().REC_SECTIONS_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_SECTIONS_Ins(BEREC_SECTIONS en)
        {
            var lista = new DAREC_SECTIONS().REC_SECTIONS_GET_by_SEC_ID(en.SEC_ID);
            if (lista.Count() == 0)
                return new DAREC_SECTIONS().REC_SECTIONS_Ins(en);
            else
                return false;
        }

        public bool REC_SECTIONS_Upd(BEREC_SECTIONS en)
        {
            return new DAREC_SECTIONS().REC_SECTIONS_Upd(en);
        }

        public bool REC_SECTIONS_Del(string OWNER, string SEC_ID)
        {
            return new DAREC_SECTIONS().REC_SECTIONS_Del(OWNER, SEC_ID);
        }
    }
}
