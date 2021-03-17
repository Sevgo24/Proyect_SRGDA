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
    public class DADetalleGasto
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEDetalleGasto> Listar_Page_DetalleGasto(int id, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_DETALLE_GASTO");
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Int32, id);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_DETALLE_GASTO", id, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEDetalleGasto>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEDetalleGasto(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEDetalleGasto> Listar(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DETALLE_GASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, id);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BEDetalleGasto> lista = new List<BEDetalleGasto>();
            BEDetalleGasto detalle = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    detalle = new BEDetalleGasto();
                    detalle.MNR_DET_ID = dr.GetDecimal(dr.GetOrdinal("MNR_DET_ID"));
                    detalle.MNR_ID = dr.GetDecimal(dr.GetOrdinal("MNR_ID"));
                    detalle.EXP_TYPE = dr.GetString(dr.GetOrdinal("EXP_TYPE"));
                    detalle.EXPG_ID = dr.GetString(dr.GetOrdinal("EXPG_ID"));
                    detalle.EXP_ID = dr.GetString(dr.GetOrdinal("EXP_ID"));
                    detalle.EXP_DESC = dr.GetString(dr.GetOrdinal("EXP_DESC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("EXP_VAL_PRE")))
                        detalle.EXP_VAL_PRE = dr.GetDecimal(dr.GetOrdinal("EXP_VAL_PRE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("EXP_VAL_APR")))
                        detalle.EXP_VAL_APR = dr.GetDecimal(dr.GetOrdinal("EXP_VAL_APR"));

                    if (!dr.IsDBNull(dr.GetOrdinal("EXP_VAL_CON")))
                        detalle.EXP_VAL_CON = dr.GetDecimal(dr.GetOrdinal("EXP_VAL_CON"));

                    detalle.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    detalle.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        detalle.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                        detalle.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        detalle.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(detalle);
                }

            }
            return lista;
        }

        public int Insertar(BEDetalleGasto detalle)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_DETALLE_REQ_DINERO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, detalle.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, detalle.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@EXP_ID", DbType.String, detalle.EXP_ID);
            oDataBase.AddInParameter(oDbCommand, "@LEG_ID", DbType.Decimal, detalle.LEG_ID);
            oDataBase.AddInParameter(oDbCommand, "@EXP_VAL_PRE", DbType.Decimal, detalle.EXP_VAL_PRE);
            oDataBase.AddInParameter(oDbCommand, "@EXP_VAL_APR", DbType.Decimal, detalle.EXP_VAL_APR);
            oDataBase.AddInParameter(oDbCommand, "@EXP_VAL_CON", DbType.Decimal, detalle.EXP_VAL_CON);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, detalle.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@MNR_DET_ID", DbType.Decimal, 0);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@MNR_DET_ID"));
            return id;
        }

        public int Actualizar(BEDetalleGasto detalle)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_DETALLE_REQ_DINERO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, detalle.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_DET_ID", DbType.Decimal, detalle.MNR_DET_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, detalle.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@EXP_ID", DbType.String, detalle.EXP_ID);
            oDataBase.AddInParameter(oDbCommand, "@LEG_ID", DbType.Decimal, detalle.LEG_ID);
            oDataBase.AddInParameter(oDbCommand, "@EXP_VAL_PRE", DbType.Decimal, detalle.EXP_VAL_PRE);
            oDataBase.AddInParameter(oDbCommand, "@EXP_VAL_APR", DbType.Decimal, detalle.EXP_VAL_APR);
            oDataBase.AddInParameter(oDbCommand, "@EXP_VAL_CON", DbType.Decimal, detalle.EXP_VAL_CON);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, detalle.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BEDetalleGasto detalle)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_DETALLE_REQ_DINERO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, detalle.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_DET_ID", DbType.Decimal, detalle.MNR_DET_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, detalle.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, detalle.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(BEDetalleGasto detalle)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_DETALLE_REQ_DINERO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, detalle.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_DET_ID", DbType.Decimal, detalle.MNR_DET_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, detalle.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, detalle.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEDetalleGasto Obtener(string owner, decimal idDet, decimal id)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DETALLE_REQ_DINERO");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@MNR_DET_ID", DbType.Decimal, idDet);
                oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, id);
                BEDetalleGasto ent = null;
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        ent = new BEDetalleGasto();
                        ent.MNR_DET_ID = dr.GetDecimal(dr.GetOrdinal("MNR_DET_ID"));
                        ent.MNR_ID = dr.GetDecimal(dr.GetOrdinal("MNR_ID"));
                        ent.EXP_ID = dr.GetString(dr.GetOrdinal("EXP_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LEG_ID")))
                            ent.LEG_ID = dr.GetDecimal(dr.GetOrdinal("LEG_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("EXP_VAL_PRE")))
                            ent.EXP_VAL_PRE = dr.GetDecimal(dr.GetOrdinal("EXP_VAL_PRE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("EXP_VAL_APR")))
                            ent.EXP_VAL_APR = dr.GetDecimal(dr.GetOrdinal("EXP_VAL_APR"));

                        if (!dr.IsDBNull(dr.GetOrdinal("EXP_VAL_CON")))
                            ent.EXP_VAL_CON = dr.GetDecimal(dr.GetOrdinal("EXP_VAL_CON"));

                        ent.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        ent.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            ent.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                            ent.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("EXP_TYPE")))
                            ent.EXP_TYPE = dr.GetString(dr.GetOrdinal("EXP_TYPE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("EXPG_ID")))
                            ent.EXPG_ID = dr.GetString(dr.GetOrdinal("EXPG_ID"));
                    }
                }
                return ent;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public int Actualizar_Apro(BEDetalleGasto detalle)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_DETALLE_REQ_DINERO_APRO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, detalle.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_DET_ID", DbType.Decimal, detalle.MNR_DET_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, detalle.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@EXP_VAL_APR", DbType.Decimal, detalle.EXP_VAL_APR);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, detalle.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar_Rendir(BEDetalleGasto detalle)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_DETALLE_REQ_DINERO_RENDIR");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, detalle.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_DET_ID", DbType.Decimal, detalle.MNR_DET_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, detalle.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@EXP_VAL_CON", DbType.Decimal, detalle.EXP_VAL_CON);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, detalle.LOG_USER_UPDAT);
            oDataBase.AddInParameter(oDbCommand, "@LEG_ID", DbType.Decimal, detalle.LEG_ID);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

    }
}
