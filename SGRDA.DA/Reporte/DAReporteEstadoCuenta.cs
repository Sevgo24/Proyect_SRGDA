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
    public class DAReporteEstadoCuenta
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        //creando el metodo listar
        public List<BEReporteEstadoCuenta> ListarReporteEstadoCuenta(string Fini, string Ffin, int BPS_ID, int EST_ID, string oficina, string LIC_ID,
            int ESTADO, string oficina_nombre, string usuario)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_ESTADO_CUENTA3");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Int32, BPS_ID);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Int32, EST_ID);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.String, LIC_ID);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.String, ESTADO);
            db.AddInParameter(oDbCommand, "@NombreOficina", DbType.String, oficina_nombre);
            db.AddInParameter(oDbCommand, "@Usuario", DbType.String, usuario);
            oDbCommand.CommandTimeout = 3600;
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEReporteEstadoCuenta>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEReporteEstadoCuenta reporte = null;
                while (dr.Read())
                {
                    reporte = new BEReporteEstadoCuenta();
                    if (!dr.IsDBNull(dr.GetOrdinal("RUBRO")))
                        reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("tipo_doc")))
                        reporte.tipo_doc = dr.GetString(dr.GetOrdinal("tipo_doc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fec_emi_fact")))
                        reporte.fec_emi_fact = dr.GetString(dr.GetOrdinal("fec_emi_fact"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fec_can_fact")))
                        reporte.fec_can_fact = dr.GetString(dr.GetOrdinal("fec_can_fact"));
                    if (!dr.IsDBNull(dr.GetOrdinal("importe")))
                        reporte.importe = dr.GetDecimal(dr.GetOrdinal("importe"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ruc")))
                        reporte.ruc = dr.GetString(dr.GetOrdinal("ruc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("socio")))
                        reporte.socio = dr.GetString(dr.GetOrdinal("socio"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                        reporte.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("numero")))
                        reporte.numero = dr.GetString(dr.GetOrdinal("numero"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                        reporte.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_LICENCIA")))
                        reporte.NOMBRE_LICENCIA = dr.GetString(dr.GetOrdinal("NOMBRE_LICENCIA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        reporte.LIC_ID = dr.GetString(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                        reporte.ESTADO_SUNAT = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NC")))
                        reporte.NC = dr.GetString(dr.GetOrdinal("NC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_LOCAL")))
                        reporte.NOMBRE_LOCAL = dr.GetString(dr.GetOrdinal("NOMBRE_LOCAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                        reporte.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                    lista.Add(reporte);

                }
            }
            return lista;
        }

        public List<BEReporteEstadoCuenta> ListarReporteEstadoCuentaResumen(string Fini, string Ffin, int BPS_ID, int EST_ID, string oficina,
            string LIC_ID, int ESTADO, string oficina_nombre, string usuario)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_ESTADO_CUENTA3_RESUMEN");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Int32, BPS_ID);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Int32, EST_ID);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.String, LIC_ID);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.String, ESTADO);
            db.AddInParameter(oDbCommand, "@NombreOficina", DbType.String, oficina_nombre);
            db.AddInParameter(oDbCommand, "@Usuario", DbType.String, usuario);
            oDbCommand.CommandTimeout = 3600;
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEReporteEstadoCuenta>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEReporteEstadoCuenta reporte = null;
                while (dr.Read())
                {
                    reporte = new BEReporteEstadoCuenta();
                    if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_LICENCIA")))
                        reporte.NOMBRE_LICENCIA = dr.GetString(dr.GetOrdinal("NOMBRE_LICENCIA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RUBRO")))
                        reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("tipo_doc")))
                        reporte.tipo_doc = dr.GetString(dr.GetOrdinal("tipo_doc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("numero")))
                        reporte.numero = dr.GetString(dr.GetOrdinal("numero"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fec_emi_fact")))
                        reporte.fec_emi_fact = dr.GetString(dr.GetOrdinal("fec_emi_fact"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fec_can_fact")))
                        reporte.fec_can_fact = dr.GetString(dr.GetOrdinal("fec_can_fact"));
                    if (!dr.IsDBNull(dr.GetOrdinal("importe")))
                        reporte.importe = dr.GetDecimal(dr.GetOrdinal("importe"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                        reporte.ESTADO_SUNAT = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NC")))
                        reporte.NC = dr.GetString(dr.GetOrdinal("NC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                        reporte.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ruc")))
                        reporte.ruc = dr.GetString(dr.GetOrdinal("ruc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("socio")))
                        reporte.socio = dr.GetString(dr.GetOrdinal("socio"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                        reporte.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                    lista.Add(reporte);

                }
            }
            return lista;
        }

        public List<BEReporteEstadoCuenta> ListarReporteEstadoCuentaTransporte(string Fini, string Ffin, int BPS_ID, int EST_ID, string oficina, string LIC_ID,
   int ESTADO, string oficina_nombre, string usuario, string Modalidad)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_ESTADO_CUENTA_RACCHU");
            db.AddInParameter(oDbCommand, "@fecha1", DbType.String, Fini);
            db.AddInParameter(oDbCommand, "@fecha2", DbType.String, Ffin);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Int32, BPS_ID);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Int32, EST_ID);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, oficina);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.String, LIC_ID);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.String, ESTADO);
            db.AddInParameter(oDbCommand, "@NombreOficina", DbType.String, oficina_nombre);
            db.AddInParameter(oDbCommand, "@Usuario", DbType.String, usuario);
            db.AddInParameter(oDbCommand, "@Mod", DbType.String, Modalidad);
            oDbCommand.CommandTimeout = 3600;
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEReporteEstadoCuenta>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEReporteEstadoCuenta reporte = null;
                while (dr.Read())
                {
                    reporte = new BEReporteEstadoCuenta();
                    if (!dr.IsDBNull(dr.GetOrdinal("RUBRO")))
                        reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("tipo_doc")))
                        reporte.tipo_doc = dr.GetString(dr.GetOrdinal("tipo_doc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fec_emi_fact")))
                        reporte.fec_emi_fact = dr.GetString(dr.GetOrdinal("fec_emi_fact"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fec_can_fact")))
                        reporte.fec_can_fact = dr.GetString(dr.GetOrdinal("fec_can_fact"));
                    if (!dr.IsDBNull(dr.GetOrdinal("importe")))
                        reporte.importe = dr.GetDecimal(dr.GetOrdinal("importe"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ruc")))
                        reporte.ruc = dr.GetString(dr.GetOrdinal("ruc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("socio")))
                        reporte.socio = dr.GetString(dr.GetOrdinal("socio"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                        reporte.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("numero")))
                        reporte.numero = dr.GetString(dr.GetOrdinal("numero"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                        reporte.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_LICENCIA")))
                        reporte.NOMBRE_LICENCIA = dr.GetString(dr.GetOrdinal("NOMBRE_LICENCIA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        reporte.LIC_ID = dr.GetString(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                        reporte.ESTADO_SUNAT = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NC")))
                        reporte.NC = dr.GetString(dr.GetOrdinal("NC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_LOCAL")))
                        reporte.NOMBRE_LOCAL = dr.GetString(dr.GetOrdinal("NOMBRE_LOCAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                        reporte.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                    lista.Add(reporte);

                }
            }
            return lista;
        }

    }
}
