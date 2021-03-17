using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BETipoCalificador
    {
        public List<BETipoCalificador> TipoCalificador { get; set; }
        public BETipoCalificador() { }

        public BETipoCalificador(IDataReader Reader)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            QUA_ID = Convert.ToDecimal(Reader["QUA_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
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
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BETipoCalificador(IDataReader Reader, int flag)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            QUA_ID = Convert.ToDecimal(Reader["QUA_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                ACTIVO = "I";
            }
            else
            {
                ACTIVO = "A";
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
