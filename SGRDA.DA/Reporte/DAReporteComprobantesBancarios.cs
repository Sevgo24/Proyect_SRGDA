using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities.Reporte;
using System.Data.Common;
using Microsoft.SqlServer.Server;

namespace SGRDA.DA.Reporte
{
    public class DAReporteComprobantesBancarios
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEComprobanteBancario> ReporteComprobanteBancario(DateTime Fini, DateTime Ffin, string oficina,
            int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion, string estado
            , string con_Rechazo, string ini_Rech, string fin_Rech, int idBanco)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_REP_DEPOSITOS_BANCARIOS_2");
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@FECINI", DbType.DateTime, Fini);
            db.AddInParameter(oDbCommand, "@FECFIN", DbType.DateTime, Ffin);

            db.AddInParameter(oDbCommand, "@conFechaIngreso", DbType.Int32, conFechaIngreso);
            db.AddInParameter(oDbCommand, "@conFechaConfirmacion", DbType.Int32, conFechaConfirmacion);
            db.AddInParameter(oDbCommand, "@FINI_CON", DbType.DateTime, finiConfirmacion);
            db.AddInParameter(oDbCommand, "@FFIN_CON", DbType.DateTime, ffinConfirmacion);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.String, estado);

            db.AddInParameter(oDbCommand, "@conFechaRechazo", DbType.Int32, con_Rechazo);
            db.AddInParameter(oDbCommand, "@FINI_RECH", DbType.DateTime, ini_Rech);
            db.AddInParameter(oDbCommand, "@FFIN_RECH", DbType.DateTime, fin_Rech);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Int32, idBanco);

            var lista = new List<BEComprobanteBancario>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEComprobanteBancario reporte = null;
                while (dr.Read())
                {
                    reporte = new BEComprobanteBancario();
                    reporte.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                    reporte.NUM_CUENTA = dr.GetString(dr.GetOrdinal("NUM_CUENTA"));
                    reporte.FECHA_DEP = dr.GetString(dr.GetOrdinal("FECHA_DEP"));
                    reporte.REC_CODECONFIRMED = dr.GetString(dr.GetOrdinal("REC_CODECONFIRMED"));
                    reporte.REC_PVALUE = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE"));

                    reporte.ESTADO_DEPOSITO = dr.GetString(dr.GetOrdinal("ESTADO_DEPOSITO"));
                    reporte.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                    reporte.BANCO_DESTINO = dr.GetString(dr.GetOrdinal("BANCO_DESTINO"));
                    reporte.CUENTA_DESTINO = dr.GetString(dr.GetOrdinal("CUENTA_DESTINO"));

                    reporte.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));

                    reporte.MREC_ID = dr.GetDecimal(dr.GetOrdinal("MREC_ID"));
                    reporte.REC_OBSERVATION = dr.GetString(dr.GetOrdinal("REC_OBSERVATION"));
                    reporte.REC_REFERENCE = dr.GetString(dr.GetOrdinal("REC_REFERENCE"));

                    if(dr.GetDateTime(dr.GetOrdinal("FECHA_CONFIRMACION")).ToString("MM/dd/yyyy hh:mm tt").Equals("01/01/1900 12:00 a.m."))
                    {
                        reporte.FECHA_CONFIRMACION = "";

                    }else
                    {
                        reporte.FECHA_CONFIRMACION = dr.GetDateTime(dr.GetOrdinal("FECHA_CONFIRMACION")).ToString("MM/dd/yyyy hh:mm tt");
                    }
                    reporte.FECHA_INGRESO = dr.GetDateTime(dr.GetOrdinal("FECHA_INGRESO_DATE")).ToString("MM/dd/yyyy hh:mm tt");
                    reporte.FECHA_INGRESO_DATE = dr.GetDateTime(dr.GetOrdinal("FECHA_INGRESO_DATE"));

                    reporte.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                    reporte.REC_BALANCE = dr.GetDecimal(dr.GetOrdinal("REC_BALANCE")); 
                    reporte.RECAUDO = dr.GetDecimal(dr.GetOrdinal("RECAUDO"));
                    reporte.ACUMULADO_FACTURAS = dr.GetDecimal(dr.GetOrdinal("ACUMULADO_FACTURAS"));
                    reporte.USUARIO_MODIF = dr.GetString(dr.GetOrdinal("USUARIO_MODIF"));
                    reporte.FECHA_MODIF = dr.GetString(dr.GetOrdinal("FECHA_MODIF"));
                    lista.Add(reporte);
                }
            }
            return lista;
        }
        

        public List<BEComprobanteBancario> ReporteComprobanteBancario_Excel(DateTime Fini, DateTime Ffin, string oficina,
            int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion, string estado
            , string con_Rechazo, string ini_Rech, string fin_Rech, int idBanco)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_REP_DEPOSITOS_BANCARIOS_2_EXCEL");
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@FECINI", DbType.DateTime, Fini);
            db.AddInParameter(oDbCommand, "@FECFIN", DbType.DateTime, Ffin);

            db.AddInParameter(oDbCommand, "@conFechaIngreso", DbType.Int32, conFechaIngreso);
            db.AddInParameter(oDbCommand, "@conFechaConfirmacion", DbType.Int32, conFechaConfirmacion);
            db.AddInParameter(oDbCommand, "@FINI_CON", DbType.DateTime, finiConfirmacion);
            db.AddInParameter(oDbCommand, "@FFIN_CON", DbType.DateTime, ffinConfirmacion);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.String, estado);

            db.AddInParameter(oDbCommand, "@conFechaRechazo", DbType.Int32, con_Rechazo);
            db.AddInParameter(oDbCommand, "@FINI_RECH", DbType.DateTime, ini_Rech);
            db.AddInParameter(oDbCommand, "@FFIN_RECH", DbType.DateTime, fin_Rech);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Int32, idBanco);

            var lista = new List<BEComprobanteBancario>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEComprobanteBancario reporte = null;
                while (dr.Read())
                {
                    reporte = new BEComprobanteBancario();
                    reporte.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                    reporte.NUM_CUENTA = dr.GetString(dr.GetOrdinal("NUM_CUENTA"));
                    reporte.FECHA_DEP = dr.GetString(dr.GetOrdinal("FECHA_DEP"));
                    reporte.REC_CODECONFIRMED = dr.GetString(dr.GetOrdinal("REC_CODECONFIRMED"));
                    reporte.REC_PVALUE = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE"));

                    reporte.ESTADO_DEPOSITO = dr.GetString(dr.GetOrdinal("ESTADO_DEPOSITO"));
                    reporte.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                    reporte.BANCO_DESTINO = dr.GetString(dr.GetOrdinal("BANCO_DESTINO"));
                    reporte.CUENTA_DESTINO = dr.GetString(dr.GetOrdinal("CUENTA_DESTINO"));

                    reporte.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));

                    reporte.MREC_ID = dr.GetDecimal(dr.GetOrdinal("MREC_ID"));
                    reporte.REC_OBSERVATION = dr.GetString(dr.GetOrdinal("REC_OBSERVATION"));
                    reporte.REC_REFERENCE = dr.GetString(dr.GetOrdinal("REC_REFERENCE"));

                    if (dr.GetDateTime(dr.GetOrdinal("FECHA_CONFIRMACION")).ToString("MM/dd/yyyy hh:mm tt").Equals("01/01/1900 12:00 a.m."))
                    {
                        reporte.FECHA_CONFIRMACION = "";

                    }
                    else
                    {
                        reporte.FECHA_CONFIRMACION = dr.GetDateTime(dr.GetOrdinal("FECHA_CONFIRMACION")).ToString("MM/dd/yyyy hh:mm tt");
                    }
                    reporte.FECHA_INGRESO = dr.GetDateTime(dr.GetOrdinal("FECHA_INGRESO_DATE")).ToString("MM/dd/yyyy hh:mm tt");
                    reporte.FECHA_INGRESO_DATE = dr.GetDateTime(dr.GetOrdinal("FECHA_INGRESO_DATE"));

                    reporte.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                    reporte.REC_BALANCE = dr.GetDecimal(dr.GetOrdinal("REC_BALANCE"));
                    reporte.RECAUDO = dr.GetDecimal(dr.GetOrdinal("RECAUDO"));
                    reporte.ACUMULADO_FACTURAS = dr.GetDecimal(dr.GetOrdinal("ACUMULADO_FACTURAS"));
                    reporte.USUARIO_MODIF = dr.GetString(dr.GetOrdinal("USUARIO_MODIF"));
                    reporte.FECHA_MODIF = dr.GetString(dr.GetOrdinal("FECHA_MODIF"));
                    reporte.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                    reporte.COD_LICENCIA = dr.GetDecimal(dr.GetOrdinal("COD_LICENCIA"));
                    reporte.RUBRO_NOMBRE = dr.GetString(dr.GetOrdinal("RUBRO_NOMBRE"));
                    reporte.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                    reporte.NUMERO = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
                    reporte.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                    reporte.Correlativo = dr.GetInt64(dr.GetOrdinal("Correlativo"));
                    lista.Add(reporte);
                }
            }
            return lista;
        }
    }
}
