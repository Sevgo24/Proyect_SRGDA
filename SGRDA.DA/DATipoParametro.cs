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
    public class DATipoParametro
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BETipoParametro> Listar_Page_TipoParametro(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_PARAMETROS");
            oDataBase.AddInParameter(oDbCommand, "@PAR_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_PARAMETROS", parametro, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BETipoParametro>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BETipoParametro(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BETipoParametro> ListarTipoParametro(string owner)
        {
            List<BETipoParametro> lst = new List<BETipoParametro>();
            BETipoParametro item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_TIPO_PARAMETROS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BETipoParametro();
                        item.PAR_DESC = dr.GetString(dr.GetOrdinal("PAR_DESC"));
                        item.PAR_OBSERV = dr.GetString(dr.GetOrdinal("PAR_OBSERV"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public List<BEREC_TIPO_PARAMETRO> Get_TipoParametro(string Owner)
        {
            List<BEREC_TIPO_PARAMETRO> lst = new List<BEREC_TIPO_PARAMETRO>();
            BEREC_TIPO_PARAMETRO item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_REC_PARAMETERS_TYPE_LISTITEM"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_TIPO_PARAMETRO();
                        item.PAR_TYPE = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                        item.PAR_DESC = dr.GetString(dr.GetOrdinal("TEXT"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public BEREC_TIPO_PARAMETRO Obtener(string Owner, decimal idTipo)
        {
            BEREC_TIPO_PARAMETRO item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_REC_OBTIENE_PAR_TIPO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDataBase.AddInParameter(cm, "@PAR_TYPE", DbType.String, idTipo);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_TIPO_PARAMETRO();
                        item.PAR_TYPE = dr.GetDecimal(dr.GetOrdinal("PAR_TYPE"));
                        item.PAR_DESC = dr.GetString(dr.GetOrdinal("PAR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return item;
        }

        public List<BETipoParametro> Obtener_Parametro(string owner, decimal id)
        {
            List<BETipoParametro> lst = new List<BETipoParametro>();
            BETipoParametro Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_REC_OBTIENE_PAR_TIPO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PAR_TYPE", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoParametro();
                        Obj.PAR_TYPE = dr.GetDecimal(dr.GetOrdinal("PAR_TYPE"));
                        Obj.PAR_DESC = dr.GetString(dr.GetOrdinal("PAR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PAR_OBSERV")))
                        {
                            Obj.PAR_OBSERV = dr.GetString(dr.GetOrdinal("PAR_OBSERV"));
                        }
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

        public int Insertar(BETipoParametro en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPO_PARAMETRO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PAR_DESC", DbType.String, en.PAR_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@PAR_OBSERV", DbType.String, en.PAR_OBSERV != null ? en.PAR_OBSERV.ToString().ToUpper() : en.PAR_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BETipoParametro en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPO_PARAMETRO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PAR_TYPE", DbType.String, en.PAR_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@PAR_DESC", DbType.String, en.PAR_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@PAR_OBSERV", DbType.String, en.PAR_OBSERV != null ? en.PAR_OBSERV.ToString().ToUpper() : en.PAR_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, en.ESTADO);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);

            return n;
        }

        public int Eliminar(BETipoParametro del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPO_PARAMETRO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PAR_TYPE", DbType.String, del.PAR_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public bool existeTipoParametro(string Owner, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_TIPO_PARAMETRO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@PAR_DESC", DbType.String, nombre);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existeTipoParametro(string Owner, decimal id, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_UPDATE_EXISTE_TIPO_PARAMETRO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@PAR_DESC", DbType.String, nombre);
                oDataBase.AddInParameter(cm, "@PAR_TYPE", DbType.String, id);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public List<BEParametroSubTipo> ObtenerListaSubTipoParametro(string Owner, decimal idTIpoParametro)
        {
            List<BEParametroSubTipo> lst = new List<BEParametroSubTipo>();
            BEParametroSubTipo item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_SUBTIPO_PARAMETRO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDataBase.AddInParameter(cm, "@PAR_TYPE", DbType.Decimal, idTIpoParametro);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEParametroSubTipo();
                        item.PAR_SUBTYPE = dr.GetDecimal(dr.GetOrdinal("PAR_SUBTYPE"));
                        item.PAR_SUBTYPE_DESC = dr.GetString(dr.GetOrdinal("PAR_SUBTYPE_DESC"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }


    }
}
