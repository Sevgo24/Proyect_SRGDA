using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace SGRDA.Entities
{
    public partial class BECaracteristicaPredefinida
    {
        public List<BECaracteristicaPredefinida> CaracteristicaPredefinida { get; set; }
        public BECaracteristicaPredefinida() { }

        public BECaracteristicaPredefinida(IDataReader Reader)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            CHAR_TYPES_ID = Convert.ToDecimal(Reader["CHAR_TYPES_ID"]);
            CHAR_ID = Convert.ToDecimal(Reader["CHAR_ID"]);
            EST_ID = Convert.ToDecimal(Reader["EST_ID"]);
            TIPO = Convert.ToString(Reader["TIPO"]);
            SUBE_ID = Convert.ToDecimal(Reader["SUBE_ID"]);
            SUBTIPO = Convert.ToString(Reader["SUBTIPO"]);
            CHAR_SHORT = Convert.ToString(Reader["CHAR_SHORT"]);
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

        public BECaracteristicaPredefinida(IDataReader Reader, int flag)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            CHAR_TYPES_ID = Convert.ToDecimal(Reader["CHAR_TYPES_ID"]);
            CHAR_ID = Convert.ToDecimal(Reader["CHAR_ID"]);
            EST_ID = Convert.ToDecimal(Reader["EST_ID"]);
            TIPO = Convert.ToString(Reader["TIPO"]);
            SUBE_ID = Convert.ToDecimal(Reader["SUBE_ID"]);
            SUBTIPO = Convert.ToString(Reader["SUBTIPO"]);
            CHAR_SHORT = Convert.ToString(Reader["CHAR_SHORT"]);

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
