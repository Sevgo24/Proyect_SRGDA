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
    public class DAReporteFacturaCancelada
    {
        Database db = new DatabaseProviderFactory().Create("conexion");
        //creando el metodo listar

        public List<BEFacturaCancelada> ListarReporteFacturaCancelada(string Fini, string Ffin, string oficina
                    , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion
                    , int? rubro,string parametrosRubro,string ModalidadDetalle)
        //,string parametros
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_FACTURA_CANCELADA");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, rubro);
            db.AddInParameter(oDbCommand, "@conFechaIngreso", DbType.Int32, conFechaIngreso);
            db.AddInParameter(oDbCommand, "@conFechaConfirmacion", DbType.Int32, conFechaConfirmacion);
            db.AddInParameter(oDbCommand, "@FINI_CON", DbType.DateTime, finiConfirmacion);
            db.AddInParameter(oDbCommand, "@FFIN_CON", DbType.DateTime, ffinConfirmacion);
            db.AddInParameter(oDbCommand, "@PARAMETROS", DbType.String, parametrosRubro);
            db.AddInParameter(oDbCommand, "@ModDetalle", DbType.String, ModalidadDetalle);
            oDbCommand.CommandTimeout = 1800;
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEFacturaCancelada>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFacturaCancelada reporte = null;
                while (dr.Read())
                {
                    reporte = new BEFacturaCancelada();

                    reporte.ANIO_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("ANIO_CANCELACION_DETALLE"));
                    reporte.MES_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("MES_CANCELACION_DETALLE"));
                    reporte.FEC_CANCELACION_DETALLE_DATE = dr.GetDateTime(dr.GetOrdinal("FEC_CANCELACION_DETALLE_DATE"));
                    reporte.FEC_CANCELACION_DETALLE = dr.GetString(dr.GetOrdinal("FEC_CANCELACION_DETALLE"));

                    reporte.ruc = dr.GetString(dr.GetOrdinal("ruc"));
                    reporte.usuario = dr.GetString(dr.GetOrdinal("usuario"));
                    reporte.documento = dr.GetString(dr.GetOrdinal("documento"));
                    reporte.femi = dr.GetString(dr.GetOrdinal("femi"));
                    reporte.documento = dr.GetString(dr.GetOrdinal("documento"));
                    reporte.periodo = dr.GetString(dr.GetOrdinal("periodo"));
                    reporte.fecan = dr.GetString(dr.GetOrdinal("fecan"));
                    reporte.importe = dr.GetDecimal(dr.GetOrdinal("importe"));
                    reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                    reporte.DIVISION_EST = dr.GetString(dr.GetOrdinal("DIVISION_EST"));
                    reporte.LICENCIA = dr.GetString(dr.GetOrdinal("LICENCIA"));
                    
                    lista.Add(reporte);

                }
            }
            return lista;
        }
        public List<BEFacturaCancelada> ListarReporteFacturaCanceladaEXCEL(string Fini, string Ffin, string oficina
            , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion
            , int? rubro, string parametrosRubro,string tipoenvio, string ModalidadDetalle)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_FACTURA_CANCELADA_EXCEL");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, 0);
            db.AddInParameter(oDbCommand, "@PARAMETROS", DbType.String, parametrosRubro);
            db.AddInParameter(oDbCommand, "@conFechaIngreso", DbType.Int32, conFechaIngreso);
            db.AddInParameter(oDbCommand, "@conFechaConfirmacion", DbType.Int32, conFechaConfirmacion);
            db.AddInParameter(oDbCommand, "@FINI_CON", DbType.DateTime, finiConfirmacion);
            db.AddInParameter(oDbCommand, "@FFIN_CON", DbType.DateTime, ffinConfirmacion);
            db.AddInParameter(oDbCommand, "@TipoEnvio", DbType.String, tipoenvio);
            db.AddInParameter(oDbCommand, "@ModDetalle", DbType.String, ModalidadDetalle);

            oDbCommand.CommandTimeout = 1800;
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEFacturaCancelada>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFacturaCancelada reporte = null;
                while (dr.Read())
                {
                    reporte = new BEFacturaCancelada();
                    if (!dr.IsDBNull(dr.GetOrdinal("ANIO_CANCELACION_DETALLE")))
                        reporte.ANIO_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("ANIO_CANCELACION_DETALLE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MES_CANCELACION_DETALLE")))
                        reporte.MES_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("MES_CANCELACION_DETALLE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FEC_CANCELACION_DETALLE_DATE")))
                        reporte.FEC_CANCELACION_DETALLE_DATE = dr.GetDateTime(dr.GetOrdinal("FEC_CANCELACION_DETALLE_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FEC_CANCELACION_DETALLE")))
                        reporte.FEC_CANCELACION_DETALLE = dr.GetString(dr.GetOrdinal("FEC_CANCELACION_DETALLE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ruc")))
                        reporte.ruc = dr.GetString(dr.GetOrdinal("ruc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("usuario")))
                        reporte.usuario = dr.GetString(dr.GetOrdinal("usuario"));
                    if (!dr.IsDBNull(dr.GetOrdinal("documento")))
                        reporte.documento = dr.GetString(dr.GetOrdinal("documento"));
                    if (!dr.IsDBNull(dr.GetOrdinal("femi")))
                        reporte.femi = dr.GetString(dr.GetOrdinal("femi"));
                    if (!dr.IsDBNull(dr.GetOrdinal("documento")))
                        reporte.documento = dr.GetString(dr.GetOrdinal("documento"));
                    if (!dr.IsDBNull(dr.GetOrdinal("periodo")))
                        reporte.periodo = dr.GetString(dr.GetOrdinal("periodo"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fecan")))
                        reporte.fecan = dr.GetString(dr.GetOrdinal("fecan"));
                    if (!dr.IsDBNull(dr.GetOrdinal("importe")))
                        reporte.importe = dr.GetDecimal(dr.GetOrdinal("importe"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RUBRO")))
                        reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DIVISION_EST")))
                        reporte.DIVISION_EST = dr.GetString(dr.GetOrdinal("DIVISION_EST"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LICENCIA")))
                        reporte.LICENCIA = dr.GetString(dr.GetOrdinal("LICENCIA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DEPARTAMENTO")))
                        reporte.DEPARTAMENTO = dr.GetString(dr.GetOrdinal("DEPARTAMENTO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                        reporte.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PROVINCIA")))
                        reporte.PROVINCIA = dr.GetString(dr.GetOrdinal("PROVINCIA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NODO")))
                        reporte.NODO = dr.GetString(dr.GetOrdinal("NODO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ESTABLECIMIENTO")))
                        reporte.ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                        reporte.EST_ID = dr.GetInt32(dr.GetOrdinal("EST_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Direccion")))
                        reporte.Direccion = dr.GetString(dr.GetOrdinal("Direccion"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TIPO_EST")))
                        reporte.TIPO_EST = dr.GetString(dr.GetOrdinal("TIPO_EST"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SUBTIPO_EST")))
                        reporte.SUBTIPO_EST = dr.GetString(dr.GetOrdinal("SUBTIPO_EST"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Fec_Confirmacion")))
                        reporte.Fec_Confirmacion = dr.GetString(dr.GetOrdinal("Fec_Confirmacion"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        reporte.LIC_ID = dr.GetInt32(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                        reporte.TIPO = dr.GetString(dr.GetOrdinal("TIPO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MONEDA")))
                        reporte.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MONTO_DOLAR")))
                        reporte.MONTO_DOLAR = dr.GetDecimal(dr.GetOrdinal("MONTO_DOLAR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ID_COBRO")))
                        reporte.ID_COBRO = dr.GetDecimal(dr.GetOrdinal("ID_COBRO"));
                    //


                    lista.Add(reporte);

                }
            }
            return lista;
        }
        public List<BEFacturaCancelada> ListarGrupoModXOficina(int? ID_OFF)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_LISTAR_GRUPOMODALIDAD_X_OFICINA");
            oDataBase.AddInParameter(oDbComand, "@ID_OFF", DbType.Int32, ID_OFF);

            var lista = new List<BEFacturaCancelada>();
            BEFacturaCancelada obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BEFacturaCancelada();
                    obs.MOG_ID = Convert.ToString(reader["MOG_ID"]);
                    obs.MOG_DESC = Convert.ToString(reader["MOG_DESC"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }

    }
}
