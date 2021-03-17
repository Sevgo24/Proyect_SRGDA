using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLDocumentoCampania
    {
        public int InsertarDocumento(BECampaniaDoc en)
        {
            return new DADocumentoCampania().Insertar(en);
        }
    }
}
