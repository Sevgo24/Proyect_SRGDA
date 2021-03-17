using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DANumeracionOfi
    {

        public int Insertar(BENumeradorOficina num)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_OFF_NUM");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, num.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, num.OFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, num.LOG_USER_CREAT);
                oDatabase.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, num.NMR_ID);
                oDatabase.AddInParameter(oDbCommand, "@ID_COLL_DIV", DbType.Decimal, num.ID_COLL_DIV);
                oDatabase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, num.DAD_ID);

                int n = oDatabase.ExecuteNonQuery(oDbCommand);
                return n;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public BENumeradorOficina Obtener(string owner, decimal IdOficina, decimal IdNumerador)
        {
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_NUM_OFF");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
                oDataBase.AddInParameter(oDbCommand, "@NUM_ID", DbType.Decimal, IdNumerador);
                BENumeradorOficina item = null;
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new BENumeradorOficina();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.NUM_ID = dr.GetDecimal(dr.GetOrdinal("NUM_ID"));
                        item.ID_COLL_DIV = dr.GetDecimal(dr.GetOrdinal("ID_COLL_DIV"));
                        item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                        item.TIPO_NUMERADOR = dr.GetString(dr.GetOrdinal("TIPO"));
                        item.SERIE_NUMERADOR = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BENumeradorOficina> ObtenerListaDivNumerador(decimal IdOficina, string owner)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_OFICINA_NUMERADOR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
            var lista = new List<BENumeradorOficina>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BENumeradorOficina item = null;
                while (dr.Read())
                {
                    item = new BENumeradorOficina();
                    item.NUM_ID = dr.GetDecimal(dr.GetOrdinal("NUM_ID"));
                    item.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    item.ID_COLL_DIV = dr.GetDecimal(dr.GetOrdinal("ID_COLL_DIV"));
                    item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    item.TIPO_NUMERADOR = dr.GetString(dr.GetOrdinal("TIPO"));
                    item.SERIE_NUMERADOR = dr.GetString(dr.GetOrdinal("SERIE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Activar(string owner, decimal offID, decimal numId, string user)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_NUM_OFF");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offID);
            oDataBase.AddInParameter(oDbCommand, "@NUM_ID", DbType.Decimal, numId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal offId, decimal numId, string user)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASD_OFF_NUM");

            oDatabase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offId);
            oDatabase.AddInParameter(oDbCommand, "@NUM_ID", DbType.Decimal, numId);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, user);

            int r = oDatabase.ExecuteNonQuery(oDbCommand);
            return r;
        }

    }
}
