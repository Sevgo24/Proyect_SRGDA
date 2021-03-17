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
    public class DASocioNegocioOficina
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BESocioNegocioOficina> ListarContacto(decimal offid, string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_CONTACTO_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offid);
            oDataBase.ExecuteNonQuery(oDbCommand);

            var lista = new List<BESocioNegocioOficina>();
            BESocioNegocioOficina contacto = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    contacto = new BESocioNegocioOficina();
                    contacto.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    contacto.NOMBRE = dr.GetString(dr.GetOrdinal("BPS_NAME")) + " " + dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME")) + " " + dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
                    contacto.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                    contacto.ROL = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                    contacto.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                    contacto.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        contacto.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        contacto.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        contacto.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                        contacto.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        contacto.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                    lista.Add(contacto);
                }
            }
            return lista;
        }

        public int Insertar(BESocioNegocioOficina contacto)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_CONTACTO_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, contacto.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, contacto.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, contacto.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@ROL_ID", DbType.String, contacto.ROL_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, contacto.LOG_USER_CREAT);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;

        }

        public int Actualizar(BESocioNegocioOficina contacto)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CONTACTO_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, contacto.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, contacto.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, contacto.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@ROL_ID", DbType.String, contacto.ROL_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, contacto.LOG_USER_CREAT);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BESocioNegocioOficina contacto)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CONTACTO_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, contacto.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, contacto.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, contacto.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, contacto.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(BESocioNegocioOficina contacto)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_CONTACTO_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, contacto.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, contacto.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, contacto.BPS_ID);            
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, contacto.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BESocioNegocioOficina Obtener( string owner,decimal offId, decimal bspId)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CONTACTO_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offId);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, bspId);
            oDataBase.ExecuteNonQuery(oDbCommand);

            BESocioNegocioOficina contacto = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    contacto = new BESocioNegocioOficina();
                    contacto.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    contacto.NOMBRE = dr.GetString(dr.GetOrdinal("BPS_NAME")) + " " + dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME")) + " " + dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
                    contacto.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                    contacto.ROL = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                    contacto.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                    contacto.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        contacto.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        contacto.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        contacto.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                        contacto.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        contacto.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));               
                }
            }
            return contacto;
        }

    }
}
