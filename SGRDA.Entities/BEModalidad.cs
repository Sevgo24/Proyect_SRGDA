using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEModalidad:Paginacion
    {
        public IList<BEModalidad> ListarModalidad;
        public BEModalidad()
        {
            ListarModalidad = new List<BEModalidad>();
        }
        public string OWNER { get; set; }
        public decimal MOD_ID { get; set; }
        public string MOD_DEC { get; set; }
        public string MOD_ORIG { get; set; }
        public string MOD_SOC { get; set; }
        public string CLASS_COD { get; set; }
        public string MOG_ID { get; set; }
        public string RIGHT_COD { get; set; }
        public string MOD_INCID { get; set; }
        public string MOD_USAGE { get; set; }
        public string MOD_REPER { get; set; }
        public decimal RATE_ID { get; set; }
        public decimal? MOD_COM { get; set; }
        public decimal? MOD_DISCA { get; set; }
        public decimal? MOD_DISCS { get; set; }
        public decimal? MOD_DISCC { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string MOD_OBS { get; set; }
        public string ESTADO { get; set; }

        public string TIPO_DERECHO { get; set; }
        public string TIPO_CREACION { get; set; }
        public string ORIGEN { get; set; }
        public string INCIDENCIA { get; set; }
        public string MODALIDAD { get; set; }
        public string TIPO_OBRA { get; set; }
        public string TIPO_SOCIEDAD { get; set; }
        public string USO_REPERTORIO { get; set; }

        public string ddlTipCre { get; set; }
        

        ///
        /// Descripcion de campos de la Modalidad
        /// Origen ---> MOD_ODESC
        /// sociedad ---> MOG_SDESC
        /// clases de creacion ---> CLASS_DESC
        /// grupo modalidad ---> MOG_DESC
        /// tipos de derecho ---> RIGHT_DESC
        /// incidencia ---> MOD_IDESC
        /// frecuencia de uso ---> MOD_DUSAGE
        /// uso repertorio ---> MOD_DREPER
        /// uso repertorio ---> MOG_SOC
        ///
        public string MOD_ODESC { get; set; }
        public string MOG_SDESC { get; set; }
        public string CLASS_DESC { get; set; }
        public string MOG_DESC { get; set; }
        public string RIGHT_DESC { get; set; }
        public string MOD_IDESC { get; set; }
        public string MOD_DUSAGE { get; set; }
        public string MOD_DREPER { get; set; }
        public string MOG_SOC { get; set; }
        public decimal WRFK_ID { get; set; }
        public string SAP_CODIGO { get; set; }

    }
}
