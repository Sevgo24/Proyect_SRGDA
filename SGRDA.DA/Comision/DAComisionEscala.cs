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
using SGRDA.Entities.Comision;
using System.Data.Common;

namespace SGRDA.DA.Comision
{
    public class DAComisionEscala
    {

        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public decimal Insertar(BEComisionEscala comision)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_ESCALA_COMISION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, comision.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@SET_DESC", DbType.String, comision.SET_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@SET_ACC", DbType.String, comision.SET_ACC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@SET_NTRANI", DbType.Decimal, comision.SET_NTRANI);
            oDataBase.AddInParameter(oDbCommand, "@SET_ITRANF", DbType.Decimal, comision.SET_ITRANF);
            oDataBase.AddInParameter(oDbCommand, "@SET_MOT", DbType.String, comision.SET_MOT.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, comision.LOG_USER_CREAT);
            return Convert.ToDecimal(oDataBase.ExecuteScalar(oDbCommand));
        }

        public int Actualizar(BEComisionEscala comision)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ESCALA_COMISION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, comision.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@SET_ID", DbType.String, comision.SET_ID);
            oDataBase.AddInParameter(oDbCommand, "@SET_ACC", DbType.String, comision.SET_ACC);
            oDataBase.AddInParameter(oDbCommand, "@SET_NTRANI", DbType.Decimal, comision.SET_NTRANI);
            oDataBase.AddInParameter(oDbCommand, "@SET_ITRANF", DbType.Decimal, comision.SET_ITRANF);
            oDataBase.AddInParameter(oDbCommand, "@SET_MOT", DbType.String, comision.SET_MOT);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, comision.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEComisionEscala ObtenerComision(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ESCALA_COMISION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@SET_ID", DbType.Decimal, id);
            BEComisionEscala obj = new BEComisionEscala();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    obj.SET_ID = dr.GetDecimal(dr.GetOrdinal("SET_ID"));
                    obj.SET_DESC = dr.GetString(dr.GetOrdinal("SET_DESC")).ToUpper();
                    obj.SET_ACC = dr.GetString(dr.GetOrdinal("SET_ACC")).ToUpper();
                    obj.SET_NTRANI = dr.GetDecimal(dr.GetOrdinal("SET_NTRANI"));
                    obj.SET_ITRANF = dr.GetDecimal(dr.GetOrdinal("SET_ITRANF"));
                    obj.SET_MOT = dr.GetString(dr.GetOrdinal("SET_MOT")).ToUpper();
                    obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT")).ToUpper();
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return obj;
        }

        //public List<BESociedad> Listar(string owner, string param, int st, int pagina, int cantRegxPag)
        //{
        //    Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        //    DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_SOCIEDAD");
        //    oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
        //    oDataBase.AddInParameter(oDbCommand, "@MOG_SDESC", DbType.String, param);
        //    oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
        //    oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
        //    oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
        //    oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
        //    oDataBase.ExecuteNonQuery(oDbCommand);

        //    string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

        //    Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
        //    DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_SOCIEDAD", owner, param, st, pagina, cantRegxPag, ParameterDirection.Output);

        //    var lista = new List<BESociedad>();
        //    var origen = new BESociedad();
        //    using (IDataReader dr = oDataBase1.ExecuteReader(oDbCommand1))
        //    {
        //        while (dr.Read())
        //        {
        //            origen = new BESociedad();
        //            origen.MOG_SOC = dr.GetString(dr.GetOrdinal("MOG_SOC"));
        //            origen.MOG_SDESC = dr.GetString(dr.GetOrdinal("MOG_SDESC"));
        //            if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
        //                origen.ESTADO = "ACTIVO";
        //            else
        //                origen.ESTADO = "INACTIVO";

        //            origen.TotalVirtual = Convert.ToInt32(results);
        //            lista.Add(origen);
        //        }
        //    }
        //    return lista;
        //}

        //public int Eliminar(BESociedad sociedad)
        //{
        //    DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_SOCIEDAD");
        //    oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, sociedad.OWNER);
        //    oDataBase.AddInParameter(oDbCommand, "@MOG_SOC", DbType.String, sociedad.MOG_SOC);
        //    oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, sociedad.LOG_USER_UPDAT);
        //    int r = oDataBase.ExecuteNonQuery(oDbCommand);
        //    return r;
        //}        

    }
}
