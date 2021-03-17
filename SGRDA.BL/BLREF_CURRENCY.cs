using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREF_CURRENCY
    {
        public List<BEREF_CURRENCY> ListarMoneda()
        {
            return new DAREF_CURRENCY().usp_Get_REF_CURRENCY();
        }

        public List<BEREF_CURRENCY> REF_CURRENCY_by_CUR_ALPHA(string CUR_ALPHA)
        {
            return new DAREF_CURRENCY().REF_CURRENCY_by_CUR_ALPHA(CUR_ALPHA);
        }

        public List<BEREF_CURRENCY> REF_CURRENCY_Page(string param, int st, int? pagina, int? cantRegxPag)
        {
            return new DAREF_CURRENCY().REF_CURRENCY_Page(param, st, pagina, cantRegxPag);
        }

        public bool REF_CURRENCY_Ins(BEREF_CURRENCY en)
        {
            return new DAREF_CURRENCY().REF_CURRENCY_Ins(en);
        }

        public bool REF_CURRENCY_Upd(BEREF_CURRENCY en)
        {
            return new DAREF_CURRENCY().REF_CURRENCY_Upd(en);
        }

        public bool REF_CURRENCY_Del(string CUR_ALPHA)
        {
            return new DAREF_CURRENCY().REF_CURRENCY_Del(CUR_ALPHA);
        }

        public List<BEREF_CURRENCY> ListarTipoMoneda(string owner)
        {
            return new DAREF_CURRENCY().ListarTipoMoneda(owner);
        }
        public BEREF_CURRENCY ObtenerMoneda(string owner, string id)
        {
            return new DAREF_CURRENCY().ObtenerMoneda(owner, id);
        }
    }
}
