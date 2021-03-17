using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_QUALIFY_CHAR
    {
        public List<BEREC_QUALIFY_CHAR> Get_REC_QUALIFY_CHAR()
        {
            return new DAREC_QUALIFY_CHAR().Get_REC_QUALIFY_CHAR();
        }

        public List<BEREC_QUALIFY_CHAR> REC_QUALIFY_CHAR_GET_by_QUA_ID(string OWNER, decimal QUC_ID)
        {
            return new DAREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_GET_by_QUA_ID(OWNER, QUC_ID);
        }

        public List<BEREC_QUALIFY_CHAR> REC_QUALIFY_CHAR_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_Page(param, pagina, cantRegxPag);
        }

        public bool REC_QUALIFY_CHAR_Ins(BEREC_QUALIFY_CHAR en)
        {
            return new DAREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_Ins(en);
        }

        public bool REC_QUALIFY_CHAR_Upd(BEREC_QUALIFY_CHAR en)
        {
            return new DAREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_Upd(en);
        }

        public bool REC_QUALIFY_CHAR_Del(decimal QUC_ID)
        {
            return new DAREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_Del(QUC_ID);
        }
    }
}
