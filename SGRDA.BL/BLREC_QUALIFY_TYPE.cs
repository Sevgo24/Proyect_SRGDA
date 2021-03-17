using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_QUALIFY_TYPE
    {
        public List<BEREC_QUALIFY_TYPE> Get_REC_QUALIFY_TYPE()
        {
            return new DAREC_QUALIFY_TYPE().Get_REC_QUALIFY_TYPE();
        }

        public List<BEREC_QUALIFY_TYPE> REC_QUALIFY_TYPE_GET_by_QUA_ID(decimal QUA_ID)
        {
            return new DAREC_QUALIFY_TYPE().REC_QUALIFY_TYPE_GET_by_QUA_ID(QUA_ID);
        }

        public List<BEREC_QUALIFY_TYPE> REC_QUALIFY_TYPE_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREC_QUALIFY_TYPE().REC_QUALIFY_TYPE_Page(param, pagina, cantRegxPag);
        }

        public bool REC_QUALIFY_TYPE_Ins(BEREC_QUALIFY_TYPE en)
        {
            return new DAREC_QUALIFY_TYPE().REC_QUALIFY_TYPE_Ins(en);
        }

        public bool REC_QUALIFY_TYPE_Upd(BEREC_QUALIFY_TYPE en)
        {
            return new DAREC_QUALIFY_TYPE().REC_QUALIFY_TYPE_Upd(en);
        }

        public bool REC_QUALIFY_TYPE_Del(decimal QUA_ID)
        {
            return new DAREC_QUALIFY_TYPE().REC_QUALIFY_TYPE_Del(QUA_ID);
        }
    }
}
