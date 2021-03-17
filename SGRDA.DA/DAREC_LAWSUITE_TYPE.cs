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
    public class DAREC_LAWSUITE_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_LAWSUITE_TYPE> Get_REC_LAWSUITE_TYPE()
        {
            List<BEREC_LAWSUITE_TYPE> lst = new List<BEREC_LAWSUITE_TYPE>();
            BEREC_LAWSUITE_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_LAWSUITE_TYPE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_LAWSUITE_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.LAWS_ID = dr.GetString(dr.GetOrdinal("LAWS_ID"));
                            item.LAWS_DESC = dr.GetString(dr.GetOrdinal("LAWS_DESC"));
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

        public List<BEREC_LAWSUITE_TYPE> REC_LAWSUITE_TYPE_GET_by_LAWS_ID(string LAWS_ID)
        {
            List<BEREC_LAWSUITE_TYPE> lst = new List<BEREC_LAWSUITE_TYPE>();
            BEREC_LAWSUITE_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_LAWSUITE_TYPE_GET_by_LAWS_ID"))
                {
                    db.AddInParameter(cm, "@LAWS_ID", DbType.String, LAWS_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_LAWSUITE_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.LAWS_ID = dr.GetString(dr.GetOrdinal("LAWS_ID"));
                            item.LAWS_DESC = dr.GetString(dr.GetOrdinal("LAWS_DESC"));
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

        public List<BEREC_LAWSUITE_TYPE> REC_LAWSUITE_TYPE_Page(string param, int st, int pagina, int cantRegxPag)
        {

            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_LAWSUITE_TYPE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
           // oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));


            var lista = new List<BEREC_LAWSUITE_TYPE>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_LAWSUITE_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;       

        }

        public bool REC_LAWSUITE_TYPE_Ins(BEREC_LAWSUITE_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_LAWSUITE_TYPE_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@LAWS_ID", DbType.String, en.LAWS_ID.ToUpper());
                db.AddInParameter(oDbCommand, "@LAWS_DESC", DbType.String, en.LAWS_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_LAWSUITE_TYPE_Upd(BEREC_LAWSUITE_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_LAWSUITE_TYPE_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@LAWS_ID", DbType.String, en.LAWS_ID.ToUpper());
                db.AddInParameter(oDbCommand, "@LAWS_DESC", DbType.String, en.LAWS_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_LAWSUITE_TYPE_Del(string LAWS_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_LAWSUITE_TYPE_Del");
                db.AddInParameter(oDbCommand, "@LAWS_ID", DbType.String, LAWS_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
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
