using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOLicenciaPlaneamiento 
    {
        public decimal codigoLP { get; set; }
        public decimal codigoLic { get; set; }
        public decimal codigoMod { get; set; }
        public decimal codigoEst { get; set; }
        public decimal codigoTarifa { get; set; }
        public decimal anio { get; set; }
        public decimal mes { get; set; }
        public string mesNom { get; set; }
        public decimal orden { get; set; }
        public DateTime fecha { get; set; }
        public decimal? codBloqueo { get; set; }
        public string descBloqueo { get; set; }
        public string descMes { get; set; }
        public string codTipoPago { get; set; }
        public string situacion { get; set; }

        public string situacion_Electronica { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public decimal Monto { get; set; }
        public decimal Nro { get; set; }
        public decimal SubMonto { get; set; }
        public decimal Descuento { get; set; }
        public decimal valorImpuesto { get; set; }
        public string Observacion { get; set; }
        public bool EstadoVisualizar { get; set; }

        public string NroFactura { get; set; }
        public string NroSerie { get; set; }
        public string ImporteFactura { get; set; }
        /// <summary>
        /// Estado del periodo: A (ABIERTO) P (PARCIAL) T ( TOTAL)
        /// </summary>
        /// 
        public string EstadoPeriodo { get; set; }
        public string EstadoPeriodoDes { get; set; }

        public bool EstadoPeriodoElectronico { get; set; } //validacion estado periodo electronico
    }
}
