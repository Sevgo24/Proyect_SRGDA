using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETarifaPlantillaDescuento : Paginacion
    {
        public List<BETarifaPlantillaDescuento> ListarTarifaPlantillaDsc { get; set; }
        public string OWNER { get; set; }
        public decimal TEMP_ID_DSC { get; set; }
        public string TEMP_DESC { get; set; }
        public decimal TEMP_NVAR { get; set; }
        public string DISC_FOR_TYPE { get; set; }
        public DateTime STARTS { get; set; }
        public DateTime ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string ESTADO { get; set; }
        public decimal MATRIZ_CHANGE { get; set; }

        public List<BETarifaPlantillaDescuentoCaracteristica> LstDscCaracteristica { get; set; }
        public List<BETarifaPlantillaDescuentoSeccion> LstDscSeccion { get; set; }
        public List<BETarifaPlantillaDescuentoValores> LstDscValores { get; set; }

    }
}
