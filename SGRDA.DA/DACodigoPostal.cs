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
    public class DACodigoPostal
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BECodigoPostal> Listar_Page_Codigo_Postal(decimal parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_CODIGO_POSTAL");
            oDataBase.AddInParameter(oDbCommand, "@DADV_ID", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_CODIGO_POSTAL", parametro, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BECodigoPostal>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BECodigoPostal(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BECodigoPostal> Obtener(decimal id)
        {
            List<BECodigoPostal> lst = new List<BECodigoPostal>();
            BECodigoPostal Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_POSTAL"))
            {
                oDataBase.AddInParameter(cm, "@CPO_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECodigoPostal();
                        Obj.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        Obj.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        Obj.POSITIONS = dr.GetDecimal(dr.GetOrdinal("POSITIONS"));

                        lst.Add(Obj);
                    }
                }
            }

            return lst;
        }

        public int Insertar(BECodigoPostal en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_CODIGO_POSTAL");
            oDataBase.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
            oDataBase.AddInParameter(oDbCommand, "@POSITIONS", DbType.Decimal, en.POSITIONS);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }
        public int Actualizar(BECodigoPostal en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CODIGO_POSTAL");
            oDataBase.AddInParameter(oDbCommand, "@CPO_ID", DbType.Decimal, en.TIS_N);
            oDataBase.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
            oDataBase.AddInParameter(oDbCommand, "@POSITIONS", DbType.Decimal, en.POSITIONS);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDATE);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BECodigoPostal del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CODIGO_POSTAL");
            oDataBase.AddInParameter(oDbCommand, "@CPO_ID", DbType.Decimal, del.CPO_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
