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
    public class DATarifaPlantillaDescuentoCaracteristica
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaPlantillaDescuentoCaracteristica caracteristica)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_PLANTILLA_DESC_CARACTERISTICAS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, caracteristica.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, caracteristica.TEMP_ID_DSC);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_ID", DbType.Decimal, caracteristica.CHAR_ID);
            oDataBase.AddInParameter(oDbCommand, "@SECC_CHARSEQ", DbType.Decimal, caracteristica.SECC_CHARSEQ);
            oDataBase.AddInParameter(oDbCommand, "@IND_TR", DbType.Decimal, caracteristica.IND_TR);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, caracteristica.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@TEMP_ID_DSC_CHAR", DbType.Decimal, Convert.ToInt32(caracteristica.TEMP_ID_DSC_CHAR));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@TEMP_ID_DSC_CHAR"));
            return id;
        }

        public int Actualizar(BETarifaPlantillaDescuentoCaracteristica caracteristica)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CHAR_PLANTILLA_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, caracteristica.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_CHAR", DbType.Decimal, caracteristica.TEMP_ID_DSC_CHAR);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, caracteristica.TEMP_ID_DSC);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_ID", DbType.String, caracteristica.CHAR_ID);
            oDataBase.AddInParameter(oDbCommand, "@IND_TR", DbType.Decimal, caracteristica.IND_TR);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, caracteristica.LOG_USER_UPDATE);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BETarifaPlantillaDescuentoCaracteristica caracteristica)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CHAR_PLANTILLA_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, caracteristica.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_CHAR", DbType.Decimal, caracteristica.TEMP_ID_DSC_CHAR);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, caracteristica.TEMP_ID_DSC);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, caracteristica.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
            
        public List<BETarifaPlantillaDescuentoCaracteristica> Listar(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CARACTERISTICA_PLANTILLA_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, id);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BETarifaPlantillaDescuentoCaracteristica> lista = new List<BETarifaPlantillaDescuentoCaracteristica>();
            BETarifaPlantillaDescuentoCaracteristica detalle = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    detalle = new BETarifaPlantillaDescuentoCaracteristica();
                    detalle.TEMP_ID_DSC_CHAR = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_CHAR"));
                    detalle.TEMP_ID_DSC = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC"));
                    detalle.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                    detalle.CHAR_SHORT = dr.GetString(dr.GetOrdinal("CHAR_SHORT"));
                    detalle.IND_TR = dr.GetDecimal(dr.GetOrdinal("IND_TR"));
                    detalle.IND_TR = dr.GetDecimal(dr.GetOrdinal("IND_TR"));
                    detalle.SECC_CHARSEQ = dr.GetDecimal(dr.GetOrdinal("SECC_CHARSEQ"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))  detalle.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))            detalle.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                    lista.Add(detalle);
                }
            }
            return lista;
        }

        public BETarifaPlantillaDescuentoCaracteristica Obtener(string owner, decimal idPlantilla, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_OBTENER_CHAR_PLANTILLA_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_CHAR", DbType.Decimal, id);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, idPlantilla);
            BETarifaPlantillaDescuentoCaracteristica ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaPlantillaDescuentoCaracteristica();
                    ent.TEMP_ID_DSC_CHAR = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_CHAR"));
                    ent.TEMP_ID_DSC = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC"));
                    ent.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                    ent.IND_TR = dr.GetDecimal(dr.GetOrdinal("IND_TR"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return ent;
        }


    }
}
