using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class SocioNegocio : Paginacion
    {
        /// <summary>
        /// Empresa
        /// </summary>
        public string OWNER { get; set; }
        public decimal BPS_ID { get; set; }
        public char ENT_TYPE { get; set; }
        public string ENT_TYPE_NOMBRE { get; set; }

        /// <summary>
        /// Razon Social
        /// </summary>
        public string BPS_NAME { get; set; }

        /// <summary>
        /// Nombsres
        /// </summary>
        public string BPS_FIRST_NAME { get; set; }
        public string BPS_FATH_SURNAME { get; set; }
        public string BPS_MOTH_SURNAME { get; set; }
        /// <summary>
        /// Tipo de Documento-  RUC DNI
        /// </summary>
        public decimal TAXT_ID { get; set; }
        public string TAXN_NAME { get; set; }
        /// <summary>
        /// Valor del Tipo de documento
        /// 
        public string TAX_ID { get; set; }
        public char BPS_USER { get; set; }
        public char BPS_COLLECTOR { get; set; }
        public char BPS_ASSOCIATION { get; set; }
        public char BPS_EMPLOYEE { get; set; }
        public char BPS_GROUP { get; set; }
        public char BPS_SUPPLIER { get; set; }
        public string BPS_INT_N { get; set; }
        public Nullable<DateTime> BPS_INT { get; set; }
        public Nullable<DateTime> ENDS { get; set; }

        public string BPS_DESC { get; set; }

        public string BPS_TRADE_NAME { get; set; }

        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }

        public List<BEObservationGral> Observaciones { get; set; }
        public List<BEParametroGral> Parametros { get; set; }
        public List<BEDocumentoGral> Documentos { get; set; }
        public List<BEDireccion> Direcciones { get; set; }

        public List<BETelefono> Telefonos { get; set; }
        public List<BECorreo> Correos { get; set; }
        public List<BERedes_Sociales> RedSocial { get; set; }
        public List<BEAsociado> Asociados { get; set; }
        /// <summary>
        /// addon db 20140727
        /// </summary>
        public string ACTIVO { get; set; }

        /// <summary>
        /// Valores Divisiones
        /// </summary>
        public decimal DAD_ID { get; set; }
        public string  DAD_NAME { get; set; }
        public Boolean BPS_VERIFICADO { get; set; }
        public string VERIFICADO { get; set; }
        public string LOG_USER_VERIFICADO { get; set; }
        public DateTime LOG_DATE_VERIFICADO { get; set; }
    }
} 