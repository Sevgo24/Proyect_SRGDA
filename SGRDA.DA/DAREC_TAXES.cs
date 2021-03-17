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
using System.Configuration;

namespace SGRDA.DA
{
    public class DAREC_TAXES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_TAXES> REC_TAXES_GET(string owner, string descripcion, decimal territorio)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_TAXES_GET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, descripcion);
            db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, territorio);
            var lista = new List<BEREC_TAXES>();
            BEREC_TAXES ent = null;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    ent = new BEREC_TAXES();
                    ent.OWNER = reader.GetString(reader.GetOrdinal("OWNER"));
                    ent.TAX_ID = reader.GetDecimal(reader.GetOrdinal("TAX_ID"));
                    ent.TIS_N = reader.GetDecimal(reader.GetOrdinal("TIS_N"));
                    ent.TAX_COD = reader.GetString(reader.GetOrdinal("TAX_COD"));
                    ent.DESCRIPTION = reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                    ent.TAX_CACC = reader.GetDecimal(reader.GetOrdinal("TAX_CACC"));
                    ent.NAME_TER = reader.GetString(reader.GetOrdinal("NAME_TER"));
                    if (!reader.IsDBNull(reader.GetOrdinal("START")))
                        ent.START = reader.GetDateTime(reader.GetOrdinal("START"));
                    lista.Add(ent);
                    //lista.Add(new BEREC_TAXES(reader));
                }
            }
            return lista;
        }

        public bool REC_TAXES_Ins(BEREC_TAXES en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_TAXES_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
                db.AddInParameter(oDbCommand, "@TAX_COD", DbType.String, en.TAX_COD);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION);
                db.AddInParameter(oDbCommand, "@START", DbType.DateTime, en.START);
                db.AddInParameter(oDbCommand, "@TAX_CACC", DbType.Decimal, en.TAX_CACC);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_TAXES_Upd_by_TAX_ID(BEREC_TAXES en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_TAXES_Upd_by_TAX_ID");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@TAX_ID", DbType.Decimal, en.TAX_ID);
                db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
                db.AddInParameter(oDbCommand, "@TAX_COD", DbType.String, en.TAX_COD);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION);
                db.AddInParameter(oDbCommand, "@TAX_CACC", DbType.Decimal, en.TAX_CACC);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_TAXES_Del_by_TAX_ID(decimal TAX_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_TAXES_Del_by_TAX_ID");
                db.AddInParameter(oDbCommand, "@TAX_ID", DbType.Decimal, TAX_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREC_TAXES> REC_TAXES_GET_by_TAX_ID(decimal TAX_ID)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_TAXES_GET_by_TAX_ID");
            db.AddInParameter(oDbCommand, "@TAX_ID", DbType.Decimal, TAX_ID);
            db.AddInParameter(oDbCommand, "@owner", DbType.String, GlobalVars.Global.OWNER);
            var lista = new List<BEREC_TAXES>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_TAXES(reader));
            }
            return lista;
        }

        public List<BEREC_TAXES> usp_REC_GET_TAXES_Page(string owner, string param, decimal territorio, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_GET_TAXES_Page");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@param", DbType.String, param);
            db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, territorio);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database db1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = db1.GetStoredProcCommand("usp_REC_GET_TAXES_Page", owner, param, territorio, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_TAXES>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_TAXES(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEREC_TAXES> Listar(string owner, string descripcion, decimal territorio, int estado, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_IMPUESTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPCION", DbType.String, descripcion);
            oDataBase.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, territorio);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_IMPUESTO", owner, descripcion, territorio, estado, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_TAXES>();
            BEREC_TAXES impuesto = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    impuesto = new BEREC_TAXES();
                    impuesto.TAX_ID = dr.GetDecimal(dr.GetOrdinal("TAX_ID"));
                    impuesto.TAX_COD = dr.GetString(dr.GetOrdinal("TAX_COD"));
                    impuesto.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    if (!dr.IsDBNull(dr.GetOrdinal("START"))) impuesto.START = dr.GetDateTime(dr.GetOrdinal("START"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS"))) impuesto.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    impuesto.TAX_CACC = dr.GetDecimal(dr.GetOrdinal("TAX_CACC"));
                    impuesto.NAME_TER = dr.GetString(dr.GetOrdinal("NAME_TER"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        impuesto.ESTADO = "ACTIVO";
                    else
                        impuesto.ESTADO = "INACTIVO";

                    impuesto.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(impuesto);
                }
            }
            return lista;
        }

        public int Eliminar(BEREC_TAXES impuesto)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_IMPUESTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, impuesto.OWNER);
            db.AddInParameter(oDbCommand, "@TAX_ID", DbType.Decimal, impuesto.TAX_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, impuesto.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEREC_TAXES Obtener(string owner, decimal id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_IMPUESTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@TAX_ID", DbType.Decimal, id);

            BEREC_TAXES ent = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BEREC_TAXES();
                    ent.TAX_ID = dr.GetDecimal(dr.GetOrdinal("TAX_ID"));
                    ent.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                    ent.TAX_COD = dr.GetString(dr.GetOrdinal("TAX_COD"));
                    ent.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    ent.TAX_CACC = dr.GetDecimal(dr.GetOrdinal("TAX_CACC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("START")))
                        ent.FECHA_VIGENCIA = dr.GetDateTime(dr.GetOrdinal("START")).ToString("dd/MM/yyyy");
                }
            }
            return ent;
        }

        public int Insertar(BEREC_TAXES impuesto)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_IMPUESTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, impuesto.OWNER);
            db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, impuesto.TIS_N);
            db.AddInParameter(oDbCommand, "@TAX_COD", DbType.String, impuesto.TAX_COD);
            db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, impuesto.DESCRIPTION.ToUpper());
            db.AddInParameter(oDbCommand, "@START", DbType.DateTime, impuesto.START);
            db.AddInParameter(oDbCommand, "@TAX_CACC", DbType.Decimal, impuesto.TAX_CACC);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, impuesto.LOG_USER_CREAT.ToUpper());
            db.AddOutParameter(oDbCommand, "@TAX_ID", DbType.Decimal, 0);

            int r = db.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@TAX_ID"));
            return id;
        }

        public int Actualizar(BEREC_TAXES impuesto)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_IMPUESTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, impuesto.OWNER);
            db.AddInParameter(oDbCommand, "@TAX_ID", DbType.Decimal, impuesto.TAX_ID);
            db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, impuesto.TIS_N);
            db.AddInParameter(oDbCommand, "@TAX_COD", DbType.String, impuesto.TAX_COD);
            db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, impuesto.DESCRIPTION.ToUpper());
            db.AddInParameter(oDbCommand, "@START", DbType.DateTime, impuesto.START);
            db.AddInParameter(oDbCommand, "@TAX_CACC", DbType.Decimal, impuesto.TAX_CACC);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, impuesto.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ObtenerXDescripcion(BEREC_TAXES impuesto)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_IMPUESTO_DESC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, impuesto.OWNER);
            db.AddInParameter(oDbCommand, "@TAX_ID", DbType.Decimal, impuesto.TAX_ID);
            db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, impuesto.DESCRIPTION);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        #region igv
        public decimal ObtenerIGV(string owner, decimal id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_VALOR_IGV");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@division", DbType.Decimal, id);
            decimal igv=Convert.ToDecimal( db.ExecuteScalar(oDbCommand));

            return igv;
        }
        #endregion

    }
}
