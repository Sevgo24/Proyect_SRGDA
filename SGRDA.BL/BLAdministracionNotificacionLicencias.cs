using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAdministracionNotificacionLicencias
    {
        public List<BEAdministracionNotificacionEventos> ListaNotificacionesEventos(decimal CodigoLicencia,decimal Oficina, string NombreLicencia, string NombreEstablecimiento, int ConFecha, string FechaInicial, string FechaFin,int EstadoLicencia)
        {
            return new DAAdministracionNotificacionLicencias().ListaNotificacionLicencias(CodigoLicencia,Oficina, NombreLicencia, NombreEstablecimiento,ConFecha,FechaInicial,FechaFin,EstadoLicencia);
        }

        public int ActualizaLicenciaNotificacion(decimal CodigoLicencia, decimal CodigoModalidad, decimal CodigoTarifa, decimal CodigoEstablecimiento, decimal CodigoSocio)
        {
            return new DAAdministracionNotificacionLicencias().ActualizaLicenciaNotificacion(CodigoLicencia, CodigoModalidad, CodigoTarifa, CodigoEstablecimiento, CodigoSocio);
        }
    }
}
