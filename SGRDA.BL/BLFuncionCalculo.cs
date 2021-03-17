using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;


namespace SGRDA.BL
{
    public class BLFuncionCalculo
    {
        public List<BEFuncionCalculo> ListarDesplegable(string owner)
        {
            return new DAFuncionCalculo().ListarDesplegable(owner);
        }
    }
}
