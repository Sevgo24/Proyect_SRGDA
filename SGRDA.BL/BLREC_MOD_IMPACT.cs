using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BlREC_MOD_IMPACT
    {
        public List<BEREC_MOD_IMPACT> Get_REC_MOD_IMPACT()
        {
            return new DAREC_MOD_IMPACT().Get_REC_MOD_IMPACT();
        }

        public List<BEREC_MOD_IMPACT> REC_MOD_IMPACT_GET_by_MOD_INCID(string MOD_INCID)
        {
            return new DAREC_MOD_IMPACT().REC_MOD_IMPACT_GET_by_MOD_INCID(MOD_INCID);
        }

        public List<BEREC_MOD_IMPACT> REC_MOD_IMPACT_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_MOD_IMPACT().REC_MOD_IMPACT_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_MOD_IMPACT_Ins(BEREC_MOD_IMPACT en)
        {
            return new DAREC_MOD_IMPACT().REC_MOD_IMPACT_Ins(en);
        }

        public bool REC_MOD_IMPACT_Upd(BEREC_MOD_IMPACT en)
        {
            return new DAREC_MOD_IMPACT().REC_MOD_IMPACT_Upd(en);
        }

        public int Eliminar(BEREC_MOD_IMPACT Incidencia)
        {
            return new DAREC_MOD_IMPACT().Eliminar(Incidencia);
        }
    }
}
