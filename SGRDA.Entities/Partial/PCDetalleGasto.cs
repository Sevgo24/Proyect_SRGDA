using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEDetalleGasto
    {
        public List<BEDetalleGasto> DetalleGasto { get; set; }
        public BEDetalleGasto() { }

        public BEDetalleGasto(IDataReader Reader)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            EXP_ID = Convert.ToString(Reader["EXP_ID"]);
            EXP_VAL_PRE = Convert.ToDecimal(Reader["EXP_VAL_PRE"]);
            EXP_VAL_APR = Convert.ToDecimal(Reader["EXP_VAL_APR"]);
            EXP_VAL_CON = Convert.ToDecimal(Reader["EXP_VAL_CON"]);
            LEG_ID = Convert.ToDecimal(Reader["LEG_ID"]);

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
            ESTADO = Convert.ToString(Reader["ESTADO"]);
        }

        public BEDetalleGasto(IDataReader Reader, int flag)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            EXP_ID = Convert.ToString(Reader["EXP_ID"]);
            EXP_VAL_PRE = Convert.ToDecimal(Reader["EXP_VAL_PRE"]);
            EXP_VAL_APR = Convert.ToDecimal(Reader["EXP_VAL_APR"]);
            EXP_VAL_CON = Convert.ToDecimal(Reader["EXP_VAL_CON"]);
            LEG_ID = Convert.ToDecimal(Reader["LEG_ID"]);

            //if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            //{
            //    LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            //}

            //if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            //{
            //    LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            //}

            //LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            //LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDAT"]);
            ESTADO = Convert.ToString(Reader["ESTADO"]);

            TotalVirtual = flag;
        }
    }
}
