﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
namespace SGRDA.BL
{
    public class BLRolAgente
    {
        public List<BERoles> ListarCombo(string prefijo)
        {
            return new DARolAgente().ListarTipoAgente(prefijo);
        }
    }
}
