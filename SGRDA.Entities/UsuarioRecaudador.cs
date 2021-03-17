using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class UsuarioRecaudador : Paginacion
    {
        /// <summary>
        /// Empresa
        /// </summary>
        public string OWNER { get; set; }
        public decimal BPS_ID { get; set; }

        public decimal? OFF_ID { get; set; }
        public decimal? COLL_LEVEL { get; set; }
        public string CUR_ALPHA { get; set; }
        public DateTime? AGR_DATE { get; set; }
        public DateTime? LAST_SET { get; set; }
        public DateTime? LAST_EXP { get; set; }
        public DateTime? LAST_SET_TOT { get; set; }

        public string ACC_ID { get; set; }
        public DateTime? ENDS { get; set; }

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
        public List<BEHistorico> Historicos { get; set; }
        /// <summary>
        /// addon db 20140727
        /// </summary>
        public string Activo { get; set; }
    }
}