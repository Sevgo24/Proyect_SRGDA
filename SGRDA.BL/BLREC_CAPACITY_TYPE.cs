using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREC_CAPACITY_TYPE
    {
        public List<BEREC_CAPACITY_TYPE> Get_REC_CAPACITY_TYPE()
        {
            return new DAREC_CAPACITY_TYPE().Get_REC_CAPACITY_TYPE();
        }

        public List<BEREC_CAPACITY_TYPE> REC_CAPACITY_TYPE_GET_by_CAP_ID(string CAP_ID)
        {
            return new DAREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_GET_by_CAP_ID(CAP_ID);
        }

        public List<BEREC_CAPACITY_TYPE> REC_CAPACITY_TYPE_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_Page(param, pagina, cantRegxPag);
        }

        public bool REC_CAPACITY_TYPE_Ins(BEREC_CAPACITY_TYPE en)
        {
            var lista = new DAREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_GET_by_CAP_ID(en.CAP_ID);
            if (lista.Count == 0)
                return new DAREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_Ins(en);
            else
                return false;
        }

        public bool REC_CAPACITY_TYPE_Upd(BEREC_CAPACITY_TYPE en)
        {
            return new DAREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_Upd(en);
        }

        public bool REC_CAPACITY_TYPE_Del(string CAP_ID)
        {
            return new DAREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_Del(CAP_ID);
        }
    }
}
