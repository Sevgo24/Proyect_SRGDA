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
    public class DATarifaPlantillaDescuentoValores
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaPlantillaDescuentoValores plantilla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_VALORES_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, plantilla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, plantilla.TEMP_ID_DSC);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_VAL_1", DbType.Decimal, plantilla.TEMP_ID_DSC_VAL_1);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_VAL_2", DbType.String, plantilla.TEMP_ID_DSC_VAL_2);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_VAL_3", DbType.String, plantilla.TEMP_ID_DSC_VAL_3);
            oDataBase.AddInParameter(oDbCommand, "@VAL_FORMULA", DbType.String, plantilla.VAL_FORMULA);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, plantilla.LOG_USER_CREAT);
            int result = oDataBase.ExecuteNonQuery(oDbCommand);
            return result;
        }

        public int Actualizar(BETarifaPlantillaDescuentoValores plantilla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_VALORES_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, plantilla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_MAT", DbType.Decimal, plantilla.TEMP_ID_DSC_MAT);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, plantilla.TEMP_ID_DSC);
            oDataBase.AddInParameter(oDbCommand, "@VAL_FORMULA", DbType.Decimal, plantilla.VAL_FORMULA);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, plantilla.LOG_USER_UPDATE);
            int result = oDataBase.ExecuteNonQuery(oDbCommand);
            return result;
        }

        public BETarifaPlantillaDescuentoValores Obtener(string owner, decimal id, decimal idPlantilla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_VALORES_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC_MAT", DbType.Decimal, id);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, idPlantilla);
            BETarifaPlantillaDescuentoValores ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaPlantillaDescuentoValores();
                    ent.TEMP_ID_DSC_MAT = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_MAT"));
                    ent.TEMP_ID_DSC = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC"));
                    ent.VAL_FORMULA = dr.GetDecimal(dr.GetOrdinal("VAL_FORMULA"));
                }
            }
            return ent;
        }

        public int EliminarMatriz(BETarifaPlantillaDescuento plantilla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_ELIMINAR_MATRIZ_DSC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, plantilla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, plantilla.TEMP_ID_DSC);
            int result = oDataBase.ExecuteNonQuery(oDbCommand);
            return result;
        }

        public List<BETarifaPlantillaDescuentoValores> Listar(string owner, decimal idPlantilla)
        {
            List<BETarifaPlantillaDescuentoValores> ListaMatriz = new List<BETarifaPlantillaDescuentoValores>();
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_VALORES_DSC"))
                {
                    oDataBase.AddInParameter(cm, "@TEMP_ID_DSC", DbType.Decimal, idPlantilla);
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        BETarifaPlantillaDescuentoValores matriz = null;
                        while (dr.Read())
                        {
                            matriz = new BETarifaPlantillaDescuentoValores();
                            matriz.TEMP_ID_DSC_MAT = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_MAT"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMP_ID_DSC_VAL_1")))
                                matriz.TEMP_ID_DSC_VAL_1 = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_VAL_1"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC_DESC_1")))
                                matriz.TEMP_SEC_DESC_1 = dr.GetString(dr.GetOrdinal("SECC_DESC_1"));

                            if (!dr.IsDBNull(dr.GetOrdinal("TEMP_ID_DSC_VAL_2")))
                                matriz.TEMP_ID_DSC_VAL_2 = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_VAL_2"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC_DESC_2")))
                                matriz.TEMP_SEC_DESC_2 = dr.GetString(dr.GetOrdinal("SECC_DESC_2"));

                            if (!dr.IsDBNull(dr.GetOrdinal("TEMP_ID_DSC_VAL_3")))
                                matriz.TEMP_ID_DSC_VAL_3 = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_VAL_3"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC_DESC_3")))
                                matriz.TEMP_SEC_DESC_3 = dr.GetString(dr.GetOrdinal("SECC_DESC_3"));

                            matriz.VAL_FORMULA = dr.GetDecimal(dr.GetOrdinal("VAL_FORMULA"));
                            ListaMatriz.Add(matriz);
                        }
                    }
                    return ListaMatriz;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListaMatriz;
        }

        public List<BETarifaPlantillaDescuentoValores> GenerarMatrizTemporalXML(string xml, int Cantidad)
        {
            List<BETarifaPlantillaDescuentoValores> ListaMatriz = new List<BETarifaPlantillaDescuentoValores>();
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_MATRIZ_TMP_XML"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    oDataBase.AddInParameter(cm, "@Cantidad", DbType.Int32, Cantidad);
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        BETarifaPlantillaDescuentoValores matriz = null;
                        while (dr.Read())
                        {
                            matriz = new BETarifaPlantillaDescuentoValores();
                            if (Cantidad >= 1)
                            {
                                if (!dr.IsDBNull(dr.GetOrdinal("TEMP_ID_DSC_VAL_1")))
                                    matriz.TEMP_ID_DSC_VAL_1 = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_VAL_1"));
                                if (!dr.IsDBNull(dr.GetOrdinal("SECC_DESC_1")))
                                    matriz.TEMP_SEC_DESC_1 = dr.GetString(dr.GetOrdinal("SECC_DESC_1"));
                            }

                            if (Cantidad >= 2)
                            {
                                if (!dr.IsDBNull(dr.GetOrdinal("TEMP_ID_DSC_VAL_2")))
                                    matriz.TEMP_ID_DSC_VAL_2 = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_VAL_2"));
                                if (!dr.IsDBNull(dr.GetOrdinal("SECC_DESC_2")))
                                    matriz.TEMP_SEC_DESC_2 = dr.GetString(dr.GetOrdinal("SECC_DESC_2"));
                            }

                            if (Cantidad >= 3)
                            {
                                if (!dr.IsDBNull(dr.GetOrdinal("TEMP_ID_DSC_VAL_3")))
                                    matriz.TEMP_ID_DSC_VAL_3 = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC_VAL_3"));
                                if (!dr.IsDBNull(dr.GetOrdinal("SECC_DESC_3")))
                                    matriz.TEMP_SEC_DESC_3 = dr.GetString(dr.GetOrdinal("SECC_DESC_3"));
                            }
                            ListaMatriz.Add(matriz);
                        }
                    }
                    return ListaMatriz;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListaMatriz;
        }


    }
}
