using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    [Serializable]
    public class DTOLicenciaCaracteristica
    {

        public string Owner { get; set; }
        public decimal CodigoLic { get; set; }
        public DateTime? FechaCaracteristicaLic { get; set; }
        public decimal CodigoCaracteristica { get; set; }
        /// <summary>
        /// Descripcion de la caracteristica
        /// </summary>
        public string DescCarateristica { get; set; }

        /// <summary>
        /// Tipo de la caracteristica E=establecimiento T=tarifa
        /// </summary>
        public string   Tipo { get; set; }

        public decimal? Valor { get; set; }

        public decimal CodigoLicPlan { get; set; }

        public string ValorString { get; set; }

        /// <summary>
        /// Indicador que sirve para determinar que el valor de la caractreristica es un valor alterado(No es el dato real)
        /// debido a que se desea llegar a un monto para la factura distinto a lo planificado.
        /// </summary>
        public bool? EsCaractAlterada { get; set; }
        /// <summary>
        /// Descripcion del indicador EsCaractAlterada
        /// </summary>
        public string CaractAlteradaDesc { get; set; }

    }
}
