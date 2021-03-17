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
    public class DAAdministracionEstablecimiento
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");
        public List<BEAdministracionEstablecimiento> ListaEstablecimientosAdministracion(string owner, decimal EST_ID, string BPS_NAME, string BPS_FIRST_NAME, string BPS_FATH_SURNAME, string BPS_MOTH_SURNAME, string TAX_ID, string LOG_USER_UPDAT, int CON_FECHA_CREA, string FECHA_INI_CREA, string FECHA_FIN_CREA, int CON_FECHA_UPD, string FECHA_INI_UPD, string FECHA_FIN_UPD, decimal LIC_ID, string EST_NAME, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID)
        {
            List<BEAdministracionEstablecimiento> lista = new List<BEAdministracionEstablecimiento>();
            BEAdministracionEstablecimiento entidad = null;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_ESTABLECIMIENTO_ADMINISTRACION");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, EST_ID);
            db.AddInParameter(oDbCommand, "@BPS_NAME", DbType.String, BPS_NAME);
            db.AddInParameter(oDbCommand, "@BPS_FIRST_NAME", DbType.String, BPS_FIRST_NAME);
            db.AddInParameter(oDbCommand, "@BPS_FATH_SURNAME", DbType.String, BPS_FATH_SURNAME);
            db.AddInParameter(oDbCommand, "@BPS_MOTH_SURNAME", DbType.String, BPS_MOTH_SURNAME);
            db.AddInParameter(oDbCommand, "@TAX_ID", DbType.String, TAX_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, LOG_USER_UPDAT);
            db.AddInParameter(oDbCommand, "@CON_FECHA_CREA", DbType.Int32, CON_FECHA_CREA);
            db.AddInParameter(oDbCommand, "@FECHA_INI_CREA", DbType.String, FECHA_INI_CREA);
            db.AddInParameter(oDbCommand, "@FECHA_FIN_CREA", DbType.String, FECHA_FIN_CREA);
            db.AddInParameter(oDbCommand, "@CON_FECHA_UPD", DbType.Int32, CON_FECHA_UPD);
            db.AddInParameter(oDbCommand, "@FECHA_INI_UPD", DbType.String, FECHA_INI_UPD);
            db.AddInParameter(oDbCommand, "@FECHA_FIN_UPD", DbType.String, FECHA_FIN_UPD);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@EST_NAME", DbType.String, EST_NAME);
            db.AddInParameter(oDbCommand, "@DIV_ID", DbType.Decimal, DIV_ID);
            db.AddInParameter(oDbCommand, "@DEP_ID", DbType.Decimal, DEP_ID);
            db.AddInParameter(oDbCommand, "@PROV_ID", DbType.Decimal, PROV_ID);
            db.AddInParameter(oDbCommand, "@DIST_ID", DbType.Decimal, DIST_ID);

            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionEstablecimiento();

                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            entidad.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            entidad.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.BPS_NAME = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            entidad.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            entidad.ENDS = dr.GetString(dr.GetOrdinal("ENDS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                            entidad.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            entidad.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));

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


        public int AgruparEstablecimientos(decimal EST_ID, string xmllistaEstablecimientosInactivar, string owner, string LOG_USER_UPDAT, int ACTEST, int ACTLIC)
        {
            int res = 0;
            try
            {
                BEAdministracionEstablecimiento entidad = null;
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ESTABLECIMIENTOS_ADMINISTRACION_INACTIVAR");

                db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, EST_ID);
                db.AddInParameter(oDbCommand, "@lstxml", DbType.Xml, xmllistaEstablecimientosInactivar);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, LOG_USER_UPDAT);
                db.AddInParameter(oDbCommand, "@ACTEST", DbType.Int32, ACTEST);
                db.AddInParameter(oDbCommand, "@ACTLIC", DbType.Int32, ACTLIC);

                res = db.ExecuteNonQuery(oDbCommand);


            }
            catch (Exception ex)
            {

            }

            return res;
        }


        public int ValidaEstablecimientosModif(decimal EST_ID, string owner, string usuario)
        {
            int res = 0;
            try
            {
                BESocioAdministracion entidad = null;
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_ESTABLECIMIENTO_QUALITY");

                db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, EST_ID);
                db.AddInParameter(oDbCommand, "@usu", DbType.String, usuario);


                res = Convert.ToInt32(db.ExecuteScalar(oDbCommand));


            }
            catch (Exception ex)
            {

            }

            return res;
        }


        public int ModificaEstablecimientosporSocioSeleccionado(string xml,string owner,string usuario,decimal CodigoSocio)
        {
            int res = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_EST_ADMIN_SOCIO_SELECCIONADO");

                db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@lstxml", DbType.Xml, xml);
                db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, usuario);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, CodigoSocio);


                res = db.ExecuteNonQuery(oDbCommand);


            }
            catch (Exception ex)
            {

            }

            return res;
        }

    }
}
