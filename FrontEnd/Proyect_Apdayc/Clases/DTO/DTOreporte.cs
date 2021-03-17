using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOreporte 
    {
        //
        // GET: /DTOreporte/

        public decimal idReporte { get; set; }
        public decimal idModalidad { get; set; }
        public string DescPlanilla { get; set; }
        public decimal? idShow { get; set; }
        public decimal? idArtista { get; set; }
        public string CodigoTipoPlanilla { get; set; }
        public string DsecTipoPlanilla { get; set; }
        public decimal EstadoPlanilla { get; set; }
        public string CodigoSociedad { get; set; }
        public decimal CodigoTerritorio { get; set; }
        public decimal idBps { get; set; }
        public decimal CodigoLicencia { get; set; }
        public decimal CodigoEstablecimiento { get; set; }
        public decimal CodigoPerFacturacion { get; set; }
        public string FechaInicioPeriodo { get; set; }
        public string FechaFinPeriodo { get; set; }

        public string ReporteFactura { get; set; }
        public decimal? NumeroDetalle { get; set; }
        public decimal? NumeroEjecuciones { get; set; }
        public decimal? SumaCalculos { get; set; }
        public string ReporteCode { get; set; }
        public int ReporteCopy { get; set; }

        public bool Activo { get; set; }

        public decimal CodigoTipoRep { get; set; }

        public decimal? CodigoAutorizacion { get; set; }
        public DateTime? FecDesde { get; set; }
        public DateTime? FecHasta { get; set; }

        //------------------------------cambio para planillas
        public string modUso { get; set; }
        public decimal? IdSerie { get; set; }
        public decimal CorrelativoPlanilla { get; set; }
        public decimal IndicadorEmisionPlanilla { get; set; }
        //---------------------------------------------------

        public decimal? NumReporte { get; set; }
        public string TipoDocumento { get; set; }
        public string Serie { get; set; }
        public decimal NumFactura { get; set; }
        public string Periodo { get; set; }
        public decimal Importe { get; set; }
        public DateTime? Fecha { get; set; }

        //Num referencia de planilla fisica - excepcionales
        public decimal? NumReporteReferencia { get; set; }
        public string Documentos { get; set; }

        //Nombre de Artista
        public string NAME { get; set; }

    }
}
