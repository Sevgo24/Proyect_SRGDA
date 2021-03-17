using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREC_USES_TYPE
    {
        public List<BEREC_USES_TYPE> Get_REC_USES_TYPE()
        {
            return new DAREC_USES_TYPE().Get_REC_USES_TYPE();
        }

        public List<BEREC_USES_TYPE> REC_USES_TYPE_GET_by_USET_ID(string USET_ID)
        {
            return new DAREC_USES_TYPE().REC_USES_TYPE_GET_by_USET_ID(USET_ID);
        }

        public List<BEREC_USES_TYPE> REC_USES_TYPE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_USES_TYPE().REC_USES_TYPE_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_USES_TYPE_Ins(BEREC_USES_TYPE en)
        {
            return new DAREC_USES_TYPE().REC_USES_TYPE_Ins(en);
        }

        public bool REC_USES_TYPE_Upd(BEREC_USES_TYPE en)
        {
            return new DAREC_USES_TYPE().REC_USES_TYPE_Upd(en);
        }

        public bool REC_USES_TYPE_Del(string USET_ID)
        {
            return new DAREC_USES_TYPE().REC_USES_TYPE_Del(USET_ID);
        }
    }
}
