using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using SGRDA.Entities;
namespace SGRDA.DA
{
    public class DAREC_BANKS_GRAL
    {
        private Database db = new DatabaseProviderFactory().Create("conexion");

        public List<BEREC_BANKS_GRAL> Get_REC_BANKS_GRAL()
        {
            List<BEREC_BANKS_GRAL> lst = new List<BEREC_BANKS_GRAL>();
            BEREC_BANKS_GRAL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_BANKS_GRAL"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        if (dr.FieldCount > 0)
                        {
                            //lst.Add(new BEREC_BANKS_GRAL() { BNK_ID = 0, BNK_NAME = "<--Seleccione-->" });

                            while (dr.Read())
                            {
                                item = new BEREC_BANKS_GRAL();
                                item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                                item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                                item.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                                item.BNK_C_BRANCH = dr.GetDecimal(dr.GetOrdinal("BNK_C_BRANCH"));
                                item.BNK_C_DC = dr.GetDecimal(dr.GetOrdinal("BNK_C_DC"));
                                item.BNK_C_ACCOUNT = dr.GetDecimal(dr.GetOrdinal("BNK_C_ACCOUNT"));
                                item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                                lst.Add(item);
                            }
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

        public List<BEREC_BANKS_GRAL> REC_BANKS_GRAL_GET_by_BNK_ID(string OWNER, decimal BNK_ID)
        {
            List<BEREC_BANKS_GRAL> lst = new List<BEREC_BANKS_GRAL>();
            BEREC_BANKS_GRAL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_BANKS_GRAL_GET_by_BNK_ID"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);
                    db.AddInParameter(cm, "@BNK_ID", DbType.Decimal, BNK_ID);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_BANKS_GRAL();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                            item.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                            item.BNK_C_BRANCH = dr.GetDecimal(dr.GetOrdinal("BNK_C_BRANCH"));
                            item.BNK_C_DC = dr.GetDecimal(dr.GetOrdinal("BNK_C_DC"));
                            item.BNK_C_ACCOUNT = dr.GetDecimal(dr.GetOrdinal("BNK_C_ACCOUNT"));
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

        public List<BEREC_BANKS_GRAL> USP_GET_DAREC_BANKS_GRAL_PAGE(string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BANKS_GRAL_GET_Page");
            db.AddInParameter(oDbCommand, "@param", DbType.String, param);
            //db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_BANKS_GRAL>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_BANKS_GRAL(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEREC_BANKS_BRANCH> LISTAR_SUCURSAL_X_BANCO_PAGE(string owner, decimal id, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_SUCURSAL_X_BANCO_PAGE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            DbCommand oDbCommand1 = db.GetStoredProcCommand("SGRDASS_LISTAR_SUCURSAL_X_BANCO_PAGE", owner, id, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_BANKS_BRANCH>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREC_BANKS_BRANCH(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEREC_BANKS_BPS> LISTAR_CONTACTOS_X_BANCO_PAGE(string owner, decimal id, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CONTACTOS_X_BANCO_PAGE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            DbCommand oDbCommand1 = db.GetStoredProcCommand("SGRDASS_LISTAR_CONTACTOS_X_BANCO_PAGE", owner, id, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_BANKS_BPS>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand1))
            {
                while (dr.Read())
                {
                    BEREC_BANKS_BPS item = new BEREC_BANKS_BPS();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                    item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                    item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    item.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                    {
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                    {
                        item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
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

                    lista.Add(item);
                }
                //lista.Add(new BEREC_BANKS_BPS(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_BANKS_GRAL_Ins(BEREC_BANKS_GRAL en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BANKS_GRAL_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                //db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, en.BNK_ID);
                db.AddInParameter(oDbCommand, "@BNK_NAME", DbType.String, en.BNK_NAME.ToUpper());
                db.AddInParameter(oDbCommand, "@BNK_C_BRANCH", DbType.Decimal, en.BNK_C_BRANCH);
                db.AddInParameter(oDbCommand, "@BNK_C_DC", DbType.Decimal, en.BNK_C_DC);
                db.AddInParameter(oDbCommand, "@BNK_C_ACCOUNT", DbType.Decimal, en.BNK_C_ACCOUNT);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BANKS_GRAL_Upd(BEREC_BANKS_GRAL en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BANKS_GRAL_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, en.BNK_ID);
                db.AddInParameter(oDbCommand, "@BNK_NAME", DbType.String, en.BNK_NAME.ToUpper());
                db.AddInParameter(oDbCommand, "@BNK_C_BRANCH", DbType.Decimal, en.BNK_C_BRANCH);
                db.AddInParameter(oDbCommand, "@BNK_C_DC", DbType.Decimal, en.BNK_C_DC);
                db.AddInParameter(oDbCommand, "@BNK_C_ACCOUNT", DbType.Decimal, en.BNK_C_ACCOUNT);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BANKS_GRAL_Del(decimal BNK_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BANKS_GRAL_Del");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, BNK_ID);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ObtenerLongitudCodigoSucursal(string owner, decimal id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_lONGITUD_IDSUC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, id);
            return db.ExecuteScalar(oDbCommand).ToString();
        }
    }
}