using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_LAWSUITE_ACTIVITIES_TYPE
    {
        public List<BEREC_LAWSUITE_ACTIVITIES_TYPE> Get_REC_LAWSUITE_ACTIVITIES_TYPE()
        {
            return new DAREC_LAWSUITE_ACTIVITIES_TYPE().Get_REC_LAWSUITE_ACTIVITIES_TYPE();
        }

        public List<BEREC_LAWSUITE_ACTIVITIES_TYPE> REC_LAWSUITE_ACTIVITIES_TYPE_GET_by_LAWS_ATY(string OWNER, string LAWS_ATY)
        {
            return new DAREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_GET_by_LAWS_ATY(OWNER, LAWS_ATY);
        }

        public List<BEREC_LAWSUITE_ACTIVITIES_TYPE> REC_LAWSUITE_ACTIVITIES_TYPE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_LAWSUITE_ACTIVITIES_TYPE_Ins(BEREC_LAWSUITE_ACTIVITIES_TYPE en)
        {
            var lista = new DAREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_GET_by_LAWS_ATY(GlobalVars.Global.OWNER, en.LAWS_ATY);
            if (lista.Count() == 0)
                return new DAREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_Ins(en);
            else
                return false;
        }

        public bool REC_LAWSUITE_ACTIVITIES_TYPE_Upd(BEREC_LAWSUITE_ACTIVITIES_TYPE en)
        {
            return new DAREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_Upd(en);
        }

        public bool REC_LAWSUITE_ACTIVITIES_TYPE_Del(string OWNER, string LAWS_ATY)
        {
            return new DAREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_Del(OWNER, LAWS_ATY);
        }
    }
}
