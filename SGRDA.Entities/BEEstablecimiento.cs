using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEEstablecimiento : Paginacion
    {
        public string OWNER { get; set; }
        public string EST_NAME { get; set; }
        public decimal EST_ID { get; set; }
        public decimal DAD_ID { get; set; }
        public decimal ESTT_ID { get; set; }
        public decimal SUBE_ID { get; set; }
        public decimal DIF_ID { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public string TAXN_NAME { get; set; }
        public string TAX_ID { get; set; }
        public string BPS_NAME { get; set; }
        public string BPS_FIRST_NAME { get; set; }
        public string BPS_FATH_SURNAME { get; set; }
        public string BPS_MOTH_SURNAME { get; set; }

        public string EST_TYPE { get; set; }
        public string EST_SUBTYPE { get; set; }
        public string ADDRESS { get; set; }
        public string DIVISION { get; set; }
        public decimal TAXT_ID { get; set; }
        public decimal? TAXD_ID { get; set; }

        public decimal BPS_ID { get; set; }
        public string ROL_ID { get; set; }

        public string MAIN_ADD { get; set; }

        public string Activo { get; set; }

        public List<BEObservationGral> Observaciones { get; set; }
        public List<BEParametroGral> Parametros { get; set; }
        public List<BECaracteristicaEst> Caracteristicas { get; set; }//no tiene caracteristicas generales
        public List<BEDireccion> Direccion { get; set; }
        public List<BEDocumentoGral> Documentos { get; set; }
        public List<BEInspectionEst> Inspection { get; set; }
        public List<BEAsociadosEst> Asociados { get; set; }
        public List<BELicencias> Licencias { get; set; }
        public List<BEDivisionesEst> Divisiones { get; set; }
        public List<BEDifusionEst> Difusion { get; set; }
        public List<BETelefono> Telefonos { get; set; }
        public List<BECorreo> Correos { get; set; }
        public List<BERedes_Sociales> RedSocial { get; set; }

        public string DAD_NAME { get; set; }
        public string DAD_TNAME { get; set; }
        public string UBIGEO { get; set; }
        public decimal TIS_N { get; set; }
        public decimal GEO_ID { get; set; }
        public string LATITUD { get; set; }
        public string LONGITUD { get; set; }

    }
}