using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities.Reporte;
using System.Data.Common;

namespace SGRDA.DA.Reporte
{
   public class DAReporteListarUsuario
    {
        Database db = new DatabaseProviderFactory().Create("conexion");
        //creando el metodo listar

        public List<BEReporteListarUsuarios> ListarReporteUsuario(string Fini, string Ffin,string usuario, string numero)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_USUARIO");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@usuario", DbType.String, usuario);
            db.AddInParameter(oDbCommand, "@numero", DbType.String, numero);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEReporteListarUsuarios>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEReporteListarUsuarios reporte = null;
                while (dr.Read())
                {
                    reporte = new BEReporteListarUsuarios();
                    reporte.CODIGO = dr.GetDecimal(dr.GetOrdinal("CODIGO"));
                    reporte.RAZON_SOCIAL = dr.GetString(dr.GetOrdinal("RAZON_SOCIAL"));
                    reporte.LOCAL_EST = dr.GetString(dr.GetOrdinal("LOCAL_EST"));
                    reporte.TIPO_EST = dr.GetString(dr.GetOrdinal("TIPO_EST"));
                    reporte.SUBTIPO_EST = dr.GetString(dr.GetOrdinal("SUBTIPO_EST"));
                    reporte.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));
                    reporte.FECHA_INGRESA = dr.GetString(dr.GetOrdinal("FECHA_INGRESA"));
                    reporte.TIPO_DOC = dr.GetString(dr.GetOrdinal("TIPO_DOC"));
                    reporte.NRO = dr.GetString(dr.GetOrdinal("NRO"));
                    reporte.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                    reporte.TARIFA = dr.GetDecimal(dr.GetOrdinal("TARIFA"));
                    reporte.PROVINCIA = dr.GetString(dr.GetOrdinal("PROVINCIA"));
                    reporte.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                    lista.Add(reporte);

                }
            }
            return lista;
        }
    }
}
