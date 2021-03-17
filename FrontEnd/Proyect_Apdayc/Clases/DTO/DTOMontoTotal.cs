using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOMontoTotal
    {

        /// <summary>
        /// valor del resultado del test de tarifa
        /// </summary>
        public decimal? ValorTarifa { get; set; }

        /// <summary>
        /// valor total de descuentos
        /// </summary>
        public decimal? ValorDescuento { get; set; }
        /// <summary>
        /// valor total de cargos
        /// </summary>
        public decimal? ValorCargo { get; set; }
        /// <summary>
        /// valor total de Impuesto
        /// </summary>
        public decimal? ValorImpuesto { get; set; }

        /// <summary>
        /// Monto total a Facturar - del calculo de valor tarifa - valor descuento - valor impuesto
        /// </summary>
        public decimal? ValorFinal
        {
            get;
            
            set;
        }

        public decimal? ValorDescuentoRedondeoEspecial { get; set; }

        /// <summary>
        /// Indicador para determinar si el monto corresponde a un calculo en linea de tarifa,decusnetos e impuestos 
        /// o si es de una Factura ya creada.
        /// </summary>
        public bool TieneFacturacion { get; set; }

        /// <summary>
        /// Valor Total de facturas emitidas para una planificacion
        /// </summary>
        public decimal? ValorFinalFacturado { get; set; }
    }
}