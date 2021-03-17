using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities;

namespace SGRDA.DA
{
    public class DAREF_ROLES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_ROLES> Get_REF_ROLES()
        {
            List<BEREF_ROLES> lst = new List<BEREF_ROLES>();
            BEREF_ROLES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REF_ROLES"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_ROLES();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                            item.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
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

        public List<BEREF_ROLES> REF_ROLES_GET_by_ROL_ID(decimal ROL_ID)
        {
            List<BEREF_ROLES> lst = new List<BEREF_ROLES>();
            BEREF_ROLES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_ROLES_GET_by_ROL_ID"))
                {
                    db.AddInParameter(cm, "@ROL_ID", DbType.Decimal, ROL_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_ROLES();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                            item.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
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

        public List<BEREF_ROLES> REF_ROLES_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_ROLES_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REF_ROLES_GET_Page", param, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREF_ROLES>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREF_ROLES(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REF_ROLES_Ins(BEREF_ROLES en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_ROLES_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@ROL_DESC", DbType.String, en.ROL_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_ROLES_Upd(BEREF_ROLES en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_ROLES_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@ROL_ID", DbType.String, en.ROL_ID);
                db.AddInParameter(oDbCommand, "@ROL_DESC", DbType.String, en.ROL_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_ROLES_Del(decimal ROL_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_ROLES_Del");
                db.AddInParameter(oDbCommand, "@ROL_ID", DbType.Decimal, ROL_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Valida si el socio que se va a editar puede ser modificado por la oficina del usuario logeado
        /// </summary>
        /// <param name="idOficina"></param>
        /// <param name="idSocio"></param>
        /// <returns></returns>
        public bool TienePermiso(decimal idOficina, decimal idSocio)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDAR_ROL_SOCIO");
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idSocio);
            oDataBase.AddOutParameter(oDbCommand, "@HasAccess", DbType.Boolean, 4);
            oDataBase.ExecuteNonQuery(oDbCommand);

            return Convert.ToBoolean(oDataBase.GetParameterValue(oDbCommand, "@HasAccess"));
        }

       
        /// <summary>
        /// Valida si tiene permiso para editar al socio recaudador  por la oficina del usuario logeado
        /// </summary>
        /// <param name="idOficina"></param>
        /// <param name="idSocio"></param>
        /// <returns></returns>
        public bool TienePermisoUsuRec(decimal idOficina, decimal idSocio)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDAR_ROL_SOCIO_REC");
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idSocio);
            oDataBase.AddOutParameter(oDbCommand, "@HasAccess", DbType.Boolean, 4);
            oDataBase.ExecuteNonQuery(oDbCommand);

            return Convert.ToBoolean(oDataBase.GetParameterValue(oDbCommand, "@HasAccess"));
        }

        /// <summary>
        /// Valida si tiene permiso para editar una licencia  por la oficina del usuario logeado
        /// </summary>
        /// <param name="idOficinaLogin"></param>
        /// <param name="idLicenciaEdit"></param>
        /// <returns></returns>
        public bool TienePermisoEditarLic(decimal idOficinaLogin, decimal idLicenciaEdit)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDAR_PERMISO_LICENCIA_UPD");
            oDataBase.AddInParameter(oDbCommand, "@ID_OFICINA", DbType.Decimal, idOficinaLogin);
            oDataBase.AddInParameter(oDbCommand, "@ID_LIC", DbType.Decimal, idLicenciaEdit);
            oDataBase.AddOutParameter(oDbCommand, "@HasAccess", DbType.Boolean, 4);
            oDataBase.ExecuteNonQuery(oDbCommand);
             
            return Convert.ToBoolean(oDataBase.GetParameterValue(oDbCommand, "@HasAccess"));
        }

        /// <summary>
        /// Valida si tiene permiso para editar una licencia  por la oficina del usuario logeado
        /// </summary>
        /// <param name="idOficinaLogin"></param>
        /// <param name="idLicenciaEdit"></param>
        /// <returns></returns>
        public bool TienePermisoRegistrarLic(decimal idOficinaLogin, decimal idEstablecimientoSel)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDAR_PERMISO_LICENCIA_INS");
            oDataBase.AddInParameter(oDbCommand, "@ID_OFICINA", DbType.Decimal, idOficinaLogin);
            oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, idEstablecimientoSel);
            oDataBase.AddOutParameter(oDbCommand, "@HasAccess", DbType.Boolean, 4);
            oDataBase.ExecuteNonQuery(oDbCommand);

            return Convert.ToBoolean(oDataBase.GetParameterValue(oDbCommand, "@HasAccess"));
        }
         

    }
}
