using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;


namespace SGRDA.BL
{
    public class BLRed_Social
    {
        public BERedes_Sociales Obtener(string Owner, decimal idTipo)
        {
            return new DARedSocial().Obtener(Owner, idTipo);
        }
    }
}
