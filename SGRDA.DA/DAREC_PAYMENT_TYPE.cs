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
    public class DAREC_PAYMENT_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_PAYMENT_TYPE> Get_REC_PAYMENT_TYPE()
        {
            List<BEREC_PAYMENT_TYPE> lst = new List<BEREC_PAYMENT_TYPE>();
            BEREC_PAYMENT_TYPE item = null;
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_PAYMENT_TYPE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_PAYMENT_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.PAY_BANK = dr.GetBoolean(dr.GetOrdinal("PAY_BANK"));
                            item.PAY_BANK_RECEIPT = dr.GetBoolean(dr.GetOrdinal("PAY_BANK_RECEIPT"));
                            item.PAY_AGE_RECEIPT = dr.GetBoolean(dr.GetOrdinal("PAY_AGE_RECEIPT"));
                            item.PAY_TRANSFER = dr.GetBoolean(dr.GetOrdinal("PAY_TRANSFER"));
                            item.PAY_DATE_FIX = dr.GetBoolean(dr.GetOrdinal("PAY_DATE_FIX"));
                            if (!dr.IsDBNull(dr.GetOrdinal("PAY_DATE_FIX_DAY")))
                            {
                                item.PAY_DATE_FIX_DAY = dr.GetDecimal(dr.GetOrdinal("PAY_DATE_FIX_DAY"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("VTO1")))
                            {
                                item.VTO1 = dr.GetDecimal(dr.GetOrdinal("VTO1"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("VTO2")))
                            {
                                item.VTO2 = dr.GetDecimal(dr.GetOrdinal("VTO2"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("VTO3")))
                            {
                                item.VTO3 = dr.GetDecimal(dr.GetOrdinal("VTO3"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("VTO4")))
                            {
                                item.VTO4 = dr.GetDecimal(dr.GetOrdinal("VTO4"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("VTO5")))
                            {
                                item.VTO5 = dr.GetDecimal(dr.GetOrdinal("VTO5"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("VTO6")))
                            {
                                item.VTO6 = dr.GetDecimal(dr.GetOrdinal("VTO6"));
                            }
                            lst.Add(item);
                        }
                    }
                }
            return lst;
        }

        public List<BEREC_PAYMENT_TYPE> REC_PAYMENT_TYPE_by_PAY_ID(string PAY_ID)
        {
            List<BEREC_PAYMENT_TYPE> lst = new List<BEREC_PAYMENT_TYPE>();
            BEREC_PAYMENT_TYPE item = null;
            using (DbCommand cm = db.GetStoredProcCommand("usp_REC_PAYMENT_TYPE_GET_by_PAY_ID"))
            {
                db.AddInParameter(cm, "@PAY_ID", DbType.String, PAY_ID);
                db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_PAYMENT_TYPE();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));
                        item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        item.PAY_BANK = dr.GetBoolean(dr.GetOrdinal("PAY_BANK"));
                        item.PAY_BANK_RECEIPT = dr.GetBoolean(dr.GetOrdinal("PAY_BANK_RECEIPT"));
                        item.PAY_AGE_RECEIPT = dr.GetBoolean(dr.GetOrdinal("PAY_AGE_RECEIPT"));
                        item.PAY_TRANSFER = dr.GetBoolean(dr.GetOrdinal("PAY_TRANSFER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PAY_DATE_FIX")))
                        {
                            item.PAY_DATE_FIX = dr.GetBoolean(dr.GetOrdinal("PAY_DATE_FIX"));
                        }
                        //item.PAY_DATE_FIX = dr.GetBoolean(dr.GetOrdinal("PAY_DATE_FIX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PAY_DATE_FIX_DAY")))
                        {
                            item.PAY_DATE_FIX_DAY = dr.GetDecimal(dr.GetOrdinal("PAY_DATE_FIX_DAY"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("VTO1")))
                        {
                            item.VTO1 = dr.GetDecimal(dr.GetOrdinal("VTO1"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("VTO2")))
                        {
                            item.VTO2 = dr.GetDecimal(dr.GetOrdinal("VTO2"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("VTO3")))
                        {
                            item.VTO3 = dr.GetDecimal(dr.GetOrdinal("VTO3"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("VTO4")))
                        {
                            item.VTO4 = dr.GetDecimal(dr.GetOrdinal("VTO4"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("VTO5")))
                        {
                            item.VTO5 = dr.GetDecimal(dr.GetOrdinal("VTO5"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("VTO6")))
                        {
                            item.VTO6 = dr.GetDecimal(dr.GetOrdinal("VTO6"));
                        }
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public List<BEREC_PAYMENT_TYPE> REC_PAYMENT_TYPE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_PAYMENT_TYPE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_PAYMENT_TYPE>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_PAYMENT_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public int ValidacionFormaPago(string owner, string id, string descripcion)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_VALIDACION_FORMA_PAG");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@PAY_ID", DbType.String, id);
            db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, descripcion);
            return Convert.ToInt32(db.ExecuteScalar(oDbCommand));
        }

        public bool REC_PAYMENT_TYPE_Ins(BEREC_PAYMENT_TYPE en)
        {
            bool exito = false;
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_PAYMENT_TYPE_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@PAY_ID", DbType.String, en.PAY_ID.ToUpper());
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
                db.AddInParameter(oDbCommand, "@PAY_BANK", DbType.Boolean, en.PAY_BANK);
                db.AddInParameter(oDbCommand, "@PAY_BANK_RECEIPT", DbType.Boolean, en.PAY_BANK_RECEIPT);
                db.AddInParameter(oDbCommand, "@PAY_AGE_RECEIPT", DbType.Boolean, en.PAY_AGE_RECEIPT);
                db.AddInParameter(oDbCommand, "@PAY_TRANSFER", DbType.Boolean, en.PAY_TRANSFER);
                db.AddInParameter(oDbCommand, "@PAY_DATE_FIX", DbType.Boolean, en.PAY_DATE_FIX);

                db.AddInParameter(oDbCommand, "@PAY_DATE_FIX_DAY", DbType.Decimal, en.PAY_DATE_FIX_DAY);

                db.AddInParameter(oDbCommand, "@VTO1", DbType.Decimal, en.VTO1);
                db.AddInParameter(oDbCommand, "@VTO2", DbType.Decimal, en.VTO2);
                db.AddInParameter(oDbCommand, "@VTO3", DbType.Decimal, en.VTO3);
                db.AddInParameter(oDbCommand, "@VTO4", DbType.Decimal, en.VTO4);
                db.AddInParameter(oDbCommand, "@VTO5", DbType.Decimal, en.VTO5);
                db.AddInParameter(oDbCommand, "@VTO6", DbType.Decimal, en.VTO6);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
        }

        public bool REC_PAYMENT_TYPE_Upd(BEREC_PAYMENT_TYPE en)
        {
            bool exito = false;
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_PAYMENT_TYPE_Upd");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@PAY_ID", DbType.String, en.PAY_ID.ToUpper());
            db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
            db.AddInParameter(oDbCommand, "@PAY_DATE_FIX", DbType.Boolean, en.PAY_DATE_FIX);
            db.AddInParameter(oDbCommand, "@PAY_DATE_FIX_DAY", DbType.Decimal, en.PAY_DATE_FIX_DAY);
            db.AddInParameter(oDbCommand, "@VTO1", DbType.Decimal, en.VTO1);
            db.AddInParameter(oDbCommand, "@VTO2", DbType.Decimal, en.VTO2);
            db.AddInParameter(oDbCommand, "@VTO3", DbType.Decimal, en.VTO3);
            db.AddInParameter(oDbCommand, "@VTO4", DbType.Decimal, en.VTO4);
            db.AddInParameter(oDbCommand, "@VTO5", DbType.Decimal, en.VTO5);
            db.AddInParameter(oDbCommand, "@VTO6", DbType.Decimal, en.VTO6);
            db.AddInParameter(oDbCommand, "@PAY_BANK", DbType.Boolean, en.PAY_BANK);
            db.AddInParameter(oDbCommand, "@PAY_BANK_RECEIPT", DbType.Boolean, en.PAY_BANK_RECEIPT);
            db.AddInParameter(oDbCommand, "@PAY_AGE_RECEIPT", DbType.Boolean, en.PAY_AGE_RECEIPT);
            db.AddInParameter(oDbCommand, "@PAY_TRANSFER", DbType.Boolean, en.PAY_TRANSFER);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }

        public bool REC_PAYMENT_TYPE_Del(string PAY_ID)
        {
            bool exito = false;
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_PAYMENT_TYPE_Del");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@PAY_ID", DbType.String, PAY_ID);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
        }


        public List<BEREC_PAYMENT_TYPE> ListarTipo(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("USP_REC_PAYMENT_TYPE_LISTITEM");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEREC_PAYMENT_TYPE>();
            BEREC_PAYMENT_TYPE obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BEREC_PAYMENT_TYPE();
                    obs.PAY_ID = Convert.ToString(reader["VALUE"]);
                    obs.DESCRIPTION = Convert.ToString(reader["TEXT"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public int Eliminar(BEREC_PAYMENT_TYPE del)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_PAYMENT_TYPE_Del");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PAY_ID", DbType.String, del.PAY_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
