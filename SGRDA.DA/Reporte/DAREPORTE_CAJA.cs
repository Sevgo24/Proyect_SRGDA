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
    public class DAREPORTE_CAJA
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        //creando el metodo listar

        public List<BEREPORTE_CAJA> ListarReporteCaja(string Fini, string Ffin, string oficina, int? Rubro)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_DIARIO_CAJA");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, Rubro);
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEREPORTE_CAJA>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREPORTE_CAJA reporte = null;
                while (dr.Read())
                {
                    reporte = new BEREPORTE_CAJA();
                    //no le pongo el mismo nombre que en el DS
                    reporte.rum = dr.GetString(dr.GetOrdinal("rum"));
                    reporte.serie = dr.GetInt32(dr.GetOrdinal("serie"));
                    reporte.coidtfac = dr.GetInt32(dr.GetOrdinal("coidtfac"));
                    reporte.periodo = dr.GetString(dr.GetOrdinal("periodo"));
                    //rubro as E
                    //no le pongo el mismo nombre que en el DS
                    reporte.E = dr.GetString(dr.GetOrdinal("E"));
                    reporte.importe = dr.GetDecimal(dr.GetOrdinal("importe"));
                    //fecha emicion
                    reporte.femi = dr.GetString(dr.GetOrdinal("femi"));
                    reporte.fecan = dr.GetString(dr.GetOrdinal("fecan"));
                    reporte.coidtbec = dr.GetInt32(dr.GetOrdinal("coidtbec"));
                    reporte.td = dr.GetString(dr.GetOrdinal("td"));
                    reporte.territorio = dr.GetInt32(dr.GetOrdinal("territorio"));

                    lista.Add(reporte);

                }
            }
            return lista;
        }
        //SP_VALIDAR_REPORTE_OFICINA


        public int ValidarReporteOficina(int oficina)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SP_VALIDAR_REPORTE_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OFICINA", DbType.Int32, oficina);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        //SP_VALIDAR_REPORTE_OFICINA PARA EL COMBO


        public int ValidarReporteOficinaDL(int oficina)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SP_VALIDAR_REPORTE_OFICINA_DL");
            oDataBase.AddInParameter(oDbCommand, "@OFICINA", DbType.Int32, oficina);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        // RECAUDACION
        public List<BEREPORTE_CAJA> ReporteRegistroCaja_Cobros(string Fini, string Ffin, string oficina
            , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion
            , int? Rubro, string TipoCobro)
        //, string rubros)

        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_DIARIO_CAJA_COBRO");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, Rubro);
            db.AddInParameter(oDbCommand, "@conFechaIngreso", DbType.Int32, conFechaIngreso);
            db.AddInParameter(oDbCommand, "@conFechaConfirmacion", DbType.Int32, conFechaConfirmacion);
            db.AddInParameter(oDbCommand, "@FINI_CON", DbType.DateTime, finiConfirmacion);
            db.AddInParameter(oDbCommand, "@FFIN_CON", DbType.DateTime, ffinConfirmacion);
            db.AddInParameter(oDbCommand, "@TipoCobro", DbType.String, TipoCobro);
            //db.AddInParameter(oDbCommand, "@RUBROS", DbType.String, rubros);

            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEREPORTE_CAJA>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREPORTE_CAJA reporte = null;
                while (dr.Read())
                {
                    reporte = new BEREPORTE_CAJA();
                    reporte.ANIO_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("ANIO_CANCELACION_DETALLE"));
                    reporte.MES_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("MES_CANCELACION_DETALLE"));
                    reporte.FEC_CANCELACION_DETALLE_DATE = dr.GetDateTime(dr.GetOrdinal("FEC_CANCELACION_DETALLE_DATE"));

                    reporte.FEC_CANCELACION_DETALLE = dr.GetString(dr.GetOrdinal("FEC_CANCELACION_DETALLE"));
                    reporte.ID_COBRO = dr.GetDecimal(dr.GetOrdinal("ID_COBRO"));
                    reporte.RUBRO_NOMBRE = dr.GetString(dr.GetOrdinal("RUBRO_NOMBRE"));
                    reporte.RUC = dr.GetString(dr.GetOrdinal("RUC"));
                    reporte.DOCUMENTO = dr.GetString(dr.GetOrdinal("DOCUMENTO"));
                    reporte.FEC_EMI_FACTURA = dr.GetString(dr.GetOrdinal("FEC_EMI_FACTURA"));
                    reporte.IMPORTE_DETALLE_DEPOSITO_SOLES = dr.GetDecimal(dr.GetOrdinal("IMPORTE_DETALLE_DEPOSITO_SOLES"));
                    reporte.TIPOCOBRO = dr.GetString(dr.GetOrdinal("TIPOCOBRO"));
                    lista.Add(reporte);
                }
            }
            return lista;
        }

        // CONTABLE
        public List<BEREPORTE_CAJA> ReporteContableRegistroCaja(string Fini, string Ffin, string oficina, decimal idContable, int? Rubro)
        //, string rubros)

        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_CONTABLE_DIARIO_CAJA");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@territorio", DbType.Int32, Rubro);
            db.AddInParameter(oDbCommand, "@idContable", DbType.Decimal, idContable);

            var lista = new List<BEREPORTE_CAJA>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREPORTE_CAJA reporte = null;
                while (dr.Read())
                {
                    reporte = new BEREPORTE_CAJA();
                    reporte.ANIO_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("ANIO_CANCELACION_DETALLE"));
                    reporte.MES_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("MES_CANCELACION_DETALLE"));
                    reporte.FEC_CANCELACION_DETALLE_DATE = dr.GetDateTime(dr.GetOrdinal("FEC_CANCELACION_DETALLE_DATE"));

                    reporte.FEC_CANCELACION_DETALLE = dr.GetString(dr.GetOrdinal("FEC_CANCELACION_DETALLE"));
                    reporte.ID_COBRO = dr.GetDecimal(dr.GetOrdinal("ID_COBRO"));
                    reporte.RUBRO_NOMBRE = dr.GetString(dr.GetOrdinal("RUBRO_NOMBRE"));
                    reporte.RUC = dr.GetString(dr.GetOrdinal("RUC"));
                    reporte.DOCUMENTO = dr.GetString(dr.GetOrdinal("DOCUMENTO"));
                    reporte.FEC_EMI_FACTURA = dr.GetString(dr.GetOrdinal("FEC_EMI_FACTURA"));
                    reporte.IMPORTE_DETALLE_DEPOSITO_SOLES = dr.GetDecimal(dr.GetOrdinal("IMPORTE_DETALLE_DEPOSITO_SOLES"));
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


