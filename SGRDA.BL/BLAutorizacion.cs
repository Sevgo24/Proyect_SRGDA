using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAutorizacion
    {

        public List<BEAutorizacion> AutorizacionXLicencia(decimal idLic, string owner)
        {
            return new DAAutorizacion().AutorizacionXLicencia(idLic, owner);
        }
        public int Insertar(BEAutorizacion entidad)
        {
            return new DAAutorizacion().Insertar(entidad );
        }
        public BEAutorizacion ObtenerAutorizacionXLic(string owner, decimal idLic, decimal idAut)
        {
            return new DAAutorizacion().ObtenerAutorizacionXLic(owner, idLic, idAut);
        }
        public int Actualizar(BEAutorizacion aut)
        {
            return new DAAutorizacion().Actualizar(aut);
        }
        public int Eliminar(decimal id, string owner, string usuDel)
        {
            return new DAAutorizacion().Eliminar(id,owner,usuDel);
        }
        public int Activar(decimal id, string owner, string usuDel)
        {
            return new DAAutorizacion().Activar(id, owner, usuDel);
        }
    }
}
