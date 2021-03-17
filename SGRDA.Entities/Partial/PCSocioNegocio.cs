using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class SocioNegocio 
    {
        public List<SocioNegocio> Socio_Negocio { get; set; }
        public SocioNegocio() { }

        public SocioNegocio(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            ENT_TYPE = Convert.ToChar(Reader["ENT_TYPE"]);
            ENT_TYPE_NOMBRE = Convert.ToString(Reader["ENT_TYPE_NOMBRE"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);
            BPS_FIRST_NAME = Convert.ToString(Reader["BPS_FIRST_NAME"]);
            BPS_FATH_SURNAME = Convert.ToString(Reader["BPS_FATH_SURNAME"]);
            BPS_MOTH_SURNAME = Convert.ToString(Reader["BPS_MOTH_SURNAME"]);
            TAXT_ID = Convert.ToDecimal(Reader["TAXT_ID"]);
            TAXN_NAME = Convert.ToString(Reader["TAXN_NAME"]);
            TAX_ID = Convert.ToString(Reader["TAX_ID"]);
            BPS_USER = Convert.ToChar(Reader["USUARIO"]);
            BPS_COLLECTOR = Convert.ToChar(Reader["BPS_COLLECTOR"]);
            BPS_ASSOCIATION = Convert.ToChar(Reader["BPS_ASSOCIATION"]);
            BPS_EMPLOYEE = Convert.ToChar(Reader["BPS_EMPLOYEE"]);
            BPS_GROUP = Convert.ToChar(Reader["BPS_GROUP"]);
            BPS_SUPPLIER = Convert.ToChar(Reader["BPS_SUPPLIER"]);
            BPS_INT_N = Convert.ToString(Reader["BPS_INT_N"]);

            if (!DBNull.Value.Equals(Reader["BPS_INT"]))
            {
                BPS_INT = Convert.ToDateTime(Reader["BPS_INT"]);
            }
            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
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
        }

        public SocioNegocio(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            ENT_TYPE = Convert.ToChar(Reader["ENT_TYPE"]);
            ENT_TYPE_NOMBRE = Convert.ToString(Reader["ENT_TYPE_NOMBRE"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);
            BPS_FIRST_NAME = Convert.ToString(Reader["BPS_FIRST_NAME"]);
            BPS_FATH_SURNAME = Convert.ToString(Reader["BPS_FATH_SURNAME"]);
            BPS_MOTH_SURNAME = Convert.ToString(Reader["BPS_MOTH_SURNAME"]);
            TAXT_ID = Convert.ToDecimal(Reader["TAXT_ID"]);
            TAXN_NAME = Convert.ToString(Reader["TAXN_NAME"]);
            TAX_ID = Convert.ToString(Reader["TAX_ID"]);
            //if (!DBNull.Value.Equals(Reader["BPS_INT"]))
            //{
            //    BPS_INT = Convert.ToDateTime(Reader["BPS_INT"]);
            //}
            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                ACTIVO = "I";
            }else{
                ACTIVO = "A";
            }
            BPS_USER = Convert.ToChar(Reader["BPS_USER"]);
            BPS_COLLECTOR = Convert.ToChar(Reader["BPS_COLLECTOR"]);
            BPS_ASSOCIATION = Convert.ToChar(Reader["BPS_ASSOCIATION"]);
            BPS_EMPLOYEE = Convert.ToChar(Reader["BPS_EMPLOYEE"]);
            BPS_GROUP = Convert.ToChar(Reader["BPS_GROUP"]);
            BPS_SUPPLIER = Convert.ToChar(Reader["BPS_SUPPLIER"]);
            //BPS_INT_N = Convert.ToString(Reader["BPS_INT_N"]);
            //if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            //{
            //    LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            //}

            //if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            //{
            //    LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            //}

            //LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            //LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);

            TotalVirtual = flag;
        }
    }
}
