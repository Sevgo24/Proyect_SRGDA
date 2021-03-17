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
    public class DATarifaPlantillaValor
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BETarifaPlantillaValor> Listar(string owner, decimal id)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_VALORES_PLANTILLA_TARIFA");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, id);
                oDataBase.ExecuteNonQuery(oDbCommand);

                List<BETarifaPlantillaValor> lista = new List<BETarifaPlantillaValor>();
                BETarifaPlantillaValor valor = null;
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        valor = new BETarifaPlantillaValor();
                        valor.TEMPS_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS_ID"));
                        valor.TEMPL_ID = dr.GetDecimal(dr.GetOrdinal("TEMPL_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("IND_TR")))
                            valor.IND_TR = dr.GetString(dr.GetOrdinal("IND_TR"));
                        else
                            valor.IND_TR = "0";

                        valor.SECC_DESC = dr.GetString(dr.GetOrdinal("SECC_DESC"));
                        valor.SECC_FROM = dr.GetDecimal(dr.GetOrdinal("SECC_FROM"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SECC_TO")))
                            valor.SECC_TO = dr.GetDecimal(dr.GetOrdinal("SECC_TO"));
                        else
                            valor.SECC_TO = -1;

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            valor.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                        valor.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        valor.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        lista.Add(valor);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insertar(BETarifaPlantillaValor valor)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_VALOR_PLANTILLA_TARIFA");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, valor.TEMPL_ID);
                oDataBase.AddInParameter(oDbCommand, "@SECC_FROM", DbType.Decimal, valor.SECC_FROM);

                if (valor.SECC_TO != -1)
                    oDataBase.AddInParameter(oDbCommand, "@SECC_TO", DbType.Decimal, valor.SECC_TO);
                else
                    oDataBase.AddInParameter(oDbCommand, "@SECC_TO", DbType.Decimal, DBNull.Value);

                oDataBase.AddInParameter(oDbCommand, "@SECC_DESC", DbType.String, valor.SECC_DESC);
                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, valor.LOG_USER_CREAT);
                oDataBase.AddOutParameter(oDbCommand, "@TEMPS_ID", DbType.Decimal, Convert.ToInt32(valor.TEMPS_ID));

                int n = oDataBase.ExecuteNonQuery(oDbCommand);
                int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@TEMPS_ID"));
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public BETarifaPlantillaValor Obtener(string owner, decimal idValor, decimal idCaracteristica)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_VALOR_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@TEMPS_ID", DbType.Decimal, idValor);
            oDataBase.AddInParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, idCaracteristica);
            BETarifaPlantillaValor ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETarifaPlantillaValor();
                    ent.TEMPS_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS_ID"));
                    ent.TEMPL_ID = dr.GetDecimal(dr.GetOrdinal("TEMPL_ID"));
                    ent.SECC_FROM = dr.GetDecimal(dr.GetOrdinal("SECC_FROM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SECC_TO")))
                        ent.SECC_TO = dr.GetDecimal(dr.GetOrdinal("SECC_TO"));
                    ent.SECC_DESC = dr.GetString(dr.GetOrdinal("SECC_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return ent;
        }

        public int Actualizar(BETarifaPlantillaValor valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_VALOR_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMPS_ID", DbType.Decimal, valor.TEMPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, valor.TEMPL_ID);
            oDataBase.AddInParameter(oDbCommand, "@SECC_FROM", DbType.String, valor.SECC_FROM);
            if (valor.SECC_TO != -1)
                oDataBase.AddInParameter(oDbCommand, "@SECC_TO", DbType.String, valor.SECC_TO);
            else
                oDataBase.AddInParameter(oDbCommand, "@SECC_TO", DbType.String, DBNull.Value);
            oDataBase.AddInParameter(oDbCommand, "@SECC_DESC", DbType.String, valor.SECC_DESC);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valor.LOG_USER_UPDATE);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BETarifaPlantillaValor valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_VALOR_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMPS_ID", DbType.Decimal, valor.TEMPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, valor.TEMPL_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valor.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(BETarifaPlantillaValor valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_VALOR_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMPS_ID", DbType.Decimal, valor.TEMPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@TEMPL_ID", DbType.Decimal, valor.TEMPL_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valor.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BETarifaPlantillaValor> ListarValorRegla(string owner, decimal idPlantilla)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALORES_PLATILLA_REGLA");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, idPlantilla);
                oDataBase.ExecuteNonQuery(oDbCommand);

                List<BETarifaPlantillaValor> lista = new List<BETarifaPlantillaValor>();
                BETarifaPlantillaValor valor = null;
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        valor = new BETarifaPlantillaValor();
                        valor.TEMPS_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS_ID"));
                        valor.TEMPL_ID = dr.GetDecimal(dr.GetOrdinal("TEMPL_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("IND_TR")))
                            valor.IND_TR = dr.GetString(dr.GetOrdinal("IND_TR"));
                        else
                            valor.IND_TR = "0";

                        valor.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                        valor.CARACTERISTICA = dr.GetString(dr.GetOrdinal("CHAR_LONG"));
                        valor.SECC_DESC = dr.GetString(dr.GetOrdinal("SECC_DESC"));
                        valor.SECC_FROM = dr.GetDecimal(dr.GetOrdinal("SECC_FROM"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SECC_TO")))
                            valor.SECC_TO = dr.GetDecimal(dr.GetOrdinal("SECC_TO"));
                        else
                            valor.SECC_TO = -1;

                        lista.Add(valor);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BETarifaPlantillaValor> ListarValorReglaCaracteristicaEliminado(string owner, decimal id, string chars)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALORES_PLATILLA_REGLA_ELIMINAR_CHAR");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, id);
                oDataBase.AddInParameter(oDbCommand, "@CHARS", DbType.String, chars);
                oDataBase.ExecuteNonQuery(oDbCommand);

                List<BETarifaPlantillaValor> lista = new List<BETarifaPlantillaValor>();
                BETarifaPlantillaValor valor = null;
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        valor = new BETarifaPlantillaValor();
                        valor.TEMPS_ID = dr.GetDecimal(dr.GetOrdinal("TEMPS_ID"));
                        valor.TEMPL_ID = dr.GetDecimal(dr.GetOrdinal("TEMPL_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("IND_TR")))
                            valor.IND_TR = dr.GetString(dr.GetOrdinal("IND_TR"));
                        else
                            valor.IND_TR = "0";
                        valor.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                        valor.CARACTERISTICA = dr.GetString(dr.GetOrdinal("CHAR_LONG"));
                        valor.SECC_DESC = dr.GetString(dr.GetOrdinal("SECC_DESC"));
                        valor.SECC_FROM = dr.GetDecimal(dr.GetOrdinal("SECC_FROM"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SECC_TO")))
                            valor.SECC_TO = dr.GetDecimal(dr.GetOrdinal("SECC_TO"));
                        else
                            valor.SECC_TO = -1;

                        lista.Add(valor);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
