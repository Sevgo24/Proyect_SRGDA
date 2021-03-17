using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
namespace SGRDA.BL
{
    public class BLAdministracionCobros
    {
        public List<BEAdministracionCobros> Listar(decimal IdCobro, string NumeroOperacion, decimal Monto, decimal IdBancoDestino, decimal IdBancoOrigen, decimal IdCuenta,
                                                                decimal IdOficina, int EstadoCobro, int EstadoConfirmacion, int ConFecha, string FechaInicial, string FechaFinal,
                                                                        decimal IdSocio, decimal IdSerie, decimal NumeroDocumento)
        {
            return new DAAdministracionCobros().Listar(IdCobro, NumeroOperacion, Monto, IdBancoDestino, IdBancoOrigen, IdCuenta, IdOficina, EstadoCobro, EstadoConfirmacion,
                                                                ConFecha, FechaInicial, FechaFinal, IdSocio, IdSerie, NumeroDocumento);
        }


        public List<BEAdministracionCobros>ListarSociosCabezeraCobros(decimal IdCobro)
        {
            return new DAAdministracionCobros().ListarSocioCabezeraCobros(IdCobro);
        }

        public List<BEAdministracionCobros> ListarSocioDocumentosDetalleCobros(decimal IdCobro,decimal IdSocio)
        {
            return new DAAdministracionCobros().ListarSocioDocumentosDetalleCobros(IdCobro, IdSocio);
        }

        public List<BEAdministracionCobros> ListarReporte(decimal IdCobro, string NumeroOperacion, decimal Monto, decimal IdBancoDestino, decimal IdBancoOrigen, decimal IdCuenta,
                                                                decimal IdOficina, int EstadoCobro, int EstadoConfirmacion, int ConFecha, string FechaInicial, string FechaFinal,
                                                                        decimal IdSocio, decimal IdSerie, decimal NumeroDocumento)
        {
            return new DAAdministracionCobros().ListarReporte(IdCobro, NumeroOperacion, Monto, IdBancoDestino, IdBancoOrigen, IdCuenta, IdOficina, EstadoCobro, EstadoConfirmacion,
                                                                ConFecha, FechaInicial, FechaFinal, IdSocio, IdSerie, NumeroDocumento);
        }

        public int ActualizaEstadoCobro(decimal IdCobro,decimal IdRecCobro)
        {
            return new DAAdministracionCobros().ActualizaEstadoCobro(IdCobro, IdRecCobro);
        }
    }
}
