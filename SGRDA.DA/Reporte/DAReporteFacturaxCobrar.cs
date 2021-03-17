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
    public class DAReporteFacturaxCobrar
    {
        Database db = new DatabaseProviderFactory().Create("conexion");
        //creando el metodo listar


        public List<BEReporteFacturaxCobrar> ListarReporteFacturaxCobrar(string Fini, string Ffin, string oficina, int? Rubro, string parametrosRubro)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_FACTURA_X_COBRAR");
                db.AddInParameter(oDbCommand, "@F1", DbType.String, Fini);
                db.AddInParameter(oDbCommand, "@F2", DbType.String, Ffin);
                db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
                db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, Rubro);
                db.AddInParameter(oDbCommand, "@PARAMETROS", DbType.String, parametrosRubro);
                oDbCommand.CommandTimeout = 180;
                //db.ExecuteNonQuery(oDbCommand);
                var lista = new List<BEReporteFacturaxCobrar>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEReporteFacturaxCobrar reporte = null;
                    while (dr.Read())
                    {
                        reporte = new BEReporteFacturaxCobrar();
                        reporte.FECHA = dr.GetString(dr.GetOrdinal("FECHA"));
                        reporte.TD = dr.GetString(dr.GetOrdinal("TD"));
                        reporte.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        reporte.NUMERO = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
                        reporte.TOTAL = dr.GetDecimal(dr.GetOrdinal("TOTAL"));
                        reporte.SALDO = dr.GetDecimal(dr.GetOrdinal("SALDO"));
                        reporte.RUC = dr.GetString(dr.GetOrdinal("RUC"));
                        reporte.NOMBRE = dr.GetString(dr.GetOrdinal("NOMBRE"));
                        reporte.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                        reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));

                        reporte.IMPORTE_ORIGINAL = dr.GetDecimal(dr.GetOrdinal("IMPORTE_ORIGINAL"));
                        reporte.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
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
        public List<BEReporteFacturaxCobrar> ListarReporteFacturaxCobrar_EXCEL(string Fini, string Ffin, string oficina, int? Rubro, string tipoenvio, string parametrosRubro)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_FACTURA_X_COBRAR_EXCEL");
                db.AddInParameter(oDbCommand, "@F1", DbType.String, Fini);
                db.AddInParameter(oDbCommand, "@F2", DbType.String, Ffin);
                db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
                db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, Rubro);
                db.AddInParameter(oDbCommand, "@TipoEnvio", DbType.String, tipoenvio);
                db.AddInParameter(oDbCommand, "@PARAMETROS", DbType.String, parametrosRubro);
                oDbCommand.CommandTimeout = 180;
                //db.ExecuteNonQuery(oDbCommand);
                var lista = new List<BEReporteFacturaxCobrar>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEReporteFacturaxCobrar reporte = null;
                    while (dr.Read())
                    {
                        reporte = new BEReporteFacturaxCobrar();
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA")))
                            reporte.FECHA = dr.GetString(dr.GetOrdinal("FECHA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TD")))
                            reporte.TD = dr.GetString(dr.GetOrdinal("TD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            reporte.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO")))
                            reporte.NUMERO = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TOTAL")))
                            reporte.TOTAL = dr.GetDecimal(dr.GetOrdinal("TOTAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SALDO")))
                            reporte.SALDO = dr.GetDecimal(dr.GetOrdinal("SALDO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RUC")))
                            reporte.RUC = dr.GetString(dr.GetOrdinal("RUC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE")))
                            reporte.NOMBRE = dr.GetString(dr.GetOrdinal("NOMBRE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                            reporte.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RUBRO")))
                            reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DEPARTAMENTO")))
                            reporte.DEPARTAMENTO = dr.GetString(dr.GetOrdinal("DEPARTAMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PROVINCIA")))
                            reporte.PROVINCIA = dr.GetString(dr.GetOrdinal("PROVINCIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                            reporte.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            reporte.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            reporte.EST_ID = dr.GetInt32(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NODO")))
                            reporte.NODO = dr.GetString(dr.GetOrdinal("NODO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Direccion")))
                            reporte.Direccion = dr.GetString(dr.GetOrdinal("Direccion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_EST")))
                            reporte.TIPO_EST = dr.GetString(dr.GetOrdinal("TIPO_EST"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SUBTIPO_EST")))
                            reporte.SUBTIPO_EST = dr.GetString(dr.GetOrdinal("SUBTIPO_EST"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SUBTIPO_EST")))
                            reporte.LIC_ID = dr.GetInt32(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                            reporte.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                            reporte.TIPO = dr.GetString(dr.GetOrdinal("TIPO"));


                        if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE_ORIGINAL")))
                            reporte.IMPORTE_ORIGINAL = dr.GetDecimal(dr.GetOrdinal("IMPORTE_ORIGINAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONEDA")))
                            reporte.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                            reporte.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RUBRO_MODALIDAD")))
                            reporte.RUBRO_MODALIDAD = dr.GetString(dr.GetOrdinal("RUBRO_MODALIDAD"));

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

        //public List<BEReporteFacturaxCobrar> ListarReporteFacturaxCobrar(string Fini, string Ffin, string oficina, int? Rubro, string parametros)
        //{
        //    try
        //    {
        //        Database db = new DatabaseProviderFactory().Create("conexion");
        //        DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_FACTURA_X_COBRAR");
        //        db.AddInParameter(oDbCommand, "@F1", DbType.String, Fini);
        //        db.AddInParameter(oDbCommand, "@F2", DbType.String, Ffin);
        //        db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
        //        db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, Rubro);
        //       db.AddInParameter(oDbCommand, "@Parametros", DbType.String, parametros);

        //       //db.ExecuteNonQuery(oDbCommand);
        //       var lista = new List<BEReporteFacturaxCobrar>();
        //        using (IDataReader dr = db.ExecuteReader(oDbCommand))
        //        {
        //            BEReporteFacturaxCobrar reporte = null;
        //            while (dr.Read())
        //            {
        //                reporte = new BEReporteFacturaxCobrar();
        //                reporte.FECHA = dr.GetString(dr.GetOrdinal("FECHA"));
        //                reporte.TD = dr.GetString(dr.GetOrdinal("TD"));
        //                reporte.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
        //                reporte.NUMERO = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
        //                reporte.TOTAL = dr.GetDecimal(dr.GetOrdinal("TOTAL"));
        //                reporte.RUC = dr.GetString(dr.GetOrdinal("RUC"));
        //                reporte.NOMBRE = dr.GetString(dr.GetOrdinal("NOMBRE"));
        //                reporte.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
        //                reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
        //                lista.Add(reporte);

        //            }
        //        }
        //        return lista;

        //    }catch(Exception ex){
        //        return null;
        //    }

        //}




    }
}
