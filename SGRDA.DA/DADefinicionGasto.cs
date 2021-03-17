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
    public class DADefinicionGasto
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEDefinicionGasto> Listar_Page_DefinicionGasto(string owner, string tipo, string grupo, string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_DEFINICIONGASTO");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@EXP_TYPE", DbType.String, tipo);
            oDataBase.AddInParameter(oDbCommand, "@EXPG_ID", DbType.String, grupo);
            oDataBase.AddInParameter(oDbCommand, "@EXP_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEDefinicionGasto>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEDefinicionGasto(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEDefinicionGasto> Obtener(string owner, string nombre)
        {
            List<BEDefinicionGasto> lst = new List<BEDefinicionGasto>();
            BEDefinicionGasto Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DEFINICIONGASTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@EXP_ID", DbType.String, nombre);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDefinicionGasto();
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.EXP_TYPE = dr.GetString(dr.GetOrdinal("EXP_TYPE"));
                        Obj.EXPG_ID = dr.GetString(dr.GetOrdinal("EXPG_ID"));
                        Obj.EXP_ID = dr.GetString(dr.GetOrdinal("EXP_ID"));
                        Obj.EXP_DESC = dr.GetString(dr.GetOrdinal("EXP_DESC"));

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

        public int ValidacionInsertarObtener(string owner, string id, string descripcion)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_VALIDACION_GASTOS_DEF");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@EXP_ID", DbType.String, id);
            oDataBase.AddInParameter(oDbCommand, "@EXP_DESC", DbType.String, descripcion);
            return Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
        }

        public int Insertar(BEDefinicionGasto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_DEFINICIONGASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@EXP_ID", DbType.String, en.EXP_ID == null ? string.Empty : en.EXP_ID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXPG_ID", DbType.String, en.EXPG_ID == null ? string.Empty : en.EXPG_ID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXP_DESC", DbType.String, en.EXP_DESC == null ? string.Empty : en.EXP_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXP_ACC", DbType.String, en.EXP_ACC == null ? string.Empty : en.EXP_ACC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXP_APR", DbType.Decimal, en.EXP_APR);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEDefinicionGasto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_DEFINICIONGASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@EXP_ID", DbType.String, en.EXP_ID == null ? string.Empty : en.EXP_ID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXPG_ID", DbType.String, en.EXPG_ID == null ? string.Empty : en.EXPG_ID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXP_DESC", DbType.String, en.EXP_DESC == null ? string.Empty : en.EXP_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXP_ACC", DbType.String, en.EXP_ACC == null ? string.Empty : en.EXP_ACC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXP_APR", DbType.Decimal, en.EXP_APR);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEDefinicionGasto del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_DEFINICIONGASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@EXP_ID", DbType.String, del.EXP_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEDefinicionGasto> ListarDefinicionGasto(string Owner, string tipo)
        {
            List<BEDefinicionGasto> lst = new List<BEDefinicionGasto>();
            BEDefinicionGasto item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DEFINICIONGASTOS"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    oDataBase.AddInParameter(cm, "@EXPG_ID", DbType.String, tipo);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEDefinicionGasto();
                            item.EXP_ID = dr.GetString(dr.GetOrdinal("VALUE"));
                            item.EXP_DESC = dr.GetString(dr.GetOrdinal("TEXT"));
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
    }
}
