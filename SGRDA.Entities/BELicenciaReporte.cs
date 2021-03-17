using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaReporte
    {
        public string OWNER { get; set; }
        public decimal REPORT_DID { get; set; }
        public decimal REPORT_ID { get; set; }
        public string REPORT_DESC { get; set; }
        public decimal? SHOW_ID { get; set; }
        public decimal? ARTIST_ID { get; set; }
        public decimal REPORT_TYPE { get; set; }
        public decimal REPORT_STATUS { get; set; }
        public string MOD_ORIG { get; set; }
        public string MOD_SOC { get; set; }
        public string CLASS_COD { get; set; }
        public string MOG_ID { get; set; }
        public string RIGHT_COD { get; set; }
        public string MOD_INCID { get; set; }
        public string MOD_USAGE { get; set; }
        public string MOD_REPER { get; set; }
        public string REPORT_ESOCIETY { get; set; }
        public decimal TIS_N { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal EST_ID { get; set; }
        public decimal LIC_PL_ID { get; set; }
        public DateTime? REPORT_DATE_FROM { get; set; }
        public DateTime? REPORT_DATE_TO { get; set; }
        public string REPORT_INV { get; set; }
        public decimal? REPORT_USGD { get; set; }
        public decimal? REPORT_TIMES { get; set; }
        public decimal? REPORT_CALC { get; set; }
        public string REPORT_DIST_CODE { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
        public int REPORT_NCOPY { get; set; }
        
        public List<BELicenciaReporte> lista = new List<BELicenciaReporte>();

        public BELicenciaReporte()
        {
            lista = new List<BELicenciaReporte>();
        }

        //------------------------------cambio para planillas
        public string ModUso { get; set; }
        public decimal? NMR_ID  { get; set; }
        public decimal? REPORT_NUMBER { get; set; }
        public bool? REPORT_IND { get; set; }
        //----------------------------------------------------
        public decimal INV_ID { get; set; }
        public decimal INV_CN_REF { get; set; }
        public DateTime INV_NULL { get; set; }
        //----------------------------------------------------
        public string INVT_DESC { get; set; }
        public string NMR_SERIAL { get; set; }
        public decimal INV_NUMBER { get; set; }
        public decimal LIC_MONTH { get; set; }
        public decimal LIC_YEAR { get; set; }
        public decimal INV_NET { get; set; }
        public decimal? REPORT_NUMBER_REFERENCE { get; set; }


        //------------------------------ARTISTAS
        public string NAME { get; set; }

        public string DESCRIPCION { get; set;}

        public string VALOR { get; set;}

        //---------- LICENCIA
        public string LICENCIA { get; set; }
        public string GRUPO_MODALIDAD { get; set; }
        public string MODALIDAD { get; set; }
        public string PLANILLA { get; set; }
        public decimal MOD_ID { get; set; }
    }
}
