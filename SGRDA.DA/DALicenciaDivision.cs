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

namespace SGRDA.DA
{
    public class DALicenciaDivision
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase("conexion");

        public decimal ObtenerUbigeoEstablecimiento(decimal idEstablecimiento)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_UBIGEO_ESTABLECIMIENTO");
            oDatabase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, idEstablecimiento);
            decimal validacion = Convert.ToDecimal(oDatabase.ExecuteScalar(oDbCommand));
            return validacion;
        }

        public List<BELicenciaDivision> ObtenerDivisionesXModalidad(decimal idModalidad)
        {
            List<BELicenciaDivision> lista = new List<BELicenciaDivision>();
            BELicenciaDivision item = null;
            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_DIVISIONES_X_MODALIDAD"))
            {
                oDatabase.AddInParameter(cm, "@MOD_ID", DbType.Decimal, idModalidad);
                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BELicenciaDivision();
                        item.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                        item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                        item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                        item.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                        item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                        item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                        lista.Add(item);
                    }
                }
                return lista;
            }
        }

        public int ValidarDivsionXUbigeo(decimal idDivision, decimal ubigeo_est)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_VALIDACION_UBIGEOS_X_DIVISION");
            oDatabase.AddInParameter(oDbCommand, "@DIVISION", DbType.Decimal, idDivision);
            oDatabase.AddInParameter(oDbCommand, "@UBIGEO_EST", DbType.Decimal, ubigeo_est);
            int validacion = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return validacion;
        }

        public int Insertar(string owner, decimal idLic, decimal idDivision, string usuario)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_DIV_LICENCIA");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
            oDatabase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, idDivision);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, usuario);
            int resultado = Convert.ToInt32(oDatabase.ExecuteNonQuery(oDbCommand));
            return resultado;
        }

        public int Eliminar(string owner, decimal id, decimal idLic, decimal idDivision, string usuario)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASD_DIV_LICENCIA");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@LIC_DIV_ID", DbType.Decimal, id);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
            oDatabase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, idDivision);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, usuario);
            int resultado = Convert.ToInt32(oDatabase.ExecuteNonQuery(oDbCommand));
            return resultado;
        }

        public List<BELicenciaDivision> Listar(decimal idLicencia)
        {
            List<BELicenciaDivision> lista = new List<BELicenciaDivision>();
            BELicenciaDivision item = null;
            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_DIVISIONES_X_MODALIDAD"))
            {
                oDatabase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLicencia);
                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BELicenciaDivision();
                        item.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                        item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                        item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                        item.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                        item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                        item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                        lista.Add(item);
                    }
                }
                return lista;
            }
        }

        public List<BELicenciaDivision> ListarDivisionLicencia(string owner,decimal idLicencia)
        {
            List<BELicenciaDivision> lista = new List<BELicenciaDivision>();
            BELicenciaDivision item = null;
            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_LISTAR_DIVISION_LICENCIA"))
            {
                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDatabase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLicencia);
                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BELicenciaDivision();
                        item.LIC_DIV_ID = dr.GetDecimal(dr.GetOrdinal("LIC_DIV_ID"));
                        item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                        lista.Add(item);
                    }
                }
                return lista;
            }
        }

        public int ActualizarEstado(string owner, decimal id, decimal idLic, decimal idDivision, string usuario, decimal indicador)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_DIV_LICENCIA");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@LIC_DIV_ID", DbType.Decimal, id);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
            oDatabase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, idDivision);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, usuario);
            oDatabase.AddInParameter(oDbCommand, "@INDICADOR", DbType.Decimal, indicador);
            int resultado = Convert.ToInt32(oDatabase.ExecuteNonQuery(oDbCommand));
            return resultado;
        }

    }
}
