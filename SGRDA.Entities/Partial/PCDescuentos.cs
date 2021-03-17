using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEDescuentos
    {
        public List<BEDescuentos> Descuentos { get; set; }
        public BEDescuentos() { }

        public BEDescuentos(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ORDEN = Convert.ToInt32(Reader["ORDEN"]);
            ORIGEN = Convert.ToString(Reader["ORIGEN"]);
            FORMATO = Convert.ToString(Reader["FORMATO"]);
            DISC_ID = Convert.ToDecimal(Reader["DISC_ID"]);
            DISC_TYPE = Convert.ToDecimal(Reader["DISC_TYPE"]);
            TIPO = Convert.ToString(Reader["TIPO"]);
            DISC_NAME = Convert.ToString(Reader["DISC_NAME"]);
            DISC_SIGN = Convert.ToString(Reader["DISC_SIGN"]);
            DISC_PERC = Convert.ToDecimal(Reader["DISC_PERC"]);
            DISC_VALUE = Convert.ToDecimal(Reader["DISC_VALUE"]);
            DISC_AUT = Convert.ToChar(Reader["DISC_AUT"]);
            DISC_ACC = Convert. ToDecimal(Reader["DISC_ACC"]);
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
            BASE = Convert.ToInt32(Reader["BASE"]);
        }

        public BEDescuentos(IDataReader Reader, int flag)
        {
            DISC_ID = Convert.ToDecimal(Reader["DISC_ID"]);
            TIPO = Convert.ToString(Reader["TIPO"]);
            DISC_NAME = Convert.ToString(Reader["DISC_NAME"]);
            DISC_PERC = Convert.ToDecimal(Reader["DISC_PERC"]);
            DISC_VALUE = Convert.ToDecimal(Reader["DISC_VALUE"]);

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
