using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEClaseCreacion
    {
        public List<BEClaseCreacion> ClaseCreacion { get; set; }
        public BEClaseCreacion() { }

        public BEClaseCreacion(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            RIGHT_COD = Convert.ToString(Reader["RIGHT_COD"]);
            RIGHT_DESC = Convert.ToString(Reader["RIGHT_DESC"]);
            CLASS_COD = Convert.ToString(Reader["CLASS_COD"]);
            CLASS_COD = Convert.ToString(Reader["CLASS_COD"]);
            CLASS_DESC = Convert.ToString(Reader["CLASS_DESC"]);
            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
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
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BEClaseCreacion(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            //RIGHT_COD = Convert.ToString(Reader["RIGHT_COD"]);
            //RIGHT_DESC = Convert.ToString(Reader["RIGHT_DESC"]);
            CLASS_COD = Convert.ToString(Reader["CLASS_COD"]);
            CLASS_DESC = Convert.ToString(Reader["CLASS_DESC"]);

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
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
            TotalVirtual = flag;
        }
    }
}
