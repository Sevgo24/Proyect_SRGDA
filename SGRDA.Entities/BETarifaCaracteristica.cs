using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaCaracteristica
    {
        public string OWNER { get; set; }
        public decimal RATE_CHAR_ID { get; set; }
        public decimal RATE_ID { get; set; }
        public string RATE_CHAR_TVAR { get; set; }
        public string RATE_CHAR_DESCVAR { get; set; }
        public string RATE_CHAR_VARUNID { get; set; }
        public string RATE_CHAR_CARIDSW { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string RATE_CHAR_SHORT { get; set; }//agregando descripcion Corta

        public decimal RATE_CALC { get; set; } //IdRegla
        public decimal RATE_CALC_AR { get; set; } //IdCaracteristica
        public decimal RATE_CALC_ID { get; set; }
        public string RATE_CALCT { get; set; } //R o F
        //Tarifa - Test
        public string IND_TR { get; set; }
        public string CHAR_ORI_REG { get; set; }
        //Facturación Masiva
        public decimal? VALUE { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal LIC_CHAR_VAL { get; set; }
        public int VALIDACION_FECHA { get; set; }
        public decimal LIC_PL_ID { get; set; }
    }
}
