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
    public class DAReporeRecaudacionSedes
    {

        Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        //creando el metodo listar
        //RECAUDACION
        public List<BEReporteListarRecaudacionSedes> ListarReporteRecaudacionSedes(string Fini, string Ffin, int oficina
            , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_RECAUDACION_SEDES");
                db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
                db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
                db.AddInParameter(oDbCommand, "@oficina", DbType.Int32, oficina);

                db.AddInParameter(oDbCommand, "@conFechaIngreso", DbType.Int32, conFechaIngreso);
                db.AddInParameter(oDbCommand, "@conFechaConfirmacion", DbType.Int32, conFechaConfirmacion);
                db.AddInParameter(oDbCommand, "@FINI_CON", DbType.DateTime, finiConfirmacion);
                db.AddInParameter(oDbCommand, "@FFIN_CON", DbType.DateTime, ffinConfirmacion);
                //db.ExecuteNonQuery(oDbCommand);
                var lista = new List<BEReporteListarRecaudacionSedes>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEReporteListarRecaudacionSedes reporte = new BEReporteListarRecaudacionSedes();
                    while (dr.Read())
                    {
                        reporte = new BEReporteListarRecaudacionSedes();
                        reporte.NODO = dr.GetString(dr.GetOrdinal("NODO"));
                        reporte.TV = dr.GetDecimal(dr.GetOrdinal("TV"));

                        reporte.RADIO = dr.GetDecimal(dr.GetOrdinal("RADIO"));
                        reporte.CABLE = dr.GetDecimal(dr.GetOrdinal("CABLE"));
                        reporte.SINCRONIZACION = dr.GetDecimal(dr.GetOrdinal("SINCRONIZACION"));
                        reporte.FONO = dr.GetDecimal(dr.GetOrdinal("FONO"));
                        reporte.REDES = dr.GetDecimal(dr.GetOrdinal("REDES"));

                        reporte.MUSICA_ESPERA = dr.GetDecimal(dr.GetOrdinal("MUSICA_ESPERA"));
                        reporte.TRANSPORTE = dr.GetDecimal(dr.GetOrdinal("TRANSPORTE"));
                        reporte.LOCALES = dr.GetDecimal(dr.GetOrdinal("LOCALES"));
                        reporte.ESPECTACULOS = dr.GetDecimal(dr.GetOrdinal("ESPECTACULOS"));
                        reporte.BAILES = dr.GetDecimal(dr.GetOrdinal("BAILES"));

                        reporte.C_PRI = dr.GetDecimal(dr.GetOrdinal("C_PRI"));
                        reporte.GRAN_DERECHO = dr.GetDecimal(dr.GetOrdinal("GRAN_DERECHO"));
                        reporte.INTERNACIONAL = dr.GetDecimal(dr.GetOrdinal("INTERNACIONAL"));
                        reporte.VUD = dr.GetDecimal(dr.GetOrdinal("VUD"));
                        reporte.NC = dr.GetDecimal(dr.GetOrdinal("NC"));

                        lista.Add(reporte);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //CONTABLE
        public List<BEReporteListarRecaudacionSedes> ListarReporteContableRecaudacionSedes(string Fini, string Ffin, string oficina, decimal idContable)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_REP_CONTABLE_RECAUDACION_SEDES");
                db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
                db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
                db.AddInParameter(oDbCommand, "@oficina", DbType.String, oficina);
                db.AddInParameter(oDbCommand, "@idContable", DbType.Decimal, idContable);

                var lista = new List<BEReporteListarRecaudacionSedes>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEReporteListarRecaudacionSedes reporte = null;
                    while (dr.Read())
                    {
                        reporte = new BEReporteListarRecaudacionSedes();
                        reporte.NODO = dr.GetString(dr.GetOrdinal("NODO"));
                        reporte.TV = dr.GetDecimal(dr.GetOrdinal("TV"));
                        reporte.RADIO = dr.GetDecimal(dr.GetOrdinal("RADIO"));
                        reporte.CABLE = dr.GetDecimal(dr.GetOrdinal("CABLE"));
                        reporte.SINCRONIZACION = dr.GetDecimal(dr.GetOrdinal("SINCRONIZACION"));
                        reporte.FONO = dr.GetDecimal(dr.GetOrdinal("FONO"));
                        reporte.REDES = dr.GetDecimal(dr.GetOrdinal("REDES"));
                        reporte.MUSICA_ESPERA = dr.GetDecimal(dr.GetOrdinal("MUSICA_ESPERA"));
                        reporte.TRANSPORTE = dr.GetDecimal(dr.GetOrdinal("TRANSPORTE"));
                        reporte.LOCALES = dr.GetDecimal(dr.GetOrdinal("LOCALES"));
                        reporte.ESPECTACULOS = dr.GetDecimal(dr.GetOrdinal("ESPECTACULOS"));

                        reporte.BAILES = dr.GetDecimal(dr.GetOrdinal("BAILES"));
                        reporte.C_PRI = dr.GetDecimal(dr.GetOrdinal("C_PRI"));
                        reporte.GRAN_DERECHO = dr.GetDecimal(dr.GetOrdinal("GRAN_DERECHO"));
                        reporte.EMPADRONAMIENTO = dr.GetDecimal(dr.GetOrdinal("EMPADRONAMIENTO"));
                        reporte.NC = dr.GetDecimal(dr.GetOrdinal("NC"));
                        reporte.DERECHOS_INTERNACIONALES = dr.GetDecimal(dr.GetOrdinal("DERECHOS_INTERNACIONALES"));
                        reporte.VUD = dr.GetDecimal(dr.GetOrdinal("VUD"));
                        lista.Add(reporte);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string ObtenerFechaUltActualizacionRepRecaudacionSedes()
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_ULT_FECHA_ACT_RECAUDACION");
            string UltimafechaActualizacion = Convert.ToString(oDatabase.ExecuteScalar(oDbCommand));
            return UltimafechaActualizacion;
        }



    }
}
