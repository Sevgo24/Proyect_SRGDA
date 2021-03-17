using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODetalleMetodoPago
    {
        public decimal Id { get; set; } //consecutivo id Método de pago del recibo
        public decimal IdRecibo { get; set; }
        public string IdMetodoPago { get; set; }
        public decimal ValorIgreso { get; set; }
        public string ConfirmacionIngreso { get; set; }
        public DateTime? FechaConfirmacion { get; set; }
        public string UsuarioConfirmacionIngreso { get; set; }
        public string CodigoConfirmacionIngreso { get; set; }
        public string CuentaBancaria { get; set; }
        public string Voucher { get; set; }
        public string TipoCambio { get; set; }
        public DateTime? FechaDeposito { get; set; }

        public DateTime? FechaDevolucion { get; set; }
        public DateTime? FechaContabilizacion { get; set; }
        public DateTime? FechaConciliado { get; set; }

        public string IdBanco { get; set; }
        public string IdSucursal { get; set; }
        public string IdCuentaBancaria { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        //Método de pago
        public string MetodoPago { get; set; }
        public string Banco { get; set; }
        public string Sucursal { get; set; }
        public string IdMoneda { get; set; }
    }
}