using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;

namespace SGRDA.DA
{
    public class DACampaniaContactollamada
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BECampaniaContactollamada> ListarLoteCliente(string owner, decimal IdLote)
        {
            List<BECampaniaContactollamada> lista = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_CLIENTES_LOTE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONC_SID", DbType.Decimal, IdLote);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BECampaniaContactollamada item = null;
                    lista = new List<BECampaniaContactollamada>();

                    while (dr.Read())
                    {
                        item = new BECampaniaContactollamada();
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_MID")))
                            item.CONC_MID = dr.GetDecimal(dr.GetOrdinal("CONC_MID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_SID")))
                            item.CONC_SID = dr.GetDecimal(dr.GetOrdinal("CONC_SID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OBS_ID")))
                            item.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OBS_DESC")))
                            item.OBS_DESC = dr.GetString(dr.GetOrdinal("OBS_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OBS_VALUE")))
                            item.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_MEXPEC")))
                            item.CONC_MEXPEC = dr.GetDecimal(dr.GetOrdinal("CONC_MEXPEC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_MREAL")))
                            item.CONC_MREAL = dr.GetDecimal(dr.GetOrdinal("CONC_MREAL"));
                        item.LOG_DATE_CREATE = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OBS_TYPE")))
                            item.OBS_TYPE = dr.GetDecimal(dr.GetOrdinal("OBS_TYPE"));
                        lista.Add(item);
                    }
                }
                return lista;
            }
        }

        public int Insertar(BECampaniaContactollamada en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAI_CONTAC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddOutParameter(oDbCommand, "@CONC_MID", DbType.Decimal, Convert.ToInt32(en.CONC_MID));
            oDataBase.AddInParameter(oDbCommand, "@CONC_SID", DbType.Decimal, en.CONC_SID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, en.CONC_CID == 0 ? null : en.CONC_CID);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID == 0 ? null : en.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@OBS_ID", DbType.Decimal, en.OBS_ID == 0 ? null : en.OBS_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_MEXPEC", DbType.Decimal, en.CONC_MEXPEC == 0 ? null : en.CONC_MEXPEC);
            oDataBase.AddInParameter(oDbCommand, "@CONC_MREAL", DbType.Decimal, en.CONC_MREAL == 0 ? null : en.CONC_MREAL);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@CONC_MID"));
        }

        public int Actualizar(BECampaniaContactollamada en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAU_CONTAC_OBS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_MID", DbType.Decimal, en.CONC_MID);
            oDataBase.AddInParameter(oDbCommand, "@OBS_ID", DbType.Decimal, en.OBS_ID == 0 ? null : en.OBS_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_MEXPEC", DbType.Decimal, en.CONC_MEXPEC == 0 ? null : en.CONC_MEXPEC);
            oDataBase.AddInParameter(oDbCommand, "@CONC_MREAL", DbType.Decimal, en.CONC_MREAL == 0 ? null : en.CONC_MREAL);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BECampaniaContactollamada obtenerObservacion(string owner, decimal Id, decimal? IdObs)
        {
            BECampaniaContactollamada item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_OBTENER_OBS_CONTACTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONC_MID", DbType.String, Id);
                oDataBase.AddInParameter(cm, "@OBS_ID", DbType.String, IdObs);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BECampaniaContactollamada();
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_MID")))
                            item.CONC_MID = dr.GetDecimal(dr.GetOrdinal("CONC_MID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OBS_ID")))
                            item.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                    }
                    return item;
                }
            }
        }

        public List<BEObservationGral> ObservacionContactos(string owner)
        {
            List<BEObservationGral> lista = new List<BEObservationGral>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_DATOS_OBS_CONTACTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEObservationGral item = null;

                    while (dr.Read())
                    {
                        item = new BEObservationGral();
                        if (!dr.IsDBNull(dr.GetOrdinal("OBS_ID")))
                            item.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OBS_TYPE")))
                            item.OBS_TYPE = dr.GetInt32(dr.GetOrdinal("OBS_TYPE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENT_ID")))
                            item.ENT_ID = dr.GetInt32(dr.GetOrdinal("ENT_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OBS_VALUE")))
                            item.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                        lista.Add(item);
                    }
                    return lista;
                }
            }
        }
    }
}
