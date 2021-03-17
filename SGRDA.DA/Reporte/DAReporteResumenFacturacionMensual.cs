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
using SGRDA.Entities.Reporte;

namespace SGRDA.DA.Reporte
{
    public class DAReporteResumenFacturacionMensual
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        public List<BEReporteResumenFacturacionMensual> ReporteResumenFacturacionMensual(string Fini, string Ffin, string FiniCan, string FfinCan, string Finicon, string FfinCon, string oficina, string parametros, int DEPARTAMENTO, int PROVINCIA, int DISTRITO, string estado, string TipoDoc,string tipoenvio,string ModalidadDetalle)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("RESUMEN_FACTURACION_MENSUAL");
            db.AddInParameter(oDbCommand, "@FECHAINI", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@FECHAFIN", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@FECHACANINI", DbType.String, FiniCan);
            db.AddInParameter(oDbCommand, "@FECHACANFIN", DbType.String, FfinCan);
            db.AddInParameter(oDbCommand, "@FECHACONINI", DbType.String, Finicon);
            db.AddInParameter(oDbCommand, "@FECHACONFIN", DbType.String, FfinCon);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@ParametrosRubro", DbType.String, parametros);
            //db.AddInParameter(oDbCommand, "@UBIGEO", DbType.Int32, ubigeo);
            db.AddInParameter(oDbCommand, "@DEPARTAMENTO", DbType.Int32, DEPARTAMENTO);
            db.AddInParameter(oDbCommand, "@PROVINCIA", DbType.Int32, PROVINCIA);
            db.AddInParameter(oDbCommand, "@DISTRITO", DbType.Int32, DISTRITO);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.String, estado);
            db.AddInParameter(oDbCommand, "@TIPO_DOC", DbType.String, TipoDoc);
            db.AddInParameter(oDbCommand, "@TipoEnvio", DbType.String, tipoenvio);
            db.AddInParameter(oDbCommand, "@ModDetalle", DbType.String, ModalidadDetalle);
            oDbCommand.CommandTimeout = 1800;
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEReporteResumenFacturacionMensual>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEReporteResumenFacturacionMensual reporte = null;
                while (dr.Read())
                {
                    reporte = new BEReporteResumenFacturacionMensual();
                    if (dr.GetString(dr.GetOrdinal("FECHA_EMISION")) == null)
                    {
                        reporte.FECHA_EMISION = " ";
                    }
                    else
                    { reporte.FECHA_EMISION = dr.GetString(dr.GetOrdinal("FECHA_EMISION")); }

                    if (dr.GetString(dr.GetOrdinal("TD")) == null)
                    {
                        reporte.TD = " ";
                    }
                    else
                    { reporte.TD = dr.GetString(dr.GetOrdinal("TD")); }

                    if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                        reporte.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("NRO")))
                        reporte.NRO = dr.GetString(dr.GetOrdinal("NRO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("RUC")))
                        reporte.RUC = dr.GetString(dr.GetOrdinal("RUC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("OFICINA")))
                        reporte.OFICINA = dr.GetString(dr.GetOrdinal("OFICINA"));

                    if (!dr.IsDBNull(dr.GetOrdinal("RUBRO")))
                        reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                        reporte.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("RAZON_SOCIAL")))
                        reporte.RAZON_SOCIAL = dr.GetString(dr.GetOrdinal("RAZON_SOCIAL"));

                    if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_COMERCIAL")))
                        reporte.NOMBRE_COMERCIAL = dr.GetString(dr.GetOrdinal("NOMBRE_COMERCIAL"));

                    if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                        reporte.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                        reporte.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FEC_CANCELACION")))
                        reporte.FEC_CANCELACION = dr.GetString(dr.GetOrdinal("FEC_CANCELACION"));

                    if (!dr.IsDBNull(dr.GetOrdinal("TOTAL")))
                        reporte.TOTAL = dr.GetDecimal(dr.GetOrdinal("TOTAL"));

                    if (!dr.IsDBNull(dr.GetOrdinal("PAGOS")))
                        reporte.PAGOS = dr.GetDecimal(dr.GetOrdinal("PAGOS"));

                    if (!dr.IsDBNull(dr.GetOrdinal("SALDO")))
                        reporte.SALDO = dr.GetDecimal(dr.GetOrdinal("SALDO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                        reporte.TIPO = dr.GetString(dr.GetOrdinal("TIPO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("SUBTIPO")))
                        reporte.SUBTIPO = dr.GetString(dr.GetOrdinal("SUBTIPO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                        reporte.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MONTO_NC")))
                        reporte.MONTO_NC = dr.GetDecimal(dr.GetOrdinal("MONTO_NC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("RECAUDO")))
                        reporte.RECAUDO = dr.GetDecimal(dr.GetOrdinal("RECAUDO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("TIPO_ENVIO")))
                        reporte.TIPO_ENVIO = dr.GetString(dr.GetOrdinal("TIPO_ENVIO"));
                  
                    if (!dr.IsDBNull(dr.GetOrdinal("ID_ESTABLECIMIENTO")))
                        reporte.ID_ESTABLECIMIENTO = dr.GetInt32(dr.GetOrdinal("ID_ESTABLECIMIENTO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CORRELATIVO")))
                        reporte.CORRELATIVO = dr.GetString(dr.GetOrdinal("CORRELATIVO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CONFIRMACION")))
                        reporte.FECHA_CONFIRMACION = dr.GetString(dr.GetOrdinal("FECHA_CONFIRMACION"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        reporte.LIC_ID = dr.GetInt32(dr.GetOrdinal("LIC_ID"));


                    lista.Add(reporte);
                }
            }
            return lista;
        }
    }
}
