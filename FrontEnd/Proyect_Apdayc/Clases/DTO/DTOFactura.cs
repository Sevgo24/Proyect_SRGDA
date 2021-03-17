using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class    DTOFactura
    {
        public decimal id { get; set; }
        public decimal idLicencia { get; set; }
        public string idMoneda { get; set; }
        public int idSerieFact { get; set; }
        public decimal idTipoLic { get; set; }
        public decimal? idGF { get; set; }
        public string numFactura { get; set; }
        public decimal numFacturaActual { get; set; }
        public DateTime? fechaFact { get; set; }
        public string tipoFact { get; set; }
        public string indDetalleFact { get; set; }
        public string etapaFact { get; set; }
        public decimal numReImpresion { get; set; }
        public decimal numCopias { get; set; }
        public decimal idBps { get; set; }
        public decimal idDireccionBps { get; set; }
        public decimal idPreFact { get; set; }
        public decimal idProceso { get; set; }
        public DateTime fechaImpresionFact { get; set; }
        public DateTime fechaReImpresion { get; set; }
        public DateTime fechaCopiaImpresion        { get; set; }
        public DateTime? fechaAnulacion { get; set; }
        public string motivoAnulacion { get; set; }
        public DateTime fechaContaFact { get; set; }
        public decimal idProcesoConta { get; set; }
        public DateTime fechaLiqComTot { get; set; }
        public string observacion { get; set; }
        public string indNotaCred { get; set; }
        public decimal factRefNotCred { get; set; }
        public decimal valorBase { get; set; }
        public decimal valoImpuesto { get; set; }
        public decimal valorFinal { get; set; }
        public decimal valorBasePagado { get; set; }
        public decimal valorImpuestoPagado { get; set; }
        public decimal valorRetenciones { get; set; }
        public decimal valorTotPagado { get; set; }
        public decimal valorDescuento { get; set; }
        public decimal cobradoBase { get; set; }
        public decimal cobradoImpuesto { get; set; }
        public decimal retenciones { get; set; }
        public decimal cobradoNeto { get; set; }
        public decimal saldoFactura { get; set; }
        public DateTime fechaDist { get; set; }
        public decimal procesoDist { get; set; }
        public string key { get; set; }
        public DateTime fechaCrea { get; set; }
        public DateTime fechaMod { get; set; }
        public string usuarioCre { get; set; }
        public string usuarioMod { get; set; }
        public decimal licplId { get; set; }
        public decimal idFecha { get; set; }
        public string tipoFacturaDes { get; set; }

        public string Estado_Sunat { get; set; }

        public string socio { get; set; }
        public string doc_Identificacion { get; set; }
        public string num_Identificacion { get; set; }
        public string moneda { get; set; }
        public string tipo_pago { get; set; }
        public string grupo_fact { get; set; }
        public decimal total { get; set; }
        public decimal Nro { get; set; }
        public string idTipoPago { get; set; }
        public int estadoFact { get; set; }
        public DateTime? fecha_cancelacion { get; set; }

        //datos Factura pendiente pago
        public string tipo { get; set; }
        public DateTime? fecha_venc { get; set; }

        public decimal saldoPendiente { get; set; }
        public bool estadoVisualizar { get; set; }

        public string estadopago { get; set; }
        public string serial { get; set; }
        public decimal descuento { get; set; }
        public decimal subTotal { get; set; }
        public decimal CorrelativoNC { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// <summary>
        /// Estado del periodo: A (ABIERTO) P (PARCIAL) T ( TOTAL)
        /// </summary>
        public string EstadoPeriodo { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        public List<DTODescuentoPlantilla> listadescuentos { get; set; }

        //DIRECCION SOCIO
        public string Direccion { get; set; }

        public bool Valida_Periodo_Fact { get; set; }
    }
}