using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_RATES_GRAL: Paginacion
    {
        public BEREC_RATES_GRAL()
        {
            listaTarifa = new List<BEREC_RATES_GRAL>();
        }

        public string OWNER { get; set; }
        public decimal RATE_ID { get; set; }
        public decimal? RATE_ID_PREC { get; set; }
        public string CUR_ALPHA { get; set; }
        public string CLASS_COD { get; set; }
        public string RIGHT_COD { get; set; }
        public string MOG_ID { get; set; }
        public string MOD_INCID { get; set; }
        public string MOD_USAGE { get; set; }
        public string MOD_REPER { get; set; }
        public decimal RAT_FID { get; set; }
        public string RATE_FORMAT { get; set; }
        public string NAME { get; set; }         
        public string RATE_OBSERV { get; set; }
        public DateTime RATE_START { get; set; }
        public DateTime RATE_END { get; set; }
        public string RATE_DESC { get; set; }
        public decimal RATE_FID { get; set; }
        public decimal RATE_NVAR { get; set; }
        public decimal RATE_NCAL { get; set; }
        public string RATE_DREPERT { get; set; }
        public string RATE_ACCOUNT { get; set; }
        public string RATE_FORMULA { get; set; }
        public string RATE_FTIPO { get; set; }
        public decimal RATE_FDECI { get; set; }
        public string RATE_MINIMUM { get; set; }
        public string RATE_MTIPO { get; set; }
        public decimal RATE_MDECI { get; set; }
        public decimal RATE_TEMP { get; set; }
        public string RATE_LDESC { get; set; }     
        public DateTime? ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string ESTADO { get; set; }
        public decimal RATE_ID_ORIG { get; set; }

        public List<BEREC_RATES_GRAL> listaTarifa = null;
        public List<BETarifaReglaAsociada> ReglasAsoc { get; set; }
        public List<BETarifaCaracteristica> Caracteristica { get; set; }
        public List<BETarifaReglaParamAsociada> Parametro { get; set; }
        public List<BETarifaDescuento> Descuento { get; set; }

        public decimal MOD_ID { get; set; }
        public string MOD_DEC { get; set; }
        public string RAT_FDESC { get; set; }

        public List<BETarifaRegla> Regla { get; set; }
        public List<BETarifaTest> Test { get; set; }
        //
        public decimal VALUE_FORMULA { get; set; }
        public decimal VALUE_MINIMUN { get; set; }
        public bool STATE_CALC { get; set; }
        public int RATE_REDONDEO { get; set; }
        public decimal VUM { get; set; }
        public int RATE_REDONDEO_ESP { get; set; }
    }
}
