using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLEmisionMensual
    {
        public List<BEEmisionMensual> ListaEmisionMensual(decimal Oficina, int Mes, int Anio, int Estado, decimal CodigoLicencia, decimal CodigoSocio)
        {
            return new DAEmisionMensual().ListaEmisionMensual(Oficina, Mes, Anio, Estado, CodigoLicencia, CodigoSocio);
        }

        public List<BEEmisionMensual> ListarLicenciasEmisionMensual(decimal CodigoSocio, decimal CodigoGrupoFact, decimal CodigoOficina, int Mes, int Anio)
        {
            return new DAEmisionMensual().ListarLicenciasEmisionMensual(CodigoSocio, CodigoGrupoFact, CodigoOficina, Mes, Anio);
        }
        public List<BEEmisionMensual> ListarLicenciasPeriodos(decimal CodigoLicencia, int Mes, int Anio)
        {
            return new DAEmisionMensual().ListarLicenciasPeriodos(CodigoLicencia, Mes, Anio);
        }

        public List<BEEmisionMensual> ListarLicenciasPeriodosActualizar(decimal CodigoLicencia, int Mes, int Anio, decimal Oficina)
        {
            return new DAEmisionMensual().ListarLicenciasPeriodosActualizar(CodigoLicencia, Mes, Anio, Oficina);
        }

        public int ActualizarEstadoLicenciaEmision(decimal CodigoLicencia)
        {
            return new DAEmisionMensual().ActualizarEstadoLicenciaEmision(CodigoLicencia);
        }

        public int GenerarEmisionMensual(decimal Oficina, int mes, int anio)
        {
            var Respuesta = 0;
            using (TransactionScope transa = new TransactionScope())
            {

                Respuesta = new DAEmisionMensual().GenerarEmisionMensual(Oficina, mes, anio); ;
                transa.Complete();
            }
            return Respuesta;
        }
        public int RecuperaQueModuloUtilizar()
        {
            return new DAEmisionMensual().RecuperaQueModuloUtilizar(); ;
        }
    }
}
