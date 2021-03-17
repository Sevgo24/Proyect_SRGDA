using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_ESTABLISHMENT_GRAL
    {
        public List<BEREC_ESTABLISHMENT_GRAL> GET_REC_ESTABLISHMENT_GRAL()
        {
            return new DAREC_ESTABLISHMENT_GRAL().GET_REC_ESTABLISHMENT_GRAL();
        }
        public BEREC_ESTABLISHMENT_GRAL GET_REC_ESTABLISHMENT_GRAL(decimal EST_ID)
        {
            return new DAREC_ESTABLISHMENT_GRAL().GET_REC_ESTABLISHMENT_GRAL_X_COD(EST_ID);
        }
    }
}
