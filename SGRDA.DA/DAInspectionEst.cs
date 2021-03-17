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
    public class DAInspectionEst
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEInspectionEst> usp_Get_InspectionPage(string owner, decimal insId, decimal estId, decimal tipoest, decimal? subtipoest, decimal socio, string tipodiv, decimal? division, int pagina, int cantRegxPag)
        {
            if (subtipoest == null) subtipoest = 0;
            if (tipodiv == "0") tipodiv = string.Empty;
            if (division == null) division = 0;
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_INPECCION_POR_EST");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@INSP_ID", DbType.Decimal, insId);
            oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, estId);
            oDataBase.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, tipoest);
            oDataBase.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, subtipoest);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, socio);
            oDataBase.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, tipodiv);
            oDataBase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, division);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_INPECCION_POR_EST", owner, insId, estId, tipoest, subtipoest, socio, tipodiv, division, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEInspectionEst>();
            var inspection = new BEInspectionEst();

            using (IDataReader dr = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (dr.Read())
                {
                    inspection = new BEInspectionEst();
                    inspection.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    inspection.INSP_ID = dr.GetDecimal(dr.GetOrdinal("INSP_ID"));
                    inspection.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    inspection.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                    inspection.INSP_DOC = dr.GetString(dr.GetOrdinal("INSP_DOC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INSP_OBS")))
                        inspection.INSP_OBS = dr.GetString(dr.GetOrdinal("INSP_OBS"));
                    else
                        inspection.INSP_OBS = string.Empty;
                    inspection.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    inspection.INSP_DATE = dr.GetDateTime(dr.GetOrdinal("INSP_DATE"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        inspection.ESTADO = "ACTIVO";
                    else
                        inspection.ESTADO = "INACTIVO";

                    inspection.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                    inspection.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(inspection);
                }
            }
            return lista;
        }

        public int Insertar(BEInspectionEst en)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_INSPECTIONS_EST");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, en.EST_ID);
                oDataBase.AddInParameter(oDbComand, "@INSP_DOC", DbType.String, en.INSP_DOC.ToUpper());
                oDataBase.AddInParameter(oDbComand, "@INSP_OBS", DbType.String, en.INSP_OBS.ToUpper());
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
                oDataBase.AddInParameter(oDbComand, "@INSP_DATE", DbType.DateTime, en.INSP_DATE);
                oDataBase.AddInParameter(oDbComand, "@INSP_HOUR", DbType.Time, en.INSP_HOUR);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                retorno = oDataBase.ExecuteNonQuery(oDbComand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int Actualizar(BEInspectionEst en)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_INSPECTIONS_EST");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(oDbComand, "@INSP_ID", DbType.Decimal, en.INSP_ID);
                oDataBase.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, en.EST_ID);
                oDataBase.AddInParameter(oDbComand, "@INSP_DOC", DbType.String, en.INSP_DOC.ToUpper());
                oDataBase.AddInParameter(oDbComand, "@INSP_OBS", DbType.String, en.INSP_OBS.ToUpper());
                oDataBase.AddInParameter(oDbComand, "@INSP_DATE", DbType.DateTime, en.INSP_DATE);
                oDataBase.AddInParameter(oDbComand, "@INSP_HOUR", DbType.Time, en.INSP_HOUR);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                retorno = oDataBase.ExecuteNonQuery(oDbComand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int Activar(string owner, decimal insId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_INS_EST");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@INSP_ID", DbType.Decimal, insId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BEInspectionEst en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_INSPECTIONS_EST");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@INSP_ID", DbType.Decimal, en.INSP_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEInspectionEst Obtener(string owner, decimal idIns)
        {
            BEInspectionEst Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_INS_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@INSP_ID", DbType.Decimal, idIns);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEInspectionEst();
                        Obj.INSP_ID = dr.GetDecimal(dr.GetOrdinal("INSP_ID"));
                        Obj.INSP_DOC = dr.GetString(dr.GetOrdinal("INSP_DOC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INSP_OBS")))
                            Obj.INSP_OBS = dr.GetString(dr.GetOrdinal("INSP_OBS"));
                        Obj.INSP_DATE = dr.GetDateTime(dr.GetOrdinal("INSP_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INSP_HOUR")))
                            Obj.INSP_HOUR = dr.GetDateTime(dr.GetOrdinal("INSP_HOUR"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOUR")))
                            Obj.HOUR = dr.GetDateTime(dr.GetOrdinal("HOUR")).ToShortTimeString();

                        Obj.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        Obj.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    }
                }
            }
            return Obj;
        }

        public List<BEInspectionEst> InspeccionXEstablecimiento(decimal idEstablecimiento, string owner)
        {
            List<BEInspectionEst> inspeccion = null;
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_INS_EST"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        BEInspectionEst Obj = null;
                        inspeccion = new List<BEInspectionEst>();
                        while (dr.Read())
                        {
                            Obj = new BEInspectionEst();
                            Obj.INSP_ID = dr.GetDecimal(dr.GetOrdinal("INSP_ID"));
                            Obj.INSP_DOC = dr.GetString(dr.GetOrdinal("INSP_DOC"));
                            Obj.INSP_OBS = dr.GetString(dr.GetOrdinal("INSP_OBS"));
                            Obj.INSP_DATE = dr.GetDateTime(dr.GetOrdinal("INSP_DATE"));
                            Obj.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                            inspeccion.Add(Obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return inspeccion;
        }
    }
}
