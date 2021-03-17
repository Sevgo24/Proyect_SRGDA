using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOLicencia 
    {
        public decimal codLicencia { get; set; }
        public decimal tipoLicencia { get; set; }
        public decimal codEstado { get; set; }
        public string estadoLicencia { get; set; }
        public decimal? codMultiple { get; set; }
        public string codMoneda { get; set; }
        public string Moneda { get; set; }
        public string TipoPago { get; set; }
        public decimal codTemporalidad { get; set; }
        public string Temporalidad { get; set; }
        public string nombreLicencia { get; set; }
        public string descLicencia { get; set; }
        public decimal idDireccionEst { get; set; }
        /// <summary>
        /// indicador de factura detalla - LIC_INVD
        /// </summary>
        public string IndFactDeta { get; set; } 

        /// <summary>
        /// Indicador de Reportes de Uso Requeridos (obsoleto, ahora es Requiere documento)  - LIC_DREQ
        /// </summary>
        public string IndReqReporte { get; set; }

        /// <summary>
        /// Indicador de Actualización de Características Requerida - LIC_CREQ
        /// </summary>
        public string IndUpdCaracteristicas { get; set; }

        /// <summary>
        /// Indicador de Actualizacion de Planilla Automatica
        /// </summary>
        public string IndUpdPlanilla { get; set; }

        /// <summary>
        /// Tipo de Formas de entrega de la factura INVF_ID
        /// </summary>
        public decimal FormaEntregaFact { get; set; }

        /// <summary>
        /// Tipo de Formas de envio de la factura LIC_SEND
        /// </summary>
        public decimal? TipoEnvioFact { get; set; }

        /// <summary>
        /// Indicador de Descuentos Visibles - LIC_DISC
        /// </summary>
        public string IndDscVisible { get; set; }

        /// <summary>
        /// Indicador de Si debe o no mostrarse en una emision Mensual
        /// </summary>
        public string IndEmiMensual { get; set; }

        /// <summary>
        /// Grupo de Facturacion - LIC_GROUP
        /// </summary>
        public decimal GrupoFacturacion { get; set; }
        public string GrupoFacturacionDes { get; set; }

        public decimal codModalidad { get; set; }
        public string Modalidad { get; set; }
        public decimal codEstablecimiento { get; set; }
        public string Establecimiento { get; set; }
        public decimal codUsuDerecho { get; set; }
        public string UsuarioDerecho { get; set; }
        public decimal tarifaAsociada { get; set; }

        public bool EnBD { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        //addon dbs 20141006
        public decimal codLicenciaPadre { get; set; }
        public decimal codTarifa { get; set; }
        public decimal codTipoDivision { get; set; }
        public decimal codDivision { get; set; }
        

        public string codTipoPago { get; set; }

        public decimal codWorkFlow { get; set; }
        public decimal Nro { get; set; }        
        public decimal codFactura { get; set; }
        public string Observacion { get; set; }

        public decimal Monto { get; set; }                        
        public decimal Descuento { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal Pendiente { get; set; }



        /// <summary>
        /// Tiene planificación configurada una licencia?
        /// </summary>
        public bool hasPlanning { get; set; }

        /// <summary>
        /// Codigo de modalidad de uso
        /// PER = Permanente
        /// TEM = Temporal
        /// </summary>
        public string codModUso { get; set; }



        /// <summary>
        ///  DIVISION DE LA LICENCIA
        ///    
        /// </summary>
        public string DIVISION { get; set; }
        public string OFICINA { get; set; }

        public decimal DescuentoRedondeo { get; set; }
    }
}
