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
    public class DAROLES_PERMISOS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<ROLES_PERMISOS> usp_Get_RolesPermisosPage(int rol, int mod, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_Get_RolesPermisosPage");
            oDataBase.AddInParameter(oDbCommand, "@rol", DbType.Int32, rol);
            oDataBase.AddInParameter(oDbCommand, "@modulo", DbType.Int32, mod);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_Get_RolesPermisosPage", rol, mod, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<ROLES_PERMISOS>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new ROLES_PERMISOS(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<ROLES_PERMISOS> usp_Get_RolesPermisos(int ROL_ICODIGO_ROL, int CABE_ICODIGO_MODULO)
        {
            List<ROLES_PERMISOS> lst = new List<ROLES_PERMISOS>();
            ROLES_PERMISOS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("ROLES_PERMISOS_spBuscar"))
                {
                    db.AddInParameter(cm, "ROL_ICODIGO_ROL", DbType.Int32, ROL_ICODIGO_ROL);
                    db.AddInParameter(cm, "CABE_ICODIGO_MODULO", DbType.Int32, CABE_ICODIGO_MODULO);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new ROLES_PERMISOS();
                            item.ROL_ICODIGO_ROL = dr.GetInt32(dr.GetOrdinal("ROL_ICODIGO_ROL"));
                            item.ROL_VDESCRIPCION_ROL = dr.GetString(dr.GetOrdinal("ROL_VDESCRIPCION_ROL"));
                            item.MODU_VNOMBRE_MODULO = dr.GetString(dr.GetOrdinal("MODU_VNOMBRE_MODULO"));
                            item.MODU_VRUTA_PAGINA = dr.GetString(dr.GetOrdinal("MODU_VRUTA_PAGINA"));
                            item.ROMO_CACTIVO = dr.GetString(dr.GetOrdinal("ROMO_CACTIVO"));

                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public int usp_Ins_Roles_Permisos(ROLES_PERMISOS permisos)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("USP_INSERT_ROLES_PERMISOS");
                oDatabase.AddInParameter(oDbCommand, "@MODU_ICODIGO_MODULO", DbType.Int32, permisos.MODU_ICODIGO_MODULO);
                oDatabase.AddInParameter(oDbCommand, "@ROL_ICODIGO_ROL", DbType.Int32, permisos.ROL_ICODIGO_ROL);
                oDatabase.AddInParameter(oDbCommand, "@CABE_ICODIGO_MODULO", DbType.Int32, permisos.CABE_ICODIGO_MODULO);
                oDatabase.AddInParameter(oDbCommand, "@MODU_INIVEL_MODULO", DbType.Int32, permisos.MODU_INIVEL_MODULO);
                oDatabase.AddInParameter(oDbCommand, "@MODU_VNOMBRE_MODULO", DbType.String, permisos.MODU_VNOMBRE_MODULO);
                oDatabase.AddInParameter(oDbCommand, "@MODU_VRUTA_PAGINA", DbType.String, permisos.MODU_VRUTA_PAGINA);
                oDatabase.AddInParameter(oDbCommand, "@MODU_VDESCRIPCION_MODULO", DbType.String, permisos.MODU_VDESCRIPCION_MODULO);    
                oDatabase.AddInParameter(oDbCommand, "@MODU_CACTIVO_MODULO", DbType.String, permisos.ROMO_CACTIVO);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, permisos.LOG_USER_CREAT);

                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public int usp_Upd_RolesPermisos(ROLES_PERMISOS upd)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("USP_UPDATE_ROLES_PERMISOS");
                oDatabase.AddInParameter(oDbCommand, "@MODU_ICODIGO_MODULO", DbType.Int32, upd.MODU_ICODIGO_MODULO);
                oDatabase.AddInParameter(oDbCommand, "@ROL_ICODIGO_ROL", DbType.Int32, upd.ROL_ICODIGO_ROL);
                oDatabase.AddInParameter(oDbCommand, "@CABE_ICODIGO_MODULO", DbType.Int32, upd.CABE_ICODIGO_MODULO);
                oDatabase.AddInParameter(oDbCommand, "@MODU_VNOMBRE_MODULO", DbType.String, upd.MODU_VNOMBRE_MODULO);
                oDatabase.AddInParameter(oDbCommand, "@MODU_VRUTA_PAGINA", DbType.String, upd.MODU_VRUTA_PAGINA);
                oDatabase.AddInParameter(oDbCommand, "@MODU_VDESCRIPCION_MODULO", DbType.String, upd.MODU_VDESCRIPCION_MODULO);
                oDatabase.AddInParameter(oDbCommand, "@MODU_CACTIVO_MODULO", DbType.String, upd.ROMO_CACTIVO);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, upd.LOG_USER_CREAT);

                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public List<ROLES_PERMISOS> usp_listar_RolesPermisos_by_codigo(int id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_listar_RolesPermisos_by_codigo", id);
            var lista = new List<ROLES_PERMISOS>();


            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new ROLES_PERMISOS(reader));
            }
            return lista;
        }

        public List<ROLES_PERMISOS> usp_listarNivelModulo(int p)
        {
            try
            {
                ROLES_PERMISOS item = null;
                List<ROLES_PERMISOS> lista = new List<ROLES_PERMISOS>();
                DbCommand oDbCommand = db.GetStoredProcCommand("UPS_LISTAR_MODULO_NIVEL");
                db.AddInParameter(oDbCommand, "@NIVEL", DbType.Int32, p);
                db.ExecuteNonQuery(oDbCommand);

                using (IDataReader reader = db.ExecuteReader(oDbCommand))
                {
                    while (reader.Read())
                        //lista.Add(new ROLES_PERMISOS(reader));
                    {
                        item = new ROLES_PERMISOS();
                        item.MODU_INIVEL_MODULO = reader.GetInt32(reader.GetOrdinal("MODU_INIVEL_MODULO"));
                        item.MODU_VNOMBRE_MODULO = reader.GetString(reader.GetOrdinal("MODU_VNOMBRE_MODULO"));

                        lista.Add(item);
                    }

                }
            return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
