using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class ROLES
    {
        public List<ROLES> ROL { get; set; }
        public ROLES() { }

        public ROLES(IDataReader Reader)
        {
            ROL_ICODIGO_ROL = Convert.ToInt32(Reader["ROL_ICODIGO_ROL"]);
            ROL_VNOMBRE_ROL = Convert.ToString(Reader["ROL_VNOMBRE_ROL"]);
            ROL_VDESCRIPCION_ROL = Convert.ToString(Reader["ROL_VDESCRIPCION_ROL"]);
            ROL_CACTIVO_ROL = Convert.ToString(Reader["ROL_CACTIVO_ROL"]);

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

        public ROLES(IDataReader Reader, int flag)
        {
            ROL_ICODIGO_ROL = Convert.ToInt32(Reader["ROL_ICODIGO_ROL"]);
            ROL_VNOMBRE_ROL = Convert.ToString(Reader["ROL_VNOMBRE_ROL"]);
            ROL_VDESCRIPCION_ROL = Convert.ToString(Reader["ROL_VDESCRIPCION_ROL"]);
            ROL_CACTIVO_ROL = Convert.ToString(Reader["ROL_CACTIVO_ROL"]);

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
