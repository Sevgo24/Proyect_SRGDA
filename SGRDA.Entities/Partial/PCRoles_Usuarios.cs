using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class Roles_Usuarios
    {
        public List<Roles_Usuarios> Rol_Usuario { get; set; }
        public Roles_Usuarios() { }

        public Roles_Usuarios(IDataReader Reader)
        {
            USUA_ICODIGO_USUARIO = Convert.ToInt32(Reader["USUA_ICODIGO_USUARIO"]);
            NOMBRE_COMPLETO = Convert.ToString(Reader["NOMBRE_COMPLETO"]);
            ROL_ICODIGO_ROL = Convert.ToInt32(Reader["ROL_ICODIGO_ROL"]);
            ROL_VNOMBRE_ROL = Convert.ToString(Reader["ROL_VNOMBRE_ROL"]);
            USRO_ACTIVO = Convert.ToString(Reader["activo_usuario"]);

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

        public Roles_Usuarios(IDataReader Reader, int flag)
        {
            USUA_ICODIGO_USUARIO = Convert.ToInt32(Reader["USUA_ICODIGO_USUARIO"]);
            NOMBRE_COMPLETO = Convert.ToString(Reader["NOMBRE_COMPLETO"]);
            ROL_ICODIGO_ROL = Convert.ToInt32(Reader["ROL_ICODIGO_ROL"]);
            ROL_VNOMBRE_ROL = Convert.ToString(Reader["ROL_VNOMBRE_ROL"]);
            USRO_ACTIVO = Convert.ToString(Reader["activo_usuario"]);

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
