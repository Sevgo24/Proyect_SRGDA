using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOFacturaDetallle
    {
        public decimal Id { get; set; }
        public decimal codFactura { get; set; }
        public decimal codLicencia { get; set; }
        public string NombreLicencia { get; set; }
        public decimal codLicPlanificacion { get; set; }
        public decimal codEstablecimiento { get; set; }
        public string NombreEstablecimiento { get; set; }
        public DateTime FechaPlanificacion { get; set; }
        public decimal anio { get; set; }
        public string mes { get; set; }

        public decimal valorBruto { get; set; }
        public decimal valorDescuento { get; set; }
        public decimal valorBase { get; set; }
        public decimal valorImpuesto { get; set; }
        public decimal valorNeto { get; set; }

        public decimal addId { get; set; }
        public decimal rateId { get; set; }

        public string Periodo { get; set; }
        public string FechaPago { get; set; }
        public decimal BaseCobrado { get; set; }
        public decimal ImpuestoCobrado { get; set; }
        public decimal NetoCobrado { get; set; }
        public decimal Pendiente { get; set; }

        public decimal ValorNotaCredito { get; set; }
        public decimal CorrelativoNC { get; set; }
        public decimal PagoParcial { get; set; }
        public decimal SaldoPendiente { get; set; }

        //FACTURACION

        public string TipoNotaCredito { get; set; }
        public string Motivo { get; set; }
    }
}