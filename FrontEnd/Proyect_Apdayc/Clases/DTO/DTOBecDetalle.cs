using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOBecDetalle : Controller
    {
        public decimal id { get; set; }
        public decimal idRecibo { get; set; }
        public decimal IdFactura { get; set; }
        public decimal idLicencia { get; set; }
        public string idMoneda { get; set; }
        public int idSerieFact { get; set; }
        public string numFactura { get; set; }
        public DateTime? fechaFact { get; set; }
        public decimal idBps { get; set; }
        public string tipoDocumento{ get; set; }
        public string socio { get; set; }

        public decimal valorBase { get; set; }
        public decimal valorImpuesto { get; set; }        
        public decimal valorBasePagado { get; set; }
        public decimal valorRetenciones { get; set; }
        public decimal valorDescuento { get; set; }
        public decimal valorFinal { get; set; }
        public decimal pendienteAplicar { get; set; }
        public decimal montoAplicar { get; set; }
        public decimal montoAplicarNuevo { get; set; }
        public string Moneda { get; set; }
        public string key { get; set; }        
        public string serial { get; set; }
        public string Factura { get; set; }
        public string Recibo { get; set; }
        
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

    }
}
