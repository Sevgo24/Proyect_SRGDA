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
    public class DAREF_DIV_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_DIV_TYPE> usp_Get_REF_DIV_TYPE()
        {
            List<BEREF_DIV_TYPE> lst = new List<BEREF_DIV_TYPE>();
            BEREF_DIV_TYPE item = null;
            using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_TYPE_GET"))
            {
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREF_DIV_TYPE();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
                        item.DAD_TNAME = dr.GetString(dr.GetOrdinal("DAD_TNAME"));
                        item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));

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
            return lst;
        }

        public BEREF_DIV_TYPE Obtiene(string owner, string DAD_TYPE)
        {
            BEREF_DIV_TYPE item = null;
            using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_TYPE_GET_by_DAD_TYPE"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@DAD_TYPE", DbType.String, DAD_TYPE);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEREF_DIV_TYPE();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
                        item.DAD_TNAME = dr.GetString(dr.GetOrdinal("DAD_TNAME"));
                        item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        item.NAME_TER = dr.GetString(dr.GetOrdinal("NAME_TER"));
                        item.DIVT_OBSERV = dr.GetString(dr.GetOrdinal("DIVT_OBSERV"));

                        DateTime LOG_DATE_CREAT;
                        bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                        item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                        DateTime LOG_DATE_UPDATE;
                        bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                        item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        item.LOG_USER_UPDATE = (item.LOG_USER_UPDATE == null) ? string.Empty : item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                    }
                }
            }
            return item;
        }

        public List<BEREF_DIV_TYPE> ListarPage(string param, decimal terr, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_DIV_TYPE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, terr);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREF_DIV_TYPE>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREF_DIV_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public int Insertar(BEREF_DIV_TYPE en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_TYPE_Ins");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, en.DAD_TYPE.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_TNAME", DbType.String, en.DAD_TNAME.ToUpper());
            db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
            db.AddInParameter(oDbCommand, "@DIVT_OBSERV", DbType.String, en.DIVT_OBSERV.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEREF_DIV_TYPE en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_TYPE_Upd");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, en.DAD_TYPE.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_TNAME", DbType.String, en.DAD_TNAME.ToUpper());
            db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
            db.AddInParameter(oDbCommand, "@DIVT_OBSERV", DbType.String, en.DIVT_OBSERV.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEREF_DIV_TYPE en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_TYPE_Del");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, en.DAD_TYPE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public List<BEREF_DIV_TYPE> ListarTipoDivisiones(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPO_DIVISIONES_DDL");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEREF_DIV_TYPE>();
            BEREF_DIV_TYPE obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEREF_DIV_TYPE();
                    obs.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
                    obs.DAD_TNAME = dr.GetString(dr.GetOrdinal("DAD_TNAME"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

    }
}
