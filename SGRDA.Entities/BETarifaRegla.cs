using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaRegla:Paginacion
    {
        public List<BETarifaRegla> ListarTarifaRegla { get; set; }
        public string OWNER { get; set; }
        public decimal CALR_ID { get; set; }
        public DateTime STARTS { get; set; }
        public string CALR_DESC { get; set; }
        public decimal RATE_FREQ { get; set; }
        public decimal TEMP_ID { get; set; }
        public decimal CALR_NVAR { get; set; }
        public string CALR_ADJUST { get; set; }
        public string CALR_ACCUM { get; set; }
        public string CALC_FORMULA { get; set; }
        public string CALC_MINIMUM { get; set; }
        public string CALC_IFORMULA { get; set; }
        public string CALC_IMINIMO { get; set; }
        public decimal CALR_FOR_DEC { get; set; }
        public string CALR_FOR_TYPE { get; set; }
        public decimal CALR_MIN_DEC { get; set; }
        public string CALR_MIN_TYPE { get; set; }
        public string CALR_OBSERV { get; set; }
        public string CUR_ALPHA { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        public List<BETarifaReglaData> Caracteristicas { get; set; }   
        public List<BETarifaPlantillaValor> Valores { get; set; }
        public List<BETarifaReglaValor> MatrizValores { get; set; }
        public string ESTADO { get; set; }
        public string PERIODOCIDAD { get; set; }
        public int CantReglaAsocMant { get; set; }
        public int CantCarMatriz { get; set; }
        public string RATE_CALC_VAR { get; set; }
        public decimal RATE_ID { get; set; }
        //fACTURACIÓN MASIVA
        public decimal? VALUE_FORMULA { get; set; }
        public decimal? VALUE_MINIMUN { get; set; }
        public decimal? VALUE_R { get; set; }
        public bool STATE_CALC { get; set; }
    }
}
