using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEGrupoFacturacion
    {
        public List<BEGrupoFacturacion> GrupoFacturacion { get; set; }
        public BEGrupoFacturacion() { }

        public BEGrupoFacturacion(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            INVG_ID = Convert.ToDecimal(Reader["INVG_ID"]);
            INVG_DESC = Convert.ToString(Reader["INVG_DESC"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);
            MOG_ID = Convert.ToString(Reader["MOG_ID"]);
            GRUPO = Convert.ToString(Reader["GRUPO"]);
            ENDS = Convert.ToDateTime(Reader["ENDS"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);
        }

        public BEGrupoFacturacion(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            INVG_ID = Convert.ToDecimal(Reader["INVG_ID"]);
            INVG_DESC = Convert.ToString(Reader["INVG_DESC"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);
            MOG_ID = Convert.ToString(Reader["MOG_ID"]);
            GRUPO = Convert.ToString(Reader["GRUPO"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                Activo = "I";
            }
            else
            {
                Activo = "A";
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);

            TotalVirtual = flag;
        }
    }
}
