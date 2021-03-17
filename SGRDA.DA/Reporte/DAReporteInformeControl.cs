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
using System.Xml;

namespace SGRDA.DA.Reporte
{
   public class DAReporteInformeControl
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        public List<BERegistroVenta> ReporteRegistroVenta(string Fini, string Ffin, string oficina, int? Rubro, string parametros)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_REGISTRO_VENTA");
            db.AddInParameter(oDbCommand, "@F1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@F2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, Rubro);
            db.AddInParameter(oDbCommand, "@Parametros", DbType.String, parametros);

            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BERegistroVenta>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BERegistroVenta reporte = null;
                while (dr.Read())
                {
                    reporte = new BERegistroVenta();
                    reporte.FECHA = dr.GetString(dr.GetOrdinal("FECHA"));
                    reporte.TD = dr.GetString(dr.GetOrdinal("TD"));
                    reporte.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                    reporte.NUMERO = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
                    reporte.RUC = dr.GetString(dr.GetOrdinal("RUC"));
                    reporte.NOMBRE = dr.GetString(dr.GetOrdinal("NOMBRE"));
                    reporte.AFECTO = dr.GetDecimal(dr.GetOrdinal("AFECTO"));
                    reporte.INAFECTO = dr.GetDecimal(dr.GetOrdinal("INAFECTO"));
                    reporte.IGV = dr.GetDecimal(dr.GetOrdinal("IGV"));
                    reporte.TOTAL = dr.GetDecimal(dr.GetOrdinal("TOTAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOC_REFERENCIAS")))
                    {
                        reporte.DOC_REFERENCIAS = dr.GetString(dr.GetOrdinal("DOC_REFERENCIAS"));
                    }
                    reporte.Fecha1 = dr.GetString(dr.GetOrdinal("Fecha1"));
                    reporte.Fecha2 = dr.GetString(dr.GetOrdinal("Fecha2"));

                    lista.Add(reporte);
                }
            }
            return lista;
        }

    }
}
