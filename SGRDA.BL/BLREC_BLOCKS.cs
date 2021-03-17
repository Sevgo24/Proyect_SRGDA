using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_BLOCKS
    {
        public List<BEREC_BLOCKS> Get_REC_BLOCKS()
        {
            return new DAREC_BLOCKS().Get_REC_BLOCKS();
        }

        public List<BEREC_BLOCKS> REC_BLOCKS_GET_by_BLOCK_ID(decimal BLOCK_ID)
        {
            return new DAREC_BLOCKS().REC_BLOCKS_GET_by_BLOCK_ID(BLOCK_ID);
        }

        public List<BEREC_BLOCKS> REC_BLOCKS_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREC_BLOCKS().REC_BLOCKS_Page(param, pagina, cantRegxPag);
        }

        public bool REC_BLOCKS_Ins(BEREC_BLOCKS en)
        {
            return new DAREC_BLOCKS().REC_BLOCKS_Ins(en);
        }

        public bool REC_BLOCKS_Upd(BEREC_BLOCKS en)
        {
            return new DAREC_BLOCKS().REC_BLOCKS_Upd(en);
        }

        public bool REC_BLOCKS_Del(decimal BLOCK_ID)
        {
            return new DAREC_BLOCKS().REC_BLOCKS_Del(BLOCK_ID);
        }
    }
}
