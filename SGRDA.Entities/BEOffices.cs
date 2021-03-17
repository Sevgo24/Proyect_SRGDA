using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEOffices : Paginacion
    {
        public string OWNER { get; set; }
        public int OFF_ID { get; set; }
        public string OFF_NAME { get; set; }
        public string HQ_IND { get; set; }
        public int SOFF_ID { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public decimal ADD_ID { get; set; }
        public decimal OFF_TYPE { get; set; }
        public string OFF_CC { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string ADDRESS { get; set; }
        public string ENDSDES { get; set; }
        public int ID { get; set; }
        public int? ManagerID { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal NMR_ID { get; set; }
        public string Activo { get; set; }
        public string SOCIO { get; set; }
        public decimal OFF_ID_PRE { get; set; }

        public List<BEGrupoModalidadOficina> GrupoModalidad { get; set; }//
        //public List<BENumerador> Numeraciones { get; set; }
        public List<BEObservationGral> Observaciones { get; set; }
        public List<BEParametroGral> Parametros { get; set; }
        public List<BEDocumentoGral> Documentos { get; set; }
        public List<BEDireccion> Direcciones { get; set; }
        public List<BEOffices> Oficinas { get; set; }
        public List<BERecaudadorBps> Agentes { get; set; }
        public List<BESocioNegocioOficina> Contacto { get; set; }
        public List<BEDireccion> DireccionesHistorial { get; set; }       
        public List<BEDivisionRecaudador> DivisionAdm { get; set; }
        public List<BEOficinaDivisionModalidad> DivisionAdmGrupoModalidad { get; set; }
        public List<BENumeradorOficina> DivisionAdmNumerador { get; set; }
    }
}
