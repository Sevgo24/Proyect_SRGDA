using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEDescuentos : Paginacion
    {
        public string OWNER { get; set; }
        public decimal LIC_DISC_ID { get; set; }
        public Int32 ORDEN { get; set; }

        public decimal LIC_ID { get; set; }
        public string DISC_ORG { get; set; }

        //     public string DES_ORIGEN { get; set; }
        public string ORIGEN { get; set; }
        public decimal DISC_ID { get; set; }
        public decimal DISC_TYPE { get; set; }
        public string TIPO { get; set; }
        public string DISC_NAME { get; set; }
        public string DISC_SIGN { get; set; }
        public decimal DISC_PERC { get; set; }
        public decimal DISC_VALUE { get; set; }
        public decimal DISC_ACC { get; set; }
        public char DISC_AUT { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
        public Decimal? TEMP_ID_DSC { get; set; }
        public string Activo { get; set; }
        public decimal BPS_ID { get; set; }
        public string OBSERVACION { get; set; }

        public int DISC_ESTADO { get; set; }

        public string DISC_RESP_OBSERVACION { get; set; }

        public int CANTIDAD { get; set; }

        public decimal VALOR { get; set; }
        /// <summary>
        /// FORMATO %,$
        /// </summary>
        public string FORMATO { get; set; }
        public Int32 BASE { get; set; }

        /// <summary>
        /// Indicador si el descuento es automatico o manual
        /// </summary>
        public bool IS_AUTO { get; set; }

        public string DISC_ORG_DESC { get; set; }
        //public List<BEDescuentos> ListaDescuentos { get; set; }

        /// <summary>
        /// AQUI SE GUARDA EL VALOR CALCULADO SI EL DSC FUERA DE TARIFA
        /// </summary>
        public decimal monto { get; set; }

        public string LOG_DATE_CREACION { get; set; }
    }
}
