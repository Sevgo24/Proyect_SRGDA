using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEDireccion : Paginacion
    {
        public string OWNER { get; set; }
        public decimal ADD_ID { get; set; }
        public decimal ADD_TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal ENT_ID { get; set; }
        public string ENT_DESC { get; set; }
        public decimal TIS_N { get; set; }
        public decimal GEO_ID { get; set; }
        public decimal ROU_ID { get; set; }
        public string ROU_NAME { get; set; }
        public string ROU_NUM { get; set; }
        public string HOU_TURZN { get; set; }
        public string HOU_URZN { get; set; }
        public string HOU_NRO { get; set; }
        public string HOU_MZ { get; set; }
        public string HOU_LOT { get; set; }
        public string HOU_TETP { get; set; }
        public string HOU_NETP { get; set; }
        public string ADD_TINT { get; set; }
        public string ADD_INT { get; set; }
        public string ADD_ADDTL { get; set; }
        public string ADD_REFER { get; set; }
        public string ADDRESS { get; set; }
        public decimal CPO_ID { get; set; }
        public Nullable<char> MAIN_ADD { get; set; }
        public decimal ADD_CX { get; set; }
        public decimal ADD_CY { get; set; }
        public string REMARK { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public decimal Nro { get; set; }
    }
}
