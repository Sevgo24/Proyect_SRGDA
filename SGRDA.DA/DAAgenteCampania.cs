using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using SGRDA.Entities;

namespace SGRDA.DA
{
    public class DAAgenteCampania
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAgenteCampania> AgenteXCampania(string owner, decimal idCampania)
        {
            List<BEAgenteCampania> lista = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_CONTACTO_CAMP"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@CONC_CID", DbType.Decimal, idCampania);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    BEAgenteCampania item = null;
                    lista = new List<BEAgenteCampania>();
                    while (dr.Read())
                    {
                        item = new BEAgenteCampania();
                        if (!dr.IsDBNull(dr.GetOrdinal("SEQUENCE")))
                            item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ROL_ID")))
                            item.ROL_ID = dr.GetString(dr.GetOrdinal("ROL_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ROL_DESC")))
                            item.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
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

        public BEAgenteCampania ObtenerAsoCamp(string owner, decimal id, decimal idCampania)
        {
            BEAgenteCampania item = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_ASOCIADO_CAMP"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@Id", DbType.String, id);
                db.AddInParameter(cm, "@CONC_CID", DbType.Decimal, idCampania);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEAgenteCampania();
                        if (!dr.IsDBNull(dr.GetOrdinal("SEQUENCE")))
                            item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ROL_ID")))
                            item.ROL_ID = dr.GetString(dr.GetOrdinal("ROL_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ROL_DESC")))
                            item.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    }
                }
            }
            return item;
        }

        public int Insertar(BEAgenteCampania en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAI_CAMPANIA_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, en.CONC_CID);
            db.AddInParameter(oDbCommand, "@ROL_ID", DbType.String, en.ROL_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Update(BEAgenteCampania en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ASOCIADO_CAMP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@Id", DbType.String, en.SEQUENCE);
            db.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, en.CONC_CID);
            db.AddInParameter(oDbCommand, "@ROL_ID", DbType.Decimal, en.ROL_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal id, decimal idCampania, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAD_ASOCIADO_CAMP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@Id", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, idCampania);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public int Activar(string owner, decimal id, decimal idCampania, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ACTIVAR_ASOCIADO_CAMP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@Id", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, idCampania);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEAgenteCampania> ListarAgenteRecaudador(string owner, decimal? IdCampania)
        {
            BEAgenteCampania item = null;
            List<BEAgenteCampania> lista = new List<BEAgenteCampania>();
            using (DbCommand cm = db.GetStoredProcCommand("SGRDAS_LISTAR_AGENTES_CAMP"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@CONC_CID", DbType.Decimal, IdCampania);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEAgenteCampania();
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
    }
}
