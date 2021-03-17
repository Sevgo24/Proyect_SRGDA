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
    public class DAAdministracionSeguimientoLocalPermanente
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");



        public List<BEAdministracionSeguimientoLocalPermanente> listarLicenciaSeguimiento(string Anio,decimal CodigoOficina,string CodigoModalidad,int Mesevaluar)
        {
            List<BEAdministracionSeguimientoLocalPermanente> lista = new List<BEAdministracionSeguimientoLocalPermanente>();
            BEAdministracionSeguimientoLocalPermanente entidad = null;


            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_SEGUIMIENTO_LOCALES");

                db.AddInParameter(oDbCommand, "@AN", DbType.String, Anio);
                db.AddInParameter(oDbCommand, "@OFI", DbType.Decimal, CodigoOficina);
                db.AddInParameter(oDbCommand, "@MODA", DbType.String, CodigoModalidad);
                db.AddInParameter(oDbCommand, "@MES", DbType.Int32, Mesevaluar);

                oDbCommand.CommandTimeout = 200;


                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionSeguimientoLocalPermanente();

                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_LIC")))
                            entidad.CODIGO_LIC = dr.GetDecimal(dr.GetOrdinal("CODIGO_LIC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EMISION_PROD")))
                            entidad.EMISION_PROD = dr.GetString(dr.GetOrdinal("EMISION_PROD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOCAL")))
                            entidad.LOCAL = dr.GetString(dr.GetOrdinal("LOCAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                            entidad.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RAZONE")))
                            entidad.RAZONE = dr.GetString(dr.GetOrdinal("RAZONE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENERO")))
                            entidad.ENERO = dr.GetString(dr.GetOrdinal("ENERO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FEBRERO")))
                            entidad.FEBRERO = dr.GetString(dr.GetOrdinal("FEBRERO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MARZO")))
                            entidad.MARZO = dr.GetString(dr.GetOrdinal("MARZO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ABRIL")))
                            entidad.ABRIL = dr.GetString(dr.GetOrdinal("ABRIL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MAYO")))
                            entidad.MAYO = dr.GetString(dr.GetOrdinal("MAYO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("JUNIO")))
                            entidad.JUNIO = dr.GetString(dr.GetOrdinal("JUNIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("JULIO")))
                            entidad.JULIO = dr.GetString(dr.GetOrdinal("JULIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("AGOSTO")))
                            entidad.AGOSTO = dr.GetString(dr.GetOrdinal("AGOSTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SEPTIEMBRE")))
                            entidad.SEPTIEMBRE = dr.GetString(dr.GetOrdinal("SEPTIEMBRE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OCTUBRE")))
                            entidad.OCTUBRE = dr.GetString(dr.GetOrdinal("OCTUBRE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NOVIEMBRE")))
                            entidad.NOVIEMBRE = dr.GetString(dr.GetOrdinal("NOVIEMBRE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DICIEMBRE")))
                            entidad.DICIEMBRE = dr.GetString(dr.GetOrdinal("DICIEMBRE"));

                        lista.Add(entidad);

                    }
                }
            }
            catch (Exception EX)
            {
                return null;
            }


            return lista;
        }

        public decimal Recuperar_Lic_PL_ID(decimal lic_id,string Anio,int Mesevaluar)
        {
            decimal lic_pl_id = 0;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("Recuperar_LIC_PL_ID");

                db.AddInParameter(oDbCommand, "@lic_id", DbType.Decimal, lic_id);
                db.AddInParameter(oDbCommand, "@anio", DbType.String, Anio);
                db.AddInParameter(oDbCommand, "@mes", DbType.Int32, Mesevaluar);
                oDbCommand.CommandTimeout = 200;


                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                        lic_pl_id = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                    }
                }
            }
            catch (Exception EX)
            {
               
                return 0;
            }


            return lic_pl_id;
        }
    }
}
