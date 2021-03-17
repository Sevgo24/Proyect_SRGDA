using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREC_MOV_TYPE
    {
        public List<BEREC_MOV_TYPE> Get_REC_MOV_TYPE()
        {
            return new DAREC_MOV_TYPE().Get_REC_MOV_TYPE();
        }

        public List<BEREC_MOV_TYPE> REC_MOV_TYPE_by_MOV_TYPE(string OWNER, string MOV_TYPE)
        {
            return new DAREC_MOV_TYPE().REC_MOV_TYPE_by_MOV_TYPE(OWNER, MOV_TYPE);
        }

        public List<BEREC_MOV_TYPE> REC_MOV_TYPE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_MOV_TYPE().REC_MOV_TYPE_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_MOV_TYPE_Ins(BEREC_MOV_TYPE en)
        {
            var lista = new DAREC_MOV_TYPE().REC_MOV_TYPE_by_MOV_TYPE(GlobalVars.Global.OWNER, en.MOV_TYPE);
            if (lista.Count() == 0)
                return new DAREC_MOV_TYPE().REC_MOV_TYPE_Ins(en);
            else
                return false;
        }

        public bool REC_MOV_TYPE_Upd(BEREC_MOV_TYPE en)
        {
            return new DAREC_MOV_TYPE().REC_MOV_TYPE_Upd(en);
        }

        public bool REC_MOV_TYPE_Del(string OWNER, string MOV_TYPE)
        {
            return new DAREC_MOV_TYPE().REC_MOV_TYPE_Del(OWNER, MOV_TYPE);
        }
    }
}
