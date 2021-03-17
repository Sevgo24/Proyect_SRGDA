using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_BPS_BANKS_ACC
    {
        public List<BEREC_BPS_BANKS_ACC> Get_REC_BPS_BANKS_ACC()
        {
            return new DAREC_BPS_BANKS_ACC().Get_REC_BPS_BANKS_ACC();
        }

        public List<BEREC_BPS_BANKS_ACC> REC_BPS_BANKS_ACC_GET_by_BNK_ID(string OWNER, decimal BNK_ID, string BRCH_ID)
        {
            return new DAREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_GET_by_BNK_ID(OWNER, BNK_ID, BRCH_ID);
        }

        public List<BEREC_BPS_BANKS_ACC> REC_BPS_BANKS_ACC_Page(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_Page(owner, param, st, pagina, cantRegxPag);
        }

        public bool REC_BPS_BANKS_ACC_Ins(BEREC_BPS_BANKS_ACC en)
        {
            var lista = new DAREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_GET_by_BNK_ID(GlobalVars.Global.OWNER, en.BNK_ID, en.BRCH_ID);
            if (lista.Count == 0)
                return new DAREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_Ins(en);
            else
                return false;
        }

        public bool REC_BPS_BANKS_ACC_Upd(BEREC_BPS_BANKS_ACC en)
        {
            return new DAREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_Upd(en);
        }

        public bool REC_BPS_BANKS_ACC_Del(decimal BNK_ID)
        {
            return new DAREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_Del(BNK_ID);
        }        
    }
}
