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
    public class DATipoGasto
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoGasto> Listar_Page_TipoGasto(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPOGASTO");
            oDataBase.AddInParameter(oDbCommand, "@EXPT_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPOGASTO", parametro, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BETipoGasto>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BETipoGasto(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BETipoGasto> ListarTipoGasto(string Owner)
        {
            List<BETipoGasto> lst = new List<BETipoGasto>();
            BETipoGasto item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPOGASTOS"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BETipoGasto();
                            item.EXP_TYPE = dr.GetString(dr.GetOrdinal("VALUE"));
                            item.EXPT_DESC = dr.GetString(dr.GetOrdinal("TEXT"));
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


        public List<BETipoGasto> Obtener(string owner, string nombre)
        {
            List<BETipoGasto> lst = new List<BETipoGasto>();
            BETipoGasto Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPOGASTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@EXP_TYPE", DbType.String, nombre);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoGasto();
                        Obj.EXP_TYPE = dr.GetString(dr.GetOrdinal("EXP_TYPE"));
                        Obj.EXPT_DESC = dr.GetString(dr.GetOrdinal("EXPT_DESC"));

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

        public int Insertar(BETipoGasto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPOGASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@EXP_TYPE", DbType.String, en.EXP_TYPE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXPT_DESC", DbType.String, en.EXPT_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Actualizar(BETipoGasto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPOGASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@EXP_TYPE", DbType.String, en.EXP_TYPE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@EXPT_DESC", DbType.String, en.EXPT_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDATE.ToUpper());

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BETipoGasto del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPOGASTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@EXP_TYPE", DbType.String, del.EXP_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
