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
    public class DALicenciaAforo
    {
        //Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public int Insertar(BELicenciaAforo aforo)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_LIC_AFORO");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, aforo.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.String, aforo.LIC_ID);
                oDatabase.AddInParameter(oDbCommand, "@LIC_SEC_ID", DbType.String, aforo.LIC_SEC_ID);
                oDatabase.AddInParameter(oDbCommand, "@TICKET_PRE", DbType.Boolean, aforo.TICKET_PRE);
                oDatabase.AddInParameter(oDbCommand, "@NETO_PRE", DbType.Boolean, aforo.NETO_PRE);
                oDatabase.AddInParameter(oDbCommand, "@TICKET_LIQ", DbType.Decimal, aforo.TICKET_LIQ);
                oDatabase.AddInParameter(oDbCommand, "@NETO_LIQ", DbType.Decimal, aforo.NETO_LIQ);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, aforo.LOG_USER_CREAT);

                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<BELicenciaAforo> Listar(string owner, decimal Id, string UsuarioActual)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            List<BELicenciaAforo> Lista = new List<BELicenciaAforo>();
            BELicenciaAforo LicenciaAforo = null;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_LISTAR_LIC_LOCALIDAD");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, Id);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREATE", DbType.String, UsuarioActual);

            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    LicenciaAforo = new BELicenciaAforo();
                    LicenciaAforo.ACOUNT_ID = dr.GetDecimal(dr.GetOrdinal("ACOUNT_ID"));
                    LicenciaAforo.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    LicenciaAforo.CAP_ID = dr.GetString(dr.GetOrdinal("CAP_ID"));
                    LicenciaAforo.PER_ID = dr.GetDecimal(dr.GetOrdinal("PER_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("SEC_ID")))
                        LicenciaAforo.SEC_ID = dr.GetDecimal(dr.GetOrdinal("SEC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SEC_DESC")))
                        LicenciaAforo.SEC_DESC = dr.GetString(dr.GetOrdinal("SEC_DESC"));

                    LicenciaAforo.TICKET_PRE = dr.GetDecimal(dr.GetOrdinal("SEC_TICKETS"));
                    //LicenciaAforo.NETO_PRE = dr.GetDecimal(dr.GetOrdinal("SEC_VALUE"));
                    //LicenciaAforo.TICKET_LIQ = dr.GetDecimal(dr.GetOrdinal("TICKET_LIQ"));
                    //LicenciaAforo.NETO_LIQ = dr.GetDecimal(dr.GetOrdinal("NETO_LIQ"));

                    LicenciaAforo.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    LicenciaAforo.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        LicenciaAforo.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        LicenciaAforo.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                    Lista.Add(LicenciaAforo);
                }
            }
            return Lista;
        }

        public int Actualizar(BELicenciaAforo aforo)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_LIC_AFORO");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, aforo.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, aforo.LIC_ID);
            oDatabase.AddInParameter(oDbCommand, "@ACOUNT_ID", DbType.Decimal, aforo.ACOUNT_ID);
            oDatabase.AddInParameter(oDbCommand, "@sec_TICKETS", DbType.Decimal, aforo.TICKET_PRE);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, aforo.LOG_USER_UPDATE);

            int r = oDatabase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BELicenciaAforo aforo)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASD_LIC_AFORO");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, aforo.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@CAP_LIC_ID", DbType.Decimal, aforo.ACOUNT_ID);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, aforo.LOG_USER_UPDATE);
            int r = oDatabase.ExecuteNonQuery(oDbCommand);
            return r;
        }

    }
}
