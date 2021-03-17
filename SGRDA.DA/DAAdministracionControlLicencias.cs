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
    public class DAAdministracionControlLicencias
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

         public List<BELicencias> ListaControlLicencias(decimal LIC_ID, decimal OFF_ID, int CON_FECHA, string FECHA_INICIAL, string FECHA_FIN,int ESTADO)
        {
            List<BELicencias> lista = new List<BELicencias>();
            BELicencias entidad = null;


            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_LICENCIAS_CONTROL");
            db.AddInParameter(oDbCommand, "@LIC_ID",DbType.Decimal,LIC_ID);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
            db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, CON_FECHA);
            db.AddInParameter(oDbCommand, "@FECHA_INICIAL", DbType.String,FECHA_INICIAL);
            db.AddInParameter(oDbCommand, "@FECHA_FINAL", DbType.String, FECHA_FIN);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, ESTADO);

            try
            {
                using (IDataReader dr= db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BELicencias();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.BPS_NAME = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO")))
                            entidad.MONTO_LIRICS_BRUTO = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PRIMER_PERIODO")))
                            entidad.PERIODO_DESCRIPCION = dr.GetString(dr.GetOrdinal("PRIMER_PERIODO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA")))
                            entidad.OFICINA = dr.GetString(dr.GetOrdinal("OFICINA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CREACION")))
                            entidad.FECHA_DESCRIPCION = dr.GetString(dr.GetOrdinal("FECHA_CREACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_MODIFICACION_LIC")))
                            entidad.LIC_AUT_START = dr.GetString(dr.GetOrdinal("FECHA_MODIFICACION_LIC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CREACION_DOC")))
                            entidad.LIC_AUT_END = dr.GetString(dr.GetOrdinal("FECHA_CREACION_DOC"));


                        lista.Add(entidad);
                    }
                }

            }catch(Exception ex)
            {
                return null;
            }


            return lista;
        }


        public bool ActualizaLicenciaAprobacionLocales(decimal LIC_ID)
        {
            bool respuesta;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTUALIZA_ESTADO_FACTURACION");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            try
            {

                respuesta = Convert.ToBoolean(db.ExecuteNonQuery(oDbCommand));

            }
            catch (Exception ex)
            {
                return false;
            }

            return respuesta;
        }

        public bool ActualizaLicenciaEstadoAprobacion(decimal LIC_ID,int ESTADO)
        {
            bool respuesta;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LICENCIA_APROBACION_LOC");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, ESTADO);
            try
            {

                respuesta = Convert.ToBoolean(db.ExecuteNonQuery(oDbCommand));

            }
            catch (Exception ex)
            {
                return false;
            }

            return respuesta;
        }

    }
}
