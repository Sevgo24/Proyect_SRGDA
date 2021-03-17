using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoReporte
    {
        
        public List<BETipoReporte> Obtener(string owner)
        {
            return new DATipoReporte().Obtener(owner);
        }

        //public int Insertar(BETipoDato ins)
        //{
        //    return new DATipoDato().Insertar(ins);
        //}

        //public int Actualizar(BETipoDato upd)
        //{
        //    return new DATipoDato().Actualizar(upd);
        //}

        //public int Eliminar(BETipoDato del)
        //{
        //    return new DATipoDato().Eliminar(del);
        //}
    }
}
