using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DAUSUARIOS
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<USUARIOS> USUARIOS_spBuscarLogin(string usua_vusuario_red_usuario, string usua_vpassword_usuario)
        {
            USUARIOS be = null;
            List<USUARIOS> lista = new List<USUARIOS>();

            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USUARIOS_spBuscarLogin");
            oDataBase.AddInParameter(oDbCommand, "@usua_vusuario_red_usuario", DbType.String, usua_vusuario_red_usuario);
            oDataBase.AddInParameter(oDbCommand, "@USUA_VPASSWORD_USUARIO", DbType.String, usua_vpassword_usuario);
            oDataBase.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new USUARIOS();
                    be.ROL_ICODIGO_ROL = reader.GetInt32(reader.GetOrdinal("codigo_usuario"));
                    be.NOMBRE_COMPLETO_USUARIO = reader.GetString(reader.GetOrdinal("nombre_Completo"));
                    be.NOMBRE_ROL = reader.GetString(reader.GetOrdinal("nombre_rol"));
                    be.USUA_CACTIVO_USUARIO = reader.GetBoolean(reader.GetOrdinal("activo_usuario"));

                    lista.Add(be);
                }
            }
            return lista;
        }

        public List<USUARIOS> USUARIOS_spBuscar(int usua_icodigo_usuario, string usua_vnombre_usuario, string usua_vapellido_paterno_usuario, string usua_vapellido_materno_usuario, char usua_cactivo_usuario)
        {
            USUARIOS be = null;
            List<USUARIOS> lista = new List<USUARIOS>();

            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USUARIOS_spBuscar");
            oDataBase.AddInParameter(oDbCommand, "@usua_icodigo_usuario", DbType.Int32, usua_icodigo_usuario);
            oDataBase.AddInParameter(oDbCommand, "@usua_vnombre_usuario", DbType.String, usua_vnombre_usuario);
            oDataBase.AddInParameter(oDbCommand, "@usua_vapellido_paterno_usuario", DbType.String, usua_vapellido_paterno_usuario);
            oDataBase.AddInParameter(oDbCommand, "@usua_vapellido_materno_usuario", DbType.String, usua_vapellido_materno_usuario);
            oDataBase.AddInParameter(oDbCommand, "@usua_cactivo_usuario", DbType.Boolean, usua_cactivo_usuario);
            oDataBase.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new USUARIOS();
                    //be.USUA_ICODIGO_USUARIO = reader.GetInt32(reader.GetOrdinal("codigo_usuario"));
                    be.NOMBRE_COMPLETO_USUARIO = reader.GetString(reader.GetOrdinal("nombre_Completo"));
                    be.USUA_VUSUARIO_RED_USUARIO = reader.GetString(reader.GetOrdinal("usuario_red"));
                    be.NOMBRE_ROL = reader.GetString(reader.GetOrdinal("rol_nombre"));
                    be.USUA_CACTIVO_USUARIO = reader.GetBoolean(reader.GetOrdinal("activo_usuario"));

                    lista.Add(be);
                }
            }
            return lista;
        }

        public List<USUARIOS> usp_listar_Usuarios_by_codigo(int usua_icodigo_usuario)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_listar_Usuarios_by_codigo", usua_icodigo_usuario);
            var lista = new List<USUARIOS>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new USUARIOS(reader));
            }
            return lista;
        }

        public int usp_Upd_Usuarios(USUARIOS user)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USP_UPDATE_USUARIOS");
            oDataBase.AddInParameter(oDbCommand, "@USUA_ICODIGO_USUARIO", DbType.Int32, user.USUA_ICODIGO_USUARIO);
            oDataBase.AddInParameter(oDbCommand, "@USUA_VNOMBRE_USUARIO", DbType.String, user.USUA_VNOMBRE_USUARIO);
            oDataBase.AddInParameter(oDbCommand, "@USUA_VAPELLIDO_PATERNO_USUARIO", DbType.String, user.USUA_VAPELLIDO_PATERNO_USUARIO);
            oDataBase.AddInParameter(oDbCommand, "@USUA_VAPELLIDO_MATERNO_USUARIO", DbType.String, user.USUA_VAPELLIDO_MATERNO_USUARIO);
            oDataBase.AddInParameter(oDbCommand, "@USUA_VUSUARIO_RED_USUARIO", DbType.String, user.USUA_VUSUARIO_RED_USUARIO);
            oDataBase.AddInParameter(oDbCommand, "@USUA_VPASSWORD_USUARIO", DbType.String, user.USUA_VPASSWORD_USUARIO);
            oDataBase.AddInParameter(oDbCommand, "@ROL_ICODIGO_ROL", DbType.Int32, user.ROL_ICODIGO_ROL);
            oDataBase.AddInParameter(oDbCommand, "@USUA_CACTIVO_USUARIO",DbType.Boolean, user.USUA_CACTIVO_USUARIO);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);

            return r;
        }

        public int usp_Ins_Usuarios(USUARIOS user)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("USP_INSERT_USUARIOS");
                oDatabase.AddInParameter(oDbCommand, "@USUA_ICODIGO_USUARIO", DbType.Int32, user.USUA_ICODIGO_USUARIO);
                oDatabase.AddInParameter(oDbCommand, "@USUA_VNOMBRE_USUARIO", DbType.String, user.USUA_VNOMBRE_USUARIO);
                oDatabase.AddInParameter(oDbCommand, "@USUA_VAPELLIDO_PATERNO_USUARIO", DbType.String, user.USUA_VAPELLIDO_PATERNO_USUARIO);
                oDatabase.AddInParameter(oDbCommand, "@USUA_VAPELLIDO_MATERNO_USUARIO", DbType.String, user.USUA_VAPELLIDO_MATERNO_USUARIO);
                oDatabase.AddInParameter(oDbCommand, "@USUA_VUSUARIO_RED_USUARIO", DbType.String, user.USUA_VUSUARIO_RED_USUARIO);
                oDatabase.AddInParameter(oDbCommand, "@USUA_VPASSWORD_USUARIO", DbType.String, user.USUA_VPASSWORD_USUARIO);
                oDatabase.AddInParameter(oDbCommand, "@ROL_ICODIGO_ROL", DbType.Int32, user.ROL_ICODIGO_ROL);
                oDatabase.AddInParameter(oDbCommand, "@USUA_CACTIVO_USUARIO", DbType.Boolean, user.USUA_CACTIVO_USUARIO);

                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int usp_Upd_estado_Usuarios(USUARIOS user)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("USP_UPDATE_ESTADO_USUARIOS");
                oDatabase.AddInParameter(oDbCommand, "@usua_icodigo_usuario", DbType.Int32, user.USUA_ICODIGO_USUARIO);
                oDatabase.AddInParameter(oDbCommand, "@usua_cactivo_usuario", DbType.Boolean, user.USUA_CACTIVO_USUARIO);
                oDatabase.AddInParameter(oDbCommand, "@log_user_update", DbType.String, user.LOG_USER_UPDATE);
                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public List<USUARIOS> usp_Get_UsuariosPage(string usuario_red, string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_Get_UsuariosPage");
            oDataBase.AddInParameter(oDbCommand, "@usuario_red", DbType.String, usuario_red);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_Get_UsuariosPage", usuario_red, param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<USUARIOS>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new USUARIOS(reader, Convert.ToInt32(results)));


            }
            return lista;
        }
    }
}
