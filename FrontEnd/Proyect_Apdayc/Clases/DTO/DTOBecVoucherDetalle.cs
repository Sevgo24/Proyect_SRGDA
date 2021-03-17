using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOBecVoucherDetalle
    {
        public string OWNER { get; set; }
        public decimal id { get; set; }
        public string tipoDocumento { get; set; }
        public string Factura { get; set; }        
        public decimal valorIngreso { get; set; }
        public string confirmacionIngreso { get; set; }
        public string confirmacionIngresoDesc { get; set; }

        public DateTime? fechaDeposito { get; set; }
        public DateTime? fechaConfirmacion { get; set; }
        public string usuarioConfirmacion { get; set; }
        public string codigoConfirmacion { get; set; }
        public DateTime? fechaDevolucion { get; set; }
        public DateTime? fechaContabilizacion { get; set; }
        public DateTime? FechaConciliacion { get; set; }

        public string Banco { get; set; }
        public string Sucursal { get; set; }
        public string CuentaBancaria { get; set; }
        public string idBanco { get; set; }
        public string idSucursal { get; set; }
        public string idCuentaBancaria { get; set; }
        public string Voucher { get; set; }
        public string idTipoPago { get; set; }
        public string TipoPago { get; set; }
        public string IdMoneda { get; set; }
        public string Moneda { get; set; }

        public string Observacion { get; set; }
        public string ObservacionUsuario { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        //Formas de pago
        //public string REC_PWDESC { get; set; }
        public decimal idReciboDetalle { get; set; }
        public decimal idFactura { get; set; }
        public decimal IdSocio { get; set; }
        public decimal DOC_ID { get; set; }

    }
}