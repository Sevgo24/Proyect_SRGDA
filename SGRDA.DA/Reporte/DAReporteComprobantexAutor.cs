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
   public class DAReporteComprobantexAutor
    {
           Database db = new DatabaseProviderFactory().Create("conexion");
        //creando el metodo listar

           //public List<BEReporteComprobantexAutor> ListarReporteComprobantexAutor(string Fini, string Ffin, string oficina)
           //{
           //    Database db = new DatabaseProviderFactory().Create("conexion");
           //    DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_COMPROBANTE_X_AUTOR");
           //    db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
           //    db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
           //    db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
           //    db.ExecuteNonQuery(oDbCommand);
           //    var lista = new List<BEReporteComprobantexAutor>();
           //    using (IDataReader dr = db.ExecuteReader(oDbCommand))
           //    {
           //        BEReporteComprobantexAutor reporte = null;
           //        while (dr.Read())
           //        {
           //            reporte = new BEReporteComprobantexAutor();
           //            reporte.ruc = dr.GetString(dr.GetOrdinal("ruc"));
           //            reporte.usuario = dr.GetString(dr.GetOrdinal("usuario"));
           //            reporte.femi = dr.GetString(dr.GetOrdinal("femi"));
           //            reporte.tipo_doc = dr.GetString(dr.GetOrdinal("tipo_doc"));
           //            reporte.nro = dr.GetInt32(dr.GetOrdinal("nro"));
           //            reporte.cor_fact = dr.GetString(dr.GetOrdinal("cor_fact"));
           //            reporte.periodo = dr.GetString(dr.GetOrdinal("periodo"));
           //            reporte.importe = dr.GetDecimal(dr.GetOrdinal("importe"));
           //            reporte.evento = dr.GetString(dr.GetOrdinal("evento"));
           //            reporte.fecan = dr.GetString(dr.GetOrdinal("fecan"));
           //            reporte.cod_bec = dr.GetDecimal(dr.GetOrdinal("cod_bec"));
           //            reporte.cob = dr.GetInt32(dr.GetOrdinal("cob"));
           //            reporte.UBIGEO_EST = dr.GetString(dr.GetOrdinal("UBIGEO_EST"));
           //            reporte.ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));
           //            lista.Add(reporte);

           //        }
           //    }
           //    return lista;
           //}

    }
}
