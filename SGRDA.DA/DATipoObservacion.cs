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
    public class DATipoObservacion
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BEObservationType> Listar_Page_TipoObservacion(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPOOBSERVACION");
            oDataBase.AddInParameter(oDbCommand, "@OBS_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEObservationType>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEObservationType(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEREC_OBSERVATION_TYPE> Get_TipoObservacion(string Owner)
        {
            List<BEREC_OBSERVATION_TYPE> lst = new List<BEREC_OBSERVATION_TYPE>();
            BEREC_OBSERVATION_TYPE item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_REC_OBSERVATION_TYPE_LISTITEM"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_OBSERVATION_TYPE();
                            item.OBS_TYPE = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                            item.OBS_DESC = dr.GetString(dr.GetOrdinal("TEXT"));
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

        public BEREC_OBSERVATION_TYPE  Obtener(string Owner, int idTipoObs)
        {       
            BEREC_OBSERVATION_TYPE item = null;
                using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_REC_OBTIENE_OBS_TIPO"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    oDataBase.AddInParameter(cm, "@OBS_TYPE", DbType.String, idTipoObs);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_OBSERVATION_TYPE();
                            item.OBS_TYPE = dr.GetDecimal(dr.GetOrdinal("OBS_TYPE"));
                            item.OBS_DESC = dr.GetString(dr.GetOrdinal("OBS_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                        }
                    }
                }
           
            return item;
        }

        public List<BEREC_OBSERVATION_TYPE> ListarTipoObservacion(string Owner)
        {
            List<BEREC_OBSERVATION_TYPE> lista = new List<BEREC_OBSERVATION_TYPE>();
            BEREC_OBSERVATION_TYPE item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LISTA_TIPO_OBSERVACION"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_OBSERVATION_TYPE();
                        item.OBS_TYPE = dr.GetDecimal(dr.GetOrdinal("OBS_TYPE"));
                        item.OBS_DESC = dr.GetString(dr.GetOrdinal("OBS_DESC"));
                        item.OBS_OBSERV = dr.GetString(dr.GetOrdinal("OBS_OBSERV"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public List<BEObservationType> Obtener_Observacion(string owner, decimal id)
        {
            List<BEObservationType> lst = new List<BEObservationType>();
            BEObservationType Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_REC_OBTIENE_OBS_TIPO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@OBS_TYPE", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEObservationType();
                        Obj.TIPO = dr.GetDecimal(dr.GetOrdinal("OBS_TYPE"));
                        Obj.OBS_DESC = dr.GetString(dr.GetOrdinal("OBS_DESC"));
                        Obj.OBS_OBSERV = dr.GetString(dr.GetOrdinal("OBS_OBSERV"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        lst.Add(Obj);
                    }
                }
            }

            return lst;
        }

        public int Insertar(BEObservationType en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPO_OBSERVACION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OBS_DESC", DbType.String, en.OBS_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@OBS_OBSERV", DbType.String, en.OBS_OBSERV != null ? en.OBS_OBSERV.ToString().ToUpper() : en.OBS_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEObservationType en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPO_OBSERVACION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OBS_TYPE", DbType.Decimal, en.TIPO);
            oDataBase.AddInParameter(oDbCommand, "@OBS_DESC", DbType.String, en.OBS_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@OBS_OBSERV", DbType.String, en.OBS_OBSERV != null ? en.OBS_OBSERV.ToString().ToUpper() : en.OBS_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, en.ESTADO);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);

            return n;
        }

        public int Eliminar(BEObservationType del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPO_OBSERVACION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OBS_TYPE", DbType.String, del.TIPO);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public bool existeTipoObservacion(string Owner, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_TIPO_OBSERVACION"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@OBS_DESC", DbType.String, nombre);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existeTipoObservacion(string Owner, decimal id, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_UPDATE_EXISTE_TIPO_OBSERVACION"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@OBS_TYPE", DbType.Decimal, id);
                oDataBase.AddInParameter(cm, "@OBS_DESC", DbType.String, nombre);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }
    }
}
