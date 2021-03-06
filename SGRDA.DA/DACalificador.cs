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
    public class DACalificador
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BECalificador> Listar_Page_Calificador(decimal tipo, string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_CALIFICADORES");
            oDataBase.AddInParameter(oDbCommand, "@QUA_ID", DbType.Decimal, tipo);
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_CALIFICADORES", tipo, parametro, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BECalificador>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BECalificador(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BECalificador> Obtener(string owner, decimal id)
        {
            List<BECalificador> lst = new List<BECalificador>();
            BECalificador Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CALIFICADORES"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@QUC_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECalificador();
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.QUC_ID = dr.GetDecimal(dr.GetOrdinal("QUC_ID"));
                        Obj.QUA_ID = dr.GetDecimal(dr.GetOrdinal("QUA_ID"));
                        Obj.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));

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

        public int Insertar(BECalificador en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_CALIFICADORES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@QUA_ID", DbType.Decimal, en.QUA_ID);
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));
            return n;
        }

        public int Actualizar(BECalificador en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CALIFICADORES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@QUC_ID", DbType.Decimal, en.QUC_ID);
            oDataBase.AddInParameter(oDbCommand, "@QUA_ID", DbType.Decimal, en.QUA_ID);
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));
            return n;
        }

        public int Eliminar(BECalificador del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CALIFICADORES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@QUC_ID", DbType.Decimal, del.QUC_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
