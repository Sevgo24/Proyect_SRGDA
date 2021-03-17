using System;
using System.Collections.Generic;

namespace SGRDA.Entities
{
    public class BEComisionAgenteRecaudo
    {
        public Int64 RowNumber { get; set; }
        public string OWNER { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal auxBPS_ID { get; set; }
        public decimal MOD_ID { get; set; }
        public decimal COMT_ID { get; set; }
        public decimal LEVEL_ID { get; set; }
        public decimal COM_ORG { get; set; }
        public DateTime? COM_START { get; set; }
        public DateTime? COM_ENDS { get; set; }
        public decimal? COM_PER { get; set; }
        public decimal? COM_VAL { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string ESTADO { get; set; }
        public int TotalVirtual { get; set; }
        public string COM_DESC { get; set; }
        public string DAD_NAME { get; set; }
        public string MOD_DEC { get; set; }
        public string DESCRIPTION { get; set; } //Descripcion del nivel de agente
        public string BPS_NAME { get; set; }
        public List<BEComisionAgenteRecaudo> ListaComisionAgenteRecaudos = null;

        public BEComisionAgenteRecaudo()
        {
            ListaComisionAgenteRecaudos = new List<BEComisionAgenteRecaudo>();
        }

        /////
        /// Indicador para saber si graba o actualiza
        /////
        public int valgraba { get; set; }

        /////
        /// Indicador para el formato de la comisión
        /////
        public string Formato { get; set; }

        /////
        /// guarda el valor ya sea Monto o porcentaje
        /////
        public decimal Valor { get; set; }

        /////
        /// guarda la fecha como cadena
        /////
        public string fechaStart { get; set; }

        /////
        /// Entidades Modalidad
        /////
        public string MOD_ORIG { get; set; }
        public string MOD_SOC { get; set; }
        public string CLASS_COD { get; set; }
        public string MOG_ID { get; set; }
        public string RIGHT_COD { get; set; }
        public string MOD_INCID { get; set; }
        public string MOD_USAGE { get; set; }
        public string MOD_REPER { get; set; }

        /////
        /// Descripcion del Nivel de agente
        /////
        public string NIVEL { get; set; }

        /////
        /// Datos tarifa segun temporalidad
        /////
        public decimal RAT_FID { get; set; }
        public string RAT_FDESC { get; set; }
        public decimal RATE_ID { get; set; }
        public string NAME { get; set; }
    }
}
