using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.WorkFlow
{
    public partial class WORKF_OBJECTS :Paginacion
    {
        public List<WORKF_OBJECTS> ListarObject { get; set; }
        public string OWNER { get; set; }
        public decimal WRKF_OID { get; set; }
        public string WRKF_OINTID { get; set; }
        public string WRKF_ODESC { get; set; }
        public string WRKF_OPATH { get; set; }
        public string WRKF_OPATH_JURIDICO { get; set; }        
        public string WRKF_OBODY { get; set; }
        public string WRKF_OSUBJECT { get; set; }
        public Nullable<decimal> WRKF_OTID { get; set; }
        public System.DateTime LOG_DATE_CREAT { get; set; }
        public Nullable<System.DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<System.DateTime> ENDS { get; set; }

        public string WRKF_OTDESC { get; set; }
        public string ESTADO { get; set; }


        /// <summary>
        /// Propiedad que carga los elementos del tipo asociado al objeto
        /// </summary>
        public WORKF_OBJECTS_TYPE TipoObjeto { get; set; }

        /// <summary>
        /// Codigo del tipo de documento.
        /// </summary>
        public decimal DOC_TYPE { get; set; }

    }
}
