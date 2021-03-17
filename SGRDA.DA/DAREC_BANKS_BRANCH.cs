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
    public class DAREC_BANKS_BRANCH
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_BANKS_BRANCH> Get_REC_BANKS_BRANCH()
        {
            List<BEREC_BANKS_BRANCH> lst = new List<BEREC_BANKS_BRANCH>();
            BEREC_BANKS_BRANCH item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_BANKS_BRANCH"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_BANKS_BRANCH();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.BRCH_ID = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                            item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                            item.BRCH_NAME = dr.GetString(dr.GetOrdinal("BRCH_NAME"));
                            item.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
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

        public List<BEREC_BANKS_BRANCH> Obtiene(string OWNER, decimal BNK_ID, string BRCH_ID)
        {
            List<BEREC_BANKS_BRANCH> lst = new List<BEREC_BANKS_BRANCH>();
            BEREC_BANKS_BRANCH item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_BANKS_BRANCH_GET_by_BNK_ID_BRCH_ID"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);
                    db.AddInParameter(cm, "@BNK_ID", DbType.Decimal, BNK_ID);
                    db.AddInParameter(cm, "@BRCH_ID", DbType.String, BRCH_ID);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_BANKS_BRANCH();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.BRCH_ID = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                            item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                            item.BRCH_NAME = dr.GetString(dr.GetOrdinal("BRCH_NAME"));
                            item.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                            item.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
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

        public BEREC_BANKS_BRANCH cabeceraSucursal(string id, string owner)
        {
            BEREC_BANKS_BRANCH sucursal = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_BANKS_BRANCH_GET_by_BNK_ID_BRCH_ID"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    db.AddInParameter(cm, "@BRCH_ID", DbType.String, id);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            sucursal = new BEREC_BANKS_BRANCH();
                            sucursal.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            sucursal.BRCH_ID = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                            sucursal.auxBNK_ID = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                            sucursal.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                            sucursal.BRCH_NAME = dr.GetString(dr.GetOrdinal("BRCH_NAME"));
                            sucursal.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                            sucursal.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sucursal;
        }

        public List<BEREC_BANKS_BRANCH> REC_BANKS_BRANCH_Page(string owner, string param, decimal? idBanco, int st, int pagina, int cantRegxPag)
        {
            if (idBanco == null) idBanco = 0;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_BANKS_BRANCH_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, idBanco);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_BANKS_BRANCH>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_BANKS_BRANCH(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool InsertarSucursal(BEREC_BANKS_BRANCH en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BANKS_BRANCH_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, en.BNK_ID);
                db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, en.BRCH_ID);
                db.AddInParameter(oDbCommand, "@BRCH_NAME", DbType.String, en.BRCH_NAME);
                db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, en.ADD_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BANKS_BRANCH_Upd(BEREC_BANKS_BRANCH en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BANKS_BRANCH_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, en.BNK_ID);
                db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, en.BRCH_ID);
                db.AddInParameter(oDbCommand, "@BRCH_NAME", DbType.String, en.BRCH_NAME);
                db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, en.ADD_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BANKS_BRANCH_Del(BEREC_BANKS_BRANCH en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BANKS_BRANCH_Del");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, en.BNK_ID);
                db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, en.BRCH_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ObtenerBancoIdAntiguo(string owner, string id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_BANCOID_ANTERIOR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, id);
            return db.ExecuteScalar(oDbCommand).ToString();
        }

        public List<BEREC_BANKS_BRANCH> ListarSucursalesPorBanco(string owner, decimal IdBanco)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_SUCURSAL_X_BANCO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, IdBanco);
            var item = new BEREC_BANKS_BRANCH();
            var lista = new List<BEREC_BANKS_BRANCH>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEREC_BANKS_BRANCH();
                    item.BRCH_ID = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                    item.BRCH_NAME = dr.GetString(dr.GetOrdinal("BRCH_NAME"));
                    lista.Add(item);
                }
            }

            return lista;
        }

        public int ActualizarBanco(string owner, decimal IdBanco, decimal auxIdBanco)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_CUENTA_BANK");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, IdBanco);
            db.AddInParameter(oDbCommand, "@auxBNK_ID", DbType.Decimal, auxIdBanco);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEREC_BPS_BANKS_ACC> ListarCuentaBancaria(string owner, string sucbnkId)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_CUENTA_BANK");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            //oDataBase.AddInParameter(oDbComand, "@BNK_ID", DbType.Decimal, bnkId);
            oDataBase.AddInParameter(oDbComand, "@BRCH_ID", DbType.String, sucbnkId);
            //oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, idSocio);

            var lista = new List<BEREC_BPS_BANKS_ACC>();
            BEREC_BPS_BANKS_ACC item;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    item = new BEREC_BPS_BANKS_ACC();
                    item.BPS_ACC_ID = Convert.ToDecimal(reader["BPS_ACC_ID"]);
                    item.BACC_NUMBER = Convert.ToString(reader["BACC_NUMBER"]);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BEREC_BPS_BANKS_ACC> ListarCuentaBancariaXbanco(string owner, decimal bnkId, string moneda)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_CUENTA_X_BANK");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@BNK_ID", DbType.Decimal, bnkId);
            oDataBase.AddInParameter(oDbComand, "@MONEDA", DbType.String, moneda);

            var lista = new List<BEREC_BPS_BANKS_ACC>();
            BEREC_BPS_BANKS_ACC item;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    item = new BEREC_BPS_BANKS_ACC();
                    item.BPS_ACC_ID = Convert.ToDecimal(reader["BPS_ACC_ID"]);
                    item.BACC_NUMBER = Convert.ToString(reader["BACC_NUMBER"]);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BEREC_BANKS_BRANCH> ListarSucursalesPorNombre(string owner, decimal IdBanco, string agencia)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_SUCURSAL_X_BANCO_NOMBRE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, IdBanco);
            db.AddInParameter(oDbCommand, "@AGENCIA", DbType.String, agencia);
            var item = new BEREC_BANKS_BRANCH();
            var lista = new List<BEREC_BANKS_BRANCH>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEREC_BANKS_BRANCH();
                    item.ID = Convert.ToDecimal(dr.GetString(dr.GetOrdinal("BRCH_ID")));
                    item.BRCH_NAME = dr.GetString(dr.GetOrdinal("BRCH_NAME"));
                    lista.Add(item);
                }
            }
            return lista;
        }

    }
}
