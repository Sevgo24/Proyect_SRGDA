using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREC_DEBTORS_RANGE
    {
        public List<BEREC_DEBTORS_RANGE> Get_REC_DEBTORS_RANGE()
        {
            return new DAREC_DEBTORS_RANGE().Get_REC_DEBTORS_RANGE();
        }

        public List<BEREC_DEBTORS_RANGE> REC_DEBTORS_RANGE_GET_by_RANGE_COD(string OWNER, decimal RANGE_COD)
        {
            return new DAREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_GET_by_RANGE_COD(OWNER, RANGE_COD);
        }

        public List<BEREC_DEBTORS_RANGE> REC_DEBTORS_RANGE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_DEBTORS_RANGE_Ins(BEREC_DEBTORS_RANGE en)
        {
            return new DAREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_Ins(en);
        }

        public bool REC_DEBTORS_RANGE_Upd(BEREC_DEBTORS_RANGE en)
        {
            return new DAREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_Upd(en);
        }

        public bool REC_DEBTORS_RANGE_Del(string OWNER, decimal RANGE_COD)
        {
            return new DAREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_Del(OWNER, RANGE_COD);
        }
    }
}
