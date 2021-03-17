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
    public class DAReporteDistribucion
    {
        //Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        // NETA
        public List<BEReporteDistribucion> ListadDistribucion_Neta(string Fini, string Ffin)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DISTRIBUCION_NETA");
                db.AddInParameter(oDbCommand, "@FEC_INI", DbType.String, Fini);
                db.AddInParameter(oDbCommand, "@FEC_FIN", DbType.String, Ffin);
                oDbCommand.CommandTimeout = 1800;
                //db.ExecuteNonQuery(oDbCommand);
                var lista = new List<BEReporteDistribucion>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEReporteDistribucion reporte = new BEReporteDistribucion();
                    while (dr.Read())
                    {
                        reporte = new BEReporteDistribucion();
                        reporte.ANIO_CONTABLE = dr.GetInt32(dr.GetOrdinal("ANIO_CONTABLE"));
                        reporte.MES_CONTABLE = dr.GetInt32(dr.GetOrdinal("MES_CONTABLE"));
                        reporte.RECAUDO = dr.GetDecimal(dr.GetOrdinal("RECAUDO"));
                        reporte.NC = dr.GetDecimal(dr.GetOrdinal("NC"));
                        reporte.NETO = dr.GetDecimal(dr.GetOrdinal("NETO"));
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

        // RESUMEN
        public List<BEReporteDistribucion> ListarDistribucion_Resumen(string Fini, string Ffin)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DISTRIBUCION_RESUMEN");
                db.AddInParameter(oDbCommand, "@FEC_INI", DbType.String, Fini);
                db.AddInParameter(oDbCommand, "@FEC_FIN", DbType.String, Ffin);
                oDbCommand.CommandTimeout = 1800;
                //db.ExecuteNonQuery(oDbCommand);
                var lista = new List<BEReporteDistribucion>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEReporteDistribucion reporte = new BEReporteDistribucion();
                    while (dr.Read())
                    {
                        reporte = new BEReporteDistribucion();
                        if (!dr.IsDBNull(dr.GetOrdinal("GRUPO_ID")))
                            reporte.GRUPO_ID = dr.GetString(dr.GetOrdinal("GRUPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GRUPO")))
                            reporte.GRUPO = dr.GetString(dr.GetOrdinal("GRUPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SAP_CODIGO")))
                            reporte.SAP_CODIGO = dr.GetString(dr.GetOrdinal("SAP_CODIGO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_ID")))
                            reporte.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                            reporte.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));


                        if (!dr.IsDBNull(dr.GetOrdinal("DERECHO")))
                            reporte.DERECHO = dr.GetString(dr.GetOrdinal("DERECHO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE_RECAUDADO_SOLES")))
                            reporte.IMPORTE_RECAUDADO_SOLES = dr.GetDecimal(dr.GetOrdinal("IMPORTE_RECAUDADO_SOLES"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ANIO_CONTABLE")))
                            reporte.ANIO_CONTABLE = dr.GetInt32(dr.GetOrdinal("ANIO_CONTABLE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MES_CONTABLE")))
                            reporte.MES_CONTABLE = dr.GetInt32(dr.GetOrdinal("MES_CONTABLE"));

                        // DESCUENTOS
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_AD")))
                            reporte.MOD_AD = dr.GetDecimal(dr.GetOrdinal("MOD_AD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_AD_MONTO")))
                            reporte.MOD_AD_MONTO = dr.GetDecimal(dr.GetOrdinal("MOD_AD_MONTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_SC")))
                            reporte.MOD_SC = dr.GetDecimal(dr.GetOrdinal("MOD_SC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_SC_MONTO")))
                            reporte.MOD_SC_MONTO = dr.GetDecimal(dr.GetOrdinal("MOD_SC_MONTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_AC")))
                            reporte.MOD_AC = dr.GetDecimal(dr.GetOrdinal("MOD_AC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_AC_MONTO")))
                            reporte.MOD_AC_MONTO = dr.GetDecimal(dr.GetOrdinal("MOD_AC_MONTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_LY")))
                            reporte.MOD_LY = dr.GetDecimal(dr.GetOrdinal("MOD_LY"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_LY_MONTO")))
                            reporte.MOD_LY_MONTO = dr.GetDecimal(dr.GetOrdinal("MOD_LY_MONTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_FC")))
                            reporte.MOD_FC = dr.GetDecimal(dr.GetOrdinal("MOD_FC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_FC_MONTO")))
                            reporte.MOD_FC_MONTO = dr.GetDecimal(dr.GetOrdinal("MOD_FC_MONTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_AR")))
                            reporte.MOD_AR = dr.GetDecimal(dr.GetOrdinal("MOD_AR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_AR_MONTO")))
                            reporte.MOD_AR_MONTO = dr.GetDecimal(dr.GetOrdinal("MOD_AR_MONTO"));


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

        // RESUMEN AGRUPADO
        //public List<BEReporteDistribucion> ListarDistribucion_Resumen_Agrupado(string Fini, string Ffin)
        //{
        //    try
        //    {
        //        Database db = new DatabaseProviderFactory().Create("conexion");
        //        DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DISTRIBUCION_RESUMEN_AGRUPADO");
        //        db.AddInParameter(oDbCommand, "@FEC_INI", DbType.String, Fini);
        //        db.AddInParameter(oDbCommand, "@FEC_FIN", DbType.String, Ffin);
        //        oDbCommand.CommandTimeout = 1800;
        //        //db.ExecuteNonQuery(oDbCommand);
        //        var lista = new List<BEReporteDistribucion>();
        //        using (IDataReader dr = db.ExecuteReader(oDbCommand))
        //        {
        //            BEReporteDistribucion reporte = new BEReporteDistribucion();
        //            while (dr.Read())
        //            {
        //                reporte = new BEReporteDistribucion();
        //                if (!dr.IsDBNull(dr.GetOrdinal("GRUPO_ID")))
        //                    reporte.GRUPO_ID = dr.GetString(dr.GetOrdinal("GRUPO_ID"));
        //                if (!dr.IsDBNull(dr.GetOrdinal("GRUPO")))
        //                    reporte.GRUPO = dr.GetString(dr.GetOrdinal("GRUPO"));
        //                if (!dr.IsDBNull(dr.GetOrdinal("SAP_CODIGO")))
        //                    reporte.SAP_CODIGO = dr.GetString(dr.GetOrdinal("SAP_CODIGO"));
        //                if (!dr.IsDBNull(dr.GetOrdinal("MOD_ID")))
        //                    reporte.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
        //                if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
        //                    reporte.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));


        //                if (!dr.IsDBNull(dr.GetOrdinal("DERECHO")))
        //                    reporte.DERECHO = dr.GetString(dr.GetOrdinal("DERECHO"));
        //                if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE_RECAUDADO_SOLES")))
        //                    reporte.IMPORTE_RECAUDADO_SOLES = dr.GetDecimal(dr.GetOrdinal("IMPORTE_RECAUDADO_SOLES"));
        //                if (!dr.IsDBNull(dr.GetOrdinal("ANIO_CONTABLE")))
        //                    reporte.ANIO_CONTABLE = dr.GetInt32(dr.GetOrdinal("ANIO_CONTABLE"));
        //                if (!dr.IsDBNull(dr.GetOrdinal("MES_CONTABLE")))
        //                    reporte.MES_CONTABLE = dr.GetInt32(dr.GetOrdinal("MES_CONTABLE"));

        //                lista.Add(reporte);
        //            }
        //        }
        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}


        // DETALLADO
        public List<BEReporteDistribucion> ListarDistribucion_Detallado(string Fini, string Ffin)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DISTRIBUCION_DETALLADO");
                db.AddInParameter(oDbCommand, "@FEC_INI", DbType.String, Fini);
                db.AddInParameter(oDbCommand, "@FEC_FIN", DbType.String, Ffin);
                oDbCommand.CommandTimeout = 1800;
                //db.ExecuteNonQuery(oDbCommand);
                var lista = new List<BEReporteDistribucion>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEReporteDistribucion reporte = new BEReporteDistribucion();
                    while (dr.Read())
                    {
                        reporte = new BEReporteDistribucion();
                        reporte.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GRUPO_ID")))
                            reporte.GRUPO_ID = dr.GetString(dr.GetOrdinal("GRUPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GRUPO")))
                            reporte.GRUPO = dr.GetString(dr.GetOrdinal("GRUPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SAP_CODIGO")))
                            reporte.SAP_CODIGO = dr.GetString(dr.GetOrdinal("SAP_CODIGO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_ID")))
                            reporte.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                            reporte.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DERECHO")))
                            reporte.DERECHO = dr.GetString(dr.GetOrdinal("DERECHO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CLIENTE")))
                            reporte.CLIENTE = dr.GetString(dr.GetOrdinal("CLIENTE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            reporte.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LICENCIA")))
                            reporte.LICENCIA = dr.GetString(dr.GetOrdinal("LICENCIA"));


                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            reporte.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTABLECIMIENTO")))
                            reporte.ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_DOCUMENTO")))
                            reporte.TIPO_DOCUMENTO = dr.GetString(dr.GetOrdinal("TIPO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            reporte.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO")))
                            reporte.NUMERO = dr.GetDecimal(dr.GetOrdinal("NUMERO"));



                        if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                            reporte.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONEDA_FACTURA")))
                            reporte.MONEDA_FACTURA = dr.GetString(dr.GetOrdinal("MONEDA_FACTURA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_FACTURADO")))
                            reporte.MONTO_FACTURADO = dr.GetDecimal(dr.GetOrdinal("MONTO_FACTURADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE_RECAUDADO_SOLES")))
                            reporte.IMPORTE_RECAUDADO_SOLES = dr.GetDecimal(dr.GetOrdinal("IMPORTE_RECAUDADO_SOLES"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ANIO_CONTABLE")))
                            reporte.ANIO_CONTABLE = dr.GetInt32(dr.GetOrdinal("ANIO_CONTABLE"));


                        if (!dr.IsDBNull(dr.GetOrdinal("MES_CONTABLE")))
                            reporte.MES_CONTABLE = dr.GetInt32(dr.GetOrdinal("MES_CONTABLE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CONTABLE")))
                            reporte.FECHA_CONTABLE = dr.GetDateTime(dr.GetOrdinal("FECHA_CONTABLE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA")))
                            reporte.OFICINA = dr.GetString(dr.GetOrdinal("OFICINA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RAZON_SOCIAL")))
                            reporte.RAZON_SOCIAL = dr.GetString(dr.GetOrdinal("RAZON_SOCIAL"));


                        if (!dr.IsDBNull(dr.GetOrdinal("BEC_ESPECIAL")))
                            reporte.BEC_ESPECIAL = dr.GetInt32(dr.GetOrdinal("BEC_ESPECIAL"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BANCO_DESTINO")))
                            reporte.BANCO_DESTINO = dr.GetString(dr.GetOrdinal("BANCO_DESTINO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CTA")))
                            reporte.CTA = dr.GetString(dr.GetOrdinal("CTA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CTA_SAP")))
                            reporte.CTA_SAP = dr.GetString(dr.GetOrdinal("CTA_SAP"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_DEPOSITO")))
                            reporte.FECHA_DEPOSITO = dr.GetDateTime(dr.GetOrdinal("FECHA_DEPOSITO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO_CONFIRMACION")))
                            reporte.NRO_CONFIRMACION = dr.GetString(dr.GetOrdinal("NRO_CONFIRMACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_DEPOSITADO")))
                            reporte.MONTO_DEPOSITADO = dr.GetDecimal(dr.GetOrdinal("MONTO_DEPOSITADO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FEC_EMI_FACTURA")))
                            reporte.FEC_EMI_FACTURA = Convert.ToDateTime(dr.GetString(dr.GetOrdinal("FEC_EMI_FACTURA")));
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_CLIENTE_SAP")))
                            reporte.ID_CLIENTE_SAP = dr.GetString(dr.GetOrdinal("ID_CLIENTE_SAP"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_CLIENTE")))
                            reporte.ID_CLIENTE = dr.GetDecimal(dr.GetOrdinal("ID_CLIENTE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_COBRO")))
                            reporte.ID_COBRO = dr.GetDecimal(dr.GetOrdinal("ID_COBRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REC_PID")))
                            reporte.REC_PID = dr.GetDecimal(dr.GetOrdinal("REC_PID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_FACTURA")))
                            reporte.ID_FACTURA = dr.GetDecimal(dr.GetOrdinal("ID_FACTURA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_FACTURADET")))
                            reporte.ID_FACTURADET = dr.GetDecimal(dr.GetOrdinal("ID_FACTURADET"));

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



        public List<BEReporteDistribucion> ListaContableDesplegable()
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            List<BEReporteDistribucion> lst = new List<BEReporteDistribucion>();
            BEReporteDistribucion item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REP_DISTRIBUCION"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEReporteDistribucion();
                            item.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                            item.TIPO_REPORTE = dr.GetString(dr.GetOrdinal("DESCRIPCION"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lst;
        }




    }
}
