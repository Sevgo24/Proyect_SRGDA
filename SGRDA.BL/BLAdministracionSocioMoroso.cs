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
    public class BLAdministracionSocioMoroso
    {
        public List<BEAdministracionSocioMoroso> lista(decimal CodigoSocio ,int ConFecha,string FechaInicio,string FechaFin,int Tipo,int Estado)
        {
            return new DAAdministracionSocioMoroso().Listar(CodigoSocio,ConFecha,FechaInicio,FechaFin,Tipo,Estado);
        }

        public bool InsertarUsuarioMoroso(decimal CodigoSocio, string Descripcion, string Usuario , decimal CodigoLicencia,decimal CodigoOficina)
        {

            bool exitoInserUsuarioMoroso = false;
            using (TransactionScope transa = new TransactionScope())
            {

                exitoInserUsuarioMoroso = new DAAdministracionSocioMoroso().InsertarUsuarioMoroso(CodigoSocio, Descripcion, Usuario,  CodigoLicencia,CodigoOficina);

                transa.Complete();
            }
            return exitoInserUsuarioMoroso;
        
        }

        public bool InactivarUsuarioMoroso(decimal CodigoSocio, string Usuario)
        {

            bool exitoInactivarrUsuarioMoroso = false;
            using (TransactionScope transa = new TransactionScope())
            {

                exitoInactivarrUsuarioMoroso = new DAAdministracionSocioMoroso().InactivarUsuarioMoroso(CodigoSocio, Usuario);

                transa.Complete();
            }
            return exitoInactivarrUsuarioMoroso;

        }

        public BEAdministracionSocioMoroso Obtener(decimal CodigoSocioT)
        {
            return new DAAdministracionSocioMoroso().Obtener(CodigoSocioT);
        }

        public bool ActualizarEstadoSocioMoroso(decimal CodigoSocio,int Estado, string Usuario)
        {

            bool exitoActualizarrUsuarioMoroso = false;
            using (TransactionScope transa = new TransactionScope())
            {

                exitoActualizarrUsuarioMoroso = new DAAdministracionSocioMoroso().ActualizarEstadoSocioMoroso(CodigoSocio,Estado, Usuario);

                transa.Complete();
            }
            return exitoActualizarrUsuarioMoroso;

        }
    }
}
