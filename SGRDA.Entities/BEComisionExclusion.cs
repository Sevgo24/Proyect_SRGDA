using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEComisionExclusion
    {
        public Int64 RowNumber { get; set; }
        public string OWNER { get; set; }
        public decimal DADV_ID { get; set; }
        public decimal auxDADV_ID { get; set; }
        public decimal MOD_ID { get; set; }
        public decimal COMT_ID { get; set; }
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
        public List<BEComisionExclusion> ListaComisionEx = null;

        public BEComisionExclusion()
        {
            ListaComisionEx = new List<BEComisionExclusion>();
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
        /// Datos tarifa segun temporalidad
        /////
        public decimal RAT_FID { get; set; }
        public string RAT_FDESC { get; set; }
        public decimal RATE_ID { get; set; }
        public string NAME { get; set; }
    }
}
