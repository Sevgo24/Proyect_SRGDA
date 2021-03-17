using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEEstadoLicencia
    {
        public List<BEEstadoLicencia> EstadoLicencia { get; set; }
        public BEEstadoLicencia() { }

        public BEEstadoLicencia(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            LICS_ID = Convert.ToDecimal(Reader["LICS_ID"]);
            LICS_NAME = Convert.ToString(Reader["LICS_NAME"]);
            LICS_INI = Convert.ToChar(Reader["LICS_INI"]);
            LICS_END = Convert.ToChar(Reader["LICS_END"]);
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

        public BEEstadoLicencia(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            LICS_ID = Convert.ToDecimal(Reader["LICS_ID"]);
            LICS_NAME = Convert.ToString(Reader["LICS_NAME"]);
            LICS_INI = Convert.ToChar(Reader["LICS_INI"]);
            LICS_END = Convert.ToChar(Reader["LICS_END"]);

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
