using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_MOD_USAGE
    {
        public List<BEREC_MOD_USAGE> Get_REC_MOD_USAGE()
        {
            return new DAREC_MOD_USAGE().Get_REC_MOD_USAGE();
        }
    }
}
