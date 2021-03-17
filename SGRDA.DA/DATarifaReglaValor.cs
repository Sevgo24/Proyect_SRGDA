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
    public class DATarifaReglaValor
    {

        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public bool InsertarXML(string xml)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_REGLA_VALOR_XML"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    exito = oDataBase.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return exito;
        }

        public int Insertar(BETarifaReglaValor valor)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_REGLA_VALOR");

                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, valor.CALR_ID);
                oDataBase.AddInParameter(oDbCommand, "@CALRV_STARTS", DbType.DateTime, valor.CALRV_STARTS);

                if (valor.TEMPS1_ID != 0)
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS1_ID", DbType.Decimal, valor.TEMPS1_ID);
                else
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS1_ID", DbType.Decimal, DBNull.Value);

                if (valor.TEMPS2_ID != 0)
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS2_ID", DbType.Decimal, valor.TEMPS2_ID);
                else
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS2_ID", DbType.Decimal, DBNull.Value);

                if (valor.TEMPS3_ID != 0)
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS3_ID", DbType.Decimal, valor.TEMPS3_ID);
                else
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS3_ID", DbType.Decimal, DBNull.Value);

                if (valor.TEMPS4_ID != 0)
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS4_ID", DbType.Decimal, valor.TEMPS4_ID);
                else
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS4_ID", DbType.Decimal, DBNull.Value);

                if (valor.TEMPS5_ID != 0)
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS5_ID", DbType.Decimal, valor.TEMPS5_ID);
                else
                    oDataBase.AddInParameter(oDbCommand, "@TEMPS5_ID", DbType.Decimal, DBNull.Value);

                if (valor.CRI1_FROM != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI1_FROM", DbType.Decimal, valor.CRI1_FROM);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI1_FROM", DbType.Decimal, DBNull.Value);

                if (valor.CRI2_FROM != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI2_FROM", DbType.Decimal, valor.CRI2_FROM);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI2_FROM", DbType.Decimal, DBNull.Value);

                if (valor.CRI3_FROM != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI3_FROM", DbType.Decimal, valor.CRI3_FROM);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI3_FROM", DbType.Decimal, DBNull.Value);

                if (valor.CRI4_FROM != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI4_FROM", DbType.Decimal, valor.CRI4_FROM);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI4_FROM", DbType.Decimal, DBNull.Value);

                if (valor.CRI5_FROM != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI5_FROM", DbType.Decimal, valor.CRI5_FROM);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI5_FROM", DbType.Decimal, DBNull.Value);

                if (valor.CRI1_TO != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI1_TO", DbType.Decimal, valor.CRI1_TO);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI1_TO", DbType.Decimal, DBNull.Value);

                if (valor.CRI2_TO != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI2_TO", DbType.Decimal, valor.CRI2_TO);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI2_TO", DbType.Decimal, DBNull.Value);

                if (valor.CRI3_TO != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI3_TO", DbType.Decimal, valor.CRI3_TO);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI3_TO", DbType.Decimal, DBNull.Value);

                if (valor.CRI4_TO != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI4_TO", DbType.Decimal, valor.CRI4_TO);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI4_TO", DbType.Decimal, DBNull.Value);

                if (valor.CRI5_TO != 0)
                    oDataBase.AddInParameter(oDbCommand, "@CRI5_TO", DbType.Decimal, valor.CRI5_TO);
                else
                    oDataBase.AddInParameter(oDbCommand, "@CRI5_TO", DbType.Decimal, DBNull.Value);

                oDataBase.AddInParameter(oDbCommand, "@VAL_FORMULA", DbType.Decimal, valor.VAL_FORMULA);
                oDataBase.AddInParameter(oDbCommand, "@VAL_MINIMUM", DbType.Decimal, valor.VAL_MINIMUM);

                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, valor.LOG_USER_CREAT);

                int r = oDataBase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<BETarifaReglaValor> Listar(string owner, decimal idPlantilla, out int count)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_REGLA_TARIFA_MATRIZ");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, idPlantilla);
                oDataBase.AddOutParameter(oDbCommand, "@TOTAL_CAR", DbType.Int32, 0);
                oDataBase.ExecuteNonQuery(oDbCommand);

                int countCaracteristicas = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@TOTAL_CAR"));
                count = countCaracteristicas;
                List<BETarifaReglaValor> lista = new List<BETarifaReglaValor>();
                BETarifaReglaValor detalle = null;

                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        detalle = new BETarifaReglaValor();

                        if (!dr.IsDBNull(dr.GetOrdinal("Nro")))
                            detalle.CALRV_ID = dr.GetDecimal(dr.GetOrdinal("Nro"));
                        if (countCaracteristicas >= 1)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS1_ID")))
                                detalle.TEMPS1_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS1_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC1_DESC")))
                                detalle.SECC1_DESC = dr.GetString(dr.GetOrdinal("SECC1_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC1_FROM")))
                                detalle.CRI1_FROM = dr.GetDecimal(dr.GetOrdinal("SECC1_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC1_TO")))
                                detalle.CRI1_TO = dr.GetDecimal(dr.GetOrdinal("SECC1_TO"));
                        }

                        if (countCaracteristicas >= 2)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS2_ID")))
                                detalle.TEMPS2_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS2_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC2_DESC")))
                                detalle.SECC2_DESC = dr.GetString(dr.GetOrdinal("SECC2_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC2_FROM")))
                                detalle.CRI2_FROM = dr.GetDecimal(dr.GetOrdinal("SECC2_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC2_TO")))
                                detalle.CRI2_TO = dr.GetDecimal(dr.GetOrdinal("SECC2_TO"));
                        }

                        if (countCaracteristicas >= 3)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS3_ID")))
                                detalle.TEMPS3_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS3_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC3_DESC")))
                                detalle.SECC3_DESC = dr.GetString(dr.GetOrdinal("SECC3_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC3_FROM")))
                                detalle.CRI3_FROM = dr.GetDecimal(dr.GetOrdinal("SECC3_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC3_TO")))
                                detalle.CRI3_TO = dr.GetDecimal(dr.GetOrdinal("SECC3_TO"));
                        }

                        if (countCaracteristicas >= 4)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS4_ID")))
                                detalle.TEMPS4_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS4_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC4_DESC")))
                                detalle.SECC4_DESC = dr.GetString(dr.GetOrdinal("SECC4_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC4_FROM")))
                                detalle.CRI4_FROM = dr.GetDecimal(dr.GetOrdinal("SECC4_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC4_TO")))
                                detalle.CRI4_TO = dr.GetDecimal(dr.GetOrdinal("SECC4_TO"));
                        }

                        if (countCaracteristicas >= 5)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS5_ID")))
                                detalle.TEMPS5_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS5_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC5_DESC")))
                                detalle.SECC5_DESC = dr.GetString(dr.GetOrdinal("SECC5_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC5_FROM")))
                                detalle.CRI5_FROM = dr.GetDecimal(dr.GetOrdinal("SECC5_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC5_TO")))
                                detalle.CRI5_TO = dr.GetDecimal(dr.GetOrdinal("SECC5_TO"));
                        }
                        detalle.VAL_FORMULA = null;
                        detalle.VAL_MINIMUM = null;
                        lista.Add(detalle);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                count = 0;
                return null;
            }

        }

        public List<BETarifaReglaValor> ObtenerDatosListar(string owner, decimal id)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_REGLA_TARIFA_MATRIZ");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, id);
                //oDataBase.AddOutParameter(oDbCommand, "@TOTAL_CAR", DbType.Int32, 0);
                oDataBase.ExecuteNonQuery(oDbCommand);

                //int countCaracteristicas = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@TOTAL_CAR"));
                //count = countCaracteristicas;
                List<BETarifaReglaValor> lista = new List<BETarifaReglaValor>();
                BETarifaReglaValor detalle = null;

                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        detalle = new BETarifaReglaValor();

                        if (!dr.IsDBNull(dr.GetOrdinal("CALRV_ID")))
                            detalle.CALRV_ID = dr.GetDecimal(dr.GetOrdinal("CALRV_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("TEMPS1_ID")))
                            detalle.TEMPS1_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS1_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC1_DESC")))
                            detalle.SECC1_DESC = dr.GetString(dr.GetOrdinal("SECC1_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI1_FROM")))
                            detalle.CRI1_FROM = dr.GetDecimal(dr.GetOrdinal("CRI1_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI1_TO")))
                            detalle.CRI1_TO = dr.GetDecimal(dr.GetOrdinal("CRI1_TO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("TEMPS2_ID")))
                            detalle.TEMPS2_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS2_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC2_DESC")))
                            detalle.SECC2_DESC = dr.GetString(dr.GetOrdinal("SECC2_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI2_FROM")))
                            detalle.CRI2_FROM = dr.GetDecimal(dr.GetOrdinal("CRI2_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI2_TO")))
                            detalle.CRI2_TO = dr.GetDecimal(dr.GetOrdinal("CRI2_TO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("TEMPS3_ID")))
                            detalle.TEMPS3_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS3_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC3_DESC")))
                            detalle.SECC3_DESC = dr.GetString(dr.GetOrdinal("SECC3_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI3_FROM")))
                            detalle.CRI3_FROM = dr.GetDecimal(dr.GetOrdinal("CRI3_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI3_TO")))
                            detalle.CRI3_TO = dr.GetDecimal(dr.GetOrdinal("CRI3_TO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("TEMPS4_ID")))
                            detalle.TEMPS4_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS4_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC4_DESC")))
                            detalle.SECC4_DESC = dr.GetString(dr.GetOrdinal("SECC4_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI4_FROM")))
                            detalle.CRI4_FROM = dr.GetDecimal(dr.GetOrdinal("CRI4_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI4_TO")))
                            detalle.CRI4_TO = dr.GetDecimal(dr.GetOrdinal("CRI4_TO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("TEMPS5_ID")))
                            detalle.TEMPS5_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS5_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC5_DESC")))
                            detalle.SECC5_DESC = dr.GetString(dr.GetOrdinal("SECC5_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI5_FROM")))
                            detalle.CRI5_FROM = dr.GetDecimal(dr.GetOrdinal("CRI5_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI5_TO")))
                            detalle.CRI5_TO = dr.GetDecimal(dr.GetOrdinal("CRI5_TO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("VAL_FORMULA")))
                            detalle.VAL_FORMULA = dr.GetDecimal(dr.GetOrdinal("VAL_FORMULA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("VAL_MINIMUM")))
                            detalle.VAL_MINIMUM = dr.GetDecimal(dr.GetOrdinal("VAL_MINIMUM"));
                        lista.Add(detalle);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public int Actualizar(BETarifaReglaValor valor)
        {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_REGLA_VALOR");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@CALRV_ID", DbType.Decimal, valor.CALRV_ID);
                oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, valor.CALR_ID);
                oDataBase.AddInParameter(oDbCommand, "@VAL_FORMULA", DbType.Decimal, valor.VAL_FORMULA);
                oDataBase.AddInParameter(oDbCommand, "@VAL_MINIMUM", DbType.Decimal, valor.VAL_MINIMUM);
                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valor.LOG_USER_UPDATE);
                int r = oDataBase.ExecuteNonQuery(oDbCommand);
                return r;            
        }

        public int Eliminar(BETarifaReglaValor valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_REGLA_VALOR");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, valor.CALR_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valor.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BETarifaReglaValor Obtener(BETarifaReglaValor valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_REGLA_VALOR");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CALRV_ID", DbType.Decimal, valor.CALRV_ID);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.String, valor.CALR_ID);
            BETarifaReglaValor ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaReglaValor();
                    ent.CALRV_ID = dr.GetDecimal(dr.GetOrdinal("CALRV_ID"));
                    ent.CALR_ID = dr.GetDecimal(dr.GetOrdinal("CALR_ID"));
                    ent.VAL_FORMULA = dr.GetDecimal(dr.GetOrdinal("VAL_FORMULA"));
                    ent.VAL_MINIMUM = dr.GetDecimal(dr.GetOrdinal("VAL_MINIMUM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return ent;
        }

        public List<BETarifaReglaValor> ListarCaracteristicaEliminado(string owner, decimal idPlantilla, string chars, out int count)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_REGLA_TARIFA_MATRIZ_ELIMINAR_CHAR");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, idPlantilla);
                oDataBase.AddInParameter(oDbCommand, "@CHARS", DbType.String, chars);
                oDataBase.AddOutParameter(oDbCommand, "@TOTAL_CAR", DbType.Int32, 0);
                oDataBase.ExecuteNonQuery(oDbCommand);

                int countCaracteristicas = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@TOTAL_CAR"));
                count = countCaracteristicas;
                List<BETarifaReglaValor> lista = new List<BETarifaReglaValor>();
                BETarifaReglaValor detalle = null;

                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        detalle = new BETarifaReglaValor();

                        if (!dr.IsDBNull(dr.GetOrdinal("Nro")))
                            detalle.CALRV_ID = dr.GetDecimal(dr.GetOrdinal("Nro"));
                        if (countCaracteristicas >= 1)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS1_ID")))
                                detalle.TEMPS1_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS1_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC1_DESC")))
                                detalle.SECC1_DESC = dr.GetString(dr.GetOrdinal("SECC1_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC1_FROM")))
                                detalle.CRI1_FROM = dr.GetDecimal(dr.GetOrdinal("SECC1_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC1_TO")))
                                detalle.CRI1_TO = dr.GetDecimal(dr.GetOrdinal("SECC1_TO"));
                        }

                        if (countCaracteristicas >= 2)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS2_ID")))
                                detalle.TEMPS2_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS2_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC2_DESC")))
                                detalle.SECC2_DESC = dr.GetString(dr.GetOrdinal("SECC2_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC2_FROM")))
                                detalle.CRI2_FROM = dr.GetDecimal(dr.GetOrdinal("SECC2_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC2_TO")))
                                detalle.CRI2_TO = dr.GetDecimal(dr.GetOrdinal("SECC2_TO"));
                        }

                        if (countCaracteristicas >= 3)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS3_ID")))
                                detalle.TEMPS3_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS3_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC3_DESC")))
                                detalle.SECC3_DESC = dr.GetString(dr.GetOrdinal("SECC3_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC3_FROM")))
                                detalle.CRI3_FROM = dr.GetDecimal(dr.GetOrdinal("SECC3_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC3_TO")))
                                detalle.CRI3_TO = dr.GetDecimal(dr.GetOrdinal("SECC3_TO"));
                        }

                        if (countCaracteristicas >= 4)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS4_ID")))
                                detalle.TEMPS4_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS4_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC4_DESC")))
                                detalle.SECC4_DESC = dr.GetString(dr.GetOrdinal("SECC4_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC4_FROM")))
                                detalle.CRI4_FROM = dr.GetDecimal(dr.GetOrdinal("SECC4_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC4_TO")))
                                detalle.CRI4_TO = dr.GetDecimal(dr.GetOrdinal("SECC4_TO"));
                        }

                        if (countCaracteristicas >= 5)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("TEMPS5_ID")))
                                detalle.TEMPS5_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS5_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC5_DESC")))
                                detalle.SECC5_DESC = dr.GetString(dr.GetOrdinal("SECC5_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC5_FROM")))
                                detalle.CRI5_FROM = dr.GetDecimal(dr.GetOrdinal("SECC5_FROM"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SECC5_TO")))
                                detalle.CRI5_TO = dr.GetDecimal(dr.GetOrdinal("SECC5_TO"));
                        }
                        detalle.VAL_FORMULA = null;
                        detalle.VAL_MINIMUM = null;
                        lista.Add(detalle);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                count = 0;
                return null;
            }

        }

        public int ObtenerCantidadValores(string owner,decimal idRegla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_CANT_VALORES_REGLA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, idRegla);
            int r =Convert.ToInt32( oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ObtenerCantidadCarMatriz(string owner, decimal idRegla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CANTIDAD_CAR_REGLA_TARIFA_MATRIZ");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, idRegla);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

    }
}
