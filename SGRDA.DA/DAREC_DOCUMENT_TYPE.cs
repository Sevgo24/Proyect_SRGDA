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
    public class DAREC_DOCUMENT_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_DOCUMENT_TYPE> REC_DOCUMENT_TYPE_GET()
        {
            List<BEREC_DOCUMENT_TYPE> lst = new List<BEREC_DOCUMENT_TYPE>();
            BEREC_DOCUMENT_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_DOCUMENT_TYPE_GET"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_DOCUMENT_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DOC_TYPE = dr.GetDecimal(dr.GetOrdinal("DOC_TYPE"));
                            item.DOC_DESC = dr.GetString(dr.GetOrdinal("DOC_DESC"));
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

        public List<BEREC_DOCUMENT_TYPE> ListarPage(string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DOCUMENT_TYPE_GET_Page");
            db.AddInParameter(oDbCommand, "@param", DbType.String, param);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            
            var lista = new List<BEREC_DOCUMENT_TYPE>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_DOCUMENT_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public int Insertar(BEREC_DOCUMENT_TYPE en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DOCUMENT_TYPE_Ins");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@DOC_DESC", DbType.String, en.DOC_DESC.ToUpper());
            db.AddInParameter(oDbCommand, "@DOC_OBSERV", DbType.String, en.DOC_OBSERV != null ? en.DOC_OBSERV.ToString().ToUpper() : en.DOC_OBSERV);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEREC_DOCUMENT_TYPE en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DOCUMENT_TYPE_Upd_by_DOC_TYPE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@DOC_TYPE ", DbType.Decimal, en.DOC_TYPE);
            db.AddInParameter(oDbCommand, "@DOC_DESC ", DbType.String, en.DOC_DESC.ToUpper());
            db.AddInParameter(oDbCommand, "@DOC_OBSERV", DbType.String, en.DOC_OBSERV != null ? en.DOC_OBSERV.ToString().ToUpper() : en.DOC_OBSERV);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE ", DbType.String, en.LOG_USER_UPDATE);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, en.ESTADO);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public List<BEREC_DOCUMENT_TYPE> REC_DOCUMENT_TYPE_GET_by_DOC_TYPE(decimal DOC_TYPE)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DOCUMENT_TYPE_GET_by_DOC_TYPE", DOC_TYPE);
            var lista = new List<BEREC_DOCUMENT_TYPE>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_DOCUMENT_TYPE(reader));
            }
            return lista;
        }

        public int Eliminar(BEREC_DOCUMENT_TYPE en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DOCUMENT_TYPE_Del");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@DOC_TYPE", DbType.Decimal, en.DOC_TYPE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BEREC_DOCUMENT_TYPE Obtener(string Owner, decimal idTipo)
        {
            BEREC_DOCUMENT_TYPE item = null;
            using (DbCommand cm = db.GetStoredProcCommand("USP_REC_OBTIENE_DOC_TIPO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                db.AddInParameter(cm, "@DOC_TYPE", DbType.Decimal, idTipo);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_DOCUMENT_TYPE();
                        item.DOC_TYPE = dr.GetDecimal(dr.GetOrdinal("DOC_TYPE"));
                        item.DOC_DESC = dr.GetString(dr.GetOrdinal("DOC_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOC_OBSERV")))
                        {
                            item.DOC_OBSERV = dr.GetString(dr.GetOrdinal("DOC_OBSERV"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return item;
        }


        public List<BEREC_DOCUMENT_TYPE> ListarComboTipoDoc(string owner)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbComand = db.GetStoredProcCommand("SGRDASS_DOCUMENTO_TYPE");
                db.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

                var lista = new List<BEREC_DOCUMENT_TYPE>();
                BEREC_DOCUMENT_TYPE ent;
                using (IDataReader reader = db.ExecuteReader(oDbComand))
                {
                    while (reader.Read())
                    {
                        ent = new BEREC_DOCUMENT_TYPE();
                        ent.DOC_TYPE = Convert.ToDecimal(reader["DOC_TYPE"]);
                        ent.DOC_DESC = Convert.ToString(reader["DOC_DESC"]);
                        lista.Add(ent);
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool existeTipoDocumento(string Owner, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_EXISTE_TIPO_DOCUMENTO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(cm, "@DOC_DESC", DbType.String, nombre);
                db.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = db.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(db.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existeTipoDocumento(string Owner, decimal id, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_UPDATE_EXISTE_TIPO_DOCUMENTO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(cm, "@DOC_TYPE", DbType.Decimal, id);
                db.AddInParameter(cm, "@DOC_DESC", DbType.String, nombre);
                db.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = db.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(db.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public List<BEREC_DOCUMENT_TYPE> ListarComboTipoDocAlfresco(string owner)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbComand = db.GetStoredProcCommand("SGRDASS_DOCUMENTO_TYPE_ALFRESCO");
                //db.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

                var lista = new List<BEREC_DOCUMENT_TYPE>();
                BEREC_DOCUMENT_TYPE ent;
                using (IDataReader reader = db.ExecuteReader(oDbComand))
                {
                    while (reader.Read())
                    {
                        ent = new BEREC_DOCUMENT_TYPE();
                        ent.DOC_TYPE = Convert.ToDecimal(reader["DOC_TYPE"]);
                        ent.DOC_OBSERV = Convert.ToString(reader["DOC_OBSERV"]);
                        ent.DOC_DESC = Convert.ToString(reader["DOC_DESC"]);
                        lista.Add(ent);
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
