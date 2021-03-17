using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAjustesComision
    {
        public List<BEAjustesComision> ListarPage(string owner, decimal IdAgente, DateTime Fecha, string IdMondeda, decimal IdLicencia, decimal IdModalidad, int pagina, int cantRegxPag)
        {
            return new DAAjustesComision().ListarPage(owner,IdAgente,Fecha,IdMondeda,IdLicencia,IdModalidad,pagina, cantRegxPag);
        }

        public BEAjustesComision ObtenerDatosGrabar(BEAjustesComision en)
        {
            return new DAAjustesComision().ObtenerDatosGrabar(en);
        }

        public int Insertar(BEAjustesComision en)
        {
            return new DAAjustesComision().Insertar(en);
        }

        public BEAjustesComision ObtenerDatos(string Owner, decimal Id)
        {
            return new DAAjustesComision().ObtenerDatos(Owner, Id);
        }

        public int Actualizar(BEAjustesComision en)
        {
            return new DAAjustesComision().Actualizar(en);
        }

        public BEAjustesComision TotalValorAjusteComision(BEAjustesComision en)
        {
            return new DAAjustesComision().TotalValorAjusteComision(en);
        }

        public int ValidacionAjusteComision(BEAjustesComision en)
        {
            return new DAAjustesComision().ValidacionAjusteComision(en);
        }

        //LIBERACION RETENCIO DE COMISIONES
        public List<BEAjustesComision> ListarRetLibComisiones(string owner, decimal IdRepresentante, decimal IdTipoComision, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, decimal IdTarifa, decimal IdOficina, string IdMoneda, decimal IdDivAdm, DateTime FechaIni, DateTime FechaFin, int pagina, int cantRegxPag)
        {
            return new DAAjustesComision().ListarRetLibComisiones(owner, IdRepresentante, IdTipoComision, IdNivel, IdModalidad, IdEstablecimiento, IdLicencia, IdTarifa, IdOficina, IdMoneda, IdDivAdm, FechaIni, FechaFin, pagina, cantRegxPag);
        }

        public BEAjustesComision RetencionLiberacionTotal(BEAjustesComision en)
        {
            return new DAAjustesComision().RetencionLiberacionTotal(en);
        }

        public int ActivarRetencionLiberacion(BEAjustesComision en)
        {
            return new DAAjustesComision().ActivarRetencionLiberacion(en);
        }

        public int InactivarRetencionLiberacion(BEAjustesComision en)
        {
            return new DAAjustesComision().InactivarRetencionLiberacion(en);
        }

        public List<BEAjustesComision> ListarRetenciones(string Owner)
        {
            return new DAAjustesComision().ListarRetenciones(Owner);
        }

        //PRE-LIQUIDACION DE COMISIONES
        public List<BEAjustesComision> ListarPreYliquidacionComisiones(string owner, decimal IdRepresentante, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, DateTime FechaIni, DateTime FechaFin, int pagina, int cantRegxPag)
        {
            return new DAAjustesComision().ListarPreYliquidacionComisiones(owner, IdRepresentante, IdNivel, IdModalidad, IdEstablecimiento, IdLicencia, FechaIni, FechaFin, pagina, cantRegxPag);
        }
        
        public BEAjustesComision PreYLiquidacionTotal(BEAjustesComision en)
        {
            return new DAAjustesComision().PreYLiquidacionTotal(en);
        }

        public int ActivarLiquidacion(BEAjustesComision en)
        {
            return new DAAjustesComision().ActivarLiquidacion(en);
        }

        public List<BEAjustesComision> ListarLiquidacion(string Owner)
        {
            return new DAAjustesComision().ListarLiquidacion(Owner);
        }

        public BEAjustesComision obtenerDatosPorId(string Owner, decimal IdSequence)
        {
            return new DAAjustesComision().obtenerDatosPorId(Owner, IdSequence);
        }

        //PAGO DE COMISIONES
        public List<BEAjustesComision> ListarComisionPago(string owner, decimal IdRepresentante, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, DateTime FechaIni, DateTime FechaFin, int pagina, int cantRegxPag)
        {
            return new DAAjustesComision().ListarComisionPago(owner, IdRepresentante, IdNivel, IdModalidad, IdEstablecimiento, IdLicencia, FechaIni, FechaFin, pagina, cantRegxPag);
        }

        public BEAjustesComision PagoTotal(BEAjustesComision en)
        {
            return new DAAjustesComision().PagoTotal(en);
        }

        public int ActualizarPago(BEAjustesComision en)
        {
            return new DAAjustesComision().ActualizarPago(en);
        }
    }
}
