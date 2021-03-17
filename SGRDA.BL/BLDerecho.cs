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
    public class BLDerecho
    {
        public List<BEDerecho> ListarTipo(string owner, string class_cod)
        {
            return new DADerecho().ListarTipo(owner,class_cod);
        }
    }
}
