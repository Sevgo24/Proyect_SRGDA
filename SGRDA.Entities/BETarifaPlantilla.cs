using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{

    

    public class BETarifaPlantilla:Paginacion
    {
        public List<BETarifaPlantilla> ListarTarifaPlantilla { get; set; }

        public string OWNER { get; set; }
        public decimal TEMP_ID { get; set; }
        public string TEMP_DESC { get; set; }
        public decimal TEMP_NVAR { get; set; }
        public Nullable<DateTime> STARTS { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }        
        
        public List<BETarifaPlantillaCaracteristica> Caracteristicas { get; set; }
        public List<BETarifaPlantillaValor> Valores { get; set; }
        public string ESTADO { get; set; }
        public string LETRA { get; set; }
        public List<BETarifaReglaValor> ValoresMatriz { get; set; }
    }
}
