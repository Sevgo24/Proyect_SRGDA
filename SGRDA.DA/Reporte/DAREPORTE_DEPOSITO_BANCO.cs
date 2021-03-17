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
    public class DAREPORTE_DEPOSITO_BANCO
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        //creando el metodo listar


        public List<BEREPORTE_DEPOSITO_BANCO> ListarReporteDepositoBancario(string Fini, string Ffin, string oficina, int? Rubro)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_DEPOSITO_BANCO");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, Rubro);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEREPORTE_DEPOSITO_BANCO>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREPORTE_DEPOSITO_BANCO reporte = null;
                while (dr.Read())
                {
                    reporte = new BEREPORTE_DEPOSITO_BANCO();
                    reporte.BEC = dr.GetDecimal(dr.GetOrdinal("BEC"));
                    reporte.fec_deposito = dr.GetString(dr.GetOrdinal("fec_deposito"));
                    reporte.cuenta_corriente = dr.GetString(dr.GetOrdinal("cuenta_corriente"));
                    reporte.nro_operacion = dr.GetString(dr.GetOrdinal("nro_operacion"));
                    reporte.documento = dr.GetString(dr.GetOrdinal("documento"));
                    reporte.importe = dr.GetDecimal(dr.GetOrdinal("importe"));
                    reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                    reporte.obervacion = dr.GetString(dr.GetOrdinal("obervacion"));
                    reporte.DIVISION_EST = dr.GetString(dr.GetOrdinal("DIVISION_EST"));
                    reporte.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                    lista.Add(reporte);

                }
            }
            return lista;
        }

        //RE´PORTE RECAUDACION
        public List<BEREPORTE_DEPOSITO_BANCO> ListarReporteDepositoBancario_Cobro(string Fini, string Ffin, string oficina
                    , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion
                    , int? rubro, string TipoCobro)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_DEPOSITO_BANCO_COBROS");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, rubro);

            db.AddInParameter(oDbCommand, "@conFechaIngreso", DbType.Int32, conFechaIngreso);
            db.AddInParameter(oDbCommand, "@conFechaConfirmacion", DbType.Int32, conFechaConfirmacion);
            db.AddInParameter(oDbCommand, "@FINI_CON", DbType.DateTime, finiConfirmacion);
            db.AddInParameter(oDbCommand, "@FFIN_CON", DbType.DateTime, ffinConfirmacion);
            db.AddInParameter(oDbCommand, "@TipoCobro", DbType.String, TipoCobro);
            
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEREPORTE_DEPOSITO_BANCO>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREPORTE_DEPOSITO_BANCO reporte = null;
                while (dr.Read())
                {
                    reporte = new BEREPORTE_DEPOSITO_BANCO();
                    reporte.ANIO_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("ANIO_CANCELACION_DETALLE"));
                    reporte.MES_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("MES_CANCELACION_DETALLE"));
                    reporte.FEC_CANCELACION_DETALLE_DATE = dr.GetDateTime(dr.GetOrdinal("FEC_CANCELACION_DETALLE_DATE"));
                    reporte.FEC_CANCELACION_DETALLE = dr.GetString(dr.GetOrdinal("FEC_CANCELACION_DETALLE"));
                    
                    reporte.nro_operacion = dr.GetString(dr.GetOrdinal("NRO_OPERACION"));
                    reporte.ID_COBRO = dr.GetDecimal(dr.GetOrdinal("ID_COBRO"));
                    reporte.FEC_DEPOSITO = dr.GetString(dr.GetOrdinal("FEC_DEPOSITO"));
                    reporte.FEC_DEPOSITO_CONFIRMACION = dr.GetString(dr.GetOrdinal("FEC_DEPOSITO_CONFIRMACION"));
                    reporte.CUENTA = dr.GetString(dr.GetOrdinal("CUENTA"));
                    reporte.DEPOSITO_MONTO = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_MONTO"));
                    reporte.DEPOSITO_SALDO = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_SALDO"));
                    reporte.DEPOSITO_MONTO_SOLES = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_MONTO_SOLES"));
                    reporte.DEPOSITO_SALDO_SOLES = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_SALDO_SOLES"));

                    reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                    reporte.RUBRO_NOMBRE = dr.GetString(dr.GetOrdinal("RUBRO_NOMBRE"));
                    reporte.DOCUMENTO = dr.GetString(dr.GetOrdinal("DOCUMENTO"));
                    //reporte.FEC_COBRO_DETALLE = dr.GetString(dr.GetOrdinal("FEC_COBRO_DETALLE"));

                    reporte.IMPORTE_DETALLE_DEPOSITO = dr.GetDecimal(dr.GetOrdinal("IMPORTE_DETALLE_DEPOSITO"));
                    reporte.NODO_ID = dr.GetDecimal(dr.GetOrdinal("NODO_ID"));
                    reporte.NODO = dr.GetString(dr.GetOrdinal("NODO"));
                    reporte.TERRITORIO = dr.GetInt32(dr.GetOrdinal("TERRITORIO"));
                    reporte.TIPOCOBRO = dr.GetString(dr.GetOrdinal("TIPOCOBRO"));
                    lista.Add(reporte);

                }
            }
            return lista;
        }

        // REPORTE CONTABLE
        public List<BEREPORTE_DEPOSITO_BANCO> ListarReporteContableDepositoBancario(string Fini, string Ffin, string oficina    , decimal idContable
                    , int? rubro)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_CONTABLE_DEPOSITO_BANCO");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, rubro);
            db.AddInParameter(oDbCommand, "@idContable", DbType.Decimal, idContable);
            
            var lista = new List<BEREPORTE_DEPOSITO_BANCO>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREPORTE_DEPOSITO_BANCO reporte = null;
                while (dr.Read())
                {
                    reporte = new BEREPORTE_DEPOSITO_BANCO();
                    reporte.ANIO_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("ANIO_CANCELACION_DETALLE"));
                    reporte.MES_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("MES_CANCELACION_DETALLE"));
                    reporte.FEC_CANCELACION_DETALLE_DATE = dr.GetDateTime(dr.GetOrdinal("FEC_CANCELACION_DETALLE_DATE"));
                    reporte.FEC_CANCELACION_DETALLE = dr.GetString(dr.GetOrdinal("FEC_CANCELACION_DETALLE"));

                    reporte.nro_operacion = dr.GetString(dr.GetOrdinal("NRO_OPERACION"));
                    reporte.ID_COBRO = dr.GetDecimal(dr.GetOrdinal("ID_COBRO"));
                    reporte.FEC_DEPOSITO = dr.GetString(dr.GetOrdinal("FEC_DEPOSITO"));
                    reporte.FEC_DEPOSITO_CONFIRMACION = dr.GetString(dr.GetOrdinal("FEC_DEPOSITO_CONFIRMACION"));
                    reporte.CUENTA = dr.GetString(dr.GetOrdinal("CUENTA"));
                    reporte.DEPOSITO_MONTO = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_MONTO"));
                    reporte.DEPOSITO_SALDO = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_SALDO"));
                    reporte.DEPOSITO_MONTO_SOLES = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_MONTO_SOLES"));
                    reporte.DEPOSITO_SALDO_SOLES = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_SALDO_SOLES"));

                    reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                    reporte.RUBRO_NOMBRE = dr.GetString(dr.GetOrdinal("RUBRO_NOMBRE"));
                    reporte.DOCUMENTO = dr.GetString(dr.GetOrdinal("DOCUMENTO"));
                    //reporte.FEC_COBRO_DETALLE = dr.GetString(dr.GetOrdinal("FEC_COBRO_DETALLE"));

                    reporte.IMPORTE_DETALLE_DEPOSITO = dr.GetDecimal(dr.GetOrdinal("IMPORTE_DETALLE_DEPOSITO"));
                    reporte.NODO_ID = dr.GetDecimal(dr.GetOrdinal("NODO_ID"));
                    reporte.NODO = dr.GetString(dr.GetOrdinal("NODO"));
                    reporte.TERRITORIO = dr.GetInt32(dr.GetOrdinal("TERRITORIO"));
                    reporte.PERIODO_CONTABLE = dr.GetString(dr.GetOrdinal("PERIODO_CONTABLE"));
                    reporte.FECHA_INI = dr.GetString(dr.GetOrdinal("FECHA_INI"));
                    reporte.FECHA_FIN = dr.GetString(dr.GetOrdinal("FECHA_FIN"));
                    lista.Add(reporte);

                }
            }
            return lista;
        }
        public List<BE_TipoCobro> ListarTipoCobro()
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_LISTAR_TIPO_COBRO");
            var lista = new List<BE_TipoCobro>();
            BE_TipoCobro obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BE_TipoCobro();
                    obs.VDESC = Convert.ToString(reader["VDESC"]);
                    obs.VALUE = Convert.ToString(reader["VALUE"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }

    }
}
