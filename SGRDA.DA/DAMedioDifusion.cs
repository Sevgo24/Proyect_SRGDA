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
    public class DAMedioDifusion
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEMedioDifusion> Listar_Page_Medio_Difusion(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_MEDIO_DIFUSION");
            oDataBase.AddInParameter(oDbCommand, "@BROAD_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            
            var lista = new List<BEMedioDifusion>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEMedioDifusion(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEMedioDifusion> Obtener(string owner, decimal id)
        {
            List<BEMedioDifusion> lst = new List<BEMedioDifusion>();
            BEMedioDifusion Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_MEDIO_DIFUSION"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BROAD_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEMedioDifusion();
                        Obj.BROAD_ID = dr.GetDecimal(dr.GetOrdinal("BROAD_ID"));
                        Obj.BROAD_DESC = dr.GetString(dr.GetOrdinal("BROAD_DESC")).ToUpper();
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BEMedioDifusion en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_MEDIO_DIFUSION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BROAD_DESC", DbType.String, en.BROAD_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEMedioDifusion en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_MEDIO_DIFUSION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BROAD_ID", DbType.Decimal, en.BROAD_ID);
            oDataBase.AddInParameter(oDbCommand, "@BROAD_DESC", DbType.String, en.BROAD_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEMedioDifusion del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_MEDIO_DIFUSION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BROAD_ID", DbType.Decimal, del.BROAD_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
