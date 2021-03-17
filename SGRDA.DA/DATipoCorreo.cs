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
    public class DATipoCorreo
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BECorreoType> Listar_Page_TipoCorreo(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_CORREO");
            oDataBase.AddInParameter(oDbCommand, "@MAIL_TDESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_CORREO", parametro, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BECorreoType>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BECorreoType(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public BECorreoType Obtener(string owner, decimal idtipo)
        {
            BECorreoType Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CORREO_TYPE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@MAIL_TYPE", DbType.Decimal, idtipo);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECorreoType();
                        Obj.MAIL_TYPE = dr.GetDecimal(dr.GetOrdinal("MAIL_TYPE"));
                        Obj.MAIL_TDESC = dr.GetString(dr.GetOrdinal("MAIL_TDESC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return Obj;
        }

        public List<BECorreoType> ListarTipoCorreos(string owner)
        {
            List<BECorreoType> lst = new List<BECorreoType>();
            BECorreoType item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_TIPO_CORREOS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BECorreoType();
                        item.MAIL_TDESC = dr.GetString(dr.GetOrdinal("MAIL_TDESC"));
                        item.MAIL_OBSERV = dr.GetString(dr.GetOrdinal("MAIL_OBSERV"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public List<BECorreoType> Get_TipoCorreo(string Owner)
        {
            List<BECorreoType> lst = new List<BECorreoType>();
            BECorreoType item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CORREO_TYPE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BECorreoType();
                        item.MAIL_TYPE = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                        item.MAIL_TDESC = dr.GetString(dr.GetOrdinal("TEXT"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public List<BECorreoType> Obtener_Correo(string owner, decimal id)
        {
            List<BECorreoType> lst = new List<BECorreoType>();
            BECorreoType Obj = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_CORREO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@MAIL_TYPE", DbType.Decimal, id);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECorreoType();
                        Obj.MAIL_TYPE = dr.GetDecimal(dr.GetOrdinal("MAIL_TYPE"));
                        Obj.MAIL_TDESC = dr.GetString(dr.GetOrdinal("MAIL_TDESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MAIL_OBSERV")))
                        {
                            Obj.MAIL_OBSERV = dr.GetString(dr.GetOrdinal("MAIL_OBSERV"));
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

        public int Insertar(BECorreoType en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPO_CORREO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_TDESC", DbType.String, en.MAIL_TDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MAIL_OBSERV", DbType.String, en.MAIL_OBSERV != null ? en.MAIL_OBSERV.ToString().ToUpper() : en.MAIL_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BECorreoType en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPO_CORREO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_TYPE", DbType.String, en.MAIL_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_TDESC", DbType.String, en.MAIL_TDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MAIL_OBSERV", DbType.String, en.MAIL_OBSERV != null ? en.MAIL_OBSERV.ToString().ToUpper() : en.MAIL_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, en.ESTADO);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BECorreoType del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPO_MAIL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_TYPE", DbType.String, del.MAIL_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public bool existeTipoCorreo(string Owner, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_TIPO_CORREO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@MAIL_TDESC", DbType.String, nombre);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existeTipoCorreo(string Owner, decimal id, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_UPDATE_EXISTE_TIPO_CORREO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@MAIL_TDESC", DbType.String, nombre);
                oDataBase.AddInParameter(cm, "@MAIL_TYPE", DbType.String, id);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }
    }
}
