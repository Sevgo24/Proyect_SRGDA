using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;
using System.Data.Common;
using System.Data;

namespace SGRDA.DA
{
    public class DAREC_EST_SUBTYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_EST_SUBTYPE> REC_EST_SUBTYPE_GET()
        {
            List<BEREC_EST_SUBTYPE> lst = new List<BEREC_EST_SUBTYPE>();
            BEREC_EST_SUBTYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_EST_SUBTYPE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_EST_SUBTYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));
                            item.ESTT_ID = dr.GetDecimal(dr.GetOrdinal("ESTT_ID"));
                            item.DESCRIPTIONTYPE = dr.GetString(dr.GetOrdinal("DESCRIPTIONTYPE"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public List<BEREC_EST_SUBTYPE> ListarSubtipoEstablecimientoPorTipo(string Owner, decimal? IdTipo)
        {
            List<BEREC_EST_SUBTYPE> lst = new List<BEREC_EST_SUBTYPE>();
            BEREC_EST_SUBTYPE item = null; 
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_SUBTIPOEST_POR_TIPOEST"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    db.AddInParameter(cm, "@ESTT_ID", DbType.Decimal, IdTipo);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_EST_SUBTYPE();
                            item.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public List<BEREC_EST_SUBTYPE> usp_REC_EST_SUBTYPE_Page(string owner, string param, decimal TipoEst, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_EST_SUBTYPE_GET_Page");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@param", DbType.String, param);
            db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, TipoEst);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_EST_SUBTYPE>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_EST_SUBTYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_EST_SUBTYPE_Ins(BEREC_EST_SUBTYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_EST_SUBTYPE_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, en.ESTT_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool existeSubTipoEstablecimiento(string Owner, decimal id, string desc)
        {
            bool existe = false;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_EXISTE_SUBTIPO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(cm, "@ESTT_ID", DbType.Decimal, id);
                db.AddInParameter(cm, "@DESCRIPTION", DbType.String, desc);
                db.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = db.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(db.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existeSubTipoEstablecimiento(string Owner, decimal idEs, string desc, decimal Id)
        {
            bool existe = false;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_EXISTE_UPDATE_SUBTIPO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(cm, "@ESTT_ID", DbType.Decimal, idEs);
                db.AddInParameter(cm, "@DESCRIPTION", DbType.String, desc);
                db.AddInParameter(cm, "@SUBE_ID", DbType.Decimal, Id);
                db.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);

                int n = db.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(db.GetParameterValue(cm, "@RETORNO"));


            }
            return existe;
        }

        public bool REC_EST_SUBTYPE_Upd(BEREC_EST_SUBTYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_EST_SUBTYPE_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, en.SUBE_ID);
                db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, en.ESTT_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT ", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREC_EST_SUBTYPE> REC_EST_SUBTYPE_by_SUBE_ID(decimal SUBE_ID)
        {
            List<BEREC_EST_SUBTYPE> lst = new List<BEREC_EST_SUBTYPE>();
            BEREC_EST_SUBTYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_EST_SUBTYPE_GET_by_SUBE_ID"))
                {
                    db.AddInParameter(cm, "@SUBE_ID", DbType.Decimal, SUBE_ID);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_EST_SUBTYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));
                            item.ESTT_ID = dr.GetDecimal(dr.GetOrdinal("ESTT_ID"));
                            item.DESCRIPTIONTYPE = dr.GetString(dr.GetOrdinal("DESCRIPTIONTYPE"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public bool REC_EST_SUBTYPE_Del(decimal SUBE_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_EST_SUBTYPE_Del");
                db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, SUBE_ID);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
