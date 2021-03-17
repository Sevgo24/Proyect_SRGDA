using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREF_SOCIETY_TYPE
    {
        public List<BEREF_SOCIETY_TYPE> Get_REF_SOCIETY_TYPE()
        {
            return new DAREF_SOCIETY_TYPE().Get_REF_SOCIETY_TYPE();
        }

        public List<BEREF_SOCIETY_TYPE> REF_SOCIETY_TYPE_GET_by_SOC_TYPE(string SOC_TYPE)
        {
            return new DAREF_SOCIETY_TYPE().REF_SOCIETY_TYPE_GET_by_SOC_TYPE(SOC_TYPE);
        }

        public List<BEREF_SOCIETY_TYPE> REF_SOCIETY_TYPE_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREF_SOCIETY_TYPE().REF_SOCIETY_TYPE_Page(param, pagina, cantRegxPag);
        }

        public bool RREF_SOCIETY_TYPE_Ins(BEREF_SOCIETY_TYPE en)
        {
            return new DAREF_SOCIETY_TYPE().RREF_SOCIETY_TYPE_Ins(en);
        }

        public bool REF_SOCIETY_TYPE_Upd(BEREF_SOCIETY_TYPE en)
        {
            return new DAREF_SOCIETY_TYPE().REF_SOCIETY_TYPE_Upd(en);
        }

        public bool REF_SOCIETY_TYPE_Del(string SOC_TYPE)
        {
            return new DAREF_SOCIETY_TYPE().REF_SOCIETY_TYPE_Del(SOC_TYPE);
        }
    }
}
