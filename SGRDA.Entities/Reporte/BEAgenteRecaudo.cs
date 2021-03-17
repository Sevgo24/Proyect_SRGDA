using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEAgenteRecaudo : Paginacion
    {
        public string OWNER { get; set; }
        public decimal COLL_OFF_ID { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal DAD_ID { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal ROL_ID { get; set; }
        public DateTime START { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        //----------------
        public string BPS_NAME { get; set; }
        public string OFF_NAME { get; set; }
        public string RECAUDADOR { get; set; }
        public string DIVISION { get; set; }
        public string ROL { get; set; }
        public string F_INICIAL { get; set; }
        public string F_FINAL { get; set; }
        public List<BEAgenteRecaudo> ListarAgenteRecaudo { get; set; }
        public List<BEObservationGral> ListaObservacion { get; set; }
        public List<BEDireccion> ListaDireccion { get; set; }
        //-----------------------------------------------------------------
        public decimal DIV_RiGHTS_ID { get; set; }
        public string MOG_ID { get; set; }
        public decimal ID_COLL_DIV { get; set; }
        public string ROL_DESC { get; set; }
    }
}
