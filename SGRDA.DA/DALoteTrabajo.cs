using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;
using System.Data.Common;
using System.Data;

namespace SGRDA.DA
{
    public class DALoteTrabajo
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BELoteTrabajo> ListarLoteTrabajo(string owner, decimal Idcampania)
        {
            List<BELoteTrabajo> lista = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_LOTE_TRA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONC_CID", DbType.Decimal, Idcampania);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BELoteTrabajo item = null;
                    lista = new List<BELoteTrabajo>();
                    while (dr.Read())
                    {
                        item = new BELoteTrabajo();
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_SID")))
                            item.CONC_SID = dr.GetDecimal(dr.GetOrdinal("CONC_SID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_SDATEINI")))
                            item.CONC_SDATEINI = dr.GetDateTime(dr.GetOrdinal("CONC_SDATEINI"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_SDATEND")))
                            item.CONC_SDATEND = dr.GetDateTime(dr.GetOrdinal("CONC_SDATEND"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                            item.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public int Insertar(BELoteTrabajo en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAI_LOTE_TRABAJO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_SDATEINI", DbType.DateTime, en.CONC_SDATEINI);
            oDataBase.AddInParameter(oDbCommand, "@CONC_SDATEND", DbType.DateTime, en.CONC_SDATEND);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, en.CONC_CID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Update(BELoteTrabajo en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAU_LOTE_TRABAJO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_SID", DbType.Decimal, en.CONC_SID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_SDATEINI", DbType.DateTime, en.CONC_SDATEINI);
            oDataBase.AddInParameter(oDbCommand, "@CONC_SDATEND", DbType.DateTime, en.CONC_SDATEND);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, en.CONC_CID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BELoteTrabajo ObtenerLoteTrabajo(string owner, decimal IdLote)
        {
            BELoteTrabajo item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LOTE_TRABAJO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONC_SID", DbType.Decimal, IdLote);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELoteTrabajo();
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_SDATEINI")))
                            item.CONC_SDATEINI = dr.GetDateTime(dr.GetOrdinal("CONC_SDATEINI"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_SDATEND")))
                            item.CONC_SDATEND = dr.GetDateTime(dr.GetOrdinal("CONC_SDATEND"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_CID")))
                            item.CONC_CID = dr.GetDecimal(dr.GetOrdinal("CONC_CID"));
                    }
                }
            }
            return item;
        }

        public int Eliminar(string owner, decimal id, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAD_LOTE_TRAB");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONC_SID", DbType.Decimal, id);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(string owner, decimal id, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAU_LOTE_TRAB");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONC_SID", DbType.Decimal, id);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BELoteTrabajo> ListaLoteAgente(string owner, decimal IdCampania)
        {
            BELoteTrabajo item = null;
            List<BELoteTrabajo> lista = new List<BELoteTrabajo>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_LOTE_AGENTE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONC_CID", DbType.Decimal, IdCampania);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BELoteTrabajo();
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_SID")))
                            item.CONC_SID = dr.GetDecimal(dr.GetOrdinal("CONC_SID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOTEAGENTE")))
                            item.LOTEAGENTE = dr.GetString(dr.GetOrdinal("LOTEAGENTE"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
    }
}
