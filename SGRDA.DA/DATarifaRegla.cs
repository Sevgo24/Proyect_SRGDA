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
    public class DATarifaRegla
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaRegla regla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_REGLA");

            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, regla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@STARTS", DbType.DateTime, regla.STARTS);
            oDataBase.AddInParameter(oDbCommand, "@CALR_DESC", DbType.String, regla.CALR_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@RATE_FREQ", DbType.Decimal, regla.RATE_FREQ);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, regla.TEMP_ID);
            oDataBase.AddInParameter(oDbCommand, "@CALR_NVAR", DbType.Decimal, regla.CALR_NVAR);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ADJUST", DbType.String, regla.CALR_ADJUST);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ACCUM", DbType.String, regla.CALR_ACCUM);
            oDataBase.AddInParameter(oDbCommand, "@CALC_FORMULA", DbType.String, regla.CALC_FORMULA);
            oDataBase.AddInParameter(oDbCommand, "@CALC_MINIMUM", DbType.String, regla.CALC_MINIMUM);
            oDataBase.AddInParameter(oDbCommand, "@CALC_IFORMULA", DbType.String, regla.CALC_IFORMULA);
            oDataBase.AddInParameter(oDbCommand, "@CALC_IMINIMO", DbType.String, regla.CALC_IMINIMO);
            oDataBase.AddInParameter(oDbCommand, "@CALR_FOR_DEC", DbType.Decimal, regla.CALR_FOR_DEC);
            oDataBase.AddInParameter(oDbCommand, "@CALR_FOR_TYPE", DbType.String, regla.CALR_FOR_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@CALR_MIN_DEC", DbType.Decimal, regla.CALR_MIN_DEC);
            oDataBase.AddInParameter(oDbCommand, "@CALR_MIN_TYPE", DbType.String, regla.CALR_MIN_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@CALR_OBSERV", DbType.String, regla.CALR_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, regla.CUR_ALPHA);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, regla.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@CALR_ID", DbType.Decimal, Convert.ToInt32(regla.CALR_ID));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@CALR_ID"));
            return id;
        }

        public List<BETarifaRegla> Listar(string owner, string desc, decimal nro, DateTime fini, DateTime ffin, int estado, int periodocidad, int confecha,int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_REGLA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPCION", DbType.String, desc);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_NVAR", DbType.Decimal, nro);
            oDataBase.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, fini);
            oDataBase.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, ffin);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);
            oDataBase.AddInParameter(oDbCommand, "@PERIODOCIDAD", DbType.Int32, periodocidad);
            oDataBase.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, confecha);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BETarifaRegla>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                BETarifaRegla plantilla = null;
                while (dr.Read())
                {
                    plantilla = new BETarifaRegla();
                    plantilla.CALR_ID = dr.GetDecimal(dr.GetOrdinal("CALR_ID"));
                    plantilla.CALR_DESC = dr.GetString(dr.GetOrdinal("CALR_DESC")).ToUpper();
                    plantilla.STARTS = dr.GetDateTime(dr.GetOrdinal("STARTS"));
                    plantilla.CALR_NVAR = dr.GetDecimal(dr.GetOrdinal("CALR_NVAR"));
                    plantilla.PERIODOCIDAD = dr.GetString(dr.GetOrdinal("RAT_FDESC"));
                    plantilla.RATE_FREQ = dr.GetDecimal(dr.GetOrdinal("RATE_FREQ"));
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        plantilla.ESTADO = "ACTIVO";
                    else
                        plantilla.ESTADO = "INACTIVO";
                    plantilla.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(plantilla);
                }
            }
            return lista;
        }

        public BETarifaRegla Obtener(string owner, decimal id)
        {
            BETarifaRegla Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_REGLA_TARIFA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CALR_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETarifaRegla();
                        Obj.CALR_ID = dr.GetDecimal(dr.GetOrdinal("CALR_ID"));
                        Obj.STARTS = dr.GetDateTime(dr.GetOrdinal("STARTS"));
                        Obj.CALR_DESC = dr.GetString(dr.GetOrdinal("CALR_DESC")).ToUpper();
                        Obj.RATE_FREQ = dr.GetDecimal(dr.GetOrdinal("RATE_FREQ"));

                        Obj.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                        Obj.CALR_NVAR = dr.GetDecimal(dr.GetOrdinal("CALR_NVAR"));
                        Obj.CALR_ADJUST = dr.GetString(dr.GetOrdinal("CALR_ADJUST"));
                        Obj.CALR_ACCUM = dr.GetString(dr.GetOrdinal("CALR_ACCUM"));
                        Obj.CALC_FORMULA = dr.GetString(dr.GetOrdinal("CALC_FORMULA"));
                        Obj.CALC_MINIMUM = dr.GetString(dr.GetOrdinal("CALC_MINIMUM"));
                        Obj.CALC_IFORMULA = dr.GetString(dr.GetOrdinal("CALC_IFORMULA"));
                        Obj.CALC_IMINIMO = dr.GetString(dr.GetOrdinal("CALC_IMINIMO"));
                        Obj.CALR_FOR_DEC = dr.GetDecimal(dr.GetOrdinal("CALR_FOR_DEC"));
                        Obj.CALR_FOR_TYPE = dr.GetString(dr.GetOrdinal("CALR_FOR_TYPE"));
                        Obj.CALR_MIN_DEC = dr.GetDecimal(dr.GetOrdinal("CALR_MIN_DEC"));
                        Obj.CALR_MIN_TYPE = dr.GetString(dr.GetOrdinal("CALR_MIN_TYPE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("CALR_OBSERV")))
                            Obj.CALR_OBSERV = dr.GetString(dr.GetOrdinal("CALR_OBSERV"));
                        Obj.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));



                    }
                }
            }



            return Obj;
        }

        public int Actualizar(BETarifaRegla regla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_REGLA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, regla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, regla.CALR_ID);
            oDataBase.AddInParameter(oDbCommand, "@STARTS", DbType.DateTime, regla.STARTS);
            oDataBase.AddInParameter(oDbCommand, "@CALR_DESC", DbType.String, regla.CALR_DESC);
            oDataBase.AddInParameter(oDbCommand, "@RATE_FREQ", DbType.Decimal, regla.RATE_FREQ);
            oDataBase.AddInParameter(oDbCommand, "@CALR_NVAR", DbType.Decimal, regla.CALR_NVAR);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ADJUST", DbType.String, regla.CALR_ADJUST);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ACCUM", DbType.String, regla.CALR_ACCUM);
            oDataBase.AddInParameter(oDbCommand, "@CALC_FORMULA", DbType.String, regla.CALC_FORMULA);
            oDataBase.AddInParameter(oDbCommand, "@CALC_MINIMUM", DbType.String, regla.CALC_MINIMUM);
            oDataBase.AddInParameter(oDbCommand, "@CALC_IFORMULA", DbType.String, regla.CALC_IFORMULA);
            oDataBase.AddInParameter(oDbCommand, "@CALC_IMINIMO", DbType.String, regla.CALC_IMINIMO);
            oDataBase.AddInParameter(oDbCommand, "@CALR_FOR_DEC", DbType.Decimal, regla.CALR_FOR_DEC);
            oDataBase.AddInParameter(oDbCommand, "@CALR_FOR_TYPE", DbType.String, regla.CALR_FOR_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@CALR_MIN_DEC", DbType.Decimal, regla.CALR_MIN_DEC);
            oDataBase.AddInParameter(oDbCommand, "@CALR_MIN_TYPE", DbType.String, regla.CALR_MIN_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@CALR_OBSERV", DbType.String, regla.CALR_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, regla.CUR_ALPHA);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, regla.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BETarifaRegla> ListarRegla(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_REGLAS_MANT_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            List<BETarifaRegla> listar = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                listar = new List<BETarifaRegla>();
                BETarifaRegla regla = null;
                while (dr.Read())
                {
                    regla = new BETarifaRegla();
                    regla.CALR_ID= dr.GetDecimal (dr.GetOrdinal("CALR_ID")  );
                    regla.CALR_DESC = dr.GetString(dr.GetOrdinal("CALR_DESC"));
                    listar.Add(regla);
                }
            }
            return listar;
        }

        public List<BETarifaRegla> ListarReglaTarifaTest(string owner, decimal idTarifa)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_REGLA_TEST_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@RATE_ID", DbType.String, idTarifa);

            List<BETarifaRegla> listar = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                listar = new List<BETarifaRegla>();
                BETarifaRegla regla = null;
                while (dr.Read())
                {
                    regla = new BETarifaRegla();
                    regla.CALR_ID = dr.GetDecimal(dr.GetOrdinal("CALR_ID"));
                    regla.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                    regla.CALR_DESC = dr.GetString(dr.GetOrdinal("CALR_DESC"));
                    regla.RATE_CALC_VAR = dr.GetString(dr.GetOrdinal("RATE_CALC_VAR"));
                    regla.CALR_NVAR = dr.GetDecimal(dr.GetOrdinal("CALR_NVAR"));
                    regla.CALR_ADJUST = dr.GetString(dr.GetOrdinal("CALR_ADJUST"));
                    regla.CALR_ACCUM = dr.GetString(dr.GetOrdinal("CALR_ACCUM"));
                    regla.CALC_FORMULA = dr.GetString(dr.GetOrdinal("CALC_FORMULA"));
                    regla.CALC_MINIMUM = dr.GetString(dr.GetOrdinal("CALC_MINIMUM"));

                    regla.CALR_FOR_TYPE = dr.GetString(dr.GetOrdinal("CALR_FOR_TYPE"));
                    regla.CALR_FOR_DEC = dr.GetDecimal(dr.GetOrdinal("CALR_FOR_DEC"));
                    regla.CALR_MIN_TYPE = dr.GetString(dr.GetOrdinal("CALR_MIN_TYPE"));
                    regla.CALR_MIN_DEC = dr.GetDecimal(dr.GetOrdinal("CALR_MIN_DEC"));
                    listar.Add(regla);
                }
            }
            return listar;
        }

        public int Eliminar(BETarifaRegla regla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_ELIMINAR_REGLA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, regla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, regla.CALR_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, regla.LOG_USER_UPDATE);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        //Facturación Masiva
        public List<BETarifaRegla> ListarReglaTarifaTestXML(string owner,string xml)
        {
            List<BETarifaRegla> listar = new List<BETarifaRegla>();
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_REGLA_TEST_TARIFA_LICENCIA"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    BETarifaRegla regla = null;
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {                        
                        while (dr.Read())
                        {
                            regla = new BETarifaRegla();
                            regla.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                            regla.CALR_ID = dr.GetDecimal(dr.GetOrdinal("CALR_ID"));
                            regla.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                            regla.CALR_DESC = dr.GetString(dr.GetOrdinal("CALR_DESC"));
                            regla.RATE_CALC_VAR = dr.GetString(dr.GetOrdinal("RATE_CALC_VAR"));
                            regla.CALR_NVAR = dr.GetDecimal(dr.GetOrdinal("CALR_NVAR"));
                            regla.CALR_ADJUST = dr.GetString(dr.GetOrdinal("CALR_ADJUST"));
                            regla.CALR_ACCUM = dr.GetString(dr.GetOrdinal("CALR_ACCUM"));
                            regla.CALC_FORMULA = dr.GetString(dr.GetOrdinal("CALC_FORMULA"));
                            regla.CALC_MINIMUM = dr.GetString(dr.GetOrdinal("CALC_MINIMUM"));

                            regla.CALR_FOR_TYPE = dr.GetString(dr.GetOrdinal("CALR_FOR_TYPE"));
                            regla.CALR_FOR_DEC = dr.GetDecimal(dr.GetOrdinal("CALR_FOR_DEC"));
                            regla.CALR_MIN_TYPE = dr.GetString(dr.GetOrdinal("CALR_MIN_TYPE"));
                            regla.CALR_MIN_DEC = dr.GetDecimal(dr.GetOrdinal("CALR_MIN_DEC"));
                            listar.Add(regla);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listar;
        }


    }
}
