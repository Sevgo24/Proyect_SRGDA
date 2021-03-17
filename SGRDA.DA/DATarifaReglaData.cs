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
    public class DATarifaReglaData
    {

        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaReglaData data)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_REGLA_DATA");

            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, data.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, data.CALR_ID);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_ID", DbType.Decimal, data.CHAR_ID);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_OTYPE", DbType.String, data.CHAR_OTYPE);
            oDataBase.AddInParameter(oDbCommand, "@CALRD_VAR", DbType.String, data.CALRD_VAR);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, data.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@CALRD_ID", DbType.Decimal, Convert.ToInt32(data.CALR_ID));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@CALRD_ID"));
            return id;
        }

        public List<BETarifaReglaData> Listar(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CARACTERISTICA_REGLA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, id);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BETarifaReglaData> lista = new List<BETarifaReglaData>();
            BETarifaReglaData detalle = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    detalle = new BETarifaReglaData();
                    detalle.CALR_ID = dr.GetDecimal(dr.GetOrdinal("CALR_ID"));
                    detalle.CALRD_ID = dr.GetDecimal(dr.GetOrdinal("CALRD_ID"));
                    detalle.CALRD_VAR = dr.GetString(dr.GetOrdinal("CALRD_VAR"));
                    detalle.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                    detalle.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                    detalle.CHAR_OTYPE = dr.GetString(dr.GetOrdinal("CHAR_OTYPE"));
                    detalle.CHAR_LONG = dr.GetString(dr.GetOrdinal("CHAR_LONG"));
                    detalle.IND_TR = dr.GetString(dr.GetOrdinal("IND_TR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        detalle.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    detalle.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    detalle.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                    lista.Add(detalle);
                }

            }
            return lista;
        }

        public int Eliminar(BETarifaReglaData data)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CARACTERISTICA_TARIFA_REGLA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, data.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CALRD_ID", DbType.Decimal, data.CALRD_ID);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, data.CALR_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, data.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BETarifaReglaData Obtener(string owner, decimal idRegla, decimal idDetalle)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CARACTERISTICA_TARIFA_REGLA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, idRegla);
            oDataBase.AddInParameter(oDbCommand, "@CALRD_ID", DbType.Decimal, idDetalle);
            BETarifaReglaData ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaReglaData();
                    ent.CALRD_ID = dr.GetDecimal(dr.GetOrdinal("CALRD_ID"));
                    ent.CALR_ID = dr.GetDecimal(dr.GetOrdinal("CALR_ID"));
                    ent.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                    ent.CALRD_VAR = dr.GetString(dr.GetOrdinal("CALRD_VAR"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return ent;
        }

        public int Actualizar(BETarifaReglaData data)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_REGLA_DATA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, data.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CALRD_ID", DbType.Decimal, data.CALRD_ID);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, data.CALR_ID);
            oDataBase.AddInParameter(oDbCommand, "@CALRD_VAR", DbType.String, data.CALRD_VAR);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, data.LOG_USER_CREAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

    }
}