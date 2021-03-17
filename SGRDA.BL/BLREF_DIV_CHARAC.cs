using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;


namespace SGRDA.BL
{
    public class BLREF_DIV_CHARAC
    {
        public List<BEREF_DIV_CHARAC> usp_Get_REF_DIV_CHARAC()
        {
            return new DAREF_DIV_CHARAC().usp_Get_REF_DIV_CHARAC();
        }

        public List<BEREF_DIV_CHARAC> usp_Get_REF_DIV_TYPE_by_DAC_ID(string DAC_ID)
        {
            return new DAREF_DIV_CHARAC().usp_Get_REF_DIV_TYPE_by_DAC_ID(DAC_ID);
        }

        public List<BEREF_DIV_CHARAC> usp_REF_DIV_CHARAC_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREF_DIV_CHARAC().usp_REF_DIV_CHARAC_Page(param, st, pagina, cantRegxPag);
        }

        public bool REF_DIV_CHARAC_Ins(BEREF_DIV_CHARAC en)
        {
            return new DAREF_DIV_CHARAC().REF_DIV_CHARAC_Ins(en);
        }

        public bool REF_DIV_CHARAC_Upd(BEREF_DIV_CHARAC en)
        {
            return new DAREF_DIV_CHARAC().REF_DIV_CHARAC_Upd(en);
        }

        public bool REF_DIV_CHARAC_Del(string DAC_ID)
        {
            return new DAREF_DIV_CHARAC().REF_DIV_CHARAC_Del(DAC_ID);
        }

        public List<BEREF_DIV_CHARAC> ListarTipoCaracteristicas(string owner)
        {
            return new DAREF_DIV_CHARAC().ListarTipoCaracteristicas(owner);
        }
    }
}
