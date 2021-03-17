using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;


namespace SGRDA.BL
{
    public class BLLicenciaAforo
    {
        public int Insertar(BELicenciaAforo aforo)
        {
            return new DALicenciaAforo().Insertar(aforo);
        }

        public List<BELicenciaAforo> Listar(string owner, decimal Id, string UsuarioActual)
        {
            return new DALicenciaAforo().Listar(owner, Id, UsuarioActual);
        }

        public int Actualizar(BELicenciaAforo aforo)
        {
            return new DALicenciaAforo().Actualizar(aforo);
        }

        public int Eliminar(BELicenciaAforo aforo)
        {
            return new  DALicenciaAforo().Eliminar(aforo);
        }
    }
}
