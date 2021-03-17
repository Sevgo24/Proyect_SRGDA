using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEReporteArtistaDetallado
    {
        // Cabecera
        public decimal LIC_ID { get; set; }
        public string EVENTO { get; set; }
        public string SOCIO { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string FECHA_AUTORIZACION_INI { get; set; }
        public string FECHA_AUTORIZACION_FIN { get; set; }
        public string NOMBRE_ESTABLECIMIENTO { get; set; }
        public string UBIGEO { get; set; }

        //////Detalle
        public int CANTIDAD_ARTISTAS { get; set; } 
        public string ID_GRUPO { get; set; }
        public string GRUPO { get; set; }
        public decimal ID_MOD { get; set; }
        public string MODALIDAD { get; set; }
        public string CODIGO_SAP { get; set; }
        public int CANTIDAD_ARTISTA_SHOW { get; set; }
        public decimal ID_SHOW { get; set; }
        public string SHOW_NAME { get; set; }
        public string SHOW_START { get; set; }
        public string SHOW_ENDS { get; set; }
        public decimal ARTISTA_ID { get; set; }
        public string ARTISTA { get; set; }
        public decimal ARTISTA_ID_SGS { get; set; }
        public string ESTADO_ARTISTA { get; set; }

        //Detalle Factura
        public decimal INV_ID { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string SERIE { get; set; }
        public string NUMERO { get; set; }
        public string FACTURA { get; set; }
        public string FECHA_EMISION { get; set; }
        public string FECHA_CANCELACION { get; set; }
        public string FECHA_CONTABLE { get; set; }
        public string FECHA_ANULLADO { get; set; }
        public string MONEDA { get; set; }
        public decimal FACTURADO { get; set; }
        public decimal RECAUDADO { get; set; }
        public decimal PENDIENTE { get; set; }
        public string ESTADO { get; set; }


    }
}
