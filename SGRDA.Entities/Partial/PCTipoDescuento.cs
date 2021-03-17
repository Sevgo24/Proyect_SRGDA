using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace SGRDA.Entities
{
    public partial class BETipoDescuento
    {
        public List<BETipoDescuento> TipoDescuento { get; set; }
        public BETipoDescuento() { }

        public BETipoDescuento(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DISC_TYPE = Convert.ToDecimal(Reader["DISC_TYPE"]);
            DISC_TYPE_NAME = Convert.ToString(Reader["DISC_TYPE_NAME"]);
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

        public BETipoDescuento(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DISC_TYPE = Convert.ToDecimal(Reader["DISC_TYPE"]);
            DISC_TYPE_NAME = Convert.ToString(Reader["DISC_TYPE_NAME"]);

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
