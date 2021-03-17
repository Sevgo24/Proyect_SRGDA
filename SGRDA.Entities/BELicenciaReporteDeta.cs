using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaReporteDeta
    {
        public string OWNER { get; set; }
        public decimal REPORT_DID { get; set; }
        public decimal REPORT_ID { get; set; }
        public string REP_TITLE { get; set; }
        public string REP_AUTHOR_1 { get; set; }
        public string REP_AUTHOR_2 { get; set; }
        public string REP_ARTIST { get; set; }
        public string REP_SHOW { get; set; }
        public DateTime? REP_DATE_EMISION { get; set; }
        public DateTime?  REP_HOUR_EMISION { get; set; }
        public decimal? REP_DUR_MIN { get; set; }
        public decimal? REP_DUR_SEC { get; set; }
        public decimal? REP_DUR_TSEC { get; set; }
        public decimal REP_TIMES { get; set; }
        public decimal REP_CBASE { get; set; }
        public string ISWC { get; set; }
        public string ISRC { get; set; }
        public string IPI_NAME { get; set; }
        public string    NAME_IP { get; set; }
        public string CUR_ALPHA { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
    }
}
