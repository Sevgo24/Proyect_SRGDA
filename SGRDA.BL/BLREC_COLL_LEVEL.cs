using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_COLL_LEVEL
    {
        public List<BEREC_COLL_LEVEL> Get_REC_COLL_LEVEL()
        {
            return new DAREC_COLL_LEVEL().Get_REC_COLL_LEVEL();
        }
        public List<BEREC_COLL_LEVEL> LISTAR_REC_COLL_LEVEL()
        {
            return new DAREC_COLL_LEVEL().LISTAR_REC_COLL_LEVEL();
        }
        public BEREC_COLL_LEVEL LISTAR_REC_COLL_LEVEL_X_ID(decimal LEVEL_ID)
        {
            return new DAREC_COLL_LEVEL().LISTAR_REC_COLL_LEVEL_X_ID(LEVEL_ID);
        }

        public List<BEREC_COLL_LEVEL> REC_COLL_LEVEL_GET_by_LEVEL_ID(decimal LEVEL_ID)
        {
            return new DAREC_COLL_LEVEL().REC_COLL_LEVEL_GET_by_LEVEL_ID(LEVEL_ID);
        }

        public List<BEREC_COLL_LEVEL> REC_COLL_LEVEL_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREC_COLL_LEVEL().REC_COLL_LEVEL_Page(param, pagina, cantRegxPag);
        }

        public bool REC_COLL_LEVEL_Ins(BEREC_COLL_LEVEL en)
        {
            return new DAREC_COLL_LEVEL().REC_COLL_LEVEL_Ins(en);
        }

        public bool REC_COLL_LEVEL_Upd(BEREC_COLL_LEVEL en)
        {
            return new DAREC_COLL_LEVEL().REC_COLL_LEVEL_Upd(en);
        }

        public bool REC_COLL_LEVEL_Del(decimal LEVEL_ID)
        {
            return new DAREC_COLL_LEVEL().REC_COLL_LEVEL_Del(LEVEL_ID);
        }
    }
}
