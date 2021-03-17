using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLParametroGral
    {
        public int Insertar(BEParametroGral para)
        {
            return new DAParametroGral().Insertar(para);
        }
        public List<BEParametroGral> ObtenerParXLic(decimal codLic, string owner, decimal tipoEntidad)
        {
            return new DAParametroGral().ParametroXLicencia(codLic, owner, tipoEntidad);
        
        }
        public BEParametroGral ObtenerParaXCodLic(string owner, decimal idPar, decimal idLic, decimal idEntidad)
        {
            return new DAParametroGral().ObtenerParLic(owner, idPar, idLic, idEntidad);
        }
        public int Update(BEParametroGral par)
        {
            return new DAParametroGral().Update(par);
        }
        public int Eliminar(string owner, decimal parId, string user)
        {
            return new DAParametroGral().Eliminar(owner, parId, user);
        }
        public int Activar(string owner, decimal parId, string user)
        {
            return new DAParametroGral().Activar(owner, parId, user);
        }
    }
}
