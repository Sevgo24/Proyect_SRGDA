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
    public class DAGrupoGasto
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEGrupoGasto> Listar_Page_GrupoGasto(string tipo, string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_GRUPOGASTO");
            oDataBase.AddInParameter(oDbCommand, "@EXP_TYPE", DbType.String, tipo);
            oDataBase.AddInParameter(oDbCommand, "@EXPG_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_GRUPOGASTO",tipo, parametro, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEGrupoGasto>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEGrupoGasto(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEGrupoGasto> Obtener(string owner, string nombre)
        {
            List<BEGrupoGasto> lst = new List<BEGrupoGasto>();
            BEGrupoGasto Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_GRUPOGASTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@EXPG_ID", DbType.String, nombre);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEGrupoGasto();
                        Obj.EXP_TYPE = dr.GetString(dr.GetOrdinal("EXP_TYPE"));
                        Obj.EXPG_ID = dr.GetString(dr.GetOrdinal("EXPG_ID"));
                        Obj.EXPG_DESC = dr.GetString(dr.GetOrdinal("EXPG_DESC"));

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

        public int Insertar(BEGrupoGasto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_GRUPOGASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@EXPG_ID", DbType.String, en.EXPG_ID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXP_TYPE", DbType.String, en.EXP_TYPE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXPG_DESC", DbType.String, en.EXPG_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));
            return n;
        }

        public int Actualizar(BEGrupoGasto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_GRUPOGASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@EXPG_ID", DbType.String, en.EXPG_ID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXP_TYPE", DbType.String, en.EXP_TYPE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXPG_DESC", DbType.String, en.EXPG_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDATE.ToUpper());

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));
            return n;
        }

        public int Eliminar(BEGrupoGasto del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_GRUPOGASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@EXPG_ID", DbType.String, del.EXPG_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEGrupoGasto> ListarGrupoGasto(string Owner, string tipo)
        {
            List<BEGrupoGasto> lst = new List<BEGrupoGasto>();
            BEGrupoGasto item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_GRUPOGASTOS"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    oDataBase.AddInParameter(cm, "@EXP_TYPE", DbType.String, tipo);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEGrupoGasto();
                            item.EXPG_ID = dr.GetString(dr.GetOrdinal("VALUE"));
                            item.EXPG_DESC = dr.GetString(dr.GetOrdinal("TEXT"));
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
