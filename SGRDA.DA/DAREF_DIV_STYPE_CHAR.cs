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
    public class DAREF_DIV_STYPE_CHAR
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_DIV_STYPE_CHAR> usp_Get_REF_DIV_STYPE_CHAR()
        {
            List<BEREF_DIV_STYPE_CHAR> lst = new List<BEREF_DIV_STYPE_CHAR>();
            BEREF_DIV_STYPE_CHAR item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_STYPE_CHAR_GET"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIV_STYPE_CHAR();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DAC_ID = dr.GetString(dr.GetOrdinal("DAC_ID"));
                            item.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));

                            item.DAD_TNAME = dr.GetString(dr.GetOrdinal("DAD_TNAME"));
                            item.DAD_SNAME = dr.GetString(dr.GetOrdinal("DAD_SNAME"));

                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDATE = (item.LOG_USER_UPDATE == null) ? string.Empty : item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

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

        public List<BEREF_DIV_STYPE_CHAR> usp_REF_DIV_STYPE_CHAR_GET_by_Patametros(string DAC_ID, string DAD_TYPE, string DAD_STYPE)
        {
            List<BEREF_DIV_STYPE_CHAR> lst = new List<BEREF_DIV_STYPE_CHAR>();
            BEREF_DIV_STYPE_CHAR item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_STYPE_CHAR_GET_by_Patametros"))
                {
                    db.AddInParameter(cm, "@DAC_ID", DbType.String, DAC_ID);
                    db.AddInParameter(cm, "@DAD_TYPE", DbType.String, DAD_TYPE);
                    db.AddInParameter(cm, "@DAD_STYPE", DbType.String, DAD_STYPE);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIV_STYPE_CHAR();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DAC_ID = dr.GetString(dr.GetOrdinal("DAC_ID"));
                            item.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
                            item.DAD_STYPE =Convert.ToString( dr.GetDecimal(dr.GetOrdinal("DAD_STYPE")));
                            item.DAD_TNAME = dr.GetString(dr.GetOrdinal("DAD_TNAME"));
                            item.DAD_SNAME = dr.GetString(dr.GetOrdinal("DAD_SNAME"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
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

        public List<BEREF_DIV_STYPE_CHAR> usp_REF_DIV_STYPE_CHAR_Page(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_DIV_STYPE_CHAR_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REF_DIV_STYPE_CHAR_GET_Page    ", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREF_DIV_STYPE_CHAR>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREF_DIV_STYPE_CHAR(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REF_DIV_STYPE_CHAR_Ins(BEREF_DIV_STYPE_CHAR en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_STYPE_CHAR_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, en.DAD_TYPE);
                db.AddInParameter(oDbCommand, "@DAC_ID", DbType.String, en.DAC_ID);
                db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.String, en.DAD_STYPE);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_DIV_STYPE_CHAR_Upd(BEREF_DIV_STYPE_CHAR en, string auxDAC_ID, string auxDAD_TYPE, string auxDAD_STYPE)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_STYPE_CHAR_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, en.DAD_TYPE);
                db.AddInParameter(oDbCommand, "@DAC_ID", DbType.String, en.DAC_ID);
                db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.Decimal, en.DAD_STYPE);

                db.AddInParameter(oDbCommand, "@auxDAD_TYPE", DbType.String, auxDAD_TYPE);
                db.AddInParameter(oDbCommand, "@auxDAC_ID", DbType.String, auxDAC_ID);
                db.AddInParameter(oDbCommand, "@auxDAD_STYPE", DbType.Decimal, auxDAD_STYPE);

                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_DIV_STYPE_CHAR_Del(string DAC_ID, string DAD_TYPE, string DAD_STYPE)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_STYPE_CHAR_Del");
                db.AddInParameter(oDbCommand, "@DAC_ID", DbType.String, DAC_ID);
                db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, DAD_TYPE);
                db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.String, DAD_STYPE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
