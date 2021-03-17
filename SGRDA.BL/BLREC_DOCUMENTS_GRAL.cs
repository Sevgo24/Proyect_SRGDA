using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREC_DOCUMENTS_GRAL
    {
        public List<BEREC_DOCUMENTS_GRAL> Get_REC_DOCUMENTS_GRAL()
        {
            return new DAREC_DOCUMENTS_GRAL().Get_REC_DOCUMENTS_GRAL();
        }

        public List<BEREC_DOCUMENTS_GRAL> REC_DOCUMENTS_GRAL_GET_by_DOC_ID(string OWNER, decimal DOC_ID)
        {
            return new DAREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_GET_by_DOC_ID(OWNER, DOC_ID);
        }

        public List<BEREC_DOCUMENTS_GRAL> REC_DOCUMENTS_GRAL_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_Page(param, pagina, cantRegxPag);
        }

        public bool REC_DOCUMENTS_GRAL_Ins(BEREC_DOCUMENTS_GRAL en)
        {
            return new DAREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_Ins(en);
        }

        public bool REC_DOCUMENTS_GRAL_Upd(BEREC_DOCUMENTS_GRAL en)
        {
            return new DAREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_Upd(en);
        }

        public bool REC_DOCUMENTS_GRAL_Del(string OWNER, decimal DOC_ID)
        {
            return new DAREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_Del(OWNER, DOC_ID);
        }
    }
}
