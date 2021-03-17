using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities;
using System.Data.Common;


namespace SGRDA.DA
{
    public class DAAdministracionNotificacionLicencias
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAdministracionNotificacionEventos> ListaNotificacionLicencias(decimal CodigoLicencia, decimal Oficina,string NombreLicencia,string NombreEstablecimiento ,int ConFecha, string FechaInicial, string FechaFin,int EstadoLicencia)
        {
            List<BEAdministracionNotificacionEventos> lista = new List<BEAdministracionNotificacionEventos>();
            BEAdministracionNotificacionEventos entidad = null;


            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_LICENCIAS_CAR");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, Oficina);
            db.AddInParameter(oDbCommand, "@NOM_LIC", DbType.String, NombreLicencia);
            db.AddInParameter(oDbCommand, "@NOM_EST", DbType.String, NombreEstablecimiento);
            db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, ConFecha);
            db.AddInParameter(oDbCommand, "@FECHA_INICIAL", DbType.String, FechaInicial);
            db.AddInParameter(oDbCommand, "@FECHA_FINAL", DbType.String, FechaFin);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, EstadoLicencia);

            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionNotificacionEventos();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.IdLicencia = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_INI_EVENT")))
                            entidad.FechaInicioEvento = dr.GetDateTime(dr.GetOrdinal("FECHA_INI_EVENT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_FIN_EVENT")))
                            entidad.FechaFinEvento = dr.GetDateTime(dr.GetOrdinal("FECHA_FIN_EVENT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            entidad.DescripcionUbigeo = dr.GetString(dr.GetOrdinal("UBIGEO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            entidad.NombreEstablecimiento = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            entidad.IdEstablecimiento = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            entidad.DescripcionDireccion = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.DescripcionEstado = dr.GetString(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTOS")))
                            entidad.DocumentosDescripcion = dr.GetString(dr.GetOrdinal("DOCUMENTOS"));


                        lista.Add(entidad);
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }


            return lista;
        }

        public int ActualizaLicenciaNotificacion(decimal CodigoLicencia, decimal CodigoModalidad, decimal CodigoTarifa, decimal CodigoEstablecimiento, decimal CodigoSocio)
        {
            int r = 0;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LICENCIA_CAR");
            db.AddInParameter(oDbCommand, "@MOD_ID",DbType.Decimal, CodigoModalidad);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, CodigoEstablecimiento);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, CodigoSocio);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, CodigoTarifa);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);

            r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
