using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREC_BPS_QUALY
    {
        public List<BEREC_BPS_QUALY> Get_REC_BPS_QUALY()
        {
            return new DAREC_BPS_QUALY().Get_REC_BPS_QUALY();
        }

        public List<BEREC_BPS_QUALY> REC_BPS_QUALY_GET_by_BPS_ID(string OWNER, decimal BPS_ID)
        {
            return new DAREC_BPS_QUALY().REC_BPS_QUALY_GET_by_BPS_ID(OWNER, BPS_ID);
        }

        public List<BEREC_BPS_QUALY> REC_BPS_QUALY_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREC_BPS_QUALY().REC_BPS_QUALY_Page(param, pagina, cantRegxPag);
        }

        public bool REC_BPS_QUALY_Ins(BEREC_BPS_QUALY en)
        {
            var lista = new DAREC_BPS_QUALY().REC_BPS_QUALY_GET_by_BPS_ID(GlobalVars.Global.OWNER, en.BPS_ID);
            if (lista.Count == 0)
                return new DAREC_BPS_QUALY().REC_BPS_QUALY_Ins(en);
            else
                return false;
        }

        public bool REC_BPS_QUALY_Upd(BEREC_BPS_QUALY en)
        {
            return new DAREC_BPS_QUALY().REC_BPS_QUALY_Upd(en);
        }

        public bool REC_BPS_QUALY_Del(string OWNER, decimal BPS_ID)
        {
            return new DAREC_BPS_QUALY().REC_BPS_QUALY_Del(OWNER, BPS_ID);
        }
    }
}
