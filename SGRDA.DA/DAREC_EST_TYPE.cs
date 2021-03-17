using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;
using System.Data.Common;
using System.Data;

namespace SGRDA.DA
{
    public class DAREC_EST_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_EST_TYPE> REC_EST_TYPE_GET()
        {
            List<BEREC_EST_TYPE> lst = new List<BEREC_EST_TYPE>();
            BEREC_EST_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_EST_TYPE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_EST_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.ESTT_ID = dr.GetDecimal(dr.GetOrdinal("ESTT_ID"));
                            item.ECON_ID = dr.GetString(dr.GetOrdinal("ECON_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.ECON_DESC = dr.GetString(dr.GetOrdinal("ECON_DESC"));
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

        public List<BEREC_EST_TYPE> usp_REC_EST_TYPE_Page(string param, string tipo, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_EST_TYPE_GET_Page");
            db.AddInParameter(oDbCommand, "@param", DbType.String, param);
            db.AddInParameter(oDbCommand, "@ECON_ID", DbType.String, tipo);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEREC_EST_TYPE>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_EST_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_EST_TYPE_Ins(BEREC_EST_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_EST_TYPE_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@ECON_ID", DbType.String, en.ECON_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool existeTipoEstablecimiento(string Owner, string id, string desc)
        {
            bool existe = false;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_EXISTE_TIPO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(cm, "@ECON_ID", DbType.String, id);
                db.AddInParameter(cm, "@DESCRIPTION", DbType.String, desc);
                db.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = db.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(db.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existeTipoEstablecimiento(string Owner, string idEco, string desc, decimal Id)
        {
            bool existe = false;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_EXISTE_UPDATE_TIPO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(cm, "@ECON_ID", DbType.String, idEco);
                db.AddInParameter(cm, "@DESCRIPTION", DbType.String, desc);
                db.AddInParameter(cm, "@ESTT_ID", DbType.Decimal, Id);
                db.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = db.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(db.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool REC_EST_TYPE_Upd(BEREC_EST_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_EST_TYPE_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@ESTT_ID ", DbType.Decimal, en.ESTT_ID);
                db.AddInParameter(oDbCommand, "@ECON_ID", DbType.String, en.ECON_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION ", DbType.String, en.DESCRIPTION.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT ", DbType.String, en.LOG_USER_UPDATE.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREC_EST_TYPE> REC_EST_TYPE_GET_by_ESTT_ID(decimal ESTT_ID)
        {
            List<BEREC_EST_TYPE> lst = new List<BEREC_EST_TYPE>();
            BEREC_EST_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_EST_TYPE_GET_by_ESTT_ID"))
                {
                    db.AddInParameter(cm, "@ESTT_ID", DbType.Decimal, ESTT_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_EST_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.ESTT_ID = dr.GetDecimal(dr.GetOrdinal("ESTT_ID"));
                            item.ECON_ID = dr.GetString(dr.GetOrdinal("ECON_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.ECON_DESC = dr.GetString(dr.GetOrdinal("ECON_DESC"));
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

        public bool REC_EST_TYPE_Del(decimal ESTT_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_EST_TYPE_Del");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, ESTT_ID);
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
