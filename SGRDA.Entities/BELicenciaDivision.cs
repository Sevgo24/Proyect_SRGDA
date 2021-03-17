using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaDivision
    {
        public string OWNER { get; set; }
        public decimal LIC_DIV_ID { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal DAD_ID { get; set; }
        public DateTime LOG_DATE_CREAT{ get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        //
        public decimal OFF_ID { get; set; }
        //OFICINA - NOMBRE
        public string OFF_NAME { get; set; }
        //DIVISION - NOMBRE
        public string DAD_NAME { get; set; }
        public string MOG_ID { get; set; }
        //GRUPO MODALIDAD - NOMBRE
        public string MOG_DESC { get; set; }
        public List<BELicenciaDivisionAgente> ListarAgenteXDivision { get; set; }
    }
}
