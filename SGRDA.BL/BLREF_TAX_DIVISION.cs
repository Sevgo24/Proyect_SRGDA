using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREF_TAX_DIVISION
    {
        public List<BEREF_TAX_DIVISION> Get_REF_TAX_DIVISION(string owner,decimal territorio)
        {
            return new DAREC_TAX_DIVISION().Get_REF_TAX_DIVISION(owner, territorio);
        }

        public List<BEREF_TAX_DIVISION> REF_TAX_DIVISION_GET_by_TAXD_ID(decimal TAXD_ID)
        {
            return new DAREC_TAX_DIVISION().REF_TAX_DIVISION_GET_by_TAXD_ID(TAXD_ID);
        }

        public List<BEREF_TAX_DIVISION> REF_TAX_DIVISION_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_TAX_DIVISION().REF_TAX_DIVISION_Page(param, st, pagina, cantRegxPag);
        }

        public bool REF_TAX_DIVISION_Ins(BEREF_TAX_DIVISION en)
        {
            return new DAREC_TAX_DIVISION().REF_TAX_DIVISION_Ins(en);
        }

        public bool REF_TAX_DIVISION_Upd(BEREF_TAX_DIVISION en)
        {
            return new DAREC_TAX_DIVISION().REF_TAX_DIVISION_Upd(en);
        }

        public bool REF_TAX_DIVISION_Del(decimal TAXD_ID)
        {
            return new DAREC_TAX_DIVISION().REF_TAX_DIVISION_Del(TAXD_ID);
        }
    }
}
