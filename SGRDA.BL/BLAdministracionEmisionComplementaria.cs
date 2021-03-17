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
    public class BLAdministracionEmisionComplementaria
    {
        public List<BEAdministracionEmisionComplementaria> Listar(decimal COdigoEmision, decimal CodigoLicencia, decimal CodigoOficina, int Estado, int ConFecha, string FechaInicial, string FechaFinal)
        {
            return new DAAdministracionEmisionComplementaria().Listar(COdigoEmision, CodigoLicencia, CodigoOficina, Estado, ConFecha, FechaInicial, FechaFinal); ;
        }

        public List<BEAdministracionEmisionComplementariaDetalle> ListarDetalle(decimal COdigoEmision)
        {
            return new DAAdministracionEmisionComplementaria().ListarDetalle(COdigoEmision); ;
        }

        public int ActualizarEstadoDetEmisionComplementaria(decimal IdEsmisionComplementariaDet)
        {
            return new DAAdministracionEmisionComplementaria().ActualizaEstadoDetallEmisionComplementaria(IdEsmisionComplementariaDet);
        }
        public decimal InsertaActualizaCabEmiComplementaria(BEAdministracionEmisionComplementaria entidad)
        {
            entidad.OWNER = GlobalVars.Global.OWNER;
            return new DAAdministracionEmisionComplementaria().InsertaActualizaCabEmiComplementaria(entidad);
        }

        public List<BEAdministracionEmisionComplementariaDetalle> ListarConsultaLicenciaDetalle(decimal codLicencia, decimal CodigoSocio, int mes, int anio,decimal CodigoOficina,decimal codcab)
        {
            return new DAAdministracionEmisionComplementaria().ListarConsultaLicenciaDetalle(codLicencia, CodigoSocio, mes, anio, CodigoOficina, codcab);
        }

        public List<BEAdministracionEmisionComplementariaDetalle> ListarLicenciarRegistradaDetalle(decimal codcab)
        {
            return new DAAdministracionEmisionComplementaria().ListarLicenciarRegistradaDetalle(codcab);
        }

        public int InsertarLicenciaPlaneamientoDetalle(BEAdministracionEmisionComplementariaDetalle entidad)
        {
            return new DAAdministracionEmisionComplementaria().InsertarLicenciaPlaneamientoDetalle(entidad);
        }

        public int QuitarLicenciaPlaneamientoDetalle(BEAdministracionEmisionComplementariaDetalle entidad)
        {
            return new DAAdministracionEmisionComplementaria().QuitarLicenciaPlaneamientoDetalle(entidad);
        }
        public int ActualizaDefinitivaCabDetComplementario(decimal CodCab,string usuario )
        {
            return new DAAdministracionEmisionComplementaria().ActualizaDefinitivaCabDetComplementario(CodCab, usuario);
        }


        public int GenerarEmisionComplementaria(decimal cabCod, string usu)
        {
            var Respuesta = 0;
            using (TransactionScope transa = new TransactionScope())
            {

                Respuesta = new DAAdministracionEmisionComplementaria().GenerarEmisionComplementaria(cabCod, usu); ;
                transa.Complete();
            }
            return Respuesta;
        }


        public int RechazaSolicitudEmisionComplementaria(decimal cabCod, string usu)
        {
            var Respuesta = 0;
            using (TransactionScope transa = new TransactionScope())
            {

                Respuesta = new DAAdministracionEmisionComplementaria().RechazaSolicitudEmisionComplementaria(cabCod, usu); ;
                transa.Complete();
            }
            return Respuesta;
        }

        public List<BEFactura> ListaDocumentoGeneradoxEmiComplementaria(decimal codcab)
        {
            return new DAAdministracionEmisionComplementaria().ListaDocumentoGeneradoxEmiComplementaria(codcab);
        }


        public BEAdministracionEmisionComplementaria ObtenerEmisionComplementaria(decimal CodCab)
        {
            return new DAAdministracionEmisionComplementaria().ObtenerEmisionComplementaria(CodCab);
        }


    }
}
