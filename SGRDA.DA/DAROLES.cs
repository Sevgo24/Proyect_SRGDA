using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities;
using System.Configuration;
namespace SGRDA.DA
{
    public class DAROLES
    {
        public List<ROLES> usp_listar_Roles()
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_listar_Roles");
            var lista = new List<ROLES>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new ROLES(reader));
            }
            return lista;
        }


        public List<ROLES> usp_listar_Roles_by_codigo(int cod)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_listar_Roles_by_codigo",cod);
            var lista = new List<ROLES>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new ROLES(reader));
            }
            return lista;
        }

        public int usp_Upd_Roles(ROLES rol)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_Upd_Roles");
            oDataBase.AddInParameter(oDbCommand, "@rol_icodigo_rol", DbType.Int32, rol.ROL_ICODIGO_ROL);
            oDataBase.AddInParameter(oDbCommand, "@rol_vnombre_rol", DbType.String, rol.ROL_VNOMBRE_ROL);
            oDataBase.AddInParameter(oDbCommand, "@rol_vdescripcion_rol", DbType.String, rol.ROL_VDESCRIPCION_ROL);
            oDataBase.AddInParameter(oDbCommand, "@rol_cactivo_rol", DbType.String, rol.ROL_CACTIVO_ROL);
            oDataBase.AddInParameter(oDbCommand, "@log_user_update", DbType.String, rol.LOG_USER_UPDATE);

           int r= oDataBase.ExecuteNonQuery(oDbCommand);
            
            return r;
        }

        public int usp_Ins_Roles(ROLES rol)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("usp_Ins_Roles");
                oDatabase.AddInParameter(oDbCommand, "@rol_vnombre_rol", DbType.String, rol.ROL_VNOMBRE_ROL);
                oDatabase.AddInParameter(oDbCommand, "@rol_vdescripcion_rol", DbType.String, rol.ROL_VDESCRIPCION_ROL);
                oDatabase.AddInParameter(oDbCommand, "@rol_cactivo_rol", DbType.String, rol.ROL_CACTIVO_ROL);
                oDatabase.AddInParameter(oDbCommand, "@log_user_creat", DbType.String, rol.LOG_USER_CREAT);

                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {

                return 0;
            }
            
        }

        public int usp_Upd_estado_Roles(ROLES rol)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("usp_Upd_estado_Roles");
                oDatabase.AddInParameter(oDbCommand, "@rol_icodigo_rol", DbType.Int32, rol.ROL_ICODIGO_ROL);
                oDatabase.AddInParameter(oDbCommand, "@rol_cactivo_rol", DbType.String, rol.ROL_CACTIVO_ROL);
                oDatabase.AddInParameter(oDbCommand, "@log_user_update", DbType.String, rol.LOG_USER_UPDATE);
                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public List<ROLES> usp_Get_RolesPage(string param,int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_Get_RolesPage");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString( oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
                       
            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_Get_RolesPage", param,pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<ROLES>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new ROLES(reader, Convert.ToInt32(results)));        
            }
            return lista;
        }
    }
}
