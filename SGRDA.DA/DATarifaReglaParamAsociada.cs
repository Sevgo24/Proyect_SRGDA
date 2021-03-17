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
    public class DATarifaReglaParamAsociada
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaReglaParamAsociada param)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_MANT_TARIFA_PARAMETRO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, param.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_ID", DbType.Decimal, param.RATE_CHAR_ID);
            db.AddInParameter(oDbCommand, "@RATE_CALC_ID", DbType.Decimal, param.RATE_CALC_ID);
            db.AddInParameter(oDbCommand, "@RATE_CALC_AR", DbType.Decimal, param.RATE_CALC_AR);
            db.AddInParameter(oDbCommand, "@RATE_CALC", DbType.String, param.RATE_CALC);
            db.AddInParameter(oDbCommand, "@RATE_PARAM_VAR", DbType.String, param.RATE_PARAM_VAR);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, param.LOG_USER_CREAT);           

            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public List<BETarifaReglaParamAsociada> Listar(string owner, decimal idRate)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_LISTAR_MANT_TARIFA_PARAMETRO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idRate);
            db.ExecuteNonQuery(oDbCommand);

            List<BETarifaReglaParamAsociada> lista = new List<BETarifaReglaParamAsociada>();
            BETarifaReglaParamAsociada param = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    param = new BETarifaReglaParamAsociada();
                    param.RATE_PARAM_ID = dr.GetDecimal(dr.GetOrdinal("RATE_PARAM_ID"));
                    param.RATE_CHAR_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CHAR_ID"));
                    param.RATE_CALC_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_ID"));
                    param.RATE_CALC_AR = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_AR"));
                    param.RATE_CALC = dr.GetDecimal(dr.GetOrdinal("RATE_CALC"));
                    param.RATE_PARAM_VAR = dr.GetString(dr.GetOrdinal("RATE_PARAM_VAR"));
                    param.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    param.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        param.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(param);
                }
            }
            return lista;
        }

        public BETarifaReglaParamAsociada Obtener(string owner, decimal idParam,decimal idChar,decimal idElemto)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_MANT_TARIFA_PARAMETRO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_PARAM_ID", DbType.Decimal, idParam);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_ID", DbType.Decimal, idChar);
            db.AddInParameter(oDbCommand, "@RATE_CALC_ID", DbType.Decimal, idElemto);
            BETarifaReglaParamAsociada ent = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaReglaParamAsociada();
                    ent.RATE_PARAM_ID = dr.GetDecimal(dr.GetOrdinal("RATE_PARAM_ID"));
                    ent.RATE_CHAR_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CHAR_ID"));
                    ent.RATE_CALC_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_ID"));
                    ent.RATE_CALC_AR = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_AR"));
                    ent.RATE_CALC = dr.GetDecimal(dr.GetOrdinal("RATE_CALC"));
                    ent.RATE_PARAM_VAR = dr.GetString(dr.GetOrdinal("RATE_PARAM_VAR"));
                }
            }
            return ent;
        }

        public int Actualizar(BETarifaReglaParamAsociada param)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_MANT_TARIFA_PARAMETRO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, param.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_PARAM_ID", DbType.Decimal, param.RATE_PARAM_ID);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_ID", DbType.Decimal, param.RATE_CHAR_ID);
            db.AddInParameter(oDbCommand, "@RATE_CALC_ID", DbType.String, param.RATE_CALC_ID);
            db.AddInParameter(oDbCommand, "@RATE_PARAM_VAR", DbType.String, param.RATE_PARAM_VAR);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, param.LOG_USER_UPDAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BETarifaReglaParamAsociada param)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_MANT_TARIFA_PARAMETRO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, param.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_PARAM_ID", DbType.Decimal, param.RATE_PARAM_ID);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_ID", DbType.Decimal, param.RATE_CHAR_ID);
            db.AddInParameter(oDbCommand, "@RATE_CALC_ID", DbType.String, param.RATE_CALC_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, param.LOG_USER_UPDAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }


    }
}
