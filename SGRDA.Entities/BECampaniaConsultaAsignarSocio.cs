using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BECampaniaConsultaAsignarSocio
    {
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public string TAXN_NAME { get; set; }
        public string TAX_ID { get; set; }
        public decimal INV_NET { get; set; }
        public decimal INV_COLLECTN { get; set; }
        public decimal INV_BALANCE { get; set; }
        public string BPS_USER { get; set; }
        public string BPS_COLLECTOR { get; set; }
        public string BPS_ASSOCIATION { get; set; }
        public string BPS_GROUP { get; set; }
        public string BPS_EMPLOYEE { get; set; }
        public string BPS_SUPPLIER { get; set; }
        public string PERFIL { get; set; }

        public List<BECampaniaConsultaAsignarSocio> AsignarSocioCab { get; set; }
        public List<BECampaniaConsultaAsignarSocio> AsignarSocioDet { get; set; }
        public List<BECampaniaConsultaAsignarSocio> AsignarSocioSubDet { get; set; }

        public decimal LIC_ID { get; set; }
        public string LIC_NAME { get; set; }
        public string EST_NAME { get; set; }
        //public decimal INV_NET { get; set; }
        //public decimal INV_COLLECTN { get; set; }
        //public decimal INV_BALANCE { get; set; }
        public string CUR_DESC { get; set; }
        public string LIC_DATE_CAD { get; set; }
        public decimal LIC_PL_ID { get; set; }
    }
}
