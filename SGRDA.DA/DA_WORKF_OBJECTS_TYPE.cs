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
using SGRDA.Entities.WorkFlow;
using System.Data.Common;

namespace SGRDA.DA.WorkFlow
{
    public class DA_WORKF_OBJECTS_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<WORKF_OBJECTS_TYPE> ListarTipoObjeto(string owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBJECTS_TYPE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.ExecuteNonQuery(oDbCommand);

            List<WORKF_OBJECTS_TYPE> lista = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_OBJECTS_TYPE>();
                WORKF_OBJECTS_TYPE flujo = null;
                while (dr.Read())
                {
                    flujo = new WORKF_OBJECTS_TYPE();
                    flujo.WRKF_OTID = dr.GetDecimal(dr.GetOrdinal("WRKF_OTID"));
                    flujo.WRKF_OTDESC = dr.GetString(dr.GetOrdinal("WRKF_OTDESC"));
                    lista.Add(flujo);
                }
            }
            return lista;
        }


        public WORKF_OBJECTS_TYPE obtener(string owner, decimal? idTObj)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_OBJECT_TYPE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_OTID", DbType.String, idTObj);
            db.ExecuteNonQuery(oDbCommand);

              WORKF_OBJECTS_TYPE item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new WORKF_OBJECTS_TYPE();
                    item.WRKF_OTID = dr.GetDecimal(dr.GetOrdinal("WRKF_OTID"));
                    item.WRKF_OTDESC = dr.GetString(dr.GetOrdinal("WRKF_OTDESC"));
                    item.WRKF_OPREF = dr.GetString(dr.GetOrdinal("WRKF_OPREF"));

                }
            }
            return item;
        }

    }
}
