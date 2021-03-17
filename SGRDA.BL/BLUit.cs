using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLUit
    {   
        /// <summary>
        /// Obtiene el igv actual
        /// </summary>
        /// <returns></returns>
        public BEUit ListaUit()
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAUit().ListaUit(owner);
        }
    }
}
