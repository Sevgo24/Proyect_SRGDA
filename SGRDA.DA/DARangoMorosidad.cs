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
    public class DARangoMorosidad
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BERangoMorosidad> Listar_Page_Rango_Morosidad(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_RANGO_MOROSIDAD");
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_RANGO_MOROSIDAD", parametro, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BERangoMorosidad>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BERangoMorosidad(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BERangoMorosidad> Listar(string owner)
        {
            List<BERangoMorosidad> lst = new List<BERangoMorosidad>();
            BERangoMorosidad Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_RANGO_MOSOSIDAD"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BERangoMorosidad();
                        Obj.RANGE_COD = dr.GetDecimal(dr.GetOrdinal("RANGE_COD"));
                        Obj.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        Obj.RANGE_FROM = dr.GetDecimal(dr.GetOrdinal("RANGE_FROM"));
                        Obj.RANGE_TO = dr.GetDecimal(dr.GetOrdinal("RANGE_TO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            Obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            Obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }                      
                        Obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            Obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
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

        public List<BERangoMorosidad> Obtener(decimal id)
        {
            List<BERangoMorosidad> lst = new List<BERangoMorosidad>();
            BERangoMorosidad Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_RANGO_MOROSIDAD"))
            {
                oDataBase.AddInParameter(cm, "@RANGE_COD", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BERangoMorosidad();
                        Obj.RANGE_COD = dr.GetDecimal(dr.GetOrdinal("RANGE_COD"));
                        Obj.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        Obj.RANGE_FROM = dr.GetDecimal(dr.GetOrdinal("RANGE_FROM"));
                        Obj.RANGE_TO = dr.GetDecimal(dr.GetOrdinal("RANGE_TO"));

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

        public int Insertar(BERangoMorosidad en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_RANGO_MOROSIDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@RANGE_FROM", DbType.Decimal, en.RANGE_FROM);
            oDataBase.AddInParameter(oDbCommand, "@RANGE_TO", DbType.Decimal, en.RANGE_TO);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BERangoMorosidad en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_RANGO_MOROSIDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@RANGE_COD", DbType.Decimal, en.RANGE_COD);
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@RANGE_FROM", DbType.Decimal, en.RANGE_FROM);
            oDataBase.AddInParameter(oDbCommand, "@RANGE_TO", DbType.Decimal, en.RANGE_TO);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BERangoMorosidad del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_RANGO_MOSOSIDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@RANGE_COD", DbType.Decimal, del.RANGE_COD);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
