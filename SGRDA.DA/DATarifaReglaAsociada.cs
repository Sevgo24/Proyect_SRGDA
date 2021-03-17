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
    public class DATarifaReglaAsociada
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaReglaAsociada tarifaAsoc)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_MANT_TARIFA_ASOC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, tarifaAsoc.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_CALC", DbType.Decimal, tarifaAsoc.RATE_CALC);
            db.AddInParameter(oDbCommand, "@RATE_CALCT", DbType.String, tarifaAsoc.RATE_CALCT);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, tarifaAsoc.RATE_ID);
            db.AddInParameter(oDbCommand, "@RATE_CALC_VAR", DbType.String, tarifaAsoc.RATE_CALC_VAR);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, tarifaAsoc.LOG_USER_CREAT);
            db.AddOutParameter(oDbCommand, "@RATE_CALC_ID", DbType.Decimal, Convert.ToInt32(tarifaAsoc.RATE_CALC_ID));

            int n = db.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@RATE_CALC_ID"));
            return id;
        }

        public List<BETarifaReglaAsociada> Listar(string owner, decimal idRate)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_LISTA_MANT_TARIFA_ASOC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idRate);
            db.ExecuteNonQuery(oDbCommand);

            List<BETarifaReglaAsociada> lista = new List<BETarifaReglaAsociada>();
            BETarifaReglaAsociada regla = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    regla = new BETarifaReglaAsociada();
                    regla.RATE_CALC_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_ID"));
                    regla.RATE_CALC = dr.GetDecimal(dr.GetOrdinal("RATE_CALC"));
                    regla.RATE_CALCT = dr.GetString(dr.GetOrdinal("RATE_CALCT"));
                    regla.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    regla.RATE_CALC_VAR = dr.GetString(dr.GetOrdinal("RATE_CALC_VAR"));
                    regla.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    regla.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        regla.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    regla.ELEMENTO = dr.GetString(dr.GetOrdinal("ELEMENTO"));
                    lista.Add(regla);
                }
            }
            return lista;
        }

        public BETarifaReglaAsociada Obtener(string owner, decimal idTarifa, decimal idRegla)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_REGLA_MANT_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idTarifa);
            db.AddInParameter(oDbCommand, "@RATE_CALC_ID", DbType.Decimal, idRegla);
            BETarifaReglaAsociada ent = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaReglaAsociada();
                    ent.RATE_CALC_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_ID"));
                    ent.RATE_CALC = dr.GetDecimal(dr.GetOrdinal("RATE_CALC"));
                    ent.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    ent.RATE_CALCT = dr.GetString(dr.GetOrdinal("RATE_CALCT"));
                    ent.RATE_CALC_VAR = dr.GetString(dr.GetOrdinal("RATE_CALC_VAR"));
                }
            }
            return ent;
        }

        public int Actualizar(BETarifaReglaAsociada tarifaAsoc)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_REGLA_MANT_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, tarifaAsoc.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, tarifaAsoc.RATE_ID);
            db.AddInParameter(oDbCommand, "@RATE_CALC_ID", DbType.Decimal, tarifaAsoc.RATE_CALC_ID);
            db.AddInParameter(oDbCommand, "@RATE_CALC_VAR", DbType.String, tarifaAsoc.RATE_CALC_VAR);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, tarifaAsoc.LOG_USER_UPDAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BETarifaReglaAsociada tarifaAsoc)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_REGLA_MANT_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, tarifaAsoc.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, tarifaAsoc.RATE_ID);
            db.AddInParameter(oDbCommand, "@RATE_CALC_ID", DbType.Decimal, tarifaAsoc.RATE_CALC_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, tarifaAsoc.LOG_USER_UPDAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int CantReglaAsocMant(string owner,decimal idRegla)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CANT_REGLA_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_CALC", DbType.Decimal,idRegla);
            int n = Convert.ToInt32( db.ExecuteScalar(oDbCommand));
            return n;
        }

    }
}
