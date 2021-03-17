using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_LIC_TYPE
    {
        public List<BEREC_LIC_TYPE> GET_REC_LIC_TYPE()
        {
            return new DAREC_LIC_TYPE().GET_REC_LIC_TYPE();
        }
        public BEREC_LIC_TYPE GET_REC_LIC_TYPE_X_COD(decimal LIC_TYPE)
        {
            return new DAREC_LIC_TYPE().GET_REC_LIC_TYPE_X_COD(LIC_TYPE);
        }
    }
}
