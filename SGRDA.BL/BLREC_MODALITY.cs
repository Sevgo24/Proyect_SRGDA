using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_MODALITY
    {
        public List<BEREC_MODALITY> Get_Rec_Modality(string tmp)
        {
            return new DAREC_MODALITY().GET_REC_MODALITY(tmp);
        }
        public BEREC_MODALITY GET_REC_MODALITY_X_COD(decimal MOD_ID)
        {
            return new DAREC_MODALITY().GET_REC_MODALITY_X_COD(MOD_ID);
        }
    }
}
