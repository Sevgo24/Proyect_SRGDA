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
    public class DATarifaDescuento
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaDescuento descuento)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_DESCUENTO_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, descuento.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, descuento.RATE_ID);
            db.AddInParameter(oDbCommand, "@DISC_ID", DbType.String, descuento.DISC_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, descuento.LOG_USER_CREAT);
            db.AddOutParameter(oDbCommand, "@RATE_DISC_ID", DbType.Decimal, Convert.ToInt32(descuento.RATE_DISC_ID));

            int n = db.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@RATE_DISC_ID"));
            return id;
        }

        public int Eliminar(BETarifaDescuento descuento)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_DESCUENTO_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, descuento.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_DISC_ID", DbType.Decimal, descuento.RATE_DISC_ID);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, descuento.RATE_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, descuento.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Activar(BETarifaDescuento descuento)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_DESCUENTO_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, descuento.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_DISC_ID", DbType.Decimal, descuento.RATE_DISC_ID);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, descuento.RATE_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, descuento.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }
        
        public List<BETarifaDescuento> Listar(string owner, decimal idRate)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_LISTA_DESCUENTO_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idRate);
            db.ExecuteNonQuery(oDbCommand);

            List<BETarifaDescuento> lista = new List<BETarifaDescuento>();
            BETarifaDescuento descuento = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    descuento = new BETarifaDescuento();
                    descuento.RATE_DISC_ID = dr.GetDecimal(dr.GetOrdinal("RATE_DISC_ID"));
                    descuento.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    descuento.DISC_TYPE = dr.GetDecimal(dr.GetOrdinal("DISC_TYPE"));
                    descuento.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                    descuento.DISC_TYPE_NAME = dr.GetString(dr.GetOrdinal("DISC_TYPE_NAME"));
                    descuento.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));
                    descuento.DISC_SIGN = dr.GetString(dr.GetOrdinal("DISC_SIGN"));
                    descuento.DISC_PERC = dr.GetDecimal(dr.GetOrdinal("DISC_PERC"));
                    descuento.DISC_VALUE = dr.GetDecimal(dr.GetOrdinal("DISC_VALUE"));
                    descuento.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    descuento.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        descuento.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(descuento);
                }
            }
            return lista;
        }

        public BETarifaDescuento Obtener(string owner, decimal idTarifa, decimal id,decimal idDescuento)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_DESCUENTO_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idTarifa);
            db.AddInParameter(oDbCommand, "@RATE_DISC_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, idDescuento);
            BETarifaDescuento ent = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaDescuento();
                    ent.RATE_DISC_ID = dr.GetDecimal(dr.GetOrdinal("RATE_DISC_ID"));
                    ent.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    ent.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                }
            }
            return ent;
        }


    }
}
