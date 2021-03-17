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
    public class DATarifaPlantillaCaracteristica
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaPlantillaCaracteristica caracteristica)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_CARACTERISTICA_TARIFA_PLANTILLA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, caracteristica.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, caracteristica.TEMP_ID);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_ID", DbType.String, caracteristica.CHAR_ID);
            oDataBase.AddInParameter(oDbCommand, "@IND_TR", DbType.String, caracteristica.IND_TR);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, caracteristica.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, Convert.ToInt32(caracteristica.TEMPL_ID));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "TEMPL_ID"));
            return id;
        }

        public List<BETarifaPlantillaCaracteristica> Listar(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CARACTERISTICA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, id);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BETarifaPlantillaCaracteristica> lista = new List<BETarifaPlantillaCaracteristica>();
            BETarifaPlantillaCaracteristica detalle = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    detalle = new BETarifaPlantillaCaracteristica();
                    detalle.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                    detalle.TEMPL_ID = dr.GetDecimal(dr.GetOrdinal("TEMPL_ID"));
                    detalle.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));

                    detalle.IND_TR = dr.GetString(dr.GetOrdinal("IND_TR"));
                    detalle.CHAR_LONG = dr.GetString(dr.GetOrdinal("CHAR_LONG"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        detalle.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                    detalle.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        detalle.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                    lista.Add(detalle);
                }

            }
            return lista;
        }

        public BETarifaPlantillaCaracteristica Obtener(string owner, decimal idPlantilla, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CARACTERISTICA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, idPlantilla);
            oDataBase.AddInParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, id);
            BETarifaPlantillaCaracteristica ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaPlantillaCaracteristica();
                    ent.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                    ent.TEMPL_ID = dr.GetDecimal(dr.GetOrdinal("TEMPL_ID"));
                    ent.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                    ent.IND_TR = dr.GetString(dr.GetOrdinal("IND_TR"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return ent;
        }

        public int Actualizar(BETarifaPlantillaCaracteristica caracteristica)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CARACTERISTICA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, caracteristica.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, caracteristica.TEMPL_ID);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, caracteristica.TEMP_ID);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_ID", DbType.String, caracteristica.CHAR_ID);
            oDataBase.AddInParameter(oDbCommand, "@IND_TR", DbType.String, caracteristica.IND_TR);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, caracteristica.LOG_USER_UPDATE);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BETarifaPlantillaCaracteristica caracteristica)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CARACTERISTICA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, caracteristica.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, caracteristica.TEMPL_ID);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, caracteristica.TEMP_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, caracteristica.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(BETarifaPlantillaCaracteristica caracteristica)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_CARACTERISTICA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, caracteristica.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, caracteristica.TEMPL_ID);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, caracteristica.TEMP_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, caracteristica.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

    }
}
