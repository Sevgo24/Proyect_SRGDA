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
    public class DATipoAccion
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoAccion> Listar_Page_TipoAccion(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPOACCION");
            oDataBase.AddInParameter(oDbCommand, "@WRKF_ATNAME", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BETipoAccion>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BETipoAccion(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BETipoAccion> Obtener(string owner, decimal id)
        {
            List<BETipoAccion> lst = new List<BETipoAccion>();
            BETipoAccion Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPOACCION"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@WRKF_ATID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoAccion();
                        Obj.WRKF_ATID = dr.GetDecimal(dr.GetOrdinal("WRKF_ATID"));
                        Obj.WRKF_ATNAME = dr.GetString(dr.GetOrdinal("WRKF_ATNAME")).ToUpper();
                        Obj.WRKF_ATLABEL = dr.GetString(dr.GetOrdinal("WRKF_ATLABEL"));
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BETipoAccion en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPOACCION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_ATNAME", DbType.String, en.WRKF_ATNAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@WRKF_ATLABEL", DbType.String, en.WRKF_ATLABEL.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Actualizar(BETipoAccion en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPOACCION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_ATID", DbType.Decimal, en.WRKF_ATID);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_ATNAME", DbType.String, en.WRKF_ATNAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@WRKF_ATLABEL", DbType.String, en.WRKF_ATLABEL.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BETipoAccion del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPOACCION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_ATID", DbType.Decimal, del.WRKF_ATID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
