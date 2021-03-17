using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREF_CREATION_CLASS
    {
        public List<BEREF_CREATION_CLASS> usp_Get_REF_CREATION_CLASS(string CLASS_COD, string CLASS_DESC, string COD_PARENT_CLASS)
        {
            return new DAREF_CREATION_CLASS().usp_Get_REF_CREATION_CLASS(CLASS_COD, CLASS_DESC, COD_PARENT_CLASS);
        }

        public List<BEREF_CREATION_CLASS> ListarTipo(string owner)
        {
            return new DAREF_CREATION_CLASS().ListarTipo(owner);
        }
    }
}
