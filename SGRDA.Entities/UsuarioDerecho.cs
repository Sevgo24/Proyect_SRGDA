using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class UsuarioDerecho : Paginacion
    {
        /// <summary>
        /// Empresa
        /// </summary>
        public string OWNER { get; set; }
        public decimal BPS_ID { get; set; }
         
        public string PAY_ID  { get; set; }
        public decimal? BPS_GROUP{ get; set; }
        public decimal? BPS_DIS_PER{ get; set; }
        public decimal? BPS_DIS_AMO{ get; set; }

        public string BPS_DIS_REA{ get; set; }
        public string ACC_ID { get; set; }

       
        public  DateTime? ENDS { get; set; }

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
        public List<BEAsociado> Asociados { get; set; }
        public List<BERedes_Sociales> RedSocial { get; set; }

        /// <summary>
        /// addon db 20140727
        /// </summary>
        public string Activo { get; set; }


        /// <summary>
        /// addon db 20150521
        /// </summary>
        public string BPS_PARTIDA { get; set; }
        public string BPS_ZONA { get; set; }
        public string BPS_SEDE { get; set; }

    }
} 