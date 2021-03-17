using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAdministracionControlLicencia
    {
        public List<BELicencias> ListaControlLicencias(decimal LIC_ID, decimal OFF_ID, int CON_FECHA, string FECHA_INICIAL, string FECHA_FIN,int ESTADO)
        {
            return new DAAdministracionControlLicencias().ListaControlLicencias(LIC_ID, OFF_ID, CON_FECHA, FECHA_INICIAL, FECHA_FIN, ESTADO);
        }

        public bool ActualizaLicenciaAprobacionLocales(decimal LIC_ID)
        {
            return new DAAdministracionControlLicencias().ActualizaLicenciaAprobacionLocales(LIC_ID);
        }
        public bool ActualizaLicenciaEstadoAprobacion(decimal LIC_ID,int ESTADO)
        {
            return new DAAdministracionControlLicencias().ActualizaLicenciaEstadoAprobacion(LIC_ID, ESTADO);
        }


        
    }
}
