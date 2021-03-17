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
    public class DATarifaCaracteristica
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaCaracteristica tarifaCaracteristica)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_MANT_TARIFA_CARACTERISTICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, tarifaCaracteristica.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, tarifaCaracteristica.RATE_ID);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_TVAR", DbType.String, tarifaCaracteristica.RATE_CHAR_TVAR);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_DESCVAR", DbType.String, tarifaCaracteristica.RATE_CHAR_DESCVAR);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_VARUNID", DbType.String, tarifaCaracteristica.RATE_CHAR_VARUNID);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_CARIDSW", DbType.String, tarifaCaracteristica.RATE_CHAR_CARIDSW);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, tarifaCaracteristica.LOG_USER_CREAT);
            db.AddOutParameter(oDbCommand, "@RATE_CHAR_ID", DbType.Decimal, Convert.ToInt32(tarifaCaracteristica.RATE_CHAR_ID));

            int n = db.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@RATE_CHAR_ID"));
            return id;
        }

        public List<BETarifaCaracteristica> Listar(string owner, decimal idRate)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_MANT_TARIFA_CARACTERISTICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idRate);
            db.ExecuteNonQuery(oDbCommand);

            List<BETarifaCaracteristica> lista = new List<BETarifaCaracteristica>();
            BETarifaCaracteristica caracteristica = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    caracteristica = new BETarifaCaracteristica();
                    caracteristica.RATE_CHAR_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CHAR_ID"));
                    caracteristica.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    caracteristica.RATE_CHAR_TVAR = dr.GetString(dr.GetOrdinal("RATE_CHAR_TVAR"));
                    caracteristica.RATE_CHAR_SHORT = dr.GetString(dr.GetOrdinal("CHAR_SHORT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RATE_CHAR_DESCVAR")))
                    caracteristica.RATE_CHAR_DESCVAR = dr.GetString(dr.GetOrdinal("RATE_CHAR_DESCVAR"));
                    caracteristica.RATE_CHAR_VARUNID = dr.GetString(dr.GetOrdinal("RATE_CHAR_VARUNID"));
                    caracteristica.RATE_CHAR_CARIDSW = dr.GetString(dr.GetOrdinal("RATE_CHAR_CARIDSW"));
                    caracteristica.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    caracteristica.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        caracteristica.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    caracteristica.RATE_CALC_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_ID"));
                    caracteristica.RATE_CALCT = dr.GetString(dr.GetOrdinal("RATE_CALCT"));     
                    caracteristica.RATE_CALC_AR = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_AR"));
                    caracteristica.RATE_CALC = dr.GetDecimal(dr.GetOrdinal("RATE_CALC"));    
                    lista.Add(caracteristica);
                }
            }
            return lista;
        }

        public BETarifaCaracteristica Obtener(string owner, decimal idTarifa, decimal idChar)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_MANT_TARIFA_CARACTERISTICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idTarifa);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_ID", DbType.Decimal, idChar);
            BETarifaCaracteristica ent = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaCaracteristica();
                    ent.RATE_CHAR_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CHAR_ID"));
                   ent.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                   if (!dr.IsDBNull(dr.GetOrdinal("RATE_CHAR_TVAR")))
                       ent.RATE_CHAR_TVAR = dr.GetString(dr.GetOrdinal("RATE_CHAR_TVAR"));
                   if (!dr.IsDBNull(dr.GetOrdinal("RATE_CHAR_DESCVAR")))
                       ent.RATE_CHAR_DESCVAR = dr.GetString(dr.GetOrdinal("RATE_CHAR_DESCVAR"));
                    ent.RATE_CHAR_VARUNID = dr.GetString(dr.GetOrdinal("RATE_CHAR_VARUNID"));
                    ent.RATE_CHAR_CARIDSW = dr.GetString(dr.GetOrdinal("RATE_CHAR_CARIDSW"));
                }
            }
            return ent;
        }

        public int Actualizar(BETarifaCaracteristica caracteristica)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_MANT_TARIFA_CARACTERISTICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, caracteristica.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_ID", DbType.Decimal, caracteristica.RATE_CHAR_ID);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, caracteristica.RATE_ID);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_TVAR", DbType.String, caracteristica.RATE_CHAR_TVAR);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_DESCVAR", DbType.String, caracteristica.RATE_CHAR_DESCVAR);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_VARUNID", DbType.String, caracteristica.RATE_CHAR_VARUNID);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_CARIDSW", DbType.String, caracteristica.RATE_CHAR_CARIDSW);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, caracteristica.LOG_USER_UPDAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BETarifaCaracteristica caracteristica)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_MANT_TARIFA_CARACTERISTICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, caracteristica.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_CHAR_ID", DbType.Decimal, caracteristica.RATE_CHAR_ID);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, caracteristica.RATE_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, caracteristica.LOG_USER_UPDAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

       
    }
}
