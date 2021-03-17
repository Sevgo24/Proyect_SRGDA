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
    public class DARequerimientoDinero
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BERequerimientoDinero> Listar(string owner, decimal id, string tipo, string nro, string nombre, int estado, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_REQ_DINERO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, id);
            oDataBase.AddInParameter(oDbCommand, "@TAXT_ID", DbType.Decimal, tipo);
            oDataBase.AddInParameter(oDbCommand, "@TAX_ID", DbType.String, nro);
            oDataBase.AddInParameter(oDbCommand, "@BPS_NAME", DbType.String, nombre);
            oDataBase.AddInParameter(oDbCommand, "@STATE", DbType.Int32, estado);

            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BERequerimientoDinero>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BERequerimientoDinero(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BERequerimientoDinero> Listar_Page_Detalle_Gasto(int id, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_DETALLE_GASTO");
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Int32, id);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_DETALLE_GASTO", id, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BERequerimientoDinero>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BERequerimientoDinero(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public BERequerimientoDinero Obtener(string owner, decimal id)
        {
            BERequerimientoDinero Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_REQUERIMIENTO_GASTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@MNR_ID", DbType.String, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BERequerimientoDinero();
                        Obj.MNR_ID = dr.GetDecimal(dr.GetOrdinal("MNR_ID"));
                        Obj.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        Obj.STT_ID = dr.GetDecimal(dr.GetOrdinal("STT_ID"));
                        Obj.BPS_NAME = dr.GetString(dr.GetOrdinal("NAME"));
                        Obj.MNR_DESC = dr.GetString(dr.GetOrdinal("MNR_DESC"));
                        Obj.MNR_DATE = dr.GetDateTime(dr.GetOrdinal("MNR_DATE"));
                        Obj.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MNR_VALUE_PRE")))
                            Obj.MNR_VALUE_PRE = dr.GetDecimal(dr.GetOrdinal("MNR_VALUE_PRE"));
                        else
                            Obj.MNR_VALUE_PRE = 0;
                        if (!dr.IsDBNull(dr.GetOrdinal("MNR_VALUE_APR")))
                            Obj.MNR_VALUE_APR = dr.GetDecimal(dr.GetOrdinal("MNR_VALUE_APR"));
                        else
                            Obj.MNR_VALUE_APR = 0;
                        if (!dr.IsDBNull(dr.GetOrdinal("MNR_VALUE_CON")))
                            Obj.MNR_VALUE_CON = dr.GetDecimal(dr.GetOrdinal("MNR_VALUE_CON"));
                        else
                            Obj.MNR_VALUE_CON = 0;

                    }
                }
            }
            return Obj;
        }

        public int Insertar(BERequerimientoDinero req)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_REQ_DINERO_NUEVO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, req.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, req.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@STT_ID", DbType.Decimal, req.STT_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_DESC", DbType.String, req.MNR_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MNR_DATE", DbType.DateTime, req.MNR_DATE);
            oDataBase.AddInParameter(oDbCommand, "@MNR_VALUE_PRE", DbType.Decimal, req.MNR_VALUE_PRE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, req.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@MNR_ID", DbType.Decimal, Convert.ToInt32(req.MNR_ID));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@MNR_ID"));
            return id;
        }

        public int Actualizar(BERequerimientoDinero req)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_REQ_DINERO_NUEVO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, req.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, req.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@STT_ID", DbType.Decimal, req.STT_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_DESC", DbType.String, req.MNR_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MNR_DATE", DbType.DateTime, req.MNR_DATE);
            oDataBase.AddInParameter(oDbCommand, "@MNR_VALUE_PRE", DbType.Decimal, req.MNR_VALUE_PRE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, req.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar_Estado_Apro(BERequerimientoDinero req)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_REQ_DINERO_ESTADO_APRO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, req.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, req.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@STT_ID", DbType.Decimal, req.STT_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_VALUE_APR", DbType.Decimal, req.MNR_VALUE_APR);
            oDataBase.AddInParameter(oDbCommand, "@MNR_APP_USER", DbType.String, req.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar_Estado_Rendir(BERequerimientoDinero req)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_REQ_DINERO_ESTADO_RENDIR");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, req.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, req.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@STT_ID", DbType.Decimal, req.STT_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_VALUE_CON", DbType.Decimal, req.MNR_VALUE_CON);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, req.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int InsertarReintegro(BERequerimientoDinero req)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_REQ_DINERO_NUEVO_REINTEGRO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, req.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, req.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@STT_ID", DbType.Decimal, req.STT_ID);
            oDataBase.AddInParameter(oDbCommand, "@MNR_DESC", DbType.String, req.MNR_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MNR_DATE", DbType.DateTime, req.MNR_DATE);
            oDataBase.AddInParameter(oDbCommand, "@MNR_VALUE_PRE", DbType.Decimal, req.MNR_VALUE_PRE);
            oDataBase.AddInParameter(oDbCommand, "@MNR_VALUE_APR", DbType.Decimal, req.MNR_VALUE_APR);
            oDataBase.AddInParameter(oDbCommand, "@MNR_VALUE_CON", DbType.Decimal, req.MNR_VALUE_CON);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, req.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@MNR_ID", DbType.Decimal, Convert.ToInt32(req.MNR_ID));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@MNR_ID"));
            return id;
        }


    }
}
