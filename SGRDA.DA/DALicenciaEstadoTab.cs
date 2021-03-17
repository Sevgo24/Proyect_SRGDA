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
    public class DALicenciaEstadoTab
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BELicenciaEstadoTab> ListarTabEstadoLicencia(string owner, decimal Id)
        {
            List<BELicenciaEstadoTab> lista = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TAB_LICENCIA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                //oDataBase.AddInParameter(cm, "@LICS_ID", DbType.Decimal, Id);
                oDataBase.AddInParameter(cm, "@WORKF_SID", DbType.Decimal, Id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                    BELicenciaEstadoTab item = null;
                    lista = new List<BELicenciaEstadoTab>();
                    while (dr.Read())
                    {
                        item = new BELicenciaEstadoTab();
                        item.SECUENCIA = dr.GetDecimal(dr.GetOrdinal("SECUENCIA"));
                        item.TAB_ID = dr.GetDecimal(dr.GetOrdinal("TAB_ID"));
                        //item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                        item.WORKF_SID = dr.GetDecimal(dr.GetOrdinal("WORKF_SID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        lista.Add(item);

                    }
                }
            }
            return lista;
        }

        public BELicenciaEstadoTab ObtenerEstadoLicenciaTab(string owner, decimal tabId, decimal liscId)
        {
            BELicenciaEstadoTab item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ESTADOLICENCIATAB"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@TAB_ID", DbType.Decimal, tabId);
                //oDataBase.AddInParameter(cm, "@LICS_ID", DbType.Decimal, liscId);
                oDataBase.AddInParameter(cm, "@WORKF_SID", DbType.Decimal, liscId);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELicenciaEstadoTab();
                        if (!dr.IsDBNull(dr.GetOrdinal("SECUENCIA")))
                        {
                            item.SECUENCIA = dr.GetDecimal(dr.GetOrdinal("SECUENCIA"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("TAB_ID")))
                        {
                            item.TAB_ID = dr.GetDecimal(dr.GetOrdinal("TAB_ID"));
                        }

                        //if (!dr.IsDBNull(dr.GetOrdinal("LICS_ID")))
                        //{
                        //    item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                        //}

                        if (!dr.IsDBNull(dr.GetOrdinal("WORKF_SID")))
                        {
                            item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("WORKF_SID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
                return item;
            }
        }

        public int Insertar(BELicenciaEstadoTab en)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_ESTADOLICENCIATAB");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddOutParameter(oDbComand, "@SECUENCIA", DbType.Decimal, Convert.ToInt32(en.SECUENCIA));
            oDataBase.AddInParameter(oDbComand, "@TAB_ID", DbType.Decimal, en.TAB_ID);
            //oDataBase.AddInParameter(oDbComand, "@LICS_ID", DbType.Decimal, en.LICS_ID);
            oDataBase.AddInParameter(oDbComand, "@WORKF_SID", DbType.Decimal, en.WORKF_SID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbComand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbComand, "@SECUENCIA"));
            return id;
        }

        public int Actualizar(BELicenciaEstadoTab en)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_ESTADOLICENCIATAB");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbComand, "@SECUENCIA", DbType.Decimal, en.SECUENCIA);
            oDataBase.AddInParameter(oDbComand, "@TAB_ID", DbType.Decimal, en.TAB_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            return oDataBase.ExecuteNonQuery(oDbComand);
        }

        public int Activar(string owner, decimal secuencia, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_ESTADOLICENCIATAB");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@SECUENCIA", DbType.Decimal, secuencia);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal secuencia, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_INACTIVAR_ESTADOLICENCIATAB");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@SECUENCIA", DbType.Decimal, secuencia);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
