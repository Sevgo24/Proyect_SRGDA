using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class USUARIOS
    {

        public List<USUARIOS> USUARIO { get; set; }
        public USUARIOS() { }

        public USUARIOS(IDataReader Reader)
        {
            USUA_ICODIGO_USUARIO = Convert.ToInt32(Reader["USUA_ICODIGO_USUARIO"]);
            USUA_VNOMBRE_USUARIO = Convert.ToString(Reader["USUA_VNOMBRE_USUARIO"]);
            USUA_VAPELLIDO_PATERNO_USUARIO = Convert.ToString(Reader["USUA_VAPELLIDO_PATERNO_USUARIO"]);
            USUA_VAPELLIDO_MATERNO_USUARIO = Convert.ToString(Reader["USUA_VAPELLIDO_MATERNO_USUARIO"]);
            USUA_VUSUARIO_RED_USUARIO = Convert.ToString(Reader["USUA_VUSUARIO_RED_USUARIO"]);
            USUA_VPASSWORD_USUARIO = Convert.ToString(Reader["USUA_VPASSWORD_USUARIO"]);
            USUA_CACTIVO_USUARIO = Convert.ToBoolean(Reader["USUA_CACTIVO_USUARIO"]);

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

        public USUARIOS(IDataReader Reader, int flag)
        {
            USUA_ICODIGO_USUARIO = Convert.ToInt32(Reader["USUA_ICODIGO_USUARIO"]);
            USUA_VNOMBRE_USUARIO = Convert.ToString(Reader["USUA_VNOMBRE_USUARIO"]);
            USUA_VAPELLIDO_PATERNO_USUARIO = Convert.ToString(Reader["USUA_VAPELLIDO_PATERNO_USUARIO"]);
            USUA_VAPELLIDO_MATERNO_USUARIO = Convert.ToString(Reader["USUA_VAPELLIDO_MATERNO_USUARIO"]);
            USUA_VUSUARIO_RED_USUARIO = Convert.ToString(Reader["USUA_VUSUARIO_RED_USUARIO"]);
            USUA_VPASSWORD_USUARIO = Convert.ToString(Reader["USUA_VPASSWORD_USUARIO"]);
            USUA_CACTIVO_USUARIO = Convert.ToBoolean(Reader["USUA_CACTIVO_USUARIO"]);

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
