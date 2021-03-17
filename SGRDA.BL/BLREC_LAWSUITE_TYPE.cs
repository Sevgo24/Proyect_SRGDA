using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_LAWSUITE_TYPE
    {
        public List<BEREC_LAWSUITE_TYPE> Get_REC_LAWSUITE_TYPE()
        {
            return new DAREC_LAWSUITE_TYPE().Get_REC_LAWSUITE_TYPE();
        }

        public List<BEREC_LAWSUITE_TYPE> REC_LAWSUITE_TYPE_GET_by_LAWS_ID(string LAWS_ID)
        {
            return new DAREC_LAWSUITE_TYPE().REC_LAWSUITE_TYPE_GET_by_LAWS_ID(LAWS_ID);
        }

        public List<BEREC_LAWSUITE_TYPE> REC_LAWSUITE_TYPE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_LAWSUITE_TYPE().REC_LAWSUITE_TYPE_Page(param, st ,pagina, cantRegxPag);
        }

        public bool REC_LAWSUITE_TYPE_Ins(BEREC_LAWSUITE_TYPE en)
        {
            var lista = new DAREC_LAWSUITE_TYPE().REC_LAWSUITE_TYPE_GET_by_LAWS_ID(en.LAWS_ID);
            if (lista.Count() == 0)
                return new DAREC_LAWSUITE_TYPE().REC_LAWSUITE_TYPE_Ins(en);
            else
                return false;
        }

        public bool REC_LAWSUITE_TYPE_Upd(BEREC_LAWSUITE_TYPE en)
        {
            return new DAREC_LAWSUITE_TYPE().REC_LAWSUITE_TYPE_Upd(en);
        }

        public bool REC_LAWSUITE_TYPE_Del(string LAWS_ID)
        {
            return new DAREC_LAWSUITE_TYPE().REC_LAWSUITE_TYPE_Del(LAWS_ID);
        }
    }
}
