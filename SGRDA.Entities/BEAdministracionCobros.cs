using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAdministracionCobros
    {
        public decimal CodigoCobro { get; set; }
        public decimal CodigoRecCobro { get; set; }
        public string CodigoReferencia { get; set; }
        public decimal MontoVoucher { get; set; }
        public decimal MontoSaldoPendiente { get; set; }
        public decimal MontoSaldoUsado { get; set; }
        public decimal CodigoBancoOrigen { get; set; }
        public string NombreBancoOrigen { get; set; }
        public decimal CodigoBancoDestino { get; set; }
        public string NombreBancoDestino { get; set; }
        public decimal CodigoCuentaBanco { get; set; }
        public string DescripcionCuentaBanco { get; set; }
        public decimal CodigoOficina { get; set; }
        public string NombreOficinaCobro { get; set; }
        public int CodigoEstadoCobro { get; set; }
        public string DescripcionEstadoCobro { get; set; }
        public int CodigoEstadoCobroConfirmacion { get; set; }
        public string DescripcionEstadoCobroConfirmacion { get; set; }
        public string FechaCobroConfirmacion { get; set; }
        public decimal CodigoSocioCobro { get; set; }
        public string NombreyApelidosSocioCobro { get; set; }
        public decimal CodigoSerie { get; set; }
        public string DescripcionSerie { get; set; }
        public decimal NumeroFactura { get; set; }
        public decimal MontoDocumento { get; set; }
        public string EstadoDocumento { get; set; }
        public decimal CodigoOficinaLicencia { get; set; }
        public string DescripcionOficinaLicencia { get; set; }
        public string DocumentoIdentificacionSocio { get; set; }
        public int CantidadDocumentosxSocio { get; set; }
        public decimal CodigoDocumento { get; set; }
        public int EstadoCobro { get; set; }

        //

    }
}
