using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

using System.Transactions;

namespace SGRDA.BL
{
    public class BLDocumentoLic
    {
        public int Insertar (BEDocumentoLic obs){
            return new DADocumentoLic().Insertar(obs);
        }
    }
}
