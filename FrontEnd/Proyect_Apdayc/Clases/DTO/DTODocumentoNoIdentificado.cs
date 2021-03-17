using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODocumentoNoIdentificado
    {
        public DateTime FechaCreacion { get; set; }
        public string Usuario_Crea { get; set; }
        public decimal Id_Oficina { get; set; }
        public string Id_Moneda { get; set; }
        public decimal Id_Socio { get; set; }
        public decimal Monto_Original { get; set; }
        public decimal Monto_Soles { get; set; }
        //--  FACTURA --
        public decimal Id_factura { get; set; }
        public decimal Id_FacturaDetalle { get; set; }
        public decimal Tipo_Cambio { get; set; }
        //-- COBRO --
        public decimal Id_Cobro { get; set; }
        public decimal Id_Recibo { get; set; }
        public string ID_Tipo_Deposito { get; set; }
        public int Id_Banco { get; set; }
        public int Id_Cuenta { get; set; }
        public DateTime Fecha_Deposito { get; set; }
        public string Nro_Confirmacion { get; set; }
        public decimal Id_Deposito { get; set; }

        public string Observacion { get; set; }

        
        // DESCRIPCION
        //public string tipoPago { get; set; }
        //public string banco { get; set; }
        //public string moneda { get; set; }
        //public string cuenta { get; set; }
        //public string fechaDeposito { get; set; }
        //public string fechaDeposito { get; set; }


    }
}