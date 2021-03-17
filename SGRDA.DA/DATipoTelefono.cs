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
    public class DATipoTelefono
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BETelefonoType> Listar_Page_TipoTelefono(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_TELEFONO");
            oDataBase.AddInParameter(oDbCommand, "@PHONE_TDESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_TELEFONO", parametro, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BETelefonoType>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BETelefonoType(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public BETelefonoType Obtener(string owner, decimal idtipo)
        {

            BETelefonoType Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TELEFONO_TYPE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PHONE_TYPE", DbType.Decimal, idtipo);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETelefonoType();
                        Obj.PHONE_TYPE = dr.GetDecimal(dr.GetOrdinal("PHONE_TYPE"));
                        Obj.PHONE_TDESC = dr.GetString(dr.GetOrdinal("PHONE_TDESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PHONE_TOBSERV")))
                        {
                            Obj.PHONE_TOBSERV = dr.GetString(dr.GetOrdinal("PHONE_TOBSERV"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public List<BETelefonoType> Get_TipoTelefono(string Owner)
        {
            List<BETelefonoType> lst = new List<BETelefonoType>();
            BETelefonoType item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TELEFONOS_TYPE"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BETelefonoType();
                            item.PHONE_TYPE = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                            item.PHONE_TDESC = dr.GetString(dr.GetOrdinal("TEXT"));
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

        public List<BETelefonoType> Obtener_Telefono(string owner, decimal id)
        {
            List<BETelefonoType> lst = new List<BETelefonoType>();
            BETelefonoType Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_TELEFONO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PHONE_TYPE", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETelefonoType();
                        Obj.PHONE_TYPE = dr.GetDecimal(dr.GetOrdinal("PHONE_TYPE"));
                        Obj.PHONE_TDESC = dr.GetString(dr.GetOrdinal("PHONE_TDESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PHONE_TOBSERV")))
                        {
                            Obj.PHONE_TOBSERV = dr.GetString(dr.GetOrdinal("PHONE_TOBSERV"));
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

        public int Insertar(BETelefonoType en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPO_TELEFONO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_TDESC", DbType.String, en.PHONE_TDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@PHONE_TOBSERV", DbType.String, en.PHONE_TOBSERV != null ? en.PHONE_TOBSERV.ToString().ToUpper() : en.PHONE_TOBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());

            int n = oDataBase.ExecuteNonQuery(oDbCommand);

            return n;
        }

        public int Actualizar(BETelefonoType en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPO_TELEFONO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_TYPE", DbType.String, en.PHONE_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_TDESC", DbType.String, en.PHONE_TDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@PHONE_TOBSERV", DbType.String, en.PHONE_TOBSERV != null ? en.PHONE_TOBSERV.ToString().ToUpper() : en.PHONE_TOBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, en.ESTADO);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);

            return n;
        }

        public int Eliminar(BETelefonoType del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPO_TELEFONO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_TYPE", DbType.String, del.PHONE_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public bool existeTipoTelefono(string Owner, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_TIPO_TELEFONO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@PHONE_TDESC", DbType.String, nombre);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existeTipoTelefono(string Owner, decimal id, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_UPDATE_EXISTE_TIPO_TELEFONO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@PHONE_TDESC", DbType.String, nombre);
                oDataBase.AddInParameter(cm, "@PHONE_TYPE", DbType.Decimal, id);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }
    }
}
