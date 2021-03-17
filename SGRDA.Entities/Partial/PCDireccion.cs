using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEDireccion
    {
        public List<BEDireccion> Direccion { get; set; }
        public BEDireccion() { }

        public BEDireccion(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ADD_ID = Convert.ToDecimal(Reader["ADD_ID"]);
            ADD_TYPE = Convert.ToDecimal(Reader["ADD_TYPE"]);
            ENT_ID = Convert.ToDecimal(Reader["ENT_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            GEO_ID = Convert.ToDecimal(Reader["GEO_ID"]);
            ROU_ID = Convert.ToDecimal(Reader["ROU_ID"]);
            ROU_NAME = Convert.ToString(Reader["ROU_NAME"]);
            ROU_NUM = Convert.ToString(Reader["ROU_NUM"]);
            HOU_TURZN = Convert.ToString(Reader["HOU_TURZN"]);
            HOU_URZN = Convert.ToString(Reader["HOU_URZN"]);
            HOU_NRO = Convert.ToString(Reader["HOU_NRO"]);
            HOU_MZ = Convert.ToString(Reader["HOU_MZ"]);
            HOU_LOT = Convert.ToString(Reader["HOU_LOT"]);
            HOU_TETP = Convert.ToString(Reader["HOU_TETP"]);
            HOU_NETP = Convert.ToString(Reader["HOU_NETP"]);
            ADD_TINT = Convert.ToString(Reader["ADD_TINT"]);
            ADD_INT = Convert.ToString(Reader["ADD_INT"]);
            ADD_ADDTL = Convert.ToString(Reader["ADD_ADDTL"]);
            ADD_REFER = Convert.ToString(Reader["ADD_REFER"]);
            ADDRESS = Convert.ToString(Reader["ADDRESS"]);
            CPO_ID = Convert.ToDecimal(Reader["CPO_ID"]);
            MAIN_ADD = Convert.ToChar(Reader["MAIN_ADD"]);
            ADD_CX = Convert.ToDecimal(Reader["ADD_CX"]);
            ADD_CY = Convert.ToDecimal(Reader["ADD_CY"]);
            REMARK = Convert.ToString(Reader["REMARK"]);
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

        public BEDireccion(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ADD_ID = Convert.ToDecimal(Reader["ADD_ID"]);
            ADD_TYPE = Convert.ToDecimal(Reader["ADD_TYPE"]);
            ENT_ID = Convert.ToDecimal(Reader["ENT_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            GEO_ID = Convert.ToDecimal(Reader["GEO_ID"]);
            ROU_ID = Convert.ToDecimal(Reader["ROU_ID"]);
            ROU_NAME = Convert.ToString(Reader["ROU_NAME"]);
            ROU_NUM = Convert.ToString(Reader["ROU_NUM"]);
            HOU_TURZN = Convert.ToString(Reader["HOU_TURZN"]);
            HOU_URZN = Convert.ToString(Reader["HOU_URZN"]);
            HOU_NRO = Convert.ToString(Reader["HOU_NRO"]);
            HOU_MZ = Convert.ToString(Reader["HOU_MZ"]);
            HOU_LOT = Convert.ToString(Reader["HOU_LOT"]);
            HOU_TETP = Convert.ToString(Reader["HOU_TETP"]);
            HOU_NETP = Convert.ToString(Reader["HOU_NETP"]);
            ADD_TINT = Convert.ToString(Reader["ADD_TINT"]);
            ADD_INT = Convert.ToString(Reader["ADD_INT"]);
            ADD_ADDTL = Convert.ToString(Reader["ADD_ADDTL"]);
            ADD_REFER = Convert.ToString(Reader["ADD_REFER"]);
            ADDRESS = Convert.ToString(Reader["ADDRESS"]);
            CPO_ID = Convert.ToDecimal(Reader["CPO_ID"]);
            MAIN_ADD = Convert.ToChar(Reader["MAIN_ADD"]);
            ADD_CX = Convert.ToDecimal(Reader["ADD_CX"]);
            ADD_CY = Convert.ToDecimal(Reader["ADD_CY"]);
            REMARK = Convert.ToString(Reader["REMARK"]);
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

            TotalVirtual = flag;
        }
    }
}
