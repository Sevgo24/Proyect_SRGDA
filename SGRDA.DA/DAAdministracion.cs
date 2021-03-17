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
    public class DAAdministracion
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BELicencias> ListaLicenciasTrasladar(Decimal BPS_ID, decimal LIC_ID, string NOM_LIC, decimal LIC_MASTER, decimal ID_GROUP, decimal OFF_ID, decimal DIV1, decimal DIV2, decimal DIV3, decimal DIVISION,string MOD_GROUP,decimal AGE_ID,int CON_FECHA_CREACION,string FECHA_CREA_INCIAL,string FECHA_CREA_FINAL)
        {
            List<BELicencias> lista = new List<BELicencias>();
            BELicencias entidad = null;
            
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_LICENCIAS_A_TRASLADAR");
            db.AddInParameter( oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@NOM_LIC", DbType.String, NOM_LIC);
            db.AddInParameter(oDbCommand, "@LIC_MASTER", DbType.Decimal, LIC_MASTER);
            db.AddInParameter(oDbCommand, "@ID_GROUP", DbType.Decimal, ID_GROUP);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
            db.AddInParameter(oDbCommand, "@DIV1", DbType.Decimal, DIV1);
            db.AddInParameter(oDbCommand, "@DIV2", DbType.Decimal, DIV2);
            db.AddInParameter(oDbCommand, "@DIV3", DbType.Decimal, DIV3);
            db.AddInParameter(oDbCommand, "@DIVISION", DbType.Decimal, DIVISION);
            db.AddInParameter(oDbCommand, "@MOD_GROUP", DbType.String, MOD_GROUP);
            db.AddInParameter(oDbCommand, "@AGE_ID", DbType.Decimal, AGE_ID);
            db.AddInParameter(oDbCommand, "@CON_FECHA_CREACION", DbType.Int32, CON_FECHA_CREACION);
            db.AddInParameter(oDbCommand, "@FECHA_CREA_INCIAL", DbType.String, FECHA_CREA_INCIAL);
            db.AddInParameter(oDbCommand, "@FECHA_CREA_FINAL", DbType.String, FECHA_CREA_FINAL);


            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BELicencias();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                            entidad.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DAD_NAME")))
                            entidad.DIVISION = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFF_NAME")))
                            entidad.OFICINA = dr.GetString(dr.GetOrdinal("OFF_NAME"));

                        lista.Add(entidad);
                    }

                }

            }catch (Exception ex)
            {

                return null;
            }

            return lista;

        }

        public int ActualizaEstblecimientoActivo(decimal EST_ID_Activo, decimal EST_ID_INACTIVO)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ESTABLECIMIENTO_DUPLICADO");
            db.AddInParameter(oDbCommand, "@EST_ACTIVO", DbType.Decimal, EST_ID_Activo);
            db.AddInParameter(oDbCommand, "@EST_INACTIVO", DbType.Decimal, EST_ID_INACTIVO);

            int r = Convert.ToInt32( db.ExecuteScalar(oDbCommand));

            return r;
        }
        public int AtualizarFactPendiCancelado(string xmllist, string xmllistAgente, int FACT_PENID,int FACT_HISTO)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_LICENCIAS_FACTURAS_PENDIENTES_CANCELADAS");
            db.AddInParameter(oDbCommand, "@xmlLst", DbType.String, xmllist);
            db.AddInParameter(oDbCommand, "@xmlAgenLst", DbType.String, xmllistAgente);
            db.AddInParameter(oDbCommand, "@FAC_PEND", DbType.Int32, FACT_PENID);
            db.AddInParameter(oDbCommand, "@FAC_CANC", DbType.Int32, FACT_HISTO);
            int r = Convert.ToInt32( db.ExecuteScalar(oDbCommand));

            return r;
        }


    }
}
