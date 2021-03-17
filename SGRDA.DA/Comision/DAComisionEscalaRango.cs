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
    public class DAComisionEscalaRango
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public int Insertar(BEComisionEscalaRango detalle)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_ESCALA_COMISION_DETALLE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, detalle.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@SET_ID", DbType.Decimal, detalle.SET_ID);
            oDataBase.AddInParameter(oDbCommand, "@PRG_ORDER", DbType.Decimal, detalle.PRG_ORDER);
            oDataBase.AddInParameter(oDbCommand, "@PRG_VALUEI", DbType.Decimal, detalle.PRG_VALUEI);
            oDataBase.AddInParameter(oDbCommand, "@PRG_VALUEF", DbType.Decimal, detalle.PRG_VALUEF);
            oDataBase.AddInParameter(oDbCommand, "@PRG_PERC", DbType.Decimal, detalle.PRG_PERC);
            oDataBase.AddInParameter(oDbCommand, "@PRG_VALUEC", DbType.Decimal, detalle.PRG_VALUEC);
            oDataBase.AddInParameter(oDbCommand, "@OG_USER_CREAT", DbType.String, detalle.LOG_USER_CREAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEComisionEscalaRango> ObtenerListaDetalle(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_OBTENER_ESCALA_COMISION_DETALLE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@SET_ID", DbType.Decimal, id);
            List<BEComisionEscalaRango> ListaComisionRangoDetalle = new List<BEComisionEscalaRango>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    BEComisionEscalaRango obj = new BEComisionEscalaRango();
                    obj.RANG_ID = dr.GetDecimal(dr.GetOrdinal("RANG_ID"));
                    obj.SET_ID = dr.GetDecimal(dr.GetOrdinal("SET_ID"));
                    obj.PRG_ORDER = dr.GetDecimal(dr.GetOrdinal("PRG_ORDER"));
                    obj.PRG_VALUEI = dr.GetDecimal(dr.GetOrdinal("PRG_VALUEI"));
                    obj.PRG_VALUEF = dr.GetDecimal(dr.GetOrdinal("PRG_VALUEF"));
                    obj.PRG_PERC = dr.GetDecimal(dr.GetOrdinal("PRG_PERC"));
                    obj.PRG_VALUEC = dr.GetDecimal(dr.GetOrdinal("PRG_VALUEC"));
                    obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    ListaComisionRangoDetalle.Add(obj);
                }
            }
            return ListaComisionRangoDetalle;
        }


    }
}
