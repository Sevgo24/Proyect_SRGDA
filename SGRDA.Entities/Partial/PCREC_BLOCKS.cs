using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_BLOCKS
    {
        public List<BEREC_BLOCKS> RECBLOCKS { get; set; }
        public BEREC_BLOCKS() { }

        public BEREC_BLOCKS(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BLOCK_ID = Convert.ToDecimal(Reader["BLOCK_ID"]);
            BLOCK_DESC = Convert.ToString(Reader["BLOCK_DESC"]);
            BLOCK_PULL = Convert.ToString(Reader["BLOCK_PULL"]);
            BLOCK_AUT = Convert.ToString(Reader["BLOCK_AUT"]);

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

        public BEREC_BLOCKS(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BLOCK_ID = Convert.ToDecimal(Reader["BLOCK_ID"]);
            BLOCK_DESC = Convert.ToString(Reader["BLOCK_DESC"]);
            BLOCK_PULL = Convert.ToString(Reader["BLOCK_PULL"]);
            BLOCK_AUT = Convert.ToString(Reader["BLOCK_AUT"]);

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
