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
    public class DATipoProceso
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoProceso> Listar_Page_TipoProceso(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_PROCESO");
            oDataBase.AddInParameter(oDbCommand, "@PROC_TDESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BETipoProceso>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BETipoProceso(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BETipoProceso> Obtener(string owner, decimal id)
        {
            List<BETipoProceso> lst = new List<BETipoProceso>();
            BETipoProceso Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_PROCESO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PROC_TYPE", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoProceso();
                        Obj.PROC_TYPE = dr.GetDecimal(dr.GetOrdinal("PROC_TYPE"));
                        Obj.PROC_TDESC = dr.GetString(dr.GetOrdinal("PROC_TDESC")).ToUpper();
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

        public List<BETipoProceso> Listar(string owner)
        {
            List<BETipoProceso> lst = new List<BETipoProceso>();
            BETipoProceso Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoProceso();
                        Obj.PROC_TYPE = dr.GetDecimal(dr.GetOrdinal("PROC_TYPE"));
                        Obj.PROC_TDESC = dr.GetString(dr.GetOrdinal("PROC_TDESC")).ToUpper();
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BETipoProceso en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPO_PROCESO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PROC_TDESC", DbType.String, en.PROC_TDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Actualizar(BETipoProceso en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPO_PROCESO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PROC_TYPE", DbType.Decimal, en.PROC_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@PROC_TDESC", DbType.String, en.PROC_TDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BETipoProceso del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPO_PROCESO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PROC_TYPE", DbType.Decimal, del.PROC_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
