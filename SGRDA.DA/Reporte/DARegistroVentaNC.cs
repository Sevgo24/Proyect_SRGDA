using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.DA.Reporte
{
    public class DARegistroVentaNC
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        public List<BERegistroVentaNC> ReporteRegistroVenta_NC(string Fini, string Ffin, string oficina, int ESTADO)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_REGISTRO_VENTA_NC");
            db.AddInParameter(oDbCommand, "@F1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@F2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, ESTADO);


            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BERegistroVentaNC>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BERegistroVentaNC reporte = null;
                while (dr.Read())
                {
                    reporte = new BERegistroVentaNC();
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
                    reporte.OFICINA = dr.GetString(dr.GetOrdinal("OFICINA"));
                    reporte.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));


                    lista.Add(reporte);
                }
            }
            return lista;
        }
    }
}

