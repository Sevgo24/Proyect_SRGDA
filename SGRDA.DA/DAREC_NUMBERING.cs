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
    public class DAREC_NUMBERING
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_NUMBERING> Get_REC_NUMBERING()
        {
            List<BEREC_NUMBERING> lst = new List<BEREC_NUMBERING>();
            BEREC_NUMBERING item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_NUMBERING"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_NUMBERING();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                            item.NMR_TYPE = dr.GetString(dr.GetOrdinal("NMR_TYPE"));
                            item.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                            item.NMR_NAME = dr.GetString(dr.GetOrdinal("NMR_NAME"));
                            item.W_SERIAL = dr.GetString(dr.GetOrdinal("W_SERIAL"));
                            item.W_YEAR = dr.GetString(dr.GetOrdinal("W_YEAR"));

                            item.NMR_FORM = dr.GetDecimal(dr.GetOrdinal("NMR_FORM"));
                            item.NMR_TO = dr.GetDecimal(dr.GetOrdinal("NMR_TO"));
                            item.NMR_NOW = dr.GetDecimal(dr.GetOrdinal("NMR_NOW"));

                            item.AJUST = dr.GetString(dr.GetOrdinal("AJUST"));

                            item.POS_SERIAL = dr.GetDecimal(dr.GetOrdinal("POS_SERIAL"));
                            item.LON_YEAR = dr.GetDecimal(dr.GetOrdinal("LON_YEAR"));
                            item.POS_YEAR = dr.GetDecimal(dr.GetOrdinal("POS_YEAR"));

                            item.DIVIDER1 = dr.GetString(dr.GetOrdinal("DIVIDER1"));
                            item.DIVIDER2 = dr.GetString(dr.GetOrdinal("DIVIDER2"));
                            item.NMR_MANUAL = dr.GetString(dr.GetOrdinal("NMR_MANUAL"));

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDATE = (item.LOG_USER_UPDATE == null) ? string.Empty : item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

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

        public List<BEREC_NUMBERING> REC_NUMBERING_by_NMR_ID(decimal NMR_ID)
        {
            List<BEREC_NUMBERING> lst = new List<BEREC_NUMBERING>();
            BEREC_NUMBERING item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_NUMBERING_GET_by_NMR_ID"))
                {
                    db.AddInParameter(cm, "@NMR_ID", DbType.Decimal, NMR_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_NUMBERING();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                            item.NMR_TYPE = dr.GetString(dr.GetOrdinal("NMR_TYPE"));
                            item.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                            item.NMR_NAME = dr.GetString(dr.GetOrdinal("NMR_NAME"));
                            item.W_SERIAL = dr.GetString(dr.GetOrdinal("W_SERIAL"));
                            item.W_YEAR = dr.GetString(dr.GetOrdinal("W_YEAR"));

                            item.NMR_FORM = dr.GetDecimal(dr.GetOrdinal("NMR_FORM"));
                            item.NMR_TO = dr.GetDecimal(dr.GetOrdinal("NMR_TO"));
                            item.NMR_NOW = dr.GetDecimal(dr.GetOrdinal("NMR_NOW"));

                            item.AJUST = dr.GetString(dr.GetOrdinal("AJUST"));

                            item.POS_SERIAL = dr.GetDecimal(dr.GetOrdinal("POS_SERIAL"));
                            item.LON_YEAR = dr.GetDecimal(dr.GetOrdinal("LON_YEAR"));
                            item.POS_YEAR = dr.GetDecimal(dr.GetOrdinal("POS_YEAR"));

                            if (!dr.IsDBNull(dr.GetOrdinal("DIVIDER1")))
                                item.DIVIDER1 = dr.GetString(dr.GetOrdinal("DIVIDER1"));
                            else
                                item.DIVIDER1 = "";

                            if (!dr.IsDBNull(dr.GetOrdinal("DIVIDER2")))
                                item.DIVIDER2 = dr.GetString(dr.GetOrdinal("DIVIDER2"));
                            else
                                item.DIVIDER2 = "";

                            if (!dr.IsDBNull(dr.GetOrdinal("NMR_MANUAL")))
                                item.NMR_MANUAL = dr.GetString(dr.GetOrdinal("NMR_MANUAL"));
                            else
                                item.NMR_MANUAL = "0";

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDATE = (item.LOG_USER_UPDATE == null) ? string.Empty : item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lst;
        }



        public List<BEREC_NUMBERING> REC_NUMBERING_Page(string owner, string param, int st, string serie, decimal off_id, int tipo, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_NUMBERING_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@serie", DbType.String, serie);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@ofF_id", DbType.Decimal, off_id);
            oDataBase.AddInParameter(oDbCommand, "@tipo", DbType.Int32, tipo);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_NUMBERING>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_NUMBERING(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEREC_NUMBERING> ListarCorrelativosRecibo(string owner, string param, int? st, string serie, int pagina, int cantRegxPag)
        {
            if (st == null) st = 1;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CORRELATIVO_RECIBO");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@serie", DbType.String, serie);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_NUMBERING>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_NUMBERING(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEREC_NUMBERING> ListarCorrelativosNotaCredito(string owner, string param,string serie, int? st, int pagina, int cantRegxPag)
        {
            if (st == null) st = 1;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CORRELATIVO_NOTACREDITO");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@serie", DbType.String, serie);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_NUMBERING>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_NUMBERING(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_NUMBERING_Ins(BEREC_NUMBERING en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_NUMBERING_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                db.AddInParameter(oDbCommand, "@NMR_NAME", DbType.String, en.NMR_NAME.ToUpper());
                db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
                db.AddInParameter(oDbCommand, "@NMR_TYPE", DbType.String, en.NMR_TYPE.ToUpper());
                db.AddInParameter(oDbCommand, "@AJUST", DbType.String, en.AJUST.ToUpper());

                db.AddInParameter(oDbCommand, "@NMR_FORM", DbType.Decimal, en.NMR_FORM);
                db.AddInParameter(oDbCommand, "@NMR_TO", DbType.Decimal, en.NMR_TO);
                db.AddInParameter(oDbCommand, "@NMR_NOW", DbType.Decimal, en.NMR_NOW);
                db.AddInParameter(oDbCommand, "@NMR_MANUAL", DbType.String, en.NMR_MANUAL.ToUpper());

                db.AddInParameter(oDbCommand, "@W_SERIAL", DbType.String, en.W_SERIAL.ToUpper());
                if (en.W_SERIAL == "0")
                {
                    db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, string.Empty);
                    db.AddInParameter(oDbCommand, "@POS_SERIAL", DbType.Decimal, 0);
                }
                else
                {
                    db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, en.NMR_SERIAL.ToUpper());
                    db.AddInParameter(oDbCommand, "@POS_SERIAL", DbType.Decimal, en.POS_SERIAL);
                }

                db.AddInParameter(oDbCommand, "@W_YEAR", DbType.String, en.W_YEAR.ToUpper());
                db.AddInParameter(oDbCommand, "@LON_YEAR", DbType.Decimal, en.LON_YEAR);
                db.AddInParameter(oDbCommand, "@POS_YEAR", DbType.Decimal, en.POS_YEAR);

                db.AddInParameter(oDbCommand, "@DIVIDER1", DbType.String, en.DIVIDER1.ToUpper());
                db.AddInParameter(oDbCommand, "@DIVIDER2", DbType.String, en.DIVIDER2.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_NUMBERING_Upd(BEREC_NUMBERING en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_NUMBERING_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, en.NMR_ID);
                db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
                db.AddInParameter(oDbCommand, "@NMR_TYPE", DbType.String, en.NMR_TYPE.ToUpper());
                db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, en.NMR_SERIAL.ToUpper());
                db.AddInParameter(oDbCommand, "@NMR_NAME", DbType.String, en.NMR_NAME.ToUpper());
                db.AddInParameter(oDbCommand, "@W_SERIAL", DbType.String, en.W_SERIAL.ToUpper());
                db.AddInParameter(oDbCommand, "@W_YEAR", DbType.String, en.W_YEAR.ToUpper());
                db.AddInParameter(oDbCommand, "@NMR_FORM", DbType.Decimal, en.NMR_FORM);
                db.AddInParameter(oDbCommand, "@NMR_TO", DbType.Decimal, en.NMR_TO);
                db.AddInParameter(oDbCommand, "@NMR_NOW", DbType.Decimal, en.NMR_NOW);
                db.AddInParameter(oDbCommand, "@AJUST", DbType.String, en.AJUST.ToUpper());
                db.AddInParameter(oDbCommand, "@POS_SERIAL", DbType.Decimal, en.POS_SERIAL);
                db.AddInParameter(oDbCommand, "@LON_YEAR", DbType.Decimal, en.LON_YEAR);
                db.AddInParameter(oDbCommand, "@POS_YEAR", DbType.Decimal, en.POS_YEAR);
                db.AddInParameter(oDbCommand, "@DIVIDER1", DbType.String, en.DIVIDER1.ToUpper());
                db.AddInParameter(oDbCommand, "@DIVIDER2", DbType.String, en.DIVIDER2.ToUpper());
                db.AddInParameter(oDbCommand, "@NMR_MANUAL", DbType.String, en.NMR_MANUAL.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_NUMBERING_Del(decimal NMR_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_NUMBERING_Del");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, NMR_ID);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREC_NUMBERING> ListarXtipo(string owner, string nmrType)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_NUMERING");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@NMR_TYPE", DbType.String, nmrType);
            oDataBase.ExecuteNonQuery(oDbCommand);

            var lista = new List<BEREC_NUMBERING>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                BEREC_NUMBERING ent;
                while (reader.Read())
                {
                    ent = new BEREC_NUMBERING();
                    ent.NMR_ID = Convert.ToDecimal(reader["NMR_ID"]);
                    ent.NMR_SERIAL = Convert.ToString(reader["NMR_SERIAL"]);
                    lista.Add(ent);
                }
            }
            return lista;
        }

        public List<BEREC_NUMBERING> ListarSerie(string owner)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommnad = oDatabase.GetStoredProcCommand("SGRDAS_LISTAR_SERIE");
            oDatabase.AddInParameter(oDbCommnad, "@OWNER", DbType.String, owner);
            oDatabase.ExecuteNonQuery(oDbCommnad);
            var lista = new List<BEREC_NUMBERING>();

            using (IDataReader reader = oDatabase.ExecuteReader(oDbCommnad))
            {
                BEREC_NUMBERING item = null;
                while (reader.Read())
                {
                    item = new BEREC_NUMBERING();
                    item.NMR_ID = Convert.ToDecimal(reader["NMR_ID"]);
                    item.NMR_SERIAL = Convert.ToString(reader["NMR_SERIAL"]);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int ObtenerXSerie(BEREC_NUMBERING correlativo)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CORRELATIVO_SERIE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, correlativo.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@NMR_ID", DbType.String, correlativo.NMR_ID);
            oDataBase.AddInParameter(oDbCommand, "@NMR_TYPE", DbType.String, correlativo.NMR_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, correlativo.NMR_SERIAL);
            BEOrigenModalidad obj = new BEOrigenModalidad();
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public BEREC_NUMBERING ObtenerNombre(string owner, decimal id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CORRELATIVO_DESC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, id);
            oDataBase.ExecuteNonQuery(oDbCommand);

            BEREC_NUMBERING ent = null;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {

                while (reader.Read())
                {
                    ent = new BEREC_NUMBERING();
                    ent.NMR_ID = Convert.ToDecimal(reader["NMR_ID"]);
                    ent.NMR_NAME = Convert.ToString(reader["NMR_NAME"]);
                    ent.NMR_SERIAL = Convert.ToString(reader["NMR_SERIAL"]);
                    ent.NMR_NOW = Convert.ToDecimal(reader["NMR_NOW"]);
                    ent.NMR_TYPE = Convert.ToString(reader["NMR_TYPE"]);
                    ent.NMR_TDESC = Convert.ToString(reader["NMR_TDESC"]);
                }
            }
            return ent;

        }

        public List<BEREC_NUMBERING> ListarSerieXtipo(string owner, string tipo)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_SERIE_X_TIPO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@NMR_TYPE", DbType.String, tipo);
            List<BEREC_NUMBERING> lista = new List<BEREC_NUMBERING>();
            BEREC_NUMBERING item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEREC_NUMBERING();
                    if (!dr.IsDBNull(dr.GetOrdinal("NMR_ID")))
                        item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                        item.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public BEREC_NUMBERING ObtenerCorrelativoXtipo(string owner, string tipo)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_CORRELATIVO_X_TIPO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@NMR_TYPE", DbType.String, tipo);
            BEREC_NUMBERING item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEREC_NUMBERING();
                    if (!dr.IsDBNull(dr.GetOrdinal("NMR_NOW")))
                        item.NMR_NOW = dr.GetDecimal(dr.GetOrdinal("NMR_NOW"));
                }
                return item;
            }
        }

        public int ActualizarCorrelativoPlanilla(string owner, decimal reportNumber, decimal? idReport, string user)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAU_CORRELATIVO_PLANILLA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@REPORT_NUMBER", DbType.Decimal, reportNumber);
            oDataBase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, idReport);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ObtenerCorrelativoPLanilla(string owner, decimal idReport)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_VALIDAR_PLANILLA_IMP");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, idReport);
            BEOrigenModalidad obj = new BEOrigenModalidad();
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BEREC_NUMBERING> ListarNumeradores(string owner, string param, int st, string serie,string tipoSerie, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_NUMBERING_GET_Page");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_NUMERADORES");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@serie", DbType.String, serie);
            oDataBase.AddInParameter(oDbCommand, "@tipoSerie", DbType.String, tipoSerie);            
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_NUMBERING>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_NUMBERING(reader, Convert.ToInt32(results)));
            }
            return lista;
        }



        public int ValidarSerieNumero(decimal idSerie, decimal numero)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_NUM_DOC_VALIDACION");
            oDataBase.AddInParameter(oDbCommand, "@INV_NMR", DbType.Decimal, idSerie);
            oDataBase.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numero);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ValidarSerieTipoSocio(string idSerie, decimal documento)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDA_DOCUMENTO_SOCIO_TIPO");
            oDataBase.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, idSerie);
            oDataBase.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, documento);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

    }
}
