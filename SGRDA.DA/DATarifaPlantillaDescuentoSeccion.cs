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
    public class DATarifaPlantillaDescuentoSeccion
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaPlantillaDescuentoSeccion seccion)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_PLANTILLA_DESC_SECCION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, seccion.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_CHAR", DbType.Decimal, seccion.TEMP_ID_DSC_CHAR);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, seccion.TEMP_ID_DSC);
            oDataBase.AddInParameter(oDbCommand, "@SECC_DESC", DbType.String, seccion.SECC_DESC);
            oDataBase.AddInParameter(oDbCommand, "@SECC_FROM", DbType.Decimal, seccion.SECC_FROM);
            oDataBase.AddInParameter(oDbCommand, "@SECC_TO", DbType.Decimal, seccion.SECC_TO);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, seccion.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@TEMP_ID_DSC_VAL", DbType.Decimal, Convert.ToInt32(seccion.TEMP_ID_DSC_VAL));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@TEMP_ID_DSC_VAL"));
            return id;
        }

        public int Actualizar(BETarifaPlantillaDescuentoSeccion valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_SECCION_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_VAL", DbType.Decimal, valor.TEMP_ID_DSC_VAL);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_CHAR", DbType.Decimal, valor.TEMP_ID_DSC_CHAR);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, valor.TEMP_ID_DSC);

            oDataBase.AddInParameter(oDbCommand, "@SECC_DESC", DbType.String, valor.SECC_DESC);
            oDataBase.AddInParameter(oDbCommand, "@SECC_FROM", DbType.String, valor.SECC_FROM);
            if (valor.SECC_TO != -1)
                oDataBase.AddInParameter(oDbCommand, "@SECC_TO", DbType.String, valor.SECC_TO);
            else
                oDataBase.AddInParameter(oDbCommand, "@SECC_TO", DbType.String, DBNull.Value);

            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valor.LOG_USER_UPDATE);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BETarifaPlantillaDescuentoSeccion valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_SECCION_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_VAL", DbType.Decimal, valor.TEMP_ID_DSC_VAL);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_CHAR", DbType.Decimal, valor.TEMP_ID_DSC_CHAR);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, valor.TEMP_ID_DSC);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valor.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BETarifaPlantillaDescuentoSeccion Obtener(string owner, decimal idValor, decimal idCaracteristica, decimal idPlantilla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SECCION_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_VAL", DbType.Decimal, idValor);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_CHAR", DbType.Decimal, idCaracteristica);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, idPlantilla);
            BETarifaPlantillaDescuentoSeccion ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaPlantillaDescuentoSeccion();
                    ent.TEMP_ID_DSC_VAL = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_VAL"));
                    ent.TEMP_ID_DSC_CHAR = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_CHAR"));
                    ent.TEMP_ID_DSC = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC"));
                    ent.SECC_DESC = dr.GetString(dr.GetOrdinal("SECC_DESC"));
                    ent.SECC_FROM = dr.GetDecimal(dr.GetOrdinal("SECC_FROM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SECC_TO"))) ent.SECC_TO = dr.GetDecimal(dr.GetOrdinal("SECC_TO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS"))) ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return ent;
        }



        public List<BETarifaPlantillaDescuentoSeccion> Listar(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_SECCIONES_CHAR_PLANTILLA_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, id);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BETarifaPlantillaDescuentoSeccion> lista = new List<BETarifaPlantillaDescuentoSeccion>();
            BETarifaPlantillaDescuentoSeccion seccion = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    seccion = new BETarifaPlantillaDescuentoSeccion();
                    seccion.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    seccion.TEMP_ID_DSC_VAL = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_VAL"));
                    seccion.TEMP_ID_DSC_CHAR = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_CHAR"));
                    seccion.TEMP_ID_DSC = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC"));
                    seccion.SECC_DESC = dr.GetString(dr.GetOrdinal("SECC_DESC"));
                    seccion.SECC_FROM = dr.GetDecimal(dr.GetOrdinal("SECC_FROM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SECC_TO"))) seccion.SECC_TO = dr.GetDecimal(dr.GetOrdinal("SECC_TO"));
                    seccion.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    seccion.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS"))) seccion.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                    seccion.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                    seccion.SECC_CHARSEQ = dr.GetDecimal(dr.GetOrdinal("SECC_CHARSEQ"));
                    lista.Add(seccion);
                }
            }
            return lista;
        }


    }
}
