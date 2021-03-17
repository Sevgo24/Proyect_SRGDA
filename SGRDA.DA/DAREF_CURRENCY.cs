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
    public class DAREF_CURRENCY
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public BEREF_CURRENCY ObtenerMoneda(string owner, string id)
        {
            BEREF_CURRENCY item = null;

            using (DbCommand cm = db.GetStoredProcCommand("usp_REF_CURRENCY_GET_by_CUR_ALPHA"))
            {
                //db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, id);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREF_CURRENCY();
                        item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                        item.CUR_DESC = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                        item.CUR_NUM = dr.GetDecimal(dr.GetOrdinal("CUR_NUM"));
                        item.UNIT_MAJOR = dr.GetString(dr.GetOrdinal("UNIT_MAJOR"));
                        item.UNIT_MINOR = dr.GetString(dr.GetOrdinal("UNIT_MINOR"));
                        item.DECIMALS = dr.GetDecimal(dr.GetOrdinal("DECIMALS"));
                        item.FORMAT = dr.GetString(dr.GetOrdinal("FORMAT"));
                    }
                }
            }
            return item;
        }
        public List<BEREF_CURRENCY> usp_Get_REF_CURRENCY()
        {
            List<BEREF_CURRENCY> lst = new List<BEREF_CURRENCY>();
            BEREF_CURRENCY item;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REF_CURRENCY"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_CURRENCY();
                            item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                            item.CUR_DESC = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                            item.CUR_NUM = dr.GetDecimal(dr.GetOrdinal("CUR_NUM"));
                            item.UNIT_MAJOR = dr.GetString(dr.GetOrdinal("UNIT_MAJOR"));
                            item.UNIT_MINOR = dr.GetString(dr.GetOrdinal("UNIT_MINOR"));
                            item.DECIMALS = dr.GetDecimal(dr.GetOrdinal("DECIMALS"));
                            item.FORMAT = dr.GetString(dr.GetOrdinal("FORMAT"));
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

        public List<BEREF_CURRENCY> REF_CURRENCY_by_CUR_ALPHA(string CUR_ALPHA)
        {
            List<BEREF_CURRENCY> lst = new List<BEREF_CURRENCY>();
            BEREF_CURRENCY item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_CURRENCY_GET_by_CUR_ALPHA"))
                {
                    db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, CUR_ALPHA);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_CURRENCY();
                            item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                            item.CUR_DESC = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                            item.CUR_NUM = dr.GetDecimal(dr.GetOrdinal("CUR_NUM"));
                            item.UNIT_MAJOR = dr.GetString(dr.GetOrdinal("UNIT_MAJOR"));
                            item.UNIT_MINOR = dr.GetString(dr.GetOrdinal("UNIT_MINOR"));
                            item.DECIMALS = dr.GetDecimal(dr.GetOrdinal("DECIMALS"));
                            item.FORMAT = dr.GetString(dr.GetOrdinal("FORMAT"));
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

        public List<BEREF_CURRENCY> REF_CURRENCY_Page(string param, int st, int? pagina, int? cantRegxPag)
        {
            List<BEREF_CURRENCY> lista = new List<BEREF_CURRENCY>();
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_CURRENCY_GET_Page");
            db.AddInParameter(oDbCommand, "@param", DbType.String, param);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REF_CURRENCY_GET_Page", param, st, pagina, cantRegxPag, ParameterDirection.Output);

            //using (IDataReader reader = db.ExecuteReader(oDbCommand))
            //{
            //    while (reader.Read())
            //        lista.Add(new BEREF_CURRENCY(reader, Convert.ToInt32(results)));
            //}


            var item = new BEREF_CURRENCY();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    item = new BEREF_CURRENCY();
                    item.CUR_ALPHA = reader.GetString(reader.GetOrdinal("CUR_ALPHA"));
                    item.CUR_DESC = reader.GetString(reader.GetOrdinal("CUR_DESC"));
                    item.CUR_NUM = reader.GetDecimal(reader.GetOrdinal("CUR_NUM"));
                    item.FORMAT = reader.GetString(reader.GetOrdinal("FORMAT"));

                    if (reader.IsDBNull(reader.GetOrdinal("ENDS")))
                        item.ACTIVO = "ACTIVO";
                    else
                        item.ACTIVO = "INACTIVO";

                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }

            return lista;
        }

        public bool REF_CURRENCY_Ins(BEREF_CURRENCY en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_CURRENCY_Ins");
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, en.CUR_ALPHA.ToUpper());
                db.AddInParameter(oDbCommand, "@CUR_DESC", DbType.String, en.CUR_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@CUR_NUM", DbType.Decimal, en.CUR_NUM);
                db.AddInParameter(oDbCommand, "@UNIT_MAJOR", DbType.String, en.UNIT_MAJOR.ToUpper());
                db.AddInParameter(oDbCommand, "@UNIT_MINOR", DbType.String, en.UNIT_MINOR.ToUpper());
                db.AddInParameter(oDbCommand, "@DECIMALS", DbType.Decimal, en.DECIMALS);
                db.AddInParameter(oDbCommand, "@FORMAT", DbType.String, en.FORMAT.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_CURRENCY_Upd(BEREF_CURRENCY en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_CURRENCY_Upd");
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, en.CUR_ALPHA.ToUpper());
                db.AddInParameter(oDbCommand, "@CUR_DESC", DbType.String, en.CUR_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@CUR_NUM", DbType.Decimal, en.CUR_NUM);
                db.AddInParameter(oDbCommand, "@UNIT_MAJOR", DbType.String, en.UNIT_MAJOR.ToUpper());
                db.AddInParameter(oDbCommand, "@UNIT_MINOR", DbType.String, en.UNIT_MINOR.ToUpper());
                db.AddInParameter(oDbCommand, "@DECIMALS", DbType.Decimal, en.DECIMALS);
                db.AddInParameter(oDbCommand, "@FORMAT", DbType.String, en.FORMAT.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_CURRENCY_Del(string CUR_ALPHA)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_CURRENCY_Del");
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, CUR_ALPHA);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREF_CURRENCY> ListarTipoMoneda(string owner)
        {
            List<BEREF_CURRENCY> lst = new List<BEREF_CURRENCY>();
            BEREF_CURRENCY item;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_MONEDA"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREF_CURRENCY();
                        item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                        item.CUR_DESC = dr.GetString(dr.GetOrdinal("CUR_DESC")).ToUpper();
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }
}

