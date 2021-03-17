using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEBloqueos
    {
        public List<BEBloqueos> Bloqueos { get; set; }
        public BEBloqueos() { }

        public BEBloqueos(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BLOCK_ID = Convert.ToDecimal(Reader["BLOCK_ID"]);
            BLOCK_DESC = Convert.ToString(Reader["BLOCK_DESC"]);
            BLOCK_PULL = Convert.ToChar(Reader["BLOCK_PULL"]);
            BLOCK_AUT = Convert.ToChar(Reader["BLOCK_AUT"]);
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

        public BEBloqueos(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BLOCK_ID = Convert.ToDecimal(Reader["BLOCK_ID"]);
            BLOCK_DESC = Convert.ToString(Reader["BLOCK_DESC"]);
            BLOCK_PULL = Convert.ToChar(Reader["BLOCK_PULL"]);
            BLOCK_AUT = Convert.ToChar(Reader["BLOCK_AUT"]);

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
